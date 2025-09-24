using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu()]
public class CharacterClassSO : ScriptableObject
{
    public int healthForLevel;
    public WeaponSO defaultWeapon;
    [SerializeReference] public List<AbilitySO> abilities;// = new List<AbilitySO>();
}
