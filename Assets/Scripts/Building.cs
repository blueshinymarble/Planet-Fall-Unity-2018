﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Occupied";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}