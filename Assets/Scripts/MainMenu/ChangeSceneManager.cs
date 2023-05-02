using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeSceneManager : MonoBehaviour
{

    public ScriptableObjectsScript[] scriptableScene;

    private int currentScene = 0;

    [SerializeField]private TextMeshProUGUI sceneName;
    [SerializeField]private Slider mainSlider;
    [SerializeField]private TextMeshProUGUI textScene;

    [SerializeField]private Button[] Buttons;

    [SerializeField]private VideoPlayer _vp;


    [SerializeField]private GameObject imagenVideo;

    [SerializeField]private Slider videoSlider;
    [SerializeField] private Button exitButton;

    private bool isDraggingSlider = false;



    private void Start()
    {
        UpdateData();
        DesactivateHudVideo();
    }

    private void Update()
    {
        if (!_vp.isPlaying || isDraggingSlider)
        {
            return;
        }

        videoSlider.value = (float)_vp.time;
        
    }

    public void OnSliderValueChanged()
    {
        isDraggingSlider = true;
        _vp.time = videoSlider.value;
    }

  

    public void UpdateData()
    {
        currentScene = (int) mainSlider.value;

        mainSlider.value = currentScene;
        sceneName.text = scriptableScene[currentScene].nameStage;
        textScene.text = scriptableScene[currentScene].TextScene;
        _vp.clip = scriptableScene[currentScene].video;
        _vp.Stop();

        SetButtonNames();
    }

    private void SetButtonNames()
    {
        int maxRange = scriptableScene[currentScene].maxButton;

        foreach(Button b in Buttons)
        {
            b.gameObject.SetActive(false);
        }

        for (int i = 0; i <= maxRange -1; i++)
        {
            Buttons[i].gameObject.SetActive(true);
            Buttons[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = scriptableScene[currentScene].butttonNames[i];
        }
    }


    public void SelectLvl(int selectNum)
    {
        mainSlider.value = selectNum;
    }

    public void PausePlayVideo(bool pause)
    {
        if (pause.Equals(true))
        {
            _vp.Pause();
        }
        else
        {
            _vp.Play();
        }
        
    }

    public void PlayVideo()
    {
        _vp.Play();
        ActivateHudVideo();

        imagenVideo.SetActive(true);


        videoSlider.maxValue = (float)_vp.length;
        videoSlider.value = (float)_vp.time;
    }

    public void ExitVideo()
    {
        _vp.Stop();
        DesactivateHudVideo();
        imagenVideo.SetActive(false);
    }


    public void ActivateHudVideo()
    {
        videoSlider.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }

    public void DesactivateHudVideo()
    {
        videoSlider.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
}
