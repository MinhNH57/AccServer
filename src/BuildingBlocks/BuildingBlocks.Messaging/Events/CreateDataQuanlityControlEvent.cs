namespace BuildingBlocks.Messaging.Events;

public record CreateDataQuanlityControlEvent(
    int CodeUnit,
    string UserCode,
    string DataType,
    string NumberOfVoucher,
    string? Notes,
    bool IsREject,
    Guid IdVoucher) : IntergrationEvent;