using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private RectTransform myRect;

	// Use this for initialization
	void Start ()
    {
        myRect = gameObject.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update ()
    {
        myRect.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	}
}
