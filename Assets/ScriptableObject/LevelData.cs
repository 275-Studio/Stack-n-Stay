using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Create New Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<GameObject> itemList;
    
}