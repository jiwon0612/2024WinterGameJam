using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Vector3 _lunchPos;
    protected Vector3 _basePos;
    protected Vector3 realPos;
    protected float _totalLength;
    [SerializeField]
    protected Vector3 _targetPos;

    [SerializeField]
    protected float _speed;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        transform.LookAt(_targetPos);
    }
    public void Lunch(Vector3 targetPos, Vector3 lunchPos)
    {
        if (gameObject == null)
            Instantiate(this);
        _targetPos = targetPos;
        _lunchPos = lunchPos;
        transform.position = _lunchPos;
        transform.LookAt(_targetPos);
    }

    public void FixedUpdate()
    {
        MoveBullet();
    }
    protected virtual void MoveBullet()
    {
        SetBasePos();
        SetRealPos();
        transform.GetChild(0).localPosition = realPos;
    }

    protected virtual void SetRealPos()
    {
        realPos = _basePos;
    }
    protected virtual void SetBasePos()
    {
        _basePos += Vector3.forward * _speed;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_lunchPos, _targetPos);
    }
#endif
}
