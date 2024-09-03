using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Difficulty
{
    public float maxTime;
    public float difficulty;
}

public class DifficultySystem : MonoBehaviour
{
    public float timer = 0;
    public float difficulty = 1;
    public Difficulty[] difficulties;

    public void Update()
    {
        timer += Time.deltaTime;
    }
}
