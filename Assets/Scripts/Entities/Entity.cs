using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{ 
    public event System.Action OnDamaged;
    public event System.Action OnDeath;

    public int currentHealth;
    public int maxHealth;
    public bool persistenceRequired;
    [SerializeField] protected bool isPooled;
    [SerializeField] protected float age;
    [SerializeField] protected float maxAge;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {

    }

    protected virtual void AgeBehaviour()
    {
        if (!persistenceRequired)
        {
            if (age >= 0)
            {
                age += Time.deltaTime;
            }
            if (age > maxAge)
            {
                Despawn();
            }
        }
    }

    public void TakeDamage()
    {
        TakeDamage(1);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth = Math.Max(currentHealth -= _damage, 0);
        InvokeOnDamaged();
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        InvokeOnDeath();
        Despawn();
    }

    public virtual void Despawn()
    {
        if (isPooled)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected void InvokeOnDamaged()
    {
        OnDamaged?.Invoke();
    }

    protected void InvokeOnDeath()
    {
        OnDeath?.Invoke();
        Debug.LogWarning(this + " died");
    }
}
