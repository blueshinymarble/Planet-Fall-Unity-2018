﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    public GameObject[] TerrainTiles;
    public GameObject powerPlant;

    private TurnFlowManager turnFlowManager;

    // Use this for initialization
    void Start()
    {
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
        Board board = GameObject.Find("Board").GetComponent<Board>();
        while (gameObject.transform.childCount < 1)
        {
            board.ChooseTile((Board.TerrainTilesEnum)Random.Range(0, 6), transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        //play selection animation;
    }

    private void OnMouseDown()
    {
        if (gameObject.tag != "Hazard")
        {
            if (turnFlowManager.currentState == TurnFlowManager.State.firstRound)
            {
                GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
                newBase.transform.parent = gameObject.transform;
                MinimiseTerrain();
            }
        }
    }

    void MinimiseTerrain()
    {
        Animator[] myTerrainTileAnimators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator anim in myTerrainTileAnimators)
        {
            anim.Play("terrain minimise");
        }
    }

}