using UnityEngine;

public class AIHealth : Health, IReturnToPool
{
    void OnEnable()
    {
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        UpdateHealthBar();
    } 
    public override void TakeDamage()
    {
        if (currentHealth < 0) return;
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            // Enemy Dead
            AIManager.Instance?.CheckWin();
            ReturnToPool(); 
            ActivateDestroyParticle();
        }
    }
    void ActivateDestroyParticle()
    {
        GameObject part = PoolManager.Instance.Get(PoolObjectType.Kill);
        part.transform.position = transform.position;
        part.transform.rotation = transform.rotation;
    }
    public void ReturnToPool() =>PoolManager.Instance.ReturnToPool(PoolObjectType.Enemy,gameObject);
}

