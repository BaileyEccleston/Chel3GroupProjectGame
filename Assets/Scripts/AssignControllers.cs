using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class AssignControllers : MonoBehaviour
{


    public PlayerInput player1;
    public PlayerInput player2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var controllers = Gamepad.all;

        if (controllers.Count >= 1)
        {
            InputUser.PerformPairingWithDevice(controllers[0], player1.user);
            Debug.Log("Assigned player 1 to controller 1");
        }
        if (controllers.Count >= 2)
        {
            InputUser.PerformPairingWithDevice(controllers[1], player2.user);
            Debug.Log("Assigned player 1 to controller 2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
