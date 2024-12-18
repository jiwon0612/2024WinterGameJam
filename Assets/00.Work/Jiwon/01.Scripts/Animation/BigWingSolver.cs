using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations.Rigging;

public enum WingState
{
    Idle,
    Winging,
    Fold,
    Unfold
}

public class BigWingSolver : MonoBehaviour, IRigAnimControl
{
    private EntityRenderer _renderer;
    private Rig _rig;

    private WingState _currentState;

    private bool _isChangeState;
    
    public GameObject RigObject => gameObject;
    public void InitAnimControl(EntityRenderer renderer)
    {
        _renderer = renderer;
        
        _rig = GetComponent<Rig>();
        
        _currentState = WingState.Idle;
        _renderer.OnAnimationTrigger += Trigger;
        EnterState();
    }

    private void OnDisable()
    {
        _renderer.OnAnimationTrigger -= Trigger;
    }

    private void Update()
    {
        if (_isChangeState) return;
        
        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                break;
        }
    }

    private void EnterState()
    {
        _renderer.Animator.SetBool(_currentState.ToString(), true);
        
        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                break;
        }
    }

    private void ExitState()
    {
        _renderer.Animator.SetBool(_currentState.ToString(), false);

        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                break;
        }
        
    }

    private void Trigger()
    {
        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                ChangeState(WingState.Idle);
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                ChangeState(WingState.Idle);
                break;
        }
    }

    public void ChangeState(WingState newState)
    {
        _isChangeState = true;
        ExitState();
        _currentState = newState;
        EnterState();
        _isChangeState = false;
    }
}
