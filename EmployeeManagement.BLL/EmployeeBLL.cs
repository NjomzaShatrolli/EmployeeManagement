using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL;
using System;
using System.Windows.Forms;

namespace EmployeeManagement.BLL
{
    public static class EmployeeBLL
    {
        public static object LoadData()
        {
            var employees = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.GetEmployees, null).ToEmployeeList();
            return employees;
        }

        public static void VerifyData(string EmpName, string EmpGen, int EmpDepartment, DateTime EmpDOB, DateTime EmpJoinedDate, decimal EmpSalary)
        {
            if (EmpName == null || EmpGen == null || EmpDepartment == 0 || EmpDOB == null || EmpJoinedDate == null || EmpSalary == 0)
            {
                MessageBox.Show("Please Fill All Enteries!");
                return;
            }

            DAEmployee.SaveEmployeeData(EmpName, EmpGen,EmpDepartment, EmpDOB, EmpJoinedDate, EmpSalary);
        }

        public static void VerifyToUpdateData(int EmpId, string EmpName, string EmpGen, int EmpDepartment, DateTime EmpDOB, DateTime EmpJoinedDate, decimal EmpSalary)
        {
            if (EmpId != 0 && EmpName != null && EmpGen != null && EmpDepartment != 0 && EmpDOB != null && EmpJoinedDate != null && EmpSalary != 0)
            {
                DAEmployee.UpdateData(EmpId, EmpName, EmpGen,EmpDepartment, EmpDOB, EmpJoinedDate, EmpSalary);
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }

        }

        public static void VerifyToDelete(int EmpId)
        {
            if (EmpId != 0)
            {
                DAEmployee.DeleteData(EmpId);
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
    }
}
