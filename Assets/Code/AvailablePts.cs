using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailablePts : MonoBehaviour
{
    public static int points = 100;
    public Text txt;
    public bool UseOne()
    {
        bool success = false;
        if (points > 0)
        {
            points--;
            success = true;
            txt.text = "Spending Points : " + (points + "").PadLeft(3, '0');
        }
        return success;
    }
    // Use this for initialization
    void Start()
    {
        txt.text = "Spending Points : " + (points+"").PadLeft(3,'0');
    }

    // Update is called once per frame
    void Update()
    {

    }
}
