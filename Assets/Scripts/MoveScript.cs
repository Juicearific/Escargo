using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour
{
    /* Constants */
    public const int HEIGHT = 25;
    public const int WIDTH = 50;

    /* Public Variables */
    public Object slimeSprite;
    public int[,] slimeGrid = new int[WIDTH, HEIGHT];
    public Text slimeBox;
    public string upKey = "w";
    public string downKey = "s";
    public string leftKey = "a";
    public string rightKey = "d";
    public string slimeKey = "e";

    /* Private Variables */
    private bool placeSlime = true;

    void Update()
    {
        /* Slime Trigger */
        if (Input.GetKeyUp(slimeKey))
        {
            placeSlime = !placeSlime;
        }
    }

    void FixedUpdate()
    {
        /* movement */
        if (Input.GetKey(rightKey))
        {
            moveChar(Vector2.right);
        }
        else if (Input.GetKey(downKey))
        {
            moveChar(Vector2.down);
        }
        else if (Input.GetKey(leftKey))
        {
            moveChar(Vector2.left);
        }
        else if (Input.GetKey(upKey))
        {

            moveChar(Vector2.up);
        }
        else {
            moveChar(Vector2.zero);
        }
    }

    void moveChar(Vector2 targetVelocity)
    {
        if (placeSlime)
            setSlime();
        slimeBox.text = "Slime: " + GetComponent<PlayerScript>().slime + "/100";
        float mSpeed = GetComponent<PlayerScript>().moveSpeed;
        GetComponent<Rigidbody2D>().velocity = targetVelocity * mSpeed;
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
    }

    void setSlime()
    {
        int gridX = (int)GetComponent<Transform>().position.x;
        int gridY = (int)GetComponent<Transform>().position.y;
        if (slimeGrid[gridX,gridY] == 0) // will need to be changed once we have multiple players
        {
            slimeGrid[gridX, gridY] = GetComponent<PlayerScript>().playerID;
            int slimeAmt = GetComponent<PlayerScript>().slime - PlayerScript.SLIME_COST;
            if (slimeAmt >= 0)
            {
                GetComponent<PlayerScript>().slime = slimeAmt;
                Object.Instantiate(slimeSprite, new Vector3(((float)gridX) + .5f, ((float)gridY) + .5f, 0), Quaternion.identity);
                //GetComponent<PlayerScript>().slider.value = GetComponent<PlayerScript>().slime;
            }
        }
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//Shelling - Display Animation for Shell Collision here.
		float pushback_force = 0.5f;
		if (collision.gameObject.tag == "Player") {
			Vector2 direction = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
			direction = -direction.normalized;
			float savedSpeed = gameObject.GetComponent<PlayerScript> ().moveSpeed;
			gameObject.GetComponent<PlayerScript> ().moveSpeed = 0;
			gameObject.GetComponent<Rigidbody2D>().AddForce(direction * pushback_force);
			StartCoroutine(pushBackStun(savedSpeed)); //Stun after push back
		}
	}

	IEnumerator pushBackStun(float storedSpeed)
	{
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<PlayerScript> ().moveSpeed = storedSpeed;
		//Remove Animation for Shell Collision here.
	}
}
	