using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyLifebarManager : MonoBehaviour
{

    [SerializeField] private PlayerWeapon weapon;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask behindLayerMask;

    [SerializeField] private GameObject lifebarGO;
    private UILifebarBehavior lifebar;
    private UIEnemyNameDisplay enemyName;

    private void Awake()
    {
        lifebar = lifebarGO.GetComponentInChildren<UILifebarBehavior>();
        enemyName = lifebarGO.GetComponentInChildren<UIEnemyNameDisplay>();
    }


    private void Update()
    {
        RaycastHit hit;
        RaycastHit hitBehind;
        RaycastHit[] hits;
        Physics.Raycast(weapon.inventory.playerController.playerView.transform.position, weapon.inventory.playerController.playerView.transform.forward* 1000, out hit, 10000f, layerMask);
        Physics.Raycast(weapon.inventory.playerController.playerView.transform.position, weapon.inventory.playerController.playerView.transform.forward* 1000, out hitBehind, 10000f, behindLayerMask);

        if (hitBehind.collider)
        {
            Debug.Log(hitBehind.collider);
            Debug.Log(hitBehind.collider.gameObject);
                LifeSystem ls = hitBehind.collider.gameObject.GetComponentInParent<LifeSystem>();
                lifebar.UpdateLifebar(ls);
                enemyName.UpdateName(ls.entityName);
                lifebarGO.SetActive(true);            
        }
        else if (hit.collider)
        {
            Debug.Log(hit.collider);
            Debug.Log(hit.collider.gameObject);
            LifeSystem ls = hit.collider.gameObject.GetComponentInParent<LifeSystem>();

            lifebar.UpdateLifebar(ls);
            enemyName.UpdateName(ls.entityName);
            lifebarGO.SetActive(true);
        }
        else
        {
            lifebarGO.SetActive(false);
        }
    }
}
