using EmployeeManagement.Department;
using EmployeeManagement.Employee;
using EmployeeManagement.Salary;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void lblDepartment_Click(object sender, EventArgs e)
        {
            Department_Form depForm = new Department_Form();
            depForm.Show();
            this.Hide();
        }

        private void lblEmployee_Click(object sender, EventArgs e)
        {
            Employee_Form employeeForm = new Employee_Form();
            employeeForm.Show();
            this.Hide();
        }
    
        private void lblSalary_Click(object sender, EventArgs e)
        {
            Salary_Form salaryForm = new Salary_Form();
            salaryForm.Show();
            this.Hide();
        }

        private void lblLogOut_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void cmbLang_SelectedIndexChanged_1(object sender, EventArgs e)
        {  
            switch (cmbLang.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sq-AL");
                    break;
            }

            this.Controls.Clear();
            InitializeComponent();
        }
    }
}
