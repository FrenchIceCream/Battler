using UnityEngine;

public enum DamageType
{ 
    Slashing,
    Piercing,
    Bludgeoning
}

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public DamageType damageType;
    public int damageAmount;
    public GameObject weaponPrefab;
}
