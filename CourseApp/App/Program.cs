


using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Exceptions;


GroupService groupService = new GroupService();
StudentService studentService = new StudentService();
ExamService examService = new ExamService();
string opt;
do
{
    Console.WriteLine("1. Add group");
    Console.WriteLine("2. Get group by id");
    Console.WriteLine("3. Get all groups");
    Console.WriteLine("4. Edit group");
    Console.WriteLine("5. Add student");
    Console.WriteLine("6. Get all students");
    Console.WriteLine("7. Delete student");
    Console.WriteLine("8. Edit student");
    Console.WriteLine("9. Create exam");
    Console.WriteLine("10. Create an exam result");
    Console.WriteLine("11. Show Exams with max point");


    opt = Console.ReadLine();


    switch (opt)
    {
        case "1":

            var group = GetGroup();
            try
            {
                groupService.Create(group);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case "2":
            int id = GetId();
            try
            {
                group = groupService.GetById(id);
                Console.WriteLine(group.Id + "-" + group.No + "-" + group.Limit + "-" + group.StartDate.ToString("yyyy-MM-dd"));
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case "3":
            var groups = groupService.GetAll();

            foreach (var item in groups)
            {
                Console.WriteLine(item.Id + "-" + item.No + "-" + item.Limit + "-" + item.Students.Count);
            }
            break;
        case "4":
            id = GetId();
            group = GetGroup();

            try
            {
                groupService.Update(id, group);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case "5":
            Student student = GetStudent();

            try
            {
                studentService.Create(student);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case "6":
            var students = studentService.GetAll();

            foreach (var std in students)
            {
                Console.WriteLine(std.Id + "-" + std.Fullname + "-" + std.Point + "-" + std.Group.No);
            }
            break;
        case "7":
            id = GetId();

            try
            {
                studentService.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case "8":
            id = GetId();
            student = GetStudent();

            try
            {
                studentService.Update(id, student);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case "9":
            Exam exam = GetExam();

            try
            {
                examService.Create(exam);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case "10":
            Console.WriteLine("Exams");
            foreach (var item in examService.GetAll())
            {
                Console.WriteLine(item.Id+"-"+item.Subject);
            }

            int examId = GetId("ExamId");

            foreach (var std in studentService.GetAll())
            {
                Console.WriteLine(std.Id + "-" + std.Fullname);
            }

            int stdId = GetId("StudentId");

            Console.Write("Point: ");
            string pointStr;
            byte point;
            do
            {
                Console.Write("Point: ");
                pointStr = Console.ReadLine();

            } while (!byte.TryParse(pointStr,out point));


            var studentExam = new StudentExam
            {
                ExamId = examId,
                StudentId = stdId,
                Point = point
            };

            try
            {
                examService.AddStudentResult(studentExam);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case "11":
            foreach (Exam item in examService.GetAll())
            {
                var result = item.StudentExams.OrderByDescending(x => x.Point).FirstOrDefault();
                Console.WriteLine(item.Id+"-"+item.Subject+"-"+ result?.Point+"("+ result?.Student.Fullname + ")");
            }
            break;
        case "0":
            break;
        default:
            break;
    }
} while (opt != "0");


Group GetGroup()
{
    Console.Write("No: ");
    string no = Console.ReadLine();
    
    string limitStr;
    int limit;
    do
    {
        Console.Write("Limit: ");
        limitStr = Console.ReadLine();
    } while (!int.TryParse(limitStr,out limit));

    string dateStr;
    DateTime date;
    do
    {
        Console.Write("StartDate: ");
        dateStr = Console.ReadLine();
    } while (!DateTime.TryParse(dateStr,out date));
   
    var group = new Group
    {
        No = no,
        Limit = limit,
        StartDate = date,
    };

    return group;

}
int GetId(string inputName="Id")
{
    string idStr;
    int id;
    do
    {
        Console.Write(inputName+": ");
        idStr = Console.ReadLine();
    } while (!int.TryParse(idStr,out id));

    return id;
}
Student GetStudent()
{
    Console.Write("Fullname:");
    string fullname = Console.ReadLine();

    string pointStr;
    byte point;
    do
    {
        Console.Write("Point:");
        pointStr = Console.ReadLine();
    } while (!byte.TryParse(pointStr, out point));

    int groupId = GetId("GroupId");

    var student = new Student
    {
       Fullname=fullname,
       GroupId = groupId,
       Point = point
    };

    return student;

}

Exam GetExam()
{
    Console.Write("Subject");
    string subject = Console.ReadLine();

    string dateStr;
    DateTime date;

    do
    {
        Console.WriteLine("StartDate");
        dateStr = Console.ReadLine();
    } while (!DateTime.TryParse(dateStr,out date));

    return new Exam
    {
        Subject = subject,
        StartDate = date,
    };
}

