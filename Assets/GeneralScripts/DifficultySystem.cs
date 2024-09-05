using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Difficulty
{
    public float maxTime;
    public float difficulty;
}

public class DifficultySystem : MonoBehaviour
{
    public float timer = 0;
    public Difficulty difficulty;
    public List<Difficulty> difficulties;
    public List<DifficultyCallBack> difficultyCallBacks;

    public void Start()
    {
        difficulty = difficulties.First();
        foreach (DifficultyCallBack d in FindObjectsOfType<DifficultyCallBack>())
        {
            d.SubscribeToDifficultySystem(this);
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > difficulty.maxTime)
        {
            difficulty = getDifficulty();
            foreach (DifficultyCallBack d in difficultyCallBacks)
            {
                d.UpdateDifficulty();
            }
        }
    }

    public Difficulty getDifficulty()
    {
        Difficulty _realDifficulty = difficulties.First();
        foreach (Difficulty d in difficulties)
        {
            if (timer < d.maxTime)
            {
                _realDifficulty = d;
                break;
            }
        }
        return _realDifficulty;
    }
}
