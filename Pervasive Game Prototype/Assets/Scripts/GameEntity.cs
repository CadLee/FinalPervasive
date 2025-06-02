using UnityEngine;

public abstract class  GameEntity : MonoBehaviour
{

    [SerializeField] public int maxHP;
    public float health;

    [SerializeField] public int maxStamina;
    public float stamina;

    [SerializeField] public int staminaRegen;
    [SerializeField] public float staminaDrain;
    protected bool isDead;

    void Start()
    {
        health = maxHP;
        stamina = maxStamina;
        isDead = false;
    }

    void FixedUpdate()
    {
        if (stamina < maxStamina)
        {
            stamina += staminaRegen / 100f;
        }

        if (health <= 0)
        {
            death();
            isDead = true;
        }
    }

    public void RegenStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += staminaRegen / 100f;
        }
    }


    public void damage(int damageHP) 
    {
        health -= damageHP;
    }
    public abstract void death();
}
