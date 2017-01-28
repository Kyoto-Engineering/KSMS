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
    public partial class QuotoSUI : Form
    {
        public QuotoSUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            QuotoStatus frm = new QuotoStatus();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PendingQuoto frm = new PendingQuoto();
            frm.Show();
        }

        private void QuotoSUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            MainUI frm = new MainUI();
            frm.Show();
        }

        private void QuotoSUI_Load(object sender, EventArgs e)
        {
            Button button = new Button();
            button.Name = "btn";
            button.Width = 132;
            button.Height = 89;
            button.Location = new Point(262, 85);

            button.Text = "";
            Bitmap bmp = new Bitmap(button.ClientRectangle.Width, button.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(button.BackColor);

                string line1 = "Pending Quotation";
                string line2 = "(To Change Status)";

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font timesnewroman = new System.Drawing.Font("Times New Roman", 12.25F,System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))))
                {
                    Rectangle RC = button.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, timesnewroman, Brushes.Black, RC, SF);
                }

                using (Font courier = new System.Drawing.Font("Times New Roman", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, courier, Brushes.Red, button1.ClientRectangle, SF);
                }
            }
            button.Image = bmp;
            button.ImageAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(button);
            button.Click += new System.EventHandler(this.button2_Click);
        }

    }
}
