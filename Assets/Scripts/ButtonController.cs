using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private GameObject confirmBaseButtons;
	// Use this for initialization
	void Start ()
    {
        confirmBaseButtons = GameObject.Find("Confirm cancel base placement");
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
	}
    
    public void TestBloom()
    {
        bloomController.ChooseSpaceSpawnBloom();
    }

    public void Next()
    {
        turnFlowManager.ManageTurn();
    }

    public void ConfirmBase()
    {
        turnFlowManager.playerBasePlaced = true;
        //make the next button clickable
        //move yourself out of the way
    }

}
