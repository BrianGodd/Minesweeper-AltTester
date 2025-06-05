using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = FormatTime(elapsedTime);
        }
    }
    
    public void ResetTimer()
    {
        elapsedTime = 0f;
        timerText.text = FormatTime(elapsedTime);
        isRunning = false;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
