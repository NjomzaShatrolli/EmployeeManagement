using System;
namespace EmployeeManagement.BO
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int Attendance { get; set; }
        public string PeriodTime { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }

        public Employee Employee { get; set; }
    }
}
