//Stuns player for a reduced duration
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperScript : PowerUpEffectScript
{
    float movementSpeed;
    
    public override void addEffect()
    {
        powerUpDur = 2; // Change duration to 2 seconds.
        movementSpeed = gameObject.GetComponent<PlayerScript>().moveSpeed;
        gameObject.GetComponent<PlayerScript>().moveSpeed = 0;
    }

    public override void removeEffect()
    {
        gameObject.GetComponent<PlayerScript>().moveSpeed += movementSpeed;
        //Added instead of set just incase a movement speed boost wore off while it is stunned.
    }

    public override Texture getImage()
    {
        return Resources.Load<Texture>("Art/Pickups/copper_ring_small");
    }
}
