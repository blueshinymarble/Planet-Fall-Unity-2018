using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTile : MonoBehaviour
{
    public GameObject[] TerrainTiles;
    public GameObject powerPlant;
    public GameObject selectingSpace;
    public GameObject selector;
    public Camera cam;
    public GameObject shipSelector;
    public GameObject prefabShip;

    private TurnFlowManager turnFlowManager;
    private BloomController bloomController;
    private Text controlTokenCounter;
    private Animator anim;
    private RectTransform confirmCancel;
    private RectTransform confirmCancelBloom;
    private ButtonController myButtonController;

    // Use this for initialization
    void Start()
    {
        myButtonController = GameObject.Find("Button Controller").GetComponent<ButtonController>();
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
            GameObject selecting = Instantiate(selector, gameObject.transform.position, Quaternion.identity);
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.shipSelected == true)
        {
            GameObject selecting = Instantiate(shipSelector, new Vector3(transform.position.x, 6f, transform.position.z), Quaternion.identity);
            selecting.transform.parent = gameObject.transform;
        }
    }

    private void OnMouseExit() 
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound) // when the mouse leaves the tile it destroys the green selectable base that was used to illustrate that a base could be spawned here and brings back the terrain of the tile
        {
            DestroySelecting("Selecting");
        }
        else if (gameObject.tag != "Hazard" && turnFlowManager.currentState == TurnFlowManager.State.action)
        {
            DestroySelecting("Selecting");
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
        { // if its the first round it spawns in the player's base and brings in the confrim and cancel buttons
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            turnFlowManager.playerBasePlaced = true;
            GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
            newBase.transform.parent = gameObject.transform;
            DestroySelecting("Selecting");
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            confirmCancel.anchoredPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.shipSelected == true)
        {
            GameObject newShip = Instantiate(prefabShip, transform.position, Quaternion.identity);
            newShip.transform.parent = gameObject.transform;
            myButtonController.shipSelected = false;
            DestroySelecting("Selecting");

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

    void DestroySelecting(string name)
    {
        GameObject[] selectings = GameObject.FindGameObjectsWithTag(name);
        foreach (GameObject selecting in selectings)
        {
            Destroy(selecting);
        }
    }
}