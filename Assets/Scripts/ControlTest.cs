using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class ControlTest : MonoBehaviour
{
    public InputMaster controls;

    private void Awake()
    {
        // Create 2 separate controls for the different gamepads
        //controls1 = new InputMaster();
        //controls2 = new InputMaster();

        //controls1.devices = new[] { Gamepad.all[0] };
        //controls2.devices = new[] { Gamepad.all[1] };

        // Create new input master ref
        controls = new InputMaster();

        // Charge will now be called when 'Charge' button is pressed
        controls.Player1.ChargePress.performed += _ => Charge();

        // Turn will now be called when 'Turning' buttons are pressed (Either one)
        // Will pass in float value from -1 to 1
        controls.Player1.Turning.performed += ctx => Turn(ctx.ReadValue<float>());
    }

    private void Update()
    {
        // Quick way to test for button press. Not to use for permanent controls
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame) Debug.Log("Space was pressed");
    }

    void Turn(float _dir)
    {
        Debug.Log("Turning..." + _dir);
    }

    // Charge function
    void Charge()
    {
        Debug.Log("Charging....");
    }

    private void OnEnable()
    {
        // Enables all controls
        controls.Enable();
    }

    private void OnDisable()
    {
        // Disables all controls
        controls.Disable();
    }
}
