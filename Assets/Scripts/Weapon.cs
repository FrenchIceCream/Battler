using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SpriteRenderer weaponSpriteRenderer;
    WeaponSO weaponSO;
    public void SetWeapon(WeaponSO weaponSO)
    {
        weaponSpriteRenderer.sprite = weaponSO.weaponSprite;
        this.weaponSO = weaponSO;
    }

    public void ClearWeapon()
    {
        weaponSpriteRenderer.sprite = null;
        weaponSO = null;
    }

    public WeaponSO GetWeaponSO() { return weaponSO; }

    public int GetDamage() { return weaponSO.damageAmount; }

    public DamageType GetDamageType() { return weaponSO.damageType; }
}
