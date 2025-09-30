using System;
using UnityEngine;
using UnityEngine.Events;

public class ClassSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject panelGameObject;
    [SerializeField] GameObject classPanelSingle;
    
    private void Start()
    {
        SetCharacterClasses();
    }

    public void SetCharacterClasses(UnityAction action = null)
    {
        foreach (Transform child in transform)
        {
            if (classPanelSingle != child.gameObject)
                Destroy(child.gameObject);
        }

        foreach (CharacterClassSO characterClassSO in Player.Instance.GetPossibleCharacterClasses())
        {
            GameObject spawnedItem = Instantiate(classPanelSingle, this.transform);
            ClassPanelSingleUI classPanelSingleUI = spawnedItem.GetComponent<ClassPanelSingleUI>();
            classPanelSingleUI.SetFromCharacterClassSO(characterClassSO);

            classPanelSingleUI.chooseButton.onClick.AddListener(() => {
                Player.Instance.AddCharacterClass(characterClassSO);
                Hide();
                GameManager.attackState = GameManager.AttackState.Ready;
                action?.Invoke();
            });

            spawnedItem.SetActive(true);
        }
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
