using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {
    private float MaxHeight;
    private float MinHeight;
    private float StartHeight;
    // Use this for initialization
    void Start () {
        CycleTime = CycleTime;
        float pos = transform.position.y;
        StartHeight = pos;
        MaxHeight = pos + height / 2;
        MinHeight = pos - height / 2;
    }
    public float height =5;
    public float Height { get { return height; }
        set {
            height = value;
            MaxHeight = StartHeight + height / 2;
            MinHeight = StartHeight - height / 2;
        } }
    public float CycleTime { get { return CYCLETIME; } set { Sum = ApproachZero(); CYCLETIME = value; } }
    public float Sum = 0;//for cos wave
    public float CYCLETIME  =5;
    public float CyclePos = 0;
    public float CurveModifier(float percentage)//smoother
    {
        float increment = Mathf.Cos(percentage * 2 * Mathf.PI);
        float multiplier = (increment > 0) ? 1 : -1;
        return multiplier * Mathf.Sqrt(Mathf.Abs(increment));
    }
    //public float CurveModifier(float percentage)//cos
    //{
    //    float increment = Mathf.Cos(percentage * 2 * Mathf.PI);
    //    float multiplier = (increment > 0) ? 1 : -1;
    //    return multiplier * Mathf.Sqrt(Mathf.Abs(increment));
    //}
    //public float CurveModifier(float percentage)// sharp
    //{
    //    return (percentage<.25||percentage>=.75)?1f:-1f;
    //}
    public float ApproachZero()
    {
        float iterations = 1000000;
        float sum = 0;
        int pos = 0;
        while (pos++ < iterations)
        {
            sum += Mathf.Abs(CurveModifier(pos/iterations)/iterations);
        }
        return sum;
    }
    // Update is called once per frame
    void Update()
    {
        CyclePos += Time.deltaTime;
        if (CyclePos > CycleTime) CyclePos = 0;
        float move = (Height * 2) * CurveModifier(CyclePos/CycleTime)/(CycleTime/Time.deltaTime)/Sum;
        //Debug.Log(MinHeight+"    "+ move+"    "+MaxHeight);
        //if (Within(transform.position.y+move))
            transform.Translate(0, move, 0);

    }
    private bool Within(float height)
    {
        return height <= MaxHeight && height >= MinHeight;
    }

}
