public class PlayerUltimateComp : EntityUltimateComp
{
    public override void Initialize(Entity entity)
    {
        _entity = entity;
        _currentUltimateValue = 0;
        
        IsCanHit = true;
    }
    
    public override void SetUltimateValue(float value)
    {
        if (!IsCanHit) return;
        
        _currentUltimateValue -= value;
        if (_currentUltimateValue <= 0)
        {
            OnMaxUltimateCharge?.Invoke();
        }
        OnUltimateChange?.Invoke(_currentUltimateValue);
    }
}
