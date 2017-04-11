//Increases movement speed of player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : PowerUpEffectScript {
    
    public override void addEffect()
    {
		player.setMoveSpeed(player.getMoveSpeed() + (2f * player.powerUpEffects["Carrot"]));
    }

    public override void removeEffect()
    {
        base.removeEffect();
		player.setMoveSpeed(player.getMoveSpeed() - (2f * player.powerUpEffects["Carrot"]));
    }
}
