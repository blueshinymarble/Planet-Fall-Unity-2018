using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public int myControlTokens;
    public BloomController bloomController;

	// Use this for initialization
	void Start ()
    {
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        myControlTokens = bloomController.controlTokens;
        gameObject.transform.parent.tag = "Control Point"; // changes the tag of the tile. This is important when new control points need to be spawned because the bloom controller needs to know which tiles were used
	}
}
