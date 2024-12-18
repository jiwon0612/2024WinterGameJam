using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMover : EntityMover
{
    [Header("PlayerMoverSetting")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;

    public void Dash(int xDir, Action callback = null)
    {
        StopImmediately();
        IsCanMove = false;
        _rigidbody.AddForce(new Vector3(xDir, 0f) * _dashSpeed, ForceMode.Impulse);
        transform.DORotate(new Vector3(0,0,-xDir * 360), _dashTime, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        StartCoroutine(DashCoroutine(callback));
    }
    
    private IEnumerator DashCoroutine(Action callback = null)
    {
        yield return new WaitForSeconds(_dashTime);
        IsCanMove = true;
        callback?.Invoke();
    }
}
