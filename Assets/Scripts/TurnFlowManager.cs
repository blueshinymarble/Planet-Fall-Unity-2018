using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnFlowManager : MonoBehaviour
{

    public enum State {firstRound, roundBegins, bloom, action, scoring, placeBloomAndCounters, placeCounters, finalScoring, controlPoints}
    public State currentState;
    public bool firstRound;
    public int currentRoundInt;
    public bool playerBasePlaced;
    public bool readyToContinue;
    public int controlPointCount;

    private GameObject roundAnnouncer;
    private Text generalAnnouncer;
    private BloomController bloomController;
    private Animator endButtonAnim;

	// Use this for initialization
	void Start ()
    {
        readyToContinue = true;
        endButtonAnim = GameObject.Find("End Button").GetComponent<Animator>();
        endButtonAnim.Play("end turn");
        playerBasePlaced = false;
        roundAnnouncer = GameObject.Find("Round counter");
        currentRoundInt = 0;
        firstRound = true;
        generalAnnouncer = GameObject.Find("Announcer").GetComponentInChildren<Text>();
        currentState = State.roundBegins;
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        generalAnnouncer.text = "Beginning of round";
	}

    private void Update()
    {
        Debug.Log(Input.mousePosition);
    }

    public void ManageTurn() // manages the turn 
    { // bloom state needs to place the control points and then the maximum amount of tokens on those points from the get go
        switch (currentState)
        {
            case State.roundBegins:
                GameObject[] controlPointTiles = GameObject.FindGameObjectsWithTag("Control Point");
                controlPointCount = controlPointTiles.Length;
                endButtonAnim.SetBool("readyToContinue", false); // sets whether the continue button nis ready or not
                if (firstRound)
                {
                    currentState = State.firstRound;
                    generalAnnouncer.text = "Click a valid space (not a hazard or another player's base) to place your base";
                    firstRound = false;
                }
                else if (controlPointCount == 0)
                {
                    currentState = State.controlPoints;
                    generalAnnouncer.text = "Control Points set";
                    endButtonAnim.SetBool("readyToContinue", false);
                    bloomController.LegalSpaceControl();
                    bloomController.SpawnControlPoints();
                    currentState = State.action;
                }
                else
                {
                    currentState = State.action;
                    generalAnnouncer.text = "Actions";
                }
                break;

            case State.controlPoints:
                currentState = State.action;
                generalAnnouncer.text = "Action phase";
                break;

            case State.firstRound:
                if (playerBasePlaced == true)
                {
                    generalAnnouncer.text = "Control Points set";
                    endButtonAnim.SetBool("readyToContinue", false);
                    currentState = State.controlPoints;
                    bloomController.LegalSpaceControl();
                    bloomController.SpawnControlPoints();
                }
                break;

            case State.bloom:
                currentState = State.action;
                generalAnnouncer.text = "Action phase";
                break;

            case State.action:
                if (bloomController.controlTokens == 0 && currentRoundInt == 8)
                {
                    currentState = State.finalScoring;
                    generalAnnouncer.text = "Final scoring";
                }
                else
                {
                    currentState = State.roundBegins;
                    generalAnnouncer.text = "Next turn";
                }
                break;
        }
    }
}
