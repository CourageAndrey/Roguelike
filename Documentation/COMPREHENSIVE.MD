Roguelike comprehensive description

# 1 World
## 1.1 Space
The world is divided into regions, each region is divided into cells of 1x1x1 step size. All space is 3-dimensional. The distance between adjacent cells equals 1 for cells differing by one coordinate (X, Y or Z). For diagonal cells, the distance is determined by the laws of geometry.
Areas can be external (outdoors, where weather applies) or internal (rooms or caves, having their own microclimate connected with the outdoor environment through a balance mechanism). Cells of these areas are correspondingly external or internal.

## 1.2 Time
World time is measured according to the modern Gregorian calendar with precision to fractions of a second. All world objects exist within a unified timeline, which is hidden behind the sequence of their turns.
According to the date, seasons change (winter, spring, summer, and autumn, three months each). According to the time of day, periods change (night, morning, day, and evening, 6 hours each, night begins at 22:00).

## 1.3 Object visibility
Not all world cells are visible simultaneously.
Cell visibility is determined by the observer's vision strength – whether their vision "reaches" a certain cell or not. World objects are located in cells, and there can be several objects in one cell. A cell can be transparent (neighboring cells are visible through it) or not (nothing is visible behind it).
Some objects emit sound – these can be heard at a certain distance, determined by the observer's ear strength, regardless of walls and objects in the sound's path.
Some objects emit smell – around such objects and behind them when they move, a "trace" briefly remains, which can be detected by those with a sensitive enough nose. In the presence of wind, the olfactory "aura" from stationary objects shifts in the direction of the wind.
Note that visibility applies to cells, while sound and smell are emitted by world objects.

## 1.4 Weather
External cells have weather, which is determined by:
- temperature (below or above zero);
- precipitation;
- wind strength;
- sun visibility/brightness.
Each creature has a range of comfortable temperatures, a minimum tolerable temperature (if down to the minimum – there is a risk of catching a cold, below it – frostbite and possibility of death, protection is possible with warm clothing) and a maximum tolerable (up to the maximum – can lose consciousness, above – there is a chance of being roasted, no protection). Below freezing, liquids freeze; in extreme heat, they boil. In cold conditions, food and hot objects cool down. Temperature in buildings and caves depends on the presence of doors and an active fire source.
Precipitation has strength, with which rain pours or snow falls. If one is under precipitation without a cloak or umbrella, clothing gets wet, and one can also get sick. Heavy precipitation can wet improperly stored paper items or rust metal ones. During thunderstorms, metal objects and those located high can attract lightning.
Wind can knock down flying projectiles, move lightweight objects, and hinder movement.
If the sun is obscured, visibility deteriorates.

## 1.5 Objects
World cells contain objects. An object has the following properties:
- material it is made of (some materials are flammable);
- whether the object is transparent, i.e., whether what is behind it can be seen, and whether light passes through it;
- whether the object is an obstacle, i.e., whether one can step on the cell where such an object stands. Obviously, no more than one obstacle object can be on one cell simultaneously;
- weight, which is considered when someone carries the object or it is placed on a not too strong support like ice.
Some world objects are living creatures that move and act according to some logic.
If there are no objects in a cell, then the cell's background (its floor, ground, or other covering) is drawn on the screen in that cell.

## 1.6 Floor
The "lower part" of a cell (floor or ground) determines:
- the strength of sound produced when walking on it;
- tracks left on it;
- whether a certain plant can be planted in this cell;
- the difficulty of detecting plants, mushrooms, and dropped items on it.

# 2 Creatures
## 2.1 General properties
Like any object, a living creature has weight. In addition, each creature has a list of items that can be found on its body after death. All living creatures have such items, determined by their body: internal organs, skin or hide, other trophies like fangs. Moreover, people and other intelligent creatures have inventories containing items that can be stolen from them.
All living creatures have a body consisting of organs of the following types:
- locomotor limb (legs, tentacles, fins, tails), the loss of which hinders movement;
- active limb, allowing interaction with objects (hands, tentacles, trunks);
- attacking organ (hands, legs, jaws, horns);
- sensory organ (eyes, ears, noses);
- thinking organ, damage to which leads to loss of consciousness (as a rule, this is the brain);
- other (internal organs).
At the same time, any organ can be vital, which means that its loss or critical damage will lead to the immediate death of the owner.
Each organ has a number of points, the loss of which leads to its irreversible damage:
- how much blood the organ can lose;
- how many hits the organ can withstand (this value is close in meaning to the common concept of "hit points" in role-playing games).

## 2.2 Characteristics
To determine the effectiveness of certain actions, the following characteristics inherent to characters (non-player and player) are used:
- **strength** determines how much a character can carry and how hard they hit with hands or heavy weapons;
- **reaction** determines how quickly a character performs their actions, and their ability to dodge, as well as proficiency with light fencing weapons;
- **intelligence** determines the speed of performing actions requiring mental effort (for example, reading) and the amount of available memory, in which maps of terrain, task lists, combat techniques, alchemical recipes, and foreign languages can be stored;
- **willpower** determines how well a character endures hunger and pain shock;
- **charisma** determines the ability to negotiate with people;
- **perception** is determined by how developed the available organs of hearing, sight, and smell are. Affects the range of "object visibility". Vision affects shooting accuracy;
- **appearance** affects the attitude of other characters and is determined by the character's cleanliness, their physique, items worn on them, signs of diseases (including already transferred).
- **luck** sometimes allows one to avoid danger or win.

Also, people have skills and traits related to these characteristics.

## 2.3 Inventory
All living creatures have a default inventory that contains their body parts and everything that can be picked up from their corpse. In addition, characters can wear wardrobe items listed below. In general, each creature can have wearable items: a collar on a dog, a saddle on a horse, etc.
- hat or helmet on the head;
- underwear (for example, a shirt, rags, or loincloth);
- dress (meaning any outer clothing: caftan, frock coat, women's dress and the like, this also includes gambeson);
- cloak (outer layer of clothing: sheepskin coats, fur coats, armor);
- raincoat (thrown over shoulders for protection from bad weather);
- gauntlets or gloves (protect hands during fencing, alchemy, metal forging, etc.);
- boots (protect feet from water, snow, etc. Swamp boots help when moving through swamps, snowshoes – respectively, through snow).
In addition, one can attach amulets, rings, bracelets, and other jewelry to oneself.
Obviously, besides protection from cold and precipitation, clothing performs other functions. The attitude of characters towards you depends on the cost of clothing, its integrity and wear, as well as cleanliness (for example, from blood stains). Obviously, representatives of different factions will wear different clothing and evaluate unfamiliar characters precisely by the uniform worn.
A character can carry several inventory items on their person, which contain other items: a pouch for jewelry, a tube for paper, a belt with potions, a quiver with arrows, a backpack full of junk, and others. It should be understood that each such item limits the character's mobility. Too many items cannot be carried. If, due to some circumstances, a character is loaded above their maximum, determined by strength, they will be crushed to death.

## 2.4 Food and hunger, daily life and routine
To successfully maintain their own life, a character in the game must:
- eat and drink;
- sleep;
- take care of their appearance: wash, repair clothing and equipment.
How much a character will need all of this depends on what they do and for how long. Physical and mental loads cause hunger, thirst, and fatigue faster. Traveling through difficult terrain and battles contaminate equipment. As a result of battles, some equipment may need repair and sharpening.
If one does not eat for a long time – strength and reaction decrease, though dying from hunger is quite difficult. Dying from thirst is much easier, lack of water reduces strength and intelligence. Strong willpower allows one to endure hunger and thirst longer. One can die by drinking too much water or overeating. Regular overeating causes an increase in the character's weight, which will lead to loss of strength, reaction speed, and appearance. Sleep deprivation greatly reduces all characteristics and leads to hallucinations, loss of consciousness, and even death. If one doesn't take care of oneself, the character's appearance will decrease, dulled and broken equipment will work worse, and the body will begin to emit a more noticeable smell.
Useful tips. Cooked food is more nutritious than raw and keeps longer. Water is better boiled before consumption. Alcohol consumption causes negative consequences and addiction. Equipment not fixed in time sometimes breaks and tears irreversibly.

## 2.5 Living world
The world around the player lives and develops according to its own laws. But the established balance can be disrupted. Examples below.
- If in a forest or swamp in hot dry weather an unextinguished campfire is left, this can lead to a large forest fire.
- If in a certain area all animals of one species are killed, they will not soon reappear in that area. And only if they have somewhere to return from. Predators whose prey has disappeared will also die out. Collecting all plants and mushrooms is not dangerous due to plant biology (the amount of seeds they scatter).
- People need to drink daily – and water for this needs to be delivered from the nearest sources. A dammed river or poisoned well – a good reason for the rapid extinction of residents of a separate village or castle.
- People need to eat. That is why they grow plants and animals. Poisoned and burned crops, infected or stolen animals, poisoned products in barns – also a valid reason for population reduction.
- Clothing and armor, tools and weapons – everything wears out over time and becomes unusable. For normal functioning of a human settlement, workshops, mills, forges, mines, and the like must be located in it or nearby.
- In places where people live, they try to light illumination at night.
- In short – the game world lives perfectly and gets along without the player.

## 2.6 Skills
- carpentry: create and fix wooden furniture, weapons, armors, missiles
- smithing: create, fix and improve metal furniture, weapons, armors, missiles
- masonry: create and fix stone buildings and furniture
- pottery: create and fix things made of clay
- **NO mining and woodpicking** - AXES weapon mastery instead
- cooking: transform food ingredients into more nutritious dishes
- alchemy: transform liquids, minerals, plant and animal parts into other ones
- herbalism: detect seeds and plants, understand their properties
- hunting: detect, kill animals (and resist their attacks), also extract their parts (skin, horns, etc.)
- fishing: catch fish from rivers, lakes and sea
- agriculture: seed and harvest plants
- animal husbandry (includes milking cows and goats, and beekeeping): **UNCLEAR**
- spinning and weaving: transform plant fibers and animal wool > strings > fabric > clothes and repair them
- leathercraft: make and repair leather clothes and armors
- healing: take care of anyone's wounds and diseases
- sneaking: move in sneak mode with lesser probability to be detected
- pickpocketing: steal items from others inventories
- mechanics: create and repair mechanical items, pick locks and disarm traps
- alertness: detecting hidden items, traps and sneaking characters
- swimming: move through water in safer ways
- climbing: move through hills and mountains in safer ways
- hygiene: take care about appearance of own body and wearings, protecting from diseases
- trade: make shop prices better
- speechcraft: make it possible to convince and justify oneself
- etiquette: increase success probability when talking with nobles and/or foreigners
- literacy: make it possible to read and write
- foreign languages: make it possible to talk/read/write with foreigners
- music (depends on specific instruments: string, percussion or wind ones): makes it possible to play corresponding instruments, has synergies with each other

## 2.7 Weapon mastery
Depending on selected weapon item in hands, there are following weapon masteries:
- hand-to-hand, including boxing, kicking and wrestling
- blades: swords, daggers, sabers, etc.
- maces, including flails and hammers, etc.
- polearms, including spears and forks
- bows and crossbows
- slings and other throwing weapons

There are following types of damage possible (depending on weapon and used missiles):
- piercing `колющий` (-1 HP, start bleeding)
- slashing `режущий` (-some HP, start bleeding, can cut limbs off)
- chopping `рубящий` (-many HP, start bleeding, can cut limbs off)
- bludgeoning `дробящий` (-many HP, can confuse, make asleep or instantly kill)
- elemental (of many types, listed below) `стихийный`

## 2.8 Craft and recipes
All characters (both playable and non-playable ones) have "memory cells" (depending on their intelligence), which allow them to master skills, languages, weapons and recipes. All of them can have different difficulty (requires different numbers of memory). If there are no free memory cells, then new skills and knowledge cannot be obtained or improved. If the number of memory cells decreases somehow (for example, if the head is damaged), some skills or recipes can be decreased or lost/forgotten.

## 2.9 Traits
The number of traits at start depends on game difficulty level. Then, with game progress, a character can obtain new traits.
- increase characteristics: very strong, very intelligent, etc.
- increase skills: inborn hunter, inborn healer, etc.
- increase skills progress: fast learner of ...
- long-lived or short-lived, which modify maximum character life span
- increase (up to full immunity) or decrease (up to critical vulnerability) elemental resistance
- increase or decrease derived numbers like movement speed, reaction speed, carrying capacity, shooting range, hunger and thirst, etc.

## 2.10 States
- dirty (decrease APP)
- poisoned (periodically inflicts damage)
- drunk (depending on degree: increases CHA > increases LUK, but decreases other characteristics > falls asleep > makes addicted)
- hangover (can be also caused by drug addiction, decreases all characteristics)
- scared (character tries to run away from the source of danger instead of acting)
- lightning burned
- acid burned
- fire burned
- sun burned
- frozen (all burns decrease health and stamina, inflict damage and can destroy body parts or even cause death)
- confused (character starts to wander randomly instead of acting)
- losing blood (character starts to lose health)
- falling asleep `(must be combined with next one)`
- tired (character just falls asleep)

## 2.11 Diseases
- fever
- cough
- fungus `(грибок)`
- leprosy
- lues `(сифилис)`
- plague
- ulcer `(язва)`
- worms
- dermatitis
- migraine `(мигрень)`
- itch `(зуд, чесотка)`

# 3 Society
People belong to different ethnic groups, who have their own strengths and weaknesses, as well as appearance features and languages. Regions (and especially settlements) belong to different countries, which have their own language, currency, and governing hierarchy. The world, when generated, creates a long history for each people and state.

# 4 Lore
It is not interesting to play a game that is the same as our boring real life full of mundane routine. Moreover, it would require implementing complicated physics, chemistry, biology, etc. mechanisms – and no one has enough resources to emulate all this stuff. Therefore, the world will be a magical one – but it must not be controversial.

The **Creator** existed before this world and created it out of nothing in order to have fun looking at his own creation and creatures.

At first, he created forces, which combine to create the material world:
- air, which is everywhere
- water and
- ground, which is condensed inside air making it possible to live there
- fire and
- frost, which are dynamic forces that run the world
- metal and
- poison and
- electricity and
- acid, which are additional sub-elements made from the first five and
- soul, which is the essence of life.

All these elemental forces created the whole world:
- grass plains, which are best for growing crops
- snow tundras, cold and appearing lifeless (though this is not true)
- sand deserts, hot and also appearing lifeless (which is almost true)
- rocky mountains, full of minerals and ore
- dangerous swamps, full of poisonous creatures
- rivers, lakes and seas, full of fish and other mystical creatures.

Then, the Creator created lesser deities to serve him and world inhabitants:
- god of life, who created living creatures of all kinds and allowed them to bear their children, and also protects nature
- god of death, who supports balance and takes life away
- god of mind and knowledge, who created people to use their brains to learn and change the world
- god of craft, who helps to transform and combine materials into one another
- god of trade, who allows people to exchange the results of their work with each other
- god of theft, who watches those who act unfairly
- god of magic, who makes it possible to do unusual things and transform elements
- god of luck, who assists people according to his unknown will.

Then the god of life created people of different races:
- plainsmen, who are good at agriculture and science
- nomads, who resist hot and dry deserts, skilled in trade
- highlanders, strong and enduring, who are the best miners and smiths
- northmen, who resist cold and frosty tundras, good fishermen and sailors
- junglemen, who resist poison and diseases, good at survival and hunting, even when hunting other people
People of different races have their own countries, languages, laws, armies and money – and also their own pantheons. They worship the forces of nature and gods listed above – however, their pantheons are completely different, they can emphasize different aspects of gods, ignore some of them or invent their own deities. Despite all differences, they talk, trade and marry each other, and sometimes live far away from their native countries.
Also, this god helps to plant and collect crops.

The god of death made living creatures mortal. When someone dies, the death god disassembles the creature into the elements it consists of and returns them to nature.

The god of mind makes it possible to learn, teach and remember. This god helps to collect knowledge, created languages for people, and taught them to read and write.

The god of craft patronizes cooking, mining, smithing and alchemy.

The god of trade invented money to make it possible to exchange between people.

The god of theft changes the balance between people according to the will of other gods and people's prayers and actions.

The god of magic makes unusual and impossible things possible. This god simply makes the world around more interesting.

All gods accept prayers and donations, but can react differently depending on race, time and other circumstances.
All gods can grant pieces of their power to mortals or even inhabit their bodies (creating avatars, who are significantly more powerful compared to ordinary people around).

# 5 Mechanics

When any live creature is created, it creates a corresponding body with organs.

When a character is created, the following aspects are taken into account:
- race
- profession
- sex
- a little bit of random.
All of the above are combined to create characteristics and skills numbers.

According to all of these (if applicable), MaxHP of character and organs is calculated. This number is a simplified amount of damage (or pain) which can be obtained before organ destruction or creature death.
Max HP = 10 + CEIL(STR/10) + FLOOR(WIL/20)

Own body weight = STR*7.5 kg

Max carrying weight without penalty = STR/2 kg
Each additional STR kg decreases REA by 1
Max carrying weight until death because of broken spinal cord = STR * 10 kg

A character must eat food to compensate STR*24 energy points every day and drink the same amount of water.
Every hour decreases energy and water by MAX(STR-(WIL/10),1) points.
When energy > STR*WIL, then the character is bloated (REA -5, PER -1, INT -1).
When energy < STR, then the character is hungry (REA +1, PER +1, WIL -2)
When energy < -(STR+WIL)*100, then the character dies of starvation.
When water > STR*10, then the character feels unwell (REA -1, PER -1, WIL -1).
When water > STR*30, then the character dies because of water poisoning.
Overeating and overdrinking can also lead to vomiting, which decreases food and water points - and also can decrease some diseases and poisoning.

When a character hits someone without a weapon, the basic damage is 1..STR/2 HP. Target can resist damage based on STR and WIL **TBD**
Also, Floor(STR/10) is added to weapon damage (except crossbows and other mechanical).

Time of every action is multiplied by (1-Power(0.05, REA-10)).

Memory is based on INT - **TBD**.

Visible range is PER cells during the day, 3/4 PER for morning and evening and 1/4 PER during night. When a light source (like candle or torch) with L power is equipped, daytime debuff is reduced by L cells. Also, weather can decrease visibility range (fog, heavy rain and snowfall). Static light sources (fires, candles, etc.) with L power make around them an L cell radius area, which remains visible disregarding daytime.
Max shooting range is STR cells. Range penalty is **TBD**.
