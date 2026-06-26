namespace SmartAccCloud.Domain.Entity.SmartFund
{
    public class RuleBaseUnion
    {
        public string? CodeUser { get; set; }
        public int? CodeUnitOk { get; set; }
        public int? CodeUnit { get; set; }
        public string? NameUnit { get; set; }
        public bool IsAllow { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Notes { get; set; }
        public string? CodeObject { get; set; }
        public string? GrpCode { get; set; }
        public string? GrpName { get; set; }
    }
}
