using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingDeath : Boss
{
    [SerializeField]
    float time;
    protected override void AfterInitComp()
    {
        base.AfterInitComp();

        ChangeState(StateName.Idle);
    }

    public override void Death()
    {
        base.Death();
        transform.DOMove(_deathPosition, 7);
    }
}
