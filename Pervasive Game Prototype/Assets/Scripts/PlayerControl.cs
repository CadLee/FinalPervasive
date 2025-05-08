using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float chargeTime;

    private PlayerInputActions controls;
    private bool isLeftHandCharging = false;
    private bool isRightHandCharging = false;

    private enum ArmState { Neutral, PulledBack, PushedOutside, PushedInside, Block, Idle }
    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private void Awake()
    {
        controls = new PlayerInputActions();

        controls.Player.Enable();

        controls.Player.LeftHand.performed += LeftHand_performed;
        controls.Player.RightHand.performed += RightHand_performed;
    }

    private void LeftHand_performed(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();

        HandleArmInput(inputVector, "Left Hand", ref leftArmState, isLeftJoystick: true);
    }

    private void RightHand_performed(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();

        HandleArmInput(inputVector, "Right Hand", ref rightArmState, isLeftJoystick: false);
    }

    private void HandleArmInput(Vector2 inputVector, string hand, ref ArmState armState, bool isLeftJoystick)
    {
        if (inputVector.y >= -0.3f && inputVector.y <= 0.3f && inputVector.x >= -0.3f && inputVector.x <= 0.3f && armState != ArmState.Neutral)
        {
            Debug.Log($"{hand} Neutral");
            armState = ArmState.Neutral; // Reset state after block
        }

        else if (inputVector.y >= -0.8f && inputVector.y <= -0.3f && armState != ArmState.PulledBack)
        {
            Debug.Log($"{hand} PulledBack");
            armState = ArmState.PulledBack; // Reset state after block
        }

        else if (inputVector.y >= -1f && inputVector.y <= -0.8f && armState != ArmState.Block)
        {
            Debug.Log($"{hand} Block");
            armState = ArmState.Block; // Reset state after block
        }

        else if (isLeftJoystick && (armState == ArmState.Neutral))
        {
            if (inputVector.x <= -0.6f)
            {
                Debug.Log($"{hand} Pushed Out");
                armState = ArmState.PushedOutside; // Left joystick pushed left
            }
            else if (inputVector.x >= 0.6f)
            {
                Debug.Log($"{hand} Pulled in");
                armState = ArmState.PushedInside; // Left joystick pushed right
            }
        }
        else if (!isLeftJoystick && (armState == ArmState.Neutral))
        {
            if (inputVector.x >= 0.5f)
            {
                Debug.Log($"{hand} Pushed Out");
                armState = ArmState.PushedOutside; // Right joystick pushed right
            }
            else if (inputVector.x <= -0.5f)
            {
                Debug.Log($"{hand} Pulled in");
                armState = ArmState.PushedInside; // Right joystick pushed left
            }
        }

        if (inputVector.y >= 0.5f)
        {
            if (armState == ArmState.PulledBack || armState == ArmState.PulledBack)
            {
                Debug.Log($"{hand} Straight");
                armState = ArmState.Idle;
            }
            else if (armState == ArmState.PushedOutside)
            {
                Debug.Log($"{hand} Hook");
                armState = ArmState.Idle;
            }
            else if (armState == ArmState.PushedInside)
            {
                Debug.Log($"{hand} Uppercut");
                armState = ArmState.Idle;
            }
            else if (armState == ArmState.Neutral)
            {
                Debug.Log($"{hand} Punch/Jab");
                armState = ArmState.Idle;
            }
        }
    }
}
