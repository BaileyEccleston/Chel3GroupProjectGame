using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    int numberOfCheckpoints;

    public int currentCheckpoint = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberOfCheckpoints = checkpoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
