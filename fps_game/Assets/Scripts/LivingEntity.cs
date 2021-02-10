using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagealbe
{
    public float startingHealth = 100f;
    public float currentHealth { get; private set; }
    public bool isDead { get; private set; }
    public event System.Action onDeath;

    protected virtual void OnEnable()
    {
        isDead = false;
        currentHealth = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath();
        }
        isDead = true;
    }

    public virtual void RecoverHealth(float newHealth)
    {
        if (isDead)
        {
            return;
        }
        currentHealth += newHealth;
    }
}
