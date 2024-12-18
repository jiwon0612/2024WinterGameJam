using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingIdleState : EntityState
{
    private ScreamingDeath _boss;
    private float _timer;
    private float _lastCheckTime;
    public ScreamingIdleState(Entity entity, int i) : base(entity, 1)
    {
        _boss = entity as ScreamingDeath;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("!!");
        _lastCheckTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if(_lastCheckTime + _timer < Time.time)
        {
            _lastCheckTime = Time.time;
            _boss.ChangeState(StateName.Attack);
        }
    }
}
