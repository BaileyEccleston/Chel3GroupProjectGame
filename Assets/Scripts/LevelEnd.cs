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

    GameObject[] players;

    GameObject player1;
    GameObject player2;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player1 = players[0];
        player2 = players[1];
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
            Vector3 newPos = new Vector3(119.07f, 34.2f, 4.37f);
            player1.transform.position = newPos;

            newPos = new Vector3(119.7F, 34.2f, 5);
            player2.transform.position = newPos;
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
