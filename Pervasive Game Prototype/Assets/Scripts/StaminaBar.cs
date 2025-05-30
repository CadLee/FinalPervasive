using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI staminaBarValueText;

    public void UpdateStamina(float currentStamina, int maxSTA)
    {
        staminaBarValueText.text = ((int)currentStamina).ToString() + "/" + maxSTA.ToString();

        slider.maxValue = maxSTA;
        slider.value = currentStamina;
    }
}
