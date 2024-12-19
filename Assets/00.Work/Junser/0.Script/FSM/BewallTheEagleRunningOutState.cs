using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BewallTheEagleRunningOutState : EntityState
{
    private BewallTheEagle _boss;
    private Collider _collider;
    public BewallTheEagleRunningOutState(Entity entity, int i) : base(entity, i)
    {
        _boss = entity as BewallTheEagle;
    }

    public override void Enter()
    {
        base.Enter();
        _boss.OnRunOut?.Invoke();
        _collider = _boss.GetComponent<Collider>();
        _collider.isTrigger = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_boss.transform.DOMove(_boss._attackComponent.player.position - _boss.transform.position, _boss.runoutTime)
            .SetEase(Ease.InQuint)
            .SetLoops(2, LoopType.Yoyo))
            .AppendCallback(()=>
            { 
                _boss.ChangeState(StateName.Idle);
                _collider.isTrigger = false;
            });
        sequence.Play();
    }
}
