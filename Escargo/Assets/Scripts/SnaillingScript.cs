using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Node
{
    public int p_x, p_y;
    public int x, y;
    public int g, h, f;

    public Node()
    {
        x = -1;
        y = -1;
    }

    public Node(int xPos, int yPos, int pX, int pY, int pG, int distX, int distY)
    {
        x = xPos;
        y = yPos;
        g = pG + 1;
        h = distance(distX, distY);
        f = g + h;
        p_x = pX;
        p_y = pY;
    }

	public int distance(int otherX, int otherY)
    {
        int dist = Mathf.Abs(otherX - x) + Mathf.Abs(otherY - y); // Manhatten distance
        return dist;
    }
};

public class SnaillingScript : MonoBehaviour {

    /* Constants */
    public const int NUM_SNAILLINGS = 5;
    public const int HEIGHT = 25;
    public const int WIDTH = 50;

    /* Public Variables */
    public int[,] slimeGrid = new int[WIDTH, HEIGHT];
    public bool isPathfinding = false;
    public bool isSnailfinding = false;
    public List<KeyValuePair<int, int>> closestNode = new List<KeyValuePair<int, int>>();
    public Object snaillingsSprite;
    public GameObject[] snaillings = new GameObject[NUM_SNAILLINGS];
    public int currentSnail = 0;
    public Stack<Vector3>[] snaillingsMove = new Stack<Vector3>[NUM_SNAILLINGS];
    public bool[] sMoveLocks = new bool[NUM_SNAILLINGS];

    /* Private Variables */
    private float spawnTimer = 0.0f;
    private float moveTimer = 0.0f;
    private float snailStartX;
    private float snailStartY;
    public int playerID = 1;

    void Start()
    {
        snailStartX = GetComponent<Transform>().position.x;
        snailStartY = GetComponent<Transform>().position.y;
        for (int i = 0; i < NUM_SNAILLINGS; i++)
        {
            snaillings[i] = (GameObject)Object.Instantiate(snaillingsSprite, new Vector3(snailStartX, snailStartY, -1), Quaternion.identity);
            snaillingsMove[i] = new Stack<Vector3>();
        }
    }

    void FixedUpdate () {
        /* snaillings spawn */
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 10f && currentSnail < NUM_SNAILLINGS)
        {
            spawnTimer = 0f;
            snaillings[currentSnail].transform.position = new Vector3(snaillings[currentSnail].transform.position.x,
                snaillings[currentSnail].transform.position.y, 0);
            snaillings[currentSnail].tag = "P" + playerID.ToString() + "Snailling";
            //findPath(currentSnail, (int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y);
            currentSnail++;
        }
        /* snailings move */
        moveTimer += Time.deltaTime;
        if (moveTimer >= 0.5f)
        {
            moveTimer = 0f;
            for (int i = 0; i < currentSnail; i++)
            {
				if (snaillings [i] != null) {
					snaillingsMove [i].Clear ();
					KeyValuePair<int, int> n = closestNode [0];
					int oX = (int)snaillings [i].transform.position.x;
					int oY = (int)snaillings [i].transform.position.y;
					Thread snailPathThread = new Thread (() => findPath (i, oX, oY, n.Key, n.Value));
					snailPathThread.Start ();

					if (snaillings [i] != null && snaillingsMove [i].Count > 0) {
						Vector3 origPos = snaillings [i].transform.position;
						Vector3 newPos = snaillingsMove [i].Pop ();
						float speed = 15f;
						float smooth = 1.0f - Mathf.Pow (0.5f, Time.deltaTime * speed);
						snaillings [i].transform.position = Vector3.Slerp (origPos, newPos, smooth);
					}
				}
            }
        }
    }

    public void pathfind()
    {
        isPathfinding = true;
        
        isPathfinding = false;
    }
    
    public void findPath(int sID, int origX, int origY, int distX, int distY)
    {
        isSnailfinding = true;
        //initialize the open list, initialize the closed list
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        Node dist = new Node();

        //put the starting node on the open list(its g = 0, parent = null)
        open.Add(new Node(origX, origY, -1, -1, -1, distX, distY));

        bool foundGoal = false;
        while (open.Count != 0 && !foundGoal) {
            //find the node with the least f on the open list, call it "q"
            Node q = open[0];
            int q_pos = 0;
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].f <= q.f)
                {
                    q = open[i];
                    q_pos = i;
                }
            }
            open.RemoveAt(q_pos);

            //generate q's (max four) successors and set their parents to q
            List<Node> succ =  new List<Node>();
            if (q.x + 1 < WIDTH && slimeGrid[q.x + 1,q.y] == playerID)
            { // check for node below
                succ.Add(new Node(q.x + 1, q.y, q.x, q.y, q.g, distX, distY));
            }
            if (q.x - 1 >= 0 && slimeGrid[q.x - 1,q.y] == playerID)
            { // check for node above
                succ.Add(new Node(q.x - 1, q.y, q.x, q.y, q.g, distX, distY));
            }
            if (q.y + 1 < HEIGHT && slimeGrid[q.x,q.y + 1] == playerID)
            { // check for node right
                succ.Add(new Node(q.x, q.y + 1, q.x, q.y, q.g, distX, distY));
            }
            if (q.y - 1 >= 0 && slimeGrid[q.x,q.y - 1] == playerID)
            { // check for node left
                succ.Add(new Node(q.x, q.y - 1, q.x, q.y, q.g, distX, distY));
            }

            foreach (Node n in succ)
            {
                if (n.x == distX && n.y == distY)
                {
                    dist.x = n.x;
                    dist.y = n.y;
                    dist.p_x = n.p_x;
                    dist.p_y = n.p_y;
                    foundGoal = true;
                }
                bool foundOpen = searchList(open, n);
                bool foundClosed = searchList(closed, n);

                if (!foundGoal && !foundOpen && !foundClosed)
                { //otherwise, add the node to the open list
                    Node temp = n;
                    open.Add(n);
                }
            }
            closed.Add(q);
        }
        
        if (dist.x != -1)
        {
            //sMoveLocks[sID] = true;
            while (dist.p_x != -1 || dist.p_y != -1) {
                snaillingsMove[sID].Push(new Vector3(dist.x + .5f, dist.y + .5f, 0));
			    dist.x = dist.p_x;
                dist.y = dist.p_y;
			    for (int i = 0; i < closed.Count; i++) {
				    if (closed[i].x == dist.p_x && closed[i].y == dist.p_y) {
                        dist.p_x = closed[i].p_x;
                        dist.p_y = closed[i].p_y;
				    }
			    }
		    }
            //sMoveLocks[sID] = false;
        }
        isSnailfinding = false;
    }

    bool searchList(List<Node> l, Node n)
    {
        foreach (Node no in l)
        {
            if (no.x == n.x && no.y == n.y)
            {
                return true;
            }
        }
        return false;
    }
}
