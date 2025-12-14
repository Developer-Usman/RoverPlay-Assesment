using UnityEngine;

public class PlayerHealth : Health
{
    void OnEnable()
    {
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        UpdateHealthBar();
    }
    public override void TakeDamage()
    {
        if (currentHealth < 0 || IsImmortal) return;
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            // Player Dead
            gameObject.SetActive(false);
        }
    }
}
