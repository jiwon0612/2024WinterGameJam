using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum WingState
{
    Idle,
    Winging,
    Fold,
    Unfold,
    SlowFold
}

public class PlayerRenderer : EntityRenderer
{
    private EntityRenderer _renderer;

    public WingState CurrentState;

    private bool _isChangeState;
    private bool _isWinging;
    
    public UnityEvent OnWingingEvent;
    
    public SmallWingRig TailRig { get; private set; }
    
    public SmallWingRig MiddleWing { get; private set; }
    
    public override void AfterInit()
    {
        TailRig = GetRigComp<SmallWingRig>("TailWingRig");
        MiddleWing = GetRigComp<SmallWingRig>("MiddleWingRig");
        
        CurrentState = WingState.Idle;
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
        
        switch (CurrentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                break;
            case WingState.SlowFold:
                break;
        }
    }
    
    private void EnterState()
    {
        Animator.SetBool(CurrentState.ToString(), true);
        
        switch (CurrentState)
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
            case WingState.SlowFold:
                break;
        }
    }
    
    private void ExitState()
    {
        Animator.SetBool(CurrentState.ToString(), false);

        switch (CurrentState)
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
            case WingState.SlowFold:
                break;
        }
        
    }
    
    private void Trigger()
    {
        switch (CurrentState)
        {
            case WingState.Idle:
                break;
            case WingState.Winging:
            {
                OnWingingEvent?.Invoke();
            }
                break;
            case WingState.Fold:
                break;
            case WingState.Unfold:
                ChangeState(WingState.Idle);
                break;
            case WingState.SlowFold:
                break;
        }
    }
    
    public void ChangeState(WingState newState)
    {
        _isChangeState = true;
        ExitState();
        CurrentState = newState;
        EnterState();
        _isChangeState = false;
    }
    
    
}
