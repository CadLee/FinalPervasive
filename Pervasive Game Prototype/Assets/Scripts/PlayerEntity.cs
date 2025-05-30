using UnityEngine;
using UnityEngine.Rendering;

public class PlayerEntity : GameEntity
{
    private PlayerControl controls;

    public GameObject Opponent;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    void Start()
    {
        controls = GetComponent<PlayerControl>();

        health = maxHP;
        stamina = maxStamina;
        isDead = false;

        healthBar.UpdateHealth(health, maxHP);
        staminaBar.UpdateStamina(stamina, maxStamina);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10; // Example damage
            healthBar.UpdateHealth(health, maxHP);
            Debug.Log("Player took damage, current health: " + health);
        }


    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            death();
            isDead = true;
        }

        if (
            (controls.GetArmState(false) == ArmState.Neutral&& controls.GetArmState(true) == ArmState.Neutral)
            &&  controls.GetBodyState() == BodyState.Neutral
            ) 
        {
            RegenStamina();
        }

        staminaBar.UpdateStamina(stamina, maxStamina);
    }

    public override void death()
    {
        throw new System.NotImplementedException();
    }

    public float Stamina
    {
        get 
        { 
            return stamina; 
        }
        set
        {
            stamina = value;
            staminaBar.UpdateStamina(stamina, maxStamina);
        }
    }

    public float StaminaDrain
    {
        get
        {
            return staminaDrain;
        }
    }
}
