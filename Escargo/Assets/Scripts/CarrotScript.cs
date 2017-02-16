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
        gameObject.GetComponent<PlayerScript>().moveSpeed -= 2f;
    }
}
