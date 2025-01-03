using System;
using UnityEditor;
using UnityEngine;

public class EntityMover : MonoBehaviour, IEntityAfterInitable
{
    [Header("MoveSetting")] 
    [SerializeField] private float maxXMove;
    [SerializeField] private float maxYMove;

    [SerializeField] protected float xSpeed;
    [SerializeField] protected float ySpeed;
        
    protected Entity _entity;
    protected Rigidbody _rigidbody;

    protected Vector3 _startPos;
    protected Vector3 _maxCanMoveSpace;
    protected Vector3 _minCanMoveSpace;
    private bool _isInit = false;
    
    public NotifyValue<Vector2> MoveDirection { get; private set; }
    public bool IsCanMove { get; set; }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _rigidbody = GetComponent<Rigidbody>();
        MoveDirection = new NotifyValue<Vector2>();
        _startPos = transform.position;
        
        _isInit = true;
        IsCanMove = true;
        
        _minCanMoveSpace = new Vector3(_startPos.x - maxXMove, _startPos.y - maxYMove, _startPos.z);
        _maxCanMoveSpace = new Vector3(_startPos.x + maxXMove, _startPos.y + maxYMove, _startPos.z);
    }

    public virtual void AfterInit()
    {
        
    }

    public virtual void SetMove(Vector2 dir)
    {
        MoveDirection.Value = dir;
    }

    public void StopImmediately()
    {
        MoveDirection.Value = Vector2.zero;
        _rigidbody.velocity = Vector3.zero;
    }

    public virtual void ControlMove()
    {
        transform.localPosition = Vector3Clamp(transform.localPosition, _minCanMoveSpace, _maxCanMoveSpace);
        
        if (IsCanMove)
        {
            _rigidbody.velocity = MoveDirection.Value * xSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        ControlMove();
    }

    private void OnDrawGizmos()
    {
        if (_isInit)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(_startPos, new Vector3(maxXMove * 2, maxYMove * 2, 2));
            Gizmos.color = Color.white;
        }
    }

    public Vector3 Vector3Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(value.x, min.x, max.x);
        float y = Mathf.Clamp(value.y, min.y, max.y);
        float z = Mathf.Clamp(value.z, min.z, max.z);
        
        return new Vector3(x, y, z);
    }
}
