using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public enum ArmState { Neutral, PulledBack, PushedOutside, PushedInside, Block, Idle }
public enum BodyState { Neutral, WeaveLeft, WeaveRight, Sway , Duck}
public enum AimState { Neutral, AimLeft, AimRight}

public enum Punch { Jab, Straight, Hook, Cross, Uppercut, JumpingUppercut, KidneyShot, MidCross, GutShot, DefHook, DefBigHook, DefOverhand, DefKidneyShot, HeavyUppercut, MidHook}

//To do: Sent these punches to the opponent and then within opponent they will process how they take the damage,
// Same will be done on the 

public class PlayerControl : MonoBehaviour
{
    private PlayerInputActions controls;
    private PlayerEntity playerEntity;
    
    public GameObject opponentObject;
    private OpponentEntity opponentEntity;

    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private BodyState bodyState = BodyState.Neutral;

    private AimState aimState = AimState.Neutral;

    public TextMeshProUGUI LTooltip;
    public TextMeshProUGUI RTooltip;

    private void Awake()
    {
        controls = new PlayerInputActions();

        playerEntity = GetComponent<PlayerEntity>();
        opponentEntity = opponentObject.GetComponent<OpponentEntity>();

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

        TooltipLeft();
        TooltipRight();
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
        float neutralZone = 0.1f; // Magnitude of the neutral zone

        //Neutral Hands
        if (inputVector.x < neutralZone && inputVector.x > -neutralZone && inputVector.y < neutralZone && inputVector.y > -neutralZone && armState != ArmState.Neutral)
        {
            //Debug.Log($"{hand} Neutral");
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
        
        // Neutral Body
        if (bodyState == BodyState.Neutral)
        {
            // Neutral Aim                  or  Aim opposite
            if ( aimState == AimState.Neutral || ( aimState == AimState.AimLeft && hand == "Right Hand" ) || ( aimState == AimState.AimRight && hand == "Left Hand" ) )
            {
                if (playerEntity.Stamina >= playerEntity.StaminaDrain)
                {
                    // Straight
                    if (armState == ArmState.PulledBack || armState == ArmState.PulledBack)
                    {
                        opponentEntity.ProcessHit(Punch.Straight, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    // Outside
                    else if (armState == ArmState.PushedOutside)
                    {
                        opponentEntity.ProcessHit(Punch.Hook, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    // Inside
                    else if (armState == ArmState.PushedInside)
                    {
                        opponentEntity.ProcessHit(Punch.Cross, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    // Jab
                    else if (armState == ArmState.Neutral)
                    {
                        opponentEntity.ProcessHit(Punch.Jab, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }
                }
                
                
            }
            
            // While Aiming
            if ((aimState == AimState.AimLeft && hand == "Left Hand") || (aimState == AimState.AimRight && hand == "Right Hand"))
            {

                if (playerEntity.Stamina >= playerEntity.StaminaDrain)
                {
                    // Straight
                    if (armState == ArmState.PulledBack || armState == ArmState.PulledBack)
                    {
                        opponentEntity.ProcessHit(Punch.Uppercut, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    // Outside
                    else if (armState == ArmState.PushedOutside)
                    {
                        opponentEntity.ProcessHit(Punch.KidneyShot, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    //Inside
                    else if (armState == ArmState.PushedInside)
                    {
                        opponentEntity.ProcessHit(Punch.MidCross, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    //Jab
                    else if (armState == ArmState.Neutral)
                    {
                        opponentEntity.ProcessHit(Punch.GutShot, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }
                }
            }
            
        }
        
        //     Weave Body Left                  AND Left hand           AND Left Aim Neutral             ||   Weave Body Right AND Right hand
        if ( ( bodyState == BodyState.WeaveLeft && hand == "Left Hand" && aimState != AimState.AimLeft ) || ( bodyState == BodyState.WeaveRight && hand == "Right Hand" && aimState != AimState.AimRight ) )
        {
            if (playerEntity.Stamina >= playerEntity.StaminaDrain * 1.5f)
            {
                // Outside
                if (armState == ArmState.PushedOutside)
                {
                    opponentEntity.ProcessHit(Punch.DefHook, hand);
                    armState = ArmState.Idle;
                    playerEntity.Stamina -= playerEntity.StaminaDrain * 1.5f;
                }

                // Straight
                if (armState == ArmState.PulledBack || armState == ArmState.PulledBack)
                {
                    opponentEntity.ProcessHit(Punch.DefBigHook, hand);
                    armState = ArmState.Idle;
                    playerEntity.Stamina -= playerEntity.StaminaDrain * 1.5f;
                }
            }
            
        }
        
        //      Weave Body Left AND Right hand                            ||   Weave Body Right AND Left hand
        else if ( ( bodyState == BodyState.WeaveLeft && hand == "Right Hand" && aimState != AimState.AimRight ) || ( bodyState == BodyState.WeaveRight && hand == "Left Hand" && aimState != AimState.AimLeft ) )
        {
            if (playerEntity.Stamina >= playerEntity.StaminaDrain * 1.5f)
            {
                // Outside
                if (armState == ArmState.PushedOutside)
                {
                    opponentEntity.ProcessHit(Punch.DefOverhand, hand);
                    armState = ArmState.Idle;
                    playerEntity.Stamina -= playerEntity.StaminaDrain * 1.5f;
                }
            }
            
        }
        
        //     Weave Body Left                  AND Left hand           AND Left Aim Neutral             ||   Weave Body Right AND Right hand
        else if ( ( bodyState == BodyState.WeaveLeft && hand == "Left Hand" && aimState == AimState.AimLeft ) || ( bodyState == BodyState.WeaveRight && hand == "Right Hand" && aimState == AimState.AimRight ) )
        {
            if (playerEntity.Stamina >= playerEntity.StaminaDrain * 1.5f)
            {
                // Outside
                if (armState == ArmState.PushedOutside)
                {
                    opponentEntity.ProcessHit(Punch.DefKidneyShot, hand);
                    armState = ArmState.Idle;
                    playerEntity.Stamina -= playerEntity.StaminaDrain * 1.5f;
                }
            }
            
        }
        
        // Body Duck
        if (bodyState == BodyState.Duck)
        {
            // Neutral Aim                  or  Aim opposite
            if ( aimState == AimState.Neutral || ( aimState == AimState.AimLeft && hand == "Right Hand" ) || ( aimState == AimState.AimRight && hand == "Left Hand" ) )
            {
                if (playerEntity.Stamina >= playerEntity.StaminaDrain)
                {
                    // Straight
                    if ((armState == ArmState.PulledBack || armState == ArmState.PulledBack)&& playerEntity.Stamina > playerEntity.StaminaDrain * 2 )
                    {
                        opponentEntity.ProcessHit(Punch.HeavyUppercut, hand);
                        armState = ArmState.Idle;
                        bodyState = BodyState.Neutral;
                        playerEntity.Stamina -= playerEntity.StaminaDrain * 2;

                    }

                    // Outside
                    else if (armState == ArmState.PushedOutside)
                    {
                        opponentEntity.ProcessHit(Punch.MidHook, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }

                    // Inside
                    else if (armState == ArmState.PushedInside)
                    {
                        opponentEntity.ProcessHit(Punch.MidCross, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;

                    }

                    // Jab
                    else if (armState == ArmState.Neutral)
                    {
                        opponentEntity.ProcessHit(Punch.GutShot, hand);
                        armState = ArmState.Idle;
                        playerEntity.Stamina -= playerEntity.StaminaDrain;
                    }
                }
            }
        }
    }

    public void TooltipLeft() 
    {
        if (bodyState == BodyState.Neutral) 
        {
            if (aimState == AimState.Neutral || aimState == AimState.AimRight)
            {
                LTooltip.text = "JAB = FLICK UP\nSTRAIGHT = DOWN + UP\nHOOK = LEFT + UP\nCROSS = RIGHT+UP";
                LTooltip.text += "\n\n AIM BODY = LT(HOLD)";
            }
            else if (aimState == AimState.AimLeft)
            {
                LTooltip.text = "GUTSHOT = FLICK UP\nUPPERCUT = DOWN + UP\nKIDNEY SHOT = LEFT + UP\nMIDCROSS = RIGHT+UP";
                LTooltip.text += "\n\n AIM NEUTRAL = RELEASE LT";
            }

            LTooltip.text += "\n\nWEAVE LEFT = HOLD LB\nWEAVE RIGHT = HOLD RB\nSWAY = HOLD (LB + RB)";
        }
        else if (bodyState == BodyState.WeaveLeft)
        {
            if(aimState == AimState.Neutral || aimState == AimState.AimRight)
            {
                LTooltip.text = "DEF HOOK = LEFT + UP\nDEF BIG HOOK = DOWN + LEFT + UP";
            }
            else if (aimState == AimState.AimLeft)
            {
                LTooltip.text = "DEF KIDNEY SHOT = LEFT + UP";
            }

            LTooltip.text += "\n\nNEUTRAL STANCE = RELEASE LB\nSWAY = RB(HOLD)";
        }
        else if (bodyState == BodyState.WeaveRight)
        {
            if(aimState == AimState.Neutral || aimState == AimState.AimRight)
            {
                LTooltip.text = "DEF OVERHAND = LEFT + UP";
            }
            else if (aimState == AimState.AimLeft)
            {
                LTooltip.text = "";
                LTooltip.text += "\n\n AIM NEUTRAL = RELEASE LT";
            }
            LTooltip.text += "\n\nNEUTRAL STANCE = RELEASE RB\nSWAY = LB(HOLD)";
        }
        else if (bodyState == BodyState.Duck)
        {
            LTooltip.text = "GUT SHOT = FLICK UP\nMID HOOK = LEFT + UP\nMID CROSS = RIGHT + UP\nHEAVY UPPERCUT = DOWN+UP";

            LTooltip.text += "\n\nSWAY = HOLD LB + RB\nNEUTRAL STANCE = RELEASE LT + RT";
        }
        else if (bodyState == BodyState.Sway)
        {
            LTooltip.text = "DUCK = HOLD LT + RT\nNEUTRAL STANCE = RELEASE LB + RB";
        }
    }

    public void TooltipRight()
    {
        if (bodyState == BodyState.Neutral)
        {
            if (aimState == AimState.Neutral || aimState == AimState.AimLeft)
            {
                RTooltip.text = "JAB = FLICK UP\nSTRAIGHT = DOWN + UP\nHOOK = RIGHT + UP\nCROSS = LEFT+UP";
                RTooltip.text += "\n\n AIM BODY = RT(HOLD)";
            }
            else if (aimState == AimState.AimRight)
            {
                RTooltip.text = "GUTSHOT = FLICK UP\nUPPERCUT = DOWN + UP\nKIDNEY SHOT = RIGHT + UP\nMIDCROSS = LEFT+UP";
                RTooltip.text += "\n\n AIM NEUTRAL = RELEASE RT";
            }

            RTooltip.text += "\n\nWEAVE RIGHT = HOLD RB\nWEAVE LEFT = HOLD LB\nSWAY = HOLD (LB + RB)";
        }
        else if (bodyState == BodyState.WeaveRight)
        {
            if (aimState == AimState.Neutral || aimState == AimState.AimLeft)
            {
                RTooltip.text = "DEF HOOK = RIGHT + UP\nDEF BIG HOOK = DOWN + RIGHT + UP";
            }
            else if (aimState == AimState.AimRight)
            {
                RTooltip.text = "DEF KIDNEY SHOT = RIGHT + UP";
            }

            RTooltip.text += "\n\nNEUTRAL STANCE = RELEASE RB\nSWAY = LB(HOLD)";
        }
        else if (bodyState == BodyState.WeaveLeft)
        {
            if (aimState == AimState.Neutral || aimState == AimState.AimLeft)
            {
                RTooltip.text = "DEF OVERHAND = RIGHT + UP";
            }
            else if (aimState == AimState.AimRight)
            {
                RTooltip.text = "";
                RTooltip.text += "\n\n AIM NEUTRAL = RELEASE RT";
            }
            RTooltip.text += "\n\nNEUTRAL STANCE = RELEASE LB\nSWAY = RB(HOLD)";
        }
        else if (bodyState == BodyState.Duck)
        {
            RTooltip.text = "GUT SHOT = FLICK UP\nMID HOOK = RIGHT + UP\nMID CROSS = LEFT + UP\nHEAVY UPPERCUT = DOWN+UP";

            RTooltip.text += "\n\nSWAY = HOLD LB + RB\nNEUTRAL STANCE = RELEASE LT + RT";
        }
        else if (bodyState == BodyState.Sway)
        {
            RTooltip.text = "DUCK = HOLD LT + RT\nNEUTRAL STANCE = RELEASE LB + RB";
        }
    }

    public ArmState GetArmState(bool isLeftHand)
    {
        return isLeftHand ? leftArmState : rightArmState;
    }

    public BodyState GetBodyState()
    {
        return bodyState;
    }

    public AimState GetAimState()
    {
        return aimState;
    }
}
