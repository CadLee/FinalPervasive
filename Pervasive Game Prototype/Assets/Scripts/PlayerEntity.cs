using UnityEngine;

public class PlayerEntity : GameEntity
{
    private PlayerControl controls;

    public HealthBar healthBar;

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
        
    }

    public override void death()
    {
        throw new System.NotImplementedException();
    }
}
