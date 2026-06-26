namespace BuildingBlocks.Messaging.Events;

public record DeleteFileEvent(string KeyTable, string ColumnTable) : IntergrationEvent;
