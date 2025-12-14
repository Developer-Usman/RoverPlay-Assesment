
using UnityEngine;
using UnityEngine.UI;
public abstract class Health : MonoBehaviour
{
    public bool IsImmortal = false;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float currentHealth = 0;
    [SerializeField] protected float damage = 0;
    public Slider healthbar;
    public abstract void TakeDamage();
    public void UpdateHealthBar()
    {
        healthbar.value = currentHealth;
    }

}