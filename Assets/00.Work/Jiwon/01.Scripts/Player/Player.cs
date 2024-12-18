using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    [SerializeField] private PlayerInputSO input;
    
    public PlayerMover Mover {get; private set;}
    public PlayerRenderer Renderer {get; private set;}
    
    protected override void AfterInitComp()
    {
        base.AfterInitComp();

        Mover = GetCompo<PlayerMover>();
        Renderer = GetCompo<PlayerRenderer>();

        input.OnRightDashEvent += TryRightDash;
        input.OnLeftDashEvent += TryLeftDash;
    }

    private void OnDisable()
    {
        input.OnRightDashEvent -= TryRightDash;
        input.OnLeftDashEvent -= TryLeftDash;
    }

    private void Update()
    {
        Mover.SetMove(input.MoveDirection);
    }

    private void TryRightDash()
    {
        var rig = Renderer.GetRigComp<BigWingSolver>("BigWingRig");
        rig.ChangeState(WingState.Fold);
        Mover.Dash(1, () =>
        {
            Debug.Log("Dd");
            rig.ChangeState(WingState.Unfold);
        });
    }

    private void TryLeftDash()
    {
        var rig = Renderer.GetRigComp<BigWingSolver>("BigWingRig");
        rig.ChangeState(WingState.Fold);
        Mover.Dash(-1, () =>
        {
            Debug.Log("dd");
            rig.ChangeState(WingState.Unfold);
        });
    }
}
