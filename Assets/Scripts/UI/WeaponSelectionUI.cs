using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject panelGameObject;
    [SerializeField] WeaponSelectionSingleUI currentWeaponSingleUI;
    [SerializeField] WeaponSelectionSingleUI newWeaponSingleUI;
    [SerializeField] ClassSelectionUI classSelectionUI;

    public void SetWeaponOnCards(WeaponSO currentWeaponSO, WeaponSO newWeaponSO)
    {
        currentWeaponSingleUI.SetWeaponOnCard(currentWeaponSO);
        newWeaponSingleUI.SetWeaponOnCard(newWeaponSO);

        newWeaponSingleUI.chooseButton.onClick.RemoveAllListeners();
        newWeaponSingleUI.chooseButton.onClick.AddListener(() => { 
            Player.Instance.SetWeapon(newWeaponSO); 
            Hide();
        });
    }

    public void Show()
    {
        panelGameObject.SetActive(true);
    }

    public void Hide()
    {
        panelGameObject.SetActive(false);
    }
}
