using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCollection : MonoBehaviour {

    public static int FrameCount = 0;
    public int CurrentFrameCount = FrameCount;
    public int PackCount = 0;
    public static float RadiusStatic = 10;
    public float Radius = 100;
    public int TotalImagePacks = 1;

    public float RotationInterval;
    private Vector3 _defaultPosition;
    private Vector3 _defaultRotation;

    private int _imagePackIndex;
    private int _imagePackLevel = 20;

    public List<GameObject> ImagePackCollection;

    public bool isGeneratingSphere;


    public bool IsRotating { get; set; }
    // Use this for initialization
    void Start ()
    {
        RadiusStatic = Radius;
        ImagePackCollection = new List<GameObject>();
        _defaultRotation = new Vector3(0,0,70);
        _defaultPosition = new Vector3(Radius, 0, 0);
        RotationInterval = -10;
        isGeneratingSphere = true;
    }
	
	// Update is called once per frame
	void Update ()
	{

        if(isGeneratingSphere)
            GenerateImagePack();

	    RotateToLocation();
    }


    void GenerateImagePack()
    {
        if (_imagePackIndex < _imagePackLevel)
        {
            ImagePackCollection.Add(new GameObject("ImagePack" + PackCount));
            GameObject currentImagePack = ImagePackCollection[_imagePackIndex];
            currentImagePack.AddComponent<ImagePack>();
            currentImagePack.transform.parent = transform;
            currentImagePack.transform.localPosition = _defaultPosition;
            currentImagePack.GetComponent<ImagePack>().IntitializeFrames();
            currentImagePack.GetComponent<ImagePack>().GenerateNewFrames();
            _defaultRotation.y += 15;
            currentImagePack.transform.rotation = Quaternion.Euler(_defaultRotation);
            _imagePackIndex++;
        }
        else
        {
            _defaultRotation.z += RotationInterval;
            _imagePackLevel += 20;

            if (_defaultRotation.z < -70)
            {
                _defaultRotation.z = 70;
                isGeneratingSphere = false;
                StartRotation();
                
            }
        }
    }

    public void RotateToLocation()
    {
        if (IsRotating)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
        }
    }

    public void StartRotation()
    {
        IsRotating = true;
    }

}
