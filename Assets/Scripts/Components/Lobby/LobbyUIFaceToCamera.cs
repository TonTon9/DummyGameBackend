using Components.Common;
using UnityEngine;

namespace Components.Lobby
{
    public class LobbyUIFaceToCamera : BaseMonoBehaviour
    {
        private CameraRotator _lobbyCamera;
        protected override void Initialize()
        {
            base.Initialize();
            _lobbyCamera = FindObjectOfType<CameraRotator>();
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _lobbyCamera.transform.rotation * Vector3.forward,
                _lobbyCamera.transform.rotation * Vector3.up);
        }
    }
}