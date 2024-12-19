using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    private List<TrailRenderer> _trailRenderer = new List<TrailRenderer>();

    string Ipoolable.PoolName => gameObject.name;

    GameObject Ipoolable.ObjectPrefab => gameObject;

    public UnityEvent OnDeadEvent;


    private void Awake()
    {
        TrailRenderer[] trailRenderers = GetComponentsInChildren<TrailRenderer>();
        _trailRenderer = trailRenderers.ConvertTo<List<TrailRenderer>>();
    }
    public void Lunch(Vector3 targetPos, Vector3 lunchPos)
    {
        _targetPos = targetPos;
        _lunchPos = lunchPos;
        transform.position = _lunchPos;
        _basePos = Vector3.zero;
        transform.LookAt(_targetPos);

        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 360f));

        foreach (TrailRenderer trailRenderer in _trailRenderer)
        {
            trailRenderer.Clear();
            trailRenderer.emitting = true;
        }
    }

    public void FixedUpdate()
    {
        MoveBullet();
        ClacArrive();
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

    private void ClacArrive()
    {
        if (Util.InverseVectorLerp(_lunchPos, _targetPos, _basePos.z)>=1.3f)
        {
            BulletDetected();
        }
    }

    public void BulletDetected()
    {
        OnDeadEvent?.Invoke();
        PoolManager.Instance.Push(this);
    }
    void Ipoolable.ResetItem()
    {
        transform.localPosition = Vector3.zero;
        foreach (Transform child in transform)
        {
            child.localPosition = Vector3.zero;
            
        }
        foreach (TrailRenderer trailRenderer in _trailRenderer)
        {
            trailRenderer.emitting =false;
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
