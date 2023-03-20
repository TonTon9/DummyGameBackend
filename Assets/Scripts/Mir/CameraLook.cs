using System;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float Xsens = 200f;
    
    [SerializeField]
    private float Ysens = 200f;
    
    private float XRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xAngle = Input.GetAxis("Mouse Y") * Ysens * Time.deltaTime;
        float yAngle = Input.GetAxis("Mouse X") * Xsens * Time.deltaTime;

        XRotation -= xAngle;
        XRotation = Mathf.Clamp(XRotation, -80, 80);
        
        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        
        _player.transform.Rotate(0, yAngle, 0);
    }
}
