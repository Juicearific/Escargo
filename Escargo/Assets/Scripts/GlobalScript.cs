using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour {
	public const int NUM_PLAYERS = 4;
	public GameObject[] snaillingDisplayPrefabs = new GameObject[NUM_PLAYERS];
	// 0 => Pierre
	// 1 => Kenta
	// 2 => LilJim
	// 3 => BigBertha
	private GameObject[] snaillingPanels = new GameObject[NUM_PLAYERS];
	private string[] snailTypes = new string[NUM_PLAYERS];
	private Color[] snailColors = new Color[NUM_PLAYERS];

	public void changeDisplay(int playerID, int newSnaillingCount) {
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (snaillingPanels [i] != null) {
				snaillingPanels [i].GetComponentInChildren<UnityEngine.UI.Text> ().text = newSnaillingCount.ToString ()
				+ "/" + SnaillingScript.NUM_SNAILLINGS.ToString ();
				snaillingPanels [i].GetComponentInChildren<UnityEngine.UI.Slider> ().value = newSnaillingCount;
			}
		}
	}

	public void setSnaillingDisplay(int playerID, string snailType, GameObject snaillingPanel, Color snailColor) {
		snaillingPanels [playerID - 1] = snaillingPanel;
		snailTypes [playerID - 1] = snailType;
		snailColors [playerID - 1] = snailColor;
		setupPanels ();
	}

	public void setupPanels() {
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (snaillingPanels [i] != null) {
				foreach (Transform child in snaillingPanels[i].transform)
					GameObject.Destroy (child.gameObject);
				for (int j = 0; j < NUM_PLAYERS; j++) {
					if (snailTypes [j] != null) {
						GameObject prefab;
						if (snailTypes[j] == "pierre") {
							prefab = Instantiate (snaillingDisplayPrefabs [0]);
						} else if (snailTypes[j] == "kenta") {
							prefab = Instantiate (snaillingDisplayPrefabs [1]);
						} else if (snailTypes[j] == "liljim") {
							prefab = Instantiate (snaillingDisplayPrefabs [2]);
						} else {//Assume it's bertha.
							prefab = Instantiate (snaillingDisplayPrefabs [3]);
						}
						prefab.transform.SetParent (snaillingPanels [i].transform, false);
						prefab.GetComponentInChildren<UnityEngine.UI.Slider> ().fillRect.GetComponent<UnityEngine.UI.Image> ().color = snailColors [j];
					}
				}

			}
		}
	}
}