namespace TinyIoc.Contract
{
    public interface IComponentLifetime
    {
        ISharingLifetimeScope FindScope(ISharingLifetimeScope scope);
    }
}
