using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveScript : MonoBehaviour
{

    /* Constants */
    public const int HEIGHT = 25;
    public const int WIDTH = 50;

    /* Public Variables */
    public Object slimeSprite;
    public int[,] slimeGrid = new int[WIDTH, HEIGHT];

    /* Private Variables */
    private bool placeSlime = false;
    private string upKey = "w";
    private string downKey = "s";
    private string leftKey = "a";
    private string rightKey = "d";
    private string slimeKey = "e";

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
        GetComponent<Rigidbody2D>().velocity = targetVelocity * gameObject.GetComponent<PlayerScript>().moveSpeed;
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
                GetComponent<PlayerScript>().slider.value = GetComponent<PlayerScript>().slime;
            }
        }
    }
}
