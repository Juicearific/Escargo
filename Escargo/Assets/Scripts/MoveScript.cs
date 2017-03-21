﻿using UnityEngine;
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

        if (slimeGrid[gridX, gridY] == 0)
        {
            GetComponent<SnaillingScript>().findPath(snaillings[0], 0, gridX, gridY);
            for (int i = 1; i < snails; i++)
            {
                GetComponent<SnaillingScript>().findPath(snaillings[i], i, (int)snaillings[i-1].transform.position.x, (int)snaillings[i - 1].transform.position.y);
            }
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
}
