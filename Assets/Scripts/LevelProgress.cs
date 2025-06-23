using System.Collections.Generic;
using UnityEngine;

public static class LevelProgress
{
    public static void Load(List<LevelData> levels)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].isCompleted = PlayerPrefs.GetInt($"LevelCompleted_{i}", 0) == 1;
            levels[i].isUnlocked = PlayerPrefs.GetInt($"LevelUnlocked_{i}", i == 0 ? 1 : 0) == 1; // buka level 0 default
        }
    }

    public static void SaveCompleted(int index)
    {
        PlayerPrefs.SetInt($"LevelCompleted_{index}", 1);
    }

    public static void UnlockNext(int index)
    {
        PlayerPrefs.SetInt($"LevelUnlocked_{index + 1}", 1);
    }
}