/*
 * This script handles:
 *  - The duration of the power up
 *  - Subclasses of this abstract class handles:
 *      - Effect that the power up provides
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffectScript : MonoBehaviour {

    public int powerUpDur = 5; //5 Seconds
    public Coroutine currentCoroutine;
    
	void Start () {
        addEffect();
        currentCoroutine = StartCoroutine(powerUpTimer(powerUpDur));
	}

    IEnumerator powerUpTimer(int timer)
    {
        yield return new WaitForSeconds(timer);
        removeEffect();
        Destroy(this);
    }

    public abstract void addEffect();
    public abstract void removeEffect();

}
