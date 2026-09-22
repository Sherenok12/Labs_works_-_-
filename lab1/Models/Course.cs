using System.Collections.Generic;

namespace UniversityApp.Models
{
 
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();
        public override string ToString() => $"{Title} ({Credits} кредитів)";
    }
}
