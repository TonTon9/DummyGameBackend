using Mirror;
using UnityEngine;

public class Movement : NetworkBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _feetPosition;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 2f;

    [SerializeField] private Animator _fpsAnimation;
    [SerializeField] private Animator _tpAnimation;

    private float _gravity = -16f;
    private Vector3 _velocity;

    private void Update()
    {
        if (!isLocalPlayer) return;

        bool isGrounded = Physics.CheckSphere(_feetPosition.transform.position, 0.4f, _groundMask);

        if (isGrounded && _velocity.y <= 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            _fpsAnimation.SetBool("Walking", true);
            _tpAnimation.SetBool("Walking", true);
        } else
        {
            _fpsAnimation.SetBool("Walking", false);
            _tpAnimation.SetBool("Walking", false);
        }

        Vector3 move_direction = transform.right * x + transform.forward * z;

        _characterController.Move(move_direction * Time.deltaTime * _speed);

        if (Input.GetButtonDown("Jump"))
        {
            if (!isGrounded)
            {
                return;
            }

            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }
}