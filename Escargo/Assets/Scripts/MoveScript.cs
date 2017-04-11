using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;

public class MoveScript : MonoBehaviour
{
    /* Constants */
	private const int NUM_SLIME_SPRITES = 6;

    /* Public Variables */
    public GameObject[] otherPlayers;
    public GameObject[] slimeSprites = new GameObject[NUM_SLIME_SPRITES];
    // 0 - Dottrail
    // 1 - Singletrail
    // 2 - Sidetrail
    // 3 - Cornertrail
    // 4 - Tritrail
    // 5 - Quadtrail
    public GameObject[,] slimeObjGrid = new GameObject[SnaillingScript.WIDTH, SnaillingScript.HEIGHT];
    /*public Text slimeBox;*/
    public string upKey = "w";
    public string downKey = "s";
    public string leftKey = "a";
    public string rightKey = "d";
    public string slimeKey = "e";

    /* Private Variables */
    private bool placeSlime = true;
	private Animator anim;

	void Start() {
		//Set color of slime trails
		for (int i = 0; i < NUM_SLIME_SPRITES; i++) {
			slimeSprites [i].GetComponent<SpriteRenderer> ().color = GetComponent<PlayerScript> ().playerColor;
		}
		anim = GetComponent<Animator>();
	}

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
			anim.SetInteger ("Direction", 1);
            moveChar(Vector2.right);
        }
        else if (Input.GetKey(downKey))
        {
			anim.SetInteger ("Direction", 4);
            moveChar(Vector2.down);
        }
        else if (Input.GetKey(leftKey))
        {
			anim.SetInteger ("Direction", 2);
            moveChar(Vector2.left);
        }
        else if (Input.GetKey(upKey))
        {
			anim.SetInteger ("Direction", 3);
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
		float mSpeed = GetComponent<PlayerScript>().getMoveSpeed();
        GetComponent<Rigidbody2D>().velocity = targetVelocity * mSpeed;
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
    }

    void setSlime()
    {
        int gridX = (int)GetComponent<Transform>().position.x;
        int gridY = (int)GetComponent<Transform>().position.y;

        int slimeAmt = GetComponent<PlayerScript>().getSlime() - PlayerScript.SLIME_COST;
        if (SnaillingScript.slimeGrid[gridX, gridY] == 0 && slimeAmt >= 0)
        {
            SnaillingScript.slimeGrid[gridX, gridY] = GetComponent<PlayerScript>().playerID;
            KeyValuePair<int, int> n = new KeyValuePair<int, int>(gridX, gridY);

            foreach (GameObject oP in otherPlayers)
            {
                if (oP.GetComponent<SnaillingScript>().closestNode.Contains(n))
                {
                    oP.GetComponent<SnaillingScript>().closestNode.Remove(n);
                }
            }

            if (GetComponent<SnaillingScript>().closestNode.Count > 0)
            {
                KeyValuePair<int, int> closeN = GetComponent<SnaillingScript>().closestNode[0];
                if (BasicMap.hVals[gridY][gridX] < BasicMap.hVals[closeN.Value][closeN.Key])
                {
                    GetComponent<SnaillingScript>().closestNode.Insert(0, n);
                }
            } else
            {
                GetComponent<SnaillingScript>().closestNode.Insert(0, n);
            }
            
			GetComponent<PlayerScript>().changeSlimeBar(slimeAmt);
            slimeObjGrid[gridX, gridY] = GameObject.Instantiate(slimeSprites[0], new Vector3(((float)gridX) + .5f, ((float)gridY) + .5f, 0), Quaternion.identity);
            updateSlime(gridX, gridY, 0);
        }
	}

    void updateSlime(int x, int y, int depth)
    {
        if (depth < 2) // Should only need to adjust up to two slimes out - but, we can adjust this if we find the need to affect more later. 
        {
            bool up = false;
            bool left = false;
            bool right = false;
            bool down = false;
            int id = GetComponent<PlayerScript>().playerID;
            int[,] grid = SnaillingScript.slimeGrid;
            // Set up the adjacencies
            if (x - 1 >= 0 && grid[x - 1, y] == id)
            {
                left = true;
                updateSlime(x - 1, y, depth + 1);
            }
            if (x + 1 < SnaillingScript.WIDTH && grid[x + 1, y] == id)
            {
                right = true;
                updateSlime(x + 1, y, depth + 1);
            }
            if (y - 1 >= 0 && grid[x, y - 1] == id)
            {
                down = true;
                updateSlime(x, y - 1, depth + 1);
            }
            if (y + 1 < SnaillingScript.HEIGHT && grid[x, y + 1] == id)
            {
                up = true;
                updateSlime(x, y + 1, depth + 1);
            }

            // Adjust the sprites
            // For Quad:
            if (left && right && up && down)
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x,y] = GameObject.Instantiate(slimeSprites[5], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
            }
            // For Tri:
            else if (left && down && right) // Default
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[4], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
            }
            else if (up && right && down) // 90 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[4], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 90);
            }
            else if (left && up && right) // 180 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[4], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 180);
            }
            else if (down && left && up) // 270 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[4], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 270);
            }
            // For Corner:
            else if (left && down) // Default
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[3], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
            }
            else if (down && right) // 90 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[3], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 90);
            }
            else if (up && right) // 180 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[3], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 180);
            }
            else if (left && up) // 270 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[3], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 270);
            }
            // For Sidetrail
            else if (left && right) // Default
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[2], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
            }
            else if (up && down) // 90 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[2], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 90);
            }
            // For Singletrail
            else if (left) // Default
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[1], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
            }
            else if (down) // 90 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[1], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 90);
            }
            else if (right) // 180 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[1], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 180);
            }
            else if (up) // 270 deg rotation
            {
                Destroy(slimeObjGrid[x, y]);
                slimeObjGrid[x, y] = GameObject.Instantiate(slimeSprites[1], new Vector3(((float)x) + .5f, ((float)y) + .5f, 0), Quaternion.identity);
                slimeObjGrid[x, y].transform.Rotate(0, 0, 270);
            }

        }
    }

	void OnCollisionEnter2D(Collision2D collision) {
		//Shelling - Display Animation for Shell Collision here.
		float pushback_force = 0.5f;
		if (collision.gameObject.tag == "Player") {
			anim.Play ("shelling");
			Vector2 currentPos = gameObject.transform.localPosition;
			Vector2 direction = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
			direction = -direction.normalized;
			float savedSpeed = gameObject.GetComponent<PlayerScript> ().getMoveSpeed();
			gameObject.GetComponent<PlayerScript> ().setMoveSpeed(0);
			gameObject.GetComponent<Rigidbody2D>().AddForce(direction * pushback_force);
			StartCoroutine (checkForOutOfBounds ());
			StartCoroutine(pushBackStun(savedSpeed)); //Stun after push back
		}
	}
	IEnumerator checkForOutOfBounds() {
		yield return new WaitForSeconds (0.05f);
		if (gameObject.transform.localPosition.x < 0) {
			gameObject.transform.localPosition = new Vector2(1.0f, gameObject.transform.localPosition.y);
		}
		if (gameObject.transform.localPosition.y > 24.5) {
			gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, 24.5f);
		}
	}

	IEnumerator pushBackStun(float storedSpeed)
	{
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<PlayerScript> ().setMoveSpeed(storedSpeed);
		//Remove Animation for Shell Collision here.
	}
}
	