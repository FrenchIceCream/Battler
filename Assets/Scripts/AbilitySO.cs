using System;
using System.ComponentModel;
using UnityEditor.Playables;
using UnityEngine;

public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    public virtual void ActivateAbility(Player player)
    {
        Debug.Log("Activating Ability - not overriden");
    }

    public virtual int Apply(Player player = null, Enemy enemy = null)
    {
        Debug.Log("Applying Ability - not overriden");
        return 0;
    }
}

//==============================ROGUE==============================

[CreateAssetMenu(fileName = "RogueAbility1", menuName = "AbilitySO/Rogue1")]
public class RogueAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddApplyingDamageAbilities(this);
    }

    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return player.GetStats().Dexterity > enemy.GetStats().Dexterity ? 1 : 0;
    }
}

[CreateAssetMenu(fileName = "RogueAbility2", menuName = "AbilitySO/Rogue2")]
public class RogueAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.GetStats().Dexterity += 1;
    }
}

[CreateAssetMenu(fileName = "RogueAbility3", menuName = "AbilitySO/Rogue3")]
public class RogueAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddApplyingDamageAbilities(this);
    }
    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber - 1;
    }
}



//==============================WARRIOR==============================

[CreateAssetMenu(fileName = "WarriorAbility1", menuName = "AbilitySO/Warrior1")]
public class WarriorAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddApplyingDamageAbilities(this);
    }

    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber == 1 ? player.GetWeaponSO().damageAmount : 0;
    }
}

[CreateAssetMenu(fileName = "WarriorAbility2", menuName = "AbilitySO/Warrior2")]
public class WarriorAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddDamageTakenAbilities(this);
    }

    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return player.GetStats().Strength > enemy.GetStats().Strength ? -3 : 0;
    }
}

[CreateAssetMenu(fileName = "WarriorAbility3", menuName = "AbilitySO/Warrior3")]
public class WarriorAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.GetStats().Strength += 1;
    }
}



//==============================BARBARIAN==============================

[CreateAssetMenu(fileName = "BarbarianAbility1", menuName = "AbilitySO/Barbarian1")]
public class BarbarianAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddApplyingDamageAbilities(this);
    }

    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber >= 3 ? 3 : -1;
    }
}

[CreateAssetMenu(fileName = "BarbarianAbility2", menuName = "AbilitySO/Barbarian2")]
public class BarbarianAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.AddDamageTakenAbilities(this);
    }

    public override int Apply(Player player = null, Enemy enemy = null)
    {
        return -player.GetStats().Dexterity;
    }
}

[CreateAssetMenu(fileName = "BarbarianAbility3", menuName = "AbilitySO/Barbarian3")]
public class BarbarianAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        player.GetStats().Stamina += 1;
    }
}
