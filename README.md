# Modern-Pong-Game

Achievement Level: Gemini

Project Scope: A Modified version of Pong, where players have the ability to acquire skills from skill boxes and use them to aid in scoring against their opponents

Problem Motivation:
While attending an online programming course, the game Pong was introduced to us under the course’s Game track. The game was used as an example to teach us the basics of programming a game. Then, an idea came: Pong is a very simple game yet it does not bring the same level of excitement as today’s due to its simplicity. 

Proposed core features:
1. Skills 

   Players can acquire skills by directing the ball towards skill boxes that randomly generate within the game board. Each player can only hold up to 3 skills in their    inventory at any given time. Upon scoring, any skill that was already in use will be removed from the respective player's inventory. The skills players can use are    as follows:

    1.1 InvisiBall:
    Upon colliding with the user's paddle, the ball will turn invisible. The skill is deactivated when the ball hits the opponent's paddle or the user scores a goal.

    1.2 BounceBall:
    Upon colliding with the user's paddle, the ball will bounce. The skill is deactivated when the ball hits the opponent's paddle or the user scores a goal.
    
    1.3 Slingshot:
    On activating, the ball will move faster for a short period of time. 
    
    1.4 Impassable:
    On activating, the paddle will become longer for a short period of time.
    
    1.5 Multi-Ball:
    Upon colliding with the user's paddle, the ball will summon 3 ball clones, camoflauging itself among the clones. The real ball will have a lighter shade compared       to the clones. The clones will not collide with the paddle and pass through it instead. The skill is deactivated when the real ball hits the opponent's paddle or       the user scores a goal.
 
2. Player vs Player/Bot

    2.1 Player vs Player: (PvP)
    Players can play against each other on the same screen.(Couch Multiplayer) Each player uses a seperate set of keys to manoveurve the paddles and activate their         skills on a keyboard. The victor is determined by the first player to score 11 goals.

    2.2 Player vs AI: (PvA)
    Players can fight against bots of 3 different difficulties: Easy, Medium, Hard. Players can only fight the bot at the next difficulty if they have defeated the bot     at the current difficulty.

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
      
      Skill Box:
      
       - Create a randomiser that generates a random enum value that corresponds to a skill. The skill's Activate function is then fed to an available Delegate (if any)
   
   Miscellaneous:
   
     - Add in UI start menu
     - Add in UI pause menu 
     - Create music for start menu using soundTrap
     - Create a few musics for gameplay
     - Create an Audio Manager for gameplay musics (to be played in order)
     - Add in more goal animations and ball trails using particle system and trail renderer respectively
     - Create new paddle and ball skins using GIMP/Microsoft Paint 3D
