using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloomController : MonoBehaviour
{
    public GameObject bloom;
    public int controlTokens;

    private Text controlCounterText;
	// Use this for initialization
	void Start ()
    {
        controlTokens = Random.Range(3, 5);
        controlCounterText = GameObject.Find("Control Token counter").GetComponentInChildren<Text>();
        controlCounterText.text = "Control Tokens: " + controlTokens;
    }
    
    public void PlaceBloomToken()
    {
        if (controlTokens == 0)
        {
            ChooseSpaceSpawnBloom();//place new blooms and place one token;
            controlCounterText.text = "Control Tokens: " + controlTokens;//display control counters;
        }
        else
        {
            //place a control token;
            //display control counters;
        }
    }

    public void ChooseSpaceSpawnBloom()
    {
        GameObject[] bloomPointsToDestroy = GameObject.FindGameObjectsWithTag("Bloom");
        foreach (GameObject toDestroy in bloomPointsToDestroy)
        {
            Destroy(toDestroy);
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
