using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AttackPatternList")]
public class AttackPatternSO : ScriptableObject
{
    public List<AttackSO> attackList;
    private int index = 0;
    public AttackSO GetPattern()
    {
        index = index % attackList.Count;
        return attackList[index];
    }
}