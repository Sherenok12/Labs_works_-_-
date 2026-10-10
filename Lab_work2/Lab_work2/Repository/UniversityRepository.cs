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
        private readonly List<Department> _departments = new();
        private readonly List<Teacher> _teachers = new();

        private int _departmentId = 1;
        private int _teacherId = 1;
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

        public Course AddCourse(string title, int credits, int teacherId)
        {
            Teacher teacher = GetTeacherById(teacherId);
            if (teacher == null)
            {
                throw new System.ArgumentException("Викладача не знайдено");
            }

            var course = new Course
            {
                Id = _courseId++,
                Title = title,
                Credits = credits,
                TeacherId = teacherId,
                Teacher = teacher
            };

            _courses.Add(course);
            teacher.Courses.Add(course);
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

            if (_enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
            {
                throw new System.InvalidOperationException("Студент уже записаний на цей курс");
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

        public Department AddDepartment(string name)
        {
            var department = new Department { Id = _departmentId++, Name = name };
            _departments.Add(department);
            return department;
        }

        public IReadOnlyList<Department> GetDepartments() => _departments;

        public Teacher AddTeacher(string fullName, int departmentId)
        {
            Department department = _departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
            {
                throw new System.ArgumentException("Кафедру не знайдено");
            }

            var teacher = new Teacher
            {
                Id = _teacherId++,
                FullName = fullName,
                DepartmentId = departmentId,
                Department = department
            };

            _teachers.Add(teacher);
            department.Teachers.Add(teacher);
            return teacher;
        }

        public IReadOnlyList<Teacher> GetTeachers() => _teachers;

        public Teacher GetTeacherById(int id) => _teachers.FirstOrDefault(t => t.Id == id);
    }
}