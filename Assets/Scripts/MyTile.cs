using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTile : MonoBehaviour
{
    public GameObject[] TerrainTiles;
    public GameObject powerPlant;
    public GameObject selectingSpace;
    public Camera cam;

    private TurnFlowManager turnFlowManager;
    private BloomController bloomController;
    private Text controlTokenCounter;
    private Animator anim;
    private RectTransform confirmCancel;
    private RectTransform confirmCancelBloom;

    // Use this for initialization
    void Start()
    {
        confirmCancel = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
        confirmCancelBloom = GameObject.Find("Confirm bloom placement panel").GetComponent<RectTransform>();
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        controlTokenCounter = GameObject.Find("Control Token counter").GetComponentInChildren<Text>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
        Board board = GameObject.Find("Board").GetComponent<Board>();
        while (gameObject.transform.childCount < 1) // calls the board script's method to choose a tile randomly
        {
            board.ChooseTile((Board.TerrainTilesEnum)Random.Range(0, 6), transform);
        }
    }

    private void OnMouseEnter() 
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound) // when the mouse hovers the tile during the first round of the game the terrain of that tile shrinks to show it is a possible spawn location for the player's base
        {
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            GameObject selectingBase = Instantiate(selectingSpace, transform.position, Quaternion.identity);
            selectingBase.transform.parent = gameObject.transform;
        }
    }

    private void OnMouseExit() 
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound) // when the mouse leaves the tile it destroys the green selectable base that was used to illustrate that a base could be spawned here and brings back the terrain of the tile
        {
            GameObject[] selectings = GameObject.FindGameObjectsWithTag("Selecting");
            foreach (GameObject selecting in selectings)
            {
                Destroy(selecting);
            }
            gameObject.GetComponentInChildren<Animator>().Play("terrain maximise");
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
        { // if its the first round it spawns in the player's base and brings in the confrim and cancel buttons
            turnFlowManager.playerBasePlaced = true;
            GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
            newBase.transform.parent = gameObject.transform;
            GameObject[] selectings = GameObject.FindGameObjectsWithTag("Selecting");
            foreach (GameObject selecting in selectings)
            {
                Destroy(selecting);
            }
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            confirmCancel.anchoredPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        else if (turnFlowManager.currentState == TurnFlowManager.State.placeBloomAndCounters && bloomController.tokenPlaced == false && gameObject.tag == "Control Point")
        { // if its the place bloom and counters phase it lets the player choose this space to place a control token
            confirmCancelBloom.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        else if (turnFlowManager.currentState == TurnFlowManager.State.placeCounters && bloomController.tokenPlaced == false && gameObject.tag == "Control Point")
        { // if its the placec counters phase ibasically does the same thing as the place bloom and counters phase
            confirmCancelBloom.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    void MinimiseTerrain() // method that was written to combat a bug that occured when a base was placed and cancelled but the mouse was already hovering on a tile and base placed the tile wouldnt play the minimise animation
    {   // this method ensures the animation will always be played
        Animator[] myTerrainTileAnimators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator anim in myTerrainTileAnimators)
        {
            anim.Play("terrain minimise");
        }
    }

}