using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickClick : MonoBehaviour {
    // Use this for initialization
	void Start () {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GetComponentInParent<TileMove>().Move();
        }
    }
}
