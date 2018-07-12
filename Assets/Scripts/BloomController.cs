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

    private void Update()
    {
        Debug.Log("tokenPlaced bool is " + tokenPlaced);
    }

    public void PlaceBloomToken()
    {
        controlTokens--;
        controlCounterText.text = "Control Tokens: " + controlTokens;
        tokenPlaced = true;
    }

    public void RemoveBloomToken()
    {
        controlTokens++;
        controlCounterText.text = "Control Tokens: " + controlTokens;
        tokenPlaced = false;
    }

    public void ChooseSpaceSpawnBloom()
    {
        if (controlTokens == 0)
        {
            controlTokens = Random.Range(3, 5);
        }

        controlCounterText.text = "Control Tokens: " + controlTokens;

        GameObject[] bloomPointsToDestroy = GameObject.FindGameObjectsWithTag("Bloom");
        foreach (GameObject toDestroy in bloomPointsToDestroy)
        {
            Destroy(toDestroy);
        }

        GameObject[] controlPoints = GameObject.FindGameObjectsWithTag("Control Point");
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
