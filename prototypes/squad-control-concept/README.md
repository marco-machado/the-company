<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Does real-time (no pause) control of 4 Chip-controlled agents feel good - split, regroup, focus-fire? -->
<!-- Date: 2026-10-06 -->

# Squad Control Prototype

**Status:** concluded — PROCEED (2026-10-07)

## Hypothesis
If the player commands four agents in real time (no pause) through one sandbox arena, they feel
in cold, absolute command. True if, within 2 minutes, a tester splits the squad, regroups it, and
focus-fires a threat without fighting the controls. Refuted if they say they were "babysitting"
agents.

## How to run
Code lives in `Assets/Prototypes/SquadControl/` (Unity compiles only `Assets/`).
1. Unity 6000.6.4f1: menu **Prototype > Squad Control > Create + Open Scene** (first run creates
   `SquadControlProto.unity`; click it again to open it later).
2. Press Play. Everything is built at runtime from primitives.

## Controls
| Input | Action |
|---|---|
| LMB click / drag box | select agent(s); Shift adds |
| 1-4 | select that agent (Shift adds); Space = all |
| Ctrl+Z / Ctrl+X, then Z / X | assign / recall control group |
| RMB ground | move selection (ring formation) |
| RMB enemy | focus-fire (agents close to range + line of sight, then shoot) |
| S | stop |
| Arrow keys | pan camera |
| R | restart |

Agents (amber) return fire when idle; they do not shoot while moving. Enemies (magenta) wake on
line of sight, chase and shoot. Console logs a telemetry line when the mission ends.

## Findings
Hypothesis CONFIRMED over 2 developer runs: split, regroup and focus-fire all within 2 minutes; group
focus-fire felt instant. Friction: arrow-key pan (WASD expected). Enemies too weak (0 losses); idle
return fire alone won one run. Full details: [REPORT.md](REPORT.md).
