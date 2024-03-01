using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerWeapon weapon;
    public BaseModule moduleEquiped;
    public List<BaseModule> modulesStocked;

    public void HoldTrigger(InputValue val)
    {
        weapon.HoldTrigger(val);
    }

    public bool TryAddModule(BaseModule _module)
    {
        modulesStocked.Add(_module);
        return true;
    }

    public void EquipModule(int _moduleIndice)
    {
        if (!moduleEquiped)
        {
            moduleEquiped = modulesStocked[_moduleIndice];
            modulesStocked.RemoveAt(_moduleIndice);
        }
        else
        {
            BaseModule _target = moduleEquiped;
            while (_target.nextModule)
            {
                _target = _target.nextModule;
            }
            _target.nextModule = modulesStocked[_moduleIndice];
            modulesStocked.RemoveAt(_moduleIndice);
        }
    }

    public void UnEquipModule()
    {
        if (moduleEquiped)
        {
            if (!moduleEquiped.nextModule)
            {
                modulesStocked.Add(moduleEquiped);
                moduleEquiped = null;
                return;
            }
            BaseModule target = moduleEquiped;
            while (target.nextModule && target.nextModule.nextModule)
            {
                target = target.nextModule;
            }
            modulesStocked.Add(moduleEquiped.nextModule);
            moduleEquiped.nextModule = null;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerInventory))]
public class LockScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("Equip Module 0",GUILayout.Width(200)))
        {
            PlayerInventory script = (PlayerInventory)target;
            script.EquipModule(0);
        }
        if (GUILayout.Button("Unequip Last Module", GUILayout.Width(200)))
        {
            PlayerInventory script = (PlayerInventory)target;
            script.UnEquipModule();
        }
    }
}
#endif