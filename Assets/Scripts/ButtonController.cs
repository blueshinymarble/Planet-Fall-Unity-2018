using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private RectTransform confirmBaseButtons;
	// Use this for initialization
	void Start ()
    {
        confirmBaseButtons = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
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
        //make the next button clickable
        confirmBaseButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
    }

    public void CancelBase()
    {
        
    }
}
