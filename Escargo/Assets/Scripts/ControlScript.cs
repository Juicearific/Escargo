using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour
{


    public InputField p1Up;
    public InputField p1Left;
    public InputField p1Right;
    public InputField p1Down;
    public InputField p1Slime;
    public InputField p1Map;

    public InputField p2Up;
    public InputField p2Left;
    public InputField p2Right;
    public InputField p2Down;
    public InputField p2Slime;
    public InputField p2Map;

    public InputField p3Up;
    public InputField p3Left;
    public InputField p3Right;
    public InputField p3Down;
    public InputField p3Slime;
    public InputField p3Map;

    public InputField p4Up;
    public InputField p4Left;
    public InputField p4Right;
    public InputField p4Down;
    public InputField p4Slime;
    public InputField p4Map;

    public void Update()
    {

    }

    public void Start()
    {

    }

    public void goHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnGUI()
    {

        // Up
        if (p1Up.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Up.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Up.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Up.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }
        // Left
        if (p1Left.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Left.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Left.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Left.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }
        // Right
        if (p1Right.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Right.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Right.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Right.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }
        // Down
        if (p1Down.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Down.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Down.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Down.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }
        // Slime
        if (p1Slime.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Slime.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Slime.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Slime.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }
        // Map
        if (p1Map.isFocused)
        {
            if (Event.current.isKey)
            {
                p1Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
        }

        if (p2Map.isFocused)
        {
            if (Event.current.isKey)
            {
                p2Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
        }

        if (p3Map.isFocused)
        {
            if (Event.current.isKey)
            {
                p3Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
        }

        if (p4Map.isFocused)
        {
            if (Event.current.isKey)
            {
                p4Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
        }


    }

}