using System;
using UnityEngine;

public class CharacterInputsController : MonoBehaviour
{
    public event Action<float, float> OnWASDCalled;
    public event Action<float, float> OnMouseMoveCalled;
    public event Action OnJumpCalled;
    public event Action OnSitCalled;
    public event Action OnStayCalled;
    public event Action OnSprintCalled;
    public event Action OnWalkCalled;

    private void Update()
    {
        OnWASDCalled?.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMouseMoveCalled?.Invoke(Input.GetAxis("Mouse Y") * Time.deltaTime,Input.GetAxis("Mouse X") * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpCalled?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnSitCalled?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            OnStayCalled?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnSprintCalled?.Invoke();
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnWalkCalled?.Invoke();
        }
    }
}