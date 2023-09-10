using EmployeeManagement.BLL;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EmployeeManagement.Salary
{
    public partial class Salary_Form : Form
    {
        private List<BO.Employee> employees;

        public Salary_Form()
        {
            InitializeComponent();
            cmbName.DisplayMember = "Employee";
            cmbName.ValueMember = "EmployeeId";
        } 
        
        private void Salary_Form_Load(object sender, EventArgs e)
        {
            employees = DataBaseHelper.ExecuteStoredProcedure(StoredProcedures.GetEmployees, null).ToEmployeeList();
            employees.Insert(0, new BO.Employee {EmployeeId = 0, EmpName = "Select an Employee"});

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
                var selectedEmployee = (BO.Employee)cmbName.SelectedItem;
                int attendance = int.Parse(txtbDays.Text);
                DateTime periodTime = dtpPeriodTime.Value;
                decimal empSalary = decimal.Parse(txtEmpSalary.Text);

                if (attendance > 0 && attendance <= 31) 
                {
                    // Check if attended for 20 days or more to increase salary
                    if (attendance > 22)
                    {
                        decimal salaryOfDay = empSalary / 22;
                        empSalary += (attendance-22) * salaryOfDay * 1.1m;
                    }

                    // Calculate pay date as one month after the period time
                    DateTime payDate = periodTime.AddMonths(1);

                    SalaryBLL.VerifyData(selectedEmployee.EmployeeId, attendance, Convert.ToString(periodTime), empSalary, payDate);
                    BindData();
                }
                else
                {
                    MessageBox.Show("Employee can't attend work for more than 31 days in a month.", "Attendance Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Reset();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please enter valid values.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            { 
                var selectedEmployee = (BO.Employee)cmbName.SelectedItem;
                SalaryBLL.VerifyToUpdateData(ID, selectedEmployee.EmployeeId, int.Parse(txtbDays.Text), Convert.ToString(dtpPeriodTime.Value), decimal.Parse(txtEmpSalary.Text), dtpPeriodTime.Value.AddMonths(1));
                BindData();
                Reset();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please enter valid values.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SalaryBLL.VerifyToDelete(ID);
            BindData();
            Reset();
        }

        //Display employee's Salary
        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbName.SelectedItem is BO.Employee selectedEmployee)
            {
                txtEmpSalary.Text = selectedEmployee.EmpSalary.ToString("0.00");
            }
            else
            {
                txtEmpSalary.Text = ""; 
            }
        }

        public void Reset()
        {
            cmbName.Text = "Select an Employee";
            txtbDays.Text = "";
            dtpPeriodTime.Value = DateTime.Now;
            txtEmpSalary.Text = "";
        }

        public void BindData()
        {
            salaryBindingSource.DataSource = SalaryBLL.LoadData();
        }

        private int ID;
        private void dgvSalary_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dgvSalary.Rows[e.RowIndex].Cells[0].Value.ToString());

            BO.Employee employee = employees.FirstOrDefault(emp => emp.EmployeeId.ToString() == dgvSalary.Rows[e.RowIndex].Cells[1].Value.ToString());
            cmbName.Text = employee.EmpName.ToString();

            txtbDays.Text = dgvSalary.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtpPeriodTime.Value = Convert.ToDateTime(dgvSalary.Rows[e.RowIndex].Cells[3].Value.ToString());

            decimal salary;
            if (decimal.TryParse(dgvSalary.Rows[e.RowIndex].Cells[4].Value.ToString(), out salary))
            {
                txtEmpSalary.Text = salary.ToString();
            }
            else
            {
                txtEmpSalary.Text = "Invalid Salary";
            }

            DateTime pay = dtpPeriodTime.Value.AddMonths(1);
            pay = Convert.ToDateTime(dgvSalary.Rows[e.RowIndex].Cells[5].Value.ToString());
        }

        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbLang.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    employees[0].EmpName = "Select an Employee";
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sq-AL");
                    employees[0].EmpName = "Zgjidhni nje Punonjes";
                    break;
            }

            this.Controls.Clear();
            InitializeComponent();
            cmbName.DataSource = employees;
            BindData();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            HomePage frm = new HomePage();
            frm.Show();
            this.Hide();
        }
    }
}
