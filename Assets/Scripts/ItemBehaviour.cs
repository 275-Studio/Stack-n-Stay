using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasDropped = false;
    private bool isParented = false;
    private bool isGameOverTriggered = false;
    public GameObject originPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Drop()
    {
        if (hasDropped) return;

        rb.bodyType = RigidbodyType2D.Dynamic;
        hasDropped = true;

        if (GameManager.Instance != null && originPrefab != null)
        {
            GameManager.Instance.levelManager.uiItemList.RemoveIcon(originPrefab);
        }

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.RegisterSpawnedItem(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isParented || isGameOverTriggered) return;

        if (collision.collider.CompareTag("Road") || collision.collider.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            isGameOverTriggered = true;
        }
    }
}