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
    public int currentLevelIndex;
    public List<LevelTreshold> LevelTresholds;

    private void Start()
    {
        AddExp(0);
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        if (LevelTresholds[currentLevelIndex].treshold <= this.exp)
        {
            currentLevelIndex++;
            LevelTresholds[currentLevelIndex].levelEvent.Invoke();
        }
    }
}
