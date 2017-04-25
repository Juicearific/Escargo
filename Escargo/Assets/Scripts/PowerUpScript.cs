/*
 * This script handles:
 *  - When a power up is added or removed to/from a player.
 *  - Respawn of the power ups.
 *  - What kind of power up is being added/removed.
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
	/* CONSTANTS */
	private const int SUGAR_EFFECT = 30;
	private const int FLOWER_EFFECT = 30;
	public const int NUM_POWERUPS = 6;
	public GameObject displayPrefab;

	public enum PowerUpType
	{
		Flower,
		Water,
		Sugar,
		Salt,
		Copper,
		Carrot};

	public Sprite[] images = new Sprite[NUM_POWERUPS];
	public PowerUpType powerUp;
	private int respawnTime = 5;
	// 5 seconds

	private bool negativePowerUp() {
		return powerUp == PowerUpType.Copper || powerUp == PowerUpType.Sugar || powerUp == PowerUpType.Salt;
	}

	private void generateRandomPowerUp() {
		// If it is a negative power up, make sure it comes back as a positive one next.
		while (negativePowerUp()) {
			powerUp = (PowerUpType)UnityEngine.Random.Range (0, NUM_POWERUPS);
		}
		GetComponent<SpriteRenderer> ().sprite = images [(int)powerUp];
	}

	void Start() {
		generateRandomPowerUp ();
	}
	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "Player") {
			PlayerScript player = collider.gameObject.GetComponent<PlayerScript> ();
			if (player.powerUpEffects [powerUp.ToString ()] == 0) {
				//If the effect is 0... do not eat the power up at all. Ignore it.
				return;
			}
			/* Code for PickUp items
             * These items have no duration and no opposites.
             * Their effects are immediate - so no need to do anything intensive.
             * */
			if (powerUp == PowerUpType.Sugar) {
				int slimeAmt = player.getSlime () - (int)(SUGAR_EFFECT * player.powerUpEffects ["Sugar"]);
				if (slimeAmt < 0) { 
					slimeAmt = 0;
				}
				player.changeSlimeBar (slimeAmt);
			} else if (powerUp == PowerUpType.Flower) {
				int slimeAmt = player.getSlime () + (int)(FLOWER_EFFECT * player.powerUpEffects ["Flower"]);
				if (slimeAmt > player.SLIME_MAX) { 
					slimeAmt = player.SLIME_MAX; 
				}
				player.changeSlimeBar (slimeAmt);
			} else {
				/* Code for power ups with duration */
				PowerUpEffectScript script = null;

				/* Only Salt and Water are opposites right now. */
				if (powerUp == PowerUpType.Salt || powerUp == PowerUpType.Water) {
					PowerUpType opposite;
					if (powerUp == PowerUpType.Salt)
						opposite = PowerUpType.Water;
					else
						opposite = PowerUpType.Salt;

					if ((script = (PowerUpEffectScript)collider.gameObject.GetComponent (opposite.ToString () + "Script")) != null) {
						//If power up is opposite of power up on player. - removes power up and does not put new power up on
						script.StopCoroutine (script.currentCoroutine);
						script.removeEffect ();
						Destroy (script);
					}
				}

				if (script == null) { //If it is null then it means an opposite was not found or not looked for.
					if ((script = (PowerUpEffectScript)collider.gameObject.GetComponent (powerUp.ToString () + "Script")) != null) {
						//If same power up is already on player - removes the power up first
						script.StopCoroutine (script.currentCoroutine);
						script.removeEffect ();
						Destroy (script);
					}
					//puts new power up on
					PowerUpEffectScript s = (PowerUpEffectScript)collider.gameObject.AddComponent (Type.GetType (powerUp.ToString () + "Script"));
					GameObject prefab = Instantiate (displayPrefab);
					prefab.GetComponentInChildren<UnityEngine.UI.RawImage> ().texture = images [(int)powerUp].texture;
					s.setPrefab (prefab);
				}
			}
			setActive (false);
			StartCoroutine (respawn ());
		}
	}

	void setActive (bool active)
	{
		//Disable if false
		//Enable if true
		GetComponent<Renderer> ().enabled = active;
		GetComponent<BoxCollider2D> ().enabled = active; //Dependent on collider used.
	}

	IEnumerator respawn ()
	{
		yield return new WaitForSeconds (respawnTime);
		generateRandomPowerUp ();
		setActive (true);
	}
}
