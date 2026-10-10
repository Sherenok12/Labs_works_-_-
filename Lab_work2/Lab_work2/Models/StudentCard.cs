using System;

namespace UniversityApp.Models
{

    public class StudentCard
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string CardNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public override string ToString() => $"Квиток № {CardNumber} (виданий {IssueDate:d})";
    }
}