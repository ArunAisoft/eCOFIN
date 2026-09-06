namespace eCOFIN.Application.DTOs.Masters
{
    // ── Read DTO (returned to grid) ───────────────────────────────────────────
    public class BankInstrumentDto
    {
        public string  BankCode           { get; set; } = string.Empty;
        public string? BankName           { get; set; }
        public string  AccountCode        { get; set; } = string.Empty;
        public string? AccountDescription { get; set; }
        public string? InstrumentCategory { get; set; }
        public string? InstrumentType     { get; set; }
        public decimal InstrumentBookNo   { get; set; }
        public string? BookDescription    { get; set; }
        public decimal StartingSerialNo   { get; set; }
        public decimal EndingSerialNo     { get; set; }
        public decimal?   RunningSerialNo    { get; set; }
        public decimal?   InstrumentLeaves   { get; set; }
        public string? ActiveStatus       { get; set; }
    }

    // ── Create / Update model (sent from Angular) ─────────────────────────────
    public class BankInstrumentCreateModel
    {
        public string  BankCode           { get; set; } = string.Empty;
        public string  AccountCode        { get; set; } = string.Empty;
        public string? InstrumentCategory { get; set; }
        public string? InstrumentType     { get; set; }
        public decimal?    InstrumentBookNo   { get; set; }   // null on new → auto-assigned
        public string? BookDescription    { get; set; }
        public decimal StartingSerialNo   { get; set; }
        public decimal EndingSerialNo     { get; set; }
        public decimal?   RunningSerialNo    { get; set; }
        public decimal?   InstrumentLeaves   { get; set; }
        public string? ActiveStatus       { get; set; }
        public string? Username           { get; set; }
        public string? Location           { get; set; }
        public string? AccPeriod          { get; set; }
    }
}
