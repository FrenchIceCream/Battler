using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassPanelSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI classNameText;
    [SerializeField] TextMeshProUGUI classLevelText;
    [SerializeField] TextMeshProUGUI abilityNameText;
    [SerializeField] TextMeshProUGUI abilityDescText;
    public Button chooseButton;

    public void SetFromCharacterClassSO(CharacterClassSO characterClassSO, int level = 1)
    {
        classNameText.text = characterClassSO.className;
        classLevelText.text = level.ToString();
        abilityNameText.text = characterClassSO.abilities[level - 1].abilityName;
        abilityDescText.text = characterClassSO.abilities[level - 1].abilityDescription;
    }
}
