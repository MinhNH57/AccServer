namespace BuildingBlocks.Messaging.Events;

public interface ICreatedSuccessfully
{
    public Guid Id { get; }
    public int RefType { get; }
}
