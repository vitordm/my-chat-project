namespace MyChat.Infra.Crosscutting.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity : IEntity<long>
    {
        
    }
}
