using System;

namespace PersonnelAccessSystem.Models
{
    public class AccessCard
    {
        public int CardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string AccessLevel { get; set; } // "Temporary" или "Permanent"
        public string PhotoPath { get; set; }
        public DateTime? EndDate { get; set; } // Дата окончания для временных пропусков
    }
}