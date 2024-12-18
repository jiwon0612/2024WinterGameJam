using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    private Transform lunchPosition;
    private AttackSO _currentAttackPattern;
    [SerializeField]
    private AttackPatternSO _attackPattern;
    private void SetPattern()
    {
        _currentAttackPattern = _attackPattern.GetPattern();
    }
    private void Shot()
    {
        BulletSetSO reloadedBullet =Instantiate(_currentAttackPattern.GetBullet());
        reloadedBullet.Shot(player.position, lunchPosition.position);
    }

    private void Start()
    {
        SetPattern();
        Shot();
    }
}


