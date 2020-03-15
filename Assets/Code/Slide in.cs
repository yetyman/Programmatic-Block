using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidein : MonoBehaviour {

    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;
	// Use this for initialization
	void Start () {
        transform.Translate(xOffset, yOffset, zOffset, Space.Self);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
