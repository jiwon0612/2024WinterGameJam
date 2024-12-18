using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    [SerializeField] private PlayerInputSO input;

    [Header("Setting")] 
    [SerializeField] private float rotateValue;
    
    public PlayerMover Mover {get; private set;}
    public PlayerRenderer Renderer {get; private set;}
    
    protected override void AfterInitComp()
    {
        base.AfterInitComp();

        Mover = GetCompo<PlayerMover>();
        Renderer = GetCompo<PlayerRenderer>();

        input.OnRightDashEvent += TryRightDash;
        input.OnLeftDashEvent += TryLeftDash;
        Mover.OnWingingEvent.AddListener(Winging);
    }

    private void OnDisable()
    {
        Mover.OnWingingEvent.RemoveListener(Winging);
        input.OnRightDashEvent -= TryRightDash;
        input.OnLeftDashEvent -= TryLeftDash;
    }

    private void Update()
    {
        Mover.SetMove(input.MoveDirection);
        if (input.MoveDirection == Vector2.zero)
        {
            Renderer.ChangeState(WingState.Idle);
            return;
        }
        
        if (input.MoveDirection.y > 0.1f)
        {
            
        }
        else if (input.MoveDirection.y < -0.1f)
        {
            Renderer.ChangeState(WingState.Fold);
        }

        //if (Mathf.Abs(input.MoveDirection.x) > 0.1f)
            
    }

    private void Winging() => Renderer.ChangeState(WingState.Winging);
    
    private void TryRightDash()
    {
        Renderer.TailRig.SetFold(true);
        Renderer.ChangeState(WingState.Fold);
        Mover.Dash(1, () =>
        {
            Renderer.TailRig.SetFold(false);
            Renderer.ChangeState(WingState.Unfold);
        });
    }

    private void TryLeftDash()
    {
        Renderer.TailRig.SetFold(true);
        Renderer.ChangeState(WingState.Fold);
        Mover.Dash(-1, () =>
        {
            Renderer.TailRig.SetFold(false);
            Renderer.ChangeState(WingState.Unfold);
        });
    }
}
