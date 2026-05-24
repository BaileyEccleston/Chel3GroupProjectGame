using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeCycle : MonoBehaviour
{

    public CostumeSwap Swap;
    public int Number;

    private void Start()
    {
        Number = 1;
    }

    private void Update()
    {
    
    }
    public void OnButtonForwardClick() 
    {

        Number++;
    
    }
    public void OnButtonBackClick()
    {

        Number--;

    }




}
