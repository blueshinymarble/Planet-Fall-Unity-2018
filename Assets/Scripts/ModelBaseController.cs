using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBaseController : MonoBehaviour
{
    public Material shield;
    public Material vulnerable;
    public Material damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColliderTags()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponentInChildren<MeshRenderer>().material != null)
            {
                Debug.Log("log");
            }
            /*if (child.gameObject.GetComponentInChildren<MeshRenderer>().material == shield)
            {
                child.gameObject.tag = "Shield";
            }
            else if (child.gameObject.GetComponentInChildren<MeshRenderer>().material == vulnerable)
            {
                child.gameObject.tag = "Vulnerable";
            }
            else if (child.gameObject.GetComponentInChildren<MeshRenderer>().material == damage)
            {
                child.gameObject.tag = "Move Attack";
            }*/
        }
    }
}
