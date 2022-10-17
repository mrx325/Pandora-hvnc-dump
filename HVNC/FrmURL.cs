using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;

namespace HVNC
{
	public class FrmURL : Form
	{
		public int count;

		private IContainer components;

		private Panel panel1;

		private Label label18;

		private Guna2DragControl guna2DragControl1;

		private Guna2Button StartHiddenURLbtn;

		public Guna2TextBox Urlbox;

		public FrmURL()
		{
			InitializeComponent();
		}

		private void StartHiddenURLbtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Urlbox.Text))
				{
					MessageBox.Show("URL is not valid!");
					return;
				}
				FrmMain.saveurl = Urlbox.Text;
				FrmMain.ispressed = true;
				Close();
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
				Close();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmURL));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.StartHiddenURLbtn = new Guna.UI2.WinForms.Guna2Button();
            this.Urlbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.panel1.Controls.Add(this.label18);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 40);
            this.panel1.TabIndex = 14;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Gainsboro;
            this.label18.Location = new System.Drawing.Point(141, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(141, 20);
            this.label18.TabIndex = 2;
            this.label18.Text = "Visit URL (Hidden)";
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.panel1;
            // 
            // StartHiddenURLbtn
            // 
            this.StartHiddenURLbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartHiddenURLbtn.Animated = true;
            this.StartHiddenURLbtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.StartHiddenURLbtn.BorderThickness = 3;
            this.StartHiddenURLbtn.CheckedState.Parent = this.StartHiddenURLbtn;
            this.StartHiddenURLbtn.CustomImages.Parent = this.StartHiddenURLbtn;
            this.StartHiddenURLbtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.StartHiddenURLbtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.StartHiddenURLbtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.StartHiddenURLbtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.StartHiddenURLbtn.DisabledState.Parent = this.StartHiddenURLbtn;
            this.StartHiddenURLbtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.StartHiddenURLbtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StartHiddenURLbtn.ForeColor = System.Drawing.Color.White;
            this.StartHiddenURLbtn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.StartHiddenURLbtn.HoverState.Parent = this.StartHiddenURLbtn;
            this.StartHiddenURLbtn.Image = ((System.Drawing.Image)(resources.GetObject("StartHiddenURLbtn.Image")));
            this.StartHiddenURLbtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.StartHiddenURLbtn.ImageSize = new System.Drawing.Size(30, 30);
            this.StartHiddenURLbtn.Location = new System.Drawing.Point(50, 109);
            this.StartHiddenURLbtn.Name = "StartHiddenURLbtn";
            this.StartHiddenURLbtn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.StartHiddenURLbtn.ShadowDecoration.Parent = this.StartHiddenURLbtn;
            this.StartHiddenURLbtn.Size = new System.Drawing.Size(313, 39);
            this.StartHiddenURLbtn.TabIndex = 50;
            this.StartHiddenURLbtn.Text = "Start";
            this.StartHiddenURLbtn.Click += new System.EventHandler(this.StartHiddenURLbtn_Click);
            // 
            // Urlbox
            // 
            this.Urlbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Urlbox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.Urlbox.BorderThickness = 3;
            this.Urlbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Urlbox.DefaultText = "https://mail.google.com";
            this.Urlbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Urlbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Urlbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Urlbox.DisabledState.Parent = this.Urlbox;
            this.Urlbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Urlbox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.Urlbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Urlbox.FocusedState.Parent = this.Urlbox;
            this.Urlbox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Urlbox.ForeColor = System.Drawing.Color.Gainsboro;
            this.Urlbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Urlbox.HoverState.Parent = this.Urlbox;
            this.Urlbox.Location = new System.Drawing.Point(50, 56);
            this.Urlbox.Name = "Urlbox";
            this.Urlbox.PasswordChar = '\0';
            this.Urlbox.PlaceholderText = "";
            this.Urlbox.SelectedText = "";
            this.Urlbox.SelectionStart = 23;
            this.Urlbox.ShadowDecoration.Parent = this.Urlbox;
            this.Urlbox.Size = new System.Drawing.Size(313, 38);
            this.Urlbox.TabIndex = 49;
            // 
            // FrmURL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(411, 171);
            this.Controls.Add(this.StartHiddenURLbtn);
            this.Controls.Add(this.Urlbox);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmURL";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visit Url";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
	}
}
