using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KyotoSalesManagementSystem.DBGateway;

namespace KyotoSalesManagementSystem.UI
{
    public partial class SalesClientGrid22 : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public static int ftype;

        public SalesClientGrid22()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd =
                    new SqlCommand(
                        "SELECT SalesClient.SClientId, SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.Designation, ContactPersonDetails.CellNumber, SalesClient.EndUser FROM SalesClient Left JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId Left JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId   order by SalesClient.SClientId desc",
                        con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesClientGrid22_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            if (ftype == 1)
            {
            
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];

                this.Dispose();
                QuotationForOvercease frm = new QuotationForOvercease();
                frm.Show();
                frm.txtClientId.Text = dr.Cells[0].Value.ToString();
                frm.txtClientName.Text = dr.Cells[1].Value.ToString();
                frm.labelm.Text = labeln.Text;
                frm.BrandcomboBox.Enabled = true;
                frm.txtOProductId.Enabled = false;
                frm.txtOSProductName.Enabled = false;
                frm.groupBox3.Enabled = false;
                frm.groupBox2.Enabled = false;
                frm.groupBox7.Enabled = false;
                frm.dateTimePicker1.Focus();
                //frm.txtAttention.Focus();
                // this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            else if (ftype==3)
            {
                try
                {
                    DataGridViewRow dr = dataGridView1.SelectedRows[0];

                    this.Dispose();
                    Quotation frm = new Quotation();
                    frm.Show();
                    frm.txtClientId.Text = dr.Cells[0].Value.ToString();
                    frm.txtClientName.Text = dr.Cells[1].Value.ToString();
                    frm.labelm.Text = labeln.Text;
                    frm.BrandcomboBox.Enabled = true;
                    frm.txtProId.Enabled = false;
                    frm.txtSProductName.Enabled = false;
                    frm.groupBox3.Enabled = false;
                    frm.groupBox2.Enabled = false;
                    frm.groupBox7.Enabled = false;
                    frm.dateTimePicker1.Focus();
                    //frm.txtAttention.Focus();
                    // this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else if (ftype == 2)
            {
                try
                {
                    DataGridViewRow dr = dataGridView1.SelectedRows[0];

                    this.Dispose();
                    QuotationForCustom frm = new QuotationForCustom();
                    frm.Show();
                    frm.txtClientId.Text = dr.Cells[0].Value.ToString();
                    frm.txtClientName.Text = dr.Cells[1].Value.ToString();
                    frm.BrandcomboBox.Enabled = true;
                    frm.groupBox3.Enabled = false;
                    frm.groupBox2.Enabled = false;
                    frm.groupBox7.Enabled = false;
                    frm.labelm.Text = labeln.Text;
                    frm.dateTimePicker1.Focus();
                    //frm.txtAttention.Focus();
                    // this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (ftype == 4)
            {
                try
                {
                    DataGridViewRow dr = dataGridView1.SelectedRows[0];

                    this.Dispose();
                    QuotationMMix frm = new QuotationMMix();
                    frm.Show();
                    
                    frm.txtClientId.Text = dr.Cells[0].Value.ToString();
                    frm.txtClientName.Text = dr.Cells[1].Value.ToString();
                    frm.labelm.Text = labeln.Text;
                    frm.dateTimePicker1.Focus();
                    //frm.txtAttention.Focus();
                    // this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
    }
}
}
