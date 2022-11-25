using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Entity
{
    public void HordeCompleted()
    {
        currentHealth = Math.Max(currentHealth += 10, maxHealth);
    }
}
