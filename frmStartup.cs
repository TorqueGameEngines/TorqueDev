/*

This file is part of TorqueDev.

TorqueDev is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by the 
Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

TorqueDev is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with TorqueDev.  If not, see <http://www.gnu.org/licenses>

EXCEPTIONS TO THE GPL: TorqueDev links in a number of third party libraries,
which are exempt from the license.  If you want to write closed-source
third party modules that you are going to link into TorqueDev, you may do so
without restriction.  I acknowledge that this technically allows for
one to bypass the open source provisions of the GPL, but the real goal
is to keep the core of TorqueDev free and open.  The rest is up to you.

*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmStartup.
	/// </summary>
	internal class frmStartup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.ListBox lstRecent;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.RadioButton optNewProj;
		private System.Windows.Forms.RadioButton optOpenProj;
		private System.Windows.Forms.RadioButton optRecentProj;
		private ActiproSoftware.WinUICore.OwnerDrawContextMenu ctRecent;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuRemFromList;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Crownwood.DotNetMagic.Controls.TabControl tabControl1;
		private Crownwood.DotNetMagic.Controls.TabPage tabPage1;
		private System.ComponentModel.IContainer components;

		public frmStartup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Owner = g.Main;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmStartup));
			this.optNewProj = new System.Windows.Forms.RadioButton();
			this.optOpenProj = new System.Windows.Forms.RadioButton();
			this.optRecentProj = new System.Windows.Forms.RadioButton();
			this.lstRecent = new System.Windows.Forms.ListBox();
			this.ctRecent = new ActiproSoftware.WinUICore.OwnerDrawContextMenu();
			this.mnuRemFromList = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tabControl1 = new Crownwood.DotNetMagic.Controls.TabControl();
			this.tabPage1 = new Crownwood.DotNetMagic.Controls.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// optNewProj
			// 
			this.optNewProj.Checked = true;
			this.optNewProj.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optNewProj.Location = new System.Drawing.Point(152, 16);
			this.optNewProj.Name = "optNewProj";
			this.optNewProj.Size = new System.Drawing.Size(128, 16);
			this.optNewProj.TabIndex = 1;
			this.optNewProj.TabStop = true;
			this.optNewProj.Text = "Create New Project";
			// 
			// optOpenProj
			// 
			this.optOpenProj.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optOpenProj.Location = new System.Drawing.Point(152, 48);
			this.optOpenProj.Name = "optOpenProj";
			this.optOpenProj.Size = new System.Drawing.Size(136, 16);
			this.optOpenProj.TabIndex = 2;
			this.optOpenProj.Text = "Open Existing Project";
			// 
			// optRecentProj
			// 
			this.optRecentProj.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optRecentProj.Location = new System.Drawing.Point(152, 80);
			this.optRecentProj.Name = "optRecentProj";
			this.optRecentProj.Size = new System.Drawing.Size(136, 16);
			this.optRecentProj.TabIndex = 2;
			this.optRecentProj.Text = "Open Recent Project:";
			// 
			// lstRecent
			// 
			this.lstRecent.ContextMenu = this.ctRecent;
			this.lstRecent.HorizontalScrollbar = true;
			this.lstRecent.Location = new System.Drawing.Point(168, 104);
			this.lstRecent.Name = "lstRecent";
			this.lstRecent.Size = new System.Drawing.Size(256, 121);
			this.lstRecent.TabIndex = 3;
			this.lstRecent.DoubleClick += new System.EventHandler(this.lstRecent_DoubleClick);
			// 
			// ctRecent
			// 
			this.ctRecent.MenuItems.AddRange(new ActiproSoftware.WinUICore.OwnerDrawMenuItem[] {
																					 this.mnuRemFromList});
			this.ctRecent.Popup += new System.EventHandler(this.ctRecent_Popup);
			// 
			// mnuRemFromList
			// 
			this.mnuRemFromList.Index = 0;
			this.mnuRemFromList.OwnerDraw = true;
			this.mnuRemFromList.Text = "&Remove from list";
			this.mnuRemFromList.Click += new System.EventHandler(this.mnuRemFromList_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(352, 392);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(88, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(256, 392);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.Location = new System.Drawing.Point(16, 152);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(102, 17);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "TorqueDev Website";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel2.Location = new System.Drawing.Point(16, 168);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(78, 17);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Garage Games";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(-176, -32);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(640, 140);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 7;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.BackColor = System.Drawing.SystemColors.Control;
			this.tabControl1.ButtonActiveColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.tabControl1.ButtonInactiveColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.tabControl1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.tabControl1.HotTextColor = System.Drawing.SystemColors.ActiveCaption;
			this.tabControl1.ImageList = null;
			this.tabControl1.Location = new System.Drawing.Point(8, 120);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.OfficeDockSides = false;
			this.tabControl1.OfficeStyle = Crownwood.DotNetMagic.Controls.OfficeStyle.SoftWhite;
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowDropSelect = false;
			this.tabControl1.Size = new System.Drawing.Size(440, 264);
			this.tabControl1.Style = Crownwood.DotNetMagic.Common.VisualStyle.IDE2005;
			this.tabControl1.TabIndex = 8;
			this.tabControl1.TabPages.AddRange(new Crownwood.DotNetMagic.Controls.TabPage[] {
																								this.tabPage1});
			this.tabControl1.TextColor = System.Drawing.SystemColors.ControlText;
			this.tabControl1.TextInactiveColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.tabControl1.TextTips = true;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage1.Controls.Add(this.optOpenProj);
			this.tabPage1.Controls.Add(this.optRecentProj);
			this.tabPage1.Controls.Add(this.optNewProj);
			this.tabPage1.Controls.Add(this.lstRecent);
			this.tabPage1.Controls.Add(this.linkLabel1);
			this.tabPage1.Controls.Add(this.linkLabel2);
			this.tabPage1.Controls.Add(this.pictureBox1);
			this.tabPage1.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPage1.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPage1.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPage1.Location = new System.Drawing.Point(1, 1);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPage1.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPage1.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPage1.Size = new System.Drawing.Size(438, 237);
			this.tabPage1.TabIndex = 4;
			this.tabPage1.Title = "Welcome";
			this.tabPage1.ToolTip = "Welcome";
			// 
			// frmStartup
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(458, 431);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmStartup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Welcome";
			this.Load += new System.EventHandler(this.frmStartup_Load);
			this.Closed += new System.EventHandler(this.frmStartup_Closed);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://torque.netmercs.net/");
		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.garagegames.com/");
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmStartup_Load(object sender, System.EventArgs e) {
			
			IDictionaryEnumerator enumer = g.Config.recent_files.GetEnumerator();

			while(enumer.MoveNext())
				lstRecent.Items.Add(enumer.Key.ToString());

			if (lstRecent.Items.Count > 0) {
				lstRecent.SelectedIndex = lstRecent.Items.Count - 1;
				optRecentProj.Checked = true;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			if (this.optNewProj.Checked) {
				this.Hide();
				g.Main.mnuFile_NewProject_Click(null, null);
			} else if (this.optOpenProj.Checked) {
				this.Hide();
				g.Main.mnuFile_OpenProj_Click(null, null);
			} else if (this.optRecentProj.Checked) {
				this.Hide();

				if (lstRecent.SelectedIndex != -1) {
					g.Main.OpenProject(lstRecent.SelectedItem.ToString());
				}
			}

			this.Close();
		}

		private void frmStartup_Closed(object sender, System.EventArgs e) {
			this.Dispose();
		}

		private void lstRecent_DoubleClick(object sender, System.EventArgs e) {
			if (lstRecent.SelectedIndex != -1) {
				this.Hide();
				g.Main.OpenProject(lstRecent.SelectedItem.ToString());
				this.Close();
			}
		}

		private void ctRecent_Popup(object sender, System.EventArgs e) {
			if (lstRecent.SelectedIndex == -1)
				this.mnuRemFromList.Enabled = false;
			else
				this.mnuRemFromList.Enabled = true;
		}

		private void mnuRemFromList_Click(object sender, System.EventArgs e) {
			if (g.Config.recent_files.ContainsKey(lstRecent.SelectedItem)) {
				g.Config.recent_files.Remove(lstRecent.SelectedItem);
				lstRecent.Items.RemoveAt(lstRecent.SelectedIndex);
			}
		}
	}
}
