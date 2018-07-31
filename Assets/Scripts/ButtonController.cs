using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonController : MonoBehaviour
{
    public bool shipSelected;
    public bool turretSelected;
    public bool soldierSelected;
    public bool mechSelected;
    public Quaternion rotationToRevertTo;

    private BloomController bloomController;
    private TurnFlowManager turnFlowManager;
    private RectTransform confirmBaseButtons;
    private RectTransform confirmBloomButtons;
    private RectTransform confirmCancelRotate;
    private RectTransform soldierPanel;
    private Animator endButtonAnim;
    private Vector3 toRest;

	// Use this for initialization
	void Start ()
    {
        toRest = new Vector3(-793, 540, 0);
        shipSelected = false;
        turretSelected = false;
        soldierSelected = false;
        mechSelected = false;
        endButtonAnim = GameObject.Find("End Button").GetComponent<Animator>();
        confirmCancelRotate = GameObject.Find("Confirm Cancel Rotate").GetComponent<RectTransform>();
        confirmBaseButtons = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
        soldierPanel = GameObject.Find("Soldier Panel").GetComponent<RectTransform>();
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
	}

    void SetAllToFalse()
    {
        shipSelected = false;
        turretSelected = false;
        soldierSelected = false;
        mechSelected = false;
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
        confirmBaseButtons.anchoredPosition = toRest;//move yourself out of the way
    }

    public void CancelBase() // cancels the player's selection for their base
    {
        GameObject[] basesToDestroy = GameObject.FindGameObjectsWithTag("Just Placed Base");
        foreach (GameObject placedBase in basesToDestroy) // destroys the instantiated base prefab
        {
            Base baseScript = placedBase.GetComponent<Base>(); // the script is called base not building
            baseScript.ChangeParentTagPlayAnim();
            Destroy(placedBase);
        }
        confirmBaseButtons.anchoredPosition = toRest; //moves the confirm cancel buttons out of the way
        endButtonAnim.SetBool("readyToContinue", false); // makes sure the end turn button can't be pressed
        turnFlowManager.playerBasePlaced = false; // this bool is necessary so that terrain minimise maximise animation doesnt play when the mouse is just hovering over a terrain
    }

    public void SelectShip()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && shipSelected == false)
        {
            SetAllToFalse();
            shipSelected = true;
        }
        else
        {
            SetAllToFalse();
        }
    }

    public void SelectTurret()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && turretSelected == false)
        {
            SetAllToFalse();
            turretSelected = true;
        }
        else
        {
            SetAllToFalse();
        }
    }
    public void Rotate()
    {
        GameObject[] justPlaced = GameObject.FindGameObjectsWithTag("Just Placed");
        GameObject[] justSelected = GameObject.FindGameObjectsWithTag("Just Selected");
        List<GameObject> toRotate = new List<GameObject>();
        toRotate.AddRange(justPlaced);
        toRotate.AddRange(justSelected);
        foreach (GameObject turret in toRotate)
        {
            turret.transform.Rotate(0, 60, 0);
        }
    }

    public void ConfirmSelection()
    {
        GameObject[] justPlaced = GameObject.FindGameObjectsWithTag("Just Placed");
        GameObject[] justSelected = GameObject.FindGameObjectsWithTag("Just Selected");
        List<GameObject> toConfirm = new List<GameObject>();
        toConfirm.AddRange(justPlaced);
        toConfirm.AddRange(justSelected);
        foreach (GameObject turret in toConfirm)
        {
            turret.tag = "No tag";
        }
        confirmCancelRotate.anchoredPosition = toRest;
        soldierPanel.anchoredPosition = toRest;
    }

    public void Cancel()
    {
        GameObject[] toCancel = GameObject.FindGameObjectsWithTag("Just Placed");
        foreach (GameObject turret in toCancel)
        {
            Destroy(turret);
        }
        confirmCancelRotate.anchoredPosition = toRest;
        soldierPanel.anchoredPosition = toRest;
    }

    public void SelectSoldier()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && soldierSelected == false)
        {
            SetAllToFalse();
            soldierSelected = true;
        }
        else
        {
            SetAllToFalse();
        }
    }

    public void SelectMech()
    {
        if (turnFlowManager.currentState == TurnFlowManager.State.action && mechSelected == false)
        {
            SetAllToFalse();
            mechSelected = true;
        }
        else
        {
            SetAllToFalse();
        }
    }

    public void CancelSelection()
    {
        GameObject[] justPlaced = GameObject.FindGameObjectsWithTag("Just Placed");
        GameObject[] justSelected = GameObject.FindGameObjectsWithTag("Just Selected");
        List<GameObject> toCancel = new List<GameObject>();
        toCancel.AddRange(justPlaced);
        toCancel.AddRange(justSelected);
        foreach (GameObject objectToCancel in toCancel)
        {
            if (objectToCancel.tag == "Just Placed")
            {
                if (objectToCancel.GetComponent<TurretController>())
                {
                    objectToCancel.GetComponent<TurretController>().ChangeParentTagPlayAnim();
                    Destroy(objectToCancel);
                    confirmCancelRotate.anchoredPosition = toRest;
                }
                else if (objectToCancel.GetComponent<Soldier>())
                {
                    objectToCancel.GetComponent<Soldier>().ChangeParentTagPlayAnim();
                    Destroy(objectToCancel);
                    confirmCancelRotate.anchoredPosition = toRest;
                }
            }
            else
            {
                objectToCancel.tag = ("No tag");
                objectToCancel.transform.rotation = rotationToRevertTo;
            }
        }
        confirmCancelRotate.anchoredPosition = toRest;
        soldierPanel.anchoredPosition = toRest;
    }

    public void MoveUnitOneSpace()
    {

        // if this button is pressed the unit moves one space in the direction of its red pip
    }
}
