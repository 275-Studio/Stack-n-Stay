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
        if (isParented || isGameOverTriggered) return;
        Debug.Log("Kena: " + collision.collider.name);
        if (collision.collider.CompareTag("Truck"))
        {
            transform.SetParent(collision.transform);
            isParented = true;
        }else if (collision.collider.CompareTag("Item"))
        {
            Transform other = collision.transform;
            if (other.parent != null && other.parent.CompareTag("Truck"))
            {
                transform.SetParent(other.parent);
                isParented = true;
            }
        }else if (collision.collider.CompareTag("Road"))
        {
            Debug.Log("Game Over");
            isGameOverTriggered = true;
            GameManager.Instance.GameOver();
        }
    }
}