using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; } = new();
        public override string ToString() => $"Кафедра {Name}";
    }
}