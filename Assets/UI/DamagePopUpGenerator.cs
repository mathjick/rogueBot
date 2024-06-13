using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator instance;

    [Header("Pop-Up Prefabs -")]
    public GameObject normalHitPrefab;
    public GameObject critPrefab;

    [Header("Random Spawn Bounds -")]
    public Vector3 tr;
    public float[] yBounds = new float[2];
    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;

    private void Awake()
    {
        if (instance != null && instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            CreatePopUp(Vector3.one * 2, Random.Range(0, 1000).ToString(), true);
        }
    }

    public void CreatePopUp(Vector3 position, string text, bool isCrit)
    {
        GameObject popup;
        var randPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        if (isCrit)
        {
            popup = Instantiate(critPrefab, position + randPos, Quaternion.identity);
            var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            temp.text = text + "!";
        }
        else
        {
            popup = Instantiate(normalHitPrefab, position + randPos, Quaternion.identity);
            var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            temp.text = text;
        }
        Destroy(popup, 1f);
    }
}
