using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (playerStats != null)
            playerStats.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.OnHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.CurrentHealth;
    }
}
