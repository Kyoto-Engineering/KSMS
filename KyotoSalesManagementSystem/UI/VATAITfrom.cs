using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KyotoSalesManagementSystem.UI
{
    public partial class VATAITfrom : Form
    {
        public VATAITfrom()
        {
            InitializeComponent();
        }

        private void btnVat_Click(object sender, EventArgs e)
        {
            this.Dispose();
            VatUpdate frm=new VatUpdate();
            frm.Show();
        }

        private void btnAIT_Click(object sender, EventArgs e)
        {
            this.Dispose();
            UpdateAIT frm=new UpdateAIT();
            frm.Show();
        }
    }
}
