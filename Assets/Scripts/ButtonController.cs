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
        endButtonAnim.SetBool("readyToContinue", true);//make the next button clickable
        confirmBaseButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
    }

    public void CancelBase()
    {
        GameObject[] basesToDestroy = GameObject.FindGameObjectsWithTag("Just Placed Base");
        foreach (GameObject placedBase in basesToDestroy)
        {
            Destroy(placedBase);
        }
        confirmBaseButtons.anchoredPosition = new Vector3(-793, 540);
        endButtonAnim.SetBool("readyToContinue", false);
        turnFlowManager.playerBasePlaced = false;
        GameObject.FindGameObjectWithTag("Occupied").GetComponentInChildren<Animator>().Play("terrain maximise");
        GameObject changeTag = GameObject.FindGameObjectWithTag("Occupied");
        changeTag.tag = "Legal Space";
    }

    public void ConfirmBloomPlacement()
    {
        confirmBloomButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
        endButtonAnim.SetBool("readyToContinue", true);
    }

    public void CancelBloomPlacement()
    {
        bloomController.RemoveBloomToken();
        confirmBloomButtons.anchoredPosition = new Vector3(-793, 540);//move yourself out of the way
    }
}
