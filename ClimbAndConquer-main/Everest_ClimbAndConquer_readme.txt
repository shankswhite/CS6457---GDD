i. Start Scene:
Title Screen - Climb and Conquer
Our main game Scene: AlphaTerrain

ii. How to Play:
- Please use keyboard and mouse as controller to play the game. 

1. Click on begin the climb to start the game. During the game, Click ESC to pop up the in game menu.

2. Basic objective:
Move up the hill in the direction in front of you and try to reach the top. The goal is to reach the flag on top of mountains. Player can see the flag from the respawn point.

3. Failure conditions:
The stamina bar reaches empty.

4. Duration of the game:
5-10 minutes

5.Movement method:
- Use WASD to move, use mouse to turn.
- Use Tab to switch out guns and mouse 0 to shoot. Press Tab again to retrieve the gun. Movement speed is reduced when shooting and you can't turn well
- Use space to jump and climb obstacles.

6. Brief description of the game:
As you move towards the top of the mountain flag, monkeys will spawn in your field of view on the way to obstruct you, the monkeys can damage you by throwing bananas and touching. Also, each climb will drain your stamina.



iv. Each teammate's work
Zhao, Xiaofeng:
- Did: Character Animation; Animator; Scripts for Movement, Climbing, Shooting; Playtest
- Assets implemented: Main Character; Cinemachine Free Look; Winpoint; Climbable Obstacles; Banana_Rifle
- Scripts: Character/CharacterAudioHandler, CharacterCommon, CharacterInput, CharacterScriptMotionController, GroundScanner, VaultController, VaultToggle; Shoot/CrossHair, EnemyDamage, Projectiles, ShootSystem, WeaponController

Liu, Xiang:
- Did: Enemy System (Enemy appreach, hit, and disapreach); Character Destination Projection (used for Enemy system to generate enemy on predicted destination); Path Finding; Weather System (adjust snow level based on height/altitude);  
- Assets implemented: Enemy (Monkey) Manager; Weather On Top Of Character; Footstep Effect; In-game Resume.
- Scripts: DestinationPredictor (predict where the character will move to), MonkeysController (control when the monkey show up and disappear), WeatherController (display snow based on location of character), BackgroundMusicController  (play background music), YetiAudioController (play audio effect when Yeti approaches), part of YetiAttackController (worked the sound effect & bug fix of attack is not causing damage), GameQuitter;

Baghal, Zaid
- Did: Level Design, Audio (Music and SFX), Projectile Spawner, Terrain Prefabs
- Assets Implemented: AlphaTerrain, Audio, RockProjectile, Yeti
- Scripts: RockSpawner, RockCollision. 

Raja, Sidharth:
- Did: Enemy (Monkey) AI, Enemy State Machine (idle, moving, attacking, dying), Enemy AI Attacking (throwing a bananas), Enemy aiming at player, Tuning animation events to visible action for "game feel"
- Assets Implemented: Monkey (Enemy) prefab, BananaSphere (Rudimentary Ammo) prefab, Old Terrain (discarded in favor of different version)
- Scripts: MonkeyMovement (this is kind of a catchall for all the monkey stuff)

Hamzah Hameed:
-What I did: Project Management, Stamina System, Stamina Bar, Gameover State, Health Powerups, Invincibilty Powerups, Speedup Powerup, main menu and Graphics for the main menu and for the game over screen.
-Assets Implemented: Main menu, Stamina bar, Health Powerup, Invincible Powerup, Speedup Powerup, Gameover Screen. 
-Scripts: Stamina, HealthPowerup, InvinciblePowerup, PauseMenuToggle, PowerupCollector, SpeedUpPowerup, GameStarter.
