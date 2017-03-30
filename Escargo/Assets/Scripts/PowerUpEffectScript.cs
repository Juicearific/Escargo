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
    public GameObject generatedDisplay;

	void Start ()
    {
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

    IEnumerator powerUpTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        removeEffect();
        Destroy(generatedDisplay);
        Destroy(this);
    }

    private void generateDisplay()
    {
        generatedDisplay = Instantiate(Resources.Load<GameObject>("PowerUpTimer"));//Get Prefab from Resources folder
        Vector3 scale = generatedDisplay.transform.localScale;
        generatedDisplay.transform.SetParent(gameObject.transform.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.transform);
        generatedDisplay.transform.localScale = scale;
        generatedDisplay.GetComponentInChildren<RawImage>().texture = getImage();
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
    public abstract Texture getImage();
}
