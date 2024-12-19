using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingDeathDeathState : EntityState
{
    private ScreamingDeath _boss;
    private float _timer;
    private float _lastCheckTime;
    public ScreamingDeathDeathState(Entity entity, int i) : base(entity, 1)
    {
        _boss = entity as ScreamingDeath;
    }
    public override void Enter()
    {
        _boss.OnDeath?.Invoke();
        base.Enter();
        _boss.Death();
    }
}
