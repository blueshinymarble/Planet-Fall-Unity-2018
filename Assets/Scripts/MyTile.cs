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

    // Use this for initialization
    void Start()
    {
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
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
        { // if its the first round it spawns in the player's base and brings in the confirm and cancel buttons
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            turnFlowManager.playerBasePlaced = true;
            GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
            newBase.transform.parent = gameObject.transform;
            DestroySelecting("Selecting");

            if (Input.mousePosition.x > 380f && Input.mousePosition.x < 1539f && Input.mousePosition.y > 200f)
            {
                confirmCancel.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 10f, 0);
            }
            else
            {
                confirmCancel.anchoredPosition = new Vector3(Input.mousePosition.x, 200f, 0);
            }
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
            if (Input.mousePosition.x > 740f && Input.mousePosition.y > 200f)
            {
                confirmCancelRotate.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 10f, 0);
            }
            else
            {
                confirmCancelRotate.anchoredPosition = new Vector3(Input.mousePosition.x, 200f, 0);
            }
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.soldierSelected == true)
        {
            // placing a soldier on the space
            GameObject newSoldier = Instantiate(prefabSoldier, transform.position, Quaternion.identity);
            newSoldier.transform.parent = gameObject.transform;
            myButtonController.soldierSelected = false;
            DestroySelecting("Selecting");
            
        }
        else if (gameObject.tag == "Legal Space" && turnFlowManager.currentState == TurnFlowManager.State.action && myButtonController.mechSelected == true)
        {
            // placing a mech
            GameObject newMech = Instantiate(prefabMech, transform.position, Quaternion.identity);
            newMech.transform.parent = gameObject.transform;
            myButtonController.mechSelected = false;
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