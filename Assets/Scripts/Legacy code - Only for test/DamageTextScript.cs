using System;
using TMPro;
using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
    private GameObject _cameraGameObject;
    public void DestroyText()
    {
        Destroy(gameObject);
    }

    public void GetCalled(float damage, GameObject camera)
    {
        GetComponent<TextMeshPro>().text = damage.ToString();
        _cameraGameObject = camera;
    }

    private void LateUpdate()
    {
        if (_cameraGameObject != null)
        {
            transform.LookAt(transform.position + _cameraGameObject.transform.rotation * Vector3.forward, _cameraGameObject.transform.rotation * Vector3.up);
        }
    }
}
