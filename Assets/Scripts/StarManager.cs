using UnityEngine;

public class StarManager : MonoBehaviour
{


    public int starsEarned = 0;

    [Header("Collectables")]
    public int collectablesToCollect;
    public int collectablesCollected = 0;
    bool collectedAllCollectables = false;

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


    }


}
