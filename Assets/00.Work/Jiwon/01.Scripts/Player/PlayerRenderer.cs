using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WingState
{
    Idle,
    Winging,
    Fold,
    Unfold
}

public class PlayerRenderer : EntityRenderer
{
    private EntityRenderer _renderer;

    private WingState _currentState;

    private bool _isChangeState;
    private bool _isWinging;
    
    public SmallWingRig TailRig { get; private set; }
    
    public SmallWingRig MiddleWing { get; private set; }
    
    public override void AfterInit()
    {
        TailRig = GetRigComp<SmallWingRig>("TailWingRig");
        MiddleWing = GetRigComp<SmallWingRig>("MiddleWingRig");
        
        _currentState = WingState.Idle;
        OnAnimationTrigger += Trigger;
        EnterState();
    }
    
    private void OnDisable()
    {
        OnAnimationTrigger -= Trigger;
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
        Animator.SetBool(_currentState.ToString(), true);
        
        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                _isWinging = true;
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                break;
        }
    }
    
    private void ExitState()
    {
        Animator.SetBool(_currentState.ToString(), false);

        switch (_currentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                _isWinging = false;
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
