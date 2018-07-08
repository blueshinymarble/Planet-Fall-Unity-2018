using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTile : MonoBehaviour
{
    public GameObject[] TerrainTiles;
    public GameObject powerPlant;

    private TurnFlowManager turnFlowManager;
    private BloomController bloomController;
    private Text controlTokenCounter;

    // Use this for initialization
    void Start()
    {
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
        Board board = GameObject.Find("Board").GetComponent<Board>();
        while (gameObject.transform.childCount < 1)
        {
            board.ChooseTile((Board.TerrainTilesEnum)Random.Range(0, 6), transform);
        }
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        controlTokenCounter = GameObject.Find("Control Token counter").GetComponentInChildren<Text>();
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
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false)
        {
            if (turnFlowManager.currentState == TurnFlowManager.State.firstRound)
            {
                GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
                newBase.transform.parent = gameObject.transform;
                MinimiseTerrain();
                turnFlowManager.playerBasePlaced = true;
            }
        }
        else if (turnFlowManager.currentState == TurnFlowManager.State.bloom)
        {
            gameObject.GetComponentInChildren<ControlPoint>().myControlTokens++;
            bloomController.controlTokens--;
            controlTokenCounter.text = "Control Tokens: " + bloomController.controlTokens;
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