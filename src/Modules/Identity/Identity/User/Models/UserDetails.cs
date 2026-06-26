namespace Identity.User.Models;

public class UserDetails
{
    public Guid Id { get; set; }
    public int? CodeUnit { get; set; }
    public string? CodeUser { get; set; }
    public string? NameUser { get; set; }
    public string? DeviceName { get; set; }
    public string? DeviceManufactureName { get; set; }
    public string? IdDevice { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }
    public string? CaCertData { get; set; }
    public string? CaSerialNumber { get; set; }
    public string? CaUserId { get; set; }
    public DateTime? TimeLoginMobile { get; set; }
    public DateTime? TimeLoginWeb { get; set; }
}
