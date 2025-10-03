using TMPro;
using UnityEngine;

public class StatsPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI maxHealthText;
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI dexterityText;
    [SerializeField] TextMeshProUGUI staminaText;

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetNewStats(int maxHealh, int strength = 0, int dexterity = 0, int stamina = 0)
    {
        maxHealthText.text = $"����� ��������: {maxHealh}";
        strengthText.text = $"����: {strength}";
        dexterityText.text = $"��������: {dexterity}";
        staminaText.text = $"������������: {stamina}";
    }
}
