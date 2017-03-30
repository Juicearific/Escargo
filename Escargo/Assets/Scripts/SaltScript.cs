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
            int slimeAmt = GetComponent<PlayerScript>().slime - slimeTic;
            if (slimeAmt < 0) slimeAmt = 0;
            GetComponent<PlayerScript>().slime = slimeAmt;

            yield return new WaitForSeconds(ticDuration);
        }
        yield return null; 
    }

    public override Texture getImage()
    {
        return Resources.Load<Texture>("Art/Pickups/Salt_small");
    }
}
