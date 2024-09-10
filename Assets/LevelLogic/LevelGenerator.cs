using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

[System.Serializable]
public class UsedTile
{
    public Vector3 position;
    public GameObject blockLocked;
    public bool isUsed;
    public bool isOrigin;
    public (int,int) coordinates;
}

[System.Serializable]
public class TileRow
{
    public List<UsedTile> tiles;
}

[System.Serializable]
public class BlockPercentGeneration
{
    public GameObject block;
    [Range(0,1)]
    public float chanceToGenerate;
    public int minimumToGenerate;
    public int maximumToGenerate;
    public bool originSuitable;
    public bool classicSuitable;
}

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    public GameObject player;
    public List<TileRow> PossibleSpawnPoints = new List<TileRow>();
    public List<BlockPercentGeneration> BaseBlocks;
    public List<GameObject> BlockToGenerateAfter;
    public int maxNbrBlocks;
    [Range(0,1)]
    public float chaosFactor;
    [Range(0,1)]
    public float elongatedFactor;

    public List<Vector3> origines = new List<Vector3>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GenerateWholeLevel();
    }

    public void GenerateWholeLevel()
    {
        if(chaosFactor == 1)
        {
            chaosFactor = 0.99f;
        }
        if(elongatedFactor == 1)
        {
            elongatedFactor = 0.99f;
        }
        // initAllCoordinates
        for (int x = 0; x < PossibleSpawnPoints.Count; x++)
        {
            for (int y = 0; y < PossibleSpawnPoints[x].tiles.Count; y++)
            {
                PossibleSpawnPoints[x].tiles[y].coordinates = (x, y);
            }
        }
        int _nbrBlocks = 0;
        //first round, generate origins
        for(int x = 0; x< PossibleSpawnPoints.Count; x++)
        {
            for(int y = 0; y < PossibleSpawnPoints[x].tiles.Count; y++)
            {
                if (PossibleSpawnPoints[x].tiles[y].isOrigin)
                {
                    origines.Add(PossibleSpawnPoints[x].tiles[y].position);
                    RollRandomBLock(PossibleSpawnPoints[x].tiles[y],(x,y));
                    _nbrBlocks++;
                }
            }
        }

        //second round, generate the rest and repeat until maxNbrBlocks is reached
        bool _generatingBase = true;
        while (_generatingBase)
        {
            List<(int,int)> possibleSpawnPlaces = new List<(int, int)>();
            for (int x = 0; x < PossibleSpawnPoints.Count; x++)
            {
                for (int y = 0; y < PossibleSpawnPoints[x].tiles.Count; y++)
                {
                    if (!PossibleSpawnPoints[x].tiles[y].isUsed)
                    {
                        if (UsedBlockNear((x, y)))
                        {
                            possibleSpawnPlaces.Add((x, y));
                        }
                    }
                }
            }
            if (possibleSpawnPlaces.Count > 0)
            {
                int _nbrOfBlockToRemove = Mathf.FloorToInt(possibleSpawnPlaces.Count * chaosFactor);
                if( possibleSpawnPlaces.Count - _nbrOfBlockToRemove >= 1)
                {
                    Debug.Log("Sorting");
                    possibleSpawnPlaces.Sort((x, y) => distanceBetweenTwoPoints(new Vector2(x.Item1, x.Item2), new Vector2(origines[0].x, origines[0].y)) < distanceBetweenTwoPoints(new Vector2(y.Item1, y.Item2), new Vector2(origines[0].x, origines[0].y)) ? -1 : 1);
                    Debug.Log("----------");
                    foreach (var item in possibleSpawnPlaces)
                    {
                        Debug.Log(distanceBetweenTwoPoints(new Vector2(item.Item1, item.Item2), new Vector2(origines[0].x, origines[0].y)));
                    }
                    for (int i = 0; i < _nbrOfBlockToRemove; i++)
                    {
                        int _randomIndex = UnityEngine.Random.Range(0, (int)Mathf.Round(possibleSpawnPlaces.Count * ( 1 - elongatedFactor)));
                        possibleSpawnPlaces.RemoveAt(_randomIndex);
                    }
                }
                while(possibleSpawnPlaces.Count > 0 && _generatingBase)
                {
                    //pick a random possible spawn place
                    int _randomIndex = UnityEngine.Random.Range(0, possibleSpawnPlaces.Count);
                    if (!PossibleSpawnPoints[possibleSpawnPlaces[_randomIndex].Item1].tiles[possibleSpawnPlaces[_randomIndex].Item2].isUsed)
                    {
                        RollRandomBLock(PossibleSpawnPoints[possibleSpawnPlaces[_randomIndex].Item1].tiles[possibleSpawnPlaces[_randomIndex].Item2], possibleSpawnPlaces[_randomIndex]);
                    }
                    //remove the used place from the list
                    possibleSpawnPlaces.RemoveAt(_randomIndex);
                    _nbrBlocks++;
                    if(_nbrBlocks >= maxNbrBlocks)
                    {
                        _generatingBase = false;
                    }
                }
            }
            else
            {
                Debug.LogError("No more possible spawn places, aborting");
                break;
            }
        }
        int _generatingAfter = BlockToGenerateAfter.Count;
        while (_generatingAfter > 0)
        {
            List<(int, int)> possibleSpawnPlaces = new List<(int, int)>();
            for (int x = 0; x < PossibleSpawnPoints.Count; x++)
            {
                for (int y = 0; y < PossibleSpawnPoints[x].tiles.Count; y++)
                {
                    if (!PossibleSpawnPoints[x].tiles[y].isUsed)
                    {
                        if (UsedBlockNear((x, y)))
                        {
                            possibleSpawnPlaces.Add((x, y));
                        }
                    }
                }
            }
            if (possibleSpawnPlaces.Count > 0)
            {
                while (possibleSpawnPlaces.Count > 0 && _generatingAfter > 0)
                {
                    //pick a random possible spawn place
                    if (!PossibleSpawnPoints[possibleSpawnPlaces[_generatingAfter].Item1].tiles[possibleSpawnPlaces[_generatingAfter].Item2].isUsed)
                    {
                        GenerateBlock(BlockToGenerateAfter[0], PossibleSpawnPoints[possibleSpawnPlaces[_generatingAfter].Item1].tiles[possibleSpawnPlaces[_generatingAfter].Item2], possibleSpawnPlaces[_generatingAfter]);
                        _generatingAfter--;
                    }
                    //remove the used place from the list
                    possibleSpawnPlaces.RemoveAt(_generatingAfter);
                }
            }
            else
            {
                Debug.LogError("No more possible spawn places, aborting");
                break;
            }
        }

        //last round, lock all doors that are not next to a used block
        for (int x = 0; x < PossibleSpawnPoints.Count; x++)
        {
            for (int y = 0; y < PossibleSpawnPoints[x].tiles.Count; y++)
            {
                if (PossibleSpawnPoints[x].tiles[y].isUsed)
                {
                    if (x == 0)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.North);
                    }
                    if (x == PossibleSpawnPoints.Count - 1)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.South);
                    }
                    if (y == 0)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.West);
                    }
                    if (y == PossibleSpawnPoints[x].tiles.Count - 1)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.East);
                    }

                    if (x - 1 >= 0 && !PossibleSpawnPoints[x - 1].tiles[y].isUsed)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.North);
                    }
                    if (x + 1 < PossibleSpawnPoints.Count && !PossibleSpawnPoints[x + 1].tiles[y].isUsed)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.South);
                    }
                    if (y - 1 >= 0 && !PossibleSpawnPoints[x].tiles[y - 1].isUsed)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.West);
                    }
                    if (y + 1 < PossibleSpawnPoints[x].tiles.Count && !PossibleSpawnPoints[x].tiles[y + 1].isUsed)
                    {
                        PossibleSpawnPoints[x].tiles[y].blockLocked.GetComponent<BlockLogic>().EliminateDoor(DoorDirection.East);
                    }
                }
            }
        }
    }

    #region utilities

    public List<Tuple<int,int>> FindOrigins(List<TileRow> _rows)
    {
        List<Tuple<int, int>> _result = new List<Tuple<int, int>>();
        for (int x = 0; x < _rows.Count; x++)
        {
            for (int y = 0; y < _rows[x].tiles.Count; y++)
            {
                if (_rows[x].tiles[y].isOrigin)
                {
                    _result.Add(new Tuple<int,int>(x, y));
                }
            }
        }
        return _result;
    }

    public void RollRandomBLock(UsedTile _place, (int, int) _coordinateCode)
    {
        //chek if the block is already used
        if (_place.isUsed)
        {
            Debug.LogError("Trying to use a block that is already used");
            return;
        }

        //load all base blocks
        List<BlockPercentGeneration> _suitableRoomsType = new List<BlockPercentGeneration>();

        //create a list depending on if it's the origin or not
        if (_place.isOrigin)
        {
            // Take all block suitable for Spawn
            foreach (BlockPercentGeneration _block in BaseBlocks)
            {
                if (_block.originSuitable)
                {
                    _suitableRoomsType.Add(_block);
                }
            }
        }
        else
        {
            foreach (BlockPercentGeneration _block in BaseBlocks)
            {
                if (_block.classicSuitable)
                {
                    _suitableRoomsType.Add(_block);
                }
            }
        }

        // Force in all minimal required blocks
        List<BlockPercentGeneration> _minimalRooms = new List<BlockPercentGeneration>();
        foreach (BlockPercentGeneration _block in _suitableRoomsType)
        {
            if (_block.minimumToGenerate > 0)
            {
                _minimalRooms.Add(_block);
            }
        }
        if(_minimalRooms.Count > 0)
        {
            _suitableRoomsType = _minimalRooms;
        }

        // Take out all maximum required blocks
        List<BlockPercentGeneration> _noMaximumRooms = new List<BlockPercentGeneration>();
        foreach (BlockPercentGeneration _block in _suitableRoomsType)
        {
            if (_block.maximumToGenerate > 0)
            {
                _noMaximumRooms.Add(_block);
            }
        }
        _suitableRoomsType = _noMaximumRooms;



        // Roll randomly
        float totalChance = 0f;
        foreach(BlockPercentGeneration _block in _suitableRoomsType)
        {
            totalChance += _block.chanceToGenerate;
        }
        float pickedNumber = UnityEngine.Random.value * totalChance;
        foreach(BlockPercentGeneration _block in _suitableRoomsType)
        {
            pickedNumber -= _block.chanceToGenerate;
            if(pickedNumber <= 0)
            {
                GenerateBlock(_block, _place, _coordinateCode);
            }
        }
    }

    public void GenerateBlock(BlockPercentGeneration _block, UsedTile _place, (int, int) _coordinateCode)
    {
        GameObject newBlock = Instantiate(_block.block, _place.position, Quaternion.identity);
        newBlock.name = newBlock.name + "-Generated X" + _coordinateCode.Item1 + " Y" + _coordinateCode.Item2;
        PossibleSpawnPoints[_coordinateCode.Item1].tiles[_coordinateCode.Item2].blockLocked = newBlock;
        PossibleSpawnPoints[_coordinateCode.Item1].tiles[_coordinateCode.Item2].isUsed = true;
    }

    public void GenerateBlock(GameObject _block, UsedTile _place, (int, int) _coordinateCode)
    {
        GameObject newBlock = Instantiate(_block, _place.position, Quaternion.identity);
        newBlock.name = newBlock.name + "-Generated X" + _coordinateCode.Item1 + " Y" + _coordinateCode.Item2;
        PossibleSpawnPoints[_coordinateCode.Item1].tiles[_coordinateCode.Item2].blockLocked = newBlock;
        PossibleSpawnPoints[_coordinateCode.Item1].tiles[_coordinateCode.Item2].isUsed = true;
    }

    public bool UsedBlockNear((int,int) _coords)
    {
        bool _result = false;
        if (_coords.Item1 - 1 >= 0 && PossibleSpawnPoints[_coords.Item1 - 1].tiles[_coords.Item2].isUsed)
        {
            _result = true;
        }
        else if (_coords.Item1 + 1 < PossibleSpawnPoints.Count && PossibleSpawnPoints[_coords.Item1 + 1].tiles[_coords.Item2].isUsed)
        {
            _result = true;
        }
        else if (_coords.Item2 - 1 >= 0 && PossibleSpawnPoints[_coords.Item1].tiles[_coords.Item2 - 1].isUsed)
        {
            _result = true;
        }
        else if (_coords.Item2 + 1 < PossibleSpawnPoints[_coords.Item1].tiles.Count && PossibleSpawnPoints[_coords.Item1].tiles[_coords.Item2 + 1].isUsed)
        {
            _result = true;
        }

        return _result;
    }

    public float distanceBetweenTwoPoints(Vector2 _point1, Vector2 _point2)
    {
        return Mathf.Sqrt(Mathf.Pow(_point1.x - _point2.x, 2) + Mathf.Pow(_point1.y - _point2.y, 2));
    }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    float gridSize = 1.0f;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator myScript = (LevelGenerator)target;
        if (GUILayout.Button("Generate Whole Level"))
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            myScript.GenerateWholeLevel();
        }
        gridSize = EditorGUILayout.FloatField("Increase scale by:", gridSize);
        if (GUILayout.Button("Generate Spawnpoints"))
        {
            gridSize = Mathf.Round(gridSize);
            if(gridSize > 1)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        myScript.PossibleSpawnPoints[x].tiles[y].position = new Vector3(x * 100, 0, y * 100);
                    }
                }
            }
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
#endif
