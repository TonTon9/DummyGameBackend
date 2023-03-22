using Character.Behaviours.Movement;
using Mirror;
using UnityEngine;

namespace Game.Player.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacter : NetworkBehaviour
    {
        private IMove _moveBehaviour;

        //private CharacterController _characterController;

        // private void Awake()
        // {
        //     _characterController = GetComponent<CharacterController>();
        //     _moveBehaviour = new CharacterControllerMovement(_characterController, transform, );
        // }

        private void Start()
        {
        
        }
    }
}

