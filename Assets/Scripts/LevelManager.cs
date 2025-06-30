using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 

    public List<LevelData> levels;
    public ItemSpawner spawner;
    public Button startTruckButton;
    private List<GameObject> spawnedItems = new List<GameObject>();

    private int currentLevelIndex = 0;
    private int currentItemIndex = 0;
    private LevelData currentLevel;
    public LevelData CurrentLevel => currentLevel;
    public UIItemList uiItemList;
    public Transform obstacleParent;
    public Vector2 obstacleStartPos = new Vector2(-2.37f, -3.76f);
    public float obstacleSpacing = 8f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        LoadLevel(index);
        Debug.Log(index);
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
        GameManager.Instance?.SpawnVehicleFromLevelData();
        SpawnObstacles();
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
    public GameObject GetVehiclePrefab()
    {
        return currentLevel != null ? currentLevel.vehiclePrefab : null;
    }
    public void SpawnObstacles()
    {
        if (currentLevel == null || currentLevel.ObstacleList == null) return;

        Vector2 spawnPos = obstacleStartPos;

        for (int i = 0; i < currentLevel.ObstacleList.Count; i++)
        {
            GameObject obstaclePrefab = currentLevel.ObstacleList[i];
            if (obstaclePrefab == null) continue;

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

            if (obstacleParent != null)
                obstacle.transform.parent = obstacleParent;

            RegisterSpawnedItem(obstacle); 

            spawnPos.x += obstacleSpacing; 
        }
    }

}
