using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Animations.Rigging;

public class EntityRenderer : MonoBehaviour, IEntityComponent
{
    protected Entity _entity;
    private Dictionary<string, IRigAnimControl> _rigControls;

    public void Initialize(Entity entity)
    {
        _entity = entity;

        _rigControls = new Dictionary<string, IRigAnimControl>();
        GetComponentsInChildren<IRigAnimControl>(true).ToList()
            .ForEach((rig) => _rigControls.Add(rig.RigObject.name, rig));
        
        RigCompInit();
    }

    private void RigCompInit()
    {
        _rigControls.Values.ToList().ForEach((rig) => rig.InitAnimControl(this));
    }
    
    public T GetRigComp<T>(string name) where T : IRigAnimControl
    {
        if (_rigControls.TryGetValue(name, out IRigAnimControl rig))
        {
            return (T)rig;
        }

        Debug.LogError($"{name} not found");
        return default(T);
    }
}