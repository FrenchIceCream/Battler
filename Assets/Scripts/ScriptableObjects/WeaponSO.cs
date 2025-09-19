using UnityEngine;

public enum DamageType
{ 
    Slashing,
    Piercing,
    Bludgeoning,
    None
}

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public DamageType damageType;
    public int damageAmount;
}
