using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlock : MonoBehaviour {

    public List<TileMove>[,,] blockPositions; //kill me now. that datastructure

    GameObject[] uSides = new GameObject[4];
    GameObject[] uCorners = new GameObject[8];
    GameObject[] uEdges = new GameObject[12];
    Quaternion[] transforms = new Quaternion[6];
    GameObject[][,] sides = new GameObject[6][,];
    GameObject[] sideCube = new GameObject[6];

    public int Width = 2;
    public GameObject Edge;
    public GameObject Corner;
    public GameObject SideCube0;
    public GameObject SideCube1;
    public GameObject SideCube2;
    public GameObject SideCube3;
    public GameObject SideCube4;
    public GameObject SideCube5;
    // Use this for initialization  
    void Start () {

        blockPositions = new List<TileMove>[Width+2,Width+2,Width+2];
        for (int x = 0; x < Width + 2; x++)
            for (int y = 0; y < Width + 2; y++)
                for (int z = 0; z < Width + 2; z++)
                    blockPositions[x, y, z] = new List<TileMove>();


        int cornerCount = 0;
        int edgecount = 0;
        transforms[0] = new Quaternion();
        transforms[1] = Quaternion.AngleAxis(-90, Vector3.right);
        transforms[2] = Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.right) ;
        transforms[3] = Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.right);
        transforms[4] = Quaternion.AngleAxis(270, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.right);
        transforms[5] = Quaternion.AngleAxis(180, Vector3.right) * Quaternion.AngleAxis(-90, Vector3.up);
        sideCube[0] = SideCube0;
        sideCube[1] = SideCube1;
        sideCube[2] = SideCube2;
        sideCube[3] = SideCube3;
        sideCube[4] = SideCube4;
        sideCube[5] = SideCube5;
        for (int i = 0; i < sides.Length; i++)
        {
            GameObject tmp4 = new GameObject("Side");
            tmp4.transform.parent = transform;
            tmp4.transform.localRotation = transforms[i];
            sides[i] = new GameObject[Width, Width];
            for (int g = 0; g < sides[i].GetLength(0); g++) {
                for (int h = 0; h < sides[i].GetLength(1); h++) {
                    sides[i][g, h]= Instantiate(sideCube[i], tmp4.transform, false);
                    Vector3 translate = new Vector3(g - (Width - 1) / 2f, (Width + 1) / 2f, h - (Width - 1) / 2f);
                    sides[i][g, h].transform.parent = tmp4.transform;
                    sides[i][g, h].transform.Translate(translate);
                    sides[i][g, h].name = translate.ToString();//tmp4.transform.localRotation.ToString() + "," + 
                }
            }
        }
        for (int i = 0; i < uSides.Length; i++)
        {
            GameObject tmp3 = new GameObject("Edges");
            

            uSides[i] = new GameObject("U"+i);


            for (int u = 0; u<uSides.Length-1; u++)
            {
                if (u > 0)
                {
                    //corners
                    GameObject tmp = new GameObject("Corner");
                    tmp.transform.parent = uSides[i].transform;
                    tmp.transform.localEulerAngles = Vector3.zero;
                    tmp.transform.Rotate(0, 90 * (u - 1), 0);

                    uCorners[cornerCount] = Instantiate(Corner, tmp.transform, false);
                    uCorners[cornerCount].name = "C" + cornerCount;
                    uCorners[cornerCount].transform.Translate((Width+1)/2f, 0, (Width + 1) / 2f);
                    cornerCount++;
                }

                //sides
                GameObject tmp2 = new GameObject("Edge");

                tmp2.transform.parent = uSides[i].transform;
                tmp2.transform.localEulerAngles = Vector3.zero;
                tmp2.transform.Rotate(0, 90 * u, 0);

                uEdges[edgecount] = Instantiate(Edge, tmp2.transform, false);
                uEdges[edgecount].name = "E" + edgecount;
                uEdges[edgecount].transform.localScale += new Vector3(Width - 1, 0, 0);
                uEdges[edgecount].transform.Translate(0, 0, (Width + 1) / 2f);
                edgecount++;
            }

            uSides[i].transform.parent = tmp3.transform;
            uSides[i].transform.Translate(0, (Width + 1) / 2f, 0);

            tmp3.transform.parent = transform;
            tmp3.transform.localEulerAngles = Vector3.zero;
            tmp3.transform.transform.Rotate(0, 0, 90 * i);
        }

        transform.localScale = new Vector3(6f / (Width + 2), 6f / (Width + 2), 6f / (Width + 2));
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public float RecordBlockPushed(TileMove tile)
    {
        
        //centered point
        var cPt = this.transform.InverseTransformPoint(tile.transform.parent.TransformPoint(tile.GoalPosition));
        //zero indexed point

        var absx = Mathf.Abs(cPt.x * 100).Round();//these values should only ever be increments of .5, any further decimal place would be floating point error. need to compare them. this is more reliable than approximately()
        var absy = Mathf.Abs(cPt.y * 100).Round();
        var absz = Mathf.Abs(cPt.z * 100).Round();

        var ziPt = new Vector3(cPt.x + (Width + 1) / 2f, cPt.y + (Width + 1) / 2f, cPt.z + (Width + 1) / 2f);

        var rewardedPoints = 0f;

        
        //add 3-count for corners, 2-count for sides, 1 for nonintersecting positions, 6-count for center pieces if they exist
        //possibly plus the count of moves so far(occurrences in the table)

        //we should be starting out looking at a cube which sits in the -z range and moves in the positive z range
        if (absx + absy + absz == 0)
            //center +6 - count
            rewardedPoints = 6 - blockPositions[ziPt.x.Round(), ziPt.y.Round(), ziPt.z.Round()].Count;
        else if (absx == absy && absy == absz)
            //corner +3 - count
            rewardedPoints = 3 - blockPositions[ziPt.x.Round(), ziPt.y.Round(), ziPt.z.Round()].Count;
        else if ((absx == absy || absy == absz || absx == absz)
            && Mathf.Abs(Mathf.Round(tile.GoalPosition.x * 100)) != Mathf.Abs(Mathf.Round(tile.GoalPosition.z * 100)))
            //side +2 - count
            rewardedPoints = 2 - blockPositions[ziPt.x.Round(), ziPt.y.Round(), ziPt.z.Round()].Count;
        else
            //non intersecting +1
            rewardedPoints = 1;

        blockPositions[ziPt.x.Round(), ziPt.y.Round(), ziPt.z.Round()].Add(tile);


        return rewardedPoints;
    }

   


}
