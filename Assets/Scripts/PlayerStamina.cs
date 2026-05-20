using System.Transactions;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina;
    public float currentStamina;
    public float staminaPerSecond;

    public Image staminaBar;

    bool barToggle;


    private void Start()
    {
        barToggle = false;
        staminaBar.gameObject.SetActive(false);
        currentStamina = maxStamina;
    }

    private void FixedUpdate()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }


    public void LoseStamina(float stamina)
    {
        currentStamina -= stamina;
    }

    public void GainStamina(float stamina)
    {
        currentStamina += stamina;
    }



    public void ResetStamina()
    {

        StartCoroutine(ToggleStamina());
        currentStamina = maxStamina;
    }

    public void JustResetStamina()
    {
        currentStamina = maxStamina;
    }

    public void EmptyStamina()
    {
        currentStamina = 0;
    }

    public IEnumerator ToggleStamina()
    {
        if (barToggle)
        {
            barToggle = false;
            yield return new WaitForSeconds(2);
            staminaBar.gameObject.SetActive(false);
        }
        else
        {
            barToggle = true;
            staminaBar.gameObject.SetActive(true);
        }
    }
}
