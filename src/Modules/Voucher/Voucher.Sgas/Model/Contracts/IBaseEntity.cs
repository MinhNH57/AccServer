using BuildingBlocks.SmartMapper;
using OpenTelemetry.Context.Propagation;

namespace Voucher.Sgas.Model.Contracts;

public interface IBaseEntity
{
    [SmartMapIgnore]
    public DateTime CreateDate { get; set; }
    [SmartMapIgnore]
    public string? CreateBy { get; set; }
    [SmartMapIgnore]
    public DateTime? ModifyDate { get; set; }
    [SmartMapIgnore]
    public string? ModifyBy { get; set; }
}