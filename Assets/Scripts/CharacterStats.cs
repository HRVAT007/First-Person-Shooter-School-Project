using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //character stats
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    //objects to hide and show
    public GameObject objectToHide;
    public GameObject objectToShow;

    //bool
    [SerializeField] protected bool isDead;
    [SerializeField] protected bool isBuilding;

    private void Start()
    {
            InitialVariables();
    }

    public virtual void CheckHealth()
    {
        if(health <= 0 && isBuilding == false)
        {
            health = 0;
            Die();
        }

        if(health <= 0 && isBuilding == true)
        {
            Destroy(objectToHide);
            objectToShow.SetActive(true);
        }

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }

    public virtual void Building()
    {
        isBuilding = true;
    }

    public void SetHealthTo(int healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitialVariables()
    {
            maxHealth = 100;
            SetHealthTo(100);
            isDead = false;
        isBuilding = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
