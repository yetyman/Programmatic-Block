using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    

    public float rotationSpeed = 20;
	// Update is called once per frame
	void Update () {
        float xRotate = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        float yRotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(xRotate, yRotate, 0, Space.World);
    }
}
