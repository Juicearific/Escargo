using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnaillingScript : MonoBehaviour {

    /* Constants */
    public const int NUM_SNAILLINGS = 5;

    /* Public Variables */
    public Object snaillingsSprite;
    public GameObject[] snaillings = new GameObject[NUM_SNAILLINGS];


    /* Private Variables */
    private int currentSnail = 0;
    private float spawnTimer = 0.0f;
    private float moveTimer = 0.0f;
    private Stack<int> snaillingsMove = new Stack<int>();
    private int[,] snaillingsGrid = new int[MoveScript.WIDTH, MoveScript.HEIGHT];
    private float snailStartX;
    private float snailStartY;


    void Start()
    {
        snailStartX = GetComponent<Transform>().position.x;
        snailStartY = GetComponent<Transform>().position.y;
        for (int i = 0; i < NUM_SNAILLINGS; i++)
        {
            snaillings[i] = (GameObject)Object.Instantiate(snaillingsSprite, new Vector3(snailStartX, snailStartY, -1), Quaternion.identity);
            snaillings[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void FixedUpdate () {
        /* snaillings spawn */
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 5f && currentSnail < NUM_SNAILLINGS)
        {
            spawnTimer = 0f;
            snaillings[currentSnail].transform.position = new Vector3(snaillings[currentSnail].transform.position.x,
                snaillings[currentSnail].transform.position.y, -1);
            snaillings[currentSnail].GetComponent<BoxCollider2D>().enabled = true;
            snaillings[currentSnail].tag = "P" + GetComponent<PlayerScript>().playerID.ToString() + "Snailling";
            currentSnail++;
        }
        /* snailings move */
        moveTimer += Time.deltaTime;
        if (moveTimer >= 2f)
        {
            moveTimer = 0f;
            for (int i = 0; i < currentSnail; i++)
            {
                if (snaillings[i] != null)
                {
                    if (findPath(snaillings[i], i+1))
                    {
                        takePath(snaillings[i]);
                    }
                }
            }
        }
    }

    bool findPath(GameObject snail, int sID)
    {
        int gridX = (int)snail.GetComponent<Transform>().position.x;
        int gridY = (int)snail.GetComponent<Transform>().position.y;
        int playerID = GetComponent<PlayerScript>().playerID;
        int[,] slimeGrid = GetComponent<MoveScript>().slimeGrid;
        if (gridX + 1 < MoveScript.WIDTH && slimeGrid[gridX + 1, gridY] == playerID && snaillingsGrid[gridX + 1, gridY] != sID) // forward
        {
            snaillingsMove.Push(1);
            snaillingsGrid[gridX + 1, gridY] = sID; // visited
            return true;
        }
        else if (gridY + 1 < MoveScript.HEIGHT && slimeGrid[gridX, gridY + 1] == playerID && snaillingsGrid[gridX, gridY + 1] != sID)
        { // up
            snaillingsMove.Push(2);
            snaillingsGrid[gridX, gridY + 1] = sID; // visited
            return true;
        }
        else if (gridY - 1 >= 0 && slimeGrid[gridX, gridY - 1] == playerID && snaillingsGrid[gridX, gridY - 1] != sID)
        { // down
            snaillingsMove.Push(-2);
            snaillingsGrid[gridX, gridY - 1] = sID; // visited
            return true;
        }
        else if (gridX - 1 >= 0 && slimeGrid[gridX - 1, gridY] == playerID && snaillingsGrid[gridX - 1, gridY] != sID)
        { // backwards
            snaillingsMove.Push(-1);
            snaillingsGrid[gridX - 1, gridY] = sID; // visited
            return true;
        }
        else
        {
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
                    snail.transform.position = new Vector3(snail.transform.position.x - 1f, snail.transform.position.y, 0);
                    break;
                case 1:
                    snail.transform.position = new Vector3(snail.transform.position.x + 1f, snail.transform.position.y, 0);
                    break;
                case -2:
                    snail.transform.position = new Vector3(snail.transform.position.x, snail.transform.position.y - 1f, 0);
                    break;
                case 2:
                    snail.transform.position = new Vector3(snail.transform.position.x, snail.transform.position.y + 1f, 0);
                    break;
            }
        }
    }
}
