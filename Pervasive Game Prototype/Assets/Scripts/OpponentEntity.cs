using UnityEngine;

public class OpponentEntity : GameEntity
{
    private PlayerEntity player;
    private PlayerControl playerControl;

    private enum OpponentType { Still, Blocking }
    [SerializeField] OpponentType opponentType;

    [SerializeField] int damageMult;


    private ArmState leftArmState = ArmState.Neutral;
    private ArmState rightArmState = ArmState.Neutral;

    private BodyState bodyState = BodyState.Neutral;
    //private AimState aimState = AimState.Neutral;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    public HitEffects hitEffects;

    public Punchsoundeffect soundEffects;

    void Start()
    {
        health = maxHP;
        stamina = maxStamina;
        isDead = false;

        player = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        healthBar.UpdateHealth(health, maxHP);
        staminaBar.UpdateStamina(stamina, maxStamina);
    }

    void Update()
    {
        
    }

    public void ProcessHit(Punch punch, string hand)
    {

        soundEffects.PlayPunchSound();

        if (bodyState == BodyState.Sway) 
        {
            damage(0); // No damage taken during sway
        }

        if (hand == "Left Hand")
        {
            if (punch == Punch.Jab)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a jab from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a jab from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Straight)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Straight from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Straight from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Hook)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a hook from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a hook from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Cross) 
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a cross from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a cross from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Uppercut)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Uppercut from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took an Uppercut from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.JumpingUppercut)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Jumping Uppercut from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Jumping Uppercut from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.KidneyShot)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Kidney Shot from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Kidney Shot from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.MidCross)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Mid Cross from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Mid Cross from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.GutShot)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Gut Shot from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Gut Shot from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefHook)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive hook from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive hook from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefBigHook)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive big hook from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive big hook from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefOverhand)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive overhand from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive overhand from the left hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefKidneyShot)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive kidney shot from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a defensive kidney shot from the left hand");
                    damage(10);
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.HeavyUppercut)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Heavy Uppercut from the left hand");
                    damage(2 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a Heavy Uppercut from the left hand");
                    damage(10 * damageMult);
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.MidHook)
            {
                if (leftArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive Mid Hook from the left hand");
                    damage(1 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a defensive Mid Hook from the left hand");
                    damage(10 * damageMult);
                    hitEffects.PlayHitEffect();
                }
            }

        }
        else
        {
            if (punch == Punch.Jab)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a jab from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a jab from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Straight)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Straight from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Straight from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Hook)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a hook from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a hook from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Cross)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a cross from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a cross from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.Uppercut)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked an Uppercut from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took an Uppercut from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.JumpingUppercut)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Jumping Uppercut from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Jumping Uppercut from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.KidneyShot)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Kidney Shot from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Kidney Shot from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.MidCross)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Mid Cross from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Mid Cross from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.GutShot)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Gut Shot from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a Gut Shot from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefHook)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive hook from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive hook from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefBigHook)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive big hook from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive big hook from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefOverhand)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive overhand from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    damage(10 * damageMult);
                    Debug.Log("Opponent took a defensive overhand from the right hand");
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.DefKidneyShot)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive kidney shot from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a defensive kidney shot from the right hand");
                    hitEffects.PlayHitEffect();
                    damage(10 * damageMult);
                }
            }
            else if (punch == Punch.HeavyUppercut)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a Heavy Uppercut from the right hand");
                    damage(2 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a Heavy Uppercut from the right hand");
                    damage(20 * damageMult);
                    hitEffects.PlayHitEffect();
                }
            }
            else if (punch == Punch.MidHook)
            {
                if (rightArmState == ArmState.Block)
                {
                    Debug.Log("Opponent blocked a defensive Mid Hook from the right hand");
                    damage(1 * damageMult);
                }
                else
                {
                    Debug.Log("Opponent took a defensive Mid Hook from the right hand");
                    hitEffects.PlayHitEffect();
                    damage(10 * damageMult);
                }
            }
        }

        healthBar.UpdateHealth(health, maxHP);
    }

    public override void death()
    {
        Debug.Log("Opponent has died.");
    }
}