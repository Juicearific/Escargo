using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour {
	public const int NUM_PLAYERS = 4;
	private const int PIERRE = 0;
	private const int KENTA = 1;
	private const int LILJIM = 2;
	private const int BIGBERTHA = 3;
	private Color[] colors = { Color.blue, Color.red, Color.yellow, Color.black };
	public GameObject[] snailPrefabs = new GameObject[NUM_PLAYERS];
	public GameObject[] snaillingDisplayPrefabs = new GameObject[NUM_PLAYERS];
	public GameObject[] player1SlimeSprites = new GameObject[MoveScript.NUM_SLIME_SPRITES];
	public GameObject[] player2SlimeSprites = new GameObject[MoveScript.NUM_SLIME_SPRITES];
	public GameObject[] player3SlimeSprites = new GameObject[MoveScript.NUM_SLIME_SPRITES];
	public GameObject[] player4SlimeSprites = new GameObject[MoveScript.NUM_SLIME_SPRITES];
	private GameObject[][] specificPlayerSprites = new GameObject[NUM_PLAYERS][];
	private static PlayerScript[] players = new PlayerScript[NUM_PLAYERS];
	private Dictionary<GameObject, GameObject[]> playersSnaillingDisplays = new Dictionary<GameObject, GameObject[]>();
	private float startX = 1.5f;
	private float startY = 24f;
	private const float START_DISTANCE = 6.0f;
	public static bool Ready = false;


	public void Start() {
		specificPlayerSprites [0] = player1SlimeSprites;
		specificPlayerSprites [1] = player2SlimeSprites;
		specificPlayerSprites [2] = player3SlimeSprites;
		specificPlayerSprites [3] = player4SlimeSprites;
		setupPlayers();
		setupCameras ();
		setupPanels ();
		Ready = true;
		}

	public void changeDisplay(int playerID, int newSnaillingCount) {
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (players [i] != null) {
				if (newSnaillingCount == 1) {
					Color c = new Color (players [playerID - 1].playerColor.r, players [playerID - 1].playerColor.g, players [playerID - 1].playerColor.b, 255);
					playersSnaillingDisplays [players [i].snaillingPanel] [playerID - 1].GetComponentInChildren<UnityEngine.UI.Slider> ().fillRect.GetComponent<UnityEngine.UI.Image> ().color = c;
				}
				playersSnaillingDisplays[players[i].snaillingPanel][playerID - 1].GetComponentInChildren<UnityEngine.UI.Text> ().text = newSnaillingCount.ToString ()
				+ "/" + SnaillingScript.NUM_SNAILLINGS.ToString ();
				playersSnaillingDisplays[players[i].snaillingPanel][playerID - 1].GetComponentInChildren<UnityEngine.UI.Slider> ().value = newSnaillingCount;
			}
		}
	}

	private void setupPlayers() {
		setupPlayer (OptionsStaticScript.p1Name, 1);
		setupPlayer (OptionsStaticScript.p2Name, 2);
		setupPlayer (OptionsStaticScript.p3Name, 3);
		setupPlayer (OptionsStaticScript.p4Name, 4);
	}

	private void setupPlayer(string name, int ID) {
		GameObject player = null;
		switch (name) {
		case "n/a":
			break;//No player selected
		case "pierre":
			player = Instantiate (snailPrefabs [PIERRE]);
			break;
		case "kenta":
			player = Instantiate (snailPrefabs [KENTA]);
			break;
		case "liljim":
			player = Instantiate (snailPrefabs [LILJIM]);
			break;
		case "bertha":
			player = Instantiate (snailPrefabs [BIGBERTHA]);
			break;
		}
		if (player != null) {
			PlayerScript script = player.GetComponent<PlayerScript> ();
			players [ID - 1] = script;
			script.playerID = ID;
			script.playerColor = colors [ID - 1];
			player.GetComponent<SnaillingScript> ().playerID = ID;
			player.GetComponent<MoveScript> ().setSlimeSprites (specificPlayerSprites[ID - 1]);
			player.GetComponent<MoveScript> ().updateColors ();
			player.transform.localPosition = new Vector2 (startX, startY - (START_DISTANCE * (ID - 1)));
		} else {
			players [ID - 1] = null;
		}
	}

	private void setupCameras() {
		int numCameras = 0;
		SplitscreenScript script = GetComponent<SplitscreenScript> ();
		if (players [0] != null) {
			script.cam1 = players [0].gameObject.GetComponentInChildren<Camera> ();
			numCameras++;
		}
		if (players [1] != null) {
			script.cam2 = players [1].gameObject.GetComponentInChildren<Camera> ();
			numCameras++;
		}
		if (players [2] != null) {
			script.cam3 = players [2].gameObject.GetComponentInChildren<Camera> ();
			numCameras++;
		}
		if (players [3] != null) {
			script.cam4 = players [3].gameObject.GetComponentInChildren<Camera> ();
			numCameras++;
		}
		SplitscreenScript.numCameras = numCameras;
		if (numCameras > 2) {
			for (int i = 0; i < NUM_PLAYERS; i++) {
				snaillingDisplayPrefabs [i].transform.localScale = new Vector3 (.7f, .7f, 1f);
			}
		} else {
			for (int i = 0; i < NUM_PLAYERS; i++) {
				snaillingDisplayPrefabs [i].transform.localScale = new Vector3 (1f, 1f, 1f);
			}
		}
		script.initCameras ();
	}

	private void setupPanels() {
		for (int i = 0; i < NUM_PLAYERS; i++) {
			if (players [i] != null) {
				GameObject[] prefabs = new GameObject[NUM_PLAYERS];
				for (int j = 0; j < NUM_PLAYERS; j++) {
					if (players[j] != null) {
						GameObject prefab;
						if (players[j].snailType == "pierre") {
							prefab = Instantiate (snaillingDisplayPrefabs [PIERRE]);
						} else if (players[j].snailType == "kenta") {
							prefab = Instantiate (snaillingDisplayPrefabs [KENTA]);
						} else if (players[j].snailType == "liljim") {
							prefab = Instantiate (snaillingDisplayPrefabs [LILJIM]);
						} else {//Assume it's bertha.
							prefab = Instantiate (snaillingDisplayPrefabs [BIGBERTHA]);
						}
						prefab.transform.SetParent (players [i].snaillingPanel.transform, false);
						Color c = new Color (players [j].playerColor.r, players [j].playerColor.g, players [j].playerColor.b, 0);
						prefab.GetComponentInChildren<UnityEngine.UI.Slider> ().fillRect.GetComponent<UnityEngine.UI.Image> ().color = c;
						prefab.GetComponentInChildren<UnityEngine.UI.Text> ().text = "0/" + SnaillingScript.NUM_SNAILLINGS.ToString ();
						prefabs [j] = prefab;
					}
				}
				playersSnaillingDisplays.Add (players [i].snaillingPanel, prefabs);
			}
		}
	}
	public static PlayerScript[] getPlayers() {
		return players;
	}
}