# Tasks: Climate and Weather Subsystem

**Input**: Design documents from `/specs/1-climate-weather/`  
**Prerequisites**: plan.md, spec.md  
**Branch**: `1-climate-weather`

**Tests**: Test tasks are included per the specification requirements for validation.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4, US5)
- Include exact file paths in descriptions

## Path Conventions

- Core logic: `Code/Roguelike.Core/`
- UI: `Code/Roguelike.Console/`
- Tests: `Code/Roguelike.Tests/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Configuration and shared weather infrastructure

- [X] T001 Create WeatherBalance.cs configuration class in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [X] T002 Add Weather property to Balance.cs in Code/Roguelike.Core/Configuration/Balance.cs
- [X] T003 [P] Create LanguageWeather.cs localization class in Code/Roguelike.Core/Localization/LanguageWeather.cs
- [X] T004 [P] Add Weather property to Language.cs in Code/Roguelike.Core/Localization/Language.cs
- [X] T005 Add wind direction utilities to Direction.cs in Code/Roguelike.Core/Direction.cs

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core weather infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [X] T006 Enhance Weather.cs with seasonal/time-based temperature calculation in Code/Roguelike.Core/Weather.cs
- [X] T007 Add precipitation strength levels to Weather.cs in Code/Roguelike.Core/Weather.cs
- [X] T008 Add sun visibility state to Weather.cs in Code/Roguelike.Core/Weather.cs
- [X] T009 Implement weather transition logic in Weather.cs in Code/Roguelike.Core/Weather.cs
- [X] T010 Add temperature comfort ranges to Race class or creature definitions in Code/Roguelike.Core/Race.cs
- [X] T011 Integrate weather update timing into World.DoOneStep() in Code/Roguelike.Core/World.cs

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Temperature Effects on Gameplay (Priority: P1) 🎯 MVP

**Goal**: Implement temperature-based survival mechanics with seasonal/time variation, indoor/outdoor differences, and character consequences (cold, frostbite, heat exhaustion)

**Independent Test**: Place character in freezing conditions without warm clothing and verify cold/frostbite effects. Place character in extreme heat and verify heat exhaustion. Verify indoor spaces with fires are warmer than outdoors. Test liquid freezing at 0°C.

### Tests for User Story 1

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T012 [P] [US1] Create WeatherTest.cs with temperature calculation tests in Code/Roguelike.Tests/WeatherTest.cs
- [ ] T013 [P] [US1] Create TemperatureEffectsTest.cs with cold/heat consequence tests in Code/Roguelike.Tests/TemperatureEffectsTest.cs
- [ ] T014 [P] [US1] Create IndoorClimateTest.cs with microclimate balance tests in Code/Roguelike.Tests/IndoorClimateTest.cs

### Implementation for User Story 1

- [ ] T015 [US1] Implement temperature calculation formula in Weather.cs using Time.Season and Time.TimeOfDay in Code/Roguelike.Core/Weather.cs
- [ ] T016 [US1] Add indoor microclimate balance to CellEnvironment.cs for InteriorCellEnvironment in Code/Roguelike.Core/CellEnvironment.cs
- [ ] T017 [US1] Add temperature exposure tracking to State.cs aspect in Code/Roguelike.Core/Aspects/State.cs
- [ ] T018 [US1] Implement temperature tolerance checks in Alive.cs PassTime() method in Code/Roguelike.Core/Objects/Alive.cs
- [ ] T019 [US1] Add cold-related consequences (catching cold, frostbite) to State.cs in Code/Roguelike.Core/Aspects/State.cs
- [ ] T020 [US1] Add heat-related consequences (heat exhaustion, overheating) to State.cs in Code/Roguelike.Core/Aspects/State.cs
- [ ] T021 [US1] Implement liquid freezing logic (water below 0°C) in appropriate entity classes in Code/Roguelike.Core/
- [ ] T022 [US1] Implement liquid boiling logic (water above 100°C) in appropriate entity classes in Code/Roguelike.Core/
- [ ] T023 [US1] Add temperature display to ConsoleUi.cs in Code/Roguelike.Console/ConsoleUi.cs
- [ ] T024 [US1] Add localization strings for temperature descriptors in LanguageWeather.cs in Code/Roguelike.Core/Localization/LanguageWeather.cs

**Checkpoint**: At this point, User Story 1 should be fully functional - temperature affects character survival, indoor/outdoor differences work, liquids freeze/boil

---

## Phase 4: User Story 5 - Indoor Microclimate (Priority: P2)

**Goal**: Enable players to create comfortable indoor spaces through doors and fire sources that protect from external weather

**Independent Test**: Build indoor space with closed doors and fire source, verify temperature is significantly warmer than outside. Test with open doors and verify minimal protection. Test precipitation protection indoors.

**Note**: Implemented alongside US1 as they share infrastructure, but tested independently

### Tests for User Story 5

- [ ] T025 [P] [US5] Add door open/closed tests to IndoorClimateTest.cs in Code/Roguelike.Tests/IndoorClimateTest.cs
- [ ] T026 [P] [US5] Add fire source heating tests to IndoorClimateTest.cs in Code/Roguelike.Tests/IndoorClimateTest.cs

### Implementation for User Story 5

- [ ] T027 [US5] Implement door status tracking for indoor temperature calculation in CellEnvironment.cs in Code/Roguelike.Core/CellEnvironment.cs
- [ ] T028 [US5] Implement fire source detection and heating effect in CellEnvironment.cs in Code/Roguelike.Core/CellEnvironment.cs
- [ ] T029 [US5] Add heat dissipation rates to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T030 [US5] Implement indoor precipitation protection logic in CellEnvironment.cs in Code/Roguelike.Core/CellEnvironment.cs
- [ ] T031 [US5] Add UI feedback for indoor climate in ConsoleUi.cs in Code/Roguelike.Console/ConsoleUi.cs

**Checkpoint**: Indoor spaces now provide meaningful shelter from temperature and precipitation when properly designed

---

## Phase 5: User Story 2 - Precipitation and Weather Conditions (Priority: P2)

**Goal**: Implement rain, snow, and thunderstorms with clothing wetness, item damage (paper/metal), and lightning strikes

**Independent Test**: Expose character without cloak to rain and verify wetness/sickness. Test paper item damage in heavy rain. Test metal rust in prolonged rain. Verify lightning strikes during thunderstorms with increased probability for metal carriers.

### Tests for User Story 2

- [ ] T032 [P] [US2] Add precipitation tests to WeatherTest.cs in Code/Roguelike.Tests/WeatherTest.cs
- [ ] T033 [P] [US2] Create WetnessTest.cs for clothing wetness and sickness tests in Code/Roguelike.Tests/WetnessTest.cs
- [ ] T034 [P] [US2] Create ItemDamageTest.cs for rust and paper damage tests in Code/Roguelike.Tests/ItemDamageTest.cs

### Implementation for User Story 2

- [ ] T035 [P] [US2] Add wetness status tracking to State.cs aspect in Code/Roguelike.Core/Aspects/State.cs
- [ ] T036 [P] [US2] Implement rain/snow determination based on temperature in Weather.cs in Code/Roguelike.Core/Weather.cs
- [ ] T037 [US2] Implement clothing wetness accumulation in State.cs for characters exposed to rain in Code/Roguelike.Core/Aspects/State.cs
- [ ] T038 [US2] Implement sickness risk increase for wet clothing in State.cs in Code/Roguelike.Core/Aspects/State.cs
- [ ] T039 [US2] Add drying rate calculation (near fire/indoors) to State.cs in Code/Roguelike.Core/Aspects/State.cs
- [ ] T040 [US2] Add rust damage tracking to Item.cs for metal materials in Code/Roguelike.Core/Items/Item.cs
- [ ] T041 [US2] Add water damage tracking to Item.cs for paper materials in Code/Roguelike.Core/Items/Item.cs
- [ ] T042 [US2] Implement lightning strike probability calculation in Weather.cs in Code/Roguelike.Core/Weather.cs
- [ ] T043 [US2] Implement lightning strike effects on characters in Alive.cs in Code/Roguelike.Core/Objects/Alive.cs
- [ ] T044 [US2] Add precipitation visual feedback to ConsoleUi.cs in Code/Roguelike.Console/ConsoleUi.cs
- [ ] T045 [US2] Add localization strings for precipitation types in LanguageWeather.cs in Code/Roguelike.Core/Localization/LanguageWeather.cs
- [ ] T046 [US2] Add wetness accumulation rates to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T047 [US2] Add rust/damage rates to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs

**Checkpoint**: Precipitation now affects character wetness, item durability, and creates lightning strike risk

---

## Phase 6: User Story 3 - Wind Effects on Projectiles and Objects (Priority: P3)

**Goal**: Implement wind that deflects projectiles, moves lightweight objects, and hinders movement during storms

**Independent Test**: Shoot arrow in strong wind and verify trajectory deflection and hit rate reduction. Place lightweight object outdoors and verify wind movement. Test character movement speed reduction in strong winds.

### Tests for User Story 3

- [ ] T048 [P] [US3] Add wind projectile deflection tests to Fighter aspect tests in Code/Roguelike.Tests/FighterTest.cs
- [ ] T049 [P] [US3] Create WindEffectsTest.cs for object movement and character speed tests in Code/Roguelike.Tests/WindEffectsTest.cs

### Implementation for User Story 3

- [ ] T050 [P] [US3] Implement projectile deflection calculation based on wind in Fighter.cs aspect in Code/Roguelike.Core/Aspects/Fighter.cs
- [ ] T051 [P] [US3] Add wind deflection formula to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T052 [US3] Implement lightweight object movement logic in Object.cs or Cell.cs in Code/Roguelike.Core/Object.cs
- [ ] T053 [US3] Implement character movement speed reduction in strong winds in movement action handlers in Code/Roguelike.Core/
- [ ] T054 [US3] Add object weight/stability thresholds to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T055 [US3] Add wind visual indicators to ConsoleUi.cs in Code/Roguelike.Console/ConsoleUi.cs
- [ ] T056 [US3] Add localization strings for wind strength in LanguageWeather.cs in Code/Roguelike.Core/Localization/LanguageWeather.cs

**Checkpoint**: Wind now creates tactical considerations for ranged combat and affects object/character movement

---

## Phase 7: User Story 4 - Sunlight and Visibility (Priority: P3)

**Goal**: Implement sun obscurement by clouds that reduces vision range, affecting observation and tactics

**Independent Test**: Compare character vision range during clear sunny conditions versus overcast/stormy conditions. Verify 20-30% visibility reduction in storms.

### Tests for User Story 4

- [ ] T057 [P] [US4] Create VisibilityTest.cs for sun obscurement and vision range tests in Code/Roguelike.Tests/VisibilityTest.cs

### Implementation for User Story 4

- [ ] T058 [P] [US4] Add sun visibility calculation to Weather.cs in Code/Roguelike.Core/Weather.cs
- [ ] T059 [US4] Implement vision range modification based on weather in Camera.cs aspect in Code/Roguelike.Core/Aspects/Camera.cs
- [ ] T060 [US4] Add visibility multipliers to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T061 [US4] Add visual feedback for reduced visibility to ConsoleUi.cs in Code/Roguelike.Console/ConsoleUi.cs
- [ ] T062 [US4] Add localization strings for sun visibility states in LanguageWeather.cs in Code/Roguelike.Core/Localization/LanguageWeather.cs

**Checkpoint**: Weather conditions now affect tactical awareness through visibility reduction

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T063 [P] Add weather status display to Hero.cs UI feedback in Code/Roguelike.Core/Objects/Hero.cs
- [ ] T064 [P] Add weather-related key handlers to Program.cs in Code/Roguelike.Console/Program.cs
- [ ] T065 Code review and refactoring for weather system consistency
- [ ] T066 Performance optimization for weather calculations across large regions
- [ ] T067 [P] Update XML serialization for weather state in Save.cs in Code/Roguelike.Core/Save.cs
- [ ] T068 [P] Add comprehensive weather balance tuning values to WeatherBalance.cs in Code/Roguelike.Core/Configuration/WeatherBalance.cs
- [ ] T069 Manual gameplay testing for weather feel and balance per plan.md testing strategy
- [ ] T070 Validate all 10 success criteria from spec.md (SC-001 through SC-010)

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phases 3-7)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3)
- **Polish (Phase 8)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1) - Temperature**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 5 (P2) - Indoor Microclimate**: Shares infrastructure with US1, tested independently but implemented alongside
- **User Story 2 (P2) - Precipitation**: Can start after Foundational (Phase 2) - Independent but enhanced by US1 temperature
- **User Story 3 (P3) - Wind**: Can start after Foundational (Phase 2) - Independent, no dependencies on other stories
- **User Story 4 (P3) - Visibility**: Can start after Foundational (Phase 2) - Independent, no dependencies on other stories

### Within Each User Story

- Tests MUST be written and FAIL before implementation
- Configuration/balance before logic implementation
- Aspect modifications before object usage
- Core implementation before UI display
- Localization strings alongside implementation
- Story complete before moving to next priority

### Parallel Opportunities

#### Phase 1 (Setup)
- T003 and T004 (Localization) can run in parallel with T001 and T002 (Configuration)
- T005 (Direction utilities) can run in parallel with all others

#### Phase 2 (Foundational)
- After T006 completes, T007, T008, T009 can run in parallel
- T010 can run in parallel with T006-T009
- T011 must wait for T006-T009 to complete

#### User Story 1 (Phase 3)
- All three test files (T012, T013, T014) can be created in parallel
- After tests, T015 must complete first
- Then T016, T017, T024 can run in parallel
- Then T018, T019, T020, T021, T022 implementation tasks
- Finally T023 UI

#### User Story 5 (Phase 4)
- T025, T026 tests can run in parallel
- T027, T028, T029 can run in parallel
- T030, T031 can run in parallel

#### User Story 2 (Phase 5)
- T032, T033, T034 tests can run in parallel
- T035, T036 can run in parallel
- After T035 completes: T037, T038, T039 run sequentially
- After T036 completes: T040, T041, T042 can run in parallel
- T043, T044, T045, T046, T047 implementation tasks

#### User Story 3 (Phase 6)
- T048, T049 tests can run in parallel
- T050, T051 can run in parallel
- T052, T053, T054 can run in parallel
- T055, T056 can run in parallel

#### User Story 4 (Phase 7)
- T058, T059, T060 can run in parallel after T057 test
- T061, T062 can run in parallel

#### Phase 8 (Polish)
- T063, T064, T067, T068 can all run in parallel
- T065, T066 depend on implementation completion
- T069, T070 are final validation tasks

### MVP Scope (Recommended First Release)

**User Story 1 (Temperature) + User Story 5 (Indoor Microclimate)** = Complete survivable weather system

This delivers:
- ✅ Temperature-based survival mechanics (cold, heat, frostbite)
- ✅ Seasonal and time-of-day variation
- ✅ Indoor shelter protection with doors and fires
- ✅ Liquid state changes (freezing, boiling)
- ✅ Complete gameplay loop: survive → seek shelter → build fire → survive

Later increments add:
- **+US2**: Precipitation, wetness, item damage, lightning
- **+US3**: Wind effects on combat and movement
- **+US4**: Visibility reduction

---

## Task Summary

**Total Tasks**: 70
- **Phase 1 (Setup)**: 5 tasks
- **Phase 2 (Foundational)**: 6 tasks (BLOCKING)
- **Phase 3 (US1 - Temperature)**: 13 tasks (P1 - MVP)
- **Phase 4 (US5 - Indoor Microclimate)**: 7 tasks (P2 - MVP)
- **Phase 5 (US2 - Precipitation)**: 16 tasks (P2)
- **Phase 6 (US3 - Wind)**: 9 tasks (P3)
- **Phase 7 (US4 - Visibility)**: 6 tasks (P3)
- **Phase 8 (Polish)**: 8 tasks

**Parallel Opportunities**: 35+ tasks marked [P] can run in parallel within their phase

**Independent Test Criteria**: Each user story has clear test criteria and can be validated independently

**MVP Recommendation**: Phases 1-4 (Setup → Foundational → US1 → US5) = 31 tasks for complete temperature survival system
