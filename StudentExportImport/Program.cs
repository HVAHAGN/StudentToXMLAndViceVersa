using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;


namespace StudentExportImport
{
    class Program
    {
        static void Main(string[] args)
        {
            int count;
            while (true)
            {
                Console.WriteLine("Please enter number...");
                int.TryParse(Console.ReadLine(), out count);
                if (count > 0)
                    break;
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error something happened!");
                }
            }
            var students = CreateRandomStudent(count);
            Shuffle(students);
            Print(students);


            ExportStudentsToXml(students);
            var xmlStudents = ImportStudentsFromXml();
            Console.WriteLine("Students list from XML");
            Print(xmlStudents);
            


            Console.ReadLine();
        }



        private static void Print(List<Student> students)
        {
            if (students == null)
            {
                Console.WriteLine("Error! the list is empty..");
                return;
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($">>>>>>>>>>>>>>>>Students List<<<<<<<<<<<<<<<<");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("FirstName\tLastName\tAge\tEmail\n");
            Console.ResetColor();

            foreach (var item in students)
            {
                Console.WriteLine($"{item.FirstName}\t{item.LastName}\t{item.Age}\t{item.Email}\n");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("---------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();

            }



        }

        public static void ExportStudentsToXml(List<Student>students)
        {
            if (students == null)
                return;
            XDocument xdoc = new XDocument();
            XElement xmlStudents = new XElement("students");
            foreach (var student in students)
            {
                XElement xmlStudent = new XElement("student");
                xmlStudent.Add(new XElement(nameof(student.FirstName), student.FirstName));
                xmlStudent.Add(new XElement(nameof(student.LastName), student.LastName));
                xmlStudent.Add(new XElement(nameof(student.Age), student.Age));
                xmlStudent.Add(new XElement(nameof(student.Email), student.Email));
                xmlStudents.Add(xmlStudent);

            }

            xdoc.Add(xmlStudents);
            xdoc.Save("students.xml");
        }



        public static List<Student> ImportStudentsFromXml()
        {
            List<Student> students = new List<Student>();
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load("students.xml");
                XmlElement xRoot = xdoc.DocumentElement;
                foreach(XmlElement xNode in xRoot)
                {
                    Student student = new Student();
                    foreach(XmlNode childNode in xNode.ChildNodes)
                    {
                        if (childNode.Name == nameof(Student.FirstName))
                            student.FirstName = childNode.InnerText;
                        if (childNode.Name == nameof(Student.LastName))
                            student.LastName = childNode.InnerText;
                        if (childNode.Name == nameof(Student.Age))
                            student.Age = Convert.ToInt32(childNode.InnerText);
                        if (childNode.Name == nameof(Student.Email))
                            student.Email = childNode.InnerText;
                    }
                    students.Add(student);
                }

                return students;

            }
            catch (FileNotFoundException ex)
            {
                return null;
            }


           
        }

        public static List<Student> CreateRandomStudent(int count)
        {
            var students = new List<Student>(count);
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                students.Add(new Student
                {
                    FirstName=$"A{i}",
                    LastName=$"A{i}yan",
                    Age=rnd.Next(16,45),
                    Email=$"A{i}gamil.com"

                });
            }
            return students;

        } 
        public static void Shuffle<T>(List<T> source)
        {
            Random rnd = new Random();
            int n = source.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var sttr = source[k];
                source[k] = source[n];
                source[n] = sttr;
            }
        }
    }
}
