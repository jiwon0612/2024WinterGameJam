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
    [SerializeField] private float dashCollTime;
    [SerializeField] private float fireCollTime;

    public PlayerMover Mover { get; private set; }
    public PlayerRenderer Renderer { get; private set; }

    private Tween _rotateTween;
    private float _currentDashTime;
    private float _currentFireTime;

    private bool _isDash;

    protected override void AfterInitComp()
    {
        base.AfterInitComp();
        
        _currentDashTime = dashCollTime;
        _currentFireTime = fireCollTime;
        
        Mover = GetCompo<PlayerMover>();
        Renderer = GetCompo<PlayerRenderer>();

        input.OnRightDashEvent += TryRightDash;
        input.OnLeftDashEvent += TryLeftDash;
        input.OnFireEvent += TryFire;
        Mover.OnWingingEvent.AddListener(Winging);
        Mover.MoveDirection.OnValueChanged += HandleRotatePlayer;
    }

    private void OnDisable()
    {
        Mover.OnWingingEvent.RemoveListener(Winging);
        Mover.MoveDirection.OnValueChanged -= HandleRotatePlayer;
        input.OnFireEvent -= TryFire;
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
        if (_currentDashTime < dashCollTime)
            _currentDashTime += Time.deltaTime;

        if (_currentFireTime < fireCollTime)
            _currentFireTime += Time.deltaTime;
    }

    private void Winging() => Renderer.ChangeState(WingState.Winging);

    private void TryRightDash()
    {
        if (_currentDashTime < dashCollTime) return;
        
        _currentDashTime = 0;
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
        if (_currentDashTime < dashCollTime) return;
        
        _currentDashTime = 0;
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
    
    private void TryFire()
    {
        if (_isDash) return;
        if (_currentFireTime < fireCollTime) return;
        _currentFireTime = 0;
        Debug.Log("슈우웃");
    }
}