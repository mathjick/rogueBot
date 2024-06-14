using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifebarBehavior : MonoBehaviour
{
    private RectTransform rt;
    private Vector2 baseSize;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        baseSize = rt.sizeDelta;
    }
    private void Update()
    {
        
    }

    public void UpdateLifebar(LifeSystem ls)
    {
        rt.sizeDelta = new Vector2(baseSize.x / ls.maxLifePoints * ls.lifePoints, baseSize.y);
    }
}
