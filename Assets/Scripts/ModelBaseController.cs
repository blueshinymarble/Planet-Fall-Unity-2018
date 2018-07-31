using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBaseController : MonoBehaviour
{
    private GameObject moveAttack;

    // Use this for initialization
    void Start()
    {
        SetCheckerToDamage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCheckerToDamage()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.tag == "Move Attack")
            {
                moveAttack = child.gameObject;
            }
        }
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<CheckerScript>())
            {
                Debug.Log("here");
                child.parent = moveAttack.transform;
            } 
        }
    }
}
