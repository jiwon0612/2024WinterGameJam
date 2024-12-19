using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackComponent : MonoBehaviour, IEntityComponent
{
    [SerializeField]
    Transform player;
    [SerializeField]
    private Transform lunchPosition;
    private BulletSetSO _currentAttackPattern;
    [SerializeField]
    private List<BulletSetSO> _attackPattern;

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
            _currentAttackPattern.Shot(player.position, lunchPosition.position);
            yield return new WaitForSeconds(_currentAttackPattern.shotDelay);
        }
        yield return null;
    }
}


