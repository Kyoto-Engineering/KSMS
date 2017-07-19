using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using KyotoSalesManagementSystem.DAO;
using KyotoSalesManagementSystem.LoginUI;
using KyotoSalesManagementSystem.DBGateway;
using KyotoSalesManagementSystem.Reports;
using ZXing;
using ZXing.Common;

namespace KyotoSalesManagementSystem.UI
{
    public partial class ReturnRequest : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string impOd;
        private DataGridViewRow dr;
        private int checkvalue,smId,available;
        private int SupplierId;
        private int Sio,OI,CI,CR;
        public string ShID;
        private string shipmentOrderNo,clientId,quotationId,brandCode;
        public Nullable<Int64> brandid;
        private Dictionary<int,string> orderList=new Dictionary<int, string>();
        List< Tuple<int,string,int> > refList=new List<Tuple<int, string, int>>();

        public ReturnRequest()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var x = from entry in orderList
            //    where entry.Value == comboBox1.Text
            //    select entry.Key;
            //OI=x.FirstOrDefault();
           var y = (from entry in refList
                     where entry.Item2 == comboBox1.Text
                     select new { Id = entry.Item1, CLId = entry.Item3 });
            //var z = refList.Where(entry => entry.Item2 == comboBox1.Text)
            //    .Select(entry => new {oi=Convert.ToInt32(entry.Item1),ci=Convert.ToInt32(entry.Item3)});
            OI=y.FirstOrDefault().Id;
            CI = y.FirstOrDefault().CLId;
            Type type = y.GetType();
            //PropertyInfo p = type.GetProperty("Id");

            //object OI = p.GetValue(y, null);
            //object CI = (int)type.GetProperty("CLId").GetValue(y, null);

            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT        ReturnRequest.* FROM  Delivery INNER JOIN  OutTable ON Delivery.DeliveryId = OutTable.DeliveryId INNER JOIN  ReturnRequest ON OutTable.OutId = ReturnRequest.OutId INNER JOIN  SalesClient ON Delivery.SClientId = SalesClient.SClientId WHERE (SalesClient.SClientId ="+CI+" )";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                con.Close();
                string qry2 =
                    "SELECT        MAX(ReturnRequest.SlOfClient) AS Expr1 FROM  ReturnRequest INNER JOIN OutTable ON ReturnRequest.OutId = OutTable.OutId INNER JOIN  Delivery ON OutTable.DeliveryId = Delivery.DeliveryId INNER JOIN  SalesClient ON Delivery.SClientId = SalesClient.SClientId GROUP BY SalesClient.SClientId HAVING (SalesClient.SClientId = " + CI + " )";
                cmd = new SqlCommand(qry2, con);
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    CR = rdr.GetInt32(0) + 1;
                    con.Close();
                }
                else
                {
                    con.Close();
                }
            }
           
            else
            {
                con.Close();
                CR = 1;
            }
            
        }




        private void ReturnRequest_Load(object sender, EventArgs e)
        {
            ComboLoad();


        }

        private void ComboLoad()
        {
            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT OutTable.OutId,Delivery.RefNo, Delivery.SClientId FROM  Delivery INNER JOIN OutTable ON Delivery.DeliveryId = OutTable.DeliveryId where OutTable.OutId not in (SELECT  ReturnRequest.OutId FROM  ReturnRequest INNER JOIN ReturnApproval ON ReturnRequest.RRid = ReturnApproval.RRId)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
                int OutId = rdr.GetInt32(0);
                string reff =rdr.GetString(1);
                int CId = rdr.GetInt32(2);
                Tuple<int, string, int> refTuple = new Tuple<int, string, int>(OutId,reff,CId);
                //orderList.Add(OutId,reff);
                refList.Add(refTuple);
            }
            con.Close();
            foreach (Tuple<int,string,int> x in refList)
            {
                comboBox1.Items.Add(x.Item2);
            }
            //foreach (KeyValuePair<int,string> refPair in orderList)
            //{
            //    comboBox1.Items.Add(refPair.Value);
            //}
        }

        private void ClearselectedProduct()
        {
            impOd = null;
            textBox1.Clear();
        }


        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                    button1.Enabled = false;
                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                    "INSERT INTO ReturnRequest (OutId, EntryDate, CauseOfReturn, UserId,SlOfClient)VALUES        (" + OI + ",@d2,@d1," + frmLogin.uId + ","+CR+")";
                    cmd = new SqlCommand(q1, con);
                    cmd.Parameters.AddWithValue("@d1", textBox1.Text);
                cmd.Parameters.AddWithValue("@d2", DateTime.UtcNow.ToLocalTime());
                    
                    con.Open();
                cmd.ExecuteNonQuery();
                    con.Close();
                    
                    
                    MessageBox.Show("Delivery Order Done");
                    ComboLoad();
                    button1.Enabled = true;


                }
                else
                {
                    MessageBox.Show("Give Return Cause");
                    
                }

            }
            else
            {
                MessageBox.Show("Select Delivery Order");
            }
        }
        }
    }
