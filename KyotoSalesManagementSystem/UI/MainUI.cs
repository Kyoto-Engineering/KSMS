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
using KyotoSalesManagementSystem.DBGateway;
using KyotoSalesManagementSystem.LoginUI;
using KyotoSalesManagementSystem.Reports;

namespace KyotoSalesManagementSystem.UI
{
    public partial class MainUI : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public MainUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Quotation frm=new Quotation();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotationForOvercease frm = new QuotationForOvercease();
            frm.Show();
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmLogin frm=new frmLogin();
            frm.Show();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            //this.Dispose();
            frmRegistration frm = new frmRegistration();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QUI f2=new QUI();
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotoSUI frm = new QuotoSUI();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            DeliveryOrder frm = new DeliveryOrder();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotationForCustom frm = new QuotationForCustom();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IUI f2 = new IUI();
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DAUI f2 = new DAUI();
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotationMMix frm = new QuotationMMix();
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
Delivery f2=new Delivery();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void PrintDeliverybutton_Click(object sender, EventArgs e)
        {
            DeliUI f11 = new DeliUI();
            this.Visible = false;

            f11.ShowDialog();
            this.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ReturnRequest f11 = new ReturnRequest();
            this.Visible = false;
            f11.ShowDialog();
            this.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Invoice frm=new Invoice();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void AllSalesbutton_Click(object sender, EventArgs e)
        {
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
            SalesReportWithTotalAmount sr = new SalesReportWithTotalAmount();
            tables = sr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ReportSource = sr;
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void DateWiseSalesReportbutton_Click(object sender, EventArgs e)
        {
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
            DateWiseSalesReport sr = new DateWiseSalesReport();
            tables = sr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ReportSource = sr;
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void WiseSalesReportbuttonbutton_Click(object sender, EventArgs e)
        {
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
            CompanyWiseSalesReport sr = new CompanyWiseSalesReport();
            tables = sr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ReportSource = sr;
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }
    }
}
