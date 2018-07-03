using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private BloomController bloomController;

	// Use this for initialization
	void Start ()
    {
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
	}
    
    public void TestBloom()
    {
        bloomController.ChooseSpaceSpawnBloom();
    }

}
