using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : Entity
{
    [SerializeField] private PlayerInputSO input;

    [Header("Setting")] 
    [SerializeField] private float rotateValue;
    [SerializeField] private float lerpSpeed;

    public PlayerMover Mover { get; private set; }
    public PlayerRenderer Renderer { get; private set; }

    private Tween _rotateTween;

    private bool _isDash;

    protected override void AfterInitComp()
    {
        base.AfterInitComp();

        Mover = GetCompo<PlayerMover>();
        Renderer = GetCompo<PlayerRenderer>();

        input.OnRightDashEvent += TryRightDash;
        input.OnLeftDashEvent += TryLeftDash;
        Mover.OnWingingEvent.AddListener(Winging);
        Mover.MoveDirection.OnValueChanged += HandleRotatePlayer;
    }

    private void OnDisable()
    {
        Mover.OnWingingEvent.RemoveListener(Winging);
        Mover.MoveDirection.OnValueChanged -= HandleRotatePlayer;
        input.OnRightDashEvent -= TryRightDash;
        input.OnLeftDashEvent -= TryLeftDash;
    }
    
    private void HandleRotatePlayer(Vector2 prev, Vector2 next)
    {
        if (_rotateTween.IsActive()) _rotateTween.Kill();

        transform.DORotate(new Vector3(0, 0, next.x * rotateValue), lerpSpeed).SetEase(Ease.Linear);

        if (!_isDash)
        {
            if (next.y <= -0.1f)
                Renderer.ChangeState(WingState.SlowFold);
            else if (next.y >= 0.1f)
                Renderer.ChangeState(WingState.Winging);
            else if (Mathf.Approximately(next.y, 0f))
                Renderer.ChangeState(WingState.Idle);
        }
            
    }

    private void Update()
    {
        Mover.SetMove(input.MoveDirection);
    }

    private void Winging() => Renderer.ChangeState(WingState.Winging);

    private void TryRightDash()
    {
        Renderer.TailRig.SetFold(true);
        Renderer.ChangeState(WingState.Fold);
        _isDash = true;
        Mover.Dash(1, () =>
        {
            _isDash = false;
            Renderer.TailRig.SetFold(false);
            Renderer.ChangeState(WingState.Unfold);
        });
    }

    private void TryLeftDash()
    {
        Renderer.TailRig.SetFold(true);
        Renderer.ChangeState(WingState.Fold);
        _isDash = true;
        Mover.Dash(-1, () =>
        {
            _isDash = false;
            Renderer.TailRig.SetFold(false);
            Renderer.ChangeState(WingState.Unfold);
        });
    }
}