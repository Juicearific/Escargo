/*
 * This script handles:
 *  - The duration of the power up
 *  - Subclasses of this abstract class handles:
 *      - Effect that the power up provides
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUpEffectScript : MonoBehaviour {

    public float powerUpDur = 5.0f; //5 Seconds
    private float secondsLeft;
    public Coroutine currentCoroutine;
	public GameObject generatedDisplay = null;
	public PlayerScript player;

	void Start ()
    {
		player = GetComponent<PlayerScript> ();
        addEffect();
        secondsLeft = powerUpDur;
        generateDisplay();
        currentCoroutine = StartCoroutine(powerUpTimer(powerUpDur));
	}

    void Update()
    {
        powerUpDur -= Time.deltaTime;
        if (powerUpDur < secondsLeft) //Re-display every 1 second.
        {
            generatedDisplay.GetComponentInChildren<Text>().text = ((int)powerUpDur).ToString() + "s";
            secondsLeft -= 1.0f;
        }
        
    }
	public void setPrefab(GameObject displayPrefab) {
		generatedDisplay = displayPrefab;
	}
    IEnumerator powerUpTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        removeEffect();
        Destroy(generatedDisplay);
        Destroy(this);
    }

    private void generateDisplay()
    {
		if (generatedDisplay == null) {
			Debug.LogError("Please send a prefab into the power up effect script.");
		}
        generatedDisplay.transform.SetParent(gameObject.transform.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.transform, false);
        generatedDisplay.GetComponentInChildren<Text>().text = powerUpDur.ToString() + "s";
    }

    public abstract void addEffect();
    public virtual void removeEffect()
    {
        if (generatedDisplay != null)
        {
            Destroy(generatedDisplay);
        }
    }
}
