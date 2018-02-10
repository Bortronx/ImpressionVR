using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
public class Frame : MonoBehaviour
{
    public float ImageThickness = 0.02f;
    public float FrameThickness = 0.02f;
    public float FrameOutlineProportion = 0.10f;

    public string Name;
    public bool IsInverted;

    public GameObject FrameBody;
    public GameObject Image;
    public ImagePack ImagePack;

    private static int _frameImageIndex;

    private Quaternion _imagePackParentRotation;

    private Material _imageMat;
    private Texture _imageTexture;

    private Vector3 _newImageScale;
    private Vector3 _newFrameBodyScale;
    private Vector3 _imageOffset;

    public string Texture;

    public List<string> Tags { get; set; }


    void Awake()
    {
        _imageOffset = (IsInverted) ? new Vector3(ImageThickness, 0, 0) :
                                        new Vector3(-ImageThickness, 0, 0);
    }

    // Use this for initialization
	void Start ()
	{
	    _imagePackParentRotation = transform.parent.transform.rotation;
        ShowNewImage();
	    IntitializeImage();
	    IntitalizeFrameBody();
    }
	
	// Update is called once per frame
	void Update () {


    }

    public void ShowNewImage()
    {
        Texture = ImageHandler.ImageFiles[_frameImageIndex];
        Texture = Texture.Replace(".jpg", "");
        if (_frameImageIndex >= ImageHandler.ImageFiles.Length - 1)
            _frameImageIndex = 0;
        _frameImageIndex++;

        _imageTexture = Resources.Load(Texture) as Texture;
    }

    private void IntitializeImage()
    {
        _newImageScale = new Vector3(ImageThickness, 1, 1);
        Image = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Image.transform.parent = transform;
        Image.transform.localPosition = _imageOffset;
        Image.transform.rotation = _imagePackParentRotation;
        Image.transform.localScale = _newImageScale;

        _imageMat = Image.GetComponent<Renderer>().material;
        _imageMat.mainTexture = _imageTexture;

        _newImageScale = Image.transform.localScale;
        _newImageScale.x = ImageThickness;

        ScaleImage();
    }

    private void IntitalizeFrameBody()
    {
        _newFrameBodyScale = new Vector3(FrameThickness, 1, 1);
        FrameBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        FrameBody.transform.parent = transform;
        FrameBody.transform.localPosition = Vector3.zero;
        FrameBody.transform.rotation = _imagePackParentRotation;  
        FrameBody.transform.localScale = _newFrameBodyScale;

        ScaleFrameBody();
    }

    public void ScaleImage()
    {
        _newImageScale.y = _imageTexture.height;
        _newImageScale.z = _imageTexture.width;

        while(_newImageScale.y > 50)
        _newImageScale.y /= 10;

        while (_newImageScale.z > 50)
        _newImageScale.z /= 10;

        Image.transform.localScale = _newImageScale;
    }

    public void ScaleFrameBody()
    {
        _newFrameBodyScale.y = _newImageScale.y + (_newImageScale.y * FrameOutlineProportion / (_newImageScale.z / _newImageScale.y));
        _newFrameBodyScale.z = _newImageScale.z + (_newImageScale.z * (FrameOutlineProportion / (_newImageScale.z /_newImageScale.y)));

        FrameBody.transform.localScale = _newFrameBodyScale;
    }

    public void SelectFrame()
    {

    }
    public void GrabFrame(Transform controllerTransform)
    {
        MoveTowardLocaiton(controllerTransform.position);
    }

    // String Parsing
    void ParseTextureNameString(string textureName)
    {
        char[] delimiterCharacters = { ',' };

        string[] word = textureName.Split(delimiterCharacters);

        Name = word[0];

        for (int i = 1; i < word.Length; i++)
        {
            Tags.Add(word[i]);
        }
    }

    void MoveTowardLocaiton(Vector3 targetPosiiton)
    {
        Vector3 _direction = transform.position - targetPosiiton;
        transform.Translate(_direction.normalized * Time.deltaTime);
    }

    void InvertFrame()
    {
        transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
    }
    // Strech the image and set its scale then set the scale of the frame to 0.5 devided by (ImageTexture scale devided by 2)

}
