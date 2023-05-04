# COMP416 Project 

The task of this project is the creation of a single-player Real Time Strategy (RTS) game in Unity game engine.

---

## RTS Game Design “King of the Mountain”

- **Player**: The player controls a medieval village colony.
- **Setting**: Medieval age.
- **Goal**: The player must develop a colony and build high tier buildings, to gain access to warriors. The player wins if they find and kill the enemies that occupy the top of the mountain, where the game begins. If the enemies kill all remaining player’s units, the player loses.

### Look-up table

#### Resources
- **Food**: Gathered by interacting with berry bushes or from building a mill, which will produce a standard amount over time. Used to build new units.
- **Wood**: Gathered by interacting with trees. Used to build buildings of all tiers.
- **Stone**: Gathered by interacting with rocks. Used to build higher tier buildings.
- **Gold**: Gathered by interacting with gold ores. Used to build elite warriors.

#### Units
- **Villager**: Low tier unit. Good for gathering food and constructing buildings, but not for combat. Requires very little food to build.
- **Woodcutter**: Basic tier unit. Good for gathering wood and constructing buildings, but not for combat. Requires some food to build.
- **Stonemason**: Basic tier unit. Good for gathering stone and constructing buildings, but not for combat. Requires some food to build.
- **Gold Miner**: High tier unit. Good for gathering gold and constructing buildings, but not for combat. Requires a lot of food to build.
- **Warrior**: Elite tier unit. Good for combat and fast, but not so for gathering resources. Requires a lot of food and gold to build.
- **Enemy (AI)**: High tier unit. Good for combat and fast. Guards over a stronghold which the player must find and clear to win the game.

#### Doodads
- **Bush**: Offers food, easiest to gather and everywhere. All units can gather food by interacting with a bush, although a villager is best suited since they don’t have good gathering rates for any other resource. The bush is then destroyed.
- **Tree**: Offers wood, easy to gather and plenty. All units can gather wood by interacting with a tree, although a woodcutter is best suited since they have the best gathering rate for this type of resource. The tree is then destroyed.
- **Rock**: Offers stone, hard to gather and scarce. All units can gather stone by interacting with a rock, although a stonemason is best suited since they have the best gathering rate for this type of resource. The rock is then destroyed.
- **Gold Ore**: Offers gold, hardest to gather and rarest. All units can gather gold by interacting with a gold ore, although a gold miner is best suited since they have the best gathering rate for this type of resource. The ore is then destroyed. 

#### Buildings
- **Mill**: Enables food production over time. Requires very little wood and very little time to build. Can be upgraded to increase food production rate.
- **Woodcutter building**: Enables the creation of woodcutters at the cost of food. Requires little wood and little time to build.
- **Stonemason building**: Enables the creation of stonemasons at the cost of food. Requires some wood and some time to build.
- **Gold miner building**: Enables the creation of gold miners at the cost of food. Requires a lot of wood, stone and time to build.
- **Warrior building**: Enables the creation of warriors at the cost of food and gold. Requires the most wood, stone and time to build.

#### Commands
- **Move to Position**: Commands a unit to move to a position of the map;
- **Gather Resource**: Commands a unit to gather resources from a doodad.
- **Construct Building**: Commands a unit to construct a building that is on under-construction phase.
- **Attack**: Commands a unit to attack an enemy unit.

## Report

### Scripts

- **BuildingSelection**: Responsible for enabling the corresponding buttons that work as building UIs with the different building functionalities with which the player can interact when these buildings are selected.
- **BuildingUCManager**: Responsible for the construction sequence of the building scaffolds, depending on the building they are going to be replaced with, and the total construction percentage that units offer, when they are commanded to construct.
- **CameraManager**: Responsible for the camera’s WASD movement and zoom through scrolling. 
- **DoodadManager**: Responsible for the resource gathering sequence from the doodads, depending on the type of resource they offer, the available amount of resources left on the doodads and the gathering rates that correspond to each resource for the different types of units.
- **Enemy**: Responsible for storing and checking the list that contains the enemies, for the occasion of it reaching zero and the player achieving victory.
- **EnemyManager**: Responsible for the hostile AI behavior upon the entrance of a player’s unit into the detection radius of an enemy.
- **GameEnemyLevels**: Responsible for the random spawn position of the enemy fortress in one of the six corners of the map.
- **GameLevels**: Responsible for the random spawn position of the doodads across the map
- **Player**: Responsible for all the player’s units and buildings instantiations and for keeping track of all the resources gained by the player as well as for storing and checking the list that contains the player’s units, for the occasion of it reaching zero and the player being dealt defeat.
- **UIBuilding**: Responsible for updating the percentage counter when a building is constructed.
- **UIDoodad**: Responsible for updating the available resources still remaining on a doodad.
- **UIEndGame**: Responsible for activating the end game screen that pops when the game is either won or lost with all the necessary button options.
- **UIMainMenu**: Responsible for opening the main menu scene when the game is either loaded for the first time or when the player requests to be brought back to it with all the necessary button options.
- **UIMiniCanvas**: Responsible for rearranging all the mini canvases atop units, doodads, and buildings with the information they carry to be always looking at the camera.
- **UIPauseMenu**: Responsible for activating the pause menu screen with all the necessary button options.
- **UIPlayer**: Responsible for updating all resource counters on the main UI of the game and for handling the mission log
- **UIUnitHealthBar**: Responsible for activating the health bar of player’s units and enemies when their health points status change.
- **UIUnitState**: Responsible for switching between the possible unit state icons for the player to easily recognize their units’ current state.
- **Unit**: Responsible for all the unit’s main methods, setting and switching between the appropriate unit state, executing the commands given to the unit and handling the unit in case it gets killed.
- **UnitManager**: Responsible for distributing commands and managing what the unit is going to do next, based on which object the player clicked.
- **UnitMover**: Responsible for gathering the coordinates the player has clicked on and for determining the destinations the units are going to position themselves at.
- **UnitSelection**: Responsible for handling single or multi-unit selection and for handling the drag selection box inside of which units are selected.

### Assets

All imported models are stored in the “_imported” folder on the Assets. Game’s title image was generated from *https://www.coolgenerator.com/png-text-generator*, while UI sprites, fonts and all of the models were imported from *https://www.kenney.nl/assets*. Music tracks for the game were downloaded from *https://xdeviruchi.itch.io/8-bit-fantasy-adventure-music-pack* and produced by xDeviruchi.
The models were mixed and matched and combined by me to form the game’s prefabs (e.g., buildings, warrior and enemy prefabs). All textures for the units were also created by me, based and customized from the original textures the unit model was imported with. 
