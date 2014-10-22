namespace MidtermTakeTwo
{
    partial class PlotSettingsForm
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
            this.tbMinX = new System.Windows.Forms.TextBox();
            this.tbMaxX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMinY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMaxY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min X";
            // 
            // tbMinX
            // 
            this.tbMinX.Location = new System.Drawing.Point(53, 10);
            this.tbMinX.Name = "tbMinX";
            this.tbMinX.Size = new System.Drawing.Size(129, 20);
            this.tbMinX.TabIndex = 1;
            // 
            // tbMaxX
            // 
            this.tbMaxX.Location = new System.Drawing.Point(53, 36);
            this.tbMaxX.Name = "tbMaxX";
            this.tbMaxX.Size = new System.Drawing.Size(129, 20);
            this.tbMaxX.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max X";
            // 
            // tbMinY
            // 
            this.tbMinY.Location = new System.Drawing.Point(53, 62);
            this.tbMinY.Name = "tbMinY";
            this.tbMinY.Size = new System.Drawing.Size(129, 20);
            this.tbMinY.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Min Y";
            // 
            // tbMaxY
            // 
            this.tbMaxY.Location = new System.Drawing.Point(53, 88);
            this.tbMaxY.Name = "tbMaxY";
            this.tbMaxY.Size = new System.Drawing.Size(129, 20);
            this.tbMaxY.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max Y";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 115);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(166, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PlotSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 151);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbMaxY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMinY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMaxX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMinX);
            this.Controls.Add(this.label1);
            this.Name = "PlotSettingsForm";
            this.Text = "PlotSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMinX;
        private System.Windows.Forms.TextBox tbMaxX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMinY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMaxY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
    }
}