using UnityEngine;
using System.Collections;


public class _ColorRotator : MonoBehaviour {

    int whichColor;

    const int min = 0x80;
    const int max = 0xFF;

    float red = min;
    float green = min;
    float blue = min;
    public float stepR = 1;     //irrelevant, exposed and modified in unity
    public float stepG = 2;
    //const float stepB = 0.33f;
    float dirR = 1;
    float dirG = 1;
    //float dirB = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



        //pseudo

        //do all reds, greens, blues.

        red += stepR * dirR;

        if (red % max == 0) { dirR *= -1; }


        green += stepG * dirG;

        if (green % max == 0) { dirG *= -1; }

        //blue += stepB * dirB;

        //if (blue % max == 0) { dirB *= -1; }


        Color color = new Color(red / 0xFF, green / 0xFF, blue / 0xFF, 1);

        gameObject.renderer.material.color = color;

        //Debug.Log(color);


	}
}
