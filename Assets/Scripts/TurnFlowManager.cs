﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnFlowManager : MonoBehaviour
{

    public enum State {firstRound, roundBegins, bloom, action, scoring}
    public State currentState;

    private GameObject announcer;
	// Use this for initialization
	void Start ()
    {
        announcer = GameObject.Find("Announcer");
        currentState = State.firstRound;
        SpaceForBase();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpaceForBase()
    {
        announcer.GetComponentInChildren<Text>().text = "Choose a valid space (not a hazard or another player's base) to place your base.";
        announcer.GetComponent<Animator>().Play("announcer appear");
    }
}
