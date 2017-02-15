using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : PowerUpEffectScript
{
    private int ticDuration = 1;
    private int slimeTic = 5;

    public override void addEffect()
    {
        Debug.Log(name + "invoked addEffect");
        int numOfTics = Mathf.FloorToInt(powerUpDur / ticDuration);
        StartCoroutine(tic(numOfTics));
    }

    public override void removeEffect()
    {
        //Do not need to remove it. It stops when it is finished.
    }

    private IEnumerator tic(int numOfTics)
    {
        for (int i = 0; i < numOfTics; i++)
        {
            int slimeAmt = GetComponent<PlayerScript>().slime + slimeTic;
            if (slimeTic > PlayerScript.SLIME_MAX) slimeAmt = PlayerScript.SLIME_MAX;
            GetComponent<PlayerScript>().slime = slimeAmt;

            yield return new WaitForSeconds(ticDuration);
        }
        yield return null;
    }
}
