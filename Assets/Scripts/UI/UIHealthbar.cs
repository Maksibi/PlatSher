using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    private Entity entity;
    private CharacterStats stats;
    private RectTransform _transform;
    private Slider slider;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        _transform = GetComponentInChildren<RectTransform>();
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<CharacterStats>();
        slider = GetComponentInChildren<Slider>();

        entity.onFlipped += FlipUI;
        stats.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.CurrentHealth;
    }

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        stats.OnHealthChanged -= UpdateHealthUI;
    }

    private void FlipUI()
    {
        _transform.Rotate(0, 180, 0);
    }
}
