using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) {
        Debug_Log.Call_WriteLog("OnTriggerEnter");
        //gameObject.GetComponent<Renderer>().material.color = Color.blue;
        gameObject.GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit(Collider c)
    {
        Debug_Log.Call_WriteLog("OnTriggerExit");
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
