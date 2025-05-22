using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI healthBarValueText;

    public void UpdateHealth(int currentHealth, int maxHP)
    {
        healthBarValueText.text = currentHealth.ToString() + "/" + maxHP.ToString();

        slider.maxValue = maxHP;
        slider.value = currentHealth;
    }
}
