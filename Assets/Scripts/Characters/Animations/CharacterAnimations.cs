using Character.Behaviours.Movement;
using Components;
using Mirror;
using UnityEngine;

namespace Character.Animations
{
    [RequireComponent(typeof(NetworkAnimator), typeof(CharacterControllerMovement))]
    public class CharacterAnimations : BaseMonoBehaviour
    {
        private Animator _animator;
        private NetworkAnimator _networkAnimator;
        private CharacterControllerMovement _characterController;

        protected override void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
            _networkAnimator = GetComponent<NetworkAnimator>();
            _characterController = GetComponent<CharacterControllerMovement>();
            
            _networkAnimator.animator = _animator;
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            _characterController.OnSit += Sit;
            _characterController.OnStay += Stay;
            _characterController.OnJump += Jump;
            _characterController.OnSpeedChanged += SetSpeed;
        }

        protected override void UnSubscribe()
        {
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
