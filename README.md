# SparkingSteel

If you are playing the game on the Unity editor, you will not be able to properly
control the game with a Keyboard. An Xbox 360 mapPED type controller should be used

Assets/ButtonAnimations.cs

controls the majority of the Player Mech's functionality

Assets/EnemyAI.cs

controls the majority of the Enemy AI

Assets/PlayerHealth.cs

manages and updates the player's health

Assets/EnemyHealth.cs

manages and updates the enemy's health

Assets/OVR/Scripts/Util/OVRPlayerController.cs

A script which came with the Oculus Unity package. Has been modified to include
the dodging functionality refered to as "dash" within the code. Properties have also been changed to get the 
desired speed, turning speed etc. from the controls.

Assets/Plugins/Android/Assets

An oculus sig file must be placed here for your own personal phone if you want to run a build of this project on your device
