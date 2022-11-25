using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectiles")]

public class ProjectileScriptableObject : ItemScriptableObject
{
    public TrashType trashType;
}
