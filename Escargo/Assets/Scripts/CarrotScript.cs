//Increases movement speed of player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : PowerUpEffectScript {
    
    public override void addEffect()
    {
        gameObject.GetComponent<PlayerScript>().moveSpeed += 2f;
    }

    public override void removeEffect()
    {
        base.removeEffect();
        gameObject.GetComponent<PlayerScript>().moveSpeed -= 2f;
    }

    public override Texture getImage()
    {
        return Resources.Load<Texture>("Art/Pickups/carrot_small");
    }
}
