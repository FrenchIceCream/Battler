using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : BaseCharacter
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Weapon weapon;

    public WeaponSO GetWeaponSO() { return weaponSO; }

    int playerLevel = 0;

    override protected void Start()
    {
        base.Start();

        //TODO set from outside
        weapon.SetWeapon(weaponSO);
    }

    int GetOverallDamage(BaseCharacter opponent)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in ApplyingDamageAbilities)
        {
            damageFromAbilities += abilitySO.Apply();
        }
        return weapon.GetDamage() + stats.Strength + damageFromAbilities;
    }

    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-GetOverallDamage(opponent));
        Debug.Log("Attack (player)");
    }
}
