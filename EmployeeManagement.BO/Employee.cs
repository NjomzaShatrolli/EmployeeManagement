using System;

namespace EmployeeManagement.BO
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string EmpGen { get; set; }
        public int EmpDepartment { get; set; }
        public DateTime EmpDOB { get; set; }
        public DateTime EmpJoinedDate { get; set; }
        public decimal EmpSalary { get; set; }
        public Department Department { get; set; }

        public override string ToString()
        {
            return EmpName; // Return the employee name for display
        }
    }
}
