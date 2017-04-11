using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsStaticScript : MonoBehaviour
{
    public static KeyCode[,] controls = new KeyCode[4, 6] { { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.E, KeyCode.Q } ,
                                                            {KeyCode.F, KeyCode.C, KeyCode.B, KeyCode.V, KeyCode.G, KeyCode.R},
                                                            {KeyCode.U, KeyCode.H, KeyCode.K, KeyCode.J, KeyCode.I, KeyCode.Y},
                                                            {KeyCode.L, KeyCode.Comma, KeyCode.Slash, KeyCode.Period, KeyCode.Semicolon, KeyCode.O } };
    // [x,0] = Up
    // [x,1] = Left
    // [x,2] = Right
    // [x,3] = Down
    // [x,4] = Slime Toggle
    // [x,5] = Map

    // Current players: Can be 'pierre', 'kenta', 'jim', 'bertha', or 'n/a'
    public static string p1Name = "pierre";
    public static string p2Name = "kenta";
    public static string p3Name = "n/a";
    public static string p4Name = "n/a";
}