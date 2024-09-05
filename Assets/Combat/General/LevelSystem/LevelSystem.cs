using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelTreshold
{
    public int level;
    public int treshold;
    public UnityEvent levelEvent;
}
public class LevelSystem : MonoBehaviour
{
    public int exp;
    public LevelTreshold currentLevel;
    public List<LevelTreshold> LevelTresholds;

    public int GetLevel()
    {
        int level = 0;
        foreach (LevelTreshold treshold in LevelTresholds)
        {
            if (exp >= treshold.treshold)
            {
                level = treshold.level;
            }
        }
        return level;
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        if (GetLevel() > currentLevel.level)
        {
            currentLevel = LevelTresholds.Find(x => x.level == GetLevel());
            currentLevel.levelEvent.Invoke();
        }
    }
}
