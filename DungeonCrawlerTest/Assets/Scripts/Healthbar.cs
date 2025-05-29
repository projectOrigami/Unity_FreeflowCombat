using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour // if it will have a slider
{
    private Slider healthSlider;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;

        UpdateHealth(maxHealth);
    }

    public void UpdateHealth(int amount = 0, Action<bool> OnHealthEmpty = null)
    {
        currentHealth = Mathf.Max(0, currentHealth + amount);
        healthSlider.value = currentHealth;
        OnHealthEmpty?.Invoke(currentHealth == 0);
    }
}
