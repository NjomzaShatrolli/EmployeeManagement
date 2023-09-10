using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL;
using System.Windows.Forms;
using EmployeeManagement.BO;

namespace EmployeeManagement.BLL
{
    public static class DepartmentBLL
    {
        public static object LoadData()
        {
            var departments = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.GetDepartment, null).ToDepartmentList();
            return departments;
        }

        public static void VerifyData(string DepName)
        {
            if (DepName is null)
            {
                MessageBox.Show("Please fill all enteries");
                return;
            }

            DADepartment.SaveData(DepName);
        }

        public static void VerifyToUpdateData(int Id, string DepName)
        {
            if (Id != 0 && DepName != null)
            {
                DADepartment.UpdateData(Id, DepName);
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }

        }

        public static void VerifyToDelete(int Id)
        {
            if (Id != 0)
            {
                DADepartment.DeleteData(Id);
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
    }
}
