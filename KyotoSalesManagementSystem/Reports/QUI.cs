﻿using System;
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
using ZXing;
using ZXing.Common;

namespace KyotoSalesManagementSystem.Reports
{
    public partial class QUI : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        private SqlDataReader rdr;
        public int quotationId, brandid;
        public string qtype;
        public QUI()
        {
            InitializeComponent();
            //Shown += new EventHandler(Form1_Shown);

           
        }
        void Form1_Shown(object sender, EventArgs e)
        {
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();
        }
        // On worker thread so do our thing!
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
        private void QUI_Load(object sender, EventArgs e)
        {
             try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(RefNumForQuotation.ReferenceNo) from RefNumForQuotation";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex!=-1)
            {
                if (qtype == "General")
                {
                    Report1();
                }
                else
                {
                    Report2();
                }
            }
            else
            {
                MessageBox.Show(@"Select Refrenece No First");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select   T.QuotationId, T.QType, T.BrandId from  Quotation T JOIN RefNumForQuotation N ON T.QuotationId = N.QuotationId  where N.ReferenceNo='" + comboBox1.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

               if (rdr.Read())
                    {
                       // txtBalance.Text = (rdr.GetDouble(0).ToString());
                        quotationId = (rdr.GetInt32(0));
                        qtype = (rdr.GetString(1));
                        brandid = Convert.ToInt32(rdr["BrandId"]);
                    }
                con.Close();

            }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
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
            with1.DatabaseName = "ProductNRelatedDB";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            ReportDocument cr = new ReportDocument();
            if (brandid == 1)
            {
                cr = new CrystalReport2();
            }
            else if (brandid == 2)
            {
                cr = new QuotationKEAL();
            }
            else if (brandid == 3)
            {
                cr = new QuotationAzbil();
            }
            else if (brandid == 4)
            {
                cr = new QuotationBusinessAutomation();
            }
            else if (brandid == 5)
            {
                cr = new QuotationIRD();
            }
            else if (brandid == 6)
            {
                cr = new QuotationKawasima();
            }
            else if (brandid == 7)
            {
                cr = new QuotationChigo();
            }
            else if (brandid == 8)
            {
                cr = new QuotationSamsungDVM();
            }

            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            BArcode ds = new BArcode();

            var content = comboBox1.Text;
            var writer = new BarcodeWriter
            {

                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 100,
                    Width = 465
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
            with1.DatabaseName = "ProductNRelatedDB";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            ReportDocument cr = new ReportDocument();
            if (brandid == 1)
            {
                cr = new CQOmron();
            }
            else if (brandid == 2)
            {
                cr = new CQWithoutLogo();
            }
            else if (brandid == 3)
            {
                cr = new CQAzbil();
            }
            else if (brandid == 4)
            {
                cr = new CQBusinessAutomation();
            }
            else if (brandid == 5)
            {
                cr = new CQIRD();
            }
            else if (brandid == 6)
            {
                cr = new CQKawaShima();
            }
            else if (brandid == 7)
            {
                cr = new CQChigo();
            }
            else if (brandid == 8)
            {
                cr = new CQSamsung_DVM();
            }
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            BArcode ds = new BArcode();

            var content = comboBox1.Text;
            var writer = new BarcodeWriter
            {

                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 100,
                    Width = 465
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
        }
    }

