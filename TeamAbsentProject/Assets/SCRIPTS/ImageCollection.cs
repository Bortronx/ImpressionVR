using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImageHandler))]
public class ImageCollection : MonoBehaviour
{
    public float Radius = 100;
    public int MaxImageSize = 50;

    public float ImageThickness = 0.02f;
    public float FrameThickness = 0.02f;
    public float FrameOutlineProportion = 0.10f;

    [SerializeField]
    private float _rotationInterval;
    private Vector3 _defaultRotation;
    
    private int _imagePackIndex;
    private int _imagePacksInARevolution = 20;

    private ImageHandler _handler;
    private List<GameObject> _imagePackCollection;

    private delegate void CollectionProcessing();
    private CollectionProcessing _process;

    // Use this for initialization
    void Start ()
    {
        _handler = GetComponent<ImageHandler>();
        _imagePackCollection = new List<GameObject>();
        _defaultRotation = new Vector3(0,0,70);
        _rotationInterval = -10;
        _process = GenerateImagePack;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    _process();
    }


    private void GenerateImagePack()
    {
        if (_imagePackIndex < _imagePacksInARevolution)
        {
            _imagePackCollection.Add(new GameObject("ImagePack" + _imagePackIndex));
            GameObject currentImagePack = _imagePackCollection[_imagePackCollection.Count-1];
            currentImagePack.AddComponent<ImagePack>();
            currentImagePack.GetComponent<ImagePack>().ImageCollection = this;
            currentImagePack.GetComponent<ImagePack>().Handler = _handler;
            currentImagePack.transform.parent = transform;
            currentImagePack.transform.localPosition = Vector3.zero;
            currentImagePack.GetComponent<ImagePack>().IntitializeFrames();
            currentImagePack.GetComponent<ImagePack>().GenerateNewFrames();
            _defaultRotation.y += 15;
            currentImagePack.transform.rotation = Quaternion.Euler(_defaultRotation);
            _imagePackIndex++;
        }
        else
        {
            _defaultRotation.z += _rotationInterval;
            _imagePacksInARevolution += 20;

            if (_defaultRotation.z < -70)
            {
                _defaultRotation.z = 70;
                _process = RotateImagePacks;
                
            }
        }
    }

    public void RotateImagePacks()
    {
            transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
    }

    public int GetTotalFrameCount()
    {
        int totalFrames = 0;
        foreach (GameObject pack in _imagePackCollection)
        {
            totalFrames += pack.GetComponent<ImagePack>().Frame.Count;
        }
        return totalFrames;
    }
}
