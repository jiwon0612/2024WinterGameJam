using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Animations.Rigging;

public class EntityRenderer : MonoBehaviour, IEntityComponent
{
    protected Entity _entity;
    private Dictionary<Type, IRigAnimControl> _rigControls;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        
        _rigControls = new Dictionary<Type, IRigAnimControl>();
        GetComponentsInChildren<IRigAnimControl>(true).ToList().ForEach((rig) => _rigControls.Add(rig.GetType(), rig));
    }

    public T GetRigComp<T>(Type findType) where T : IRigAnimControl
    {
        if (_rigControls.TryGetValue(typeof(T), out IRigAnimControl rigControl))
        {
            return (T)rigControl;
        }
        else
        {
            Debug.LogError($"{typeof(T)} not found");
            return default;
        }
    }
}
