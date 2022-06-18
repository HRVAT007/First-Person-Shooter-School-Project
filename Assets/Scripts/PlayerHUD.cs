using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text maxHealthText;
    [SerializeField] private Text currentZombieCountText;

    private void Update()
    {
        ZombieCounter();
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    private void ZombieCounter()
    {
        int numberOfZombies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        currentZombieCountText.text = numberOfZombies.ToString();
    }
}
