﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KyotoSalesManagementSystem.DBGateway;
using KyotoSalesManagementSystem.UI;

namespace KyotoSalesManagementSystem.LoginUI
{
    public partial class frmLogin : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public static int uId;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void oKButton_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                SqlConnection myConnection = default(SqlConnection);
                myConnection = new SqlConnection(cs.DBConn);

                SqlCommand myCommand = default(SqlCommand);

                myCommand = new SqlCommand("SELECT Username,password FROM Registration WHERE Username = @username AND password = @UserPassword", myConnection);
                SqlParameter uName = new SqlParameter("@username", SqlDbType.VarChar);
                SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                uName.Value = txtUserName.Text;
                uPassword.Value = txtPassword.Text;
                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);

                myCommand.Connection.Open();

                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                if (myReader.Read() == true)
                {
                    int i;
                    ProgressBar1.Visible = true;
                    ProgressBar1.Maximum = 5000;
                    //ProgressBar1.Maximum = 5;
                    ProgressBar1.Minimum = 0;
                    ProgressBar1.Value = 4;
                    ProgressBar1.Step = 1;

                    for (i = 0; i <= 5000; i++)
                    {
                        ProgressBar1.PerformStep();
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select usertype,UserId from Registration where Username='" + txtUserName.Text + "' and Password='" + txtPassword.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtUserType.Text = (rdr.GetString(0));
                        uId = (rdr.GetInt32(1));
                    }
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }

                    if (txtUserType.Text.Trim() == "Admin")
                    {
                        
                        MainUI frm = new MainUI();
                        this.Hide();
                        frm.Show();
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                        frm.txtlblUser.Text = txtUserName.Text;
                    }
                    if (txtUserType.Text.Trim() == "User")
                    {
                        this.Hide();
                        // OnlyUIForHR frm = new OnlyUIForHR();
                        this.Visible = false;
                        //frm.ShowDialog();
                        this.Visible = true;
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                        // frm.lblUser2.Text = txtUserName.Text;
                    }

                }


                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();

                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            ChangePassword frm = new ChangePassword();
            frm.Show();
            frm.txtUserName.Text = "";
            frm.txtNewPassword.Text = "";
            frm.txtOldPassword.Text = "";
            frm.txtConfirmPassword.Text = "";
        }

        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            
          
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                oKButton_Click(this,new EventArgs());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
