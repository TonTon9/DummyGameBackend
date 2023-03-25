using System;
using Components;
using Cysharp.Threading.Tasks;
using Models.Game;
using UnityEngine;

namespace Character.Behaviours.Movement
{
    [RequireComponent(typeof(CharacterInputsController), typeof(CharacterController), typeof(GameCharacterModel))]
    public class CharacterControllerMovement : BaseNetworkMonoBehaviour, IMove
    {
        public event Action OnJump;
        public event Action OnSit;
        public event Action OnStay;
        public event Action<float> OnSpeedChanged;

        [SerializeField] private float _sprintMoveSpeedMultiplayer = 3f;

        [SerializeField] private float _jumpPower = 5f;

        [SerializeField] private float _gravityPower = 20f;

        [SerializeField] private float _sitMoveSpeedMultiplayer = 0.4f;

        private bool _isSitting;
        private bool _isSprinting;
        private CharacterInputsController _characterInputsController;
        private CharacterController _characterController;
        private GameCharacterModel _model;
        private Vector3 _direction;
        private float _gravityForce;
        private float _xDirectionInTheStartOfJump;
        private float _zDirectionInTheStartOfJump;
        private float _sitMoveSpeed;
        private float _sprintMoveSpeed;

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
            base.Initialize();
            _characterInputsController = GetComponent<CharacterInputsController>();
            _characterController = GetComponent<CharacterController>();
            _model = GetComponent<GameCharacterModel>();
            SetMoveSpeeds(_model.MoveSpeed);
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
            _model.OnMoveSpeedChanged += SetMoveSpeeds;
            _characterInputsController.OnWASDCalled += Move;
            _characterInputsController.OnSitCalled += Sit;
            _characterInputsController.OnStayCalled += Stay;
            _characterInputsController.OnJumpCalled += Jump;
            _characterInputsController.OnSprintCalled += Sprint;
            _characterInputsController.OnWalkCalled += Walk;
        }

        public void Move(float moveX, float moveZ)
        {
            if (_isSitting)
            {
                MoveCharacter(moveX, moveZ,_sitMoveSpeed);
            } else
            {
                if (_isSprinting)
                {
                    MoveCharacter(moveX, moveZ, _sprintMoveSpeed);    
                } else
                {
                    MoveCharacter(moveX, moveZ, _model.MoveSpeed);
                }
            }
        }

        private void MoveCharacter(float moveX, float moveZ, float speed)
        {
            _direction = Vector3.zero;
            if (_characterController.isGrounded)
            {
                _direction.x = moveX;
                _direction.z = moveZ;    
            } else
            {
                _direction.x = _xDirectionInTheStartOfJump;
                _direction.z = _zDirectionInTheStartOfJump;
            }
            _direction = _direction.normalized * speed;
            
            OnSpeedChanged?.Invoke(_direction.magnitude);

            _direction.y = _gravityForce;
            
            if (_characterController.isGrounded)
            {
                _direction = transform.TransformDirection(_direction);    
            }

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
                
                 _xDirectionInTheStartOfJump = _direction.x;
                _zDirectionInTheStartOfJump = _direction.z;
            }
        }
        
        private void SetMoveSpeeds(float moveSpeed)
        {
            _sitMoveSpeed = _model.MoveSpeed * _sitMoveSpeedMultiplayer;
            _sprintMoveSpeed = _model.MoveSpeed * _sprintMoveSpeedMultiplayer;
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

        private void Sprint()
        {
            _isSprinting = true;
        }

        private void Walk()
        {
            _isSprinting = false;
        }
        
        protected override void UnSubscribe()
        {
            if (!hasAuthority)
            {
                return;
            }
            base.UnSubscribe();
            _characterInputsController.OnWASDCalled -= Move;
            _characterInputsController.OnSitCalled -= Sit;
            _characterInputsController.OnStayCalled -= Stay;
            _characterInputsController.OnJumpCalled -= Jump;
            _characterInputsController.OnSprintCalled -= Sprint;
            _characterInputsController.OnWalkCalled -= Walk;
        }
    }
}
