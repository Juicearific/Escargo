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
    public Object slimeSprite;
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
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x + moveSpeed, this.gameObject.transform.position.y));
        }
        else if (Input.GetKey(downKey))
        {
            moveChar(Vector2.down);
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - moveSpeed));
        }
        else if (Input.GetKey(leftKey))
        {
            moveChar(Vector2.left);
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x - moveSpeed, this.gameObject.transform.position.y));
        }
        else if (Input.GetKey(upKey))
        {

            moveChar(Vector2.up);
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + moveSpeed));
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
                Object.Instantiate(slimeSprite, new Vector3(((float)gridX) + .5f, ((float)gridY) + .5f, 0), Quaternion.identity);
                //GetComponent<PlayerScript>().slider.value = GetComponent<PlayerScript>().slime;
            }
        }
    }
}
