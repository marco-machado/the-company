// PROTOTYPE - NOT FOR PRODUCTION
// Question: Does real-time (no pause) control of 4 Chip-controlled agents feel good - split, regroup, focus-fire?
// Date: 2026-10-06

using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// Everything is built at runtime from primitives: add this to an empty scene and press Play.
public class SquadPrototype : MonoBehaviour
{
    // ---- hardcoded tuning (prototype) ----
    const float AgentSpeed = 6f, AgentRange = 11f, AgentCooldown = 0.45f, AgentDamage = 14f, AgentHp = 100f;
    const float EnemySpeed = 3.5f, EnemyRange = 9f, EnemyCooldown = 0.9f, EnemyDamage = 8f, EnemyHp = 60f, EnemyAggro = 16f;
    const float PanSpeed = 22f;

    static readonly Color Amber = new Color(1f, 0.65f, 0.1f);
    static readonly Color Magenta = new Color(0.9f, 0.1f, 0.7f);

    readonly List<Unit> agents = new List<Unit>();
    readonly List<Unit> enemies = new List<Unit>();
    readonly List<Unit> selected = new List<Unit>();
    List<Unit> groupA = new List<Unit>(), groupB = new List<Unit>();

    Camera cam;
    Vector2 dragStart;
    bool dragging;
    string result = "";

    // telemetry: observations for the playtest debrief
    int moveOrders, attackOrders, subsetOrders, focusFireOrders, regroupOrders;
    float firstSubsetAt = -1f, firstFocusAt = -1f, firstRegroupAt = -1f;

    void Awake()
    {
        BuildWorld();
        BuildUnits();
        SetSelection(agents);
    }

    // ------------------------------------------------------------------ world
    void BuildWorld()
    {
        var camGo = new GameObject("Camera", typeof(Camera), typeof(AudioListener));
        camGo.tag = "MainCamera";
        cam = camGo.GetComponent<Camera>();
        cam.backgroundColor = new Color(0.02f, 0.03f, 0.06f);
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.fieldOfView = 45f;
        cam.transform.SetPositionAndRotation(new Vector3(-8f, 30f, -26f), Quaternion.Euler(55f, 0f, 0f));

        var light = new GameObject("Light").AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.1f;
        light.transform.rotation = Quaternion.Euler(60f, 30f, 0f);

        var ground = Prim(PrimitiveType.Cube, "Ground", new Vector3(0, -0.5f, 0), new Vector3(40, 1, 40), new Color(0.12f, 0.14f, 0.18f));
        var surface = ground.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.All;
        surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;

        // outer walls
        Wall(new Vector3(0, 1.5f, 20.5f), new Vector3(42, 3, 1));
        Wall(new Vector3(0, 1.5f, -20.5f), new Vector3(42, 3, 1));
        Wall(new Vector3(20.5f, 1.5f, 0), new Vector3(1, 3, 42));
        Wall(new Vector3(-20.5f, 1.5f, 0), new Vector3(1, 3, 42));
        // interior walls: three lanes to the far side
        Wall(new Vector3(-6, 1.5f, -4), new Vector3(14, 3, 1));
        Wall(new Vector3(8, 1.5f, 6), new Vector3(1, 3, 16));
        Wall(new Vector3(-10, 1.5f, 10), new Vector3(12, 3, 1));
        Wall(new Vector3(12, 1.5f, -10), new Vector3(10, 3, 1));
        // low cover (blocks shots, not tall)
        Cover(new Vector3(0, 0.6f, 0));
        Cover(new Vector3(-3, 0.6f, 6));
        Cover(new Vector3(5, 0.6f, -6));
        Cover(new Vector3(-14, 0.6f, -10));
        Cover(new Vector3(14, 0.6f, 14));

        surface.BuildNavMesh();
    }

    void Wall(Vector3 pos, Vector3 size) => Obstacle(pos, size, new Color(0.2f, 0.22f, 0.3f));
    void Cover(Vector3 pos) => Obstacle(pos, new Vector3(2, 1.2f, 2), new Color(0.3f, 0.3f, 0.34f));

    void Obstacle(Vector3 pos, Vector3 size, Color c)
    {
        var go = Prim(PrimitiveType.Cube, "Obstacle", pos, size, c);
        var mod = go.AddComponent<NavMeshModifier>();
        mod.overrideArea = true;
        mod.area = 1; // Not Walkable
    }

    // ------------------------------------------------------------------ units
    void BuildUnits()
    {
        var starts = new[] { new Vector3(-17, 1, -17), new Vector3(-15, 1, -17), new Vector3(-17, 1, -15), new Vector3(-15, 1, -15) };
        for (int i = 0; i < 4; i++) agents.Add(MakeUnit(true, starts[i], "Agent" + (i + 1)));

        var epos = new[]
        {
            new Vector3(0, 1, 3), new Vector3(3, 1, 0),
            new Vector3(15, 1, 10), new Vector3(16, 1, 12),
            new Vector3(-12, 1, 15), new Vector3(15, 1, -15),
        };
        foreach (var p in epos) enemies.Add(MakeUnit(false, p, "Enemy"));
    }

    Unit MakeUnit(bool isAgent, Vector3 pos, string name)
    {
        var go = Prim(PrimitiveType.Capsule, name, pos, Vector3.one, isAgent ? Amber : Magenta);
        var nav = go.AddComponent<NavMeshAgent>();
        nav.baseOffset = 1f;
        nav.radius = 0.5f;
        nav.height = 2f;
        nav.speed = isAgent ? AgentSpeed : EnemySpeed;
        nav.acceleration = 40f;
        nav.angularSpeed = 720f;
        nav.stoppingDistance = 0.1f;
        nav.Warp(new Vector3(pos.x, 0, pos.z));

        var u = go.AddComponent<Unit>();
        u.isAgent = isAgent;
        u.nav = nav;
        u.maxHp = u.hp = isAgent ? AgentHp : EnemyHp;
        u.range = isAgent ? AgentRange : EnemyRange;
        u.cooldown = isAgent ? AgentCooldown : EnemyCooldown;
        u.damage = isAgent ? AgentDamage : EnemyDamage;

        if (isAgent)
        {
            u.ring = Prim(PrimitiveType.Cylinder, "Ring", Vector3.zero, new Vector3(1.8f, 0.02f, 1.8f), Color.white, false);
            u.ring.transform.SetParent(go.transform, false);
            u.ring.transform.localPosition = new Vector3(0, -0.98f, 0);
            u.ring.transform.localScale = new Vector3(1.8f, 0.02f, 1.8f);
        }
        return u;
    }

    GameObject Prim(PrimitiveType t, string name, Vector3 pos, Vector3 scale, Color c, bool collider = true)
    {
        var go = GameObject.CreatePrimitive(t);
        go.name = name;
        go.transform.position = pos;
        go.transform.localScale = scale;
        go.GetComponent<Renderer>().material.color = c;
        if (!collider) Destroy(go.GetComponent<Collider>());
        return go;
    }

    // ------------------------------------------------------------------ update
    void Update()
    {
        var m = Mouse.current;
        var k = Keyboard.current;
        if (m == null || k == null) return;

        if (k.rKey.wasPressedThisFrame) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex < 0 ? 0 : SceneManager.GetActiveScene().buildIndex); return; }

        Pan(k);
        HandleSelectionInput(m, k);
        if (m.rightButton.wasPressedThisFrame) RightClick(m.position.ReadValue(), k.leftShiftKey.isPressed);
        if (k.sKey.wasPressedThisFrame) foreach (var u in selected) { u.target = null; u.nav.ResetPath(); }

        foreach (var a in agents) if (!a.Dead) TickAgent(a);
        foreach (var e in enemies) if (!e.Dead) TickEnemy(e);

        foreach (var u in agents) if (u.ring) u.ring.SetActive(u.selected && !u.Dead);

        if (result == "")
        {
            if (enemies.All(e => e.Dead)) End("MISSION COMPLETE");
            else if (agents.All(a => a.Dead)) End("SQUAD LOST");
        }
    }

    void Pan(Keyboard k)
    {
        var d = Vector3.zero;
        if (k.upArrowKey.isPressed) d.z += 1;
        if (k.downArrowKey.isPressed) d.z -= 1;
        if (k.rightArrowKey.isPressed) d.x += 1;
        if (k.leftArrowKey.isPressed) d.x -= 1;
        cam.transform.position += d * PanSpeed * Time.deltaTime;
    }

    void HandleSelectionInput(Mouse m, Keyboard k)
    {
        bool shift = k.leftShiftKey.isPressed, ctrl = k.leftCtrlKey.isPressed || k.leftCommandKey.isPressed;
        Vector2 mp = m.position.ReadValue();

        if (m.leftButton.wasPressedThisFrame) { dragStart = mp; dragging = true; }
        if (m.leftButton.wasReleasedThisFrame && dragging)
        {
            dragging = false;
            if ((mp - dragStart).magnitude > 8f) BoxSelect(dragStart, mp, shift);
            else ClickSelect(mp, shift);
        }

        // 1-4 select an agent (shift adds), Space selects everyone
        var digits = new[] { k.digit1Key, k.digit2Key, k.digit3Key, k.digit4Key };
        for (int i = 0; i < 4; i++)
            if (digits[i].wasPressedThisFrame && !agents[i].Dead)
            {
                if (shift) { var l = new List<Unit>(selected); if (!l.Contains(agents[i])) l.Add(agents[i]); SetSelection(l); }
                else SetSelection(new List<Unit> { agents[i] });
            }
        if (k.spaceKey.wasPressedThisFrame) SetSelection(agents.Where(a => !a.Dead).ToList());

        // control groups: Ctrl+Z / Ctrl+X assign, Z / X recall
        if (k.zKey.wasPressedThisFrame) { if (ctrl) groupA = new List<Unit>(selected); else SetSelection(groupA.Where(a => !a.Dead).ToList()); }
        if (k.xKey.wasPressedThisFrame) { if (ctrl) groupB = new List<Unit>(selected); else SetSelection(groupB.Where(a => !a.Dead).ToList()); }
    }

    void ClickSelect(Vector2 mp, bool shift)
    {
        var ray = cam.ScreenPointToRay(mp);
        Unit hit = null;
        if (Physics.Raycast(ray, out var h, 200f)) hit = h.collider.GetComponentInParent<Unit>();
        if (hit != null && hit.isAgent && !hit.Dead)
        {
            var l = shift ? new List<Unit>(selected) : new List<Unit>();
            if (!l.Contains(hit)) l.Add(hit);
            SetSelection(l);
        }
        else if (!shift) SetSelection(new List<Unit>());
    }

    void BoxSelect(Vector2 a, Vector2 b, bool shift)
    {
        var r = Rect.MinMaxRect(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
        var l = shift ? new List<Unit>(selected) : new List<Unit>();
        foreach (var u in agents)
            if (!u.Dead && r.Contains(cam.WorldToScreenPoint(u.transform.position)) && !l.Contains(u)) l.Add(u);
        SetSelection(l);
    }

    void SetSelection(List<Unit> l)
    {
        selected.Clear();
        selected.AddRange(l);
        foreach (var a in agents) a.selected = selected.Contains(a);
    }

    void RightClick(Vector2 mp, bool queue)
    {
        if (selected.Count == 0) return;
        var ray = cam.ScreenPointToRay(mp);
        if (!Physics.Raycast(ray, out var h, 200f)) return;

        int alive = agents.Count(a => !a.Dead);
        float t = Time.timeSinceLevelLoad;
        var tgt = h.collider.GetComponentInParent<Unit>();

        if (tgt != null && !tgt.isAgent && !tgt.Dead)
        {
            attackOrders++;
            foreach (var u in selected) u.target = tgt;
            if (selected.Count >= 2) { focusFireOrders++; if (firstFocusAt < 0) firstFocusAt = t; }
            return;
        }

        moveOrders++;
        if (selected.Count < alive) { subsetOrders++; if (firstSubsetAt < 0) firstSubsetAt = t; }
        else if (subsetOrders > 0) { regroupOrders++; if (firstRegroupAt < 0) firstRegroupAt = t; }

        int n = selected.Count;
        for (int i = 0; i < n; i++)
        {
            var off = n == 1 ? Vector3.zero : Quaternion.Euler(0, 360f * i / n, 0) * Vector3.forward * 1.3f;
            if (NavMesh.SamplePosition(h.point + off, out var nh, 3f, NavMesh.AllAreas))
            {
                selected[i].target = null;
                selected[i].nav.SetDestination(nh.position);
            }
        }
    }

    // ------------------------------------------------------------------ unit behaviour
    void TickAgent(Unit u)
    {
        if (u.target != null && u.target.Dead) u.target = null;

        if (u.target != null)
        {
            float d = Vector3.Distance(u.transform.position, u.target.transform.position);
            if (d <= u.range && HasLos(u, u.target)) { u.nav.ResetPath(); Face(u, u.target); Fire(u, u.target); }
            else u.nav.SetDestination(u.target.transform.position);
        }
        else if (!u.nav.pathPending && u.nav.remainingDistance <= u.nav.stoppingDistance + 0.1f)
        {
            // idle: return fire at anything visible in range
            var e = Nearest(u, enemies, u.range);
            if (e != null) { Face(u, e); Fire(u, e); }
        }
    }

    void TickEnemy(Unit u)
    {
        var tgt = Nearest(u, agents, EnemyAggro);
        if (tgt != null)
        {
            u.alerted = true;
            u.lastSeen = tgt.transform.position;
            float d = Vector3.Distance(u.transform.position, tgt.transform.position);
            if (d <= u.range) { u.nav.ResetPath(); Face(u, tgt); Fire(u, tgt); }
            else u.nav.SetDestination(tgt.transform.position);
        }
        else if (u.alerted) u.nav.SetDestination(u.lastSeen);
    }

    Unit Nearest(Unit from, List<Unit> pool, float maxDist)
    {
        Unit best = null;
        float bd = maxDist;
        foreach (var p in pool)
        {
            if (p.Dead) continue;
            float d = Vector3.Distance(from.transform.position, p.transform.position);
            if (d < bd && HasLos(from, p)) { bd = d; best = p; }
        }
        return best;
    }

    bool HasLos(Unit a, Unit b)
    {
        if (!Physics.Linecast(a.transform.position, b.transform.position, out var h)) return true;
        return h.collider.GetComponentInParent<Unit>() == b;
    }

    void Face(Unit u, Unit t)
    {
        var d = t.transform.position - u.transform.position;
        d.y = 0;
        if (d.sqrMagnitude > 0.01f) u.transform.rotation = Quaternion.LookRotation(d);
    }

    void Fire(Unit u, Unit t)
    {
        if (Time.time < u.nextShot) return;
        u.nextShot = Time.time + u.cooldown;
        t.hp -= u.damage;
        Tracer(u.transform.position, t.transform.position, u.isAgent ? Amber : Magenta);
        if (t.hp <= 0) Kill(t);
    }

    void Kill(Unit t)
    {
        t.hp = 0;
        t.nav.enabled = false;
        t.gameObject.SetActive(false);
        selected.Remove(t);
        t.selected = false;
    }

    void Tracer(Vector3 a, Vector3 b, Color c)
    {
        var go = Prim(PrimitiveType.Cube, "Tracer", (a + b) * 0.5f, new Vector3(0.07f, 0.07f, Vector3.Distance(a, b)), c, false);
        go.transform.rotation = Quaternion.LookRotation(b - a);
        Destroy(go, 0.07f);
    }

    void End(string msg)
    {
        result = msg;
        Debug.Log($"[SquadControl] {msg} t={Time.timeSinceLevelLoad:F0}s moves={moveOrders} attacks={attackOrders} " +
                  $"subsetOrders={subsetOrders} focusFire={focusFireOrders} regroups={regroupOrders} " +
                  $"firstSubset={firstSubsetAt:F0}s firstFocus={firstFocusAt:F0}s firstRegroup={firstRegroupAt:F0}s " +
                  $"agentsAlive={agents.Count(a => !a.Dead)} enemiesAlive={enemies.Count(e => !e.Dead)}");
    }

    // ------------------------------------------------------------------ HUD (IMGUI)
    void OnGUI()
    {
        var white = Texture2D.whiteTexture;
        var big = new GUIStyle(GUI.skin.label) { fontSize = 28, alignment = TextAnchor.MiddleCenter };

        // hp bars + selection feedback in world space
        foreach (var u in agents.Concat(enemies))
        {
            if (u.Dead) continue;
            var sp = cam.WorldToScreenPoint(u.transform.position + Vector3.up * 1.4f);
            if (sp.z < 0) continue;
            float w = 40f, y = Screen.height - sp.y;
            GUI.color = new Color(0, 0, 0, 0.7f);
            GUI.DrawTexture(new Rect(sp.x - w / 2, y, w, 5), white);
            GUI.color = u.isAgent ? Amber : Magenta;
            GUI.DrawTexture(new Rect(sp.x - w / 2, y, w * u.hp / u.maxHp, 5), white);
        }

        // drag box
        if (dragging && Mouse.current != null)
        {
            var mp = Mouse.current.position.ReadValue();
            var a = new Vector2(Mathf.Min(dragStart.x, mp.x), Screen.height - Mathf.Max(dragStart.y, mp.y));
            var s = new Vector2(Mathf.Abs(mp.x - dragStart.x), Mathf.Abs(mp.y - dragStart.y));
            GUI.color = new Color(1f, 0.65f, 0.1f, 0.25f);
            GUI.DrawTexture(new Rect(a, s), white);
        }

        // squad strip
        GUI.color = Color.white;
        for (int i = 0; i < agents.Count; i++)
        {
            var a = agents[i];
            var r = new Rect(20 + i * 130, Screen.height - 60, 120, 44);
            GUI.color = a.Dead ? new Color(0.3f, 0, 0, 0.8f) : a.selected ? new Color(1f, 0.65f, 0.1f, 0.9f) : new Color(0, 0, 0, 0.7f);
            GUI.DrawTexture(r, white);
            GUI.color = a.selected ? Color.black : Color.white;
            GUI.Label(r, $"  [{i + 1}] {(a.Dead ? "DEAD" : Mathf.CeilToInt(a.hp) + " hp")}");
        }

        GUI.color = Color.white;
        GUI.Label(new Rect(20, 10, 900, 100),
            "LMB click/drag: select   1-4: agent (Shift adds)   Space: all   Ctrl+Z/X assign group, Z/X recall\n" +
            "RMB ground: move   RMB enemy: focus-fire   S: stop   Arrows: pan camera   R: restart\n" +
            $"Enemies left: {enemies.Count(e => !e.Dead)}   time {Time.timeSinceLevelLoad:F0}s   " +
            $"subset orders {subsetOrders}  focus-fire {focusFireOrders}  regroups {regroupOrders}");

        if (result != "") GUI.Label(new Rect(0, Screen.height / 2 - 40, Screen.width, 80), result + "  (R to restart)", big);
    }
}

public class Unit : MonoBehaviour
{
    public bool isAgent, selected, alerted;
    public float hp, maxHp, range, cooldown, damage, nextShot;
    public NavMeshAgent nav;
    public Unit target;
    public GameObject ring;
    public Vector3 lastSeen;
    public bool Dead => hp <= 0;
}
