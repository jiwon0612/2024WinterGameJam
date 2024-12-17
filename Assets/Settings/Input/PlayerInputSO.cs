using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Control;

[CreateAssetMenu(menuName = "SO/Input/PlayerInput")]
public class PlayerInputSO : ScriptableObject, IPlayerActions
{
    public Vector2 MoveDirection { get; private set; }
    
    private Control _control;

    private void OnEnable()
    {
        if (_control == null)
        {
            _control = new Control();
            _control.Enable();
        }
        _control.Player.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }
}
