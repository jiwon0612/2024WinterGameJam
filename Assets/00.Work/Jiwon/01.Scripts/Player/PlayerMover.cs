using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMover : EntityMover
{
    [Header("PlayerMoverSetting")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [Header("WingingSetting")]
    [SerializeField] private float wingingTime;
    public UnityEvent OnWingingEvent;
 
    private PlayerRenderer _renderer;
    
    
    public override void AfterInit()
    {
        base.AfterInit();
        _renderer = _entity.GetCompo<PlayerRenderer>();
    }

    public void Dash(int xDir, Action callback = null)
    {
        StopImmediately();
        IsCanMove = false;
        Debug.Log("Dahs");
        _rigidbody.AddForce(new Vector3(xDir, 0f) * dashSpeed, ForceMode.Impulse);
        _renderer.transform.DOLocalRotate(new Vector3(0,0,-xDir * 360), dashTime, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        StartCoroutine(DashCoroutine(callback));
    }

    private IEnumerator DashCoroutine(Action callback = null)
    {
        yield return new WaitForSeconds(dashTime);
        IsCanMove = true;
        callback?.Invoke();
    }
}
