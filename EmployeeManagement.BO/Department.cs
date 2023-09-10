
namespace EmployeeManagement.BO
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepName { get; set; }

        public override string ToString()
        {
            return DepName; // Return the department name for display
        }
    }
}
