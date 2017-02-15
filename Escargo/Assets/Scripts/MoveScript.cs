using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{
    public string upKey = "w";
    public string downKey = "s";
    public string leftKey = "a";
    public string rightKey = "d";

    void FixedUpdate()
    {
        Vector2 targetVelocity;
        if (Input.GetKey(rightKey))
        {
            targetVelocity = Vector2.right;
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x + moveSpeed, this.gameObject.transform.position.y));
        }
        else if (Input.GetKey(downKey))
        {
            targetVelocity = Vector2.down;
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - moveSpeed));
        }
        else if (Input.GetKey(leftKey))
        {
            targetVelocity = Vector2.left;
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x - moveSpeed, this.gameObject.transform.position.y));
        }
        else if (Input.GetKey(upKey))
        {
            targetVelocity = Vector2.up;
            //GetComponent<Rigidbody2D>().MovePosition(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + moveSpeed));
        }
        else
        {
            targetVelocity = Vector2.zero;

        }
        float moveSpeed = gameObject.GetComponent<PlayerScript>().moveSpeed;
        GetComponent<Rigidbody2D>().velocity = targetVelocity * moveSpeed;
        GetComponent<Transform>().rotation = new Quaternion(0,0,0,0);
    }
}
