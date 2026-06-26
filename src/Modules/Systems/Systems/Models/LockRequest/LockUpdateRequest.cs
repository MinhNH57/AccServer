using Lock = Systems.Infrastructure.Entities.Lock;

namespace Systems.Models.LockRequest;

public class LockUpdateRequest
{
    public LockRequestStore Query { get; set; } = new();
    public List<Lock> LstLockEdit { get; set; } = new();
}