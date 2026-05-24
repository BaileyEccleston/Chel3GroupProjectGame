using UnityEngine;

public class CostumeApply : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject Outfit1;
    public GameObject Outfit2;
    public GameObject Outfit3;
    public int CostumePrefabNumber;
    void Start()
    {
        CostumePrefabNumber = PlayerPrefs.GetInt("CostumeToLoad");
        Apply();
        
    }

    // Update is called once per frame
   

    private void Apply() 
    {


        if (CostumePrefabNumber == 1)
        {
            Outfit1.SetActive(true);
            Outfit2.SetActive(false);
            Outfit3.SetActive(false);
        }
        else if (CostumePrefabNumber == 2)
        {
            Outfit1.SetActive(false);
            Outfit2.SetActive(true);
            Outfit3.SetActive(false);
        }
        else if (CostumePrefabNumber == 3)
        {
            Outfit1.SetActive(false);
            Outfit2.SetActive(false);
            Outfit3.SetActive(true);
        }




    }

}
