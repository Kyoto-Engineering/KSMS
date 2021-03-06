﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KyotoSalesManagementSystem.DBGateway;

namespace KyotoSalesManagementSystem.LoginUI
{
    public partial class UserUpdate : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public UserUpdate()
        {
            InitializeComponent();
        }

    
        private void Reset()
        {
            txtUserName.Text = "";
            cmbUserType.SelectedIndex = -1;

            txtContact_no.Text = "";
            txtName.Text = "";
            designationTextBox.Text = "";
            departmentTextBox.Text = "";
            txtEmail_Address.Text = "";
            txtPictureBox.Image = null;
            //deleteButton.Enabled = false;
            updateButton.Enabled = true;
            txtUserName.Focus();
        }
        private void newButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (cmbUserType.Text == "")
            {
                MessageBox.Show("Please select user type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserType.Focus();
                return;
            }
          
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (txtContact_no.Text == "")
            {
                MessageBox.Show("Please enter contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContact_no.Focus();
                return;
            }
            if (txtEmail_Address.Text == "")
            {
                MessageBox.Show("Please enter email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail_Address.Focus();
                return;
            }
            //if (txtPictureBox.Text == "")
            //{
            //    MessageBox.Show("Please upload your Images.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPictureBox.Focus();
            //    return;
            //}
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "Update Registration set UserType='" + cmbUserType.Text + "',Name='" + txtName.Text + "',Email='" + txtEmail_Address.Text + "',Designation='" + designationTextBox.Text + "',Department='" + departmentTextBox.Text + "',ContactNo='" + txtContact_no.Text + "',ImageSignature=@d8 where UserName='" + txtUserName.Text + "'";
                cmd = new SqlCommand(cb);
                if (txtPictureBox.Image != null)
                {
                    MemoryStream ms = new MemoryStream();
                    Bitmap bmpImage = new Bitmap(txtPictureBox.Image);
                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] data = ms.GetBuffer();
                    SqlParameter p = new SqlParameter("@d8", SqlDbType.VarBinary);
                    p.Value = data;
                    cmd.Parameters.Add(p);
                }
                else
                {
                    cmd.Parameters.Add("@d8", SqlDbType.VarBinary, -1);
                    cmd.Parameters["@d8"].Value = DBNull.Value;
                }
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                MessageBox.Show("Successfully updated", "User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateButton.Enabled = false;
                Reset();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            try
            {
                var _with1 = openFileDialog1;

                _with1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                _with1.FilterIndex = 4;

                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtPictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = txtUserName.Text.TrimEnd();
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT UserType,Password,Name,Email,Designation,Department,ContactNo FROM registration WHERE username = '" + txtUserName.Text.Trim() + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cmbUserType.Text = (rdr.GetString(0).Trim());
                    txtName.Text = (rdr.GetString(2).Trim());
                    txtEmail_Address.Text = (rdr.GetString(3).Trim());
                    designationTextBox.Text = (rdr.GetString(4).Trim());
                    departmentTextBox.Text = (rdr.GetString(5).Trim());
                    txtContact_no.Text = (rdr.GetString(6).Trim());
                   // txtPictureBox.Text = (rdr.GetString(7).Trim());



                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            UserDataGrid frm=new UserDataGrid();
                      frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label9.Visible = !this.label9.Visible;
        }

        private void UserUpdate_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }
        private void txtEmail_Address_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail_Address.Text))
            {


                string emailId = txtEmail_Address.Text.Trim();
                Regex mRegxExpression;
                mRegxExpression =
                    new Regex(
                        @"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                if (!mRegxExpression.IsMatch(emailId))
                {

                    MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtEmail_Address.Clear();
                    txtEmail_Address.Focus();

                }
            }
        }
    }
}
