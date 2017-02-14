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
    public partial class QuotationSChange : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public QuotationSChange()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Accepted")
            {
                comboBox2.Text = "Valid";
                textBox2.Visible = false;
                textBox2.Enabled = false;
                richTextBox1.Enabled = false;
                richTextBox1.Visible = false;
                label5.Visible = false;
                label4.Visible = false;
            }
            else if (comboBox1.Text == "Not Accepted")
            {
                comboBox2.Text = "Invalid";
                textBox2.Visible = true;
                textBox2.Enabled = true;
                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
            }
            else if (comboBox1.Text == "Review Wanted")
            {
                comboBox2.Text = "Invalid";
                textBox2.Visible = true;
                textBox2.Enabled = true;
                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
            }
            else if (comboBox1.Text == "Time Increased")
            {
                comboBox2.Text = "Valid";
                textBox2.Visible = true;
                textBox2.Enabled = true;
                richTextBox1.Enabled = true;
                richTextBox1.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
            }
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "Update Quotation  set ValidityStatus='" + comboBox2.Text + "', QStatus='" + comboBox1.Text + "' where QuotationId='" + int.Parse(textBox1.Text) + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                MessageBox.Show("Successfully updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                PendingQuoto frm = new PendingQuoto();
                frm.Show(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void QuotationSChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            PendingQuoto frm = new PendingQuoto();
            frm.Show(); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch)&&e.KeyChar!='.'||!Decimal.TryParse(textBox2.Text+ch,out x))
            {
                e.Handled = true;
            }
        }
    }
}
