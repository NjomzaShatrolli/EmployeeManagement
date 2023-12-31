﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        } 

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (UserTxtb.Text == "" || PassTxt.Text == "")
            {
                MessageBox.Show("Please make sure to fill in all enteries", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            }
            else if (UserTxtb.Text == "Admin" && PassTxt.Text == "1234")
            {
                HomePage frm = new HomePage();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UserTxtb.Text = "";
                PassTxt.Text = "";

            }
        }

        private void lblReset_Click(object sender, EventArgs e)
        {
            UserTxtb.Text = "";
            PassTxt.Text = "";
        }

        private void PassTxt_TextChanged(object sender, EventArgs e)
        {
            PassTxt.PasswordChar = '*';
            PassTxt.MaxLength = 20;
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
        }

       
    }
}
