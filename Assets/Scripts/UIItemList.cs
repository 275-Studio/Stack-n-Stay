using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemList : MonoBehaviour
{
    public GameObject iconPrefab;
    public Transform contentParent; 
    private List<GameObject> spawnedIcons = new List<GameObject>();

    public void ShowItemIcons(List<GameObject> items)
    {
        ClearIcons();
        foreach (var item in items)
        {
            GameObject icon = Instantiate(iconPrefab, contentParent);
            Image img = icon.GetComponent<Image>();

            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null && img != null)
            {
                img.sprite = sr.sprite;
            }

            spawnedIcons.Add(icon);
        }
    }

    // Hapus icon sesuai prefab item
    public void RemoveIcon(GameObject itemPrefab)
    {
        SpriteRenderer sr = itemPrefab.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            Image img = spawnedIcons[i].GetComponent<Image>();
            if (img != null && img.sprite != null && img.sprite.name == sr.sprite.name)
            {
                Destroy(spawnedIcons[i]);
                spawnedIcons.RemoveAt(i);
                break;
            }
        }
    }


    public void ClearIcons()
    {
        foreach (var icon in spawnedIcons)
        {
            Destroy(icon);
        }
        spawnedIcons.Clear();
    }
}