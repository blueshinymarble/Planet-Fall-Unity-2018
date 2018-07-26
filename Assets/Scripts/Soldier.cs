﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Soldier : MonoBehaviour
{
    public GameObject pip;
    public Transform[] thisUnitSlots;
    public Material[] myMaterialsArray;

    private List<Material> myMaterials = new List<Material>();
    //bool will move
    //bool will shoot and remain on my space
    //bool will shoot and move into that space
    //bool will rotate
    //bool will take damage 
    //bool will be destroyed
    //int shield strength that starts at 1

	// Use this for initialization
	void Start ()
    {
        myMaterials = myMaterialsArray.ToList();
        int pipCount = 3;
        while (pipCount > 0) 
        {
            Transform selectedSlot = thisUnitSlots[Random.Range(0, thisUnitSlots.Length - 1)];
             if (selectedSlot.childCount < 2)
             {
                GameObject spawnedPip = Instantiate(pip, new Vector3(selectedSlot.position.x, selectedSlot.position.y + 0.5f, selectedSlot.position.z), Quaternion.identity);
                spawnedPip.transform.parent = selectedSlot;
                Material randomMat = myMaterials[Random.Range(0, myMaterials.Count -1)];
                spawnedPip.GetComponent<MeshRenderer>().material = randomMat;
                myMaterials.Remove(randomMat);
                pipCount--;
             }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("clicked");
        //bring the move rotate cancel ui menu into view
        //if the space in front of the moce arrow is a legal spacec then allow the unit to move to that space.
        //if move is clicked play the animated arrow
        //possibly change the color of the hex in front of the unit to signify it can move there
        //if rotate is clicked rotate the unit
    }
}
