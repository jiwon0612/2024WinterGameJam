using System;
using UnityEditor;
using UnityEngine;

public class EntityMover : MonoBehaviour, IEntityComponent
{
    [Header("MoveSetting")] 
    [SerializeField] private float maxXMove;
    [SerializeField] private float maxYMove;

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
        
    protected Entity _entity;
    protected Rigidbody _rigidbody;

    private Vector3 _startPos;
    private Vector3 _maxCanMoveSpace;
    private Vector3 _minCanMoveSpace;
    private bool _isInit = false;
    
    public Vector2 MoveDirection { get; private set; }
    public bool IsCanMove { get; set; }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _rigidbody = GetComponent<Rigidbody>();
        _startPos = transform.position;
        
        _isInit = true;
        IsCanMove = true;
        
        _minCanMoveSpace = new Vector3(_startPos.x - maxXMove, _startPos.y - maxYMove, _startPos.z);
        _maxCanMoveSpace = new Vector3(_startPos.x + maxXMove, _startPos.y + maxYMove, _startPos.z);
    }

    public virtual void SetMove(Vector2 dir)
    {
        MoveDirection = dir;
    }

    public void StopImmediately()
    {
        MoveDirection = Vector2.zero;
        _rigidbody.velocity = Vector3.zero;
    }

    public virtual void ControlMove()
    {
        transform.localPosition = Vector3Clamp(transform.localPosition, _minCanMoveSpace, _maxCanMoveSpace);
        
        if (IsCanMove)
        {
            _rigidbody.velocity = MoveDirection * xSpeed;
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
