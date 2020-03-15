using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTracker : MonoBehaviour {
    public StatBlock sb;
    private int Width;
    // Use this for initialization
    void Start () {
        sb = GetComponent<StatBlock>();
        Width = sb.Width;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    int GetPointsAtLocation(float x,float y, float z)
    {
        return GetPointsAtLocation("("+x + ", " + y + ", " + z+")");
    }
    int GetPointsAtLocation(string cubename)
    {
        //get position - arrivalorder
        return 0;
    }



}
