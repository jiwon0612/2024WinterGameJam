using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BewallTheEagleIdleState : EntityState
{
    private BewallTheEagle _boss;
    private float _timer;
    private float _lastCheckTime;
    public BewallTheEagleIdleState(Entity entity, int i) : base(entity, 1)
    {
        _boss = entity as BewallTheEagle;
    }
    public override void Enter()
    {
        base.Enter();
        _lastCheckTime = Time.time;
        _timer = Random.Range(_boss.minAttackDelay, _boss.maxAttackDelay);
    }

    public override void Update()
    {
        base.Update();
        if (_lastCheckTime + _timer < Time.time)
        {
            _lastCheckTime = Time.time;
            _boss.ChangeState(StateName.Attack);
        }
    }
}
