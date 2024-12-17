using UnityEngine;

public class IKFootSelver : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform body;
    [SerializeField] private IKFootSelver otherFoot;
    [SerializeField] private float speed = 1;
    [SerializeField] private float stepDistance = 4;
    [SerializeField] private float stepLength = 4;
    [SerializeField] private float stepHeight = 1;
    [SerializeField] private Vector3 footOffset;

    private float _footSpacing;
    private Vector3 _oldPosition, _currentPosition, _newPosition;
    private Vector3 _oldNormal, _currentNormal, _newNormal;
    private float _lerp;

    private void Awake()
    {
        _footSpacing = transform.localPosition.x;
        _currentPosition = _newPosition = _oldPosition = transform.position;
        _currentNormal = _newNormal = _oldNormal = transform.up;
        _lerp = 1;
        
        
    }

    private void Update()
    {
        transform.position = _currentPosition;
        //transform.up = _currentNormal;
        
        Ray ray = new Ray(body.position + (body.right * _footSpacing), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, stepLength, whatIsGround))
        {
            if (Vector3.Distance(_newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && _lerp >= 1)
            {
                _lerp = 0;
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(_newPosition).z ? 1 : -1;
                _newPosition = info.point + (body.forward * stepHeight * direction) + footOffset;
                _newNormal = info.normal;
            }
        }

        if (_lerp < 1)
        {
            Vector3 tempPos = Vector3.Lerp(_oldPosition, _newPosition, _lerp);
            tempPos.y += Mathf.Sin(_lerp * Mathf.PI) * stepHeight;
            
            _currentPosition = tempPos;
            _currentNormal = Vector3.Lerp(_oldNormal, _newNormal, _lerp);
            _lerp += Time.deltaTime * speed;
        }
        else
        {
            _oldPosition = _newPosition;
            _oldNormal = _newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_newPosition, 0.5f);
        Gizmos.color = Color.white;
    }

    public bool IsMoving()
    {
        return _lerp < 1;
    }

}
