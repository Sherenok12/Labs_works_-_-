using System.Collections.Generic;
using System.Linq;
using UniversityApp.Models;

namespace UniversityApp.Repository
{

    public class UniversityRepository
    {
        private readonly List<Group> _groups = new();
        private readonly List<Student> _students = new();
        private readonly List<StudentCard> _cards = new();
        private readonly List<Course> _courses = new();
        private readonly List<Enrollment> _enrollments = new();

        private int _groupId = 1;
        private int _studentId = 1;
        private int _cardId = 1;
        private int _courseId = 1;

        public Group AddGroup(string name)
        {
            var group = new Group { Id = _groupId++, Name = name };
            _groups.Add(group);
            return group;
        }

        public IReadOnlyList<Group> GetGroups() => _groups;

        public Student AddStudent(string fullName, int groupId)
        {
            Group group = null;

            foreach (var i in _groups)
            {
                if (i.Id == groupId)
                {
                    group = i;
                    break;
                }
            }

            if (group == null)
            {
                throw new System.ArgumentException("Групу не знайдено");
            }

            var student = new Student
            {
                Id = _studentId++,
                FullName = fullName,
                GroupId = groupId,
                Group = group
            };

            _students.Add(student);
            group.Students.Add(student);
            return student;
        }

        public IReadOnlyList<Student> GetStudents() => _students;

        public Student GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public StudentCard AddStudentCard(int studentId, string cardNumber, System.DateTime issueDate)
        {
            Student student = GetStudentById(studentId);

            if (student == null)
            {
                throw new System.ArgumentException("Студента не знайдено");
            }

            if (student.Card != null)
            {
                throw new System.InvalidOperationException("У студента вже є студентський квиток");
            }

            var card = new StudentCard
            {
                Id = _cardId++,
                StudentId = studentId,
                CardNumber = cardNumber,
                IssueDate = issueDate
            };

            _cards.Add(card);
            student.Card = card;
            return card;
        }

        public Course AddCourse(string title, int credits)
        {
            var course = new Course { Id = _courseId++, Title = title, Credits = credits };
            _courses.Add(course);
            return course;
        }

        public IReadOnlyList<Course> GetCourses() => _courses;

        public Enrollment EnrollStudent(int studentId, int courseId, int? grade = null)
        {
      
            Student student = GetStudentById(studentId);
            if (student == null)
            {
                throw new System.ArgumentException("Студента не знайдено");
            }

            Course course = null;
            foreach (var c in _courses)
            {
                if (c.Id == courseId)
                {
                    course = c;
                    break;
                }
            }
            if (course == null)
            {
                throw new System.ArgumentException("Курс не знайдено");
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                Student = student,
                CourseId = courseId,
                Course = course,
                Grade = grade
            };

            _enrollments.Add(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
            return enrollment;
        }

        public IReadOnlyList<Enrollment> GetEnrollments() => _enrollments;
    }
}
