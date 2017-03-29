using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsStaticScript : MonoBehaviour
{
    public static KeyCode[,] controls = new KeyCode[4, 6];
        // [x,0] = Up
        // [x,1] = Left
        // [x,2] = Right
        // [x,3] = Down
        // [x,4] = Slime Toggle
        // [x,5] = Map
    public static bool started = false;

    void Start()
    {
        if (!started)
        {
            controls[0, 0] = KeyCode.W;
            controls[0, 1] = KeyCode.A;
            controls[0, 2] = KeyCode.S;
            controls[0, 3] = KeyCode.D;
            controls[0, 4] = KeyCode.E;
            controls[0, 5] = KeyCode.Q;

            controls[1, 0] = KeyCode.F;
            controls[1, 1] = KeyCode.C;
            controls[1, 2] = KeyCode.B;
            controls[1, 3] = KeyCode.V;
            controls[1, 4] = KeyCode.G;
            controls[1, 5] = KeyCode.R;

            controls[2, 0] = KeyCode.U;
            controls[2, 1] = KeyCode.H;
            controls[2, 2] = KeyCode.K;
            controls[2, 3] = KeyCode.J;
            controls[2, 4] = KeyCode.I;
            controls[2, 5] = KeyCode.Y;

            controls[3, 0] = KeyCode.L;
            controls[3, 1] = KeyCode.Comma;
            controls[3, 2] = KeyCode.Slash;
            controls[3, 3] = KeyCode.Period;
            controls[3, 4] = KeyCode.Semicolon;
            controls[3, 5] = KeyCode.O;

            started = true;
        }
        
    }
}
