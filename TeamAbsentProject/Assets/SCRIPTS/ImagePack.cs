using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImagePack : MonoBehaviour
{
    public float LifeStartTime;
    public float ImagePackDuration = 10;

    public List<GameObject> FrameCollection;
    public float FrameYScale;

    public int NumOfFrames = 10;

    private Vector3 _newPosition = Vector3.zero;

    public ImageCollection ImageCollection;
    public ImageHandler Handler;

    public bool IsRelated;

    // Use this for initialization
	void Start ()
	{
	    //IntitializeFrames();
	}
	
	// Update is called once per frame
	void Update () {

        if(!(FrameCollection.Count > 0)) return;
	    if ((Time.timeSinceLevelLoad - LifeStartTime) > ImagePackDuration)
	        FadeAway();
	}

    void FadeAway()
    {
        // Change frame scale and radius from player.
        if (FrameCollection[0].transform.localScale.y < FrameYScale)
        {
            Destroy(gameObject);
        }
    }

    public void GenerateNewPosition()
    {
        _newPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }

    public void GenerateNewFrames()
    {
        for (int i = 0; i < NumOfFrames; i++)
        {
            FrameCollection.Add(new GameObject("Frame" + ImageCollection.FrameCount));
            GameObject currentFrame = FrameCollection[i];
            currentFrame.AddComponent<Frame>();
            currentFrame.transform.parent = transform;
            currentFrame.transform.localPosition += _newPosition;
            currentFrame.transform.localRotation = transform.rotation;
            Handler.Frames.Add(currentFrame);
            GenerateNewPosition();
            ImageCollection.FrameCount++;
        }
    }

    public void IntitializeFrames()
    {
        ImageCollection = transform.parent.GetComponent<ImageCollection>();
        Handler = transform.parent.GetComponent<ImageHandler>();
        FrameCollection = new List<GameObject>();
        LifeStartTime = Time.timeSinceLevelLoad;
    }
}
