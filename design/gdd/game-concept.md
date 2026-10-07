# Game Concept: The Company — Sprawl

*Created: 2026-10-06*
*Status: Draft*

---

## Elevator Pitch

> It's a real-time squad tactics game where you command four Chip-controlled
> agents from the bridge of a hovercraft, seizing a neon megacity district by
> district from rival syndicates. Every district is a systemic sandbox with
> many ways in and secrets for the curious.

---

## Core Identity

| Aspect | Detail |
| ---- | ---- |
| **Genre** | Real-time squad tactics + light strategy (territory, research) |
| **Platform** | PC (Steam / Epic) |
| **Target Audience** | Explorer-Strategists — see Target Player Profile |
| **Player Count** | Single-player |
| **Session Length** | 30–90 min (missions 5–15 min) |
| **Monetization** | Premium |
| **Estimated Scope** | Large (9–12 months, solo, first game) — release tier "One City" |
| **Comparable Titles** | Syndicate (1993), Satellite Reign, Shadow Tactics |

---

## Core Fantasy

You are the head of The Company, a criminal organization whose four agents carry
"The Chip" — an implant that gives you total control over their bodies. You never
set foot in the streets. From the bridge of your hovercraft you plan, push, and
spend your agents like the assets they are, and you watch your color spread
across the city map until there is no one left to oppose you.

The promise is **cold, absolute command paired with discovery**: you are the mind
that finds the way in that nobody else saw, and the hand that decides — without a
second thought — whether to push into a losing fight or retreat and lick your
wounds.

---

## Unique Hook

**Like Syndicate, AND ALSO every district is a small immersive-sim sandbox** —
vents, hackable doors, alarm networks, Chip-hijackable civilians and guards,
destructible cover — with hidden objectives that permanently change the city.
And you command it all from a hovercraft whose rooms *are* the game's interface.

---

## Player Experience Analysis (MDA Framework)

### Target Aesthetics (What the player FEELS)

| Aesthetic | Priority | How We Deliver It |
| ---- | ---- | ---- |
| **Discovery** (exploration, secrets) | 1 | Multi-solution districts, hidden objectives, systemic interactions to learn |
| **Challenge** (obstacle course, mastery) | 2 | Real-time squad control under pressure; rival agents and police escalation |
| **Fantasy** (make-believe, role-playing) | 3 | Head of a syndicate; Chip-controlled agents; the hovercraft as command center |
| **Sensation** (sensory pleasure) | 4 | Loud, destructive firefights; neon-on-rain lighting; strong audio feedback |
| **Narrative** (drama, story arc) | 5 | Rival organizations with named leaders; story emerging from territory war and hidden objectives |
| **Expression** (self-expression, creativity) | 6 | Agent loadouts, implant builds, personal mission approaches |
| **Submission** (relaxation, comfort zone) | N/A | Deliberately absent — real-time pressure is core |
| **Fellowship** (social connection) | N/A | Single-player |

### Key Dynamics (Emergent player behaviors)

- Players scout a district before committing, looking for the "other way in."
- Players chain systems: trigger an alarm to pull police onto a rival squad, hijack a
  guard to open a door, blow a fuel truck to clear a plaza.
- Players replay districts or return later to chase a hidden objective they spotted.
- Players push the Chip dials past safe limits when a fight turns, accepting long-term
  damage to agents for short-term survival.
- Players make cold sacrifice-or-retreat calls and live with them.

### Core Mechanics (Systems we build)

1. **Real-time squad control** — four agents moved as a group or split, with direct
   targeting and weapon switching.
2. **The Chip dials** — real-time sliders (aggression, perception, reflex) that boost
   agents at the cost of health drain and implant strain.
3. **Systemic district simulation** — a small set of deep systems: alarm and police
   response, Chip-hijacking of NPCs, destructible cover and explosives.
4. **Territory and economy** — conquered districts pay tax income; rivals contest held
   districts.
5. **Research and augmentation** — fund research into weapons, equipment, and
   cybernetic implants; equip agents on the hovercraft.

---

## Player Motivation Profile

### Primary Psychological Needs Served

| Need | How This Game Satisfies It | Strength |
| ---- | ---- | ---- |
| **Autonomy** | Route choice, dial use, sacrifice vs. retreat, research order, which district to hit next | Core |
| **Competence** | Two growth lines: player skill (squad control, reading systems) and agent power (research) | Core |
| **Relatedness** | Deliberately cold toward agents; connection comes from rival organizations and the hovercraft as home | Supporting |

### Player Type Appeal (Bartle Taxonomy)

- [x] **Achievers** — How: the city map filling with Company amber; completing the research tree; hidden-objective completion.
- [x] **Explorers** — How: multi-solution districts, hidden objectives, learning how systems interact. *Primary.*
- [ ] **Socializers** — Not served (single-player, no agent relationships by design).
- [x] **Killers/Competitors** — How: domination fantasy over rival syndicates; overwhelming late-game firepower.

### Flow State Design

- **Onboarding curve**: The first mission is the game in miniature — it introduces the
  Chip, the squad, one system interaction, one destructible moment, and one visible-but-
  optional hidden objective, taught through level design rather than tutorials.
- **Difficulty scaling**: Districts held by stronger rivals field better-equipped agents
  and faster police escalation; the player answers with research and smarter approaches.
- **Feedback clarity**: Light is information (see Visual Identity Anchor) — threats,
  objectives, and owned units are readable at a glance. Post-mission debrief shows
  approach used, losses, and secrets missed.
- **Recovery from failure**: Abort and retreat is always available; dead agents are
  replaced by recruiting new ones (losing their implants and gear). Failure costs
  resources, not the campaign.

---

## Core Loop

### Moment-to-Moment (30 seconds)
Move the squad → spot a threat or opportunity → engage (shoot, hack, Chip-hijack a
civilian or guard) → adjust the Chip dials → handle the chain reaction. It must feel
good on its own: punchy weapon feedback, destruction, panicked crowds, systems
colliding into stories.

### Short-Term (5-15 minutes)
A district mission: enter → scout → choose an approach (loud, silent, hijack,
sabotage) → complete the primary objective → optionally find hidden objectives →
extract, or abort and retreat.

### Session-Level (30-120 minutes)
On the hovercraft: check the city map on the bridge → choose a district → equip and
implant agents in the armory → run the mission → spend income on research in the lab →
recruit replacements for the dead → natural stop back on the bridge with the next
target visible.

### Long-Term Progression
Districts conquered → tax income → research tree (weapons, equipment, implants) →
harder districts held by stronger rivals → final assault on the last rival's HQ
district. The game ends when the city belongs to The Company.

### Retention Hooks
- **Curiosity**: Hidden objectives spotted but not reached; unexplored districts.
- **Investment**: A research project finishing after the next payout; heavily implanted
  veteran agents you don't want to lose.
- **Social**: None by design.
- **Mastery**: Cleaner, faster, more creative solutions to districts; rival counterattacks
  on held territory.

---

## Game Pillars

### Pillar 1: Absolute Command
The player alone decides the fate of every agent — no plot armor, no auto-retreat, no
hand-holding.

*Design test*: If we're debating auto-saving an agent vs. letting them die to a bad
call, we let them die — and make it clear the death was the player's decision.

### Pillar 2: Every Door Has Three Keys
Every objective can be reached in multiple ways that emerge from systems, and curious
players find hidden objectives.

*Design test*: If we're debating one more scripted setpiece vs. one more system
interaction, we choose the system interaction.

### Pillar 3: Firefights Are the Payoff
Combat is loud, readable, and destructive; research, implants, and approach all feed
into it.

*Design test*: If we're debating a quiet solution vs. one that leads to a better fight,
we support both but polish the fight first. (Intentional tension with Pillar 2.)

### Pillar 4: Hooked in Minute One
The first mission shows the whole game — the Chip, the squad, the systems, the
destruction — taught through play, not tutorials.

*Design test*: If we're debating a tutorial pop-up vs. a level-design lesson, the level
teaches it.

### Pillar 5: The Ship Is the Interface
Every out-of-mission screen is a room of the hovercraft — bridge, armory, lab, med bay.

*Design test*: If we're debating an abstract menu vs. a diegetic room, we choose the
room — unless it costs clarity, in which case clarity wins.

### Anti-Pillars (What This Game Is NOT)

- **NOT agent personalities, bonds, or dialogue**: would undercut *Absolute Command* —
  agents are assets; the drama belongs to the rivals.
- **NOT scripted single-solution, cutscene-driven missions**: would undercut *Every
  Door Has Three Keys*.
- **NOT open-world free roam between missions**: the ship is the hub (*The Ship Is the
  Interface*), and free roam would explode scope.
- **NOT multiplayer**: netcode would eat the time that systems and firefights need.
- **NOT random loot gating power**: research is a deliberate, commanded investment.

---

## Visual Identity Anchor

**Direction: "Neon Under Glass"** — top-down 3D, stylized neon noir.

**One-line visual rule**: *The city is dark; light is always information — danger,
value, or control.*

**Supporting principles**:
1. **Light Is Information** — every bright source means something: rival laser sights,
   alarms, objectives, your agents.
   *Design test*: decorative neon vs. readable neon → readable.
2. **Your Color Means Yours** — The Company's signature Chip amber appears only on what
   you control: agents, hijacked NPCs, held districts.
   *Design test*: using amber as decoration → never.
3. **Wet and Wrecked** — rain-slick streets multiply muzzle flashes and explosions;
   destruction leaves persistent scars.
   *Design test*: clean set dressing vs. a surface that records the firefight → the surface.

**Color philosophy**: Blue-black and wet-gray base. Chip amber belongs to The Company;
each rival owns one saturated hue (magenta, cyan, toxic green). Civilians are
desaturated. Pure red is reserved for damage and alarm states.

---

## Inspiration and References

| Reference | What We Take From It | What We Do Differently | Why It Matters |
| ---- | ---- | ---- | ---- |
| Syndicate (Bullfrog, 1993) | 4-agent squad, IPA dials, research, territory map, cold tone | Systemic multi-solution districts; diegetic ship UI | Proves the core fantasy; its audience is underserved |
| Satellite Reign | Modern Syndicate-like with open city and stealth options | Discrete handcrafted districts; louder, more destructive firefights | Validates crowdfunded demand for a successor |
| Shadow Tactics / Desperados III | Multi-solution real-time tactics levels | Firefights are the payoff, not a failure state | Validates the multi-solution level audience |
| Hitman (2016+) | Systemic sandboxes, hidden opportunities, replay for secrets | Squad of four, loud solutions first-class | Validates discovery-driven replay |
| StarCraft | Snappy group selection, control groups and hotkeys; instant unit response; factions with distinct visual and tactical identities | Four agents, not armies; no base building; rivals are AI syndicates, not players | Benchmark for how real-time squad control must *feel* — the top design risk |
| Diablo | Top-down click-to-move/attack responsiveness; visceral hit feedback; dark atmosphere; build variety | Power comes from commanded research and implants, not random loot (anti-pillar) | Benchmark for moment-to-moment combat juice from a top-down camera |

**Non-game inspirations**: *The Matrix* (the hovercraft as command ship, operators
guiding agents remotely), *Ghost in the Shell* (cybernetic bodies, hacking people),
*Blade Runner* (rain, neon, corporate darkness).

---

## Target Player Profile

| Attribute | Detail |
| ---- | ---- |
| **Age range** | 25–45 |
| **Gaming experience** | Mid-core to hardcore |
| **Time availability** | 30–90 min weeknight sessions; longer weekend runs |
| **Platform preference** | PC, mouse and keyboard |
| **Current games they play** | StarCraft, Diablo, Shadow Tactics, Hitman |
| **What they're looking for** | A true Syndicate successor with real multi-solution missions |
| **What would turn them away** | Clumsy squad control, single-solution levels, hand-holding |

---

## Technical Considerations

| Consideration | Assessment |
| ---- | ---- |
| **Recommended Engine** | Undecided — `/setup-engine` will decide. Godot 4.6 reference docs already present; PC-only 3D target fits any of the three. |
| **Key Technical Challenges** | Systemic NPC AI (alarm, police, civilian panic); destruction; real-time squad pathing; many dynamic lights with rain reflections |
| **Art Style** | 3D stylized (top-down, neon noir) |
| **Art Pipeline Complexity** | Medium-high — custom low/mid-poly 3D; mood carried by lighting and post-processing |
| **Audio Needs** | Moderate-to-adaptive — punchy weapons and destruction; music shifting with alarm state |
| **Networking** | None |
| **Content Volume** | 6 districts, 2 rival organizations, ~10 weapons, ~10 implants, 4 ship rooms, 6–10 hours |
| **Procedural Systems** | None planned — districts are handcrafted; emergence comes from systems |

---

## Risks and Open Questions

### Design Risks
- Real-time control of four agents may feel clumsy — the prototype must prove squad
  control feels good.
- Systemic sandboxes may produce one dominant approach, undercutting *Every Door Has
  Three Keys*.
- Cold, interchangeable agents may reduce stakes — loss must hurt through lost
  investment (implants, gear), not attachment.

### Technical Risks
- Systemic AI plus destruction is the hardest engineering in the game, and this is a
  first game.
- Neon lighting and rain reflections at scale need early performance budgets.

### Market Risks
- Niche audience (loyal but small).
- Comparison to Satellite Reign and the Syndicate legacy sets high expectations.

### Scope Risks
- Handcrafted multi-solution districts are the slowest content to produce.
- Number of system interactions grows combinatorially — keep the system set small and deep.

### Open Questions
- Does raw real-time squad control (no pause) feel good with four agents? → Answer with
  the MVP prototype.
- How do the Chip dials balance against research power? → Prototype, then `/design-system`.
- How do rivals contest held territory without a full strategy layer? → `/map-systems`.

---

## MVP Definition

**Core hypothesis**: Commanding four Chip-controlled agents through one systemic
district — with at least three viable approaches and a hidden objective — is fun in
real time.

**Required for MVP**:
1. One district with 3+ viable approaches and 1 hidden objective
2. Real-time 4-agent squad control (group and split) with 3 weapons
3. Alarm and police response system
4. Chip-hijack of civilians and guards
5. Chip dials (boost with cost)
6. Destructible cover and explosives
7. Extract or abort-and-retreat

**Explicitly NOT in MVP** (defer to later):
- Hovercraft rooms, research tree, territory and economy
- Rival organizations as strategic actors
- Multiple districts, final art

### Scope Tiers (if budget/time shrinks)

| Tier | Content | Features | Timeline |
| ---- | ---- | ---- | ---- |
| **MVP** | 1 district | Squad control, 3 weapons, alarm/police, hijack, dials, destruction, extract/abort | ~6–8 weeks |
| **Vertical Slice** | 1 polished district | MVP + hovercraft (bridge, armory, lab), ~8-node research tree, 1 rival | ~4 months |
| **Alpha** | 6 districts, placeholder art | Full ship, full research tree, 2 rivals, territory and income | ~8 months |
| **Full Vision (release: One City)** | 6 polished districts | All features polished; game ends when the city is The Company's | ~9–12 months |

*Post-release (out of scope for this concept)*: world map with multiple cities and 4+
rivals — the original "conquer the world" vision, as a sequel or expansion.

---

## Next Steps

- [ ] Configure the engine (`/setup-engine`)
- [ ] **Prototype the core idea** (`/prototype squad-control`) — validate real-time 4-agent control in one sandbox district
- [ ] Create the art bible (`/art-bible`) from the Visual Identity Anchor
- [ ] If prototype PROCEEDS: decompose into systems (`/map-systems`)
- [ ] Design each system (`/design-system [system-name]`)
- [ ] Build vertical slice in Pre-Production (`/vertical-slice`)
- [ ] Validate core loop with playtest (`/playtest-report`)
- [ ] Plan first milestone (`/sprint-plan new`)
