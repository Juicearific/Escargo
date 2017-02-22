using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    /* Constants */
    public const int SLIME_MAX = 100; //Maximum amount the slime bar can hold
    public const int SLIME_COST = 1; //Amount of slime reduced when using slime button.

    /* Public Variables */
    public int slime = SLIME_MAX; //Slime remaining in slime bar. Initialized to be SLIME_MAX.
    public float moveSpeed = 2f; //Movement speed of snail.
    public Slider slider;
    public int playerID = 1;
    public int numSnailingsSaved = 0;
    public Text snaillingLabel;

    void Start()
    {
       // slider.maxValue = SLIME_MAX;
    }
}
