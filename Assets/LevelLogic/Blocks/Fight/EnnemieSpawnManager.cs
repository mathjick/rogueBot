using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnnemieWeigtedSpec
{
    public GameObject Ennemie;
    public float weight = 1.0f;
}

public class EnnemieSpawnManager : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public EnnemieWeigtedSpec[] Ennemies;
    public List<GameObject> EnnemiesAlive;
    public GameObject EnnemiToSpawn;
    public int maxEnnemies = 1;
    public float SpawnSpeed = 1;
    public int nbrOfSpawn = 1;
    public bool canSpawn = false;

    [SerializeField] private UnityEvent AllEnnemiDead;

    public float lastSpawnTime = 0;
    private bool finishedSpawning = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (canSpawn && !finishedSpawning && lastSpawnTime + SpawnSpeed <= Time.time && Ennemies.Length < maxEnnemies)
        {
            lastSpawnTime = Time.time;
            SpawnAnEnnemi();
        }
    }

    public void SpawnAnEnnemi()
    {
        var newEnnemi = Instantiate(RollForEnnemie(),RollForSpawnPoint());
        newEnnemi.GetComponent<IaBase>().Spawner = this;
        EnnemiesAlive.Append(newEnnemi);
        nbrOfSpawn--;
        if(nbrOfSpawn <= 0)
        {
            canSpawn = false;
            finishedSpawning = true;
        }
    }

    public GameObject RollForEnnemie()
    {
        var total_weight = 0f;
        foreach (var enemy in Ennemies)
        {
            total_weight += enemy.weight;
        }
        var roll = Random.Range(0, total_weight);
        total_weight = 0f;
        foreach (var enemy in Ennemies)
        {
            total_weight += enemy.weight;
            if(total_weight > roll)
            {
                return enemy.Ennemie;
            }
        }
        return null;
    }

    public Transform RollForSpawnPoint()
    {
        var roll = SpawnPoints[Mathf.RoundToInt(Random.Range(0,SpawnPoints.Length))].transform;
        return roll;
    }

    public void ActivateSpawner()
    {
        if (!finishedSpawning)
        {
            this.canSpawn = true;
        }
    }

    public void RemoveEnnemi(GameObject ennemi)
    {
        EnnemiesAlive.Remove(ennemi);
    }
}
