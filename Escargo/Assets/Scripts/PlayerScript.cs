using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public const int SLIME_MAX = 100; //Maximum amount the slime bar can hold

    public int slime = SLIME_MAX; //Slime remaining in slime bar. Initialized to be SLIME_MAX.
    public float moveSpeed = 2f; //Movement speed of snail.
    public int slime_cost = 10; //Amount of slime reduced when using slime button.
}
