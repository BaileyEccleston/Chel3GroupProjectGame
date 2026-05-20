using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeBackPress : MonoBehaviour
{
    public Button ButtonWardrobe;
    public GameObject ClapperBoard;
    public Animation Playclack;
    public Animation PlayBackSwivel;
    public GameObject UiButtons1;
    public GameObject UiButtons2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UiButtons2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonWardrobeBackClick()
    {

        Debug.Log("Heya");
        Playclack.Play("Clack");
        PlayBackSwivel.Play("Back Swivel");
        UiButtons1.SetActive(true);
        UiButtons2.SetActive(false);
    }
}
