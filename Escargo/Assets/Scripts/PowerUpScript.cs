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

    public enum PowerUpType { Flower, Water, Sugar, Salt, Copper, Carrot };
    public PowerUpType powerUp;
    private int respawnTime = 15; // 15 seconds

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            /* Code for PickUp items
             * These items have no duration and no opposites.
             * Their effects are immediate - so no need to do anything intensive.
             * */
            if (powerUp == PowerUpType.Sugar)
            {
                int slimeAmt = collider.gameObject.GetComponent<PlayerScript>().slime - 30;
                if (slimeAmt < 0) { slimeAmt = 0; }
                collider.gameObject.GetComponent<PlayerScript>().changeSlimeBar(slimeAmt);
            }
            else if (powerUp == PowerUpType.Flower)
            {
                int slimeAmt = collider.gameObject.GetComponent<PlayerScript>().slime + 30;
                if (slimeAmt > PlayerScript.SLIME_MAX) { slimeAmt = PlayerScript.SLIME_MAX; }
                collider.gameObject.GetComponent<PlayerScript>().changeSlimeBar(slimeAmt);
            }
            else
            {
                /* Code for power ups with duration */
                PowerUpEffectScript script = null;

                /* Only Salt and Water are opposites right now. */
                if (powerUp == PowerUpType.Salt || powerUp == PowerUpType.Water)
                {
                    PowerUpType opposite;
                    if (powerUp == PowerUpType.Salt)
                        opposite = PowerUpType.Water;
                    else
                        opposite = PowerUpType.Salt;

                    if ((script = (PowerUpEffectScript)collider.gameObject.GetComponent(opposite.ToString() + "Script")) != null)
                    {
                        //If power up is opposite of power up on player. - removes power up and does not put new power up on
                        script.StopCoroutine(script.currentCoroutine);
                        script.removeEffect();
                        Destroy(script);
                    }
                }

                if (script == null) //If it is null then it means an opposite was not found or not looked for.
                {
                    if ((script = (PowerUpEffectScript)collider.gameObject.GetComponent(powerUp.ToString() + "Script")) != null)
                    {
                        //If same power up is already on player - removes the power up first
                        script.StopCoroutine(script.currentCoroutine);
                        script.removeEffect();
                        Destroy(script);
                    }
                    //puts new power up on
                    collider.gameObject.AddComponent(Type.GetType(powerUp.ToString() + "Script"));
                }
            }
            setActive(false);
            StartCoroutine(respawn());
        }
    }

    void setActive(bool active)
    {
        //Disable if false
        //Enable if true
        GetComponent<Renderer>().enabled = active;
        GetComponent<BoxCollider2D>().enabled = active; //Dependent on collider used.
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        setActive(true);
    }
}
