using UnityEngine;
using UnityEngine.UI;

public class QuitPress : MonoBehaviour
{
    public Button ButtonQuit;
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

    public void OnButtonQuitClick()
    {

        Debug.Log("Heya");
        Playclack.Play("Clack");
        Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
