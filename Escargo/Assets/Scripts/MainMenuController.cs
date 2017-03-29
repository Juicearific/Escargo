using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene("Demo");
    }

    public void HowTo()
    {
        SceneManager.LoadScene("Controls");
    }
}
