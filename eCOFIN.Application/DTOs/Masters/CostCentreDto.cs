namespace eCOFIN.Application.DTOs.Masters
{
    // ── Read DTO  (returned to Angular) ──────────────────────────────────────
    public class CostCentreDto
    {
        public string  CostCentreCode   { get; set; } = string.Empty;  // COSTCENTRECODE
        public string? Description      { get; set; }                  // DESCRIPTION
        public string? CentreType       { get; set; }                  // CENTRETYPE
        public string? CostCentreStatus { get; set; }                  // COSTCENTRESTATUS
        public string? ObjectStatus     { get; set; }                  // OBJECTSTATUS → "ACTVE" | "INACTV"
    }

    // ── Create / Update model  (sent from Angular) ────────────────────────────
    public class CostCentreCreateModel
    {
        public string  CostCentreCode   { get; set; } = string.Empty;
        public string? Description      { get; set; }
        public string? CentreType       { get; set; }
        public string? CostCentreStatus { get; set; }
        public string? ObjectStatus     { get; set; }   // "ACTVE" | "INACTV"

        // Audit fields
        public string? Username         { get; set; }
        public string? Location         { get; set; }
        public string? AccPeriod        { get; set; }
    }
}
