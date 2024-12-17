public interface IEntityComponent
{
    public void Initialize(Entity entity);
}

public interface IEntityAfterInitable : IEntityComponent
{
    public void AfterInit();
}
