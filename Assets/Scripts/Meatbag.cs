using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public abstract class Meatbag : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    [SerializeField]
    public bool Invincible { get; set; }

    protected Meatbag(int health, int maxHealth) {
        ResetHealthValues(health, maxHealth);
    }

    public void Damage(int amount) {
        this.health -= amount;
        if (health <= 0) {
            this.health = 0;
            OnDamage(amount + health);
            OnDeath();
        }
        else {
            OnDamage(amount);
        }
    }

    public void Heal(int amount) {
        amount = health + amount - maxHealth;
        this.health += amount;
        OnHeal(amount);
    }

    public void ResetHealthValues(int health, int maxHealth) {
        this.maxHealth = maxHealth;
        this.health = health;
        if (health <= 0 || maxHealth <= 0 || health > maxHealth){
            throw new ArgumentException("Yer health is fucked.");
        }
    }

    protected abstract void OnDamage(int amount);

    protected abstract void OnHeal(int amount);

    protected abstract void OnDeath();

    public int GetHealth() {
        return this.health;
    }

    public int GetMaxHealth() {
        return this.maxHealth;
    }

    public decimal GetHealthPercent() {
        return (decimal) health / maxHealth;
    }

}
