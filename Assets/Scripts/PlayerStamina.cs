using System.Transactions;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina;
    float currentStamina;
    public float staminaPerSecond;


    private void Start()
    {
        currentStamina = maxStamina;
    }


    public void LoseStamina(float stamina)
    {
        currentStamina -= stamina;
    }

    public void GainStamina(float stamina)
    {
        currentStamina += stamina;
    }

    public void resetStamina()
    {
        currentStamina = maxStamina;
    }

    public void EmptyStamina()
    {
        currentStamina = 0;
    }
}
