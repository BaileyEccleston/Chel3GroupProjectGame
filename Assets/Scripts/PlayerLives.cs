using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;





    public int startingLives = 3;
    public int currentLives;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = startingLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    
    }



    public void UpdateHearts()
    {
        if (currentLives == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (currentLives == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if (currentLives == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (currentLives <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }

    }

    public void RestartLevel()
    {

    }

    public void IncreaseLives()
    {
        currentLives++;
        UpdateHearts();
    }

    public void DecreaseLives()
    {
        currentLives--;
        UpdateHearts();
    }

    public void RefillLives()
    {
        currentLives = startingLives;
        UpdateHearts();
    }

}
