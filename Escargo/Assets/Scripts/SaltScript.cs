using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltScript : PowerUpEffectScript
{
    private int ticDuration = 1;
    private int slimeTic = 5;

    public override void addEffect()
    {
        int numOfTics = Mathf.FloorToInt(powerUpDur / ticDuration);
        StartCoroutine(tic(numOfTics));
    }

    public override void removeEffect()
    {
        base.removeEffect();
        //Do not need to remove it. It stops when it is finished.
    }

    private IEnumerator tic(int numOfTics)
    {
        for (int i = 0; i < numOfTics; i++)
        {
			slimeTic = (int) (slimeTic * player.powerUpEffects ["Salt"]);
			int slimeAmt = player.getSlime() - slimeTic;
            if (slimeAmt < 0) slimeAmt = 0;
			player.changeSlimeBar(slimeAmt);

            yield return new WaitForSeconds(ticDuration);
        }
        yield return null; 
    }
}
