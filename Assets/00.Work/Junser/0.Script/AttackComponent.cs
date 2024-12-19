using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour, IEntityComponent
{
    public Transform player;
    [SerializeField]
    private Transform lunchPosition;
    private BulletSetSO _currentAttackPattern;
    [SerializeField]
    public List<BulletSetSO> _attackPattern;
    [SerializeField]
    private Vector3 offset;

    private int _antiDamageShotCount = 0;
    bool _eshot;

    public UnityEvent OnAttack;
    public void Initialize(Entity entity)
    {
        
    }
    public void Shot()
    {
        _currentAttackPattern = _attackPattern[Random.Range(0, _attackPattern.Count)];
        StartCoroutine(ShotDelay());
    }

    private IEnumerator ShotDelay()
    {
        for(int i = 0; i < _currentAttackPattern.shotAmount; i++)
        {
            if(_antiDamageShotCount <= 1)
            {
                _eshot = Random.Range(1,_currentAttackPattern.shotAmount) == 1;
                _antiDamageShotCount++;
            }
            else
            {
                _eshot = false;
            }
            _currentAttackPattern.Shot(player.position+offset, lunchPosition.position, _eshot);
            OnAttack?.Invoke();
            yield return new WaitForSeconds(_currentAttackPattern.shotDelay);
        }
        _antiDamageShotCount = 0;
        yield return null;
    }
}


