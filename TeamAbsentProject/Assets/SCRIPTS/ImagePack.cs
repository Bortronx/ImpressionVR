using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImagePack : MonoBehaviour
{
    public ImageCollection ImageCollection;
    public ImageHandler Handler;

    private float LifeStartTime;
    public List<GameObject> Frame;

    //FramesInAPack
    private int _numOfFrames = 10;

    // Use this for initialization
	void Start ()
	{
        
	    _numOfFrames = (int)(ImageCollection.Radius * 2 * Mathf.PI) / ImageCollection.MaxImageSize/2;
	    //IntitializeFrames();
	}
	
	// Update is called once per frame
	void Update () {

        //if(!(FrameCollection.Count > 0)) return;
	    //if ((Time.timeSinceLevelLoad - LifeStartTime) > ImagePackDuration)
	        //FadeAway();
	}



    void FadeAway()
    {
        // Change frame scale and radius from player.
        //if (FrameCollection[0].transform.localScale.y < FrameYScale)
        //{
        //    Destroy(gameObject);
        //}
    }

    public Vector3 NewRandomPosition()
    {
        return new Vector3(Random.Range(-10, 10) + ImageCollection.Radius, Random.Range(-10, 10), Random.Range(-10, 10));
    }

    public void GenerateNewFrames()
    {
        for (int i = 0; i < _numOfFrames; i++)
        {
            Frame.Add(new GameObject("Frame" + Frame.Count));
            GameObject currentFrame = Frame[Frame.Count -1];
            currentFrame.AddComponent<Frame>();
            currentFrame.GetComponent<Frame>().ImageThickness = ImageCollection.ImageThickness;
            currentFrame.GetComponent<Frame>().FrameThickness = ImageCollection.FrameThickness;
            currentFrame.GetComponent<Frame>().FrameOutlineProportion = ImageCollection.FrameOutlineProportion;
            currentFrame.GetComponent<Frame>().ImagePack = this;
            currentFrame.transform.parent = transform;
            currentFrame.transform.localPosition += NewRandomPosition();
            currentFrame.transform.localRotation = transform.rotation;
            Handler.Frames.Add(currentFrame);
        }
    }

    public void IntitializeFrames()
    {
        ImageCollection = transform.parent.GetComponent<ImageCollection>();
        Handler = transform.parent.GetComponent<ImageHandler>();
        Frame = new List<GameObject>();
        LifeStartTime = Time.timeSinceLevelLoad;
    }
}
