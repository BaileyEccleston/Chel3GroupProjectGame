using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public StarManager starManager;

    public float goalTime = 60f;

    public TextMeshProUGUI timerText;

    private float elapsedTime;

    bool levelFinished = false;

    void Update()
    {
        if (!levelFinished)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);

            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }


    public void levelComplete()
    {
        if (elapsedTime >= goalTime)
        {
            starManager.starsEarned++;
        }
    }
}
