using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockDifficulty
{
    Peacefull,Easy,Medium,Hard,Boss
}

public enum BlockType
{
    Jump,Fight,Loot,Puzzle,Spawn,Boss
}

public abstract class BlockLogic : MonoBehaviour
{
    public string BlockType;
    public int BlockNbr;
    public BlockDifficulty blockDifficulty;
    public BlockType blockType;

    public abstract void OnGeneration();
    public abstract void OnPlayerEnter();
    public abstract void OnPlayerFinished();
    public abstract void OnPlayerExit();
}
