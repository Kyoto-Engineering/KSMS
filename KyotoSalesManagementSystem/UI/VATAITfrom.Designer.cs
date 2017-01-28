namespace KyotoSalesManagementSystem.UI
{
    partial class VATAITfrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VATAITfrom));
            this.btnAIT = new System.Windows.Forms.Button();
            this.btnVat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAIT
            // 
            this.btnAIT.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAIT.Location = new System.Drawing.Point(191, 71);
            this.btnAIT.Name = "btnAIT";
            this.btnAIT.Size = new System.Drawing.Size(116, 56);
            this.btnAIT.TabIndex = 3;
            this.btnAIT.Text = "AIT";
            this.btnAIT.UseVisualStyleBackColor = true;
            this.btnAIT.Click += new System.EventHandler(this.btnAIT_Click);
            // 
            // btnVat
            // 
            this.btnVat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVat.Location = new System.Drawing.Point(24, 71);
            this.btnVat.Name = "btnVat";
            this.btnVat.Size = new System.Drawing.Size(117, 56);
            this.btnVat.TabIndex = 2;
            this.btnVat.Text = "Vat";
            this.btnVat.UseVisualStyleBackColor = true;
            this.btnVat.Click += new System.EventHandler(this.btnVat_Click);
            // 
            // VATAITfrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 231);
            this.Controls.Add(this.btnAIT);
            this.Controls.Add(this.btnVat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VATAITfrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VATAITfrom";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAIT;
        private System.Windows.Forms.Button btnVat;
    }
}