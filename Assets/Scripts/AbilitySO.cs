using System;
using UnityEditor.Playables;
using UnityEngine;

public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    public virtual void ActivateAbility(Player player)
    {
        Debug.Log("Activating Ability");
    }
}

//==============================BARBARIAN==============================

[CreateAssetMenu(fileName = "BarbarianAbility1", menuName = "AbilitySO/Barbarian1")]
public class BarbarianAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Barbarian1");
    }
}

[CreateAssetMenu(fileName = "BarbarianAbility2", menuName = "AbilitySO/Barbarian2")]
public class BarbarianAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Barbarian2");
    }
}

[CreateAssetMenu(fileName = "BarbarianAbility3", menuName = "AbilitySO/Barbarian3")]
public class BarbarianAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Barbarian3");
    }
}

//==============================WARRIOR==============================

[CreateAssetMenu(fileName = "WarriorAbility1", menuName = "AbilitySO/Warrior1")]
public class WarriorAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Warrior1");
    }
}

[CreateAssetMenu(fileName = "WarriorAbility2", menuName = "AbilitySO/Warrior2")]
public class WarriorAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Warrior2");
    }
}

[CreateAssetMenu(fileName = "WarriorAbility3", menuName = "AbilitySO/Warrior3")]
public class WarriorAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Warrior3");
    }
}

//==============================ROGUE==============================

[CreateAssetMenu(fileName = "RogueAbility1", menuName = "AbilitySO/Rogue1")]
public class RogueAbility1 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Rogue1");
    }
}

[CreateAssetMenu(fileName = "RogueAbility2", menuName = "AbilitySO/Rogue2")]
public class RogueAbility2 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Rogue2");
    }
}

[CreateAssetMenu(fileName = "RogueAbility3", menuName = "AbilitySO/Rogue3")]
public class RogueAbility3 : AbilitySO
{
    public override void ActivateAbility(Player player)
    {
        Debug.Log("Rogue3");
    }
}