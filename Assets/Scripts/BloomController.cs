using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomController : MonoBehaviour
{
    public GameObject bloom;



	// Use this for initialization
	void Start ()
    {

    }
    
    void PlaceBloomToken()
    {

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
