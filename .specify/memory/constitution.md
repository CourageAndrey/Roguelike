# Roguelike Game Constitution

## Core Principles

### I. Aspect-Based Design
Objects gain capabilities through composition of IAspect implementations, not inheritance. Each aspect represents a cohesive behavior or capability (e.g., Fighter, Inventory, Thief). Objects become aspect holders that can dynamically expose multiple capabilities. Aspects must:
- Hold a reference to their owner object (_holder)
- Be added via `AddAspects(params IAspect[] aspects)` in constructors
- Be accessed through extension methods: `GetAspect<T>()`, `TryGetAspect<T>()`, `Is<T>()`

### II. Separation of Concerns
Core game logic (Roguelike.Core) remains completely independent of UI implementations. Core is a .NET Standard 2.1 library with no external dependencies. UI clients (Roguelike.Console) reference Core but never vice versa. All game mechanics, world simulation, and business rules live in Core. UI only handles presentation, input handling, and rendering.

### III. Localization-First
All user-facing strings must use the Language system. No hardcoded English strings in game logic. Language classes in `Roguelike.Core.Localization` provide structured access to all text. Use `string.Format` with `CultureInfo.InvariantCulture` for parameterized messages. Access via `game.Language` or specific properties like `game.Language.LogActionFormats`.

### IV. Event-Driven Updates
Changes to game state trigger events that observers can subscribe to. Use `ValueChangedEventHandler<TEntity, TValue>` for state changes. Thread-safe event raising with `Volatile.Read` for handlers. Events enable decoupled view updates, logging, and AI reactions without tight coupling.

### V. Action Result Pattern
All player/NPC actions return `ActionResult` containing:
- Time (longevity/duration) for world progression
- Log messages (strings) for player feedback
- Optional Activity state changes (standing, fighting, sneaking, etc.)
Access game context through: `_holder.GetGame()`, `game.World.Balance`, `game.Language`

## Architecture Standards

### World Structure Hierarchy
```
Game → World → Region(s) → Cell(s) → Object(s)
```
- Objects exist in Cells, Cells in Regions, Regions in World
- Hero is the player character with Camera aspect
- Objects can be Active (can perform actions with NextActionTime)
- Alive objects are Active with Body, State, Inventory, Fighter, Thief aspects

### Naming Conventions
- **PascalCase**: Classes, methods, properties, public members
- **_underscore prefix**: Private fields (`_holder`, `_objects`, `_log`)
- **I prefix**: Interface names (`IAspect`, `IAlive`, `IObject`)
- **Raise prefix**: Event raising methods (`RaiseAggressiveChanged`)
- Descriptive names reflecting game domain (objects, cells, aspects, etc.)

### Code Organization
- Use `#region` blocks: Properties, Constructors, Methods, Events
- Partial classes for large files (e.g., Program.cs split across multiple files)
- Explicit property getters with braces for consistency
- Nullable reference types enabled (`<Nullable>enable</Nullable>`)
- Always check nullable references before use

### Collection Patterns
- Public properties expose `IReadOnlyCollection<T>`
- Internal mutable collections with read-only views
- Clear cached views when underlying collections change
- Use LINQ for queries, not manual iteration where possible

## Development Workflow

### Adding New Features
1. **New Objects**: Create in `Roguelike.Core.Objects`, inherit from `Object`, add aspects
2. **New Aspects**: Create in `Roguelike.Core.Aspects`, implement `IAspect`, add extension methods
3. **New Actions**: Return `ActionResult`, use Balance for timing, Language for messages
4. **UI Features**: Add to Console project partial files or create new partials
5. **Localization**: Add `Language*` classes in `Roguelike.Core.Localization` or extend existing

### Action Flow
1. User input triggers key handler in `Program.cs` partials
2. Key handler performs action and returns `ActionResult`
3. World applies action: `world.ApplyAction(hero, actionResult)`
4. World processes one step: `world.DoOneStep()`
5. Time passes for hero state: `hero.State.PassTime(longevity, language)`

### Testing Standards
- Tests in `Roguelike.Tests` project
- Follow existing test patterns (see `EventCollectionTest.cs`, `TimeTest.cs`)
- Test Core logic independently of UI
- Unit tests for game mechanics, integration tests for world simulation

## Quality Standards

### Balance Configuration
- All game balance values in `Configuration/` classes
- No magic numbers in game logic
- Balance values accessible via `game.World.Balance`
- ActionLongevity, PlayerBalance, etc. separated by concern

### Error Handling
- Use `#warning` directives for known TODOs
- Throw `ArgumentOutOfRangeException` for invalid enum values
- Handle null checks appropriately for nullable types
- Fail fast on contract violations

### Thread Safety
- Use `Volatile.Read` for event handler access
- Consider thread safety when modifying shared state
- Event collections for multi-subscriber patterns
- Immutable value types where possible (e.g., `Vector`, `Time`)

## Governance

This constitution defines the architectural principles and coding standards for the Roguelike game project. All code changes must adhere to these principles. The aspect-based design, separation of concerns, and localization-first approach are non-negotiable architectural decisions.

When principles conflict with expedience, architectural integrity takes precedence. New features must fit the established patterns. If a pattern proves inadequate, update the constitution first, then implement the change consistently across the codebase.

Constitution changes require documentation of rationale and migration path for existing code.

**Version**: 1.0.0 | **Ratified**: 2025-11-13 | **Last Amended**: 2025-11-13
