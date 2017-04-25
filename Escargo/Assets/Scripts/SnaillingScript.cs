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
        int dist = Mathf.Abs(otherX - x) + Mathf.Abs(otherY - y);
        return dist;
    }
};

public class SnaillingScript : MonoBehaviour {

    /* Constants */
    public const int NUM_SNAILLINGS = 5;
    public const int HEIGHT = 25;
    public const int WIDTH = 50;

    /* Public Variables */
    public static int[,] slimeGrid;
    public List<KeyValuePair<int, int>> closestNode;
    KeyValuePair<int, int>[] lastMove;
    public Object snaillingsSprite;
    public GameObject[] snaillings;
    public int currentSnail = 0;
    public Stack<Vector3>[] snaillingsMove;
	public bool Ready = false;
	public int playerID;

    /* Private Variables */
    private float spawnTimer = 5.0f;
    private float moveTimer = 0.0f;
    private float snailStartX;
    private float snailStartY;
    private int deadend = 0;
    Thread snailPathThread;

    void Start()
    {
        slimeGrid = new int[WIDTH, HEIGHT];
        closestNode = new List<KeyValuePair<int, int>>();
        lastMove = new KeyValuePair<int, int>[NUM_SNAILLINGS];
        snaillings = new GameObject[NUM_SNAILLINGS];
        snaillingsMove = new Stack<Vector3>[NUM_SNAILLINGS];
        snailStartX = GetComponent<Transform>().position.x;
        snailStartY = GetComponent<Transform>().position.y;
        for (int i = 0; i < NUM_SNAILLINGS; i++)
        {
            snaillings[i] = (GameObject)Object.Instantiate(snaillingsSprite, new Vector3(snailStartX, snailStartY, -500), Quaternion.identity);
            lock (snaillingsMove)
            {
                snaillingsMove[i] = new Stack<Vector3>();
            }
            lastMove[i] = new KeyValuePair<int, int>(-10,-10);
        }
		Ready = true;
    }

    void FixedUpdate () {
		if (GlobalScript.Ready) {
			/* snaillings spawn */
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= 10f && currentSnail < NUM_SNAILLINGS) {
				spawnTimer = 0f;
				snaillings [currentSnail].transform.position = new Vector3 (snaillings [currentSnail].transform.position.x,
					snaillings [currentSnail].transform.position.y, 0);
                snaillings [currentSnail].tag = "P" + GetComponent<PlayerScript> ().playerID.ToString () + "Snailling";
				currentSnail++;
			}
			/* snailings move */
			moveTimer += Time.deltaTime;
			if (moveTimer >= 1.5f) {
				moveTimer = 0f;
				for (int i = 0; i < currentSnail; i++) {
					if (snaillings [i] != null) {
                        lock (snaillingsMove)
                        {
                            snaillingsMove[i].Clear();
                        }
                        int oX = (int)snaillings [i].transform.position.x;
						int oY = (int)snaillings [i].transform.position.y;
						if (slimeGrid[oX, oY] == playerID) {
                            bool simple = findSimplePath(i, oX, oY);
							if (!simple && (deadend <= 1 || deadend > 5) && (snailPathThread == null || !snailPathThread.IsAlive)) {
                                if (deadend > 5) {
                                    deadend = 0;
                                }
                                // no simple path found, run AI
                                KeyValuePair<int, int> n = closestNode[0];
                                if (!(oX == n.Key && oY == n.Value)) {
                                    snailPathThread = new Thread(() => aStar(i, oX, oY, n.Key, n.Value));
                                    snailPathThread.Start();
									snailPathThread.Join ();
                                }
                            }

                            int count;
                            lock (snaillingsMove) {
                                count = snaillingsMove[i].Count;
                            }

                            if (count > 0) {
                                lastMove[i] = new KeyValuePair<int, int>(oX, oY);
                                Vector3 newPos;
                                lock (snaillingsMove) {
                                    newPos = snaillingsMove[i].Pop();
                                }
                                snaillings[i].transform.position = newPos; // actually make move
                                
                                /*
                                Vector3 origPos = snaillings [i].transform.position;
                                float speed = 15f;
                                float smooth = 1.0f - Mathf.Pow (0.5f, Time.deltaTime * speed);
                                snaillings [i].transform.position = Vector3.Slerp (origPos, newPos, smooth);
                                */
                            }
                        }
					}
				}
			}
		}
    }

    bool findSimplePath(int sID, int gridX, int gridY)
    {
        int dirs = 0;
        KeyValuePair<int, int> nextMove = new KeyValuePair<int, int>();
		if (gridX + 1 < WIDTH && slimeGrid[gridX + 1, gridY] == playerID)
        {
            if (!(lastMove[sID].Key == gridX + 1 && lastMove[sID].Value == gridY))
            {
                nextMove = new KeyValuePair<int, int>(gridX + 1, gridY);
            }
            dirs++;
        }
		if (gridY + 1 < HEIGHT && slimeGrid[gridX, gridY + 1] == playerID)
        {
            if (!(lastMove[sID].Key == gridX && lastMove[sID].Value == gridY + 1))
            {
                nextMove = new KeyValuePair<int, int>(gridX, gridY + 1);
            }
            dirs++;
        }
        if (gridY - 1 >= 0 && slimeGrid[gridX, gridY - 1] == playerID)
        {
            if (!(lastMove[sID].Key == gridX && lastMove[sID].Value == gridY - 1))
            {
                nextMove = new KeyValuePair<int, int>(gridX, gridY - 1);
            }
            dirs++;
        }
		if (gridX - 1 >= 0 && slimeGrid[gridX - 1, gridY] == playerID)
        {
            if (!(lastMove[sID].Key == gridX - 1 && lastMove[sID].Value == gridY))
            {
                nextMove = new KeyValuePair<int, int>(gridX - 1, gridY);
            }
            dirs++;

        }
        if (dirs == 2)
        {
            // only one path (aka only forward and backwards)
            lock (snaillingsMove)
            {
                snaillingsMove[sID].Push(new Vector3(nextMove.Key + .5f, nextMove.Value + .5f, 0));
            }
            deadend = 0;
            return true;
        } else if (dirs == 1)
        {
            deadend++;
        }
        return false;
    }

    public void aStar(int sID, int origX, int origY, int distX, int distY)
    {
        //initialize the open list, initialize the closed list
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        KeyValuePair<int, int> lowHSpot = new KeyValuePair<int, int>();
        KeyValuePair<int, int> lowHParent = new KeyValuePair<int, int>();
        Node dist = new Node();
        int lowestHVal = BasicMap.hVals[origY][origX];
        lowHSpot = new KeyValuePair<int, int>(origX, origY);

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
            List<Node> succ = new List<Node>();
            if (q.x + 1 < WIDTH && slimeGrid[q.x + 1, q.y] == playerID)
            { // check for node below
                succ.Add(new Node(q.x + 1, q.y, q.x, q.y, q.g, distX, distY));
            }
            if (q.x - 1 >= 0 && slimeGrid[q.x - 1, q.y] == playerID)
            { // check for node above
                succ.Add(new Node(q.x - 1, q.y, q.x, q.y, q.g, distX, distY));
            }
            if (q.y + 1 < HEIGHT && slimeGrid[q.x, q.y + 1] == playerID)
            { // check for node right
                succ.Add(new Node(q.x, q.y + 1, q.x, q.y, q.g, distX, distY));
            }
            if (q.y - 1 >= 0 && slimeGrid[q.x, q.y - 1] == playerID)
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
                    open.Add(n);
                    if (BasicMap.hVals[n.y][n.x] < lowestHVal)
                    {
                        // new lowestHVal
                        lowestHVal = BasicMap.hVals[n.y][n.x];
                        lowHSpot = new KeyValuePair<int, int>(n.x, n.y);
                        lowHParent = new KeyValuePair<int, int>(n.p_x, n.p_y);
                    }
                }
            }
            closed.Add(q);
        }
        if (!foundGoal) // dist not found, default to lowest h
        {
            dist.x = lowHSpot.Key;
            dist.y = lowHSpot.Value;
            dist.p_x = lowHParent.Key;
            dist.p_y = lowHParent.Value;
        }
        if (!(origX == dist.x && origY == dist.y)) { // do not astar to current pos
            while (dist.p_x >= 0 && dist.p_y >= 0)
            {
                lock (snaillingsMove)
				{
                    snaillingsMove[sID].Push(new Vector3(dist.x + .5f, dist.y + .5f, 0));
                }
                dist.x = dist.p_x;
                dist.y = dist.p_y;
                for (int i = 0; i < closed.Count; i++)
                {
                    if (closed[i].x == dist.p_x && closed[i].y == dist.p_y)
                    {
                        dist.p_x = closed[i].p_x;
                        dist.p_y = closed[i].p_y;
                    }
                }
            }
        }
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
