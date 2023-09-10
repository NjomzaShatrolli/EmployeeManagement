using EmployeeManagement.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using EmployeeManagement.BO;

namespace EmployeeManagement.DAL
{
    public static class DAEmployee
    {
        public static List<Employee> ToEmployeeList(this DataTable table)
        {
            var employees = new List<Employee>();

            foreach (DataRow row in table.Rows)
            {
                var employee = new Employee()
                {
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    EmpName = Convert.ToString(row["EmpName"]),
                    EmpGen = Convert.ToString(row["EmpGen"]),
                    EmpDepartment = Convert.ToInt32(row["EmpDepartment"]),
                    EmpDOB = Convert.ToDateTime(row["EmpDOB"]),
                    EmpJoinedDate = Convert.ToDateTime(row["EmpJoinedDate"]),
                    EmpSalary = Convert.ToDecimal(row["EmpSalary"])
                };

                employees.Add(employee);
            }

            return employees;
        }

        public static void SaveEmployeeData(string EmpName, string EmpGen, int EmpDepartment, DateTime EmpDOB, DateTime EmpJoinedDate, decimal EmpSalary)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmpName", SqlDbType.VarChar) { Value = EmpName },
                new SqlParameter("@EmpGen", SqlDbType.VarChar) { Value = EmpGen },
                new SqlParameter("@Empdepartment", SqlDbType.Int) { Value = EmpDepartment },
                new SqlParameter("@EmpDOB", SqlDbType.Date) { Value = EmpDOB },
                new SqlParameter("@EmpJoinedDate", SqlDbType.Date) { Value = EmpJoinedDate },
                new SqlParameter("@EmpSalary", SqlDbType.Decimal) { Value = EmpSalary }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.CreateEmployee, parameters);

            MessageBox.Show("Employee Registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static void UpdateData(int id,string EmpName, string EmpGen, int EmpDepartment, DateTime EmpDOB, DateTime EmpJoinedDate, decimal EmpSalary)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmpId", SqlDbType.Int) { Value = id }, 
                new SqlParameter("@EmpName", SqlDbType.VarChar) { Value = EmpName },
                new SqlParameter("@EmpGen", SqlDbType.VarChar) { Value = EmpGen },
                new SqlParameter("@Empdepartment", SqlDbType.Int) { Value = EmpDepartment },
                new SqlParameter("@EmpDOB", SqlDbType.Date) { Value = EmpDOB },
                new SqlParameter("@EmpJoinedDate", SqlDbType.Date) { Value = EmpJoinedDate },
                new SqlParameter("@EmpSalary", SqlDbType.Decimal) { Value = EmpSalary }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.UpdateEmployee, parameters);

            MessageBox.Show("Record Updated Successfully!");
        }

        public static void DeleteData(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmpId", SqlDbType.Int) { Value = id }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.DeleteEmployee, parameters);

            MessageBox.Show("Record Deleted Successfully!");
        }

    }
}
