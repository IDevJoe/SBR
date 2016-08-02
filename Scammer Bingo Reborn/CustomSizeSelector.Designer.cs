namespace Scammer_Bingo_Reborn
{
    partial class CustomSizeSelector
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
            this.label2 = new System.Windows.Forms.Label();
            this.numUD_X = new System.Windows.Forms.NumericUpDown();
            this.numUD_Y = new System.Windows.Forms.NumericUpDown();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Y)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Columns:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rows:";
            // 
            // numUD_X
            // 
            this.numUD_X.Location = new System.Drawing.Point(78, 16);
            this.numUD_X.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUD_X.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_X.Name = "numUD_X";
            this.numUD_X.Size = new System.Drawing.Size(57, 20);
            this.numUD_X.TabIndex = 2;
            this.numUD_X.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numUD_Y
            // 
            this.numUD_Y.Location = new System.Drawing.Point(78, 45);
            this.numUD_Y.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUD_Y.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUD_Y.Name = "numUD_Y";
            this.numUD_Y.Size = new System.Drawing.Size(57, 20);
            this.numUD_Y.TabIndex = 3;
            this.numUD_Y.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(13, 80);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(94, 80);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // CustomSizeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 122);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.numUD_Y);
            this.Controls.Add(this.numUD_X);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomSizeSelector";
            this.Text = "Choose button number";
            this.Load += new System.EventHandler(this.CustomSizeSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUD_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUD_X;
        private System.Windows.Forms.NumericUpDown numUD_Y;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
    }
}