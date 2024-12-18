using UnityEngine;

public abstract class EntityState
{
    protected Entity _entity;
    protected bool _isTriggerCall;
    

    public EntityState(Entity entity)
    {
        _entity = entity;
        
        _isTriggerCall = false;
    }
    
    public virtual void Enter() {}
    public virtual void Update() {}
    public virtual void Exit() {}

    public virtual void AnimationEndTrigger()
    {
        _isTriggerCall = true;
    }
}
