using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    General,
    Death
}

public class PlayerUI : MonoBehaviour
{
    public GameObject generalUI;
    public GameObject deathUI;
    [Space(5)]
    public TextMeshProUGUI hpUI;
    public GameObject bossUI;
    public Slider bossHPSlider;
    public LifeSystem playerLifeSystem;
    public LifeSystem bossLifeSystem;
    public Image DamageScreen;

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
        if(DamageScreen.color.a > 0)
        {
            DamageScreen.color = new Color(DamageScreen.color.r, DamageScreen.color.g, DamageScreen.color.b, DamageScreen.color.a - Time.deltaTime);
        }
    }

    public void setBoss(LifeSystem boss)
    {
        bossUI.SetActive(true);
        bossLifeSystem = boss;
        bossHPSlider.maxValue = bossLifeSystem.maxLifePoints;
        bossHPSlider.value = bossLifeSystem.lifePoints;
    }

    public void SwapUITo(UIType uiToSwapTo)
    {
        switch (uiToSwapTo)
        {
            case UIType.General:
                generalUI.SetActive(true);
                deathUI.SetActive(false);
                break;
            case UIType.Death:
                generalUI.SetActive(false);
                deathUI.SetActive(true);
                break;
        }
    }

    public void DamageUIEffect()
    {
        DamageScreen.color = new Color(DamageScreen.color.r, DamageScreen.color.g, DamageScreen.color.b, 0.5f);
    }
}
