using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnnemieWeigtedSpec
{
    public GameObject Ennemie;
    public float weight = 1.0f;
}

public class EnnemieSpawnManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public List<EnnemieWeigtedSpec> EnnemiesRollChance = new List<EnnemieWeigtedSpec>();
    public int maxEnnemies = 1;
    public float SpawnSpeed = 1;
    public int nbrOfSpawn = 1;
    public List<GameObject> EnnemiesAlive;

    [SerializeField] private UnityEvent AllEnnemiDead;

    public void SpawnAnEnnemi()
    {
        if (nbrOfSpawn > 0)
        {
            GameObject newEnnemi = Instantiate(RollForEnnemie(), RollForSpawnPoint().position, RollForSpawnPoint().rotation);
            newEnnemi.GetComponent<IaBase>().Spawner = this;
            newEnnemi.GetComponent<LifeSystem>().lastSourceOfDamage = PlayerController.instance.gameObject;
            EnnemiesAlive.Add(newEnnemi);
            nbrOfSpawn--;
        }
    }

    public GameObject RollForEnnemie()
    {
        //add up all the weights and pick a random number between 0 and the total
        float total = EnnemiesRollChance.Sum(weight => weight.weight);
        float randomPoint = Random.value * total;
        //go through all the items adding the weight to the runningTotal until we reach the randomPoint
        for (int i = 0; i < EnnemiesRollChance.Count; i++)
        {
            if (randomPoint < EnnemiesRollChance[i].weight)
            {
                return EnnemiesRollChance[i].Ennemie;
            }
            else
            {
                randomPoint -= EnnemiesRollChance[i].weight;
            }
        }
        return null;
    }

    public Transform RollForSpawnPoint()
    {
        //return a random spawn point beetwen the list of spawn points
        return SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform;
    }

    public void RemoveEnnemi(GameObject ennemi)
    {
        EnnemiesAlive.Remove(ennemi);
        if (EnnemiesAlive.Count == 0 && nbrOfSpawn <= 0)
        {
            AllEnnemiDead.Invoke();
        }
    }

    public void CallBackSpawn()
    {
        if (maxEnnemies > EnnemiesAlive.Count && nbrOfSpawn > 0)
        {
            SpawnAnEnnemi();
        }
        if (nbrOfSpawn > 0)
        {
            Invoke("CallBackSpawn", SpawnSpeed);
        }
    }
}
