using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[System.Serializable]
public class UsedTile
{
    public Transform position;
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
    public List<TileRow> PossibleSpawnPoints = new List<TileRow>();
    public List<BlockPercentGeneration> PossibleBlocks;
    public int maxNbrBlocks;

    public void GenerateWholeLevel()
    {
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
                    RollRandomBLock(PossibleSpawnPoints[x].tiles[y],(x,y));
                    _nbrBlocks++;
                }
            }
        }

        //second round, generate the rest and repeat until maxNbrBlocks is reached
        bool _generating = true;
        while (_generating)
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
                while(possibleSpawnPlaces.Count > 0 && _generating)
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
                        _generating = false;
                    }
                }
            }
            else
            {
                Debug.LogError("No more possible spawn places, aborting");
                break;
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
            foreach (BlockPercentGeneration _block in PossibleBlocks)
            {
                if (_block.originSuitable)
                {
                    _suitableRoomsType.Add(_block);
                }
            }
        }
        else
        {
            foreach (BlockPercentGeneration _block in PossibleBlocks)
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
        Debug.Log("Generating block " + _block.block.name + " at " + _coordinateCode.Item1 + " " + _coordinateCode.Item2 + " " + PossibleSpawnPoints[_coordinateCode.Item1].tiles[_coordinateCode.Item2].isUsed);
        GameObject newBlock = Instantiate(_block.block, _place.position.position, _place.position.rotation);
        newBlock.name = " BlockGenerated " + _coordinateCode.Item1 + " " + _coordinateCode.Item2;
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
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator myScript = (LevelGenerator)target;
        if (GUILayout.Button("Generate Whole Level"))
        {
            myScript.GenerateWholeLevel();
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
#endif
