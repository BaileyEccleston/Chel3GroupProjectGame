using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WardrobePress : MonoBehaviour
{
    public Button ButtonWardrobe;
    public GameObject ClapperBoard;
    public Animation Playclack;
    public Animation PlaySwivel;
    public WardrobeBackPress Back;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonWardrobeClick()
    {

        Debug.Log("Heya");
        Playclack.Play("Clack");
        PlaySwivel.Play("Swivel");
        Back.UiButtons1.SetActive(false);
        Back.UiButtons2.SetActive(true);

    }
}
