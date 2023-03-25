using Character.Behaviours.Movement;
using Components;
using Components.Lobby;
using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;
using Views.Game;

namespace Character.Animations
{
    [RequireComponent(typeof(NetworkAnimator), typeof(CharacterControllerMovement))]
    public class CharacterAnimations : BaseNetworkMonoBehaviour
    {
        private Animator _animator;
        private CharacterControllerMovement _characterController;

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
            _animator = GetComponentInChildren<Animator>();
            _characterController = GetComponent<CharacterControllerMovement>();
            _isInitialize = true;
        }

        protected override async void Subscribe()
        {
            await UniTask.WaitUntil(() => _isAuthorityInit && _isInitialize);
            if (!hasAuthority)
            {
                return;
            }
            _characterController.OnSit += Sit;
            _characterController.OnStay += Stay;
            _characterController.OnJump += Jump;
            _characterController.OnSpeedChanged += SetSpeed;
        }

        protected override void UnSubscribe()
        {
            if (!hasAuthority)
            {
                return;
            }
            base.UnSubscribe();
            if (_characterController != null)
            {
                _characterController.OnSit -= Sit;
                _characterController.OnStay -= Stay;
                _characterController.OnJump -= Jump;
            }
        }

        private void Sit()
        {
            _animator.SetBool("Standing", false);
        }
        
        private void Stay()
        {
            _animator.SetBool("Standing", true);
        }

        private void Jump()
        {
            _animator.SetTrigger("Jump");
        }

        private void SetSpeed(float speed)
        {
            Debug.Log(speed);
            _animator.SetFloat("Speed", speed);
        }
    }
}
