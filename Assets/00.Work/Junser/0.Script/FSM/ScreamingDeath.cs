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
}
