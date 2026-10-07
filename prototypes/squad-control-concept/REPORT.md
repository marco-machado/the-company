<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Does real-time (no pause) control of 4 Chip-controlled agents feel good - split, regroup, focus-fire? -->
<!-- Date: 2026-10-07 -->

# Concept Prototype Report: Squad Control

> **Date**: 2026-10-07 (built 2026-10-06)
> **Prototype Path**: Engine (Unity 6.6 / 6000.6.4f1, C#, URP)
> **Concept File**: design/gdd/game-concept.md

---

## Hypothesis

"If the player commands four agents in real time (no pause) through one sandbox arena, they will
feel in cold, absolute command — evidenced by a tester splitting the squad, regrouping it, and
focus-firing a threat within 2 minutes without fighting the controls." Refuted if the tester
reports "babysitting" individual agents.

---

## Riskiest Assumption Tested

That real-time control of four agents with no pause is manageable — selection, group/split
commands and control groups must feel instant (StarCraft benchmark), or the core of the game fails.
This was the concept doc's top design risk and its first open question.

**Proved out.** Selection and orders felt instant; the only control friction was camera panning.

---

## Approach

A single runtime-built greybox scene: 40×40 arena, outer walls, four interior walls forming three
lanes, five low cover boxes (block shots). Four amber capsule agents on NavMeshAgents, six magenta
enemies that wake on line of sight, chase and shoot. Hitscan shots with tracers. IMGUI HUD with HP
bars, squad strip and live order counters; a telemetry line logs to the Console on mission end.

**Path chosen:** Engine
**Reason for path:** Control feel is the hypothesis; browser latency would give false results.

**Shortcuts taken (intentional):**
- All values hardcoded; one ~380-line script plus an editor menu that creates the scene
- Primitives only, no art, audio, menus or saves
- Hitscan with 100% hit chance; cover blocks line of sight entirely
- Arrow-key camera pan only; no edge pan, no zoom
- Cut: hijack, alarm/police, Chip dials, destruction, hidden objective, ship, research
- Code placed in `Assets/Prototypes/SquadControl/` (Unity compiles only `Assets/`), deviating from
  `.claude/rules/prototype-code.md`

---

## Result

Two runs by the developer. All three target behaviours occurred within 2 minutes across the runs.

| | Run 1 | Run 2 |
|---|---|---|
| Duration to mission complete | 82s | 41s |
| Move orders | 23 | 5 |
| Subset (split) orders / first at | 12 / 24s | 2 / 29s |
| Regroups / first at | 8 / 61s | 0 / — |
| Attack orders | 0 | 7 |
| Focus-fire (≥2 agents) / first at | 0 / — | 6 / 3s |
| Agents alive at end | 4/4 | 4/4 |

- **Best moment:** "focus-firing a group in the second run felt instant."
- **Worst moment:** camera movement with the arrow keys — "i expected wasd to work too."
- **Surprise:** "surprised it worked so well for a first prototype."
- In run 1 the fight was won entirely by agents' idle return fire — zero explicit attack orders.

---

## Metrics

| Metric | Value |
|--------|-------|
| Path used | Engine |
| Iterations to playable | 0 (first build compiled and ran) |
| Prototype duration | ~1 session build + 2 runs |
| Playtesters | 1 internal (developer) / 0 external |
| Feel assessment | Group focus-fire order response felt instant; selection and move orders caused no friction; arrow-key pan felt wrong (WASD expected) |
| Hypothesis verdict | CONFIRMED |

---

## Recommendation: PROCEED

Split, regroup and focus-fire were all performed within 2 minutes, the group focus-fire order felt
instant, and the only control friction reported was the camera-pan binding, not squad command
itself. The concept's top design risk — that real-time four-agent control without pause would feel
clumsy — did not materialise. Confidence is limited by a single developer tester and by a fight
that never threatened the squad (see below).

---

## If Proceeding

- **Core tuning values discovered (starting points, not final):**
  - Agents: speed 6, range 11, 14 dmg / 0.45s, 100 hp; ring formation radius 1.3 on move.
  - Enemies: speed 3.5, range 9, 8 dmg / 0.9s, 60 hp, aggro 16. **Too forgiving** — zero agent
    losses in both runs.
- **Assumptions confirmed:** real-time 4-agent squad control with no pause is manageable;
  number-key select, select-all, box-select and right-click move/attack feel instant.
- **Assumptions disproved:** none directly. Unvalidated: the push-or-retreat tension of
  *Absolute Command* — nothing in the arena forced that call.
- **Emergent mechanics / open design questions:**
  - Idle auto-return fire won run 1 without a single attack order. The GDD must decide whether it
    stays (reduces micro) or is limited (keeps fights commanded — *Firefights Are the Payoff*).
  - Camera input: player expects WASD pan, which collides with RTS-style command hotkeys (`S` =
    stop here). Decide pan scheme (WASD / edge-pan / both) and relocate command keys accordingly.
  - Control groups (Ctrl+Z/X) went unreported — unclear if used; with four agents, per-agent
    number keys may already cover the need.

**Next steps:**
1. `/design-review design/gdd/game-concept.md`
2. `/gate-check`
3. `/map-systems`
4. `/design-system squad-control` (use the tuning values and open questions above)

Before relying on this verdict for the GDD, a fresh tester and a harder enemy tuning pass would
strengthen it.

---

## Decisions (post-debrief, 2026-10-07)

Made by the developer after reviewing this report; carry into `/design-system squad-control`.

- **Idle return fire stays.** Agents automatically fire back at visible enemies in range when idle.
  Accepted tension with *Firefights Are the Payoff*; commanded focus-fire remains the way to pick
  targets.
- **Camera: WASD + edge-pan.** Both pan schemes supported. Consequence: command hotkeys must avoid
  W/A/S/D — the prototype's `S` = stop needs a new binding; full hotkey layout is a GDD task.

---

## Lessons Learned

- **What assumptions were broken by actually building this?**
  None broken; the expected risk (control clumsiness) did not appear. The real friction was camera
  input, which the concept doc never mentioned.
- **What surprised us that didn't show up in the brainstorm?**
  How quickly it worked (playable on the first build), and that idle return fire alone can win a
  fight — a design tension with commanded firefights.
- **What would we test differently next time?**
  Tune enemies to force losses so the push-or-retreat decision gets exercised; log control-group
  use; use at least one tester who did not build it.

---

> *Prototype code location: `Assets/Prototypes/SquadControl/` (docs in `prototypes/squad-control-concept/`)*
> *This code is throwaway. Never refactor into production.*
