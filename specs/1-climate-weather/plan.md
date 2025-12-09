# Implementation Plan: Climate and Weather Subsystem

**Branch**: `1-climate-weather` | **Date**: 2025-12-09 | **Spec**: [spec.md](spec.md)  
**Input**: Feature specification from `/specs/1-climate-weather/spec.md`

## Summary

Implement a comprehensive climate and weather subsystem that tracks temperature, precipitation, wind, and sunlight conditions. The system must affect character survival (temperature tolerance, wetness, sickness), tactical gameplay (projectile deflection, visibility), and equipment durability (rust, paper damage). Indoor microclimates provide shelter through doors and fire sources. The existing `Weather` class provides foundation structure but requires complete implementation of seasonal/time-based temperature calculation, precipitation effects, wind mechanics, and indoor climate balance.

## Technical Context

**Language/Version**: C# 8.0 / .NET Standard 2.1  
**Primary Dependencies**: None (Core library has zero external dependencies per constitution)  
**Storage**: In-memory game state, XML serialization for saved games  
**Testing**: NUnit 3.x (existing test framework in Roguelike.Tests project)  
**Target Platform**: Cross-platform (.NET Standard 2.1 compatible)  
**Project Type**: Single solution with Core library + Console UI + Tests  
**Performance Goals**: Real-time turn-based game simulation, weather updates per game tick (~60 ticks/second max)  
**Constraints**: Must maintain aspect-based design, zero Core dependencies, localization-first approach  
**Scale/Scope**: Single-player game, weather affects entire regions (100x100 cells typical), thousands of objects per region

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### ✅ Aspect-Based Design
**Status**: PASS  
**Rationale**: Weather is a property of Region/CellEnvironment, not an aspect of individual objects. Character temperature tolerance will be data on creature definitions (Race class), not new aspects. Effects (wet clothing, temperature exposure) use existing State aspect. No new aspects required - leverages existing Fighter, State, Inventory aspects.

### ✅ Separation of Concerns  
**Status**: PASS  
**Rationale**: All weather logic in Roguelike.Core (Weather, CellEnvironment classes). UI (Roguelike.Console) only displays weather effects through existing rendering system. No UI dependencies in Core.

### ✅ Localization-First
**Status**: PASS  
**Rationale**: Weather descriptions, temperature-related messages, status effects all use existing Language system. New LanguageWeather class needed in Roguelike.Core.Localization for weather-specific strings (precipitation types, temperature descriptions, wind strength descriptors).

### ✅ Event-Driven Updates
**Status**: PASS  
**Rationale**: Existing `Weather.Changed` event notifies observers of weather transitions. Character state changes (wet, cold, overheated) trigger existing State events. CellEnvironment changes use existing Cell.ViewChanged event for temperature-affected visuals (frozen water, etc.).

### ✅ Action Result Pattern
**Status**: PASS  
**Rationale**: Weather effects integrated into existing action processing. Temperature damage applied during `State.PassTime()`. No new action types needed - weather modifies existing actions (movement in wind, projectile shooting in wind use existing ActionResult pattern).

## Project Structure

### Documentation (this feature)

```text
specs/1-climate-weather/
├── plan.md              # This file
├── research.md          # Phase 0 output (weather algorithms, balance formulas)
├── data-model.md        # Phase 1 output (Weather, CellClimate, temperature ranges)
├── quickstart.md        # Phase 1 output (dev guide for weather system)
├── contracts/           # Phase 1 output (empty - no external APIs)
└── checklists/
    └── requirements.md  # Already exists (specification validation)
```

### Source Code (repository root)

```text
Code/
├── Roguelike.Core/
│   ├── Weather.cs                    # MODIFY: Implement seasonal/time temperature calculation
│   ├── CellEnvironment.cs            # MODIFY: Add indoor microclimate balance
│   ├── Cell.cs                       # REVIEW: May need temperature caching
│   ├── Region.cs                     # REVIEW: Weather update integration
│   ├── Time.cs                       # REVIEW: Season/time of day access
│   ├── Direction.cs                  # MODIFY: Add wind direction utilities
│   ├── Aspects/
│   │   ├── State.cs                  # MODIFY: Add temperature exposure tracking
│   │   ├── Fighter.cs                # MODIFY: Wind affects projectiles
│   │   └── Camera.cs                 # MODIFY: Weather affects visibility range
│   ├── Objects/
│   │   ├── Alive.cs                  # REVIEW: Temperature tolerance checks
│   │   └── Hero.cs                   # REVIEW: Temperature UI feedback
│   ├── Configuration/
│   │   ├── Balance.cs                # MODIFY: Add WeatherBalance property
│   │   └── WeatherBalance.cs         # NEW: Temperature ranges, effect rates
│   ├── Localization/
│   │   ├── Language.cs               # MODIFY: Add Weather property
│   │   └── LanguageWeather.cs        # NEW: Weather strings, descriptions
│   └── Items/
│       └── Item.cs                   # REVIEW: Rust/wetness damage tracking
│
├── Roguelike.Console/
│   ├── ConsoleUi.cs                  # MODIFY: Display weather in UI
│   └── Program.cs                    # REVIEW: Weather-related key handlers
│
└── Roguelike.Tests/
    ├── WeatherTest.cs                # NEW: Weather calculation tests
    ├── TemperatureEffectsTest.cs     # NEW: Temperature consequence tests
    └── IndoorClimateTest.cs          # NEW: Microclimate balance tests
```

**Structure Decision**: Extends existing single-project structure. Weather is a Core domain concept that fits naturally alongside existing Region/Cell/Time classes. No new projects needed - all code in Roguelike.Core with UI rendering in Roguelike.Console following established patterns.

## Complexity Tracking

> No Constitution violations - table not needed

---

## Phase 0: Research & Technical Decisions

### Research Tasks

1. **Temperature Calculation Algorithm**
   - **Question**: How to calculate realistic temperature variations by season, time of day, and location?
   - **Research**: Temperature formulas for roguelike games, sinusoidal day/night cycles, seasonal temperature curves
   - **Output**: Formula for `Weather.Temperature` based on `Time.Season`, `Time.TimeOfDay`, region base temperature

2. **Indoor Microclimate Balance**
   - **Question**: How to model heat transfer between indoor/outdoor spaces through doors and from fire sources?
   - **Research**: Simplified thermodynamics for games, heat dissipation rates, door open/closed effects
   - **Output**: Algorithm for `InteriorCellEnvironment` temperature calculation considering outdoor temp, door status, fire sources

3. **Wind-Projectile Interaction**
   - **Question**: How to calculate projectile deflection based on wind speed and direction?
   - **Research**: Ballistics simulation in games, simplified physics for arrows/bolts, reasonable deflection amounts
   - **Output**: Formula in `Fighter` aspect to modify projectile trajectory based on `Weather.WindSpeed` and `Weather.WindDirection`

4. **Temperature Tolerance Ranges**
   - **Question**: What are reasonable comfortable/min/max temperature values for different creature types?
   - **Research**: Human temperature tolerance, fantasy creature adaptations (Nordmen cold-resistant, Junglemen heat-adapted)
   - **Output**: Temperature range definitions per Race in balance configuration

5. **Precipitation Effects Timing**
   - **Question**: How quickly should clothing get wet, items rust, characters get sick from exposure?
   - **Research**: Status effect accumulation rates in roguelikes, reasonable gameplay timescales
   - **Output**: Time constants in `WeatherBalance` for wetness accumulation, rust damage rates, sickness probability

6. **Lightning Strike Mechanics**
   - **Question**: What probability formula makes lightning strikes feel rare but meaningful during thunderstorms?
   - **Research**: Random event probability in roguelikes, risk/reward balance for metal items
   - **Output**: Lightning strike probability calculation based on storm strength, metal items carried, elevation

7. **Visibility Reduction Formula**
   - **Question**: How much should visibility decrease during different weather conditions?
   - **Research**: Fog of war mechanics, weather effects on perception in games
   - **Output**: Vision range multiplier formula based on `Weather.VisibilityBonus` and sun obscurement

### Research Output Location

All research findings documented in `specs/1-climate-weather/research.md`

---

## Phase 1: Design & Contracts

### Data Model

**Location**: `specs/1-climate-weather/data-model.md`

**Entities to Define**:

1. **Weather** (existing class - modifications)
   - Add seasonal/time-based temperature calculation
   - Enhance precipitation with strength levels
   - Add sun visibility state
   - Document weather transition logic

2. **WeatherBalance** (new configuration class)
   - Temperature ranges per season
   - Temperature variation by time of day
   - Precipitation effect rates
   - Wind strength thresholds
   - Lightning strike probabilities
   - Visibility reduction multipliers

3. **CellClimate** (conceptual - tracked via CellEnvironment)
   - Current temperature (calculated)
   - Precipitation exposure (boolean)
   - Wind effects (from Weather)
   - Indoor vs outdoor designation

4. **TemperatureTolerance** (data on Race/creature definitions)
   - Comfort range (min/max comfortable temp)
   - Minimum tolerable temperature
   - Maximum tolerable temperature
   - Cold resistance modifier
   - Heat resistance modifier

5. **TemperatureEffect** (status tracked in State aspect)
   - Catching cold (sickness risk accumulator)
   - Frostbite (damage over time)
   - Heat exhaustion (consciousness loss risk)
   - Overheating (death risk)

6. **WetnessEffect** (status tracked in State aspect)
   - Clothing wetness level (0-100%)
   - Drying rate (near fire/indoors)
   - Sickness probability multiplier

### Contracts

**Location**: `specs/1-climate-weather/contracts/` (will be empty - no external APIs)

This is a single-player game with no REST/GraphQL APIs. All interfaces are internal C# method signatures within Roguelike.Core.

### Development Quickstart

**Location**: `specs/1-climate-weather/quickstart.md`

**Contents**:
- Overview of weather system architecture
- Key classes and their responsibilities (Weather, CellEnvironment, WeatherBalance)
- How to add new weather effects
- How to test weather changes
- Common scenarios (adding new temperature effect, modifying wind mechanics)
- Integration points with existing systems (State, Fighter, Camera aspects)

---

## Implementation Notes

### Key Integration Points

1. **Weather Update Timing**: Weather changes occur at `Weather.NextChangeTime`. Need to integrate weather update check into `World.DoOneStep()` or similar game loop.

2. **Temperature Effects on Characters**: `Alive.State.PassTime()` checks current cell temperature against creature tolerance, applies cold/heat effects.

3. **Projectile Deflection**: `Fighter` aspect shooting methods check `Weather.WindSpeed` and `Weather.WindDirection`, modify projectile Cell path.

4. **Indoor Temperature Calculation**: `InteriorCellEnvironment.Weather.Temperature` recalculated when doors open/close, fire sources added/removed, or outdoor temperature changes significantly.

5. **Visibility Modifications**: `Camera` aspect uses `Weather.VisibilityBonus` and sun obscurement to modify vision range calculation.

6. **Item Damage**: Items in character `Inventory` or in outdoor `Cell.Objects` accumulate rust/water damage during precipitation. Checked periodically (e.g., every game hour).

### Balance Considerations

- Temperature effects should be noticeable but not punishing - players need time to react (seek shelter, equip clothing)
- Wind should affect projectiles enough to matter tactically but not make ranged combat unusable
- Lightning strikes should be rare (1-5% per thunderstorm turn) but memorable when they occur
- Indoor spaces should feel meaningfully different from outdoors in extreme weather
- Weather transitions should be gradual (30-60 minutes game time) to avoid jarring changes

### Testing Strategy

- Unit tests for temperature calculation formulas (season, time of day)
- Unit tests for indoor microclimate balance (door effects, fire heating)
- Unit tests for wind projectile deflection math
- Integration tests for weather transitions over game time
- Integration tests for character temperature exposure over time
- Manual gameplay tests for weather feel and balance

### Localization Requirements

**LanguageWeather** strings needed:
- Precipitation types (none, light rain, heavy rain, snow, thunderstorm)
- Temperature descriptors (freezing, cold, cool, comfortable, warm, hot, scorching)
- Wind strength (calm, breezy, windy, strong winds, gale)
- Sun visibility (bright sunshine, partly cloudy, overcast, dark clouds)
- Status messages ("You are getting wet", "You feel cold", "Your fingers are numb with frostbite")
- Item condition messages ("Your papers are soaked", "Your sword is rusting")

---

## Success Metrics (from Spec)

Implementation will be validated against these criteria from the specification:

- **SC-001**: Characters take measurable cold/heat damage without protection
- **SC-002**: Indoor temperatures differ 10-15°C from outdoor with closed doors + fire
- **SC-003**: Projectile hit rates reduce 20%+ in strong winds
- **SC-004**: Rain causes wet status within minutes of exposure
- **SC-005**: Liquids freeze below 0°C, boil above 100°C
- **SC-006**: Weather transitions smoothly over time
- **SC-007**: Lightning strikes occur during storms, increased for metal/elevation
- **SC-008**: Vision range reduces 20-30% in storms
- **SC-009**: Metal rusts, paper damages in prolonged precipitation
- **SC-010**: Indoor spaces effectively protect from external weather
