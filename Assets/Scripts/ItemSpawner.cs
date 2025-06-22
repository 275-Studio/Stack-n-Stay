using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject itemPrefab;
    public bool itemDropped = true;

    private GameObject previewItem;

    void Update()
    {
        if (!itemDropped && previewItem != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Ikuti posisi X dan Y mouse
            previewItem.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                DropPreviewItem();
            }
        }
    }

    public void SpawnItem(GameObject prefab)
    {
        itemPrefab = prefab;
        itemDropped = false;

        Vector3 spawnPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = 0;

        previewItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        // Nonaktifkan physics
        var rb = previewItem.GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

        // Nonaktifkan collider untuk preview
        ToggleCollider(previewItem, false);

        // Tambahkan transparansi preview
        SetAlpha(previewItem, 0.5f);
    }

    private void DropPreviewItem()
    {
        if (previewItem == null) return;

        // Aktifkan collider
        ToggleCollider(previewItem, true);

        // Aktifkan physics
        var rb = previewItem.GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;

        // Kembalikan transparansi ke normal
        SetAlpha(previewItem, 1f);

        // Jalankan logic drop
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
        // Ubah semua collider (termasuk anak-anaknya)
        foreach (var col in obj.GetComponentsInChildren<Collider2D>())
        {
            col.enabled = state;
        }
    }
}
