using TMPro;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    //stop level ending more than once
    bool levelComplete = false;

    public GameObject playUI;
    public GameObject endScreenUI;

    public StarManager starManager;

    public LevelTimer levelTimer;

    public TextMeshProUGUI endTimerText;

    // star icons
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public PlayerLives playerLives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        // trigger end when the player collides with end goal box collider
        if (other.CompareTag("Player") && !levelComplete)
        {

            // turn of level ui and turn on end ui
            levelComplete = true;
            levelTimer.levelComplete();
            float elapsedTime = levelTimer.elapsedTime;
            endScreenUI.SetActive(true);
            playUI.SetActive(false);

            int minutes = Mathf.FloorToInt(elapsedTime / 60);

            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

            endTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

            // add star for ending with 3 lives
            if (playerLives.currentLives == 3)
            {
                starManager.starsEarned++;
            }

            // display earned stars
            if (starManager.starsEarned == 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if (starManager.starsEarned == 2)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
            }
            else if (starManager.starsEarned == 1)
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
            }
            else if (starManager.starsEarned <= 0)
            {
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
            }
        }
    }

}
