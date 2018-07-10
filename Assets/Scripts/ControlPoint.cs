using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public int myControlTokens;

	// Use this for initialization
	void Start ()
    {
        myControlTokens = 0;
        gameObject.transform.parent.tag = "Control Point";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
