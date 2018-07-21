using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        gameObject.tag = "Just Placed Base"; // just placed tag for the confirm and cancel buttons
        gameObject.transform.parent.tag = "Occupied"; // set parent tile to occupied so that no control points an be spawned on 	
	}

    public void ChangeParentTagPlayAnim() 
        // changes the tag of its parent back to legal space and plays the animation to bring back the terrain
    {
        Transform myParent = gameObject.transform.parent;
        myParent.tag = "Legal Space";
        foreach (Transform child in myParent)
        {
            if (child.GetComponent<Animator>())
            {
                child.GetComponent<Animator>().Play("terrain maximise");
            }
        }
    }
}
