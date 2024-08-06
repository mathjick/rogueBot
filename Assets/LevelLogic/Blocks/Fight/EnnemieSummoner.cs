using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieSummoner : MonoBehaviour
{
    public EnnemieSpawnManager SpawnManager;
    public List<GameObject> EnnemiesInvocables;

    public void InvoqueEnnemie(int _index)
    {
        GameObject _newEnnemi = Instantiate(EnnemiesInvocables[_index], transform.position, Quaternion.identity);
        _newEnnemi.GetComponent<IaBase>().Spawner = SpawnManager;
        SpawnManager.EnnemiesAlive.Add(_newEnnemi);
    }
}
