# Arkanoid
A replica of the classic Arkanoid breakout game.

# Unity Version : 2019.4.17f1

# Instructions to Play:
## After opening the project change the window aspect ratio to 16:9. Then hit Play to play the game.

# Game Flow
* Game starts with generating a random layout of bricks.
* After the level generation the player can move the paddle to the left and right using Left arrow and Right arrow keys or the A and D keys.
* The objective of the game is to destroy all the bricks in the scene.
* The player gets 3 lives.
* Everytime the ball is destroyed, 1 life is reduced. When player runs out of lives, the game will end.
* Every brick has chance to spawn a power up.
* There are two power ups as of now:
  * Piercing ball
  * Slow ball
* There are two types of bricks. Colored bricks take 1 hit to destroy and Metal bricks take 3 hits to destroy.

# Game Flow Diagram
![alt text](https://github.com/sooraj2906/Arkanoid/blob/main/Arkanoid%20flowchart.png)

# Game Structure

 # GameManager Class : 
 ## This Class controls the game flow and overall state of the game.
      # OnBallDestroyed()   This method is used to check if any lives are remaining and decide whether to reset the ball or end the game.
      # StartGame()         This method resets the game elements for a new game.
      # UpdateScore()       This method is called whenever the score is updated anywhere in the game.
      # OnBrickDestroyed()  This method is used to calculate the score when a brick is destroyed.
      
 # BallController Class:  
 ## This class controls the movement, reset and destruction of the ball.
      # ResetBall()          This method is used to reset the position of the ball to the paddle.
      # LaunchBall()         This method is used to start moving the ball from the paddle.
      # InitBall()           This method is used to reset the properties of the ball at the start of a new game.
      
 # BrickController Class: 
 ## This class is used to check the collision of the brick with the ball and check for power ups and initiate the necessary power ups.
      
 # PaddleController Class: 
 ## This class is used to control the movement of the paddle and check the collision of any power ups with the paddle.
      
 # TileGenerator Class: 
 ## This class is used to generate the grid of bricks.
      # PlaceBricks()     This method is used to generate the grid and place the tiles in them.
      # ClearTiles()      This methos is used to create the scene of existing tiles.
      # InitTiles()       This method is used to reset the tiles at the start of the game.
      # RemoveBrick()     This method is used to remove the destroyed brick from the list of tiles
 
 # UIHud Class: 
 ## This Class monitors and updates the UI element of the game.
      # UpdateScore() This method is used to update the score in the UI.
      # UpdateLives() This method is used to update the number of lives in the UI.
      # GameEnd()     This method is used to handle Game over sequence in the UI.
      # GameStart()   This method is used to handle Game start sequence in the UI.
      # GameWin()      This method is used to handle Game win sequence in the UI.
      
 # PowerUpBase Class: 
 ## This is the base power up class from which the power ups are derived.
 
 # PowerUpController Class: 
 ## This class controls the initialization and destructions of all the power ups .
      # OnPowerUpCollided()       This method checks if the power up is already active. If so deletes the previous instance and creates a new instance of the power up.
      # InitPowerUps()            This method removes the active power ups from the scene at the start of the game.
      # AddToPowerUpUIList()      This method adds the spawned powerUpUI to a list to track them.
      # RemoveFromPowerUpUIList() This method is used to remove the powerUpUI from the list and destroy the object.
 
 # PowerUpUI Class: 
 ## This class is used spawn the powerUpUI object when a brick is destroyed and check collision with the paddle.
      
 # SlowPowerUp Class: 
 ## This is derived class of the PowerUpBase class that is used to define the properties of the Slow power up.
      
 # PiercePowerUp Class: 
 ## This is derived class of the PowerUpBase class that is used to define the properties of the Pierce power up.
      
# Known Issues:
 ## When Piercing ball power up is activated, the game loops through all the bricks and change the collider on them to trigger in order for the piercing power up to work. Looping through a list in runtime can be an expensive process. This can be improved.
      
      
# Credits
* 2D Break Out assets - <https://unityassets4free.com/2d-breakout-pack/2/>
