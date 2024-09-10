using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyCallBack : MonoBehaviour
{
    public DifficultySystem difficultySystem;
    public Difficulty difficulty;
    public UnityEvent difficultyEvent;

    public void UpdateDifficulty()
    {
        difficulty = difficultySystem.getDifficulty();
        difficultyEvent.Invoke();
    }

    public void SubscribeToDifficultySystem(DifficultySystem _system)
    {
        difficultySystem = _system;
        difficultySystem.difficultyCallBacks.Add(this);
    }

    public void PouetTesting()
    {
        Debug.Log("Pouet");
    }
}
