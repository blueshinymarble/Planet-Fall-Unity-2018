﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public GameObject[] TerrainTiles;

	// Use this for initialization
	void Start ()
    {
        Board board = GameObject.Find("Board").GetComponent<Board>();
        while (gameObject.transform.childCount < 1)
        {
            board.ChooseTile((Board.TerrainTilesEnum)Random.Range(0, 6), transform);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
