using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DifficultyCallBackData
{
    public int difficultyLevel;
    public bool eventDone;
    public UnityEvent difficultyEvent;
}
public class DifficultyCallBack : MonoBehaviour
{
    public DifficultySystem difficultySystem;
    public List<DifficultyCallBackData> difficultyEvent = new List<DifficultyCallBackData>();

    public void UpdateDifficulty()
    {
        foreach (DifficultyCallBackData _data in difficultyEvent)
        {
            if (difficultySystem.difficultyLevel >= _data.difficultyLevel && !_data.eventDone)
            {
                _data.eventDone = true;
                _data.difficultyEvent?.Invoke();
            }
        }
    }

    public void SubscribeToDifficultySystem(DifficultySystem _system)
    {
        difficultySystem = _system;
        difficultySystem.difficultyCallBacks.Add(this);
        UpdateDifficulty();
    }
}
