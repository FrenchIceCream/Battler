using System;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    [SerializeField, SerializeReference]
    IAbilityInterface abilityInterface;

    public void ActivateAbility(BaseCharacter character)
    {
        abilityInterface?.ActivateAbility(character, this);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return (int)(abilityInterface?.Apply(player, enemy));
    }
}

public interface IAbilityInterface
{
    public int Apply(Player player = null, Enemy enemy = null);
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO);
}


//==============================ROGUE==============================

public class RogueAbility1 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
        Debug.Log("Oh my god");
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return player.GetStats().Dexterity > enemy.GetStats().Dexterity ? 1 : 0;
    }
}

public class RogueAbility2 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.GetStats().Dexterity += 1;
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return 0;
    }
}

public class RogueAbility3 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
    }
    public int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber - 1;
    }
}



//==============================WARRIOR==============================

public class WarriorAbility1 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber == 1 ? player.GetWeaponSO().damageAmount : 0;
    }
}

public class WarriorAbility2 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddDamageTakenAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return player.GetStats().Strength > enemy.GetStats().Strength ? -3 : 0;
    }
}

public class WarriorAbility3 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.GetStats().Strength += 1;
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return 0;
    }
}


//==============================BARBARIAN==============================
public class BarbarianAbility1 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber < 3 ? 3 : -1;
    }
}

public class BarbarianAbility2 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddDamageTakenAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return -player.GetStats().Dexterity;
    }
}

public class BarbarianAbility3 : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.GetStats().Stamina += 1;
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return 0;
    }
}



//==============================ENEMIES==============================

public class SkeletonAbility : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddDamageTakenAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        if (player.GetWeaponSO().damageType == DamageType.Bludgeoning)
            return player.GetStats().Strength + player.GetWeaponSO().damageAmount;
        return 0;
    }
}

public class SlimeAbility : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddDamageTakenAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        if (player.GetWeaponSO().damageType == DamageType.Slashing)
            return -player.GetWeaponSO().damageAmount;
        return 0;
    }
}

public class GhostAbility : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return enemy.GetStats().Dexterity > player.GetStats().Dexterity ? 1 : 0;
    }
}

public class GolemAbility : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddDamageTakenAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return -enemy.GetStats().Dexterity;
    }
}

public class DragonAbility : IAbilityInterface
{
    public void ActivateAbility(BaseCharacter character, AbilitySO abilitySO)
    {
        character.AddApplyingDamageAbilities(abilitySO);
    }

    public int Apply(Player player = null, Enemy enemy = null)
    {
        return GameManager.roundNumber % 3 == 0 ? 3 : 0;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(AbilitySO))]
public sealed class AbilityEditor : Editor
{
    private SerializedProperty _AbilityProperty;
    private SerializedProperty _AbilityNameProperty;
    private SerializedProperty _AbilityDescProperty;
    private GUIContent _AbilityTypeContent;

    private void OnEnable()
    {
        var so = this.serializedObject;
        _AbilityProperty = so.FindProperty("abilityInterface");
        UpdateDropdownContent(_AbilityProperty);

        _AbilityNameProperty = so.FindProperty("abilityName");
        _AbilityDescProperty = so.FindProperty("abilityDescription");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_AbilityNameProperty, new GUIContent("Name"));
        EditorGUILayout.PropertyField(_AbilityDescProperty, new GUIContent("Description"));

        if (EditorGUILayout.DropdownButton(_AbilityTypeContent, FocusType.Keyboard))
        {
            OpenAbilitySelector();
        }

        var Ability = _AbilityProperty.managedReferenceValue;
        if (Ability != null)
        {
            EditorGUILayout.PropertyField(_AbilityProperty, _AbilityTypeContent, includeChildren: true);
        }
    }

    private void OpenAbilitySelector()
    {
        var genericMenu = new GenericMenu();

        var types = TypeCache.GetTypesDerivedFrom<IAbilityInterface>();
        foreach (var type in types)
        {
            genericMenu.AddItem(new GUIContent(type.Name), false, UpdateSelectedType, type);
        }

        genericMenu.ShowAsContext();
    }

    private void UpdateSelectedType(System.Object obj)
    {
        var type = (Type)obj;
        var constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor != null)
        {
            var value = constructor.Invoke(null);
            _AbilityProperty.managedReferenceValue = value;
            this.serializedObject.ApplyModifiedProperties();
            UpdateDropdownContent(_AbilityProperty);
        }
    }

    private void UpdateDropdownContent(SerializedProperty property)
    {
        var value = property.managedReferenceValue;
        _AbilityTypeContent = value != null ? new GUIContent(value.GetType().Name) : new GUIContent("Select Ability");
    }
}
#endif