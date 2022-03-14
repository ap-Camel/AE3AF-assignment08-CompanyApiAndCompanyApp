using System;
using System.Collections.Generic;
using System.Text;
using CompanyAppTask.Models;

namespace CompanyAppTask.Models
{
    public class Employee
    {

        public int EmId { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public int? DpId { get; set; }

        public virtual Department Dp { get; set; }

    }
}
