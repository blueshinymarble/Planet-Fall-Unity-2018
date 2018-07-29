using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretController : MonoBehaviour
{
    public Transform[] thisUnitSlots;
    public GameObject[] Pips;

    private List<GameObject> pipsList = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        gameObject.tag = "Just Placed";
        gameObject.transform.parent.tag = "Occupied";
        ChoosePips();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeParentTagPlayAnim()
    // changes the tag of its parent back to legal space and plays the animation to bring back the terrain
    {
        Transform myParent = gameObject.transform.parent;
        myParent.tag = "Legal Space";
        foreach (Transform child in myParent)
        {
            if (child.GetComponent<TerrainScript>() != null)
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
