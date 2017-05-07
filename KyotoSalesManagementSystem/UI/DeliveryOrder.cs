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
    public partial class DeliveryOrder : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs = new ConnectionString();
        private SqlDataReader rdr;
        public int userId, orderId;
        public string qtype;
        public DeliveryOrder()
        {
            InitializeComponent();
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
                string cd = "SELECT QType FROM Quotation where QuotationId='" + comboBox1.Text + "'";
                cmd =new SqlCommand(cd);
                cmd.Connection = con;
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    qtype = (rdr.GetString(0));
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
                Report1();
                Report2();
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

           
            //ParameterField paramField1 = new ParameterField();


            ////creating an object of ParameterFields class
            //ParameterFields paramFields1 = new ParameterFields();

            ////creating an object of ParameterDiscreteValue class
            //ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            ////set the parameter field name
            //paramField1.Name = "id";

            ////set the parameter value
            //paramDiscreteValue1.Value = orderId;

            ////add the parameter value in the ParameterField object
            //paramField1.CurrentValues.Add(paramDiscreteValue1);

            ////add the parameter in the ParameterFields object
            //paramFields1.Add(paramField1);
            //ReportView f2 = new ReportView();
            //TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            //TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            //ConnectionInfo reportConInfo = new ConnectionInfo();
            //Tables tables = default(Tables);
            ////	Table table = default(Table);
            //var with1 = reportConInfo;
            //with1.ServerName = "tcp:KyotoServer,49172";
            //with1.DatabaseName = "ProductNRelatedDB";
            //with1.UserID = "sa";
            //with1.Password = "SystemAdministrator";
            //if (qtype=="Custom")
            //{
            //   DOC cr = new DOC();
            //   tables = cr.Database.Tables;
            //   foreach (Table table in tables)
            //   {
            //       reportLogonInfo = table.LogOnInfo;
            //       reportLogonInfo.ConnectionInfo = reportConInfo;
            //       table.ApplyLogOnInfo(reportLogonInfo);
            //   }
            //   f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            //   f2.crystalReportViewer1.ReportSource = cr;
            //}
            //else
            //{
            //    DO cr =new DO();
            //    tables = cr.Database.Tables;
            //    foreach (Table table in tables)
            //    {
            //        reportLogonInfo = table.LogOnInfo;
            //        reportLogonInfo.ConnectionInfo = reportConInfo;
            //        table.ApplyLogOnInfo(reportLogonInfo);
            //    }
            //    f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            //    f2.crystalReportViewer1.ReportSource = cr;
            //}
            
           
            //this.Visible = false;

            //f2.ShowDialog();
            //this.Visible = true;
           
            
        }
        private void Report2()
        {


            //ParameterField paramField1 = new ParameterField();


            ////creating an object of ParameterFields class
            //ParameterFields paramFields1 = new ParameterFields();

            ////creating an object of ParameterDiscreteValue class
            //ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            ////set the parameter field name
            //paramField1.Name = "id";

            ////set the parameter value
            //paramDiscreteValue1.Value = orderId;

            ////add the parameter value in the ParameterField object
            //paramField1.CurrentValues.Add(paramDiscreteValue1);

            ////add the parameter in the ParameterFields object
            //paramFields1.Add(paramField1);
            //ReportView f2 = new ReportView();
            //TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            //TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            //ConnectionInfo reportConInfo = new ConnectionInfo();
            //Tables tables = default(Tables);
            ////	Table table = default(Table);
            //var with1 = reportConInfo;
            //with1.ServerName = "tcp:KyotoServer,49172";
            //with1.DatabaseName = "ProductNRelatedDB";
            //with1.UserID = "sa";
            //with1.Password = "SystemAdministrator";
            //if (qtype == "Custom")
            //{
            //    DOOC cr = new DOOC();
            //    tables = cr.Database.Tables;
            //    foreach (Table table in tables)
            //    {
            //        reportLogonInfo = table.LogOnInfo;
            //        reportLogonInfo.ConnectionInfo = reportConInfo;
            //        table.ApplyLogOnInfo(reportLogonInfo);
            //    }
            //    f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            //    f2.crystalReportViewer1.ReportSource = cr;
            //}
            //else
            //{
            //    DOO cr = new DOO();
            //    tables = cr.Database.Tables;
            //    foreach (Table table in tables)
            //    {
            //        reportLogonInfo = table.LogOnInfo;
            //        reportLogonInfo.ConnectionInfo = reportConInfo;
            //        table.ApplyLogOnInfo(reportLogonInfo);
            //    }
            //    f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            //    f2.crystalReportViewer1.ReportSource = cr;
            //}
            //this.Visible = false;

            //f2.ShowDialog();
            //this.Visible = true;


        }

    }
    
}
