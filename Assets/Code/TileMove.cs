using Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    private AvailablePts PointsUsable;
    private EarnedPts PointsEarned;

    private StatBlock SB;
    public float DistancePerMove;
    public float TimeSpan = 2;//seconds
    public float Threshold = .001f;
    public int moves;
    public float points = 0;
    public float localDepth;

    private AudioSource Sfx;

    public Vector3 GoalPosition { get { return new Vector3(transform.localPosition.x, localDepth, transform.localPosition.z); } set { } }
    // Use this for initialization
    void Start()
    {
        GoalLocalY = transform.localPosition.y;


        List<float> vals = new List<float>(new float[] { Mathf.Abs(transform.localPosition.x), Mathf.Abs(transform.localPosition.y), Mathf.Abs(transform.localPosition.z) });
        vals.Sort();
        vals.Reverse();
        moves = Mathf.Abs(Mathf.RoundToInt(vals[0] - vals[1]));
        //Debug.Log(moves + "-" + vals[0] + "-" + vals[1] + "-" + vals[2]);
        SB = GetComponentInParent<StatBlock>();
        DistancePerMove = transform.localPosition.y / (SB.Width+1);
        localDepth = (SB.Width + 1) / 2f;
        PointsUsable = SB.GetComponent<AvailablePts>();
        PointsEarned = SB.GetComponent<EarnedPts>();
        Sfx = GetComponent<AudioSource>();
        
    }

    public float GoalLocalY = 0;
    public float TimeTaken = 0;
    // Update is called once per frame
    void Update()
    {
        float height = transform.localPosition.y;
        if (height > GoalLocalY + Threshold)
        {
            float distance = DistancePerMove * Mathf.Sqrt((height - GoalLocalY) / DistancePerMove) / (TimeSpan) * Time.smoothDeltaTime;
            //float time = Mathf.Sqrt((TimeSpan -TimeTaken) / TimeSpan)/(TimeSpan*1000)*Time.smoothDeltaTime;
            transform.Translate(0, -distance, 0, Space.Self);
        }

    }


    public void Move()
    {
        Debug.Log("checking moves");
        if (moves > 0 && PointsUsable.UseOne())
        {
            Debug.Log("should move");

            moves--;
            EdgeShine e = GetComponentInChildren<EdgeShine>();
            if (e != null)
            {
                e.Shine(TimeSpan);
            }

            if (Sfx != null)
            {
                Sfx.pitch = Sfx.clip.length / TimeSpan;
                Sfx.Play();
            }

            GoalLocalY -= DistancePerMove;
            localDepth -= 1;

            var pointsTmp = GetComponentInParent<StatBlock>().RecordBlockPushed(this);
            if (PointsEarned != null)
                PointsEarned.Add(pointsTmp);

            points += pointsTmp;
        }
    }
}
