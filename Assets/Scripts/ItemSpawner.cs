using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject itemPrefab;
    public bool itemDropped = true;

    private GameObject previewItem;
    [Header("Sound Settings")]
    public AudioClip dropSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!itemDropped && previewItem != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            previewItem.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (dropSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(dropSound);
                }
                DropPreviewItem();
            }
        }
    }

    public GameObject SpawnItem(GameObject prefab)
    {
        itemPrefab = prefab;
        itemDropped = false;
        Vector3 spawnPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = 0;
        previewItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        var rb = previewItem.GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;
        ToggleCollider(previewItem, false);
        SetAlpha(previewItem, 0.5f);
        ItemBehaviour behaviour = previewItem.GetComponent<ItemBehaviour>();
        if (behaviour != null) behaviour.originPrefab = prefab;
        return previewItem; 
    }


    private void DropPreviewItem()
    {
        if (previewItem == null) return;

        ToggleCollider(previewItem, true);

        var rb = previewItem.GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;

        SetAlpha(previewItem, 1f);
        previewItem.GetComponent<ItemBehaviour>().Drop();

        previewItem = null;
        itemDropped = true;
    }

    private void SetAlpha(GameObject obj, float alpha)
    {
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            var c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    private void ToggleCollider(GameObject obj, bool state)
    {
        foreach (var col in obj.GetComponentsInChildren<Collider2D>())
        {
            col.enabled = state;
        }
    }
}
