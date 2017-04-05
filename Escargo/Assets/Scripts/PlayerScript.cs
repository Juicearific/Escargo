using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    /* Constants */
    public const int SLIME_MAX = 100; //Maximum amount the slime bar can hold
    public const int SLIME_COST = 1; //Amount of slime reduced when using slime button.

    /* Private Variables */
    private bool minimapActive = false;
    private int baseCullingMask;
    private Vector3 minimapPosition = new Vector3(24.5f,12.5f,-6.0f);
    private Camera c;
	private float storedSpeed;
    /* Public Variables */
    public int slime = SLIME_MAX; //Slime remaining in slime bar. Initialized to be SLIME_MAX.
    public float moveSpeed = 2f; //Movement speed of snail.
    public Slider slider;
    public int numSnailingsSaved = 0;
    public string minimapKey = "q";
	public GameObject snaillingPanel;
	public int playerID = 1;
	public string snailType = "pierre";
	public Color playerColor;

    void Start()
    {
        slider.maxValue = SLIME_MAX;
		slider.fillRect.GetComponent<UnityEngine.UI.Image> ().color = playerColor;
        c = gameObject.GetComponentInChildren<Camera>();
        baseCullingMask = c.cullingMask;
		storedSpeed = moveSpeed;
		Camera.main.GetComponent<GlobalScript>().setSnaillingDisplay (playerID, snailType, snaillingPanel, playerColor);
    }

    void Update()
    {
        if (Input.GetKeyUp(minimapKey))
        {
            minimapActive = !minimapActive;
            if (!minimapActive)
            {
				moveSpeed = storedSpeed;
                c.transform.localPosition = new Vector3(0, 0, -1);
                c.cullingMask = baseCullingMask;
            }
            else
            {
				storedSpeed = moveSpeed;
				moveSpeed = 0f;
                c.cullingMask = (1 << LayerMask.NameToLayer("Background")) | (1 << LayerMask.NameToLayer("Minimap"));
            }
        }

        if (minimapActive)
            c.transform.position = minimapPosition;
    }

    public void changeSlimeBar(int amt)
    {
        slime = amt;
        slider.value = slime;
    }
}