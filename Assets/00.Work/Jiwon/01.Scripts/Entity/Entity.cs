using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();
        GetComponentsInChildren<IEntityComponent>(true).ToList()
            .ForEach(comp => _components.Add(comp.GetType(), comp));
        
        InitCompo();
        AfterInitComp();
    }


    protected virtual void InitCompo()
    {
        _components.Values.ToList().ForEach(comp => comp.Initialize(this));
    }
    
    protected virtual void AfterInitComp()
    {
        _components.Values.ToList().ForEach(comp =>
        {
            if (comp is IEntityAfterInitable after)
            {
                after.AfterInit();
            }
        });    
    }

    public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
    {
        if (_components.TryGetValue(typeof(T), out IEntityComponent comp))
        {
            return (T)comp;
        }

        if (!isDerived) return default(T);
        
        Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));
        if (findType != null)
            return (T)_components[findType];
        
        return default;
    }
}
