using UnityEngine;

public abstract class  GameEntity : MonoBehaviour
{

    [SerializeField] public int maxHP;
    protected int health;

    [SerializeField] public int maxStamina;
    protected int stamina;

    [SerializeField] public int staminaRegen;
    protected bool isDead;

    void Start()
    {
        health = maxHP;
        stamina = maxStamina;
        isDead = false;
    }

    void FixedUpdate()
    {
        if (stamina <= maxStamina)
        {
            stamina += staminaRegen / 100;
        }

        if (health <= 0)
        {
            death();
            isDead = true;
        }
    }


    public void damage(int damageHP) 
    {
        health -= damageHP;
    }
    public abstract void death();
}
