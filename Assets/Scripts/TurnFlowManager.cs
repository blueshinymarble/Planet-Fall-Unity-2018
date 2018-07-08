using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnFlowManager : MonoBehaviour
{

    public enum State {firstRound, roundBegins, bloom, action, scoring}
    public State currentState;
    public bool firstRound;
    public int currentRoundInt;
    public bool playerBasePlaced;

    private GameObject roundAnnouncer;
    private Text generalAnnouncer;
    private BloomController bloomController;

	// Use this for initialization
	void Start ()
    {
        playerBasePlaced = false;
        roundAnnouncer = GameObject.Find("Round counter");
        currentRoundInt = 1;
        firstRound = true;
        generalAnnouncer = GameObject.Find("Announcer").GetComponentInChildren<Text>();
        currentState = State.roundBegins;
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        generalAnnouncer.text = "Beginning of round";
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(currentState);
	}

    public void SpaceForBase()
    {
        generalAnnouncer.text = "Click a valid space (not a hazard or another player's base) to place your base";
        generalAnnouncer.GetComponent<Animator>().Play("announcer appear");
    }

    public void ManageTurn()
    {
        switch (currentState)
        {
            case TurnFlowManager.State.roundBegins:
                if (firstRound == true)
                {
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;
                    currentState = TurnFlowManager.State.firstRound;
                    SpaceForBase();
                    firstRound = false;
                }
                else
                {
                    generalAnnouncer.text = "Click a control space to place a control counter";
                    currentState = TurnFlowManager.State.bloom;
                    bloomController.ChooseSpaceSpawnBloom();
                }
                break;

            case TurnFlowManager.State.firstRound:
                if (playerBasePlaced == true)
                {
                    currentState = TurnFlowManager.State.bloom;
                    bloomController.ChooseSpaceSpawnBloom();
                    generalAnnouncer.text = "Click a control space to place a control counter";
                }
                break;

            case TurnFlowManager.State.bloom:
                currentState = TurnFlowManager.State.action;
                generalAnnouncer.text = "Action phase";
                break;

            case TurnFlowManager.State.action:
                currentRoundInt++;
                roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;

                if (currentRoundInt == 9)
                {
                    generalAnnouncer.text = "Scoring round begins";
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Scoring Round";
                    currentState = State.scoring;
                }
                else
                {
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;
                    currentState = State.roundBegins;
                    generalAnnouncer.text = "Next round begins";
                }
                break;
        }
    }
}
