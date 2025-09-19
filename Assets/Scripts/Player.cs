using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : BaseCharacter
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Weapon weapon;



    //public event EventHandler<OnWeaponUpdatedArgs> OnWeaponUpdated;
    //public class OnWeaponUpdatedArgs : EventArgs
    //{
    //    public WeaponSO weaponSO;
    //}

    override protected void Start()
    {
        base.Start();

        //TODO set from outside
        weapon.SetWeapon(weaponSO);
    }

    int GetOverallDamage()
    {
        return stats.Strength + weapon.GetDamage();
    }

    override protected void DoDamageToOpponent(BaseCharacter opponent)
    {
        opponent.AddHealth(-GetOverallDamage());
        Debug.Log("Attack (player)");
    }

    void ActivateEffects()
    {
        //TODO;
    }
}
