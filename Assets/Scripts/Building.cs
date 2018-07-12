using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Occupied"; // changes the tag of the parent tile of this terrain to occupied so it cannot be used to spawn control points
	}
	
}
