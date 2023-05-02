using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "ScriptableObjectsScript", menuName = "ScriptableObjects/Scenes")]
public class ScriptableObjectsScript : ScriptableObject
{
    public string nameStage;
    public int numScene;

    public VideoClip video;
    public Image portadaVideo;

    public int[] sceneGo;

    public string[] butttonNames;

    public int maxButton;

    [@TextAreaAttribute(15, 20)]
    public string TextScene;
}
