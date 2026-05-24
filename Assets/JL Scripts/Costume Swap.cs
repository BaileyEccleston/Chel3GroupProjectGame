using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class CostumeSwap : MonoBehaviour
{

    public GameObject Outfit1;
    public GameObject Outfit2;
    public GameObject Outfit3;
    public SceneManagement SceneManagementR;

    public Button ForwardButton;
    public Button BackwardButton;

    public int CostumeNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CostumeNumber = 1;
    }

    public void Update()
    {
        Swap();
        Correction();
    }


    public void Swap()
    {
        if (CostumeNumber == 1)
        {
            Outfit1.SetActive(true);
            Outfit2.SetActive(false);
            Outfit3.SetActive(false);
        }
        else if (CostumeNumber == 2)
        {
            Outfit1.SetActive(false);
            Outfit2.SetActive(true);
            Outfit3.SetActive(false);
        }
        else if (CostumeNumber == 3)
        {
            Outfit1.SetActive(false);
            Outfit2.SetActive(false);
            Outfit3.SetActive(true);
        }

    }

    public void SetCostume()
    {

        PlayerPrefs.SetInt("CostumeToLoad",CostumeNumber);
        PlayerPrefs.Save();

    }
    public void Correction() 
    {

        if (CostumeNumber == 4)
        {
            CostumeNumber = 1;
        }
        else if (CostumeNumber == 0) 
        {
            CostumeNumber = 3;
        }
        Debug.Log("Correcting");
    }

    public void OnButtonForwardClick()
    {

        CostumeNumber++;

    }
    public void OnButtonBackClick()
    {

        CostumeNumber--;

    }
}
