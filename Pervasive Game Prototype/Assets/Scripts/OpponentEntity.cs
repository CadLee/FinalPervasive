using UnityEngine;

public class OpponentEntity : GameEntity
{
    private PlayerEntity player;
    private PlayerControl playerControl;

    private enum OpponentType { Still }
    [SerializeField] OpponentType opponentType;
    
    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private BodyState bodyState = BodyState.Neutral;
    private AimState aimState = AimState.Neutral;

    void Start()
    {
        health = maxHP;
        stamina = maxStamina;
        isDead = false;

        player = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();   

    }

    void Update()
    {
        
    }

    public void ProcessHit(Punch punch, string hand)
    {
        if (bodyState == BodyState.Sway) 
        {
            damage(0); // No damage taken during sway
        }

        if (hand == "Left Hand")
        {
            if (punch == Punch.Jab)
            {
                Debug.Log("Opponent took a jab from the left hand");
            }
            else if (punch == Punch.Straight)
            {
                Debug.Log("Opponent took a cross from the left hand");
            }
            else if (punch == Punch.Hook)
            {
                Debug.Log("Opponent took a hook from the left hand");
            }
            else if (punch == Punch.Cross) 
            {
                Debug.Log("Opponent took a cross from the left hand");
            }
            else if (punch == Punch.Uppercut)
            {
                Debug.Log("Opponent took an uppercut from the left hand");
            }
            else if (punch == Punch.JumpingUppercut)
            {
                Debug.Log("Opponent took a jumping uppercut from the left hand");
            }
            else if (punch == Punch.KidneyShot)
            {
                Debug.Log("Opponent took a kidney shot from the left hand");
            }
            else if (punch == Punch.MidCross)
            {
                Debug.Log("Opponent took a mid cross from the left hand");
            }
            else if (punch == Punch.GutShot)
            {
                Debug.Log("Opponent took a gut shot from the left hand");
            }
            else if (punch == Punch.DefHook)
            {
                Debug.Log("Opponent took a defensive hook from the left hand");
            }
            else if (punch == Punch.DefBigHook)
            {
                Debug.Log("Opponent took a defensive big hook from the left hand");
            }
            else if (punch == Punch.DefOverhand)
            {
                Debug.Log("Opponent took a defensive overhand frrom the left hand");
            }
            else if (punch == Punch.DefKidneyShot)
            {
                Debug.Log("Opponent took a defensive kidney shot from the left hand");
            }
            else if (punch == Punch.HeavyUppercut)
            {
                Debug.Log("Opponent took a Heavy Uppercut from the left hand");
            }
            else if (punch == Punch.MidHook)
            {
                Debug.Log("Opponent took a defensive Mid Hook from the left hand");
            }

        }
        else
        {
            if (punch == Punch.Jab)
            {
                Debug.Log("Opponent took a jab from the right hand");
            }
            else if (punch == Punch.Straight)
            {
                Debug.Log("Opponent took a cross from the right hand");
            }
            else if (punch == Punch.Hook)
            {
                Debug.Log("Opponent took a hook from the right hand");
            }
            else if (punch == Punch.Cross)
            {
                Debug.Log("Opponent took a cross from the right hand");
            }
            else if (punch == Punch.Uppercut)
            {
                Debug.Log("Opponent took an uppercut from the right hand");
            }
            else if (punch == Punch.JumpingUppercut)
            {
                Debug.Log("Opponent took a jumping uppercut from the right hand");
            }
            else if (punch == Punch.KidneyShot)
            {
                Debug.Log("Opponent took a kidney shot from the right hand");
            }
            else if (punch == Punch.MidCross)
            {
                Debug.Log("Opponent took a mid cross from the right hand");
            }
            else if (punch == Punch.GutShot)
            {
                Debug.Log("Opponent took a gut shot from the right hand");
            }
            else if (punch == Punch.DefHook)
            {
                Debug.Log("Opponent took a defensive hook from the right hand");
            }
            else if (punch == Punch.DefBigHook)
            {
                Debug.Log("Opponent took a defensive big hook from the right hand");
            }
            else if (punch == Punch.DefOverhand)
            {
                Debug.Log("Opponent took a defensive overhand from the right hand");
            }
            else if (punch == Punch.DefKidneyShot)
            {
                Debug.Log("Opponent took a defensive kidney shot from the right hand");
            }
            else if (punch == Punch.HeavyUppercut)
            {
                Debug.Log("Opponent took a Heavy Uppercut from the right hand");
            }
            else if (punch == Punch.MidHook)
            {
                Debug.Log("Opponent took a defensive Mid Hook from the right hand");
            }
        }
    }

    public override void death()
    {
        Debug.Log("Opponent has died.");
    }
}