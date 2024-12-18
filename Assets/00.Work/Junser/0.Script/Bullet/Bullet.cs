using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour,Ipoolable
{
    protected Rigidbody _rb;
    protected Vector3 _lunchPos;
    protected Vector3 _basePos;
    protected Vector3 realPos;
    protected float _totalLength;
    [SerializeField]
    protected Vector3 _targetPos;
    private bool started = false;
    [SerializeField]
    protected float _speed;

    string Ipoolable.PoolName => gameObject.name;

    GameObject Ipoolable.ObjectPrefab => gameObject;

    public void Lunch(Vector3 targetPos, Vector3 lunchPos)
    {
        if (gameObject == null)
            Instantiate(this);
        _targetPos = targetPos;
        _lunchPos = lunchPos;
        transform.position = _lunchPos;
        _basePos = Vector3.zero;
        transform.position = _lunchPos;
        transform.LookAt(_targetPos);
        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 360f));
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
        print(realPos);
    }
    protected virtual void SetBasePos()
    {
        _basePos += Vector3.forward * _speed;
    }
    void Ipoolable.ResetItem()
    {
        transform.localPosition = Vector3.zero;
        foreach (Transform child in transform)
        {
            child.localPosition = Vector3.zero;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_lunchPos, _targetPos);
    }
#endif
}
