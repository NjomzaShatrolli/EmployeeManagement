using EmployeeManagement.BO;
using EmployeeManagement.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace EmployeeManagement.DAL
{
    public static class DADepartment
    {
        public static List<Department> ToDepartmentList(this DataTable table)
        {
            var departments = new List<Department>();

            foreach (DataRow row in table.Rows)
            {
                var department = new Department
                {
                    DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                    DepName = Convert.ToString(row["DepName"]),
                };

                departments.Add(department);
            }

            return departments;
        }

        public static void SaveData(string DepName)
        {
            var parameters = new[]
            {
                    new SqlParameter("@DepName", SqlDbType.NVarChar) { Value = DepName }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.CreateDepartment, parameters);

            MessageBox.Show("Department Registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        public static void UpdateData(int id, string DepName)
        {
            var parameters = new[]
            {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = id },
                    new SqlParameter("@DepName", SqlDbType.NVarChar) { Value = DepName },
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.UpdateDepartment, parameters);

            MessageBox.Show("Record Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void DeleteData(int id)
        {
            var parameters = new[]
            {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            var result = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.DeleteDepartment, parameters);

            MessageBox.Show("Record Deleted Successfully!");
        }

    }
}
