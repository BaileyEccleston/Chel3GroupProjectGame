using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayPress : MonoBehaviour
{

    public Button ButtonPlay;
    public GameObject ClapperBoard;
    public Animation Playclack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPlayClick() 
    {

        Debug.Log("Heya");
        Playclack.Play("Clack");

    }

}



