using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtCamera : MonoBehaviour {
    public Vector3 Offset;
    static Vector3 CameraPos;
    private static GameObject TargetCamera;


    // Use this for initialization
    void Start () {
        TargetCamera = GameObject.Find("Main Camera");
        CameraPos = TargetCamera.transform.position;
        Vector3 initial = transform.rotation.eulerAngles;
        transform.LookAt(CameraPos);
        Vector3 newRotation = transform.rotation.eulerAngles;
        Offset = newRotation - initial;
        transform.Rotate(Offset);

    }

    // Update is called once per frame
    void Update () {
        CameraPos = TargetCamera.transform.position;
        if (TargetCamera != null)
        {
            transform.LookAt(CameraPos);
            transform.Rotate(Offset);
        }

	}

}
