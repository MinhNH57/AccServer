using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain.Entity.SmartFund
{
    public class SmartFileAttach
    {
        [Key]
        public Guid IdContents { get; set; }
        public string? Description { get; set; }
        public string? PathFile { get; set; }
        public string? FileNames { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public string? NumberOfVouchers { get; set; }
        public string? SizeFile { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? TypeFile { get; set; }
    }
}
