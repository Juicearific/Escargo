using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitscreenScript : MonoBehaviour {
    public static int numCameras = 2;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
	public Camera cam4;

	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreLayerCollision(9, 9, true);
        if (numCameras == 1)
        {
            cam1.rect = new Rect(0, 0, 1, 1);
        }
        else if (numCameras == 2)
        {
            cam1.rect = new Rect(0, 0, .5f, 1);
            cam2.rect = new Rect(.5f, 0, .5f, 1);
        } else if (numCameras >= 3) {
            cam1.rect = new Rect(0, .5f, .5f, .45f);
            cam2.rect = new Rect(.5f,.5f,.5f,.5f);
            cam3.rect = new Rect(0,0,.5f,.5f);
            if (numCameras == 4)
            {
                cam4.rect = new Rect(.5f, 0, .5f, .5f);
            } else {
                // set cam4 to minimap???
            }
        }
	}
}
