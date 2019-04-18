using System;
using System.Collections.Generic;
using System.Text;

namespace lab4
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Premium { get; set; }

        public virtual Employe SupplierEmploye { get; set; }
    }
}
