using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu()]
public class CharacterClassSO : ScriptableObject
{
    public string className;
    public int healthForLevel;
    public WeaponSO defaultWeapon;
    [SerializeReference] public List<AbilitySO> abilities;
}
