using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CoupleEnnemieSpawn
{
    public GameObject Ennemie;
    public GameObject SpawnPoint;
}

public class EnnemieSpawnManager : MonoBehaviour
{
    public int ActualWave = 0;
    public bool nextWaveReady = false;
    public List<GameObject> EnnemiesAlive;

    [SerializeField] private List<UnityEvent> Waves;

    private void Update()
    {
        if (nextWaveReady && EnnemiesAlive.Count() == 0)
        {
            LaunchWave();
        }
    }

    public void RemoveEnnemi(GameObject ennemi)
    {
        EnnemiesAlive.Remove(ennemi);
    }

    public void LaunchWave()
    {
        if(ActualWave < Waves.Count)
        {
            Waves[ActualWave].Invoke();
            ActualWave++;
        }
    }

    public void ReadyNextWave()
    {
        nextWaveReady = true;
    }

    public void LaunchRoom()
    {
        ReadyNextWave();
    }
}
