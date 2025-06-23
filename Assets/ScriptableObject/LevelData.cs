using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Create New Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<GameObject> itemList;
    [HideInInspector] public bool isCompleted = false;
    [HideInInspector] public bool isUnlocked = false;
}