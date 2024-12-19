using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField] protected EntityFSMSO _fsmSO;
    [SerializeField] protected Vector3 _deathPosition;

    protected StateMachine _stateMachine;

    [SerializeField] public float minAttackDelay, maxAttackDelay;
    public AttackComponent _attackComponent;
    protected BossMove _bossMove;
    public EntityState CurrentState => _stateMachine.currentState;
    protected override void AfterInitComp()
    {
        base.AfterInitComp();
        _stateMachine = new StateMachine(_fsmSO, this);
        _attackComponent = GetCompo<AttackComponent>();
        _bossMove = GetCompo<BossMove>();
        _stateMachine.Initalize(StateName.Idle);
    }

    protected virtual void Start()
    {
    }

    public virtual void Death()
    {
        _bossMove.enabled = false;
    }

    protected virtual void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    public void ChangeState(StateName stateName)
    {
        _stateMachine.ChangeState(stateName);
    }

    public EntityState GetState(StateSO state)
        => _stateMachine.GetState(state.stateName);
}
