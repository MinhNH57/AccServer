namespace BuildingBlocks.Messaging.Events;

public record ConfirmVoucherEvent(string Parameter, string IdVoucher, int CodeUnit, string CodeUser, string? MoreInfo = null) : IntergrationEvent;