using System;
using UniversityApp.Repository;
using UniversityApp.Services;

namespace UniversityApp
{
    class Program
    {
        static void Main()
        {
            var repository = new UniversityRepository();
            var infoService = new UniversityInfoService(repository);

            var depIpz = repository.AddDepartment("ІПЗ");
            var depKn = repository.AddDepartment("КН");

            var trenov = repository.AddTeacher("Треньов М.Г.", depIpz.Id);
            var kovalenko = repository.AddTeacher("Коваленко І.П.", depIpz.Id);
            var melnyk = repository.AddTeacher("Мельник О.В.", depKn.Id);

            var pd31 = repository.AddGroup("ПД-31");
            var ps32 = repository.AddGroup("ПД-32");

            var ivan = repository.AddStudent("Тараканов Родион", pd31.Id);
            var maria = repository.AddStudent("Трошкин Роман", ps32.Id);
            var oleg = repository.AddStudent("Шеренок Олександр", ps32.Id);

            repository.AddStudentCard(ivan.Id, "S-01", new DateTime(2026, 9, 1));
            repository.AddStudentCard(maria.Id, "S-02", new DateTime(2026, 9, 1));
            repository.AddStudentCard(oleg.Id, "S-03", new DateTime(2026, 9, 1));

            var programming = repository.AddCourse("Програмування", 5, trenov.Id);
            var databases = repository.AddCourse("Бази даних", 4, kovalenko.Id);
            var math = repository.AddCourse("Вища математика", 3, melnyk.Id);

            repository.EnrollStudent(ivan.Id, programming.Id, 91);
            repository.EnrollStudent(ivan.Id, databases.Id, 88);
            repository.EnrollStudent(maria.Id, programming.Id, 91);
            repository.EnrollStudent(maria.Id, math.Id, 84);
            repository.EnrollStudent(oleg.Id, databases.Id, 77);
            repository.EnrollStudent(oleg.Id, math.Id, 90);

            infoService.PrintAll();
            infoService.PrintStudentFullInfo(ivan.Id);
        }
    }
}