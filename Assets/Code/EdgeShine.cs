using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeShine : MonoBehaviour {

    public ParticleSystem EdgeShineEffect;
    private List<Vector3> Positions = new List<Vector3>();
    private List<Quaternion> Rotations = new List<Quaternion>();
    // Use this for initialization
    void Start () {
        //populate transform list
        Vector3 dimen = transform.localScale;
        //for the four sides, make 4 transforms. lots of guess and check me thinks
        //glow is y direction across x plane
        for (int i = 0; i < 4; i++)
        {
            float x = (i==0||i==2)? 0: (i==1)?1:-1;
            float z = (i==1||i==3)? 0: (i==2)?1:-1;
            Vector3 pos = new Vector3(x, 3f, z);
            Quaternion rotation = new Quaternion(0, i * 90, 180, 1);
            Positions.Add(pos);
            Rotations.Add(rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < shines.Count; i++)
        {
            if (!shines[i].isPlaying)
            {
                Destroy(shines[i]);
                shines.RemoveAt(i--);
            }
        }
	}
    float remainingLife = 0;
    List<ParticleSystem> shines = new List<ParticleSystem>();
    public void Shine(float timeSpan)
    {

        remainingLife += timeSpan;
        if (shines.Count == 0)
        {
            for (int i = 0; i < Rotations.Count; i++)
            {

                ParticleSystem anEdge = Instantiate<ParticleSystem>(EdgeShineEffect, transform);
                var main = anEdge.main;
                anEdge.Stop();
                main.duration = timeSpan;
                main.loop = false;
                Debug.Log("InstantiatedLight" + Rotations[i]);
                anEdge.transform.Rotate(0,Rotations[i].y,0);
                anEdge.transform.localPosition = Positions[i];
                anEdge.transform.parent = transform;

                anEdge.Play();
                shines.Add(anEdge);
                //instantiate edge shines

            }
        }
        else
        {
            for (int i = 0; i < shines.Count; i++)
            {
                Debug.Log("ContinuedLight");
                shines[i].Clear();
                var main = shines[i].main;
                main.startDelay = 0;
                main.prewarm = true;
                shines[i].Play();
            }
        }
    }
}
