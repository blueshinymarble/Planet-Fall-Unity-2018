using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public bool shipSelected;
    public bool turretSelected;
    public bool soldierSelected;
    public bool mechSelected;

    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private RectTransform confirmBaseButtons;
    private RectTransform confirmBloomButtons;
    private RectTransform confirmCancelRotate;
    private Animator endButtonAnim;

	// Use this for initialization
	void Start ()
    {
        shipSelected = false;
        turretSelected = false;
        soldierSelected = false;
        mechSelected = false;
        endButtonAnim = GameObject.Find("End Button").GetComponent<Animator>();
        confirmCancelRotate = GameObject.Find("Confirm Cancel Rotate").GetComponent<RectTransform>();
        confirmBaseButtons = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
	}

    public void Next() // when the end turn button is pressed it runs this method
    {
        if (endButtonAnim.GetBool("readyToContinue") == true)
        {
            turnFlowManager.ManageTurn();
        }

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

    public void SelectShip()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && shipSelected == false)
        {
            shipSelected = true;
        }
        else
        {
            shipSelected = false;
        }
    }

    public void SelectTurret()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && turretSelected == false)
        {
            turretSelected = true;
        }
        else
        {
            turretSelected = false;
        }
    }
    public void Rotate()
    {
        GameObject[] toRotate = GameObject.FindGameObjectsWithTag("Just Placed");
        foreach (GameObject turret in toRotate)
        {
            turret.transform.Rotate(0, 60, 0);
        }
    }

    public void ConfirmTurret()
    {
        GameObject[] toConfirm = GameObject.FindGameObjectsWithTag("Just Placed");
        foreach (GameObject turret in toConfirm)
        {
            turret.tag = "Turret";
        }
        confirmCancelRotate.anchoredPosition = new Vector3(-793, 540, 0);
        
    }

    public void CancelTurret()
    {
        GameObject[] toCancel = GameObject.FindGameObjectsWithTag("Just Placed");
        foreach (GameObject turret in toCancel)
        {
            Destroy(turret);
        }
        confirmCancelRotate.anchoredPosition = new Vector3(-793, 540, 0);
    }

    public void SelectSoldier()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && soldierSelected == false)
        {
            soldierSelected = true;
        }
        else
        {
            soldierSelected = false;
        }
    }

    public void SelectMech()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && mechSelected == false)
        {
            mechSelected = true;
        }
        else
        {
            mechSelected = false;
        }
    }
}
