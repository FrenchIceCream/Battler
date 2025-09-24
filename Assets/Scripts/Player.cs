using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : BaseCharacter
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Weapon weapon;

    List<AbilitySO> DamageTakenAbilities = new List<AbilitySO>();
    List<AbilitySO> ApplyingDamageAbilities = new List<AbilitySO>();

    //This function adds buffs that player uses when they're being attacked
    public void AddDamageTakenAbilities(AbilitySO abilitySO)
    {
        DamageTakenAbilities.Add(abilitySO);
    }

    //This function adds buffs that player uses when they're attacking the enemy
    public void AddApplyingDamageAbilities(AbilitySO abilitySO)
    {
        ApplyingDamageAbilities.Add(abilitySO);
    }

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
