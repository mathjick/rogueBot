using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LADRoomScript : MonoBehaviour
{
    public LifeSystem bossLifeSystem;
    public void SetupBossBar()
    {
        PlayerController.instance.playerUI.setBoss(bossLifeSystem);
    }
}
