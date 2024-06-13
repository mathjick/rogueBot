using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject weaponView;
    [Space(1)]
    [Header("------------ Look Parameters ------------")]
    [Space(1)]
    public Vector2 _lookMultiplyer;
    public GameObject _lookBufferDummy;
    public GameObject _playerBufferDummy;
    public Quaternion _recoilApplied = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _recoilApplied = Quaternion.Lerp(_recoilApplied, Quaternion.identity, 0.1f);
        playerController.playerView.transform.rotation = _lookBufferDummy.transform.rotation;
        playerController.playerTransform.rotation = _playerBufferDummy.transform.rotation;
    }

    public void Look(InputValue val)
    {
        if(Mathf.Abs(val.Get<Vector2>().x) <= 0.1 && Mathf.Abs(val.Get<Vector2>().y) <= 0.1)
        {
        }
        else
        {
            _playerBufferDummy.transform.Rotate(new Vector3(0, val.Get<Vector2>().x * _lookMultiplyer.x, 0));
            _lookBufferDummy.transform.Rotate(new Vector3(-val.Get<Vector2>().y * _lookMultiplyer.y, 0, 0));
            _lookBufferDummy.transform.localRotation = Quaternion.Euler(_lookBufferDummy.transform.localRotation.eulerAngles.x, 0, 0);
            
            if (_lookBufferDummy.transform.rotation.eulerAngles.x > 180 && _lookBufferDummy.transform.rotation.eulerAngles.x < 280)
            {
                _lookBufferDummy.transform.rotation = Quaternion.Euler(-80, _lookBufferDummy.transform.rotation.eulerAngles.y, _lookBufferDummy.transform.rotation.eulerAngles.z);
            }
            if (_lookBufferDummy.transform.rotation.eulerAngles.x > 80 && _lookBufferDummy.transform.rotation.eulerAngles.x < 180)
            {
                _lookBufferDummy.transform.rotation = Quaternion.Euler(80, _lookBufferDummy.transform.rotation.eulerAngles.y, _lookBufferDummy.transform.rotation.eulerAngles.z);
            }
            
        }
    }
}
