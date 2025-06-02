using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerEntity : GameEntity
{
    private PlayerControl controls;

    public GameObject Opponent;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    private bool infStamina = false;
    private int staminaregenvalue;
    void Start()
    {
        controls = GetComponent<PlayerControl>();

        health = maxHP;
        stamina = maxStamina;
        isDead = false;

        healthBar.UpdateHealth(health, maxHP);
        staminaBar.UpdateStamina(stamina, maxStamina);

        staminaregenvalue = staminaRegen;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10; // Example damage
            healthBar.UpdateHealth(health, maxHP);
            Debug.Log("Player took damage, current health: " + health);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            infStamina = !infStamina;
        }

        if (infStamina)
        {
            stamina = maxStamina;
            staminaRegen = 1000;
            staminaBar.UpdateStamina(stamina, maxStamina);
            Debug.Log("Infinite stamina enabled, stamina set to max and regen increased.");
        }
        else 
        {
            staminaRegen = staminaregenvalue;
            staminaBar.UpdateStamina(stamina, maxStamina);
            Debug.Log("Infinite stamina disabled, stamina regen set to normal.");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game is quitting");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Reloading scene...");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reloading scene...");
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
