namespace KyotoSalesManagementSystem.UI
{
    partial class Invoice
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLandPhone = new System.Windows.Forms.TextBox();
            this.lblLandPhone = new System.Windows.Forms.Label();
            this.dtpPromisedDate = new System.Windows.Forms.DateTimePicker();
            this.lblPromisedDate = new System.Windows.Forms.Label();
            this.txtInvoiceParty = new System.Windows.Forms.TextBox();
            this.lblInvoiceParty = new System.Windows.Forms.Label();
            this.txtPayerAddress = new System.Windows.Forms.RichTextBox();
            this.lblPayerAddress = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbQuotation = new System.Windows.Forms.ComboBox();
            this.lblQuotation = new System.Windows.Forms.Label();
            this.txtCellPhone = new System.Windows.Forms.TextBox();
            this.lblCellPhone = new System.Windows.Forms.Label();
            this.txtRP = new System.Windows.Forms.TextBox();
            this.lblRespondentPerson = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiscountPercent = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.txtVATPercent = new System.Windows.Forms.TextBox();
            this.txtAITPercent = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtVATAmount = new System.Windows.Forms.TextBox();
            this.txtAITAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNetPayable = new System.Windows.Forms.TextBox();
            this.VATlabel = new System.Windows.Forms.Label();
            this.AITlabel = new System.Windows.Forms.Label();
            this.Discountlabel = new System.Windows.Forms.Label();
            this.AdditionalDiscountlabel = new System.Windows.Forms.Label();
            this.txtAdditionalDiscount = new System.Windows.Forms.TextBox();
            this.AdvancePaymentlabel = new System.Windows.Forms.Label();
            this.txtAdvancePayment = new System.Windows.Forms.TextBox();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(436, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 31);
            this.label1.TabIndex = 173;
            this.label1.Text = "Invoice Creation Form";
            // 
            // txtLandPhone
            // 
            this.txtLandPhone.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLandPhone.Location = new System.Drawing.Point(244, 273);
            this.txtLandPhone.Name = "txtLandPhone";
            this.txtLandPhone.ReadOnly = true;
            this.txtLandPhone.Size = new System.Drawing.Size(267, 25);
            this.txtLandPhone.TabIndex = 171;
            // 
            // lblLandPhone
            // 
            this.lblLandPhone.AutoSize = true;
            this.lblLandPhone.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLandPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblLandPhone.Location = new System.Drawing.Point(145, 273);
            this.lblLandPhone.Name = "lblLandPhone";
            this.lblLandPhone.Size = new System.Drawing.Size(95, 17);
            this.lblLandPhone.TabIndex = 172;
            this.lblLandPhone.Text = "Land Phone :";
            // 
            // dtpPromisedDate
            // 
            this.dtpPromisedDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPromisedDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPromisedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPromisedDate.Location = new System.Drawing.Point(742, 365);
            this.dtpPromisedDate.Name = "dtpPromisedDate";
            this.dtpPromisedDate.Size = new System.Drawing.Size(252, 25);
            this.dtpPromisedDate.TabIndex = 155;
            this.dtpPromisedDate.ValueChanged += new System.EventHandler(this.dtpPromisedDate_ValueChanged);
            // 
            // lblPromisedDate
            // 
            this.lblPromisedDate.AutoSize = true;
            this.lblPromisedDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromisedDate.ForeColor = System.Drawing.Color.Blue;
            this.lblPromisedDate.Location = new System.Drawing.Point(545, 370);
            this.lblPromisedDate.Name = "lblPromisedDate";
            this.lblPromisedDate.Size = new System.Drawing.Size(194, 17);
            this.lblPromisedDate.TabIndex = 170;
            this.lblPromisedDate.Text = "Promised Date Of Payment :";
            // 
            // txtInvoiceParty
            // 
            this.txtInvoiceParty.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceParty.Location = new System.Drawing.Point(243, 150);
            this.txtInvoiceParty.Name = "txtInvoiceParty";
            this.txtInvoiceParty.ReadOnly = true;
            this.txtInvoiceParty.Size = new System.Drawing.Size(267, 25);
            this.txtInvoiceParty.TabIndex = 167;
            // 
            // lblInvoiceParty
            // 
            this.lblInvoiceParty.AutoSize = true;
            this.lblInvoiceParty.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceParty.ForeColor = System.Drawing.Color.Blue;
            this.lblInvoiceParty.Location = new System.Drawing.Point(52, 152);
            this.lblInvoiceParty.Name = "lblInvoiceParty";
            this.lblInvoiceParty.Size = new System.Drawing.Size(186, 17);
            this.lblInvoiceParty.TabIndex = 168;
            this.lblInvoiceParty.Text = "Invoice Party/Invoiced To :";
            // 
            // txtPayerAddress
            // 
            this.txtPayerAddress.BackColor = System.Drawing.Color.White;
            this.txtPayerAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerAddress.Location = new System.Drawing.Point(242, 186);
            this.txtPayerAddress.Name = "txtPayerAddress";
            this.txtPayerAddress.ReadOnly = true;
            this.txtPayerAddress.Size = new System.Drawing.Size(269, 74);
            this.txtPayerAddress.TabIndex = 162;
            this.txtPayerAddress.Text = "";
            // 
            // lblPayerAddress
            // 
            this.lblPayerAddress.AutoSize = true;
            this.lblPayerAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayerAddress.ForeColor = System.Drawing.Color.Blue;
            this.lblPayerAddress.Location = new System.Drawing.Point(112, 178);
            this.lblPayerAddress.Name = "lblPayerAddress";
            this.lblPayerAddress.Size = new System.Drawing.Size(123, 17);
            this.lblPayerAddress.TabIndex = 163;
            this.lblPayerAddress.Text = "Payer\'s Address :";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Coral;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(839, 425);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 59);
            this.btnSave.TabIndex = 156;
            this.btnSave.Text = "Generate Invoice";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbQuotation
            // 
            this.cmbQuotation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbQuotation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbQuotation.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQuotation.FormattingEnabled = true;
            this.cmbQuotation.Location = new System.Drawing.Point(245, 118);
            this.cmbQuotation.Name = "cmbQuotation";
            this.cmbQuotation.Size = new System.Drawing.Size(264, 25);
            this.cmbQuotation.TabIndex = 150;
            this.cmbQuotation.SelectedIndexChanged += new System.EventHandler(this.cmbQuotation_SelectedIndexChanged);
            this.cmbQuotation.Leave += new System.EventHandler(this.cmbQuotation_Leave);
            // 
            // lblQuotation
            // 
            this.lblQuotation.AutoSize = true;
            this.lblQuotation.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuotation.ForeColor = System.Drawing.Color.Blue;
            this.lblQuotation.Location = new System.Drawing.Point(55, 123);
            this.lblQuotation.Name = "lblQuotation";
            this.lblQuotation.Size = new System.Drawing.Size(184, 17);
            this.lblQuotation.TabIndex = 161;
            this.lblQuotation.Text = "Quotation Id/Ref/Number :";
            // 
            // txtCellPhone
            // 
            this.txtCellPhone.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellPhone.Location = new System.Drawing.Point(243, 350);
            this.txtCellPhone.Name = "txtCellPhone";
            this.txtCellPhone.ReadOnly = true;
            this.txtCellPhone.Size = new System.Drawing.Size(267, 25);
            this.txtCellPhone.TabIndex = 159;
            // 
            // lblCellPhone
            // 
            this.lblCellPhone.AutoSize = true;
            this.lblCellPhone.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblCellPhone.Location = new System.Drawing.Point(151, 350);
            this.lblCellPhone.Name = "lblCellPhone";
            this.lblCellPhone.Size = new System.Drawing.Size(89, 17);
            this.lblCellPhone.TabIndex = 160;
            this.lblCellPhone.Text = "Cell Phone :";
            // 
            // txtRP
            // 
            this.txtRP.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRP.Location = new System.Drawing.Point(244, 314);
            this.txtRP.Name = "txtRP";
            this.txtRP.ReadOnly = true;
            this.txtRP.Size = new System.Drawing.Size(267, 25);
            this.txtRP.TabIndex = 157;
            // 
            // lblRespondentPerson
            // 
            this.lblRespondentPerson.AutoSize = true;
            this.lblRespondentPerson.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRespondentPerson.ForeColor = System.Drawing.Color.Blue;
            this.lblRespondentPerson.Location = new System.Drawing.Point(63, 313);
            this.lblRespondentPerson.Name = "lblRespondentPerson";
            this.lblRespondentPerson.Size = new System.Drawing.Size(176, 17);
            this.lblRespondentPerson.TabIndex = 158;
            this.lblRespondentPerson.Text = "Respondent Person(RP) :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(636, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 180;
            this.label6.Text = "Total Price";
            // 
            // txtDiscountPercent
            // 
            this.txtDiscountPercent.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountPercent.Location = new System.Drawing.Point(741, 212);
            this.txtDiscountPercent.Name = "txtDiscountPercent";
            this.txtDiscountPercent.ReadOnly = true;
            this.txtDiscountPercent.Size = new System.Drawing.Size(73, 25);
            this.txtDiscountPercent.TabIndex = 179;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(812, 215);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 19);
            this.label16.TabIndex = 189;
            this.label16.Text = "%";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalPrice.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrice.ForeColor = System.Drawing.Color.Red;
            this.txtTotalPrice.Location = new System.Drawing.Point(740, 115);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(254, 26);
            this.txtTotalPrice.TabIndex = 181;
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.BackColor = System.Drawing.Color.LightGray;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.ForeColor = System.Drawing.Color.Red;
            this.txtDiscountAmount.Location = new System.Drawing.Point(864, 212);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.ReadOnly = true;
            this.txtDiscountAmount.Size = new System.Drawing.Size(128, 26);
            this.txtDiscountAmount.TabIndex = 188;
            // 
            // txtVATPercent
            // 
            this.txtVATPercent.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVATPercent.Location = new System.Drawing.Point(741, 149);
            this.txtVATPercent.Name = "txtVATPercent";
            this.txtVATPercent.Size = new System.Drawing.Size(73, 26);
            this.txtVATPercent.TabIndex = 175;
            this.txtVATPercent.TextChanged += new System.EventHandler(this.txtVATPercent_TextChanged);
            this.txtVATPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVATPercent_KeyPress);
            // 
            // txtAITPercent
            // 
            this.txtAITPercent.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAITPercent.Location = new System.Drawing.Point(740, 180);
            this.txtAITPercent.Name = "txtAITPercent";
            this.txtAITPercent.Size = new System.Drawing.Size(74, 26);
            this.txtAITPercent.TabIndex = 177;
            this.txtAITPercent.TextChanged += new System.EventHandler(this.txtAITPercent_TextChanged);
            this.txtAITPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAITPercent_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(813, 151);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 19);
            this.label17.TabIndex = 182;
            this.label17.Text = "%";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(812, 183);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 19);
            this.label18.TabIndex = 183;
            this.label18.Text = "%";
            // 
            // txtVATAmount
            // 
            this.txtVATAmount.BackColor = System.Drawing.Color.LightGray;
            this.txtVATAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVATAmount.ForeColor = System.Drawing.Color.Red;
            this.txtVATAmount.Location = new System.Drawing.Point(863, 148);
            this.txtVATAmount.Name = "txtVATAmount";
            this.txtVATAmount.ReadOnly = true;
            this.txtVATAmount.Size = new System.Drawing.Size(129, 26);
            this.txtVATAmount.TabIndex = 184;
            // 
            // txtAITAmount
            // 
            this.txtAITAmount.BackColor = System.Drawing.Color.LightGray;
            this.txtAITAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAITAmount.ForeColor = System.Drawing.Color.Red;
            this.txtAITAmount.Location = new System.Drawing.Point(864, 180);
            this.txtAITAmount.Name = "txtAITAmount";
            this.txtAITAmount.ReadOnly = true;
            this.txtAITAmount.Size = new System.Drawing.Size(128, 26);
            this.txtAITAmount.TabIndex = 185;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(641, 328);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 19);
            this.label19.TabIndex = 186;
            this.label19.Text = "Net Payable";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNetPayable
            // 
            this.txtNetPayable.BackColor = System.Drawing.Color.LightGray;
            this.txtNetPayable.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPayable.ForeColor = System.Drawing.Color.Red;
            this.txtNetPayable.Location = new System.Drawing.Point(741, 322);
            this.txtNetPayable.Name = "txtNetPayable";
            this.txtNetPayable.ReadOnly = true;
            this.txtNetPayable.Size = new System.Drawing.Size(253, 26);
            this.txtNetPayable.TabIndex = 187;
            // 
            // VATlabel
            // 
            this.VATlabel.AutoSize = true;
            this.VATlabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VATlabel.ForeColor = System.Drawing.Color.Blue;
            this.VATlabel.Location = new System.Drawing.Point(692, 154);
            this.VATlabel.Name = "VATlabel";
            this.VATlabel.Size = new System.Drawing.Size(45, 17);
            this.VATlabel.TabIndex = 190;
            this.VATlabel.Text = "VAT :";
            // 
            // AITlabel
            // 
            this.AITlabel.AutoSize = true;
            this.AITlabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AITlabel.ForeColor = System.Drawing.Color.Blue;
            this.AITlabel.Location = new System.Drawing.Point(692, 184);
            this.AITlabel.Name = "AITlabel";
            this.AITlabel.Size = new System.Drawing.Size(44, 17);
            this.AITlabel.TabIndex = 191;
            this.AITlabel.Text = "AIT :";
            // 
            // Discountlabel
            // 
            this.Discountlabel.AutoSize = true;
            this.Discountlabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Discountlabel.ForeColor = System.Drawing.Color.Blue;
            this.Discountlabel.Location = new System.Drawing.Point(662, 217);
            this.Discountlabel.Name = "Discountlabel";
            this.Discountlabel.Size = new System.Drawing.Size(75, 17);
            this.Discountlabel.TabIndex = 192;
            this.Discountlabel.Text = "Discount :";
            // 
            // AdditionalDiscountlabel
            // 
            this.AdditionalDiscountlabel.AutoSize = true;
            this.AdditionalDiscountlabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdditionalDiscountlabel.ForeColor = System.Drawing.Color.Blue;
            this.AdditionalDiscountlabel.Location = new System.Drawing.Point(591, 256);
            this.AdditionalDiscountlabel.Name = "AdditionalDiscountlabel";
            this.AdditionalDiscountlabel.Size = new System.Drawing.Size(146, 17);
            this.AdditionalDiscountlabel.TabIndex = 193;
            this.AdditionalDiscountlabel.Text = "Additional Discount :";
            // 
            // txtAdditionalDiscount
            // 
            this.txtAdditionalDiscount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalDiscount.Location = new System.Drawing.Point(740, 252);
            this.txtAdditionalDiscount.Name = "txtAdditionalDiscount";
            this.txtAdditionalDiscount.Size = new System.Drawing.Size(94, 26);
            this.txtAdditionalDiscount.TabIndex = 194;
            this.txtAdditionalDiscount.TextChanged += new System.EventHandler(this.txtAdditionalDiscount_TextChanged);
            this.txtAdditionalDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdditionalDiscount_KeyPress);
            // 
            // AdvancePaymentlabel
            // 
            this.AdvancePaymentlabel.AutoSize = true;
            this.AdvancePaymentlabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvancePaymentlabel.ForeColor = System.Drawing.Color.Blue;
            this.AdvancePaymentlabel.Location = new System.Drawing.Point(603, 292);
            this.AdvancePaymentlabel.Name = "AdvancePaymentlabel";
            this.AdvancePaymentlabel.Size = new System.Drawing.Size(134, 17);
            this.AdvancePaymentlabel.TabIndex = 195;
            this.AdvancePaymentlabel.Text = "Advance Payment :";
            // 
            // txtAdvancePayment
            // 
            this.txtAdvancePayment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvancePayment.Location = new System.Drawing.Point(740, 287);
            this.txtAdvancePayment.Name = "txtAdvancePayment";
            this.txtAdvancePayment.Size = new System.Drawing.Size(94, 26);
            this.txtAdvancePayment.TabIndex = 196;
            this.txtAdvancePayment.TextChanged += new System.EventHandler(this.txtAdvancePayment_TextChanged);
            this.txtAdvancePayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdvancePayment_KeyPress);
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(242, 425);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(252, 25);
            this.dtpDueDate.TabIndex = 198;
            this.dtpDueDate.ValueChanged += new System.EventHandler(this.dtpDueDate_ValueChanged);
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDueDate.ForeColor = System.Drawing.Color.Blue;
            this.lblDueDate.Location = new System.Drawing.Point(160, 428);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(79, 17);
            this.lblDueDate.TabIndex = 200;
            this.lblDueDate.Text = "Due Date :";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(242, 391);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(252, 25);
            this.dtpInvoiceDate.TabIndex = 197;
            this.dtpInvoiceDate.ValueChanged += new System.EventHandler(this.dtpInvoiceDate_ValueChanged);
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.Blue;
            this.lblInvoiceDate.Location = new System.Drawing.Point(138, 394);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(101, 17);
            this.lblInvoiceDate.TabIndex = 199;
            this.lblInvoiceDate.Text = "Invoice Date :";
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(1076, 604);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.lblDueDate);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.txtAdvancePayment);
            this.Controls.Add(this.AdvancePaymentlabel);
            this.Controls.Add(this.txtAdditionalDiscount);
            this.Controls.Add(this.AdditionalDiscountlabel);
            this.Controls.Add(this.Discountlabel);
            this.Controls.Add(this.AITlabel);
            this.Controls.Add(this.VATlabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDiscountPercent);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.txtDiscountAmount);
            this.Controls.Add(this.txtVATPercent);
            this.Controls.Add(this.txtAITPercent);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtVATAmount);
            this.Controls.Add(this.txtAITAmount);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtNetPayable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLandPhone);
            this.Controls.Add(this.lblLandPhone);
            this.Controls.Add(this.dtpPromisedDate);
            this.Controls.Add(this.lblPromisedDate);
            this.Controls.Add(this.txtInvoiceParty);
            this.Controls.Add(this.lblInvoiceParty);
            this.Controls.Add(this.txtPayerAddress);
            this.Controls.Add(this.lblPayerAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbQuotation);
            this.Controls.Add(this.lblQuotation);
            this.Controls.Add(this.txtCellPhone);
            this.Controls.Add(this.lblCellPhone);
            this.Controls.Add(this.txtRP);
            this.Controls.Add(this.lblRespondentPerson);
            this.Name = "Invoice";
            this.Text = "Invoice";
            this.Load += new System.EventHandler(this.Invoice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLandPhone;
        private System.Windows.Forms.Label lblLandPhone;
        private System.Windows.Forms.DateTimePicker dtpPromisedDate;
        private System.Windows.Forms.Label lblPromisedDate;
        private System.Windows.Forms.TextBox txtInvoiceParty;
        private System.Windows.Forms.Label lblInvoiceParty;
        private System.Windows.Forms.RichTextBox txtPayerAddress;
        private System.Windows.Forms.Label lblPayerAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbQuotation;
        private System.Windows.Forms.Label lblQuotation;
        private System.Windows.Forms.TextBox txtCellPhone;
        private System.Windows.Forms.Label lblCellPhone;
        private System.Windows.Forms.TextBox txtRP;
        private System.Windows.Forms.Label lblRespondentPerson;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiscountPercent;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.TextBox txtVATPercent;
        private System.Windows.Forms.TextBox txtAITPercent;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtVATAmount;
        private System.Windows.Forms.TextBox txtAITAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtNetPayable;
        private System.Windows.Forms.Label VATlabel;
        private System.Windows.Forms.Label AITlabel;
        private System.Windows.Forms.Label Discountlabel;
        private System.Windows.Forms.Label AdditionalDiscountlabel;
        private System.Windows.Forms.TextBox txtAdditionalDiscount;
        private System.Windows.Forms.Label AdvancePaymentlabel;
        private System.Windows.Forms.TextBox txtAdvancePayment;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceDate;
    }
}