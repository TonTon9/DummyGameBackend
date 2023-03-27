using Cysharp.Threading.Tasks;
using UnityEngine;
using Views.Game;

namespace Components.CharacterComponents.Camera
{
    [RequireComponent(typeof(CharacterInputsController))]
    public class CharacterCamera : BaseNetworkMonoBehaviour
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
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            _isAuthorityInit = true;
        }

        protected override async void Initialize()
        {
            await UniTask.WaitUntil(() => _isAuthorityInit);
            
            if (!hasAuthority)
            {
                return;
            }

            _inputsController = GetComponent<CharacterInputsController>();
            _cameraPositionObject = GetComponentInChildren<CameraPosition>();
            _characterCamera.position = _cameraPositionObject.transform.position;
            _characterCamera.gameObject.SetActive(true);
            _isInitialize = true;
        }

        protected override async void Subscribe()
        {
            await UniTask.WaitUntil(() => _isAuthorityInit && _isInitialize);
            
            if (!hasAuthority)
            {
                return;
            }
            
            base.Subscribe();
            _inputsController.OnMouseMoveCalled += RotateCharacter;
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
            _characterCamera.localPosition = new Vector3(_characterCamera.localPosition.x, _characterCamera.localPosition.y, _characterCamera.localPosition.z+0.25f);
        }
    }
}
