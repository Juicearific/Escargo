using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;

public class MoveScript : MonoBehaviour
{
    /* Constants */

    /* Public Variables */
    public GameObject[] otherPlayers;
    public GameObject[] slimeSprites = new GameObject[6];
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
        /*slimeBox.text = "Slime: " + GetComponent<PlayerScript>().slime + "/100";*/
        float mSpeed = GetComponent<PlayerScript>().moveSpeed;
        GetComponent<Rigidbody2D>().velocity = targetVelocity * mSpeed;
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
    }

    void setSlime()
    {
        int gridX = (int)GetComponent<Transform>().position.x;
        int gridY = (int)GetComponent<Transform>().position.y;
        int snails = GetComponent<SnaillingScript>().currentSnail;
        GameObject[] snaillings = GetComponent<SnaillingScript>().snaillings;

        if (GetComponent<SnaillingScript>().slimeGrid[gridX, gridY] == 0)
        {
            GetComponent<SnaillingScript>().slimeGrid[gridX, gridY] = GetComponent<SnaillingScript>().playerID;
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
            /*
            if (!GetComponent<SnaillingScript>().isPathfinding)
            {
                KeyValuePair<int, int> n = GetComponent<SnaillingScript>().closestNode.Peek();
                for (int i = 0; i < snails; i++)
                {
                    Thread snailPathThread = new Thread(() => GetComponent<SnaillingScript>().findPath(i,
                        (int)snaillings[i].transform.position.x, (int)snaillings[i].transform.position.y, n.Key, n.Value));
                    snailPathThread.Start();
                }
            }
            */

            int slimeAmt = GetComponent<PlayerScript>().slime - PlayerScript.SLIME_COST;
            if (slimeAmt >= 0)
            {
                GetComponent<PlayerScript>().slime = slimeAmt;
                slimeObjGrid[gridX, gridY] = GameObject.Instantiate(slimeSprites[0], new Vector3(((float)gridX) + .5f, ((float)gridY) + .5f, 0), Quaternion.identity);
                updateSlime(gridX, gridY, 0);
                GetComponent<PlayerScript>().slider.value = GetComponent<PlayerScript>().slime;
            }
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
            int id = GetComponent<SnaillingScript>().playerID;
            int[,] grid = GetComponent<SnaillingScript>().slimeGrid;
            // Set up the adjacencies
            if (x - 1 >= 0 && grid[x - 1, y] == id)
            {
                left = true;
                updateSlime(x - 1, y, depth + 1);
            }
            if (x + 1 <= SnaillingScript.WIDTH && grid[x + 1, y] == id)
            {
                right = true;
                updateSlime(x + 1, y, depth + 1);
            }
            if (y - 1 >= 0 && grid[x, y - 1] == id)
            {
                down = true;
                updateSlime(x, y - 1, depth + 1);
            }
            if (y + 1 <= SnaillingScript.HEIGHT && grid[x, y + 1] == id)
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
	