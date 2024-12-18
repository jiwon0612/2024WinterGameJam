using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    public EntityState currentState {get; private set;}

    private Dictionary<StateName, EntityState> _stateDictionary;

    public StateMachine(EntityFSMSO entityStates, Entity entity)
    {
        _stateDictionary = new Dictionary<StateName, EntityState>();

        foreach (var item in entityStates.states)
        {
            try
            {
                Type type = Type.GetType(item.className);
                var entityState = Activator.CreateInstance(type, entity, 0) as EntityState;
                _stateDictionary.Add(item.stateName, entityState);
            }
            catch (Exception e)
            {
                Debug.LogError($"{item.className} not found");
            }
        }
    }

    public EntityState GetState(StateName name)
    {
        return _stateDictionary.GetValueOrDefault(name);
    }

    public void Initalize(StateName startState)
    {
        currentState = GetState(startState);
        currentState.Enter();
    }

    public void UpdateStateMachine()
    {
        currentState.Update();
    }

    public void ChangeState(StateName newState)
    {
        currentState.Exit();
        EntityState nextState = GetState(newState);
        Debug.Assert(nextState != null, $"State : {newState} not found");
        currentState = nextState;
        currentState.Enter();
    }
}
