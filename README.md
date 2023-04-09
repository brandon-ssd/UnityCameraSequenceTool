# Camera Sequence + Player Follow Tool for Unity

These scripts allow you to create cutscenes in Unity in real time, quick and simple. Just follow these few guidelines for working results.

* CameraActionEditor script must go in Editor folder.
* Camera move script must be attached to your camera.
* Camera trigger must be attached to a boxcollider (with or without sprite, doesn't matter). Set this as a trigger.
* Player is your player movement script. This could be different if you already have a movement script. Just reference this in cameramovescript instead of "player". 
* Make Rigidbody interpolate to remove jittering.
* Attach the up down motion script to anything you want to bounce.

The Camera Move Script references your player's movement script. Mine is called "player" but yours could be different if you have a different player controller.
Make sure this refers to the correct script when you make the playerController variable for disabling movement.

Camera trigger can be attached to any gameobject that has a collider 2D component.

The trigger once boolean is used to either make it trigger one time before never being able to be triggered again, or triggered as many times as you collide with the trigger.

As for actions, simply add actions to the list that we have under camera trigger, and be sure to return to player as the last action.
When you wish to require an input to return to the player,add the input to wait action, **THEN BE SURE TO ADD A WAIT ACTION, WITH A VERY SMALL WAIT TIME, e.g. 0.01, FOR IT TO CONTINUE THE ACTION LIST**
This is very important.

I only included the up and down motion script to give some demonstration scripts to prove that following the objects also works as well.


Enjoy!
