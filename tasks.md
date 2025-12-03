# Implementation Tasks

This document lists all tasks necessary to implement the functionality described in COMPREHENSIVE.MD.

## 1. World Space and Structure

### 1.1 3D Distance Calculation
- Implement proper 3D distance calculation for diagonal cells using geometric formulas.
- Ensure distance between adjacent cells equals 1 for cells differing by one coordinate.

### 1.2 External/Internal Cell Environment
- Complete the distinction between external (outdoors) and internal (rooms/caves) cells.
- Implement microclimate connection between internal and external environments through balance mechanism.
- Ensure weather applies only to external cells.

### 1.3 Object Visibility System
- Implement vision strength-based cell visibility calculation.
- Add observer's vision range that determines which cells are visible.
- Implement transparent cell system where neighboring cells are visible through transparent objects.

### 1.4 Sound System
- Implement sound emission for objects that can produce sound.
- Add ear strength property for observers to detect sounds at certain distances.
- Make sound detection work regardless of walls and obstacles in the sound path.

### 1.5 Smell System
- Implement smell emission for objects that produce odors.
- Add olfactory trace system that briefly remains around and behind moving objects.
- Implement wind-based olfactory aura shifting for stationary objects.
- Add nose sensitivity property for smell detection.

## 2. Weather System

### 2.1 Temperature System
- Implement temperature effects on creatures (comfortable range, minimum/maximum tolerable temperatures).
- Add cold effects: risk of catching cold, frostbite, and death below minimum temperature.
- Add heat effects: loss of consciousness, chance of being roasted above maximum temperature.
- Implement warm clothing protection from cold.
- Add liquid freezing below zero and boiling in extreme heat.
- Implement food and hot object cooling in cold conditions.
- Add temperature calculation for buildings and caves based on doors and active fire sources.

### 2.2 Precipitation System
- Implement precipitation strength for rain and snow.
- Add clothing wetting when under precipitation without cloak or umbrella.
- Implement sickness risk from being wet.
- Add paper item wetting and metal rusting from heavy precipitation.
- Implement lightning attraction for metal objects and high-placed objects during thunderstorms.

### 2.3 Wind System
- Implement wind effects on flying projectiles (knocking them down).
- Add lightweight object movement by wind.
- Implement movement hindrance from strong wind.

### 2.4 Sun Visibility System
- Implement visibility deterioration when sun is obscured.
- Add sun brightness property to weather system.

### 2.5 Weather Update System
- Complete weather change mechanism that updates with time.
- Implement weather subscription system for objects that need to react to weather changes.

## 3. Objects and Cells

### 3.1 Object Properties
- Ensure all objects have material property (some materials are flammable, can drain, etc.).
- Implement object transparency property (whether light and vision pass through).
- Implement obstacle property (whether object blocks movement, only one obstacle per cell).
- Ensure weight property is properly used for carrying and support calculations.

### 3.2 Floor Properties
- Implement floor sound strength (how much sound is produced when walking).
- Add track system that records footprints and other marks on floors.
- Implement plant support property (whether certain plants can be planted).
- Add detection difficulty for plants, mushrooms, and dropped items based on floor type.

### 3.3 Cell Background System
- Ensure cell background is drawn when no objects are present in the cell.

## 4. Creature Characteristics

### 4.1 Characteristic System
- Implement strength characteristic (carrying capacity, melee damage with hands/heavy weapons).
- Implement reaction characteristic (action speed, dodging ability, light weapon proficiency).
- Implement intelligence characteristic (mental action speed, memory capacity).
- Implement willpower characteristic (hunger/pain shock endurance).
- Implement charisma characteristic (negotiation ability with people).
- Implement perception characteristic (hearing, sight, smell organ development, vision range, shooting accuracy).
- Implement appearance characteristic (cleanliness, physique, worn items, disease signs).
- Implement luck characteristic (chance to avoid danger or win).

### 4.2 Properties Calculation
- Implement MaxHP calculation: 10 + CEIL(STR/10) + FLOOR(WIL/20).
- Implement body weight calculation: STR*7.5 kg.
- Implement carrying capacity: STR/2 kg without penalty, STR*10 kg maximum until death.
- Implement reaction penalty from overloading: -1 REA per additional STR kg.

## 5. Inventory and Clothing

### 5.1 Clothing System
- Implement hat/helmet slot for head.
- Implement underwear slot (shirt, rags, loincloth).
- Implement dress slot (outer clothing: caftan, frock coat, women's dress, gambeson).
- Implement cloak slot (outer layer: sheepskin coats, fur coats, armor).
- Implement raincoat slot (protection from bad weather).
- Implement gauntlets/gloves slot (hand protection).
- Implement boots slot (foot protection, swamp boots, snowshoes).
- Implement jewelry system (amulets, rings, bracelets).

### 5.2 Clothing Effects
- Implement clothing cost, integrity, wear, and cleanliness affecting character attitude.
- Implement faction-based clothing recognition and attitude modification.
- Implement clothing protection from cold and precipitation.

### 5.3 Inventory Containers
- Implement pouch for jewelry.
- Implement tube for paper.
- Implement belt with potions.
- Implement quiver with arrows.
- Implement backpack system.
- Add mobility penalty for carrying too many items.

### 5.4 Overloading System
- Implement crushing to death when loaded above maximum capacity (STR * 10 kg).

## 6. Food, Hunger, and Daily Life

### 6.1 Food and Water System
- Implement daily energy requirement: STR*24 energy points per day.
- Implement daily water requirement: STR*24 water points per day.
- Implement hourly consumption: MAX(STR-(WIL/10),1) points per hour.
- Implement bloated state: when energy > STR*WIL (REA -5, PER -1, INT -1).
- Implement hungry state: when energy < STR (REA +1, PER +1, WIL -2).
- Implement starvation death: when energy < -(STR+WIL)*100.
- Implement water poisoning: when water > STR*10 (REA -1, PER -1, WIL -1), death at STR*30.
- Implement overeating/overdrinking vomiting that decreases food/water and can reduce diseases/poisoning.

### 6.2 Sleep System
- Implement sleep requirement and sleep deprivation effects.
- Add characteristic reduction from sleep deprivation.
- Implement hallucinations and loss of consciousness from extreme sleep deprivation.
- Implement death from extreme sleep deprivation.

### 6.3 Appearance Maintenance
- Implement washing system to maintain cleanliness.
- Implement clothing and equipment repair system.
- Implement equipment sharpening system.
- Add appearance decrease from lack of maintenance.
- Implement equipment wear from battles and difficult terrain.
- Add smell increase from poor hygiene.

### 6.4 Food Quality
- Implement cooked food being more nutritious than raw.
- Implement food preservation system (cooked food keeps longer).
- Implement water boiling for safer consumption.
- Implement alcohol consumption with negative consequences and addiction.

### 6.5 Equipment Degradation
- Implement equipment breaking and tearing when not fixed in time.
- Add irreversible damage system for equipment.

## 7. Living World

### 7.1 Fire Spread System
- Implement forest fire system from unextinguished campfires in hot dry weather.
- Add fire spread mechanics for forests and swamps.

### 7.2 Animal Population System
- Implement animal species population tracking.
- Add animal extinction when all of one species are killed in an area.
- Implement predator-prey relationship system (predators die when prey disappears).
- Add animal repopulation from neighboring areas.

### 7.3 Plant System
- Implement plant and mushroom collection system.
- Ensure plants regrow from seeds (not dangerous to collect all).

### 7.4 Settlement Needs System
- Implement daily water requirement for people in settlements.
- Add water delivery system from nearest sources.
- Implement consequences of dammed rivers or poisoned wells (population reduction).
- Implement food requirement system for settlements.
- Add crop and animal farming systems.
- Implement consequences of poisoned/burned crops, infected/stolen animals, poisoned products.
- Implement equipment wear system for settlements.
- Add requirement for workshops, mills, forges, mines in or near settlements.
- Implement nighttime illumination system for settlements.

## 8. Skills System

### 8.1 Craft Skills
- Implement carpentry skill (create and fix wooden furniture, weapons, armors, missiles).
- Implement smithing skill (create, fix and improve metal furniture, weapons, armors, missiles).
- Implement masonry skill (create and fix stone buildings and furniture).
- Implement pottery skill (create and fix clay items).
- Implement cooking skill (transform food ingredients into more nutritious dishes).
- Implement alchemy skill (transform liquids, minerals, plant and animal parts).
- Implement herbalism skill (detect seeds and plants, understand their properties).
- Implement spinning and weaving skill (transform plant fibers and animal wool > strings > fabric > clothes and repair).
- Implement leathercraft skill (make and repair leather clothes and armors).
- Implement mechanics skill (create and repair mechanical items, pick locks and disarm traps).

### 8.2 Survival Skills
- Implement hunting skill (detect, kill animals, resist attacks, extract parts like skin, horns).
- Implement fishing skill (catch fish from rivers, lakes and sea).
- Implement agriculture skill (seed and harvest plants).
- Implement animal husbandry skill (milking cows and goats, beekeeping).
- Implement healing skill (take care of wounds and diseases).
- Implement swimming skill (move through water safely).
- Implement climbing skill (move through hills and mountains safely).
- Implement hygiene skill (take care of appearance, protect from diseases).

### 8.3 Social Skills
- Implement sneaking skill (move in sneak mode with lesser probability to be detected).
- Implement pickpocketing skill (steal items from others' inventories).
- Implement alertness skill (detect hidden items, traps and sneaking characters).
- Implement trade skill (make shop prices better).
- Implement speechcraft skill (convince and justify oneself).
- Implement etiquette skill (increase success probability when talking with nobles and/or foreigners).
- Implement literacy skill (read and write).
- Implement foreign languages skill (talk/read/write with foreigners).

### 8.4 Music Skills
- Implement music skill system with instrument-specific skills (string, percussion, wind).
- Add music instrument playing capability.
- Implement music skill synergies between different instrument types.

## 9. Weapon Mastery System

### 9.1 Weapon Mastery Types
- Implement hand-to-hand mastery (boxing, kicking, wrestling).
- Implement blades mastery (swords, daggers, sabers, etc.).
- Implement maces mastery (flails, hammers, etc.).
- Implement polearms mastery (spears, forks).
- Implement bows and crossbows mastery.
- Implement slings and throwing weapons mastery.

### 9.2 Damage Types
- Implement piercing damage (-1 HP, start bleeding).
- Implement slashing damage (-some HP, start bleeding, can cut limbs off).
- Implement chopping damage (-many HP, start bleeding, can cut limbs off).
- Implement bludgeoning damage (-many HP, can confuse, make asleep or instantly kill).
- Implement elemental damage types (fire, frost, poison, electricity, acid, etc.).

## 10. Combat System

### 10.1 Attack and Defense Ratings
- Implement Attack Rating calculation: REA + STR/2 + WIL/8 + LUK/10 + skill/weapon/trait bonuses.
- Implement Defense Rating calculation: REA + LUK/5 + skill/armor+shield+weapon/trait bonuses.
- Add DR reduction for subsequent attacks in a turn (decreased by 2, 4, 8, ...).
- Implement hit probability: 50% + (AR-DR)/(AR+DR), clamped to 0..100%.

### 10.2 Damage Calculation
- Implement weapon damage: weapon damage + STR/5 + LUK/10 + WIL/20 + weapon/armor bonuses.
- Exclude STR and WIL bonuses for crossbows and mechanical weapons.
- Implement bare hands damage: 1..STR/2 HP bludgeoning damage.
- Implement bludgeoning damage reduction: reduced by WIL/10 times (minimum 1).

### 10.3 Action Time
- Implement action time multiplication: (1-Power(0.05, REA-10)).

### 10.4 Ranged Combat
- Implement max shooting range without penalty: STR cells.
- Add hit probability reduction: -15% AR per cell beyond max range.
- Implement full AR for ranged: PER + REA/5 + LUK/10 + bonuses.
- Implement DR for ranged: REA + LUK/5 + bonuses.
- Implement obstacle hit probability: (100%-PoH) for obstacles in missile track.

## 11. Memory System

### 11.1 Memory Capacity
- Implement memory system based on INT: each 1 INT grants 10 memory cells.
- Add memory bonuses from traits and other sources.

### 11.2 Memory Storage
- Implement person storage: 1 cell per person.
- Implement place storage: 1 cell per place.
- Implement recipe storage: 1 cell per cooking, alchemic, or craft recipe.
- Implement language storage: 1..10 cells depending on mastery.
- Implement music instrument storage: 1..4 cells depending on mastery.
- Implement skill storage: 1 cell per 10 skill levels.

### 11.3 Memory Loss
- Implement memory loss when head is damaged (INT decreases).
- Add skill/recipe/language decrease or loss when memory cells are lost.

## 12. Visibility and Perception

### 12.1 Vision Range
- Implement daytime vision range: PER cells.
- Implement morning/evening vision range: 3/4 PER cells.
- Implement nighttime vision range: 1/4 PER cells.
- Implement light source bonus: reduce daytime debuff by L cells for light source with L power.
- Implement weather visibility reduction (fog, heavy rain, snowfall).
- Implement static light sources: L cell radius area remains visible regardless of daytime for light source with L power.

## 13. Craft and Recipes

### 13.1 Recipe System
- Implement recipe storage in memory.
- Implement recipe difficulty system (requires different numbers of memory cells).
- Add recipe learning and forgetting mechanics.

### 13.2 Crafting System
- Implement crafting actions for all craft skills.
- Add material requirement system.
- Implement tool requirement system.
- Add crafting time and success probability based on skill level.

## 14. Traits System

### 14.1 Trait Types
- Implement characteristic increase traits (very strong, very intelligent, etc.).
- Implement skill increase traits (inborn hunter, inborn healer, etc.).
- Implement skill progress increase traits (fast learner of ...).
- Implement lifespan modification traits (long-lived, short-lived).
- Implement elemental resistance traits (increase or decrease, up to full immunity or critical vulnerability).
- Implement derived number modification traits (movement speed, reaction speed, carrying capacity, shooting range, hunger/thirst rates, etc.).

### 14.2 Trait Acquisition
- Implement trait selection at character creation based on difficulty level.
- Add trait acquisition during game progress.

## 15. States System

### 15.1 State Implementation
- Ensure dirty state decreases appearance (already exists, verify effects).
- Ensure poisoned state periodically inflicts damage (already exists, verify implementation).
- Implement drunk state with degrees: increases CHA > increases LUK, but decreases other characteristics > falls asleep > makes addicted.
- Ensure hangover state decreases all characteristics (already exists, verify implementation).
- Implement scared state (character tries to run away from danger source).
- Ensure all burn states (lightning, acid, fire, sun) decrease health and stamina, inflict damage, can destroy body parts or cause death.
- Ensure frozen state decreases health and can cause death (already exists, verify implementation).
- Implement confused state (character wanders randomly instead of acting).
- Ensure losing blood state decreases health (already exists, verify implementation).
- Implement falling asleep state (must be combined with tired state).
- Ensure tired state causes character to fall asleep (already exists, verify implementation).

## 16. Diseases System

### 16.1 Disease Implementation
- Ensure fever disease exists and is implemented (check existing implementation).
- Implement cough disease.
- Implement fungus disease.
- Ensure leprosy disease exists and is implemented (check existing implementation).
- Ensure lues disease exists and is implemented (check existing implementation).
- Ensure plague disease exists and is implemented (check existing implementation).
- Ensure ulcer disease exists and is implemented (check existing implementation).
- Ensure worms disease exists and is implemented (check existing implementation).
- Implement dermatitis disease.
- Implement migraine disease.
- Implement itch disease.

### 16.2 Disease Mechanics
- Implement disease transmission system.
- Add disease progression and effects.
- Implement disease treatment system (healing skill).

## 17. Society System

### 17.1 Ethnic Groups
- Ensure all races are properly implemented with strengths, weaknesses, appearance features, and languages (verify existing races).
- Add ethnic group history generation system.

### 17.2 Countries and States
- Implement country system with language, currency, and governing hierarchy.
- Add region and settlement ownership by countries.
- Implement world history generation for each people and state.

### 17.3 Languages
- Implement language system for different ethnic groups.
- Add language learning and communication system.
- Implement foreign language skill integration.

### 17.4 Currency System
- Implement currency system for different countries.
- Add trade and exchange mechanics using currencies.

## 18. Lore and Magic System

### 18.1 Elemental Forces
- Implement elemental force system (air, water, ground, fire, frost, metal, poison, electricity, acid, soul).
- Add elemental interactions and transformations.

### 18.2 Deities System
- Implement god of life (creates living creatures, allows reproduction, protects nature, helps with crops).
- Implement god of death (supports balance, takes life away, disassembles creatures into elements).
- Implement god of mind and knowledge (enables learning, teaching, remembering, created languages, reading, writing).
- Implement god of craft (patronizes cooking, mining, smithing, alchemy).
- Implement god of trade (invented money, enables exchange).
- Implement god of theft (changes balance, watches unfair actions).
- Implement god of magic (makes unusual things possible, transforms elements).
- Implement god of luck (assists according to unknown will).

### 18.3 Prayer and Donation System
- Implement prayer system to gods.
- Add donation system.
- Implement god reactions based on race, time, and circumstances.

### 18.4 Divine Power System
- Implement power granting system (gods grant pieces of power to mortals).
- Implement avatar system (gods can inhabit mortal bodies, creating significantly more powerful avatars).

### 18.5 Pantheon System
- Implement different pantheons for different races.
- Add emphasis on different aspects of gods, ignoring some, or inventing own deities.
- Implement world history generation with real facts, rumors, biases, and propaganda.

## 19. Character Creation

### 19.1 Character Generation
- Ensure character creation considers race, profession, sex, and random factors.
- Verify characteristics and skills are calculated from these factors.
- Ensure MaxHP of character and organs is calculated from characteristics.

## 20. Body and Organs System

### 20.1 Organ Types
- Ensure locomotor limbs are implemented (legs, tentacles, fins, tails) with movement hindrance on loss.
- Ensure active limbs are implemented (hands, tentacles, trunks) for object interaction.
- Ensure attacking organs are implemented (hands, legs, jaws, horns).
- Ensure sensory organs are implemented (eyes, ears, noses).
- Ensure thinking organ is implemented (brain) with consciousness loss on damage.
- Ensure other internal organs are implemented.

### 20.2 Organ Properties
- Ensure organs have blood points (how much blood can be lost).
- Ensure organs have hit points (how many hits can be sustained).
- Implement vital organ system (loss or critical damage causes immediate death).

### 20.3 Organ Damage
- Implement organ damage from combat.
- Add limb cutting system (from slashing and chopping damage).
- Implement organ destruction and death from critical damage.

## 21. Additional Systems

### 21.1 Sound Production
- Implement sound production when walking on different floor types.
- Add sound strength based on floor material.

### 21.2 Track System
- Implement footprint system on floors.
- Add track detection and tracking mechanics.

### 21.3 Plant Detection
- Implement plant and mushroom detection difficulty based on floor type.
- Add herbalism skill integration for plant detection.

### 21.4 Equipment Maintenance
- Implement equipment repair system.
- Add equipment sharpening system.
- Implement equipment wear tracking and effects.

### 21.5 Settlement Illumination
- Implement nighttime illumination system for settlements.
- Add light source management for settlements.

