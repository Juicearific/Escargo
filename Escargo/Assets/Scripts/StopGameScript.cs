using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class StopGameScript : MonoBehaviour {
	private const int W_WIDTH = 200;
	private const int W_HEIGHT = 80;
	private Rect window = new Rect((Screen.width - W_WIDTH)  /2, (Screen.height - W_HEIGHT) / 2, W_WIDTH, W_HEIGHT);
	private bool displayExitWindow = false;

	void OnGUI() {
		if (displayExitWindow)
			window = GUI.Window (0, window, DialogWindow, "Are you sure you want to exit?");
	}

	private void DialogWindow(int windowID) {
		int buttonHeight = 40;
		int buttonWidth = (W_WIDTH - 10) / 2;
		int buttonY = 20;

		if (GUI.Button (new Rect (5, buttonY, buttonWidth, buttonHeight), "Yes")) {
			displayExitWindow = false;
			SceneManager.LoadScene ("MainMenu");
		} else if (GUI.Button (new Rect (W_WIDTH / 2, buttonY, buttonWidth, buttonHeight), "No")) {
			displayExitWindow = false;
			//Do nothing.
		}
			
	}
	
	public void openExitWindow() {
		displayExitWindow = true;
	}

}
