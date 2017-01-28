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
    public partial class QuotoStatus : Form
    {

        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public QuotoStatus()
        {
            InitializeComponent();
        }

        private void QuotoStatus_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(Quotation.Dates),RTRIM(Quotation.QuotationId),RTRIM(SalesClient.ClientName),RTRIM(Quotation.QStatus),RTRIM(Quotation.Validity),RTRIM(Quotation.ValidityStatus),RTRIM(Quotation.NetPayable) from Quotation join RefNumForQuotation on Quotation.QuotationId=RefNumForQuotation.QuotationId  join SalesClient on RefNumForQuotation.SClientId=SalesClient.SClientId ", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotoSUI frm = new QuotoSUI();
            frm.Show();
        }

        private void QuotoStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            QuotoSUI frm = new QuotoSUI();
            frm.Show();
        }
    }
}
