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

    private GameObject roundAnnouncer;
    private GameObject announcer;
	// Use this for initialization
	void Start ()
    {
        roundAnnouncer = GameObject.Find("Round counter");
        currentRoundInt = 1;
        firstRound = true;
        announcer = GameObject.Find("Announcer");
        currentState = State.roundBegins;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(currentState);
	}

    public void SpaceForBase()
    {
        announcer.GetComponentInChildren<Text>().text = "Choose a valid space (not a hazard or another player's base) to place your base.";
        announcer.GetComponent<Animator>().Play("announcer appear");
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
                    currentState = TurnFlowManager.State.bloom;
                }
                break;

            case TurnFlowManager.State.firstRound:
                currentState = TurnFlowManager.State.bloom;
                break;

            case TurnFlowManager.State.bloom:
                currentState = TurnFlowManager.State.action;
                break;

            case TurnFlowManager.State.action:
                currentRoundInt++;
                roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;
                if (currentRoundInt == 9)
                {
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Scoring Round";
                    currentState = State.scoring;
                }
                else
                {
                    roundAnnouncer.GetComponentInChildren<Text>().text = "Round " + currentRoundInt;
                    currentState = State.roundBegins;
                }
                break;
        }
    }
}
