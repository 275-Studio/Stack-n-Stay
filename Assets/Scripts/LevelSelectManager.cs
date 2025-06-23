using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;

    public GameObject levelButtonPrefab;
    public List<LevelData> levels;
    [SerializeField] Transform easyContainerTop;
    [SerializeField] Transform easyContainerBottom;
    [SerializeField] Transform mediumContainerTop;
    [SerializeField] Transform mediumContainerBottom;
    [SerializeField] Transform hardContainerTop;
    [SerializeField] Transform hardContainerBottom;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LevelProgress.Load(levels);
        LoadLevelButtons();
    }
    void LoadLevelButtons()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject obj = Instantiate(levelButtonPrefab);
            var button = obj.GetComponent<LevelButton>();
            button.Setup(levels[i], i);

            if (i < 10)
            {
                if (i < 5)
                {
                    obj.transform.SetParent(easyContainerTop, false);
                    obj.transform.SetSiblingIndex(0); // insert paling kiri → jadi urut kanan ke kiri
                }
                else
                {
                    obj.transform.SetParent(easyContainerBottom, false);
                    obj.transform.SetSiblingIndex(0);
                }
            }
            else if (i < 20)
            {
                if (i < 15)
                {
                    obj.transform.SetParent(mediumContainerTop, false);
                    obj.transform.SetSiblingIndex(0);
                }
                else
                {
                    obj.transform.SetParent(mediumContainerBottom, false);
                    obj.transform.SetSiblingIndex(0);
                }
            }
            else
            {
                if (i < 25)
                {
                    obj.transform.SetParent(hardContainerTop, false);
                    obj.transform.SetSiblingIndex(0);
                }
                else
                {
                    obj.transform.SetParent(hardContainerBottom, false);
                    obj.transform.SetSiblingIndex(0);
                }
            }
        }
    }
    public void PlayLevel(int index)
    {
        PlayerPrefs.SetInt("SelectedLevelIndex", index);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
