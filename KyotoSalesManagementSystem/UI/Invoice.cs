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
using KyotoSalesManagementSystem.LoginUI;

namespace KyotoSalesManagementSystem.UI
{
    public partial class Invoice : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        private ConnectionString cs = new ConnectionString();
        public int refId, quotationId, sclientId, sQN, invoiceId, user_id,BrandId;
        public string referenceNo;
        public decimal aitPercent = 0, aitAmount = 0, netPayable = 0, discount = 0, discountPercent = 0, myNetPayable = 0, myVAT = 0, myAIT = 0, myDis = 0;
        public decimal vt = 0, ait = 0, dis = 0, t = 0;
        public Nullable<decimal> vatNull, aitNull, disNull;
        private delegate void ChangeFocusDelegate(Control ctl);
        SqlTransaction trnas;

        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            QuotationIdLoad();
        }

        private void changeFocus(Control ctl)
        {
            ctl.Focus();
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

                cmd.CommandText = "select TotalPrice,QVat,QAIT,Discount,NetPayable,BrandId FROM  Quotation  where QuotationId='" + quotationId + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    BrandId =Convert.ToInt32(rdr["BrandId"]);

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
                    txtDiscountPercent.Text = rdr["Discount"].ToString();
                    txtNetPayable.Text = rdr["NetPayable"].ToString();

                    if (string.IsNullOrWhiteSpace(txtVATPercent.Text))
                    {
                        txtVATAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtVATAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtVATPercent.Text)) / 100).ToString();
                    }


                    if (string.IsNullOrWhiteSpace(txtAITPercent.Text))
                    {
                        txtAITAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtAITAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtAITPercent.Text)) / 100).ToString();
                    }

                    if (string.IsNullOrWhiteSpace(txtDiscountPercent.Text))
                    {
                        txtDiscountAmount.Text = 0.ToString();
                    }
                    else
                    {
                        txtDiscountAmount.Text = ((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtDiscountPercent.Text)) / 100).ToString();
                    }

                    //if (string.IsNullOrWhiteSpace(txtAdditionalDiscount.Text))
                    //{
                    //    txtAdditionalDiscount.Text = 0.ToString();

                    //}

                    //if (string.IsNullOrWhiteSpace(txtAdvancePayment.Text))
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
                    "SELECT RefNumForQuotation.ReferenceNo FROM RefNumForQuotation INNER JOIN Quotation ON RefNumForQuotation.QuotationId = Quotation.QuotationId where Quotation.QStatus='Accepted' except SELECT RefNumForQuotation.ReferenceNo FROM RefNumForQuotation INNER JOIN Quotation ON RefNumForQuotation.QuotationId = Quotation.QuotationId INNER JOIN RefNumForInvoice ON RefNumForQuotation.QuotationId = RefNumForInvoice.QuotationId";

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
            GetNETPayable();
         }

        private void txtAITPercent_TextChanged(object sender, EventArgs e)
        {
            
            decimal val3 = 0;
            decimal val4 = 0;
            decimal.TryParse(txtAITPercent.Text, out val3);
            decimal.TryParse(txtTotalPrice.Text, out val4);
            GetNETPayable();
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
            
            if (val5 <= 0)
            {
                GetNETPayable();
                
                
            }
            else
            {
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

        private void SaveInvoice()
        {
           
                con = new SqlConnection(cs.DBConn);
                
                con.Open();
                trnas = con.BeginTransaction();
            try
            {
                String query = "insert into Invoice(InvoiceDate, DueDate, InvVAT, InvAIT, AdditionalDiscount, AdvancePayment, NETPayable, PromisedDate,UserId,EntryDate) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(query, con,trnas);
                cmd.Parameters.AddWithValue("@d1", dtpInvoiceDate.Value);
                cmd.Parameters.AddWithValue("@d2", dtpDueDate.Value);
                cmd.Parameters.AddWithValue("@d3", string.IsNullOrWhiteSpace(txtVATPercent.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtVATPercent.Text));
                cmd.Parameters.AddWithValue("@d4", string.IsNullOrWhiteSpace(txtAITPercent.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtAITPercent.Text));
                cmd.Parameters.AddWithValue("@d5", string.IsNullOrWhiteSpace(txtAdditionalDiscount.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtAdditionalDiscount.Text));
                cmd.Parameters.AddWithValue("@d6", string.IsNullOrWhiteSpace(txtAdvancePayment.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtAdvancePayment.Text));
                cmd.Parameters.AddWithValue("@d7", Convert.ToDecimal(txtNetPayable.Text));
                cmd.Parameters.AddWithValue("@d8", dtpPromisedDate.Value);
                cmd.Parameters.AddWithValue("@d9", frmLogin.uId);
                cmd.Parameters.AddWithValue("@d10", DateTime.UtcNow.ToLocalTime());
                invoiceId = (int)cmd.ExecuteScalar();
               SqlConnection con2 = new SqlConnection(cs.DBConn);
                string qr2 = "SELECT SClientId,SQN FROM RefNumForQuotation WHERE ReferenceNo= '" + cmbQuotation.Text + "'";
                cmd = new SqlCommand(qr2, con2);
                con2.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    sclientId = (rdr.GetInt32(0));
                    con2.Close();

                }
                con2.Close();
                con2.Open();
                string qry = "SELECT        RefNumForInvoice.* FROM            RefNumForInvoice WHERE        SClientId =" +
                             sclientId;
                cmd = new SqlCommand(qry, con2);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    con2.Close();
                    con2.Open();
                    string qry2 = "SELECT        MAX(SIN) AS Expr1 FROM RefNumForInvoice  GROUP BY SClientId  HAVING   SClientId =" +
                                  sclientId;
                    cmd = new SqlCommand(qry2, con2);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        sQN = (rdr.GetInt32(0)) + 1;
                    }
                    con2.Close();

                }
                else
                {
                    con2.Close();
                    sQN = 1;
                }
                
                string cb = "insert into RefNumForInvoice(SClientId,SIN,QuotationId,InvoiceId) VALUES (@d2,@d3,@d4,@d5)";
                cmd = new SqlCommand(cb, con, trnas);
         
                cmd.Parameters.AddWithValue("d2", sclientId);
                cmd.Parameters.AddWithValue("d3", sQN);
                cmd.Parameters.AddWithValue("d4", quotationId);
                cmd.Parameters.AddWithValue("d5", invoiceId);

                cmd.ExecuteNonQuery();
                trnas.Commit();
                MessageBox.Show(@"Successfully Generated", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                QuotationIdLoad();
                cmbQuotation.ResetText();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error But We Are Rle Backing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                trnas.Rollback();
            }
            con.Close();
        }


        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbQuotation.Text))
            {
                MessageBox.Show("Please select Quotation Id/Ref/Number", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new ChangeFocusDelegate(changeFocus), cmbQuotation);
            }

            else
            {
                SaveInvoice();
               
            }
        }

        private void ClearData()
        {
            cmbQuotation.Items.Clear();
            cmbQuotation.SelectedIndex = -1;
            txtInvoiceParty.Clear();
            txtPayerAddress.Clear();
            txtLandPhone.Clear();
            txtRP.Clear();
            txtCellPhone.Clear();
            dtpInvoiceDate.ResetText();
            dtpDueDate.ResetText();
            txtTotalPrice.Clear();
            txtVATPercent.TextChanged -= txtVATPercent_TextChanged;
            txtVATPercent.Clear();
            //txtVATPercent.TextChanged -= txtVATPercent_TextChanged;
            txtVATAmount.Clear();
            txtAITPercent.TextChanged -= txtAITPercent_TextChanged;
            txtAITPercent.Clear();
            txtAITAmount.Clear();
            txtDiscountPercent.Clear();
            txtDiscountAmount.Clear();
            txtAdditionalDiscount.TextChanged -= txtAdditionalDiscount_TextChanged;
            txtAdditionalDiscount.Clear();
            txtAdvancePayment.TextChanged -= txtAdvancePayment_TextChanged;
            txtAdvancePayment.Clear();
            txtNetPayable.Clear();
            dtpPromisedDate.ResetText();            
        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpInvoiceDate.Value > DateTime.Now)
            {
                MessageBox.Show("Should not be exceed Date Time from today", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpInvoiceDate.ResetText();
            }
        }

        private void dtpDueDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpInvoiceDate.Value > dtpDueDate.Value)
            {
                MessageBox.Show("Due Date Should be grater than or Equal to Invoice Date", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDueDate.ResetText();
            } 
        }

        private void dtpPromisedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDueDate.Value > dtpPromisedDate.Value)
            {
                MessageBox.Show("Promised Date Should be grater than or Equal to Due Date", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDueDate.ResetText();
                dtpPromisedDate.ResetText();
            } 
        }

        private void cmbQuotation_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbQuotation.Text) && !cmbQuotation.Items.Contains(cmbQuotation.Text))
            {
                MessageBox.Show("Please Select A Valid Quotation Id/Ref/Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbQuotation.ResetText();
                this.BeginInvoke(new ChangeFocusDelegate(changeFocus), cmbQuotation);
            }
        }
    }
}
