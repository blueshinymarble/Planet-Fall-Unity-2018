using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Hazard"; // changes the parent tile to hazard so it cannot be used for anything
	}
}
