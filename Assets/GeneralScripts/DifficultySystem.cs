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
    public int difficultyLevel = 0;
    public List<Difficulty> difficulties;
    public List<DifficultyCallBack> difficultyCallBacks;

    public void Start()
    {
        foreach (DifficultyCallBack d in FindObjectsOfType<DifficultyCallBack>())
        {
            d.SubscribeToDifficultySystem(this);
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > difficulties[difficultyLevel].maxTime)
        {
            difficultyLevel++;
            foreach (DifficultyCallBack d in difficultyCallBacks)
            {
                d.UpdateDifficulty();
            }
        }
    }
}
