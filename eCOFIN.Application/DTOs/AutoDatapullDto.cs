namespace eCOFIN.Application.DTOs
{
    public class AutoDatapullDto
    {
        public long RowId { get; set; }
        public string ActivityName { get; set; }
        public int ExecuteOrder { get; set; }
        public string ExecutedResults { get; set; }
        public string FromDataBaseName { get; set; }
        public string FromIdfieldName { get; set; }
        public string FromServerName { get; set; }
        public string FromTableName { get; set; }
        public DateTime? LastExecutedDate { get; set; }
        public DateTime LastPulledDate { get; set; }
        public string PullActive { get; set; }
        public string ReportId { get; set; }
        public string SynchroniseType { get; set; }
        public string TableName { get; set; }
        public string ToDataBaseName { get; set; }
        public string ToIdfieldName { get; set; }
        public string ToServerName { get; set; }
        public string ToTableName { get; set; }
        public string WhereCondition { get; set; }
    }
}
