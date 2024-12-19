using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, Ipoolable
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
    [SerializeField]
    protected Color _color;
    private Color _defColor;

    private List<ParticleSystem> _trailRenderer = new List<ParticleSystem>();

    string Ipoolable.PoolName => gameObject.name;

    GameObject Ipoolable.ObjectPrefab => gameObject;

    public UnityEvent OnDeadEvent;


    private void Awake()
    {
        ParticleSystem[] trailRenderers = GetComponentsInChildren<ParticleSystem>();
        _trailRenderer = trailRenderers.ConvertTo<List<ParticleSystem>>();
        if(_trailRenderer.Count > 0)
        _defColor = _trailRenderer[0].startColor;
    }
    public void Lunch(Vector3 targetPos, Vector3 lunchPos, bool isEShot)
    {
        print(isEShot);
        _targetPos = targetPos;
        _lunchPos = lunchPos;
        transform.position = _lunchPos;
        _basePos = Vector3.zero;
        transform.LookAt(_targetPos);

        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 360f));

        if (isEShot)
        {
            foreach (var item in GetComponentsInChildren<BulletCollider>())
            {
                item.energy *= -1;
            };

            foreach (ParticleSystem trailRenderer in _trailRenderer)
            {
                trailRenderer.startColor = _color;

            }
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
        if (Util.InverseVectorLerp(_lunchPos, _targetPos, _basePos.z) >= 1.3f)
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
        foreach (ParticleSystem trailRenderer in _trailRenderer)
        {
            trailRenderer.startColor = _defColor;

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
