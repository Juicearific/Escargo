using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinSceneScript : MonoBehaviour {

	public Sprite[] winImages = new Sprite[GlobalScript.NUM_PLAYERS];
	private const int PIERRE = 0;
	private const int KENTA = 1;
	private const int LILJIM = 2;
	private const int BIGBERTHA = 3;
	public Text winnerDisplayText;
	public Image winnerDisplayImage;

	void Start () {
		int winningPlayerID = WinScript.winningPlayerID;
			
		winnerDisplayText.text = "Player " + winningPlayerID + " wins!";
		switch (WinScript.winningPlayerSnailType) {
		case "pierre":
			winnerDisplayImage.sprite = winImages[PIERRE];
				break;
		case "kenta":
			winnerDisplayImage.sprite = winImages[KENTA];
			break;
		case "liljim":
			winnerDisplayImage.sprite = winImages[LILJIM];
			break;
		default:
			//Assume bertha
			winnerDisplayImage.sprite = winImages[BIGBERTHA];
			break;
		}
	}

	public void returnToStart() {
		SceneManager.LoadScene ("MainMenu");
	}
}
