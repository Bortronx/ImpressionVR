using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImagePack : MonoBehaviour
{
    public float LifeStartTime;
    public float ImagePackDuration = 10;

    public List<GameObject> FrameCollection;
    public float FrameYScale;

    private Transform _imagePackOrigin;
    private int _imagePackRadius;

    public float _yRotationInterval;
    public float _xRotationInterval;

    private float _yFrameRotation;
    private float _xFrameRotation;

    public int _numOfFrames = 10;

    private Vector3 _newPosition = Vector3.zero;

    public ImageHandler handler;

    public bool IsRelated;
    private bool _isRotating;

    public bool IsRotating
    {
        get { return _isRotating; }
        set { _isRotating = value; }
    }

    // Use this for initialization
	void Start ()
	{
	    handler = transform.parent.GetComponent<ImageHandler>();
	    FrameCollection = new List<GameObject>();
        GenerateNewFrames();
	    LifeStartTime = Time.timeSinceLevelLoad;
	    _xRotationInterval = 40;
	    _yRotationInterval = 40;
	}
	
	// Update is called once per frame
	void Update () {

        if(!(FrameCollection.Count > 0)) return;
	    if ((Time.timeSinceLevelLoad - LifeStartTime) > ImagePackDuration)
	        FadeAway();

        RotateToLocation();
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

    void GenerateNewFrames()
    {
        for (int i = 0; i < _numOfFrames; i++)
        {
            FrameCollection.Add(new GameObject("Frame" + ImageCollection.FrameCount));
            GameObject currentFrame = FrameCollection[i];
            currentFrame.AddComponent<Frame>();
            currentFrame.transform.parent = transform;
            currentFrame.SetActive(true);
            currentFrame.transform.localPosition += _newPosition;
            handler.Frames.Add(currentFrame);
            GenerateNewPosition();
            ImageCollection.FrameCount++;
        }
    }

    public void RotateToLocation()
    {
        if (IsRotating)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 15, Space.World);
        }
    }

    public void StartRotation()
    {
        IsRotating = true;
    }
}
