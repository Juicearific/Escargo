using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsGUI : MonoBehaviour {

    



    // Use this for initialization
    void Start () {
		
	}

    void OnGUI()
    {

        // Order is Up, Left, Right, Down, Slime, Map

        //Player 1:
        string p1UString = GUI.TextField(new Rect(10, 10, 100, 20), OptionsStatic.controls[0,0].ToString());
        OptionsStatic.controls[0, 0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1UString);
        string p1LString = GUI.TextField(new Rect(10, 40, 100, 20), OptionsStatic.controls[0,1].ToString());
        OptionsStatic.controls[0,1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1LString);
        string p1RString = GUI.TextField(new Rect(10, 70, 100, 20), OptionsStatic.controls[0, 2].ToString());
        OptionsStatic.controls[0, 2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1RString);
        string p1DString = GUI.TextField(new Rect(10, 100, 100, 20), OptionsStatic.controls[0, 3].ToString());
        OptionsStatic.controls[0, 3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1DString);
        string p1SString = GUI.TextField(new Rect(10, 130, 100, 20), OptionsStatic.controls[0, 4].ToString());
        OptionsStatic.controls[0, 4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1SString);
        string p1MString = GUI.TextField(new Rect(10, 160, 100, 20), OptionsStatic.controls[0, 5].ToString());
        OptionsStatic.controls[0, 5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p1MString);

        //Player 2:
        string p2UString = GUI.TextField(new Rect(120, 10, 100, 20), OptionsStatic.controls[1,0].ToString());
        OptionsStatic.controls[1,0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2UString);
        string p2LString = GUI.TextField(new Rect(120, 40, 100, 20), OptionsStatic.controls[1,1].ToString());
        OptionsStatic.controls[1,1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2LString);
        string p2RString = GUI.TextField(new Rect(120, 70, 100, 20), OptionsStatic.controls[1,2].ToString());
        OptionsStatic.controls[1,2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2RString);
        string p2DString = GUI.TextField(new Rect(120, 100, 100, 20), OptionsStatic.controls[1,3].ToString());
        OptionsStatic.controls[1,3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2DString);
        string p2SString = GUI.TextField(new Rect(120, 130, 100, 20), OptionsStatic.controls[1,4].ToString());
        OptionsStatic.controls[1,4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2SString);
        string p2MString = GUI.TextField(new Rect(120, 160, 100, 20), OptionsStatic.controls[1,5].ToString());
        OptionsStatic.controls[1,5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p2MString);

        //Player 3:
        string p3UString = GUI.TextField(new Rect(230, 10, 100, 20), OptionsStatic.controls[2,0].ToString());
        OptionsStatic.controls[2,0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3UString);
        string p3LString = GUI.TextField(new Rect(230, 40, 100, 20), OptionsStatic.controls[2,1].ToString());
        OptionsStatic.controls[2,1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3LString);
        string p3RString = GUI.TextField(new Rect(230, 70, 100, 20), OptionsStatic.controls[2,2].ToString());
        OptionsStatic.controls[2,2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3RString);
        string p3DString = GUI.TextField(new Rect(230, 100, 100, 20), OptionsStatic.controls[2,3].ToString());
        OptionsStatic.controls[2,3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3DString);
        string p3SString = GUI.TextField(new Rect(230, 130, 100, 20), OptionsStatic.controls[2,4].ToString());
        OptionsStatic.controls[2,4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3SString);
        string p3MString = GUI.TextField(new Rect(230, 160, 100, 20), OptionsStatic.controls[2,5].ToString());
        OptionsStatic.controls[2,5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p3MString);

        //Player 4:
        string p4UString = GUI.TextField(new Rect(340, 10, 100, 20), OptionsStatic.controls[3,0].ToString());
        OptionsStatic.controls[3,0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4UString);
        string p4LString = GUI.TextField(new Rect(340, 40, 100, 20), OptionsStatic.controls[3,1].ToString());
        OptionsStatic.controls[3,1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4LString);
        string p4RString = GUI.TextField(new Rect(340, 70, 100, 20), OptionsStatic.controls[3,2].ToString());
        OptionsStatic.controls[3,2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4RString);
        string p4DString = GUI.TextField(new Rect(340, 100, 100, 20), OptionsStatic.controls[3,3].ToString());
        OptionsStatic.controls[3,3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4DString);
        string p4SString = GUI.TextField(new Rect(340, 130, 100, 20), OptionsStatic.controls[3,4].ToString());
        OptionsStatic.controls[3,4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4SString);
        string p4MString = GUI.TextField(new Rect(340, 160, 100, 20), OptionsStatic.controls[3,5].ToString());
        OptionsStatic.controls[3,5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), p4MString);

        if (GUI.Button(new Rect(10, 210, 200, 20), "Exit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
