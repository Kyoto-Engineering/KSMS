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
    public partial class ProductRecordForLocalStore : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public ProductRecordForLocalStore()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(MasterStocks.Sl),RTRIM(MasterStocks.ImportOrderNo),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProductRecordForLocalStore_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Dispose();
            Quotation frm=new Quotation();
            frm.Show();
            frm.txtProductName.Text = dr.Cells[2].Value.ToString();
            frm.txtAvailableQuantity.Text = dr.Cells[4].Value.ToString();
            frm.txtUnitPrice.Text = dr.Cells[5].Value.ToString();
            frm.labelm.Text = labelg.Text;


        }
    }
}
