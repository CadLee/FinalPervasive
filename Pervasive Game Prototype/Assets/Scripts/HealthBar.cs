using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI healthBarValueText;

    public void UpdateHealth(float currentHealth, int maxHP)
    {
        healthBarValueText.text = ((int)currentHealth).ToString() + "/" + maxHP.ToString();

        slider.maxValue = maxHP;
        slider.value = currentHealth;
    }
}
