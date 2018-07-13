using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloomController : MonoBehaviour
{
    public GameObject bloom;
    public int controlTokens;
    public bool tokenPlaced;

    private Text controlCounterText;
	// Use this for initialization
	void Start ()
    {
        controlTokens = 0;
        tokenPlaced = false;
        controlCounterText = GameObject.Find("Control Token counter").GetComponentInChildren<Text>();
        controlCounterText.text = "Control Tokens: " + controlTokens;
    }

    public void PlaceBloomToken() //updates the control token count, updates the control counter text and changes the token placed bool that the end turn button depends on
    {
        controlTokens--;
        controlCounterText.text = "Control Tokens: " + controlTokens;
        tokenPlaced = true;
    }

    public void RemoveBloomToken() // does the opposite of the place bloom method
    {
        controlCounterText.text = "Control Tokens: " + controlTokens;
        tokenPlaced = false;
    }

    public void ChooseSpaceSpawnBloom() // chooses a random number of spaces and spawns control points there.
    {
        if (controlTokens == 0) // chooses a random number of control tokens 
        {
            controlTokens = Random.Range(3, 5);
        }

        controlCounterText.text = "Control Tokens: " + controlTokens;

        GameObject[] bloomPointsToDestroy = GameObject.FindGameObjectsWithTag("Bloom"); // destroy all the previous control points before spawning new ones
        foreach (GameObject toDestroy in bloomPointsToDestroy)
        {
            Destroy(toDestroy);
        }

        GameObject[] controlPoints = GameObject.FindGameObjectsWithTag("Control Point"); // finds all of the legal spaces and chooses random spaces from these to spawn control points
        foreach(GameObject controlPointTile in controlPoints)
        {
            controlPointTile.tag = "Legal Space";
        }

        GameObject[] legalSpaces = GameObject.FindGameObjectsWithTag("Legal Space");
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            GameObject randomSpace = legalSpaces[Random.Range(0, legalSpaces.Length)];
            GameObject SpawnBloom = Instantiate(bloom, randomSpace.transform.position, Quaternion.identity);
            SpawnBloom.transform.parent = randomSpace.transform;
        }
    }
}
