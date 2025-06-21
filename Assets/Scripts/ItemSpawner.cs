using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject itemPrefab;
    public bool itemDropped = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !itemDropped)
        {
            Vector3 clickWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickWorldPos.z = 0;

            GameObject spawnedItem = Instantiate(itemPrefab, clickWorldPos, Quaternion.identity);

            spawnedItem.GetComponent<ItemBehaviour>().Drop();

            itemDropped = true;
        }
    }

    public void SpawnItem(GameObject prefab)
    {
        itemPrefab = prefab;
        itemDropped = false; 
    }
}