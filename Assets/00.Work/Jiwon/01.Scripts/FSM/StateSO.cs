using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/FSM/State")]
public class StateSO : ScriptableObject
{
    public StateName stateName;
    public string className;
    
}
