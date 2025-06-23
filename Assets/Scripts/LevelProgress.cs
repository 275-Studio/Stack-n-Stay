using System.Collections.Generic;
using UnityEngine;

public static class LevelProgress
{
    public static List<LevelStatus> statusList = new List<LevelStatus>();

    public static void Load(List<LevelData> levels)
    {
        statusList.Clear();
        for (int i = 0; i < levels.Count; i++)
        {
            var status = new LevelStatus();
            status.isCompleted = PlayerPrefs.GetInt($"LevelCompleted_{i}", 0) == 1;
            status.isUnlocked = PlayerPrefs.GetInt($"LevelUnlocked_{i}", i == 0 ? 1 : 0) == 1;
            statusList.Add(status);
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
