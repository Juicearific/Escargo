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
        Debug.Log("Initial Stats: Speed: " + GetComponent<PlayerScript>().moveSpeed + " Slime: " + GetComponent<PlayerScript>().slime);
        yield return new WaitForSeconds(timer);
        removeEffect();
        Debug.Log("Final Stats: Speed: " + GetComponent<PlayerScript>().moveSpeed + " Slime: " + GetComponent<PlayerScript>().slime);
        Destroy(this);
    }

    public abstract void addEffect();
    public abstract void removeEffect();

}
