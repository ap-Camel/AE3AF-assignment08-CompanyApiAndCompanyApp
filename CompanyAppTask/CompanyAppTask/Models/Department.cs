using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyAppTask.Models
{
    public class Department
    {
        public int dpId { get; set; }
        public string name { get; set; }
        public List<Employee> employees { get; set; }
    }
}
