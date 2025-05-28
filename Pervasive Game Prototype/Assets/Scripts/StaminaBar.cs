using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI staminaBarValueText;

    public void UpdateStamina(int currentStamina, int maxSTA)
    {
        staminaBarValueText.text = currentStamina.ToString() + "/" + maxSTA.ToString();

        slider.maxValue = maxSTA;
        slider.value = currentStamina;
    }
}
