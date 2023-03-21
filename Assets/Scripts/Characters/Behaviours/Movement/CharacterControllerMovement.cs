using System;
using UnityEngine;

namespace Character.Behaviours.Movement
{
    [RequireComponent(typeof(CharacterInputsController), typeof(CharacterController))]
    public class CharacterControllerMovement : MonoBehaviour, IMove
    {
        public event Action OnJump;
        public event Action OnSit;
        public event Action OnStay;
        public event Action<float> OnSpeedChanged;
        
        [SerializeField]
        private float _moveSpeed = 5f;

        [SerializeField]
        private float _jumpPower = 5f;

        [SerializeField]
        private float _gravityPower = 20f;

        [SerializeField]
        private float _sitMoveSpeed = 1;

        private bool _isSitting;
        
        private CharacterInputsController _characterInputsController;
        private CharacterController _characterController;

        private Vector3 _direction;
        private float _gravityForce;

        private void Awake()
        {
            _characterInputsController = GetComponent<CharacterInputsController>();
            _characterController = GetComponent<CharacterController>();
            
            _characterInputsController.OnWASDCalled += Move;
            _characterInputsController.OnSitCalled += Sit;
            _characterInputsController.OnStayCalled += Stay;
            _characterInputsController.OnJumpCalled += Jump;
        }

        public void Move(float moveX, float moveZ)
        {
            if (_isSitting)
            {
                MoveCharacter(moveX, moveZ, _sitMoveSpeed);
            } else
            {
                MoveCharacter(moveX, moveZ, _moveSpeed);
            }
        }

        private void MoveCharacter(float moveX, float moveZ, float speed)
        {
            _direction = Vector3.zero;
            _direction.x = moveX * speed;
            _direction.z = moveZ * speed;
            
            OnSpeedChanged?.Invoke(_direction.magnitude);

            _direction.y = _gravityForce;
            _direction = transform.TransformDirection(_direction);
            _characterController.Move(_direction * Time.deltaTime);
            
            CustomGravity();
        }

        private void CustomGravity()
        {
            if (!_characterController.isGrounded)
            {
                _gravityForce -= _gravityPower * Time.deltaTime;
            } else
            {
                _gravityForce = -1f;
            }
        }
        
        public void Jump()
        {
            if (_characterController.isGrounded)
            {
                OnJump?.Invoke();
                _gravityForce = _jumpPower;
            }
        }

        public void Sit()
        {
            _isSitting = true;
            OnSit?.Invoke();
        }
        
        public void Stay()
        {
            _isSitting = false;
            OnStay?.Invoke();
        }
    }
}