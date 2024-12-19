using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField] protected EntityFSMSO _fsmSO;

    protected StateMachine _stateMachine;

    [SerializeField] public float minAttackDelay, maxAttackDelay;
    public AttackComponent _attackComponent;

    public EntityState CurrentSTate => _stateMachine.currentState;
    protected override void AfterInitComp()
    {
        base.AfterInitComp();
        _stateMachine = new StateMachine(_fsmSO, this);
        _attackComponent = GetCompo<AttackComponent>();

        _stateMachine.Initalize(StateName.Idle);

    }

    protected virtual void Start()
    {
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
