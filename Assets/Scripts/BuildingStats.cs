using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : CharacterStats
{
    public override void InitialVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
        isBuilding = true;
    }
}
