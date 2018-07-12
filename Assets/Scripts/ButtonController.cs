using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private RectTransform confirmBaseButtons;
    private RectTransform confirmBloomButtons;
    private Animator endButtonAnim;
	// Use this for initialization
	void Start ()
    {
        endButtonAnim = GameObject.Find("End Button").GetComponent<Animator>();
        confirmBaseButtons = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
        confirmBloomButtons = GameObject.Find("Confirm bloom placement panel").GetComponent<RectTransform>();
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
	}

    public void Next() // when the end turn button is pressed it runs this method
    {
        turnFlowManager.ManageTurn();
    }

    public void ConfirmBase() // confirm the space chosen for player's base
    {
        endButtonAnim.SetBool("readyToContinue", true);//make the next button clickable
        confirmBaseButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
    }

    public void CancelBase() // cancels the player's selection for their base
    {
        GameObject[] basesToDestroy = GameObject.FindGameObjectsWithTag("Just Placed Base");
        foreach (GameObject placedBase in basesToDestroy) // destroys the instantiated base prefab
        {
            Destroy(placedBase);
        }
        confirmBaseButtons.anchoredPosition = new Vector3(-793, 540); //moves the confirm cancel buttons out of the way
        endButtonAnim.SetBool("readyToContinue", false); // makes sure the end turn button can't be pressed
        turnFlowManager.playerBasePlaced = false; // this bool is necessary so that terrain minimise maximise animation doesnt play when the mouse is just hovering over a terrain
        GameObject.FindGameObjectWithTag("Occupied").GetComponentInChildren<Animator>().Play("terrain maximise"); // plays terrain maximise animation to make the terrain reappear
        GameObject changeTag = GameObject.FindGameObjectWithTag("Occupied"); //finds the tile that was clicked
        changeTag.tag = "Legal Space"; // changes the clicked tiles tag back to legal so it can be used again
    }

    public void ConfirmBloomPlacement() // confirm choice for control token
    {
        confirmBloomButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
        endButtonAnim.SetBool("readyToContinue", true);
    }

    public void CancelBloomPlacement() //cancel choice for control token
    {
        bloomController.RemoveBloomToken();
        confirmBloomButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
    }
}
