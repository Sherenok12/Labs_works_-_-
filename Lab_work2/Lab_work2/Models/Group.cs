using System.Collections.Generic;

namespace UniversityApp.Models
{

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new();
        public override string ToString() => $"Група {Name}";
    }
}