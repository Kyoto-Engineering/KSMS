using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KyotoSalesManagementSystem.DBGateway;

namespace KyotoSalesManagementSystem.LoginUI
{
    public partial class UserDataGrid : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        ConnectionString cs=new ConnectionString();
        public UserDataGrid()
        {
            InitializeComponent();
        }


        private void MyTestGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            sda = new SqlDataAdapter("Select  pp.UserName,pp.UserType,pp.Password,pp.Name,pp.Email,pp.Designation,pp.Department,pp.ContactNo,pp.ImageSignature from Registration as pp order by pp.UserId desc", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 90;
            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 180; 
            
            con.Close();
        }

        private void UserDataGrid_Load(object sender, EventArgs e)
        {
            MyTestGrid();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Dispose();
                frmRegistration frm = new frmRegistration();
                frm.Show();
                frm.txtUserName.Text = dr.Cells[0].Value.ToString();
                frm.cmbUserType.Text = dr.Cells[1].Value.ToString();
                frm.txtPassword.Text = dr.Cells[2].Value.ToString();
                frm.txtName.Text = dr.Cells[3].Value.ToString();
                frm.txtEmail_Address.Text = dr.Cells[4].Value.ToString();
                frm.designationTextBox.Text = dr.Cells[5].Value.ToString();
                frm.departmentTextBox.Text = dr.Cells[6].Value.ToString();
                frm.txtContact_no.Text = dr.Cells[7].Value.ToString();
                byte[] data = (byte[])dr.Cells[8].Value;
                MemoryStream ms = new MemoryStream(data);
                frm.txtPictureBox.Image = Image.FromStream(ms);
                frm.labelk.Text = frm.labelg.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
