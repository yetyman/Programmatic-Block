using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnedPts : MonoBehaviour {

    public static float points = 0;
    public Text txt;
    public void Add(float pointsEarned)
    {
        points+=pointsEarned;
        txt.text = "Earned Points : " + (points + "").PadLeft(3, '0');
    }
    // Use this for initialization
    void Start()
    {
        txt.text = "Earned Points : " + (points + "").PadLeft(3, '0');
    }

    // Update is called once per frame
    void Update()
    {

    }
}
