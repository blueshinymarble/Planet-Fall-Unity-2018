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

    // Use this for initialization
    void Start()
    {
        confirmCancel = GameObject.Find("Confirm panel").GetComponent<RectTransform>();
        bloomController = GameObject.Find("Bloom Controller").GetComponent<BloomController>();
        controlTokenCounter = GameObject.Find("Control Token counter").GetComponentInChildren<Text>();
        turnFlowManager = GameObject.Find("Turn Flow Manager").GetComponent<TurnFlowManager>();
        Board board = GameObject.Find("Board").GetComponent<Board>();
        while (gameObject.transform.childCount < 1)
        {
            board.ChooseTile((Board.TerrainTilesEnum)Random.Range(0, 6), transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
        {
            gameObject.GetComponentInChildren<Animator>().Play("terrain minimise");
            GameObject selectingBase = Instantiate(selectingSpace, transform.position, Quaternion.identity);
            selectingBase.transform.parent = gameObject.transform;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.tag != "Hazard" && turnFlowManager.playerBasePlaced == false && turnFlowManager.currentState == TurnFlowManager.State.firstRound)
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
        {
            turnFlowManager.playerBasePlaced = true;
            GameObject newBase = Instantiate(powerPlant, transform.position, Quaternion.identity);
            newBase.transform.parent = gameObject.transform;
            GameObject[] selectings = GameObject.FindGameObjectsWithTag("Selecting");
            foreach (GameObject selecting in selectings)
            {
                Destroy(selecting);
            }
            confirmCancel.anchoredPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        else if (turnFlowManager.currentState == TurnFlowManager.State.placeBloomAndCounters && bloomController.tokenPlaced == false && gameObject.tag == "Control Point")
        {
            bloomController.PlaceBloomToken();
        }
        else if (turnFlowManager.currentState == TurnFlowManager.State.placeCounters && bloomController.tokenPlaced == false && gameObject.tag == "Control Point")
        {
            bloomController.PlaceBloomToken();
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