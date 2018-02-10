using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImageHandler : MonoBehaviour
{
    public List<GameObject> Frames;
    public static string[] ImageFiles;

    void Awake()
    {
        ImageFiles = Directory.GetFiles(Application.dataPath
                    + "\\Resources", "*.jpg").Select(Path.GetFileName).ToArray();
    }
}

