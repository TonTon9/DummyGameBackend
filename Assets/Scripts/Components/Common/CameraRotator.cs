using UnityEngine;

namespace Components.Common
{
    public class CameraRotator : BaseMonoBehaviour
    {
        [SerializeField]
        private float _cameraSpeed;

        private void Update()
        {
            transform.Rotate(0, _cameraSpeed * Time.deltaTime, 0);
        }
    }
}
