using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerablePip : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Vulnerable";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
