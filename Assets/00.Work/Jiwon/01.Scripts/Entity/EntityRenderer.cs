using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

public class EntityRenderer : MonoBehaviour, IEntityAfterInitable
{
    [HideInInspector] public Entity entity;
    private Dictionary<string, IRigAnimControl> _rigControls;
    public Animator Animator { get; private set; }

    public Action OnAnimationTrigger;
    
    public void Initialize(Entity entity)
    {
        this.entity = entity;

        Animator = GetComponent<Animator>();
        
        _rigControls = new Dictionary<string, IRigAnimControl>();
        GetComponentsInChildren<IRigAnimControl>(true).ToList()
            .ForEach((rig) => _rigControls.Add(rig.RigObject.name, rig));
        
        RigCompInit();
    }

    public virtual void AfterInit()
    {
        
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

    public void AnimationTrigger()
    {
        OnAnimationTrigger?.Invoke();
    }
}