using UnityEngine;

public class PlayerUnlockable : MonoBehaviour
{
    public int EnergieCores = 0;

    private void Start()
    {
        EnergieCores = PlayerPrefs.GetInt("EnergieCores", 0);
    }

    public void AddEnergieCores(int amount)
    {
        EnergieCores += amount;
    }

    public void RemoveEnergieCores(int amount)
    {
        EnergieCores -= amount;
    }

    private void OnApplicationQuit()
    {
        SaveEnergieCores();
    }

    private void OnDestroy()
    {
        SaveEnergieCores();
    }

    public void SaveEnergieCores()
    {
        PlayerPrefs.SetInt("EnergieCores", EnergieCores);
    }
}
