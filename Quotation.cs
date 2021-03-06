﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using KyotoSalesManagementSystem.DBGateway;
using KyotoSalesManagementSystem.LoginUI;
using KyotoSalesManagementSystem.Reports;

namespace KyotoSalesManagementSystem.UI
{
    
    public partial class Quotation : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public string quotationBy;
        public double Tam;
        public string salesClientId, userId, productId, firstProductId, tAmount;
        public decimal dmaount = 0, lTAmount = 0, subAmount = 0, takeRemovePric = 0, takeRemoveQuantity = 0, takeRemove = 0, takeRemove2 = 0, presentTotalPrice = 0, tPrice = 0, taxPercent = 0, txVatAmount;
        public decimal aitPercent = 0, aitAmount = 0, netPayable = 0, discount = 0, discountPercent = 0, myNetPayable = 0, myVAT = 0, myAIT = 0, myDis = 0;
        public string pId, referenceNo, mAdv, pDoc, pOD, rOP, myMobAd, myPAS, myPOD, myROP;
        public int quotationId, sClientIdForRefNum, sQN;
        public decimal totalPercent, myMOBAd1, myPAS1, myPOD1, myROP1;
        public decimal vt = 0, ait = 0, dis = 0, t = 0;
        public Nullable<decimal> vatNull, aitNull, disNull;
             
             


       
        public Quotation()
        {
            InitializeComponent();
        }
       
       public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(ProductListSummary.ItemCode),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
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

        private void TotalAmountOfTermsAndCondition()
        {
            if (checkMobAd.Checked)
            {
                myMobAd = txtMAPercent.Text;

            }
            else
            {
                myMobAd = "0";
            }
            if (checkPASCBS.Checked)
            {
                myPAS = txtPASChalan.Text;
            }
            else
            {
                myPAS = "0";
            }
            if (checkPOD.Checked)
            {
                myPOD = txtPOD1.Text;
            }
            else
            {
                myPOD = "0";
            }
            if (checkROP.Checked)
            {
                myROP = txtROP.Text;
            }
            else
            {
                myROP = "0";
            }
            myMOBAd1 = Convert.ToDecimal(myMobAd);
            myPAS1 = Convert.ToDecimal(myPAS);
            myPOD1 = Convert.ToDecimal(myPOD);
            myROP1 = Convert.ToDecimal(myROP);
            totalPercent = myMOBAd1 + myPAS1 + myPOD1 + myROP1;
            
            
        }
        private void Quotation_Load(object sender, EventArgs e)
        {
            GetData();
            userId = frmLogin.uId.ToString();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox6.Enabled = false;
            groupBox7.Enabled = false;
        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void SaveAttention()
        {
            string  d, c,e;
            if (txtDesignation.Text != "")
            {
                d = txtDesignation.Text;
            }
            else
            {
                d = null;
            }
            if (txtContactNo.Text!= "")
            {
                c = txtContactNo.Text;
            }
            else
            {
                c = null;
            }
            if (waterMarkTextBox1.Text != "")
            {
                e = waterMarkTextBox1.Text;
            }
            else
            {
                e = null;
            }
            try
            {
                if (txtAttention.Text != "")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query = "insert into QAttentionDetails(QuotationId,Attention,Designation,ContactNo,Email) values(@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@d1", quotationId);
                    cmd.Parameters.AddWithValue("@d2", txtAttention.Text);
                    cmd.Parameters.AddWithValue("@d3", (object)d??DBNull.Value);
                    cmd.Parameters.AddWithValue("@d4", (object)c ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@d5", (object)e ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetQuotationId()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string cty4 = "SELECT MAX(Quotation.QuotationId) FROM Quotation";
            cmd = new SqlCommand(cty4);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                quotationId = (rdr.GetInt32(0));

            }
            con.Close();

        }
        private void SaveProductQuotation()
        {
            try
            {
               // GetQuotationId();

                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into ProductQuotation(QuotationId,Sl,UnitPrice,Quantity,MOQ,Specification,CountryOfOrigin) VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", quotationId);
                    cmd.Parameters.AddWithValue("d2", listView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", listView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", listView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d5", listView1.Items[i].SubItems[4].Text);
                    cmd.Parameters.AddWithValue("d6", listView1.Items[i].SubItems[5].Text);
                    cmd.Parameters.AddWithValue("d7", listView1.Items[i].SubItems[6].Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePaymentTerms()
        {
             mAdv = "Mobilization Advance  " + myMobAd+"%";
            pDoc = "Payment at Sight Of Chalan/BL/Shipping Document  " + myPAS+"%";
            pOD = "Payment on Delivery  " + myPOD+"%";
            rOP = "Rest Of Payment  " + myROP+"%  within" + txtROPDays.Text+"days";
            try
            {
                if (checkMobAd.Checked)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into PaymentTerms(Text1,QuotationId) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", mAdv);
                    cmd.Parameters.AddWithValue("d2", quotationId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (checkPASCBS.Checked)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into PaymentTerms(Text1,QuotationId) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", pDoc);
                    cmd.Parameters.AddWithValue("d2", quotationId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (checkPOD.Checked)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into PaymentTerms(Text1,QuotationId) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", pOD);
                    cmd.Parameters.AddWithValue("d2", quotationId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (checkROP.Checked)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into PaymentTerms(Text1,QuotationId) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", rOP);
                    cmd.Parameters.AddWithValue("d2", quotationId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveNoteTerms()
        {
            try
            {
                if (listView2.Items.Count != 0)
                {
                    for (int i = 0; i <= listView2.Items.Count - 1; i++)
                    {
                        con = new SqlConnection(cs.DBConn);
                        string cb = "insert into NoteTerms(Textn,QuotationId) VALUES (@d1,@d2)";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("d1", listView2.Items[i].SubItems[1].Text);
                        cmd.Parameters.AddWithValue("d2", quotationId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveReferenceNumForQuotation()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query = "Select SClientId From RefNumForQuotation where SClientId='" + txtClientId.Text+ "'";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    sClientIdForRefNum = (rdr.GetInt32(0));
                }

                if (sClientIdForRefNum == Convert.ToInt32(txtClientId.Text))
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    //string q2 = "Select SQN From RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    string qr2 = "SELECT MAX(RefNumForQuotation.SQN) FROM RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    cmd = new SqlCommand(qr2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        sQN = (rdr.GetInt32(0));
                        sQN = sQN + 1;
                        referenceNo = "OIA-" + sClientIdForRefNum + "-" + sQN + "-" + quotationId +"";
                    }
                }
                else
                {
                    sQN = 1;
                    referenceNo = "OIA-" + txtClientId.Text + "-" + sQN + "-" + quotationId + "";
                }


                if (Convert.ToInt32(txtClientId.Text) == sClientIdForRefNum)
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb ="insert into RefNumForQuotation(BrandCode,SClientId,SQN,QuotationId,ReferenceNo) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", "OIA");
                    cmd.Parameters.AddWithValue("d2", sClientIdForRefNum);
                    cmd.Parameters.AddWithValue("d3", sQN);
                    cmd.Parameters.AddWithValue("d4", quotationId);
                    cmd.Parameters.AddWithValue("d5", referenceNo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    string cb = "insert into RefNumForQuotation(BrandCode,SClientId,SQN,QuotationId,ReferenceNo) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", "OIA");
                    cmd.Parameters.AddWithValue("d2", txtClientId.Text);
                    cmd.Parameters.AddWithValue("d3", sQN);
                    cmd.Parameters.AddWithValue("d4", quotationId);
                    cmd.Parameters.AddWithValue("d5", referenceNo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

       
        public void GetNetPayable()
        {
            t = decimal.Parse(txtTotalPrice.Text);

            if (checkVAT.Checked == true)
            {
               vatNull =vt = decimal.Parse(txtVATPercent.Text);

            }
            else
            {
                vt = 0;
                txtVATAmount.Clear();
                vatNull = null;
            }

            if (checkAIT.Checked == true)
            {
                aitNull=ait = decimal.Parse(txtAITPercent.Text);
            }
            else
            {
                ait = 0;
                aitNull = null;
                txtAITAmount.Clear();
            }
            if (checkDiscount.Checked == true)
            {
               disNull= dis = decimal.Parse(txtDiscountPercent.Text);
            }
            else
            {
                dis = 0;
                disNull = null;
                txtDiscountAmount.Clear();
            }
            myVAT = (t*vt)/100;
            myAIT = (t*ait)/100;
            myDis = (t*dis)/100;

            myNetPayable = t + myVAT +  myAIT- myDis;
            txtVATAmount.Text = myVAT.ToString();
            txtAITAmount.Text = myAIT.ToString();
            txtDiscountAmount.Text = myDis.ToString();
            txtNetPayable.Text = myNetPayable.ToString();
           

        }

        private void SaveQuotation()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                string cb = "insert into Quotation(TotalPrice,QVat,QAIT,Discount,NetPayable,Validity,Delivery,UserId,Dates,QStatus,ValidityStatus,QType) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int,SCOPE_IDENTITY())";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("d1", txtTotalPrice.Text);
                cmd.Parameters.AddWithValue("d2", (object)vatNull ?? DBNull.Value);
                cmd.Parameters.AddWithValue("d3", (object)aitNull ?? DBNull.Value);
                cmd.Parameters.AddWithValue("d4", (object)disNull ?? DBNull.Value);
                cmd.Parameters.AddWithValue("d5", txtNetPayable.Text);
                cmd.Parameters.AddWithValue("d6", txtOfferValidity.Text);
                cmd.Parameters.AddWithValue("d7", txtLeadTime.Text);
                cmd.Parameters.AddWithValue("d8", userId);
                cmd.Parameters.AddWithValue("d9", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("d10","Quoted");
                cmd.Parameters.AddWithValue("d11", "Valid");
                cmd.Parameters.AddWithValue("d12", "Omron");
                con.Open();
                quotationId = (int) cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOfferValidity.Text))
            {
                MessageBox.Show("Please enter offer Validity", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOfferValidity.Focus();

            }
            else if (string.IsNullOrWhiteSpace(txtLeadTime.Text))
            {
                MessageBox.Show("Please give Lead Time", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLeadTime.Focus();

            }
            else if (checkMobAd.Checked && string.IsNullOrWhiteSpace(txtMAPercent.Text))
            {
                MessageBox.Show("Insert Mobilization Advance Or Untick Check Box", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMAPercent.Focus();


            }


            else if (checkPASCBS.Checked && string.IsNullOrWhiteSpace(txtPASChalan.Text))
            {
                MessageBox.Show("Insert Payment at Sight Of Chalan/BL/Shipping Document Or Untick Check Box",
                    "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPASChalan.Focus();

            }


            else if (checkPOD.Checked && string.IsNullOrWhiteSpace(txtPOD1.Text))
            {
                MessageBox.Show("Insert Payment on  Delivery Or Untick Check Box", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPOD1.Focus();

            }




            else if (checkROP.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtROP.Text))
                {
                    MessageBox.Show("Insert Rest Of Payment Or Untick Check Box", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtROP.Focus();

                }
                else if (string.IsNullOrWhiteSpace(txtROPDays.Text))
                {
                    MessageBox.Show("Insert Rest Of Payment Number of Days Or Untick Check Box", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtROPDays.Focus();
                }
            }
            else
            {

            GetNetPayable();

            try
            {

                TotalAmountOfTermsAndCondition();
                if (totalPercent == 100.00m)
                {
                    SaveQuotation();
                    SaveProductQuotation();
                    SaveAttention();
                    SaveReferenceNumForQuotation();
                    SavePaymentTerms();
                    SaveNoteTerms();
                    MessageBox.Show("Successfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Report();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your total Percent of Terms  And Coditions Must be equal to 100");
                    return;
                }

                //if (totalPercent == 0)
                // {
                //     SaveQuotation();
                //     SaveProductQuotation();
                //     SaveAttention();
                //     SaveReferenceNumForQuotation();
                //     SavePaymentTerms();
                //     SaveNoteTerms();
                //     MessageBox.Show("Successfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //     Reset();
                // }
                //if (totalPercent != 100 || totalPercent != 0)
                //{
                //    MessageBox.Show("Your total Percent of Terms  And Coditions Must be equal to 100");
                //    return;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


        private void CalculateTotalAmount()
        {
            try
            {
                    dmaount = Convert.ToDecimal(txtTotalAmount.Text);
                    lTAmount = subAmount + dmaount;
                    txtTotalPrice.Text = lTAmount.ToString();
                    subAmount = lTAmount;
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
        }
        private void btnOvercease_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new OverceaseProductRecord();
            frm.ShowDialog();
            this.Visible = true;
            //this.Dispose();
            //OverceaseProductRecord frm=new OverceaseProductRecord();
            //frm.Show();
        }

        private void btnLocalwarehouse_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //dynamic frm = new ProductRecordForLocalStore();
            //frm.ShowDialog();
            //this.Visible = true;
            this.Dispose();
            ProductRecordForLocalStore frm = new ProductRecordForLocalStore();
            frm.Show();
        }

        private void btnClientGrid_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SalesClientRecord frm = new SalesClientRecord();
            frm.Show();
            //this.Visible = false;
            //dynamic frm = new SalesClientRecord();
            //frm.ShowDialog();
            //this.Visible = true;
        }

        
        private void txtQuotQuantity_TextChanged(object sender, EventArgs e)
        {
            //TotalAmount();

        }

        private void GetProductId()
        {
            try
            {
               con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(MasterStocks.Sl) from  MasterStocks,ProductListSummary  WHERE MasterStocks.Sl=ProductListSummary.Sl and ProductListSummary.ProductGenericDescription = '" + txtProductName.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    productId = (rdr.GetString(0));
                   
                }
                
               con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MainUI frm=new MainUI();
            frm.Show();
        }

        private void SearchById()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(MasterStocks.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where ProductListSummary.Sl=MasterStocks.Sl and MasterStocks.Sl like '" + txtProId.Text + "' order by MasterStocks.Sl desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchByProductName()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(MasterStocks.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where MasterStocks.Sl=ProductListSummary.Sl and ProductListSummary.ProductGenericDescription like '" + txtSProductName.Text + "' order by MasterStocks.Sl desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSProductId_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = new SqlCommand("SELECT RTRIM(T.Sl),RTRIM(T.ProductGenericDescription),RTRIM(T.ItemDescription),RTRIM(T.ItemCode),RTRIM(N.MQuantity),RTRIM(N.UnitPrice) FROM ProductListSummary T JOIN MasterStocks N ON T.Sl = N.Sl  where T.ItemCode like '" + txtProId.Text + "%' order by N.Sl", con);
            //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where ProductListSummary.ProductGenericDescription like '" + txtSProductName.Text + "%' order by ProductListSummary.Sl", con);
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnHeadersVisible = false;
            while (rdr.Read() == true)
            {
                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
            }
            con.Close();
                 con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtSProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(T.Sl),RTRIM(T.ProductGenericDescription),RTRIM(T.ItemDescription),RTRIM(T.ItemCode),RTRIM(N.MQuantity),RTRIM(N.UnitPrice) FROM ProductListSummary T JOIN MasterStocks N ON T.Sl = N.Sl  where T.ProductGenericDescription like '" + txtSProductName.Text + "%' order by N.Sl", con);
                //cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where ProductListSummary.ProductGenericDescription like '" + txtSProductName.Text + "%' order by ProductListSummary.Sl", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnHeadersVisible = false;
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

            ///
            ///  cmd = new SqlCommand("SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(ProductListSummary.ItemDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl order by MasterStocks.Sl desc", con);
                //rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();
                //while (rdr.Read() == true)
                //{
                //    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3],rdr[4]);

            ///////////////



            //try
            //{
            //    con = new SqlConnection(cs.DBConn);
            //    con.Open();
            //    string query = "SELECT RTRIM(ProductListSummary.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from ProductListSummary,MasterStocks where MasterStocks.Sl=ProductListSummary.Sl and ProductListSummary.ProductGenericDescription like '" + txtSProductName.Text + "'";
            //    cmd = new SqlCommand(query, con);
            //    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    dataGridView1.Rows.Clear();
            //    while (rdr.Read() == true)
            //    {
            //        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            txtProductName.Text = dr.Cells[1].Value.ToString();
            txtAvailableQuantity.Text = dr.Cells[4].Value.ToString();
            txtUnitPrice.Text = dr.Cells[5].Value.ToString();
            labelv.Text = labelg.Text;
            pId = dr.Cells[0].Value.ToString();

        }

        private void txtQuotQuantity_TextChanged_1(object sender, EventArgs e)
        {
            decimal val1 = 0;
            decimal val2 = 0;
            decimal.TryParse(txtUnitPrice.Text, out val1);
            decimal.TryParse(txtQuotQuantity.Text, out val2);
            decimal I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();

            
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtQuotQuantity.Text == "")
            {
                MessageBox.Show("Please enter quotation Amount", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuotQuantity.Focus();
                return;
            }
            if (txtMOQ.Text == "")
            {
                MessageBox.Show("Please enter Minimum Order Quantity", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMOQ.Focus();
                return;
            }
            if (txtSpecification.Text == "")
            {
                MessageBox.Show("Please enter Specification", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSpecification.Focus();
                return;
            }
            if (txtCountryOfOrigin.Text == "")
            {
                MessageBox.Show("Please enter appropriate Country Of Origin", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCountryOfOrigin.Focus();
                return;
            }

            try
            {
                if (listView1.Items.Count == 0)
                {
                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(pId);
                    lst.SubItems.Add(txtUnitPrice.Text);
                    lst.SubItems.Add(txtQuotQuantity.Text);
                    lst.SubItems.Add(txtMOQ.Text);
                    lst.SubItems.Add(txtSpecification.Text);
                    lst.SubItems.Add(txtCountryOfOrigin.Text);
                    listView1.Items.Add(lst);

                    CalculateTotalAmount();
                    GetNetPayable();

                    txtProductName.Text = "";
                    txtUnitPrice.Text = "";
                    txtAvailableQuantity.Text = "";
                    txtQuotQuantity.Text = "";
                    txtTotalAmount.Text = "";
                    txtMOQ.Text = "";
                    txtSpecification.Text = "";
                    txtCountryOfOrigin.Text = "";

                    return;
                }

                ListViewItem lst1 = new ListViewItem();
                lst1.SubItems.Add(pId);
                lst1.SubItems.Add(txtUnitPrice.Text);
                lst1.SubItems.Add(txtQuotQuantity.Text);
                lst1.SubItems.Add(txtMOQ.Text);
                lst1.SubItems.Add(txtSpecification.Text);
                lst1.SubItems.Add(txtCountryOfOrigin.Text);
                listView1.Items.Add(lst1);

                CalculateTotalAmount();
                GetNetPayable();


                txtProductName.Text = "";
                txtUnitPrice.Text = "";
                txtAvailableQuantity.Text = "";
                txtQuotQuantity.Text = "";
                txtTotalAmount.Text = "";
                txtMOQ.Text = "";
                txtSpecification.Text = "";
                txtCountryOfOrigin.Text = "";

                return;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductName_TextChanged_1(object sender, EventArgs e)
        {
            //GetProductId();
        }

        private void btnRemove_Click_1(object sender, EventArgs e)
        {
            takeRemovePric = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[2].Text);
            takeRemoveQuantity = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[3].Text);
            takeRemove = (takeRemoveQuantity * takeRemovePric);
            subAmount = subAmount - takeRemove;
            takeRemove2 = Convert.ToDecimal(txtTotalPrice.Text);
            presentTotalPrice = subAmount;
            txtTotalPrice.Text = presentTotalPrice.ToString();


            GetNetPayable();

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    listView1.Items[i].Remove();
                }

            }
            
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }

            if (checkVAT.Checked)
            {
                txtVATPercent.ReadOnly = false;
                
                    //tPrice = Convert.ToDecimal(txtTotalPrice.Text);
                    txtVATPercent.Text = "5";
                txtVATPercent.Focus();

            }
            else
            {
                txtVATPercent.Clear();
                txtVATAmount.Clear();
                txtVATPercent.ReadOnly = true;
            }

            GetNetPayable();
           
            ////GetNetPayable();
            //if (txtVATPercent.Text == "")
            //{
            //    if (checkVAT.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        txtVATPercent.Text = "5";
            //        taxPercent = Convert.ToDecimal(txtVATPercent.Text);
            //        txVatAmount = (tPrice * taxPercent) / 100;
            //        txtVATAmount.Text = txVatAmount.ToString();

            //    }
            //}
            //else
            //{
            //    if (checkVAT.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        taxPercent = Convert.ToDecimal(txtVATPercent.Text);
            //        txVatAmount = (tPrice * taxPercent) / 100;
            //        txtVATAmount.Text = txVatAmount.ToString();

            //    }
            //}
        }

        private void checkAIT_CheckedChanged(object sender, EventArgs e)
        {

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }

            if (checkAIT.Checked)
            {
                txtAITPercent.ReadOnly = false;
                
                   
                    txtAITPercent.Text = "4";
                txtAITPercent.Focus();

            }
            else
            {
                txtAITPercent.ReadOnly = true;
                txtAITPercent.Clear();
                txtAITAmount.Clear();
            }

            GetNetPayable();
           
            ////GetNetPayable();
            //if (txtAITPercent.Text == "")
            //{
            //    if (checkAIT.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        txtAITPercent.Text = "5";
            //        taxPercent = Convert.ToDecimal(txtAITPercent.Text);
            //        txVatAmount = (tPrice * taxPercent) / 100;
            //        txtAITAmount.Text = txVatAmount.ToString();

            //    }
            //}
            //else
            //{
            //    if (checkAIT.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        taxPercent = Convert.ToDecimal(txtAITPercent.Text);
            //        txVatAmount = (tPrice * taxPercent) / 100;
            //        txtAITAmount.Text = txVatAmount.ToString();

            //    }
            //}
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }
            GetNetPayable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtQuotNote.Text == "")
            {
                MessageBox.Show("Please type your note", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuotNote.Focus();
                return;
            }
            
            try
            {
                if (listView2.Items.Count == 0)
                {
                    ListViewItem lstn = new ListViewItem();
                    lstn.SubItems.Add(txtQuotNote.Text);
                    listView2.Items.Add(lstn);
                    txtQuotNote.Text = "";
                    txtQuotNote.Focus();
                    return;
                }

                ListViewItem lstn1 = new ListViewItem();
                lstn1.SubItems.Add(txtQuotNote.Text);
                listView2.Items.Add(lstn1);
                txtQuotNote.Text = "";
                txtQuotNote.Focus();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkDiscount_CheckedChanged(object sender, EventArgs e)
        {
    
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }

            if (checkDiscount.Checked)
            {
                txtDiscountPercent.ReadOnly = false;
                

                    txtDiscountPercent.Text = "5";
                txtDiscountPercent.Focus();


            }
            else
            {
                txtDiscountPercent.Clear();
                txtDiscountAmount.Clear();
                txtDiscountPercent.ReadOnly = true;
            }

            GetNetPayable();
            //if (txtDiscountPercent.Text == "")
            //{
            //    if (checkDiscount.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        txtDiscountPercent.Text = "0";
            //        discountPercent = Convert.ToDecimal(txtDiscountPercent.Text);
            //        discount = (tPrice * discountPercent) / 100;
            //        txtDiscountAmount.Text = discount.ToString();
            //        netPayable = tPrice + txVatAmount + aitAmount - discount;
            //        txtNetPayable.Text = netPayable.ToString();
            //    }
            //}
            //else
            //{
            //    if (checkDiscount.Checked)
            //    {
            //        tPrice = Convert.ToDecimal(txtTotalPrice.Text);
            //        discountPercent = Convert.ToDecimal(txtDiscountPercent.Text);
            //        discount = (tPrice * discountPercent) / 100;
            //        txtDiscountAmount.Text = discount.ToString();
            //        netPayable = tPrice + txVatAmount + aitAmount - discount;
            //        txtNetPayable.Text = netPayable.ToString();
            //    }
            //}
        }

        private void txtVATPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void txtAITPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtDiscountPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtMOQ_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtQuotQuantity.Text, out val1);
            int.TryParse(txtMOQ.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Minimum Order quantities must be less than Quotation quantities", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuotQuantity.Text = "";
                txtMOQ.Text = "";
                txtQuotQuantity.Focus();
                return;
            }
        }

        private void Reset()
        {

            txtAttention.Clear();
            txtDesignation.Clear();
            txtContactNo.Clear();
            txtClientId.Clear();
            txtClientName.Clear();

            txtOfferValidity.Clear();
            txtLeadTime.Clear();
            txtMAPercent.Clear();
            txtPASChalan.Clear();
            txtPOD1.Clear();
            txtROP.Clear();
            txtROPDays.Clear();
            waterMarkTextBox1.Clear();
            dateTimePicker1.ResetText();

            myMOBAd1=myPAS1=myPOD1=myROP1 = 0;
            dmaount = lTAmount = subAmount = takeRemove = takeRemove2 = presentTotalPrice = tPrice = taxPercent = 0;

            sClientIdForRefNum = sQN = 0;
            totalPercent = myMOBAd1 = myPAS1 = myPOD1 = myROP1 = 0;
            vt = ait = dis = t = 0;
            

            checkMobAd.Checked = false;
            checkPASCBS.Checked = false;
            checkPOD.Checked = false;
            checkROP.Checked = false;
            checkVAT.Checked = false;
            checkAIT.Checked = false;
            checkDiscount.Checked = false;

            listView1.Items.Clear();
            listView2.Items.Clear();
         
            txtTotalAmount.Clear();
            txtVATAmount.Clear();
            txtAITAmount.Clear();
            txtDiscountAmount.Clear();

            txtVATPercent.Clear();
            txtAITPercent.Clear();
            txtDiscountPercent.Clear();
            txtNetPayable.Clear();

            txtTotalPrice.Clear();
            



        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void txtProId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(MasterStocks.Sl),RTRIM(ProductListSummary.ProductGenericDescription),RTRIM(MasterStocks.MQuantity),RTRIM(MasterStocks.UnitPrice) from MasterStocks,ProductListSummary where ProductListSummary.Sl=MasterStocks.Sl and MasterStocks.Sl like '" + txtProId.Text + "' order by MasterStocks.Sl desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void S(object sender, EventArgs e)
        {

        }

        private void txtMAPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
                
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPASChalan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;

            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPOD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;

            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtROP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;

            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtROPDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;


            }
        }

        private void txtOfferValidity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;


            }
        }

        private void txtLeadTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;


            }
        }

        private void checkAIT_CheckStateChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }
            if (txtAITPercent.Text == "")
            {
                if (checkAIT.Checked)
                {
                    tPrice = Convert.ToDecimal(txtTotalPrice.Text);
                    txtAITPercent.Text = "4";
                    aitPercent = Convert.ToDecimal(txtAITPercent.Text);
                    aitAmount = (tPrice * aitPercent) / 100;
                    txtAITAmount.Text = aitAmount.ToString();
                }
            }
            else
            {
                tPrice = Convert.ToDecimal(txtTotalPrice.Text);
                aitPercent = Convert.ToDecimal(txtAITPercent.Text);
                aitAmount = (tPrice * aitPercent) / 100;
                txtAITAmount.Text = aitAmount.ToString();
            }
        }

        private void checkDiscount_CheckStateChanged(object sender, EventArgs e)
        {
           
        }

        private void checkVAT_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }

        }

        private void txtVATPercent_TextChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }
            GetNetPayable();
        }

        private void txtAITPercent_TextChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Please Add item in the list before checked");
                return;
            }
            GetNetPayable();
        }

        private void checkAIT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClientId_TextChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox6.Enabled = true;
            groupBox7.Enabled = true;
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void Report()
        {
            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = quotationId;

            //add the parameter value in the ParameterField object
            paramField1.CurrentValues.Add(paramDiscreteValue1);

            //add the parameter in the ParameterFields object
            paramFields1.Add(paramField1);
            ReportView f2 = new ReportView();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            CrystalReport2 cr = new CrystalReport2();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void txtQuotQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;


            }
        }

        private void txtMOQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;


            }
        }

        private void txtCountryOfOrigin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtCountryOfOrigin_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key >= Key.A && e.Key <= Key.Z)
            //{
            //}
            //else
            //{
            //    e.Handled = true;
            //}
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(this,new EventArgs());
            }
        }

        private void txtQuotQuantity_Validating(object sender, CancelEventArgs e)
        {
            decimal val1 = 0;
            decimal  val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtUnitPrice.Text, out val1);
            decimal.TryParse(txtQuotQuantity.Text, out val2);
            decimal.TryParse(txtAvailableQuantity.Text, out val3);
            decimal I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
            if (val3 < val2)
            {
                MessageBox.Show("Quotation Quantity can not be greater than the Available Quantity", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQuotQuantity.Clear();
                txtQuotQuantity.Focus();
                return;
            }
        }

        private void txtMOQ_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar != (char)(Keys.Delete) || e.KeyChar == Char.Parse(".")))
            {
                e.Handled = true;
                return;
            }
        }
        //private void txtMAPercent_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
        //    {
        //        e.Handled = true;
        //        return;
        //    }
        //}



        //private void txtPASChalan_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
        //    {
        //        e.Handled = true;
        //        return;
        //    }
        //}

        //private void txtROP_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
        //    {
        //        e.Handled = true;
        //        return;
        //    }
        //}

        //private void txtROPDays_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
        //    {
        //        e.Handled = true;
        //        return;
        //    }
        //}

        private void checkMobAd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMobAd.Checked == true)
            {
                txtMAPercent.ReadOnly = false;
                txtMAPercent.Focus();
            }
            else
            {
                txtMAPercent.Clear();
                txtMAPercent.ReadOnly = true;
            }
        }

        private void checkPASCBS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPASCBS.Checked)
            {
                txtPASChalan.ReadOnly = false;
                txtPASChalan.Focus();
            }
            else
            {
                txtPASChalan.Clear();
                txtPASChalan.ReadOnly = true;
            }
        }

        private void checkPOD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPOD.Checked)
            {
                txtPOD1.ReadOnly = false;
                txtPOD1.Focus();
            }
            else
            {
                txtPOD1.Clear();
                txtPOD1.ReadOnly = true;
            }
        }

        private void checkROP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkROP.Checked)
            {
                txtROP.ReadOnly = false;
                txtROPDays.ReadOnly = false;
                txtROP.Focus();
            }
            else
            {
                txtROP.Clear();
                txtROP.ReadOnly = true;
                txtROPDays.Clear();
                txtROPDays.ReadOnly = true;
            }
        }

        private void txtMAPercent_Validating(object sender, CancelEventArgs e)
        {

            decimal val1 = 100;
            decimal val2 = 0;
            decimal val3= 0;
            decimal.TryParse(txtMAPercent.Text, out val2);
            if (val2==val3)
            {
                checkMobAd.Checked = false;
            }
            else if(val2 > val1)
            {
                MessageBox.Show("Mobilization Advance must be less Or Equal than 100", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMAPercent.Clear();
                txtMAPercent.Focus();
                //return;
            }
        }

        private void txtPASChalan_Validating(object sender, CancelEventArgs e)
        {
            decimal val1 = 100;
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtPASChalan.Text, out val2);
            if (val2 == val3)
            {
                checkPASCBS.Checked = false;
            }
            else if (val2 > val1)
            {
                MessageBox.Show("Payment at Sight Of Chalan/BL/Shipping Document  must be less Or Equal than 100", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPASChalan.Clear();
                txtPASChalan.Focus();
                //return;
            }
        }

        private void txtPOD1_Validating(object sender, CancelEventArgs e)
        {
            decimal val1 = 100;
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtPOD1.Text, out val2);
            if (val2 == val3)
            {
                checkPOD.Checked = false;
            }
            else if (val2 > val1)
            {
                MessageBox.Show("Payment on  Delivery must be less Or Equal than 100", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPOD1.Clear();
                txtPOD1.Focus();
                //return;
            }

        }

        private void txtROP_Validating(object sender, CancelEventArgs e)
        {
            decimal val1 = 100;
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtROP.Text, out val2);
            if (val2 == val3)
            {
                checkROP.Checked = false;
            }
            else if (val2 > val1)
            {
                MessageBox.Show("Rest Of Payment must be less Or Equal than 100", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtROP.Clear();
                txtROP.Focus();
                //return;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtQuotNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this,new EventArgs());
            }
        }

        private void txtAttention_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDesignation.Focus();
                e.Handled = true;
            }
        }

        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContactNo.Focus();
                e.Handled = true;
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                waterMarkTextBox1.Focus();
                e.Handled = true;
            }
        }

        private void txtOfferValidity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkMobAd.Focus();
                e.Handled = true;
            }
        }

        private void txtQuotQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMOQ.Focus();
                e.Handled = true;
            }
        }

        private void txtMOQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSpecification.Focus();
                e.Handled = true;
            }
        }

        private void txtSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCountryOfOrigin.Focus();
                e.Handled = true;
            }
        }

        private void txtVATPercent_Validating(object sender, CancelEventArgs e)
        {
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtVATPercent.Text, out val2);
            if (val2 == val3)
            {
                checkVAT.Checked = false;
            }
        }

        private void txtAITPercent_Validating(object sender, CancelEventArgs e)
        {
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtAITPercent.Text, out val2);
            if (val2 == val3)
            {
                checkAIT.Checked = false;
            }
        }

        private void txtDiscountPercent_Validating(object sender, CancelEventArgs e)
        {
            decimal val2 = 0;
            decimal val3 = 0;
            decimal.TryParse(txtDiscountPercent.Text, out val2);
            if (val2 == val3)
            {
                checkDiscount.Checked = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtAttention.Focus();
        }

        private void waterMarkTextBox1_Validating(object sender, CancelEventArgs e)
        {
            Regex mRegxExpression;

            if (waterMarkTextBox1.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(waterMarkTextBox1.Text.Trim()))
                {

                    MessageBox.Show("E-mail address format is not correct.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    waterMarkTextBox1.Focus();

                }

            }
        }

        private void waterMarkTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOfferValidity.Focus();
                e.Handled = true;
            }
        }

        private void Quotation_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            MainUI frm = new MainUI();
            frm.Show();
        }
    }
}
