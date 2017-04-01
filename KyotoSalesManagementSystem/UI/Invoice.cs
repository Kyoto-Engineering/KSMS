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
        public decimal aitPercent = 0, aitAmount = 0, netPayable = 0, discount = 0, discountPercent = 0, myNetPayable = 0, myVAT = 0, myAIT = 0, myDis = 0;
        public decimal vt = 0, ait = 0, dis = 0, t = 0;
        public Nullable<decimal> vatNull, aitNull, disNull;

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

                cmd.CommandText = "select TotalPrice,QVat,QAIT,Discount,NetPayable from Quotation where QuotationId='" + quotationId + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    txtTotalPrice.Text = rdr["TotalPrice"].ToString();
                    if (!rdr.IsDBNull(1))
                    {
                        txtVATPercent.TextChanged -= txtVATPercent_TextChanged;
                        txtVATPercent.Text = rdr["QVat"].ToString();
                        txtVATPercent.TextChanged += txtVATPercent_TextChanged;
                    }
                    else
                    {
                        txtVATPercent.Text = null;
                    }
                    if (!rdr.IsDBNull(2))
                    {
                        txtAITPercent.TextChanged -= txtAITPercent_TextChanged;
                        txtAITPercent.Text = rdr["QAIT"].ToString();
                        txtAITPercent.TextChanged += txtAITPercent_TextChanged;
                    }
                    else
                    {
                        txtAITPercent.Text = null;
                    }
                    //if (!rdr.IsDBNull(3))
                    //{
                    //    txtDiscountPercent.TextChanged -= txtAdditionalDiscount_TextChanged;
                    //    txtDiscountPercent.Text = rdr["Discount"].ToString();
                    //    txtDiscountPercent.TextChanged += txtAdditionalDiscount_TextChanged;
                    //}
                    //else
                    //{
                    //    txtDiscountPercent.Text = null;
                    //}
                    txtDiscountPercent.Text = rdr["Discount"].ToString();
                    txtNetPayable.Text = rdr["NetPayable"].ToString();

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

                    //if (txtAdditionalDiscount.Text == "")
                    //{
                    //    txtAdditionalDiscount.Text = 0.ToString();

                    //}

                    //if (txtAdvancePayment.Text == "")
                    //{
                    //    txtAdvancePayment.Text = 0.ToString();

                    //}
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
            txtNetPayable.Text = ((Convert.ToDecimal(txtTotalPrice.Text) + Convert.ToDecimal(txtVATAmount.Text) + Convert.ToDecimal(txtAITAmount.Text)) - (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(txtAdditionalDiscount.Text) + Convert.ToDecimal(txtAdvancePayment.Text))).ToString();
        }

        private void txtAITPercent_TextChanged(object sender, EventArgs e)
        {
            decimal val3 = 0;
            decimal val4 = 0;
            decimal.TryParse(txtAITPercent.Text, out val3);
            decimal.TryParse(txtTotalPrice.Text, out val4);
            decimal A = (val3 * val4) / 100;
            txtAITAmount.Text = A.ToString();
            txtNetPayable.Text = ((Convert.ToDecimal(txtTotalPrice.Text) + Convert.ToDecimal(txtVATAmount.Text) + Convert.ToDecimal(txtAITAmount.Text)) - (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(txtAdditionalDiscount.Text) + Convert.ToDecimal(txtAdvancePayment.Text))).ToString();
        }

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        private void txtVATPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtVATPercent.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txtAITPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtAITPercent.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txtAdditionalDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtAdditionalDiscount.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txtAdvancePayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !Decimal.TryParse(txtAdvancePayment.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        public void GetNETPayable()
        {
            t = decimal.Parse(txtTotalPrice.Text);

            if (!string.IsNullOrWhiteSpace(txtVATPercent.Text))
            {
                vatNull = vt = decimal.Parse(txtVATPercent.Text);

            }
            else
            {
                vt = 0;
                txtVATAmount.Clear();
                vatNull = null;
            }

            if (!string.IsNullOrWhiteSpace(txtAITPercent.Text))
            {
                aitNull = ait = decimal.Parse(txtAITPercent.Text);
            }
            else
            {
                ait = 0;
                aitNull = null;
                txtAITAmount.Clear();
            }
            if (!string.IsNullOrWhiteSpace(txtDiscountPercent.Text))
            {
                disNull = dis = decimal.Parse(txtDiscountPercent.Text);
            }
            else
            {
                dis = 0;
                disNull = null;
                txtDiscountAmount.Clear();
            }
            myVAT = (t * vt) / 100;
            myAIT = (t * ait) / 100;
            myDis = (t * dis) / 100;

            myNetPayable = t + myVAT + myAIT - myDis;
            txtVATAmount.Text = myVAT.ToString();
            txtAITAmount.Text = myAIT.ToString();
            txtDiscountAmount.Text = myDis.ToString();
            txtNetPayable.Text = myNetPayable.ToString();
        }

        public void GetNetPayableWithAdditionalDiscount()
        {
            GetNETPayable();
            txtNetPayable.Text = (Convert.ToDecimal(txtNetPayable.Text) - Convert.ToDecimal(txtAdditionalDiscount.Text)).ToString();
               
        }

        private void txtAdditionalDiscount_TextChanged(object sender, EventArgs e)
        {
           
            decimal val5 = 0;
            decimal val6 = 0;
            decimal.TryParse(txtAdditionalDiscount.Text, out val5);
            decimal.TryParse(txtNetPayable.Text, out val6);
            //decimal AD = val5;
            //txtAdditionalDiscount.Text = AD.ToString();
            ////txtNetPayable.Text = (Convert.ToDecimal(txtTotalPrice.Text) - Convert.ToDecimal(txtAdditionalDiscount.Text)).ToString();
            //txtNetPayable.Text = (Convert.ToDecimal(txtNetPayable.Text) - Convert.ToDecimal(txtAdditionalDiscount.Text)).ToString();

            if (val5 <= 0)
            {
                GetNETPayable();
                //txtNetPayable.Text = val6.ToString();
                
            }
            else
            {
               // GetNETPayable();
               //txtNetPayable.Text = (Convert.ToDecimal(txtNetPayable.Text) - Convert.ToDecimal(txtAdditionalDiscount.Text)).ToString();
                GetNetPayableWithAdditionalDiscount();
            }
        }

        private void txtAdvancePayment_TextChanged(object sender, EventArgs e)
        {
            decimal val7 = 0;
            decimal val8 = 0;
            decimal.TryParse(txtAdvancePayment.Text, out val7);
            decimal.TryParse(txtNetPayable.Text, out val8);

            if (val7 <= 0)
            {
                if (string.IsNullOrWhiteSpace(txtAdditionalDiscount.Text))
                {
                    GetNETPayable();
                }
                else
                {
                    GetNetPayableWithAdditionalDiscount();

                }
                
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtAdditionalDiscount.Text))
                {
                    GetNETPayable();
                    txtNetPayable.Text = (Convert.ToDecimal(txtNetPayable.Text) - Convert.ToDecimal(txtAdvancePayment.Text)).ToString();
                }
                else
                {
                    GetNetPayableWithAdditionalDiscount();
                    txtNetPayable.Text = (Convert.ToDecimal(txtNetPayable.Text) - Convert.ToDecimal(txtAdvancePayment.Text)).ToString();
                }
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
