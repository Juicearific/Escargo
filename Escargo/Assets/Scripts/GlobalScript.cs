using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour {
	public const int NUM_PLAYERS = 4;
	/* NOTE! PLEASE ORDER BY PLAYER ID NUMBER */
	/* i.e. index 0 in array is the player with an ID of 1! */
	public GameObject[] snaillingPanels = new GameObject[NUM_PLAYERS];
	public GameObject[] snaillingDisplayPrefabs = new GameObject[NUM_PLAYERS];
	private GameObject[] instantiatedDisplays = new GameObject[NUM_PLAYERS];


	// Use this for initialization
	void Start () {
		//Display snaillings
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (snaillingPanels [i] != null) {
				for (int j = 0; j < NUM_PLAYERS; j++) {
					if (snaillingDisplayPrefabs [j] != null) {
						GameObject display = Instantiate (snaillingDisplayPrefabs[j]);
						display.transform.SetParent(snaillingPanels[i].transform, false);
						instantiatedDisplays [j] = display;
					}
				}
			}
		}
	}

	public void changeDisplay(int playerID, int newSnaillingCount) {
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (snaillingPanels [i] != null) {
				snaillingPanels [i].GetComponentInChildren<UnityEngine.UI.Text> ().text = newSnaillingCount.ToString ()
				+ "/" + SnaillingScript.NUM_SNAILLINGS.ToString ();
			}
		}
	}
}
