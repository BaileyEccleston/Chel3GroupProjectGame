using UnityEngine;

public class StarManager : MonoBehaviour
{

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public int starsEarned = 0;

    [Header("Collectables")]
    public int collectablesToCollect;
    public int collectablesCollected = 0;
    bool collectedAllCollectables = false;


    public bool starCollectableCollected = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
        if (collectablesCollected >= collectablesToCollect && !collectedAllCollectables)
        {
            starsEarned++;
            collectedAllCollectables = true;
        }
       // Debug.Log("Stars Earned" + starsEarned);
    }

    public void UpdateHearts()
    {
        if (starsEarned == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (starsEarned == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (starsEarned == 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if (starsEarned <= 0)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }

    }


}
