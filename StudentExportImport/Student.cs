using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentExportImport
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName+" "+LastName;
        public int Age { get; set; }
        public string Email { get; set; }

    }
}
