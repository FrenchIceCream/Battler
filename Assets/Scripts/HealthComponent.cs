using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HealthComponent
{
    int health;
    int maxHealth;

    public event EventHandler<OnHealthChangedArgs> OnHealthChanged;

    public class OnHealthChangedArgs : EventArgs
    { 
        public int newHealth;
    }

    public void SetMaxHealth( int value)
    {
        if (value < 0)
            Debug.LogError("Negative health value: health not set");
        else
        {
            maxHealth = health = value;
        }
    }

    public int GetMaxHealth() { return maxHealth; }

    public void AddHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }
        OnHealthChanged(this, new OnHealthChangedArgs { newHealth = health });
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
