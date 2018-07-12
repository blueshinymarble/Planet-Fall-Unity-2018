using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.tag = "Just Placed Base";
        gameObject.transform.parent.tag = "Occupied";
		
	}
}
