using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BlockDifficulty
{
    Peacefull,Easy,Medium,Hard,Boss
}

public enum BlockType
{
    Jump,Fight,Loot,Puzzle,Spawn,Boss
}

public enum DoorDirection
{
    North, East, South, West
}

[System.Serializable]
public class Door
{
    public DoorDirection doorDirection;
    public GameObject door;
    public bool isLocked = false;
}

public class BlockLogic : MonoBehaviour
{
    public int BlockId;
    public LevelManager levelManager;
    public BlockDifficulty blockDifficulty;
    public BlockType blockType;
    public Door[] doors;
    public int pullPower = 10;
    [SerializeField] private UnityEvent OnEnterRoom;
    [SerializeField] private UnityEvent OnEnterRoomFirstTime;
    [SerializeField] private UnityEvent OnExitRoom;

    public PlayerController playerInRoom;
    private bool firstEnter = true;

    public void EnterRoom()
    {
        OnEnterRoom.Invoke();
    }
    public void ExitRoom()
    {
        OnExitRoom.Invoke();
    }

    public void EnterRoomFirstTime()
    {
        OnEnterRoomFirstTime.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tag_player")
        {
            playerInRoom = other.gameObject.GetComponent<PlayerController>() ? other.gameObject.GetComponent<PlayerController>() : other.gameObject.GetComponentInParent<PlayerController>();
            if (firstEnter)
            {
                EnterRoomFirstTime();
                firstEnter = false;
            }
            else
            {
                EnterRoom();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "tag_player")
        {
            ExitRoom();
        }
    }

    public void OpenDoors()
    {
        foreach (Door door in doors)
        {
            if (!door.isLocked)
            {
                door.door.SetActive(false);
            }
            
        }
    }

    public void CloseDoors()
    {
        foreach (Door door in doors)
        {
            if (!door.isLocked)
            {
                door.door.SetActive(true);
            }
        }
    }

    public void PullPlayer()
    {
        if(this.playerInRoom != null)
        {
            playerInRoom.rb.AddForce((this.transform.position - this.playerInRoom.transform.position).normalized * pullPower, ForceMode.Impulse);
        }
    }

    public void EliminateDoor(DoorDirection doorDirection)
    {
        // delete every door in the direction
        foreach (Door door in doors)
        {
            if (door.doorDirection == doorDirection)
            {
                door.door.SetActive(true);
                door.isLocked = true;
            }
        }
    }
}
