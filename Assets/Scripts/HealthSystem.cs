using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //public Image healthBar;
    public float healthAmount = 100;

    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
    }

    public void Healing(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        //healthBar.fillAmount = healthAmount / 100;
    }
}