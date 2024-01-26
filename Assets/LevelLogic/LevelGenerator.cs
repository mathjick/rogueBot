using System.Linq;
using UnityEngine;

public struct UsedTile
{
    int x, y;
    GameObject BLock;
}

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] Blocks;
    public GameObject[] PossibleSpawnPoints;
    public UsedTile[] Tiles;
    public int nbrOfTiles;

    private void Start()
    {
        
    }

    public void GenerateLevel()
    {
        KillDoubleInSpawnPoints();
        for (int i = 0; i < nbrOfTiles; i++)
        {

        }
    }

    public void KillDoubleInSpawnPoints()
    {
        var _clearedSpawns = PossibleSpawnPoints[0..0];
        foreach (var item in Blocks)
        {
            if (!_clearedSpawns.Contains(item))
            {
                _clearedSpawns.Append(item);
            }
        }
        PossibleSpawnPoints = _clearedSpawns;
    }
}
