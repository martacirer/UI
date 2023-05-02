using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float startTime;

    public static bool timerIsOn = true;

    float currentTime = 0;

    [Space]
    [SerializeField] TextMeshProUGUI timeText;

    private void Start()
    {
        currentTime = startTime;
    }

    private void Update()
    {
        if (timerIsOn)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                timerIsOn = false;

                GameOver();
            }

            DysplayTime();
        }
    }

    void DysplayTime()
    {
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = currentTime % 60;

        timeText.text = minutes + ":" + Mathf.RoundToInt(seconds);
    }


    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
