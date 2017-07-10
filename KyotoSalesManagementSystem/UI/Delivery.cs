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
    public partial class Delivery : Form
    {
        private SqlCommand cmd;
        ConnectionString Cs=new ConnectionString();
        private SqlConnection con;
        private SqlDataReader rdr;
        private string impOd;
        private DataGridViewRow dr;
        private int checkvalue,smId,available;
        private int SupplierId;
        private int Sio;
        public string ShID;
        private string shipmentOrderNo,clientId,quotationId,brandCode;
        public Nullable<Int64> brandid;

        public Delivery()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReferences();
            GetBrand();
        }

        private void GetBrand()
        {
            con = new SqlConnection(Cs.DBConn);
            con.Open();
            string ct = "SELECT Brand.BrandId FROM  Quotation INNER JOIN RefNumForQuotation ON Quotation.QuotationId = RefNumForQuotation.QuotationId INNER JOIN Brand ON Quotation.BrandId = Brand.BrandId where RefNumForQuotation.ReferenceNo='" + comboBox1.Text + "' ";
            cmd = new SqlCommand(ct);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {

                brandid = Convert.ToInt64(rdr["BrandId"]);
            }
            con.Close();
        }
        private void GetReferences()
        {
            if (comboBox1.SelectedIndex != -1)
            {
                con = new SqlConnection(Cs.DBConn);
                string qry =
                    "SELECT ProductQuotation.PQId, ProductListSummary.ProductGenericDescription, ProductListSummary.ItemCode, ProductListSummary.ItemDescription, ProductQuotation.Quantity, ProductQuotation.BacklogQuantity,ProductQuotation.MOQ,MasterStocks1.MQuantity FROM ProductListSummary INNER JOIN ProductQuotation ON ProductListSummary.Sl = ProductQuotation.Sl INNER JOIN RefNumForQuotation ON ProductQuotation.QuotationId = RefNumForQuotation.QuotationId inner join MasterStocks1 on ProductListSummary.Sl=MasterStocks1.Sl  where ReferenceNo='"+comboBox1.Text+"' And ProductQuotation.BacklogQuantity>0  and MQuantity>0";
                cmd = new SqlCommand(qry, con);
                dataGridView1.Rows.Clear();
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6],rdr[7]);
                }
                con.Close();
                string[] splitter = comboBox1.Text.Split('-');
                brandCode = splitter[0];
                clientId = splitter[1];
                quotationId = splitter[3];
            }
            GetShimpentOredrNo();
        }

        private void GetShimpentOredrNo()
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try
                {
                    con.Open();
                    //string q2 = "Select SQN From RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    //string qr2 = "SELECT MAX(RefNumForQuotation.SQN) FROM RefNumForQuotation where SClientId='" + sClientIdForRefNum + "'";
                    string qr2 = "SELECT  MAX(DS) FROM Delivery WHERE (SClientId = '"+clientId+"')" ;
                    cmd = new SqlCommand(qr2, con);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            Sio = (rdr.GetInt32(0));
                            Sio = Sio + 1;
                            shipmentOrderNo = brandCode + "-" + "D-" + clientId+"-" + Sio;



                        }

                        else
                        {
                            Sio = 1;
                            shipmentOrderNo = brandCode + "-" + "D-" + clientId + "-" + Sio;

                        }
                    }
                    textBox5.Text = shipmentOrderNo;

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            ComboLoad();


        }

        private void ComboLoad()
        {
            con = new SqlConnection(Cs.DBConn);
            string qry =
                "SELECT  RefNumForQuotation.ReferenceNo FROM RefNumForQuotation INNER JOIN  Quotation ON RefNumForQuotation.QuotationId = Quotation.QuotationId WHERE        (Quotation.QStatus = N'Accepted')and QType<>'Custom' And  Quotation.QuotationId  not in (SELECT QuotationId FROM Quantity where orderQty=DeliveredQuantity)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr[0]);
            }
            con.Close();
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               dr = dataGridView1.SelectedRows[0];
                impOd = dr.Cells[0].Value.ToString();
                textBox1.Text = dr.Cells[2].Value.ToString();
                textBox2.Text = dr.Cells[5].Value.ToString();
                textBox4.Text = dr.Cells[1].Value.ToString();
                textBox3.Text = dr.Cells[3].Value.ToString();
                checkvalue =Convert.ToInt32( dr.Cells[5].Value.ToString());
                available = Convert.ToInt32(dr.Cells[7].Value.ToString());
            }
            else
            {
                MessageBox.Show(@"Select Something first");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Select A Product First");
                }
                else if (string.IsNullOrWhiteSpace(textBox2.Text) || Convert.ToInt32(textBox2.Text) < 1)
                {
                    MessageBox.Show("Product With Zero , MInus or Empty Quantity Can Not Be Added");
                }
                else if (Convert.ToInt32(textBox2.Text) > checkvalue)
                {
                    MessageBox.Show("Delivery Quantity Cannot Be greater Than the Backloq Quantity");
                }
                else if (Convert.ToInt32(textBox2.Text) > available)
                {
                    MessageBox.Show("Delivery Quantity Cannot Be greater Than the Available Quantity");
                }
                else
                {


                    if (listView1.Items.Count < 1)
                    {
                        ListViewItem l1 = new ListViewItem();
                        l1.Text = impOd;
                        l1.SubItems.Add(textBox4.Text);
                        l1.SubItems.Add(textBox1.Text);
                        l1.SubItems.Add(textBox3.Text);
                        l1.SubItems.Add(textBox2.Text);
                        listView1.Items.Add(l1);
                        ClearselectedProduct();
                    }
                    else
                    {
                        if (listView1.FindItemWithText(impOd) == null)
                        {
                            ListViewItem l2 = new ListViewItem();
                            l2.Text = impOd;
                            l2.SubItems.Add(textBox4.Text);
                            l2.SubItems.Add(textBox1.Text);
                            l2.SubItems.Add(textBox3.Text);
                            l2.SubItems.Add(textBox2.Text);
                            listView1.Items.Add(l2);
                            ClearselectedProduct();
                        }
                        else
                        {
                            MessageBox.Show("This Prduct already Added");
                            ClearselectedProduct();
                        }
                    }
                
            }
            
        }

        private void ClearselectedProduct()
        {
            impOd = null;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                if (listView1.Items.Count > 0)
                {

                    con = new SqlConnection(Cs.DBConn);
                    string q1 =
                        "INSERT INTO Delivery (QuotationId, SClientId, DS, Deli_Date, UserId, EntryDate,ClientOrderNo)VALUES(@d1,@d2,@d3,@d4,@d5,@d6,@d7)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(q1, con);
                    cmd.Parameters.AddWithValue("@d1", quotationId);
                    cmd.Parameters.AddWithValue("@d2", clientId);
                    cmd.Parameters.AddWithValue("@d3", Sio);
                    
                    cmd.Parameters.AddWithValue("@d4",dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@d5", frmLogin.uId.ToString());
                    cmd.Parameters.AddWithValue("@d6", DateTime.UtcNow.ToLocalTime());
                    cmd.Parameters.Add(new SqlParameter("@d7",
                        string.IsNullOrEmpty(clientsOrderNotextBox.Text) ? (object)DBNull.Value : clientsOrderNotextBox.Text));
                    con.Open();
                     ShID = cmd.ExecuteScalar().ToString();
                    con.Close();
                    string query3 =
                        "UPDATE Delivery SET RefNo = @d1 WHERE(DeliveryId = @d2)";
                    cmd = new SqlCommand(query3, con);
                    cmd.Parameters.AddWithValue("@d1", shipmentOrderNo+"-"+ShID);
                    cmd.Parameters.AddWithValue("@d2", ShID);
                    string debugSQL = cmd.CommandText;

                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        debugSQL = debugSQL.Replace(param.ParameterName, param.Value.ToString());
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        string imprno = listView1.Items[i].Text;
                        string qty = listView1.Items[i].SubItems[4].Text;
                        string query =
                            "INSERT INTO DeliveryProduct (DeliveryId,PQId,DPQty,BacklogQty)Values(@d1,@d2,@d3,@d4)";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@d1", ShID);
                        cmd.Parameters.AddWithValue("@d2", imprno);
                        cmd.Parameters.AddWithValue("@d3", qty);
                        cmd.Parameters.AddWithValue("@d4", qty);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string query2 =
                            "UPDATE ProductQuotation SET BacklogQuantity = BacklogQuantity -@d1 WHERE (PQId = @d2)";
                        cmd = new SqlCommand(query2, con);
                        cmd.Parameters.AddWithValue("@d1", qty);
                        cmd.Parameters.AddWithValue("@d2", imprno);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Delivery Order Done");
                    Report1();
                    Report3();
                    listView1.Items.Clear();
                    dataGridView1.Rows.Clear();
                    textBox5.Clear();
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    ComboLoad();
                    button1.Enabled = true;
                    button3.Enabled = true;

                }
                else
                {
                    MessageBox.Show("No Prouduct Added");
                }

            }
            else
            {
                MessageBox.Show("May be You forgot to add Last Selected Product\r\n Add The Product");
            }
        }

        private void Report1()
        {
            //button1.Enabled = false;
            //backgroundWorker1.RunWorkerAsync();
            //progressBar1.Visible = true;

            //// To report progress from the background worker we need to set this property
            //backgroundWorker1.WorkerReportsProgress = true;
            //// This event will be raised on the worker thread when the worker starts
            //backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //// This event will be raised when we call ReportProgress
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = ShID;

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
                cr = new DeliOrderOmron();
            }
            else if (brandid == 2)
            {
                cr = new DeliOrderWithoutLogo();
            }
            else if (brandid == 3)
            {
                cr = new DeliOrderAzbil();
            }
            else if (brandid == 4)
            {
                cr = new DeliOrderBusinessAuto();
            }
            else if (brandid == 5)
            {
                cr = new DeliOrderIRD();
            }
            else if (brandid == 6)
            {
                cr = new DeliOrderKawashima();
            }
            else if (brandid == 7)
            {
                cr = new DeliOrderChigo();
            }
            else if (brandid == 8)
            {
                cr = new DeliOrderSamsung();
            }
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            BArcode ds = new BArcode();

            var content = shipmentOrderNo + "-" + ShID;
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
            dtr["REF"] = shipmentOrderNo + "-" + ShID;
            dtr["BarcodeImage"] = ms.ToArray();
            ds.Tables[0].Rows.Add(dtr);
            cr.Subreports["BarCode.rpt"].DataSourceConnections.Clear();
            cr.Subreports["BarCode.rpt"].SetDataSource(ds);
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            f2.crystalReportViewer1.ReportSource = cr;

            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
            //backgroundWorker1.CancelAsync();
            //backgroundWorker1.Dispose();
            //progressBar1.Visible = false;
            //button1.Enabled = true;
        }

        private void Report3()
        {
            //button1.Enabled = false;
            //backgroundWorker1.RunWorkerAsync();
            //progressBar1.Visible = true;

            //// To report progress from the background worker we need to set this property
            //backgroundWorker1.WorkerReportsProgress = true;
            //// This event will be raised on the worker thread when the worker starts
            //backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //// This event will be raised when we call ReportProgress
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            ParameterField paramField1 = new ParameterField();


            //creating an object of ParameterFields class
            ParameterFields paramFields1 = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();

            //set the parameter field name
            paramField1.Name = "id";

            //set the parameter value
            paramDiscreteValue1.Value = ShID;

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
                cr = new DeliOAckOmron();
            }
            else if (brandid == 2)
            {
                cr = new DelOAckWithoutLogo();
            }
            else if (brandid == 3)
            {
                cr = new DelOAckAzbil();
            }
            else if (brandid == 4)
            {
                cr = new DelOAckBusinessAutomation();
            }
            else if (brandid == 5)
            {
                cr = new DelOAckIRD();
            }
            else if (brandid == 6)
            {
                cr = new DelOAckKawashima();
            }
            else if (brandid == 7)
            {
                cr = new DelOAckChigo();
            }
            else if (brandid == 8)
            {
                cr = new DelOAckSamsung();
            }
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }

            Sections m_boSections;
            ReportObjects m_boReportObjects;
            SubreportObject m_boSubreportObject;
            //create a new ReportDocument


            //get all the sections in the report
            m_boSections = cr.ReportDefinition.Sections;
            //loop through each section of the report
            foreach (Section m_boSection in m_boSections)
            {
                m_boReportObjects = m_boSection.ReportObjects;
                //loop through each report object
                foreach (ReportObject m_boReportObject in m_boReportObjects)
                {
                    if (m_boReportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        //get the actual subreport object
                        m_boSubreportObject = (SubreportObject)m_boReportObject;

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

            var content = shipmentOrderNo + "-" + ShID;
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
            dtr["REF"] = shipmentOrderNo + "-" + ShID;
            dtr["BarcodeImage"] = ms.ToArray();
            ds.Tables[0].Rows.Add(dtr);
            cr.Subreports["BarCode.rpt"].DataSourceConnections.Clear();
            cr.Subreports["BarCode.rpt"].SetDataSource(ds);
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields1;
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
            //backgroundWorker1.CancelAsync();
            //backgroundWorker1.Dispose();
            //progressBar1.Visible = false;
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
