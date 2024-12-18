using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingAttackState : EntityState
{
    private ScreamingDeath _boss;
    public ScreamingAttackState(Entity entity, int i) : base(entity, i)
    {
        _boss = entity as ScreamingDeath;
    }
    public override void Enter()
    {
        base.Enter();
        _boss._attackComponent.Shot();
        Debug.Log("!!");
    }

    public override void Update()
    {
        base.Update();
        _boss.ChangeState(StateName.Idle);
    }
}
