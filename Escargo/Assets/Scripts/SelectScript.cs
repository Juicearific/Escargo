﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour {

    public Sprite[] imgs = new Sprite[5];
    public Image p1Img;
    public int p1Index;
    public Image p2Img;
    public int p2Index;
    public Image p3Img;
    public int p3Index;
    public Image p4Img;
    public int p4Index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void p1Right()
    {
        p1Index++;
        if (p1Index > 4)
        {
            p1Index = 0;
        }
        p1Img.sprite = imgs[p1Index];
        OptionsStaticScript.p1Name = updateSprite(p1Index);
    }

    public void p1Left()
    {
        Debug.Log("Ffuck you");
        p1Index--;
        if (p1Index < 0)
        {
            p1Index = 4;
        }
        p1Img.sprite = imgs[p1Index];
        OptionsStaticScript.p1Name = updateSprite(p1Index);
    }

    public void p2Right()
    {
        p2Index++;
        if (p2Index > 4)
        {
            p2Index = 0;
        }
        p2Img.sprite = imgs[p2Index];
        OptionsStaticScript.p2Name = updateSprite(p2Index);
    }

    public void p2Left()
    {
        p2Index--;
        if (p2Index < 0)
        {
            p2Index = 4;
        }
        p2Img.sprite = imgs[p2Index];
        OptionsStaticScript.p2Name = updateSprite(p2Index);
    }

    public void p3Right()
    {
        p3Index++;
        if (p3Index > 4)
        {
            p3Index = 0;
        }
        p3Img.sprite = imgs[p3Index];
        OptionsStaticScript.p3Name = updateSprite(p3Index);
    }

    public void p3Left()
    {
        p3Index--;
        if (p3Index < 0)
        {
            p3Index = 4;
        }
        p3Img.sprite = imgs[p3Index];
        OptionsStaticScript.p3Name = updateSprite(p3Index);
    }

    public void p4Right()
    {
        p4Index++;
        if (p4Index > 4)
        {
            p4Index = 0;
        }
        p4Img.sprite = imgs[p4Index];
        OptionsStaticScript.p4Name = updateSprite(p4Index);
    }

    public void p4Left()
    {
        p4Index--;
        if (p4Index < 0)
        {
            p4Index = 4;
        }
        p4Img.sprite = imgs[p4Index];
        OptionsStaticScript.p4Name = updateSprite(p4Index);
    }

    public string updateSprite(int index)
    {
        if (index == 0)
        {
            return "n/a";
        }
        else if(index == 1)
        {
            return "pierre";
        }
        else if(index == 2)
        {
            return "kenta";
        }
        else if (index == 3)
        {
            return "bertha";
        }
        else // (index == 4)
        {
            return "liljim";
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("Demo");
    }
}