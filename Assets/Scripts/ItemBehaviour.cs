using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasDropped = false;
    private bool isParented = false;
    private bool isGameOverTriggered = false;

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
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collided with: " + collision.collider.name + " | Tag: " + collision.collider.tag);
        if (isParented || isGameOverTriggered) return;
        if (collision.collider.CompareTag("Road")){
            Debug.Log("Game Over");
            isGameOverTriggered = true;
            GameManager.Instance.GameOver();
        }
    }
}