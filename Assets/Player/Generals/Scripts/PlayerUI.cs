using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI hpUI;
    public GameObject bossUI;
    public Slider bossHPSlider;
    public LifeSystem playerLifeSystem;
    public LifeSystem bossLifeSystem;

    public void Start()
    {
        hpUI.text = playerLifeSystem.lifePoints.ToString();
    }

    public void Update()
    {
        hpUI.text = playerLifeSystem.lifePoints.ToString();
        if (bossLifeSystem)
        {
            bossHPSlider.value = bossLifeSystem.lifePoints;
            if(bossLifeSystem.lifePoints <= 0)
            {
                bossUI.SetActive(false);
            }
        }
    }

    public void setBoss(LifeSystem boss)
    {
        bossUI.SetActive(true);
        bossLifeSystem = boss;
        bossHPSlider.maxValue = bossLifeSystem.maxLifePoints;
        bossHPSlider.value = bossLifeSystem.lifePoints;
    }


}
