using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL;
using System;
using System.Windows.Forms;

namespace EmployeeManagement.BLL
{
    public static class SalaryBLL
    {
        public static object LoadData()
        {
            var salaries = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.GetSalaries, null).ToSalaryList();
            return salaries;
        }

        public static void VerifyData(int EmployeeId, int Attendance, string PeriodTime, decimal Amount, DateTime PayDate)
        {
            if (EmployeeId == 0 || Attendance == 0 || PeriodTime == null || Amount == 0 || PayDate == null)
            {
                MessageBox.Show("Please Fill All Enteries!");
                return;
            }

            DASalary.SaveSalaryData(EmployeeId, Attendance, PeriodTime, Amount, PayDate);
        }

        public static void VerifyToUpdateData(int SalaryId, int EmployeeId, int Attendance, string PeriodTime, decimal Amount, DateTime PayDate)
        {
            if (SalaryId != 0 && EmployeeId != 0 && Attendance != 0 && PeriodTime != null && Amount != 0 && PayDate != null)
            {
                DASalary.UpdateData(SalaryId, EmployeeId, Attendance, PeriodTime, Amount, PayDate);
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }

        }

        public static void VerifyToDelete(int SalaryId)
        {
            if (SalaryId != 0)
            {
                DASalary.DeleteData(SalaryId);
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
    }
}
