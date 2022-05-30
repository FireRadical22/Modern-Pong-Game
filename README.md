# Modern-Pong-Game

Achievement Level: Gemini

Project Scope: A Modified version of Pong, where players have the ability to acquire skills from skill boxes and use them to aid in scoring against their opponents

Problem Motivation:
While attending an online programming course, the game Pong was introduced to us under the course’s Game track. The game was used as an example to teach us the basics of programming a game. Then, an idea came: Pong is a very simple game yet it does not bring the same level of excitement as today’s due to its simplicity. 

Proposed core features:
1. Skills 
Players can acquire skills by directing the ball towards skill boxes that randomly generate within the game board. Each player can only hold up to 3 skills in their inventory at any given time. Upon scoring, any skill that was already in use will be removed from the respective player's inventory. The skills players can use are as follows:

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

    2.1 Player vs Player:
    Players can play against each other on the same screen.(Couch Multiplayer) Each player uses a seperate set of keys to manoveurve the paddles and activate their         skills on a keyboard. The victor is determined by the first player to score 11 goals.

    2.2 Player vs AI:
    Players can fight against bots of 3 different difficulties: Easy, Medium, Hard. Players can only fight the bot at the next difficulty if they have defeated the bot     at the current difficulty.

3. Miscellanous
Players can enjoy different goal animations, ball trails, paddle and ball skins to use while playing against other Players/bots.

Design:

Plan: 

