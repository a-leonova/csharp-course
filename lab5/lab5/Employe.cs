using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    public class Employe
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}
