using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTile : MonoBehaviour
{
    public GameObject[] TerrainTiles;
    public GameObject powerPlant;
    public GameObject selector;
    public Camera cam;
    public GameObject prefabShip;
    public GameObject prefabTurret;
    public GameObject prefabSoldier;
    public GameObject prefabMech;

    private TurnFlowManager turnFlowManager;
    private BloomController bloomController;
    private Text controlTokenCounter;
    private Animator anim;
    private RectTransform confirmCancel;
    private RectTransform confirmCancelBloom;
    private ButtonController myButtonController;
    private RectTransform confirmCancelRotate;
    private Vector3 toSelect;

    // Use this for initialization
    void Start()
    {
        toSelect = new Vector3(920, 210, 0);
        myButtonController = GameObject.Find("Button Controller").GetComponent<ButtonController>();
        confirmCancelRotate = GameObject.Find("Confirm Cancel Rotate").GetComponent<RectTransform>();
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
        GameObject[] selectingArrows = GameObject.FindGameObjectsWithTag("Selecting");
        foreach (GameObject arrows in selectingArrows)
        {
            Destroy(arrows);
        }
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound) // when the mouse hovers the tile during the first round of the game the terrain of that tile shrinks to show it is a possible spawn location for the player's base
        {
            CreateSelectingArrows();
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.shipSelected == true)
        {
            CreateSelectingArrows();
        }
        else if (gameObject.tag== "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.turretSelected == true)
        {
            CreateSelectingArrows();
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.soldierSelected == true)
        {
            CreateSelectingArrows();
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.mechSelected == true)
        {
            CreateSelectingArrows();
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

        //need an if statement here that controls what happens when a unit moves into a space with another unit. animations need to play and shields need to be broken after which units need to either be destroyed or stay on the map with less shield

        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
        { // if its the first round it spawns in the player's base and brings in the confirm and cancel buttons
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            turnFlowManager.playerBasePlaced = true;
            GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
            newBase.transform.parent = gameObject.transform;
            DestroySelecting("Selecting");
            confirmCancel.anchoredPosition = toSelect;

        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.shipSelected == true) 
            // placing a ship on the space
        {
            GameObject newShip = Instantiate(prefabShip, transform.position, Quaternion.identity);
            newShip.transform.parent = gameObject.transform;
            myButtonController.shipSelected = false;
            DestroySelecting("Selecting");

        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.turretSelected == true) 
            // placing a turret on the space
        {
            GameObject newTurret = Instantiate(prefabTurret, transform.position, Quaternion.identity);
            newTurret.transform.parent = gameObject.transform;
            myButtonController.turretSelected = false;
            DestroySelecting("Selecting");
            confirmCancelRotate.anchoredPosition = toSelect;
            MinimiseTerrain();
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.soldierSelected == true)
        {
            // placing a soldier on the space
            GameObject newSoldier = Instantiate(prefabSoldier, transform.position, Quaternion.identity);
            newSoldier.transform.parent = gameObject.transform;
            myButtonController.soldierSelected = false;
            confirmCancelRotate.anchoredPosition = toSelect;
            DestroySelecting("Selecting");
            MinimiseTerrain();
            
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.mechSelected == true)
        {
            // placing a mech
            GameObject newMech = Instantiate(prefabMech, transform.position, Quaternion.identity);
            newMech.transform.parent = gameObject.transform;
            myButtonController.mechSelected = false;
            confirmCancelRotate.anchoredPosition = toSelect;
            DestroySelecting("Selecting");
        }
    }

    void MinimiseTerrain() // method that was written to combat a bug that occured when a base was placed and cancelled but the mouse was already hovering on a tile and base placed the tile wouldnt play the minimise animation
    {   // this method ensures the animation will always be played
        //keeps throwing warnings about not finding the animations because it ends up checking all children for the animation instead of the terrain only. need to write a fix to correct this. maybe change the tag of the terrain tiles and tell the method to only search those tiles for it
        /*Animator[] myTerrainTileAnimators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator anim in myTerrainTileAnimators)
        {
            anim.Play("terrain minimise"); 
        }*/
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<TerrainScript>())
            {
                child.gameObject.GetComponent<Animator>().Play("terrain minimise");
            }
        }
    }

    void CreateSelectingArrows()
    {
        GameObject selecting = Instantiate(selector, gameObject.transform.position, Quaternion.identity);
        selecting.transform.parent = gameObject.transform;
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