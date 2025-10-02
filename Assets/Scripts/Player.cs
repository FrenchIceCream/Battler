using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : BaseCharacter
{
    [SerializeField] Weapon weapon;

    public static Player Instance { get; private set; }

    [SerializeField] List<CharacterClassSO> possibleCharacterClasses;
    Dictionary<string, int> characterClasses;

    public List<CharacterClassSO> GetPossibleCharacterClasses() { return possibleCharacterClasses; }
    public Dictionary<string, int> GetClassNamesAndLevels() { return characterClasses; }
    
    public WeaponSO GetWeaponSO() { return weapon.GetWeaponSO(); }
    public void SetWeapon(WeaponSO weaponSO)    { weapon.SetWeapon(weaponSO); }
    public void ResetHealth()   { healthComp.ResetHealth(); }


    int playerLevel = 0;
    public int GetPlayerLevel() { return playerLevel; }

    override protected void Awake()
    {
        base.Awake();
        stats.SetInitialStats();

        characterClasses = new Dictionary<string, int>();

        if (Instance == null)
            Instance = this;

        foreach (CharacterClassSO charClass in possibleCharacterClasses)
        {
            characterClasses.Add(charClass.name, 0);
        }
    }

    override protected void Start()
    {
        base.Start();
    }

    protected override int GetOverallDamage(BaseCharacter opponent)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in ApplyingDamageAbilities)
        {
            damageFromAbilities += abilitySO.Apply(this, opponent as Enemy);
        }
        return weapon.GetDamage() + stats.Strength + damageFromAbilities;
    }

    protected override int GetDamageTakenFromOpponent(BaseCharacter opponent)
    {
        int damageFromAbilities = 0;
        foreach (AbilitySO abilitySO in DamageTakenAbilities)
        {
            damageFromAbilities += abilitySO.Apply(this, opponent as Enemy);
        }
        return damageFromAbilities;
    }
    

    public void AddCharacterClass(CharacterClassSO characterClassSO)
    {
        if (playerLevel == 0)
        {
            characterClasses.Add(characterClassSO.className, 1);
            weapon.SetWeapon(characterClassSO.defaultWeapon);
        }
        else
        {
            if (characterClasses.ContainsKey(characterClassSO.className))
                characterClasses[characterClassSO.className]++;
            else
                characterClasses.Add(characterClassSO.className, 1);
        }

        healthComp.SetMaxHealth(healthComp.GetMaxHealth() + characterClassSO.healthForLevel + stats.Stamina);
        int value = characterClasses[characterClassSO.className];
        characterClassSO.abilities[value - 1].ActivateAbility(this);

        playerLevel++;
    }
}
