using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerScript : MonoBehaviour {
    /* Constants */
    public int SLIME_MAX = 100; //Maximum amount the slime bar can hold
    public const int SLIME_COST = 1; //Amount of slime reduced when using slime button.
	public float MOVE_SPEED = 3f;//Base movement speed

    /* Private Variables */
    private bool minimapActive = false;
    private int baseCullingMask;
    private Camera c;
	private float storedSpeed;
	private int slime; //Slime remaining in slime bar. Initialized to be SLIME_MAX.
	private float moveSpeed; //Movement speed of snail.
	private int numSnailingsSaved = 0;
	private float regenTimer = 3.0f;
	private float timeTracker = 0.0f;

    /* Public Variables */
	public Slider slider;
	public GameObject snaillingPanel;
    public KeyCode minimapCode;
	public int playerID;
	public string snailType = "pierre";
	public Color playerColor;
	/*
	 * Affect of Power Ups on this player.
	 * Value of 1 implies no effect. Less than one implies reduced effect. More than 1 implies enhanced effect.
	 */
	public Dictionary<string, float> powerUpEffects = new Dictionary<string, float>() {
		{"Flower", 1.0f},
		{"Water",  1.0f},
		{"Sugar",  1.0f},
		{"Salt",   1.0f},
		{"Copper", 1.0f},
		{"Carrot", 1.0f}
	};

    void Start()
    {
        minimapCode = OptionsStaticScript.controls[playerID - 1, 5];
		slime = SLIME_MAX;
		moveSpeed = MOVE_SPEED;
		setupTraits ();
        slider.maxValue = SLIME_MAX;
		slider.fillRect.GetComponent<UnityEngine.UI.Image> ().color = playerColor;
        c = gameObject.GetComponentInChildren<Camera>();
        baseCullingMask = c.cullingMask;
		storedSpeed = moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyUp(minimapCode))
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

		if (snailType == "kenta") {
			//regen effect for kenta.
			timeTracker += Time.deltaTime;
			if (timeTracker >= regenTimer) {
				timeTracker = 0.0f;
				int new_slime = slime + 1;
				if (new_slime > SLIME_MAX)
					changeSlimeBar (SLIME_MAX);
				else
					changeSlimeBar (new_slime);
			}
		}

		if (minimapActive)
			c.transform.position = Camera.main.GetComponent<SplitscreenScript> ().minimapCamera.transform.position;
    }

    public void changeSlimeBar(int amt)
    {
        slime = amt;
        slider.value = slime;
    }

	//Kenta => Doesn't absorb sugars or flowers
	//Bertha => Increased speed, doesn't absorb flowers
	//Pierre => Increased slime, Absorbs more sugar
	//Lil Jim => Reduced speed, absorbs less sugar and more flowers
	private void setupTraits() {
		if (snailType == "pierre") {
			powerUpEffects ["Sugar"] = 5f / 3f; 
		} else if (snailType == "kenta") {
			powerUpEffects ["Sugar"] = 0f;
			powerUpEffects ["Flower"] = 0f;
		} else if (snailType == "bigbertha") {
			powerUpEffects ["Flower"] = 0f;
		} else if (snailType == "liljim") {
			powerUpEffects ["Sugar"] = 2f / 3f;
			powerUpEffects ["Flower"] = 4f / 3f;
		}
	}

	public int getSnaillingsSaved() {
		return numSnailingsSaved;
	}
	public void incrementSnaillingsSaved() {
		numSnailingsSaved++;
	}
	public int getSlime() {
		return slime;
	}
	public void setSlime(int value) {
		slime = value;
	}
	public float getMoveSpeed() {
		return moveSpeed;
	}
	public void setMoveSpeed(float val) {
		moveSpeed = val;
	}

}