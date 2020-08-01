using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform bolltransform;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - bolltransform.position;
        //Debug_Log.Call_WriteLog("11111111111");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + bolltransform.position;
    }
}
