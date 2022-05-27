using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;

    private void Start()
    {
        GetReferences();
        InitialVariables();
    }

    private void Update()
    {
        if (isDead)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GetReferences()
    {
        hud = GetComponent<PlayerHUD>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }
}
