using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public StarManager starManager;

    // goal time to earn an extra star
    public float goalTime = 180f;

    public TextMeshProUGUI timerText;

    public float elapsedTime;

    bool levelFinished = false;

    void Update()
    {
        if (!levelFinished)
        {
            elapsedTime += Time.deltaTime;

            // convert elapsed time to minutes seconds and milliseconds
            int minutes = Mathf.FloorToInt(elapsedTime / 60);

            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }


    public void levelComplete()
    {
        // give a star if the level is complete within the goal time
        if (elapsedTime <= goalTime)
        {
            starManager.starsEarned++;
        }
    }
}
