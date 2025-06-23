using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // Tambahkan ini

    public List<LevelData> levels;
    public ItemSpawner spawner;
    public Button startTruckButton;
    private List<GameObject> spawnedItems = new List<GameObject>();

    private int currentLevelIndex = 0;
    private int currentItemIndex = 0;
    private LevelData currentLevel;
    public UIItemList uiItemList;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        LoadLevel(index);
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (currentLevel == null || spawner == null) return;

        if (spawner.itemDropped)
        {
            if (currentItemIndex < currentLevel.itemList.Count)
            {
                SpawnNextItem();
                spawner.itemDropped = false;
            }
            else if (currentItemIndex >= currentLevel.itemList.Count)
            {
                startTruckButton.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levels.Count)
        {
            Debug.Log("No more levels!");
            return;
        }

        ClearPreviousItems();
        currentLevelIndex = index;
        currentLevel = levels[currentLevelIndex];
        currentItemIndex = 0;
        Time.timeScale = 1f;
        spawner.itemDropped = true;
        startTruckButton.gameObject.SetActive(false);

        if (uiItemList != null)
        {
            uiItemList.ShowItemIcons(currentLevel.itemList);
        }
    }

    void SpawnNextItem()
    {
        GameObject prefab = currentLevel.itemList[currentItemIndex];
        spawner.SpawnItem(prefab); 
        currentItemIndex++;
    }

    public void ClearPreviousItems()
    {
        foreach (var item in spawnedItems)
        {
            if (item != null)
                Destroy(item);
        }
        spawnedItems.Clear();
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex + 1);
    }

    public void RegisterSpawnedItem(GameObject item)
    {
        if (!spawnedItems.Contains(item))
            spawnedItems.Add(item);
    }
}
