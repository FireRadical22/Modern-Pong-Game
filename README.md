# Modern-Pong-Game

Achievement Level: Gemini

Project Scope: A Modified version of Pong, where players have the ability to acquire skills from item boxes and use them to aid in scoring against their opponents

Problem Motivation:
While attending an online programming course, the game Pong was introduced to us under the course’s Game track. The game was used as an example to teach us the basics of programming a game. We thought of modernising Pong, bringing in more complexity to the game and making it more enjoyable to play compared to the original Pong Game

User Stories:


Game Ideation:

GameField Design

We have decided to follow the original Pong's theme (Black and White) while setting the standard paddle and ball design to be that of the original Pong Game, keeping the game simple for users.

Management of Skills

We generated a catalogue of all abilities in the game in an AbilityCatalogue so as to refer to whenever the player is granted a skill upon the ball's interaction with the item box

Deployment of Skills

Each player is given an AbilityHolder to activate any skills there are granted by the item box. The AbilityHolder can only hold 3 skills at any given time so that players do not have too many skills in their hand to use during the game.

 
 Player vs Player/Bot

    2.1 Player vs Player: (PvP)
    Players can play against each other on the same screen.(Couch Multiplayer) Each player uses a seperate set of keys to manoveurve the paddles and activate their         skills on a keyboard. The victor is determined by the first player to score 11 goals.

    2.2 Player vs AI: (PvA)
    Players can fight against bots of 3 different difficulties: Easy, Medium, Hard. Players can only fight the bot at the next difficulty if they have defeated the bot     at the current difficulty.
    
    2.2.1 Easy Mode
    In this mode, the bot will track and follow the ball's y-coordinates only when the ball is moving away from the bot and activates its skill whenever there is a skill present in its inventory.
    
    2.2.2 Medium Mode
    In this mode, the bot will track and follow the ball's y-coordinates all the time as well but however, will activate it's skills periodically if present. Additionally, the bot will be slightly smarter than easy mode bot in its use of TimeAbility skills.
    
    2.2.3 Hard Mode
    In this mode, the bot will now predict the movement of the ball and activates it's skills and varying intervals now. Additionally, the bot will be slightly smarter than medium bot in its use of all the skills.
    
3. Animations, trials and skins

   Players can enjoy different goal animations, ball trails, paddle and ball skins to use while playing against other Players/bots.

Implementation: 

   Gameplay:
      
      1. Create the base Pong Game (with the standard paddle and ball skin)
      2. Create sound effects on collision between paddle and ball using soundTrap 
      3. Add in sound effects created into the game.
      3. Add in a simple goal animation and ball trail using Particle System component
      4. Add in natural speed multiplier that increases speed of the ball after each collision with the paddle. 
      5. Add in scoreboard to keep track of goals scored
      6. Add in a victory/defeat animation for PvP and PvA matches
   
   Skills:
   
      1. InvisiBall: toggle sprite renderer of the ball depending on whether the skill is activated/deactivated
      2. BounceBall: change value of Ball's gravityScale. When activated, set gravityScale to value set; otherwise, 0. Negate gravityScale if y-component of vector of incoming ball is positive.
      3. Multi-Ball: Instantiate 3 more Balls on collision with user's paddle with slightly darker shade of colour. Vary the y-component of all 4 ball's vectors.
      4. Slingshot: Multiply velocity of Ball by a value set during the duration of the time set. Upon deactivation, divide velocity of Ball by (Natural speed multiplier / value set).
      5. Impassable: Multiply length of the paddle by a value set when activated. Divide length of the paddle by the same value when deactivated.
      
      Skill Holder (Holds up to 3 skills for each player at any given time):
      
       - Use delegate to determine which skill's Activate function to call upon pressing key. (if a skill is found in the player's inventory, mapped to the key pressed)
       - Each delegate is mapped to a key, resulting in a total 6 delegates mapped to 6 keys.
      
      Item Box:
      
       - Create a randomiser that generates a random enum value that corresponds to a skill. The skill's Activate function is then fed to an available Delegate (if any)
   
   Miscellaneous:
   
     - Add in UI start menu
     - Add in UI pause menu 
     - Create music for start menu using soundTrap
     - Create a few musics for gameplay
     - Create an Audio Manager for gameplay musics (to be played in order)
     - Add in more goal animations and ball trails using particle system and trail renderer respectively
     - Create new paddle and ball skins using GIMP/Microsoft Paint 3D


Tech Stack:

   1. Unity Game Engine
       The main game engine used for the implementation of the game. The engine can also allow generate builds that are downloadable by people to play the game
