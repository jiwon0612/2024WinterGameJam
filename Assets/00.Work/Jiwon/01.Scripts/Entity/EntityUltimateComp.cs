using UnityEngine;
using UnityEngine.Events;

public class EntityUltimateComp : MonoBehaviour, IEntityComponent
{
    [Header("UltimateSetting")] 
    public float maxUltimateValue;
    public UnityEvent<float> OnUltimateChange;
    public UnityEvent OnMaxUltimateCharge;
    
    protected Entity _entity;
    protected float _currentUltimateValue;
    
    public bool IsCanHit { get; set; }
    
    public virtual void Initialize(Entity entity)
    {
        _entity = entity;    
        _currentUltimateValue = maxUltimateValue;
        IsCanHit = true;
    }

    public virtual void SetUltimateValue(float value)
    {
        if (!IsCanHit) return;
        
        _currentUltimateValue += value;
        if (_currentUltimateValue >= maxUltimateValue)
        {
            OnMaxUltimateCharge?.Invoke();
        }
        OnUltimateChange?.Invoke(_currentUltimateValue / maxUltimateValue);
    }
}
