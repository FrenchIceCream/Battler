using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SpriteRenderer weaponSpriteRenderer;
    int damageAmount;
    DamageType damageType;

    public void SetWeapon(WeaponSO weaponSO)
    {
        weaponSpriteRenderer.sprite = weaponSO.weaponSprite;
        damageAmount = weaponSO.damageAmount;
        damageType = weaponSO.damageType;
    }

    public void ClearWeapon()
    {
        weaponSpriteRenderer.sprite = null;
        damageType = DamageType.None;
        damageAmount = 0;
    }

    public int GetDamage() { return damageAmount; }

    public DamageType GetDamageType() { return damageType; }
}
