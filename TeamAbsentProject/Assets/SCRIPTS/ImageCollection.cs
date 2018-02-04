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
    private Vector3 _defaultRotation = Quaternion.identity.eulerAngles;

    private int _imagePackIndex;
    private int _imagePackLevel = 20;

    public List<GameObject> ImagePackCollection;

    // Use this for initialization
    void Start ()
    {
        RadiusStatic = Radius;
        ImagePackCollection = new List<GameObject>();
        _defaultPosition = new Vector3(Radius, 0, 0);
        GenerateImagePack();
        RotationInterval = 10;
        InvokeRepeating("GenerateImagePack", 2.0f, 1f);
    }
	
	// Update is called once per frame
	void Update () {


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
            currentImagePack.GetComponent<ImagePack>().StartRotation();
            currentImagePack.transform.Rotate(_defaultPosition);
            if (_imagePackIndex > 200)
                currentImagePack.GetComponent<ImagePack>()._yRotationInterval += 10;
            _imagePackIndex++;
        }
        else
        {
            _defaultRotation.z += -100;
            _imagePackLevel += 20;
        }
    }

}
