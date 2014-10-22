namespace SoapConsumer
{
    partial class SoapConsumerForm
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblWebService = new System.Windows.Forms.Label();
            this.cmbBoxWebService = new System.Windows.Forms.ComboBox();
            this.cmbBoxWebMethod = new System.Windows.Forms.ComboBox();
            this.lblWebMethod = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timerColorChanger = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 221);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(627, 164);
            this.textBox1.TabIndex = 0;
            // 
            // lblWebService
            // 
            this.lblWebService.AutoSize = true;
            this.lblWebService.Location = new System.Drawing.Point(13, 25);
            this.lblWebService.Name = "lblWebService";
            this.lblWebService.Size = new System.Drawing.Size(69, 13);
            this.lblWebService.TabIndex = 1;
            this.lblWebService.Text = "Web Service";
            // 
            // cmbBoxWebService
            // 
            this.cmbBoxWebService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxWebService.FormattingEnabled = true;
            this.cmbBoxWebService.Items.AddRange(new object[] {
            "NumberConversion"});
            this.cmbBoxWebService.Location = new System.Drawing.Point(88, 22);
            this.cmbBoxWebService.Name = "cmbBoxWebService";
            this.cmbBoxWebService.Size = new System.Drawing.Size(207, 21);
            this.cmbBoxWebService.TabIndex = 2;
            this.cmbBoxWebService.SelectedIndexChanged += new System.EventHandler(this.cmbBoxWebService_SelectedIndexChanged);
            // 
            // cmbBoxWebMethod
            // 
            this.cmbBoxWebMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxWebMethod.FormattingEnabled = true;
            this.cmbBoxWebMethod.Items.AddRange(new object[] {
            "NumberToWords"});
            this.cmbBoxWebMethod.Location = new System.Drawing.Point(401, 22);
            this.cmbBoxWebMethod.Name = "cmbBoxWebMethod";
            this.cmbBoxWebMethod.Size = new System.Drawing.Size(239, 21);
            this.cmbBoxWebMethod.TabIndex = 5;
            this.cmbBoxWebMethod.SelectedIndexChanged += new System.EventHandler(this.cmbBoxWebMethod_SelectedIndexChanged);
            // 
            // lblWebMethod
            // 
            this.lblWebMethod.AutoSize = true;
            this.lblWebMethod.Location = new System.Drawing.Point(326, 25);
            this.lblWebMethod.Name = "lblWebMethod";
            this.lblWebMethod.Size = new System.Drawing.Size(69, 13);
            this.lblWebMethod.TabIndex = 4;
            this.lblWebMethod.Text = "Web Method";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(15, 202);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(39, 13);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "Output";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(329, 95);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(89, 23);
            this.btnSendRequest.TabIndex = 8;
            this.btnSendRequest.Text = "Send Request";
            this.btnSendRequest.UseVisualStyleBackColor = true;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 95);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(307, 100);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // timerColorChanger
            // 
            this.timerColorChanger.Enabled = true;
            this.timerColorChanger.Interval = 1000;
            this.timerColorChanger.Tick += new System.EventHandler(this.timerColorChanger_Tick);
            // 
            // SoapConsumerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(652, 397);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnSendRequest);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.cmbBoxWebMethod);
            this.Controls.Add(this.lblWebMethod);
            this.Controls.Add(this.cmbBoxWebService);
            this.Controls.Add(this.lblWebService);
            this.Controls.Add(this.textBox1);
            this.Name = "SoapConsumerForm";
            this.Text = "Soap Consumer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblWebService;
        private System.Windows.Forms.ComboBox cmbBoxWebService;
        private System.Windows.Forms.ComboBox cmbBoxWebMethod;
        private System.Windows.Forms.Label lblWebMethod;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timerColorChanger;
    }
}

