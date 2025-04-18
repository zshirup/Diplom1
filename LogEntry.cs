using System;

namespace PersonnelAccessSystem.Models
{
    public class LogEntry
    {
        public int LogId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}