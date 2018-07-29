using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Soldier : MonoBehaviour
{
    public Transform[] thisUnitSlots;
    public GameObject[] Pips;

    private List<GameObject> pipsList = new List<GameObject>();
    private ModelBaseController baseController;
    private RectTransform confirmCancelRotate;
    private ButtonController buttonController;
    //bool will move
    //bool will shoot and remain on my space
    //bool will shoot and move into that space
    //bool will rotate
    //bool will take damage 
    //bool will be destroyed
    //int shield strength that starts at 1

	// Use this for initialization
	void Start ()
    {
        gameObject.tag = "Just Placed";
        gameObject.transform.parent.tag = "Occupied";
        buttonController = GameObject.Find("Button Controller").GetComponent<ButtonController>();
        confirmCancelRotate = GameObject.Find("Confirm Cancel Rotate").GetComponent<RectTransform>();
        baseController = GetComponentInChildren<ModelBaseController>();
        ChoosePips();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        GameObject[] resetTags = GameObject.FindGameObjectsWithTag("Just Placed");
        foreach(GameObject toChange in resetTags)
        {
            toChange.tag = "No tag";
        }
        buttonController.rotationToRevertTo = gameObject.transform.rotation;
        confirmCancelRotate.anchoredPosition = new Vector3(960, 270);
        if (gameObject.tag != "Just Selected")
        {
            gameObject.tag = "Just Selected";
        }
        //bring the move rotate cancel ui menu into view
        //if the space in front of the moce arrow is a legal spacec then allow the unit to move to that space.
        //if move is clicked play the animated arrow
        //possibly change the color of the hex in front of the unit to signify it can move there
        //if rotate is clicked rotate the unit
    }

    public void ChangeParentTagPlayAnim()
    // changes the tag of its parent back to legal space and plays the animation to bring back the terrain
    {
        Transform myParent = gameObject.transform.parent;
        myParent.tag = "Legal Space";
        foreach (Transform child in myParent)
        {
            if (child.gameObject.GetComponent<TerrainScript>())
            {
                child.GetComponent<Animator>().Play("terrain maximise");
            }
        }
    }

    void ChoosePips()
    {
        pipsList = Pips.ToList();
        int pipCount = 3;
        while (pipCount > 0)  // when the soldier spawns in he places pips on random edges of his base. this will change as we figure out a more permanent solution to pip spawning but as of now pip spawning is working as intended
        {
            Transform selectedSlot = thisUnitSlots[Random.Range(0, thisUnitSlots.Length - 1)];
            if (selectedSlot.childCount < 2)
            {
                GameObject randomPip = pipsList[Random.Range(0, pipCount - 1)];
                GameObject spawnedPip = Instantiate(randomPip, new Vector3(selectedSlot.position.x, selectedSlot.position.y + 0.5f, selectedSlot.position.z), Quaternion.identity);
                spawnedPip.transform.parent = selectedSlot;
                pipsList.Remove(randomPip);
                pipCount--;
            }
        }
    }
}
