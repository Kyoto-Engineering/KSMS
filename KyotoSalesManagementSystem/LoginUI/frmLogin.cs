using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        public static string EMail,NAme,DEsignation,COntact;
        public string readyPassword, dbUserName, dbPassword;
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
                string clearText = txtPassword.Text.Trim();
                string password = clearText;
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
                string readyPassword1 = Convert.ToBase64String(inArray);
                readyPassword = readyPassword1;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "SELECT UserName,Password FROM Registration WHERE UserName = '" + txtUserName.Text + "' AND Password = '" + readyPassword + "'";
                cmd = new SqlCommand(qry, con);
                rdr= cmd.ExecuteReader();
                if (rdr.Read() == true)
                {
                    dbUserName = (rdr.GetString(0));
                    dbPassword = (rdr.GetString(1));


                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select UserType,UserId,Email,Name,Designation,ContactNo from Registration where UserName='" + txtUserName.Text + "' and Password='" + readyPassword + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtUserType.Text = (rdr.GetString(0));
                        uId = (rdr.GetInt32(1));
                        EMail = (rdr.GetString(2));
                        NAme = (rdr.GetString(3));
                        DEsignation = (rdr.GetString(4));
                        COntact = (rdr.GetString(5));
                    }
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }

                    if (dbUserName == txtUserName.Text && dbPassword == readyPassword && txtUserType.Text.Trim() == "Admin")
                    {
                        MainUI frm = new MainUI();
                        this.Hide();
                        frm.Show();
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                        frm.txtlblUser.Text = txtUserName.Text;

                    }
                    if (dbUserName == txtUserName.Text && dbPassword == readyPassword && txtUserType.Text.Trim() == "User")
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
