using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPointerInput : MonoBehaviour {

    private SteamVR_LaserPointer laserPointer;
    private GameObject GrabbedFrame;

    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
    }

    // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(GrabbedFrame);
            GrabbedFrame = collision.transform.parent.gameObject;
        GrabbedFrame.GetComponent<Frame>().GrabFrame(transform);

    }



}
