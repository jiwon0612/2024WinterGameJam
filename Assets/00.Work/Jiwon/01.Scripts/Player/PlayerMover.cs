using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : EntityMover
{
    [Header("PlayerMoverSetting")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;

    public void Dash(int xDir)
    {
        StopImmediately();
        IsCanMove = false;
        _rigidbody.AddForce(new Vector3(xDir, 0f) * _dashSpeed, ForceMode.Impulse);
        StartCoroutine(DashCoroutine());
    }
    
    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(_dashTime);
        IsCanMove = true;
    }
}
