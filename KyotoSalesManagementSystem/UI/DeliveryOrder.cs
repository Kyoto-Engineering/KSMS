using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using KyotoSalesManagementSystem.DAO;
using KyotoSalesManagementSystem.DBGateway;
using KyotoSalesManagementSystem.LoginUI;
using KyotoSalesManagementSystem.Reports;
using ZXing;
using ZXing.Common;

namespace KyotoSalesManagementSystem.UI
{
    public partial class DeliveryOrder : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        private SqlDataReader rdr;
        public int userId, orderId, brandid;
        public string qtype, refid;
        public DeliveryOrder()
        {
            InitializeComponent();
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Your background task goes here
            for (int i = 0; i <= 20; i++)
            {
                // Report progress to 'UI' thread
                backgroundWorker1.ReportProgress(i);
                // Simulate long task
                System.Threading.Thread.Sleep(100);
            }
        }
        // Back on the 'UI' thread so we can update the progress bar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBar1.Value = e.ProgressPercentage;
        }
        private void DeliveryOrder_Load(object sender, EventArgs e)
        {
            userId = frmLogin.uId;
            PopulateQId();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                string cd = "SELECT Quotation.QType, Quotation.BrandId, RefNumForQuotation.ReferenceNo FROM Quotation INNER JOIN RefNumForQuotation ON Quotation.QuotationId = RefNumForQuotation.QuotationId WHERE Quotation.QuotationId = '" + comboBox1.Text + "'";
                cmd =new SqlCommand(cd);
                cmd.Connection = con;
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    qtype = (rdr.GetString(0));
                    brandid = Convert.ToInt32(rdr["BrandId"]);
                    refid = (rdr.GetString(2));
                }
                con.Close();
                con = new SqlConnection(cs.DBConn);
                string cb = "insert into DeleveryOrder(QuotationId,Cosignor,D_Date,Order_no,Order_Time) VALUES (@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int,SCOPE_IDENTITY())";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("d1", comboBox1.Text);
                cmd.Parameters.AddWithValue("d2", userId);
                cmd.Parameters.AddWithValue("d3", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("d4", string.IsNullOrEmpty(textBox1.Text) ? (object)DBNull.Value : textBox1.Text);
                cmd.Parameters.AddWithValue("d5", DateTime.UtcNow.ToLocalTime());
                con.Open();
                orderId = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("Successfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (qtype == "General")
                {
                    Report1();
                }
                else
                {
                    Report2();
                }
                
                Reset();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateQId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Quotation.QuotationId from Quotation where QStatus='Accepted' Except select DeleveryOrder.QuotationId from DeleveryOrder";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    comboBox1.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            PopulateQId();
            dateTimePicker1.Value=DateTime.Today;
            textBox1.Clear();
        }

        private void DeliveryOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            MainUI frm =new MainUI();
            frm.Show();
        }
        private void Report1()
        {
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
            progressBar1.Visible = true;

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = orderId;

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
            with1.DatabaseName = "NewProductList1";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            ReportDocument cr = new ReportDocument();
            if (brandid == 1)
            {
                cr = new DOOmron();
            }
            else if (brandid == 2)
            {
                cr = new DOWithoutLogo();
            }
            else if (brandid == 3)
            {
                cr = new DOAzbil();
            }
            else if (brandid == 4)
            {
                cr = new DOBusinessAutomation();
            }
            else if (brandid == 5)
            {
                cr = new DOIRD();
            }
            else if (brandid == 6)
            {
                cr = new DOKawaShima();
            }
                
                tables = cr.Database.Tables;
                foreach (Table table in tables)
                {
                    reportLogonInfo = table.LogOnInfo;
                    reportLogonInfo.ConnectionInfo = reportConInfo;
                    table.ApplyLogOnInfo(reportLogonInfo);
                }
        
            Sections                              m_boSections;
            ReportObjects                    m_boReportObjects;
            SubreportObject                 m_boSubreportObject;
            //create a new ReportDocument
      
     
            //get all the sections in the report
            m_boSections =cr.ReportDefinition.Sections;
            //loop through each section of the report
            foreach (Section m_boSection in m_boSections)
            {
                m_boReportObjects = m_boSection.ReportObjects;
                //loop through each report object
                foreach ( ReportObject m_boReportObject in m_boReportObjects)
                {
                    if (m_boReportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        //get the actual subreport object
                        m_boSubreportObject = (SubreportObject) m_boReportObject;
                   
                        //set subreport to the dataset e.g.;
// boReportDocument.SetDataSource(oDataSet);
// or 
// boTable.SetDataSource(oDataSet.Tables[tableName]);
                    }
                }
            }
            //show in reportviewer
            //crystalReportViewer1.ReportSource = m_boReportDocument;
            BArcode ds = new BArcode();

            var content = refid.ToString();
            var writer = new BarcodeWriter
            {

                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 100,
                    Width = 450
                }
            };
            var png = writer.Write(content);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            png.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            DataRow dtr = ds.Tables[0].NewRow();
            dtr["REF"] = comboBox1.Text;
            dtr["BarcodeImage"] = ms.ToArray();
            ds.Tables[0].Rows.Add(dtr);
            cr.Subreports["BarCode.rpt"].DataSourceConnections.Clear();
            cr.Subreports["BarCode.rpt"].SetDataSource(ds);
                f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
                f2.crystalReportViewer1.ReportSource = cr;           
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
            backgroundWorker1.CancelAsync();
            backgroundWorker1.Dispose();
            progressBar1.Visible = false;
            button1.Enabled = true;
            
        }
        private void Report2()
        {


            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = orderId;

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
            with1.DatabaseName = "NewProductList1";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            ReportDocument cr = new ReportDocument();
            if (brandid == 1)
            {
                cr = new DOCOmron();
            }
            else if (brandid == 2)
            {
                cr = new DOCWithoutLogo();
            }
            else if (brandid == 3)
            {
                cr = new DOCAzbil();
            }
            else if (brandid == 4)
            {
                cr = new DOCBusinessAutomation();
            }
            else if (brandid == 5)
            {
                cr = new DOCIRD();
            }
            else if (brandid == 6)
            {
                cr = new DOCKawaShima();
            }

            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            BArcode ds = new BArcode();

            var content = refid.ToString();
            var writer = new BarcodeWriter
            {

                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 100,
                    Width = 450
                }
            };
            var png = writer.Write(content);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            png.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            DataRow dtr = ds.Tables[0].NewRow();
            dtr["REF"] = comboBox1.Text;
            dtr["BarcodeImage"] = ms.ToArray();
            ds.Tables[0].Rows.Add(dtr);
            cr.Subreports["BarCode.rpt"].DataSourceConnections.Clear();
            cr.Subreports["BarCode.rpt"].SetDataSource(ds);
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    con = new SqlConnection(cs.DBConn);
            //    con.Open();
            //    //string ct = "select   T.QuotationId, T.QType, BrandId from  Quotation T JOIN RefNumForQuotation N ON T.QuotationId = N.QuotationId  where N.ReferenceNo='" + comboBox1.Text + "'";
            //    string ct = "SELECT dbo.Quotation.BrandId FROM dbo.Brand INNER JOIN dbo.Quotation ON dbo.Brand.BrandId = dbo.Quotation.BrandId  where QuotationId='" + comboBox1.Text + "'";
            //    cmd = new SqlCommand(ct);
            //    cmd.Connection = con;
            //    rdr = cmd.ExecuteReader();

            //    if (rdr.Read())
            //    {
                    
            //        brandid = Convert.ToInt32(rdr["BrandId"]);
            //    }
            //    con.Close();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

    }
    
}
