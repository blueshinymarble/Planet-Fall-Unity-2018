using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePip : MonoBehaviour
{
    public GameObject checker;

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent.tag = "Move Attack";
        foreach (Transform child in transform.parent)
        {
            child.gameObject.tag = "Move Attack";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
