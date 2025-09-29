using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] Image weaponSprite;
    public Button chooseButton;


    public void SetWeaponOnCard(WeaponSO weaponSO)
    {
        damageText.text = $"Урон: {weaponSO.damageAmount}";
        weaponSprite.sprite = weaponSO.weaponSprite;
    }
}
