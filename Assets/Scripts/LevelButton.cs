using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI levelText;
    public Image lockIcon; 
    public Image backgroundImage;

    private int levelIndex;

    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color lockedColor;

    public void Setup(LevelData levelData, int index)
    {
        levelIndex = index;
        var status = LevelProgress.statusList[index];
        bool isUnlocked = status.isUnlocked;
        button.interactable = isUnlocked;
        button.onClick.RemoveAllListeners();
        if (backgroundImage != null)
        {
            backgroundImage.color = isUnlocked ? unlockedColor : lockedColor;
        }
        levelText.text = (index + 1).ToString();
        if (levelText != null)
        {
            levelText.gameObject.SetActive(isUnlocked);
        }
        if (lockIcon != null)
        {
            lockIcon.gameObject.SetActive(!isUnlocked);
        }
        if (isUnlocked)
        {
            button.onClick.AddListener(() =>
            {
                LevelSelectManager.Instance.PlayLevel(index);
            });
        }
    }
}