using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MoveScript : MonoBehaviour
{
    public string upKey = "w";
    public string downKey = "s";
    public string leftKey = "a";
    public string rightKey = "d";
    public int playerID = 1;
    public Object snaillingsSprite;
    public const int numSnaillings = 10;
    GameObject[] snaillings = new GameObject[numSnaillings];
    int currentSnail = 0;
    float spawnTimer = 10.0f;
    float moveTimer = 0.0f;
    public Object slimeSprite;
    public const int height = 25;
    public const int width = 50;
    public int[,] slimeGrid = new int[width, height];
    Stack<int> snaillingsMove = new Stack<int>();
    public float snailStartX;
    public float snailStartY;
    public float moveSpeed = gameObject.GetComponent<PlayerScript>().moveSpeed;

    void Start()
    {
        snailStartX = GetComponent<Transform>().position.x;
        snailStartY = GetComponent<Transform>().position.y;
        for (int i = 0; i < numSnaillings; i++)
        {
            snaillings[i] = (GameObject)Object.Instantiate(snaillingsSprite, new Vector3(snailStartX, snailStartY, -1), Quaternion.identity);
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
        else
        {
            moveChar(Vector2.zero);
        }

        /* snaillings spawn */
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 10.0f && currentSnail < numSnaillings)
        {
            spawnTimer = 0f;
            snaillings[currentSnail].transform.position = new Vector3(snaillings[currentSnail].transform.position.x,
                snaillings[currentSnail].transform.position.y, -1);
            currentSnail++;
        }
        /* snailings move */
        moveTimer += Time.deltaTime;
        if (moveTimer >= .5f)
        {
            moveTimer = 0f;
            //Debug.Log("moving");
            for (int i = 0; i < currentSnail; i++)
            {
                if (findPath(snaillings[i]))
                {
                    Debug.Log("taking path");
                    takePath(snaillings[i]);
                }
            }
        }
    }

    bool findPath(GameObject snail)
    {
        int gridX = (int)snail.GetComponent<Transform>().position.x;
        int gridY = (int)snail.GetComponent<Transform>().position.y;
        if (gridX + 1 < width && slimeGrid[gridX + 1, gridY] == playerID) // forward
        {
            snaillingsMove.Push(1);
            return true;
        }
        else if (gridY + 1 < height && slimeGrid[gridX, gridY + 1] == playerID) { // up
            snaillingsMove.Push(2);
            return true;
        }
        else if (gridY - 1 >= 0 && slimeGrid[gridX, gridY - 1] == playerID) { // down
            snaillingsMove.Push(-2);
            return true;
        }
        else if (gridX -1 >= 0 && slimeGrid[gridX - 1, gridY] == playerID) { // backwards
            snaillingsMove.Push(-1);
            return true;
        } else {
            return false;
        }
    }

    void takePath(GameObject snail)
    {
        while (snaillingsMove.Count > 0)
        {
            int m = snaillingsMove.Pop();
            switch (m)
            {
                case -1:
                    snail.transform.position = new Vector3(snail.transform.position.x- .05f, snail.transform.position.y, 0);
                    break;
                case 1:
                    snail.transform.position = new Vector3(snail.transform.position.x + .05f, snail.transform.position.y, 0);
                    break;
                case -2:
                    snail.transform.position = new Vector3(snail.transform.position.x, snail.transform.position.y- .05f, 0);
                    break;
                case 2:
                    snail.transform.position = new Vector3(snail.transform.position.x, snail.transform.position.y + .05f, 0);
                    break;
            }
        }
    }

    void moveChar(Vector2 targetVelocity)
    {
        setSlime();
        GetComponent<Rigidbody2D>().velocity = targetVelocity * moveSpeed;
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
    }

    void setSlime()
    {
        int gridX = (int)GetComponent<Transform>().position.x;
        int gridY = (int)GetComponent<Transform>().position.y;
        if (slimeGrid[gridX,gridY] == 0) // will need to be changed once we have multiple players
        {
            slimeGrid[gridX, gridY] = playerID;
            Object.Instantiate(slimeSprite, new Vector3(((float)gridX)+.5f,((float)gridY)+.5f,0), Quaternion.identity);
        }
    }
}
