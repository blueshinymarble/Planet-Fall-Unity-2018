using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    private Animator myAnim;

	// Use this for initialization
	void Start ()
    {
        myAnim = gameObject.GetComponent<Animator>();
	}


}
