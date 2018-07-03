using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Hazard";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
