using UnityEngine;

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

    public override void death()
    {
        throw new System.NotImplementedException();
    }
}
