using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Course> Courses { get; set; } = new();
        public override string ToString() => $"{FullName} (ID: {Id})";
    }
}