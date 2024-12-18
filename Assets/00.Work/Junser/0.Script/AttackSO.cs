using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack")]
public class AttackSO : ScriptableObject
{
    public List<BulletSetSO> patterns;
    private int index;
    public BulletSetSO GetBullet()
    {
        index = index % patterns.Count;
        return patterns[index];
    }
}
