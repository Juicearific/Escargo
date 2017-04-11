//Stuns player for a reduced duration
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperScript : PowerUpEffectScript
{
    float movementSpeed;
    
    public override void addEffect()
    {
		powerUpDur = 2 * player.powerUpEffects ["Copper"]; // Change duration to 2 seconds.
		movementSpeed = player.getMoveSpeed();
		player.setMoveSpeed(0);
    }

    public override void removeEffect()
    {
        base.removeEffect();
		player.setMoveSpeed(player.getMoveSpeed() + movementSpeed);
        //Added instead of set just incase a movement speed boost wore off while it is stunned.
    }
}
