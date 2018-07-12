using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnFlowManager : MonoBehaviour
{

    public enum State {firstRound, roundBegins, bloom, action, scoring, placeBloomAndCounters, placeCounters, finalScoring}
    public State currentState;
    public bool firstRound;
    public int currentRoundInt;
    public bool playerBasePlaced;
    public bool readyToContinue;

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

    public void ManageTurn() // manages the turn 
    {
        switch (currentState)
        {
            case State.roundBegins:
                endButtonAnim.SetBool("readyToContinue", false);
                if (firstRound)
                {
                    currentState = State.firstRound;
                    generalAnnouncer.text = "Click a valid space (not a hazard or another player's base) to place your base";
                    firstRound = false;
                }
                else if (firstRound == false && bloomController.controlTokens > 0 && currentRoundInt < 9)
                {
                    currentState = State.placeCounters;
                    generalAnnouncer.text = "Click a control space to place a control counter";
                }
                else if (firstRound == false && bloomController.controlTokens == 0 && currentRoundInt < 9)
                {
                    currentState = State.placeBloomAndCounters;
                    bloomController.ChooseSpaceSpawnBloom();
                    generalAnnouncer.text = "Click a control space to place a control counter"; //TODO
                }
                break;

            case State.placeCounters:
                if (bloomController.tokenPlaced == true)
                {
                    currentState = State.action;
                    generalAnnouncer.text = "Action phase";
                    bloomController.tokenPlaced = false;
                }
                break;

            case State.placeBloomAndCounters:
                if (bloomController.tokenPlaced == true)
                {
                    currentState = State.action;
                    generalAnnouncer.text = "Action phase";
                    bloomController.tokenPlaced = false;
                    currentRoundInt++;
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;
                }
                break;

            case State.firstRound:
                if (playerBasePlaced == true)
                {
                    endButtonAnim.SetBool("readyToContinue", false);
                    currentState = State.placeBloomAndCounters;
                    generalAnnouncer.text = "Click a control space to place a control counter";
                    bloomController.tokenPlaced = false;
                    bloomController.ChooseSpaceSpawnBloom();
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
