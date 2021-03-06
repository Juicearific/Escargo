﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsScript : MonoBehaviour
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
        updateAllFields();
    }

    public void updateAllFields()
    {
        p1Up.text = OptionsStaticScript.controls[0, 0].ToString();
        p1Left.text = OptionsStaticScript.controls[0, 1].ToString();
        p1Right.text = OptionsStaticScript.controls[0, 2].ToString();
        p1Down.text = OptionsStaticScript.controls[0, 3].ToString();
        p1Slime.text = OptionsStaticScript.controls[0, 4].ToString();
        p1Map.text = OptionsStaticScript.controls[0, 5].ToString();

        p2Up.text = OptionsStaticScript.controls[1, 0].ToString();
        p2Left.text = OptionsStaticScript.controls[1, 1].ToString();
        p2Right.text = OptionsStaticScript.controls[1, 2].ToString();
        p2Down.text = OptionsStaticScript.controls[1, 3].ToString();
        p2Slime.text = OptionsStaticScript.controls[1, 4].ToString();
        p2Map.text = OptionsStaticScript.controls[1, 5].ToString();

        p3Up.text = OptionsStaticScript.controls[2, 0].ToString();
        p3Left.text = OptionsStaticScript.controls[2, 1].ToString();
        p3Right.text = OptionsStaticScript.controls[2, 2].ToString();
        p3Down.text = OptionsStaticScript.controls[2, 3].ToString();
        p3Slime.text = OptionsStaticScript.controls[2, 4].ToString();
        p3Map.text = OptionsStaticScript.controls[2, 5].ToString();

        p4Up.text = OptionsStaticScript.controls[3, 0].ToString();
        p4Left.text = OptionsStaticScript.controls[3, 1].ToString();
        p4Right.text = OptionsStaticScript.controls[3, 2].ToString();
        p4Down.text = OptionsStaticScript.controls[3, 3].ToString();
        p4Slime.text = OptionsStaticScript.controls[3, 4].ToString();
        p4Map.text = OptionsStaticScript.controls[3, 5].ToString();
    }

    public void goHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void defaults()
    {
        OptionsStaticScript.reset();
        updateAllFields();
    }

    void OnGUI()
    {

        // Up
        if (p1Up.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 0] = Event.current.keyCode;
            }
            else
            {
                p1Up.text = OptionsStaticScript.controls[0, 0].ToString();
            }
        }
        

        if (p2Up.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 0] = Event.current.keyCode;
            }
            else
            {
                p2Up.text = OptionsStaticScript.controls[1, 0].ToString();
            }
        }

        if (p3Up.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 0] = Event.current.keyCode;
            }
            else
            {
                p3Up.text = OptionsStaticScript.controls[2, 0].ToString();
            }
        }

        if (p4Up.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Up.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 0] = Event.current.keyCode;
            }
            else
            {
                p4Up.text = OptionsStaticScript.controls[3, 0].ToString();
            }
        }
        // Left
        if (p1Left.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 1] = Event.current.keyCode;
            }
            else
            {
                p1Left.text = OptionsStaticScript.controls[0, 1].ToString();
            }
        }

        if (p2Left.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 1] = Event.current.keyCode;
            }
            else
            {
                p2Left.text = OptionsStaticScript.controls[1, 1].ToString();
            }
        }

        if (p3Left.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 1] = Event.current.keyCode;
            }
            else
            {
                p3Left.text = OptionsStaticScript.controls[2, 1].ToString();
            }
        }

        if (p4Left.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Left.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 1] = Event.current.keyCode;
            }
            else
            {
                p4Left.text = OptionsStaticScript.controls[3, 1].ToString();
            }
        }
        // Right
        if (p1Right.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 2] = Event.current.keyCode;
            }
            else
            {
                p1Right.text = OptionsStaticScript.controls[0, 2].ToString();
            }
        }

        if (p2Right.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 2] = Event.current.keyCode;
            }
            else
            {
                p2Right.text = OptionsStaticScript.controls[1, 2].ToString();
            }
        }

        if (p3Right.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 2] = Event.current.keyCode;
            }
            else
            {
                p3Right.text = OptionsStaticScript.controls[2, 2].ToString();
            }
        }

        if (p4Right.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Right.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 2] = Event.current.keyCode;
            }
            else
            {
                p4Right.text = OptionsStaticScript.controls[3, 2].ToString();
            }
        }
        // Down
        if (p1Down.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 3] = Event.current.keyCode;
            }
            else
            {
                p1Down.text = OptionsStaticScript.controls[0, 3].ToString();
            }
        }

        if (p2Down.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 3] = Event.current.keyCode;
            }
            else
            {
                p2Down.text = OptionsStaticScript.controls[1, 3].ToString();
            }
        }

        if (p3Down.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 3] = Event.current.keyCode;
            }
            else
            {
                p3Down.text = OptionsStaticScript.controls[2, 3].ToString();
            }
        }

        if (p4Down.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Down.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 3] = Event.current.keyCode;
            }
            else
            {
                p4Down.text = OptionsStaticScript.controls[3, 3].ToString();
            }
        }
        // Slime
        if (p1Slime.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 4] = Event.current.keyCode;
            }
            else
            {
                p1Slime.text = OptionsStaticScript.controls[0, 4].ToString();
            }
        }

        if (p2Slime.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 4] = Event.current.keyCode;
            }
            else
            {
                p2Slime.text = OptionsStaticScript.controls[1, 4].ToString();
            }
        }

        if (p3Slime.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 4] = Event.current.keyCode;
            }
            else
            {
                p3Slime.text = OptionsStaticScript.controls[2, 4].ToString();
            }
        }

        if (p4Slime.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Slime.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 4] = Event.current.keyCode;
            }
            else
            {
                p4Slime.text = OptionsStaticScript.controls[3, 4].ToString();
            }
        }
        // Map
        if (p1Map.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p1Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[0, 5] = Event.current.keyCode;
            }
            else
            {
                p1Map.text = OptionsStaticScript.controls[0, 5].ToString();
            }
        }

        if (p2Map.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p2Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[1, 5] = Event.current.keyCode;
            }
            else
            {
                p2Map.text = OptionsStaticScript.controls[1, 5].ToString();
            }
        }

        if (p3Map.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p3Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[2, 5] = Event.current.keyCode;
            }
            else
            {
                p3Map.text = OptionsStaticScript.controls[2, 5].ToString();
            }
        }

        if (p4Map.isFocused)
        {
            if (Event.current.isKey && !OptionsStaticScript.hasDupe(Event.current.keyCode))
            {
                p4Map.text = Event.current.keyCode.ToString();
                OptionsStaticScript.controls[3, 5] = Event.current.keyCode;
            }
            else
            {
                p4Map.text = OptionsStaticScript.controls[3, 5].ToString();
            }
        }


    }

}