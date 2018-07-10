using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private Animator confirmBaseButtons;
	// Use this for initialization
	void Start ()
    {
        confirmBaseButtons = GameObject.Find("Confirm cancel base placement").GetComponent<Animator>();
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
        confirmBaseButtons.Play("move out");//move yourself out of the way
    }

}
