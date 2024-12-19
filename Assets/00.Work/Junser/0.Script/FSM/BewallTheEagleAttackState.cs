using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BewallTheEagleAttackState : EntityState
{
    private BewallTheEagle _boss;
    public BewallTheEagleAttackState(Entity entity, int i) : base(entity, i)
    {
        _boss = entity as BewallTheEagle;
    }
    public override void Enter()
    {
        base.Enter();
        if (Random.Range(0,1f)<0.05f)
        {
            _boss.ChangeState(StateName.RunOut);
        }
        _boss._attackComponent.Shot();
    }

    public override void Update()
    {
        base.Update();
        _boss.ChangeState(StateName.Idle);
    }
}
