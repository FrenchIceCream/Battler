using System;
using System.Collections.Generic;
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

        Dictionary<string, int> classesDict = Player.Instance.GetClassNamesAndLevels();

        foreach (CharacterClassSO characterClassSO in Player.Instance.GetPossibleCharacterClasses())
        {
            GameObject spawnedItem = Instantiate(classPanelSingle, this.transform);
            spawnedItem.SetActive(true);
            ClassPanelSingleUI classPanelSingleUI = spawnedItem.GetComponent<ClassPanelSingleUI>();
            if (classesDict.ContainsKey(characterClassSO.className))
                classPanelSingleUI.SetFromCharacterClassSO(characterClassSO, classesDict[characterClassSO.className] + 1);
            else
                classPanelSingleUI.SetFromCharacterClassSO(characterClassSO);


            classPanelSingleUI.chooseButton.onClick.AddListener(() => {
                Player.Instance.AddCharacterClass(characterClassSO);
                Hide();
                GameManager.attackState = GameManager.AttackState.Ready;
                action?.Invoke();
            });
        }

        Show();
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
