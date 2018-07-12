using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int myForestOneCount = 6;
    public static int myForestTwoCount = 10;
    public static int myForestThreeCount = 4;
    public static int myMountainOneCount = 12;
    public static int myMountainTwoCount = 8;
    public static int myHazardCount = 5;
    public GameObject forest01;
    public GameObject forest02;
    public GameObject forest03;
    public GameObject mountain01;
    public GameObject mountain02;
    public GameObject hazard;
    public enum TerrainTilesEnum { forest1, forest2, forest3, mountain1, mountain2, hazard};

    //private Animator myAnim;
  
	// Use this for initialization
	void Start ()
    {

	}
	
	public void ChooseTile(TerrainTilesEnum tile, Transform child) // keeps a count of each terrain type and spawns terrain according to how many of that terain type is left. This method is called by each tile 
    {
        switch (tile)
        {
            case TerrainTilesEnum.forest1:
                if (myForestOneCount > 0)
                {
                    GameObject newForest1 = Instantiate(forest01, child.transform.position, Quaternion.identity);
                    newForest1.transform.parent = child;
                    myForestOneCount--;
                }
                break;

            case TerrainTilesEnum.forest2:
                if (myForestTwoCount > 0)
                {
                    GameObject newForest2 = Instantiate(forest02, child.transform.position, Quaternion.identity);
                    newForest2.transform.parent = child;
                    myForestTwoCount--;
                }
                break;

            case TerrainTilesEnum.forest3:
                if (myForestThreeCount > 0)
                {
                    GameObject newForest3 = Instantiate(forest03, child.transform.position, Quaternion.identity);
                    newForest3.transform.parent = child;
                    myForestThreeCount--;
                }
                break;

            case TerrainTilesEnum.mountain1:
                if (myMountainOneCount > 0)
                {
                    GameObject newMountain1 = Instantiate(mountain01, child.transform.position, Quaternion.identity);
                    newMountain1.transform.parent = child;
                    myMountainOneCount--;
                }
                break;

            case TerrainTilesEnum.mountain2:
                if (myMountainTwoCount > 0)
                {
                    GameObject newMountain2 = Instantiate(mountain02, child.transform.position, Quaternion.identity);
                    newMountain2.transform.parent = child;
                    myMountainTwoCount--;
                }
                break;

            case TerrainTilesEnum.hazard:
                if (myHazardCount > 0)
                {
                    GameObject newHazard = Instantiate(hazard, child.transform.position, Quaternion.identity);
                    newHazard.transform.parent = child;
                    myHazardCount--;
                }
                break;
        }
    }
}
