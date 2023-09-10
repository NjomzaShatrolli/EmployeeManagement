using EmployeeManagement.BLL;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Helpers;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Employee
{
    public partial class Employee_Form : Form
    {
        private List<BO.Department> departments;

        public Employee_Form()
        {
            InitializeComponent(); 
            cmbDepartment.DisplayMember = "DepaName";
            cmbDepartment.ValueMember = "DepartmentId";
        }

        private void Employee_Form_Load(object sender, EventArgs e)
        {
            departments = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.GetDepartment, null).ToDepartmentList();
            departments.Insert(0, new BO.Department { DepartmentId = 0, DepName = "Select a department" });

            if (Thread.CurrentThread.CurrentUICulture.Name == "sq-AL")
            {
                cmbLang.SelectedItem = cmbLang.Items[1];
            }

            cmbLang_SelectedIndexChanged(null, null);
            BindData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(Regex.IsMatch(txtEmployeeName.Text, @"\d"))
                {
                    MessageBox.Show("Name cannot contain number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(dtpDOB.Value.Date > DateTime.Now.AddYears(-18))
                {
                    MessageBox.Show("Employee cannot be younger than 18!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(dtpDOB.Value.Date < DateTime.Now.AddYears(-65))
                {
                    MessageBox.Show("Employee cannot be older than 65!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedDepartment = (BO.Department)cmbDepartment.SelectedItem;
                EmployeeBLL.VerifyData(txtEmployeeName.Text, cmbGender.Text, selectedDepartment.DepartmentId, dtpDOB.Value, dtpJoinedDate.Value, decimal.Parse(txtbSalary.Text));
                BindData();

            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid salary format. Please enter a valid number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Reset();
        }


        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedDepartment = (BO.Department)cmbDepartment.SelectedItem;
                EmployeeBLL.VerifyToUpdateData(ID, txtEmployeeName.Text, cmbGender.Text, selectedDepartment.DepartmentId, dtpDOB.Value, dtpJoinedDate.Value, decimal.Parse(txtbSalary.Text));
                BindData();
                Reset();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid salary format. Please enter a valid value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            EmployeeBLL.VerifyToDelete(ID);
            BindData();
            Reset();
        }

        public void Reset()
        {
            txtEmployeeName.Text = "";
            cmbGender.Text = null;
            dtpDOB.Value = DateTime.Now;
            dtpJoinedDate.Value = DateTime.Now;
            cmbDepartment.Text = "Select a department";
            txtbSalary.Text = "";
        }

        private int ID;
        private void dgvEmployee_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dgvEmployee.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtEmployeeName.Text = dgvEmployee.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbGender.Text = dgvEmployee.Rows[e.RowIndex].Cells[2].Value.ToString();

            //checks if a department on the dgv matches one in departments list, if it does we will display it on the combobox when a row is clicked.
            BO.Department department = departments.FirstOrDefault(dpt=> dpt.DepartmentId.ToString() == dgvEmployee.Rows[e.RowIndex].Cells[3].Value.ToString());
            cmbDepartment.Text = department.DepName.ToString();

            dtpDOB.Value = Convert.ToDateTime(dgvEmployee.Rows[e.RowIndex].Cells[4].Value.ToString());
            dtpJoinedDate.Value = Convert.ToDateTime(dgvEmployee.Rows[e.RowIndex].Cells[5].Value.ToString());
            
            decimal salary;
            if (decimal.TryParse(dgvEmployee.Rows[e.RowIndex].Cells[6].Value.ToString(), out salary))
            {
                txtbSalary.Text = salary.ToString();
            }
            else
            {
                txtbSalary.Text = "Invalid Salary";
            }
        }

        public void BindData()
        {
            employeeBindingSource.DataSource = EmployeeBLL.LoadData();
        }

        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbLang.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    //it updates the text of the first item in the departments collection
                    departments[0].DepName = "Select a Department";
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sq-AL");
                    departments[0].DepName = "Zgjidhni nje Departament";
                    break;
            }

            this.Controls.Clear();
            InitializeComponent();
            //ensures that the department names are displayed in the currently selected language
            cmbDepartment.DataSource = departments;
            BindData();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            HomePage frm = new HomePage();
            frm.Show();
            this.Close();
        }
    }
}
