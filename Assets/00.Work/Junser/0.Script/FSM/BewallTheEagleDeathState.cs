using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BewallTheEagleDeathState : EntityState
{
    private BewallTheEagle _boss;
    private float _timer;
    private float _lastCheckTime;
    public BewallTheEagleDeathState(Entity entity, int i) : base(entity, 1)
    {
        _boss = entity as BewallTheEagle;
    }
    public override void Enter()
    {
        base.Enter();
        _boss.Death();
    }
}
