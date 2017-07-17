using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
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
        private int Sio,OI;
        public string ShID;
        private string shipmentOrderNo,clientId,quotationId,brandCode;
        public Nullable<Int64> brandid;
        private Dictionary<int,string> orderList=new Dictionary<int, string>();

        public ReturnRequest()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = from entry in orderList
                where entry.Value == comboBox1.Text
                select entry.Key;
            OI=x.FirstOrDefault();
        }




        private void ReturnRequest_Load(object sender, EventArgs e)
        {
            ComboLoad();


        }

        private void ComboLoad()
        {
            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT OutTable.OutId,Delivery.RefNo FROM  Delivery INNER JOIN OutTable ON Delivery.DeliveryId = OutTable.DeliveryId where OutTable.OutId not in (SELECT  ReturnRequest.OutId FROM  ReturnRequest INNER JOIN ReturnApproval ON ReturnRequest.RRid = ReturnApproval.RRId)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int OutId = rdr.GetInt32(0);
                string reff =rdr.GetString(1);
                orderList.Add(OutId,reff);
            }
            con.Close();
            foreach (KeyValuePair<int,string> refPair in orderList)
            {
                comboBox1.Items.Add(refPair.Value);
            }
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
            if (string.IsNullOrEmpty(textBox1.Text))
            {

                    button1.Enabled = false;
                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                    "INSERT INTO ReturnRequest (OutId, EntryDate, CauseOfReturn, UserId)VALUES        ("+OI+","+DateTime.UtcNow.ToLocalTime()+",@d1,"+frmLogin.uId+")";
                    cmd = new SqlCommand(q1, con);
                    cmd.Parameters.AddWithValue("@d1", textBox1.Text);
                    
                    con.Open();
                    ShID = cmd.ExecuteScalar().ToString();
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
