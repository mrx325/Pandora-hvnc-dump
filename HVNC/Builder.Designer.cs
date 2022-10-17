
namespace HVNC
{
    partial class Builder
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkBox1.Location = new System.Drawing.Point(62, 102);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(85, 17);
            this.checkBox1.TabIndex = 52;
            this.checkBox1.Text = "Auto Startup";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtGroupName
            // 
            this.txtGroupName.BackColor = System.Drawing.Color.DimGray;
            this.txtGroupName.ForeColor = System.Drawing.Color.Black;
            this.txtGroupName.Location = new System.Drawing.Point(62, 76);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(191, 20);
            this.txtGroupName.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(13, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Group :";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.DimGray;
            this.txtPort.ForeColor = System.Drawing.Color.Black;
            this.txtPort.Location = new System.Drawing.Point(62, 50);
            this.txtPort.Maximum = new decimal(new int[] {
            65555,
            0,
            0,
            0});
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(191, 20);
            this.txtPort.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(22, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Port :";
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.Color.DimGray;
            this.txtIP.ForeColor = System.Drawing.Color.Black;
            this.txtIP.Location = new System.Drawing.Point(62, 24);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(191, 20);
            this.txtIP.TabIndex = 46;
            // 
            // btnBuild
            // 
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuild.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuild.Location = new System.Drawing.Point(62, 125);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(191, 45);
            this.btnBuild.TabIndex = 45;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Host :";
            // 
            // Builder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(279, 194);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Builder";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Builder";
            this.Load += new System.EventHandler(this.Builder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Label label1;
    }
}