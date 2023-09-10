using EmployeeManagement.BO;
using EmployeeManagement.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagement.DAL
{
    public static class DASalary
    {
        public static List<Salary> ToSalaryList(this DataTable table)
        {
            var salaries = new List<Salary>();

            foreach (DataRow row in table.Rows)
            {
                var salary = new Salary()
                {
                    SalaryId = Convert.ToInt32(row["SalaryCode"]),
                    EmployeeId = Convert.ToInt32(row["Employee"]),
                    Attendance = Convert.ToInt32(row["Attendance"]),
                    PeriodTime = Convert.ToString(row["PeriodTime"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    PayDate = Convert.ToDateTime(row["Paydate"])
                };

                salaries.Add(salary);
            }

            return salaries;
        }

        public static void SaveSalaryData(int EmployeeId, int Attendance, string PeriodTime, decimal Amount, DateTime PayDate)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = EmployeeId },
                new SqlParameter("@Attendance", SqlDbType.Int) { Value = Attendance },
                new SqlParameter("@PeriodTime", SqlDbType.NVarChar) { Value = PeriodTime },
                new SqlParameter("@Amount", SqlDbType.Decimal) { Value = Amount },
                new SqlParameter("@PayDate", SqlDbType.DateTime) { Value = PayDate }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.CreateSalary, parameters);

            MessageBox.Show("Salary Registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void UpdateData(int id, int EmployeeId, int Attendance, string PeriodTime, decimal Amount, DateTime PayDate)
        {
            var parameters = new[]
            {
                new SqlParameter("@SalaryCode", SqlDbType.Int) { Value = id },
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = EmployeeId },
                new SqlParameter("@Attendance", SqlDbType.Int) { Value = Attendance },
                new SqlParameter("@PeriodTime", SqlDbType.NVarChar) { Value = PeriodTime },
                new SqlParameter("@Amount", SqlDbType.Decimal) { Value = Amount },
                new SqlParameter("@PayDate", SqlDbType.DateTime) { Value = PayDate }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.UpdateSalary, parameters);

            MessageBox.Show("Record Updated Successfully!");
        }

        public static void DeleteData(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@SalaryCode", SqlDbType.Int) { Value = id }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.DeleteSalary, parameters);

            MessageBox.Show("Record Deleted Successfully!");
        }
    }
}
