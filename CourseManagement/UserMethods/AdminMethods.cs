using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.UserMethods {
    static class AdminMethods {
        private static string createUserPrompt = @"";
        private static string createCoursePrompt = @"";
        private static string assignTeacherPrompt = @"";
        private static string enrollStudentPrompt = @"";

        public static void CreateAdmin() {
            Console.Clear();
            Console.WriteLine(createCoursePrompt);
            Console.WriteLine("Create Admin");
            Console.ReadKey(true);
        }
        public static void CreateTeacher() {
            Console.Clear();
            Console.WriteLine(createCoursePrompt);
            Console.WriteLine("Create Teacher");
            Console.ReadKey(true);
        }
        public static void CreateStudent() {
            Console.Clear();
            Console.WriteLine(createCoursePrompt);
            Console.WriteLine("Create Student");
            Console.ReadKey(true);
        }
        public static void CreateCourse() {
            Console.Clear();
            Console.WriteLine(createCoursePrompt);
            Console.WriteLine("Create Course");
            Console.ReadKey(true);
        }
        public static void AssignTeacher() {
            Console.Clear();
            Console.WriteLine(assignTeacherPrompt);
            Console.WriteLine("Assign Teacher to Course");
            Console.ReadKey(true);
        }
        public static void EnrollStudent() {
            Console.Clear();
            Console.WriteLine(enrollStudentPrompt);
            Console.WriteLine("Enroll Student to Course");
            Console.ReadKey(true);
        }
    }
}
