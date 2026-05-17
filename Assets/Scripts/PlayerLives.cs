using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{

    public int startingLives = 3;
    int currentLives;


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

    public void RestartLevel()
    {

    }

    public void IncreaseLives()
    {
        currentLives++;
    }

    public void DecreaseLives()
    {
        currentLives--;
    }

    public void RefillLives()
    {
        currentLives = startingLives;
    }

}
