using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
public class Frame : MonoBehaviour
{
    public const float DefaultWidth = 1f;
    public const float DefaultHeight = 1f;

    private const float ImageThickness = 0.02f;
    private const float FrameThickness = 0.02f;

    private static int _frameImageIndex;
    private List<string> _tags;
    public string Name;

    public GameObject FrameBody;
    public GameObject Image;
    public GameObject ImagePackParent;

    private Material _imageMat;
    private Texture _imageTexture;

    private float _frameScaleProportion = 0.10f;

    private Vector3 _newImageScale;
    private Vector3 _newFrameBodyScale;
    private Vector3 _imageOffset = new Vector3(0.02f, 0, 0);

    public string Texture;

    private bool _moveToHand;
    private Vector3 _direction;
    private Vector3 _targetTransofrm;

    public List<string> Tags
    {
        get { return _tags; }
        set { _tags = value; }
    }

    // Use this for initialization
	void Start ()
	{
	    Texture = ImageHandler.ImageFiles[_frameImageIndex];
	    Texture = Texture.Replace(".jpg", "");
	    if (_frameImageIndex >= ImageHandler.ImageFiles.Length - 1)
	        _frameImageIndex = 0;
	    _frameImageIndex++;
        ImagePackParent = transform.parent.gameObject;
        ShowNewImage();
	    IntitializeImage();
	    IntitalizeFrameBody();
	}
	
	// Update is called once per frame
	void Update () {

        if(!_moveToHand) return;
	    _direction = transform.position - _targetTransofrm;
	    transform.Translate(_direction.normalized * Time.deltaTime);
    }

    public void ShowNewImage()
    {
        _imageTexture = Resources.Load(Texture) as Texture;
    }

    private void IntitializeImage()
    {

        _newImageScale = new Vector3(ImageThickness, 1, 1);
        Image = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Image.transform.parent = transform;
        Image.transform.localPosition = _imageOffset;
        Image.transform.rotation = ImagePackParent.transform.rotation;
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
        FrameBody.transform.rotation = ImagePackParent.transform.rotation;  
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
        _newFrameBodyScale.y = _newImageScale.y + (_newImageScale.y * _frameScaleProportion / (_newImageScale.z / _newImageScale.y));
        _newFrameBodyScale.z = _newImageScale.z + (_newImageScale.z * (_frameScaleProportion / (_newImageScale.z /_newImageScale.y)));

        FrameBody.transform.localScale = _newFrameBodyScale;
    }

    public void GrabFrame(Transform controllerTransform)
    {
        _moveToHand = true;
        _targetTransofrm = controllerTransform.position;
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


    // Strech the image and set its scale then set the scale of the frame to 0.5 devided by (ImageTexture scale devided by 2)

}
