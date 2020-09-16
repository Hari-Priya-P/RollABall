# RollABall

1. Link to play the Game Online - https://simmer.io/@php16/rollaball (Recommended Full-Screen Mode).<br/>

### Overview:
 
This game is called ‘Roll A Ball’ and it is an add-on on top of the basic Roll A Ball tutorial on the Unity website. It is a two-player shared-screen game. The major game objects in the game are:<br/>
• Two Players<br/>
They are called as PlayerA and PlayerB. The two players share the same game board and screen. I have used two cameras to facilitate this, one camera focusing on PlayerA and another on PlayerB. The green ball is PlayerA while the red ball is PlayerB. PlayerA uses the left half of the screen and PlayerB uses the right half.<br/>
• Two kinds of Cubes/Collectibles: <br/>
There are two kinds of collectibles in the game, they are the yellow cubes and the white cubes. The white cubes are worth 1 point each and they respawn after 10 seconds while the yellow cubes are worth 2 points each and they respawn after 20 seconds.<br/>
• Walls<br/>
There are four walls on the four edges of the plane which act as boundaries for the players. <br/>

Game Rules:
1.	The game has a countdown of 2 minutes which is displayed on the top of the screen. The goal of each player is to collect as many collectibles as possible before the timer ends. The player with the maximum score wins the game.
2.	If a player’s score becomes negative before the countdown ends, then that player loses midgame and the other player wins by default.
3.	Collisions with walls will result in a penalty of 3 points.
4.	Players colliding with each other will result in a penalty of 1 point from the player with lower height. That means, if one player’s jump is higher than the other or the other player is on the ground, then this player wins the collision. <br/>
Note: Even when the two players are on the ground, the height of the two players differs by a minute amount wherein the height of the player with comparatively higher speed has more height.

Game Controls:<br/>
Player A: Arrow Keys for Directions and Space bar for Jumps<br/>
Player B: A,W,S,D Keys for Directions and X key for Jumps <br/>

Scenes Used:
1.	MainMenu (Cover Page)
2.	InstructionManual (Instructions Page)
3.	MiniGame (Main Game Page)

Scripts Used:
1.	MenuController – This script is for the functionality of the buttons Start, Instructions and Exit on the Cover Page.
2.	InstructionController – This script is specific to the InstructionManual scene where it adds functionality to the Back button on the instructions page.
3.	CameraController – This script attaches the cameras to the players so that they move along when the players move.
4.	Rotator – This script is to enable the rotation of the collectible cubes.
5.	PlayerController – This is the main script where the movement of the players are enabled, the collisions are monitored, and the scores are kept track of. It also monitors for negative scores, ends the game, and declares the winner through pop-up window. It is also used to announce the status of collisions through Text object on the screen.
6.	OptionsController – This script is majorly to enable the functionalities of the buttons Pause/Play, Reset and Quit on the Game page. This script is also used to keep track of the timer and ends the game when the countdown is completed. It announces the winner with maximum score through a pop-up after the countdown.
