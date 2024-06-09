using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UsedTile
{
    public Transform position;
    public GameObject blockLocked;
    public bool isUsed;
    public bool isOrigin;
}

[System.Serializable]
public struct TileRow
{
    public List<UsedTile> tiles;
}

[System.Serializable]
public struct BlockPercentGeneration
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
    public List<TileRow> PossibleSpawnPoints;
    public List<BlockPercentGeneration> PossibleBlocks;
    public int maxNbrBlocks;

    public void GenerateWholeLevel()
    {
        //first round, generate origins
        foreach (TileRow row in PossibleSpawnPoints)
        {
            foreach(UsedTile tile in row.tiles)
            {
                if (tile.isOrigin)
                {

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

    public void RollRandomBLock(UsedTile _place)
    {
        List<BlockPercentGeneration> _suitableRoomsType = new List<BlockPercentGeneration>();
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
            _suitableRoomsType = PossibleBlocks;
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
                GenerateBlock(_block, _place);
            }
        }
    }

    public void GenerateBlock(BlockPercentGeneration _block, UsedTile _place)
    {

    }

    #endregion
}
