using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMPButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI tmpText;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;

    void Start()
    {
        if (tmpText != null)
        {
            Color normal = normalColor;
            normal.a = 1f;
            tmpText.color = normal;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tmpText != null)
        {
            Color c = hoverColor;
            c.a = 1f; 
            tmpText.color = c;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tmpText != null)
        {
            Color c = normalColor;
            c.a = 1f; 
            tmpText.color = c;
        }
    }
}

