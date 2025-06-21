using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public ItemSpawner spawner;
    public Button startTruckButton;

    private int currentIndex = 0;

    void Start()
    {
        startTruckButton.gameObject.SetActive(false); 
        SpawnNextItem();
    }

    void Update()
    {
        if (spawner.itemDropped && currentIndex < currentLevel.itemList.Count)
        {
            SpawnNextItem();
        }
        else if (spawner.itemDropped && currentIndex >= currentLevel.itemList.Count)
        {
            startTruckButton.gameObject.SetActive(true);
        }
    }

    void SpawnNextItem()
    {
        spawner.SpawnItem(currentLevel.itemList[currentIndex]);
        currentIndex++;
    }
}
