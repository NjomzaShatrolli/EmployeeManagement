using EmployeeManagement.BLL;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EmployeeManagement.Department
{
    public partial class Department_Form : Form
    {
        public Department_Form()
        {
            InitializeComponent();
        } 
        
        private void Department_Form_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DepartmentBLL.VerifyData(txtbDepartment.Text);
            BindData();
            txtbDepartment.Text = "";
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            DepartmentBLL.VerifyToUpdateData(ID, txtbDepartment.Text);
            BindData();
            txtbDepartment.Text = "";
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DepartmentBLL.VerifyToDelete(ID);
            BindData();    
            txtbDepartment.Text = "";
        }

        private int ID;
        private void dgvDepartment_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dgvDepartment.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtbDepartment.Text = dgvDepartment.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        public void BindData()
        {
            departmentBindingSource.DataSource = DepartmentBLL.LoadData();
        }

        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
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
