using UnityEngine;

namespace Components.CharacterComponents.Camera
{
    [RequireComponent(typeof(CharacterInputsController))]
    public class CharacterCamera : MonoBehaviour
    {
        private CharacterInputsController _inputsController;

        [SerializeField]
        private float Xsens = 200f;
    
        [SerializeField]
        private float Ysens = 200f;

        [SerializeField]
        private Transform _characterCamera;
    
        private float _xRotationAngle;
        private CameraPosition _cameraPositionObject;

        private void Awake()
        {
            _inputsController = GetComponent<CharacterInputsController>();
            _inputsController.OnMouseMoveCalled += RotateCharacter;
            _cameraPositionObject = GetComponentInChildren<CameraPosition>();
            _characterCamera.position = _cameraPositionObject.transform.position;
        }

        private void RotateCharacter(float xAngle, float yAngle)
        {
            _xRotationAngle -= xAngle * Xsens;
            _xRotationAngle = Mathf.Clamp(_xRotationAngle, -80, 80);
        
            _characterCamera.localRotation = Quaternion.Euler(_xRotationAngle, 0, 0);
        
            transform.Rotate(0, yAngle * Ysens, 0);

            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            _characterCamera.position = _cameraPositionObject.transform.position;
            _characterCamera.localPosition = new Vector3(_characterCamera.localPosition.x, _characterCamera.localPosition.y, _characterCamera.localPosition.z+0.2f);

            //_characterCamera.position = new Vector3(_characterCamera.position.x, _cameraPositionObject.transform.position.y, _characterCamera.position.z);
        }
    }
}
