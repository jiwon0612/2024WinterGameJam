using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateName
{
    Idle, Attack
}

[CreateAssetMenu(menuName = "SO/FSM")]
public class EntityFSMSO : ScriptableObject
{
    public List<StateSO> states;
    public Dictionary<StateName, StateSO> _statesDictionary;
    
    public StateSO this[StateName stateName] => _statesDictionary.GetValueOrDefault(stateName);

    private void OnEnable()
    {
        if (states == null) return;
        
        _statesDictionary = new Dictionary<StateName, StateSO>();
        foreach (var state in states)
        {
            _statesDictionary.Add(state.stateName, state);
        }
    }
}

