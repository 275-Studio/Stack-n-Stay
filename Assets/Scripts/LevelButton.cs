using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI levelText;
    public GameObject lockIcon;

    private int levelIndex;

    public void Setup(LevelData levelData, int index)
    {
        levelIndex = index;
        levelText.text = (index + 1).ToString();

        var status = LevelProgress.statusList[index];
        bool isUnlocked = status.isUnlocked;

        button.interactable = isUnlocked;
        lockIcon.SetActive(!isUnlocked);

        button.onClick.AddListener(() =>
        {
            if (isUnlocked)
            {
                LevelSelectManager.Instance.PlayLevel(index);
            }
        });
    }
}
