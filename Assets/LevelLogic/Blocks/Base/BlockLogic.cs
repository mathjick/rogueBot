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

public class BlockLogic : MonoBehaviour
{
    public int BlockId;
    public LevelManager levelManager;
    public BlockDifficulty blockDifficulty;
    public BlockType blockType;
    public GameObject[] doors;
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
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

    public void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }

    public void PullPlayer()
    {
        if(this.playerInRoom != null)
        {
            playerInRoom.rb.AddForce((this.transform.position - this.playerInRoom.transform.position)*5,ForceMode.Impulse);
            Debug.Log("Pull PLayer");
        }
    }
}
