using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
	private static bool winner = false;
	public static int winningPlayerID;
	public static string winningPlayerSnailType;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Contains("Snailling") && !winner)
		{
			int playerID = getPlayerIDFromTag(collider.gameObject.tag);
			if (playerID != -1) // -1 implies playerID wasn't found in tag.
			{
				GameObject snaillingsPlayer = findPlayer(playerID);
				if (snaillingsPlayer != null)
				{
					PlayerScript player = snaillingsPlayer.GetComponent<PlayerScript> ();
					/* Change GUI to reflect that a snailling made it */
					player.incrementSnaillingsSaved (); //Increment number of snailings saved for player
					Camera.main.GetComponent<GlobalScript>().changeDisplay(playerID, player.getSnaillingsSaved());

					/* Destroy the snailling */
					destroySnailling(collider.gameObject, snaillingsPlayer.GetComponent<SnaillingScript>().snaillings);

					/* Check win condition */
					if (player.getSnaillingsSaved() >= SnaillingScript.NUM_SNAILLINGS)
					{
						winner = true; //Disable other snaillings from triggering a win.
						winningPlayerID = playerID;
						winningPlayerSnailType = player.snailType;
						snaillingsPlayer.GetComponent<PlayerScript>().playWin();
                        StartCoroutine (displayWinScreen());
					}
				}
			}
		}
	}


	IEnumerator displayWinScreen() {
		/* Display the whole map */
		foreach (PlayerScript p in GlobalScript.getPlayers()) {
			if (p != null) {
				p.GetComponentInChildren<Camera> ().enabled = false;
			}
		}
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene ("WinScene");
	}

    void destroySnailling(GameObject snailling, GameObject[] snaillings)
    {
        for (int i = 0; i < snaillings.Length; i++)
        { //Search for the snailling that collided within the snailling array.
            if (snaillings[i] != null) //Make sure we are not trying to call a method on a null gameObject.
            {
                if (snailling.GetInstanceID() == snaillings[i].GetInstanceID())
                { //If instance IDs are the same, they are the same snailling.
                    snaillings[i] = null;
                    Destroy(snailling);
                    return;
                }
            }
        }
    }

    GameObject findPlayer(int playerID)
    {
        GameObject[] listOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < listOfPlayers.Length; i++)
        {
            if (listOfPlayers[i].GetComponent<PlayerScript>().playerID == playerID)
            {
                return listOfPlayers[i]; // Return the player.
            }
        }
        return null; //null if no player found
    }

    int getPlayerIDFromTag(string tag)
    {
        int playerID = -1;
        char[] tagName = tag.ToCharArray();
        for (int i = 0; i < tagName.Length; i++)
        {//Check char one at a time to search for number and set playerID to that number.
            if (char.IsNumber(tagName[i]))
                int.TryParse(tagName[i].ToString(), out playerID);
        }
        return playerID;
    }
}
