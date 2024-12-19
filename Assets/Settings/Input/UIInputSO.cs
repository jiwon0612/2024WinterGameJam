using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Control;

[CreateAssetMenu(menuName = "SO/Input/UI")]
public class UIInputSO : ScriptableObject, IUIActions
{
    public event Action OnMenuEvent;

    private Control _control;

    private void OnEnable()
    {
        if (_control == null)
        {
            _control = new Control();
            _control.Enable();
        }

        _control.UI.SetCallbacks(this);
    }

    public void SetActive(bool value)
    {
        if (value == true)
        {
            _control.UI.Enable();
            _control.UI.SetCallbacks(this);
        }
        else
        {
            _control.UI.Disable();
        }
    }

    public void OnUI(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnMenuEvent?.Invoke();
    }
}