using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    public int health;
    public int damage;
    public int strength;
    public int dexterity;
    public int stamina;
    public List<AbilitySO> abilities;
    public WeaponSO weaponAward;
}
