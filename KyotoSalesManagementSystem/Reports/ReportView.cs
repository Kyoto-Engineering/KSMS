using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KyotoSalesManagementSystem.Reports
{
    public partial class ReportView : Form
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Visible = !this.label1.Visible;
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }
    }
}
