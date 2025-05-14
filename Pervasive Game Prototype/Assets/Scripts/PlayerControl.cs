using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerControl : MonoBehaviour
{
    private PlayerInputActions controls;

    private enum ArmState { Neutral, PulledBack, PushedOutside, PushedInside, Block, Idle }
    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private enum BodyState { Neutral, WeaveLeft, WeaveRight, Sway , Duck}
    private BodyState bodyState = BodyState.Neutral;

    private enum AimState { Neutral, AimLeft, AimRight}
    private AimState aimState = AimState.Neutral;

    private void Awake()
    {
        controls = new PlayerInputActions();

        controls.Player.Enable();

        controls.Player.SwayLeft.started += context => HandleSwayInput("Left", true);
        controls.Player.SwayLeft.canceled += context => HandleSwayInput("Left", false);

        controls.Player.SwayRight.started += context => HandleSwayInput("Right", true);
        controls.Player.SwayRight.canceled += context => HandleSwayInput("Right", false);

        controls.Player.LeftAim.started += context => HandleAimInput("Left", true);
        controls.Player.LeftAim.canceled += context => HandleAimInput("Left", false);

        controls.Player.RightAim.started += context => HandleAimInput("Right", true);
        controls.Player.RightAim.canceled += context => HandleAimInput("Right", false);
    }

    private void FixedUpdate()
    {
        LeftHand_performed();
        RightHand_performed();
    }

    private void LeftHand_performed()
    {
        Vector2 inputVector = controls.Player.LeftHand.ReadValue<Vector2>();

        HandleArmInput(inputVector, "Left Hand", ref leftArmState, isLeftJoystick: true);
    }

    private void RightHand_performed()
    {
        Vector2 inputVector = controls.Player.RightHand.ReadValue<Vector2>();

        HandleArmInput(inputVector, "Right Hand", ref rightArmState, isLeftJoystick: false);
    }

    private void HandleArmInput(Vector2 inputVector, string hand, ref ArmState armState, bool isLeftJoystick)
    {
        float neutralZone = 0.1f;

        //Neutral Hands
        if (inputVector.x < neutralZone && inputVector.x > -neutralZone && inputVector.y < neutralZone && inputVector.y > -neutralZone && armState != ArmState.Neutral)
        {
            Debug.Log($"{hand} Neutral");
            armState = ArmState.Neutral; // Reset state after block
        }

        else if (inputVector.y >= -0.8f && inputVector.y <= -0.3f && armState != ArmState.PulledBack)
        {
            armState = ArmState.PulledBack; // Reset state after block
        }

        else if (inputVector.y <= -0.8f && armState != ArmState.Block)
        {
            Debug.Log($"{hand} Block");
            armState = ArmState.Block; // Reset state after block
        }

        else if (isLeftJoystick && (armState == ArmState.Neutral))
        {
            if (inputVector.x < -0.8f)
            {
                armState = ArmState.PushedOutside; // Left joystick pushed left
            }
            else if (inputVector.x > 0.8f)
            {
                armState = ArmState.PushedInside; // Left joystick pushed right
            }
        }
        else if (!isLeftJoystick && (armState == ArmState.Neutral))
        {
            if (inputVector.x > 0.8f)
            {
                armState = ArmState.PushedOutside; // Right joystick pushed right
            }
            else if (inputVector.x < -0.8f)
            {
                armState = ArmState.PushedInside; // Right joystick pushed left
            }
        }

        // Punches
        if (inputVector.y >= 0.8f)
        {
            ProcessPunch(ref armState, hand);
        }
    }

    private void HandleSwayInput(string direction, bool performed)
    {
        if (performed)
        {
            //From Neutral to Weave
            if (bodyState == BodyState.Neutral)
            {
                if (direction == "Left")
                {
                    bodyState = BodyState.WeaveLeft;
                    Debug.Log("Weave Left");
                }
                else if (direction == "Right")
                {
                    bodyState = BodyState.WeaveRight;
                    Debug.Log("Weave Right");
                }
            }

            //From Weave to Sway
            else if (bodyState == BodyState.WeaveLeft && direction == "Right")
            {
                bodyState = BodyState.Sway;
                Debug.Log("Sway");
            }
            else if (bodyState == BodyState.WeaveRight && direction == "Left")
            {
                bodyState = BodyState.Sway;
                Debug.Log("Sway");
            }

            //From Duck to Sway
            else if (bodyState == BodyState.Duck)
            {
                bodyState = BodyState.Sway;
                Debug.Log("Sway");
            }

        }

        // (Weave or Sway) to Neutral
        if (!performed)
        {
            if (bodyState == BodyState.WeaveLeft || bodyState == BodyState.WeaveRight || bodyState == BodyState.Sway)
            {
                bodyState = BodyState.Neutral;
                Debug.Log("Neutral Body");
            }
        }
    }

    private void HandleAimInput(string hand, bool performed)
    {
        if (performed)
        {
            //Neutral to Aim
            if(aimState == AimState.Neutral)
            {
                if (hand == "Left")
                {
                    Debug.Log("Aim Left Body");
                    aimState = AimState.AimLeft;
                }
                else if (hand == "Right")
                {
                    Debug.Log("Aim Right Body");
                    aimState = AimState.AimRight;
                }
            }
            
            //From Aim to Duck
            else if (aimState == AimState.AimLeft && hand == "Right" && bodyState==BodyState.Neutral)
            {
                aimState = AimState.Neutral;
                bodyState = BodyState.Duck;
                Debug.Log("Duck");
            }
            else if (aimState == AimState.AimRight && hand == "Left" && bodyState == BodyState.Neutral)
            {
                aimState = AimState.Neutral;
                bodyState = BodyState.Duck;
                Debug.Log("Duck");
            }

            //From Aim to Duck
            else if (bodyState==BodyState.Sway)
            {
                aimState = AimState.Neutral;
                bodyState = BodyState.Duck;
                Debug.Log("Duck");
            }

        }

        
        else if (!performed)
        {
            //Aim to Neutral
            if (aimState == AimState.AimLeft|| aimState == AimState.AimRight) 
            {
                Debug.Log("Stop Aiming");
                aimState = AimState.Neutral;
            }

            //Duck to Neutral
            if (bodyState == BodyState.Duck)
            {
                Debug.Log("Stop Duck");
                bodyState = BodyState.Neutral;
            }

        }
    }

    private void ProcessPunch(ref ArmState armState, string hand)
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
