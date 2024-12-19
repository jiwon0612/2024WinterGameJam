using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Player : Entity
{
    [SerializeField] private PlayerInputSO input;

    [Header("Setting")] 
    [SerializeField] private float rotateXValue;
    [SerializeField] private float rotateYValue;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float dashCollTime;
    [SerializeField] private float fireCollTime;
    [SerializeField] private BulletSetSO defaultBullet;
    [SerializeField] private BulletSetSO ultimateBullet;

    public UnityEvent<float> OnDashValue;
    public UnityEvent<float> OnFireValue;
    public UnityEvent OnDeadEvent;
    public UnityEvent OnShootEvent;
    public UnityEvent OnUltimateShootEvent;
    public UnityEvent OnDashEvent;

    public PlayerMover Mover { get; private set; }
    public PlayerRenderer Renderer { get; private set; }
    public AttackComponent AttackComp { get; private set; }
    
    public BoxCollider UnFoldCollider { get; private set; }
    public CapsuleCollider FoldCollider { get; private set; }
    
    public PlayerAiming Aiming { get; private set; }
    
    public PlayerUltimateComp UltimateComp { get; private set; }
    
    private ParticleSystem _particle;
    private Tween _rotateTween;
    private float _currentDashTime;
    private float _currentFireTime;

    private bool _isDash;
    private bool _isReady;
    private bool _isReadyAndFired;
    public bool _isDead;

    protected override void AfterInitComp()
    {
        base.AfterInitComp();
        
        _currentDashTime = dashCollTime;
        _currentFireTime = fireCollTime;
        _isReady = false;
        _isReadyAndFired = true;
        _isDead = false;
        
        UnFoldCollider = GetComponent<BoxCollider>();
        FoldCollider = GetComponent<CapsuleCollider>();
        _particle = GetComponentInChildren<ParticleSystem>();
        
        Mover = GetCompo<PlayerMover>();
        Renderer = GetCompo<PlayerRenderer>();
        AttackComp = GetCompo<AttackComponent>();
        Aiming = GetCompo<PlayerAiming>();
        UltimateComp = GetCompo<PlayerUltimateComp>();

        input.OnRightDashEvent += TryRightDash;
        input.OnLeftDashEvent += TryLeftDash;
        input.OnFireEvent += TryFire;
        Mover.OnWingingEvent.AddListener(Winging);
        Mover.MoveDirection.OnValueChanged += HandleRotatePlayer;
        
        FoldCollider.enabled = false;
        UnFoldCollider.enabled = true;
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

        transform.DORotate(new Vector3(-next.y * rotateYValue, 0, next.x * rotateXValue), lerpSpeed).SetEase(Ease.Linear);

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
        Aiming.SetPosition(input.MousePoint);
        if (_currentDashTime < dashCollTime)
        {
            _currentDashTime += Time.deltaTime;
            OnDashValue?.Invoke(_currentDashTime / dashCollTime);
        }

        if (_currentFireTime < fireCollTime)
        {
            _currentFireTime += Time.deltaTime;
            OnFireValue?.Invoke(_currentFireTime / fireCollTime);
        }

        if (Renderer.CurrentState == WingState.Idle || Renderer.CurrentState == WingState.Winging)
        {
            FoldCollider.enabled = false;
            UnFoldCollider.enabled = true;
        }
        else
        {
            FoldCollider.enabled = true;
            UnFoldCollider.enabled = false;
        }
    }

    private void Winging() => Renderer.ChangeState(WingState.Winging);

    private void TryRightDash()
    {
        if (_currentDashTime < dashCollTime) return;
        
        _currentDashTime = 0;
        Renderer.TailRig.SetFold(true);
        Renderer.ChangeState(WingState.Fold);
        _isDash = true;
        UltimateComp.IsCanHit = false;
        
        OnDashEvent?.Invoke();
        
        Mover.Dash(1, () =>
        {
            UltimateComp.IsCanHit = true;
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
        UltimateComp.IsCanHit = false;
        
        OnDashEvent?.Invoke();
        
        Mover.Dash(-1, () =>
        {
            UltimateComp.IsCanHit = true;
            _isDash = false;
            Renderer.TailRig.SetFold(false);
            Renderer.ChangeState(WingState.Unfold);
        });
    }

    public void StartParticle()
    {
        _particle.Play();
        _isReady = true;
    }
    
    public void Dead()
    {
        _isDead = true;
        OnDeadEvent?.Invoke();   
    }
    
    private void TryFire()
    {
        if (_isDash) return;
        if (!_isReadyAndFired) return;
        if (_isReady)
        {
            OnUltimateShootEvent?.Invoke();
            AttackComp._attackPattern[0] = ultimateBullet;
            _isReadyAndFired = false;
            AttackComp.Shot();
            return;
        }
        if (_currentFireTime < fireCollTime) return;
        _currentFireTime = 0;

        
        OnShootEvent?.Invoke();
        AttackComp.Shot();
    }
}