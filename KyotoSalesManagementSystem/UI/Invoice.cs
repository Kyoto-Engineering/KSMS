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
    public partial class Invoice : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        private ConnectionString cs = new ConnectionString();
        public int refId, quotationId, sclientId, sQN, invoiceId, user_id;
        public string referenceNo;
       
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            QuotationIdLoad();
        }

        public void SelectSclientId()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select SClientId,RefId,QuotationId from RefNumForQuotation WHERE ReferenceNo= '" + cmbQuotation.Text + "'";

            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sclientId = rdr.GetInt32(0);
                refId = rdr.GetInt32(1);
                quotationId = rdr.GetInt32(2);
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


        private void PersonInfo()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText =
                    "select ClientName,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CPS,CPSCode,CDistrict,CContactNo,ContactPersonName,CellNumber from SalesClient2 where SClientId= '" +
                    sclientId + "'";
                // "select TotalPrice,QVat,QAIT,Discount,NetPayable from Quotation where QuotationId='" + quotationId + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    txtInvoiceParty.Text = rdr["ClientName"].ToString();
                    txtPayerAddress.Text = rdr["CFlatNo"] + ", " + rdr["CHouseNo"] + ", " + rdr["CRoadNo"] + ", " +
                                           rdr["CBlock"] + ", " + rdr["CArea"] + ", " + rdr["CPS"] + ", " +
                                           rdr["CPSCode"] + ", " + rdr["CDistrict"];
                    txtLandPhone.Text = rdr["CContactNo"].ToString();
                    txtRP.Text = rdr["ContactPersonName"].ToString();
                    txtCellPhone.Text = rdr["CellNumber"].ToString();
                    //txtTotalPrice.Text = rdr["TotalPrice"]
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
        private void cmbQuotation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSclientId();
            PersonInfo();

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText ="select TotalPrice,QVat,QAIT,Discount,NetPayable from Quotation where QuotationId='" + quotationId + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    txtTotalPrice.Text = rdr["TotalPrice"].ToString();
                    txtVATPercent.Text = rdr["QVat"].ToString();
                    txtAITPercent.Text = rdr["QAIT"].ToString();
                    txtDiscountPercent.Text = rdr["Discount"].ToString();
                    txtNetPayable.Text=rdr["NetPayable"].ToString();
                    
                    if (txtVATPercent.Text == "")
                    {
                        txtVATAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtVATAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtVATPercent.Text)) / 100).ToString();
                    }


                    if (txtAITPercent.Text == "")
                    {
                        txtAITAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtAITAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtAITPercent.Text)) / 100).ToString();
                    }

                    if (txtDiscountPercent.Text == "")
                    {
                        txtDiscountAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtDiscountAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtDiscountPercent.Text)) / 100).ToString();
                    }
                    
                  
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
        private void QuotationIdLoad()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query =
                    "SELECT RefNumForQuotation.ReferenceNo FROM RefNumForQuotation INNER JOIN Quotation ON RefNumForQuotation.QuotationId = Quotation.QuotationId where Quotation.QStatus='Accepted' except SELECT RefNumForQuotation.ReferenceNo FROM RefNumForQuotation INNER JOIN Quotation ON RefNumForQuotation.QuotationId = Quotation.QuotationId INNER JOIN Invoice ON RefNumForQuotation.QuotationId = Invoice.QuotationId";

                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbQuotation.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVATPercent_TextChanged(object sender, EventArgs e)
        {
            decimal val1 = 0;
            decimal val2 = 0;
            decimal.TryParse(txtVATPercent.Text, out val1);
            decimal.TryParse(txtTotalPrice.Text, out val2);
            decimal V = (val1 * val2) / 100;
            txtVATAmount.Text = V.ToString();
            txtNetPayable.Text = ((Convert.ToDecimal(txtTotalPrice.Text) + Convert.ToDecimal(txtVATAmount.Text) + Convert.ToDecimal(txtAITAmount.Text)) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString();
        }

        private void txtAITPercent_TextChanged(object sender, EventArgs e)
        {
            decimal val3 = 0;
            decimal val4 = 0;
            decimal.TryParse(txtAITPercent.Text, out val3);
            decimal.TryParse(txtTotalPrice.Text, out val4);
            decimal A = (val3 * val4) / 100;
            txtAITAmount.Text = A.ToString();
            txtNetPayable.Text = ((Convert.ToDecimal(txtTotalPrice.Text) + Convert.ToDecimal(txtVATAmount.Text) + Convert.ToDecimal(txtAITAmount.Text)) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString();
        }


    }
}
