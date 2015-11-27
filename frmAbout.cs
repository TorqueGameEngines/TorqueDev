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
using System.Net;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	internal class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblVerString;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ToolTip toolTip1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCommand1;
		private System.Windows.Forms.Label lblMemUtil;
		private Crownwood.DotNetMagic.Controls.TabControl tabControl1;
		private Crownwood.DotNetMagic.Controls.TabPage tabPage1;
		private Crownwood.DotNetMagic.Controls.TabPage tabPage2;
		private Crownwood.DotNetMagic.Controls.TabPage tabPage3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private Crownwood.DotNetMagic.Controls.TabPage tabPage4;
		private System.Windows.Forms.Label lblMiscMsg;
		private System.Windows.Forms.PictureBox pictureBox2;
		private GroupBox groupBox1;
		private Label lblLicType;
		private Label lblLicName;
		private Label lblLicLimit;
		private ActiproSoftware.MarkupLabel.MarkupLabel markupLabel1;
		private Label lblLicSerial;
		private Label lblLicCompany;
		private Label lblLicExp;
		private ListView lvPlugins;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private Label label4;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private System.ComponentModel.IContainer components;

		public frmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Owner = g.Main;

			// Load the right header image
			if (g.License["MANAGED"] == true)
				pictureBox2.Image = global::TSDev.Properties.Resources.cw_about_mng;
			else if (g.License["PRO"] == true)
				pictureBox2.Image = global::TSDev.Properties.Resources.cw_about_pro;
			else if (g.License["DONATOR"] == true || g.License["FREE"] == true)
				pictureBox2.Image = global::TSDev.Properties.Resources.cw_about_std;

			// Load up the license information
			lblLicName.Text = g.License.LicensedUser;
			lblLicCompany.Text = g.License.LicensedCompany;
			lblLicExp.Text = ((g.License.LicenseExpires.Year > 1) ? ("License expires on " + g.License.LicenseExpires.ToLongDateString()): "License Does Not Expire");
			lblLicLimit.Text = ((g.License.LicenseLimit == "Unlimited") ? "Site License" : ("Licensed for " + g.License.LicenseLimit + " Computer(s)"));
			lblLicSerial.Text = g.License.LicenseSerial;

			lblLicExp.Visible = g.License.LicenseExpires.Year > 1;

			// Check the license type
			switch (g.License.LicenseSerial.Substring(0, 3)) {
				case "CWF":
					lblLicType.Text = "Standard License";
					break;
				case "CWP":
					lblLicType.Text = "Professional License";
					break;
				case "CPS":
					lblLicType.Text = "Professional Site License";
					break;
				case "CWM":
					lblLicType.Text = "Professional Managed License";
					break;
				case "CMS":
					lblLicType.Text = "Professional Managed Site License";
					break;
				case "CWD":
					lblLicType.Text = "Donator Site License";
					break;
				default:
					lblLicType.Text = "Special License";
					break;
			}

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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblVerString = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.cmdClose = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabPage4 = new Crownwood.DotNetMagic.Controls.TabPage();
			this.lvPlugins = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label4 = new System.Windows.Forms.Label();
			this.menuCommand1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.lblMemUtil = new System.Windows.Forms.Label();
			this.tabControl1 = new Crownwood.DotNetMagic.Controls.TabControl();
			this.tabPage1 = new Crownwood.DotNetMagic.Controls.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblLicLimit = new System.Windows.Forms.Label();
			this.lblLicType = new System.Windows.Forms.Label();
			this.lblLicExp = new System.Windows.Forms.Label();
			this.lblLicSerial = new System.Windows.Forms.Label();
			this.lblLicCompany = new System.Windows.Forms.Label();
			this.lblLicName = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.tabPage2 = new Crownwood.DotNetMagic.Controls.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabPage3 = new Crownwood.DotNetMagic.Controls.TabPage();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.markupLabel1 = new ActiproSoftware.MarkupLabel.MarkupLabel();
			this.lblMiscMsg = new System.Windows.Forms.Label();
			this.tabPage4.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(80, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(165, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "TorqueDev Development IDE";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(80, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(228, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Copyright (c) 2005-2009 netMercs Group LLC";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(80, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 14);
			this.label3.TabIndex = 1;
			this.label3.Text = "All rights reserved.";
			// 
			// lblVerString
			// 
			this.lblVerString.AutoSize = true;
			this.lblVerString.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVerString.Location = new System.Drawing.Point(8, 320);
			this.lblVerString.Name = "lblVerString";
			this.lblVerString.Size = new System.Drawing.Size(16, 14);
			this.lblVerString.TabIndex = 3;
			this.lblVerString.Text = "%";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.Location = new System.Drawing.Point(8, 288);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(102, 13);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "TorqueDev Website";
			this.toolTip1.SetToolTip(this.linkLabel1, "http://www.torquedev.com/ - Official TorqueDev Website");
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel2.Location = new System.Drawing.Point(315, 288);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(77, 13);
			this.linkLabel2.TabIndex = 4;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Garage Games";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.linkLabel2, "http://www.garagegames.com/ - Offical site of the creators of the Torque engine");
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(327, 382);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(80, 24);
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage4.Controls.Add(this.lvPlugins);
			this.tabPage4.Controls.Add(this.label4);
			this.tabPage4.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPage4.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPage4.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPage4.Location = new System.Drawing.Point(1, 1);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPage4.Selected = false;
			this.tabPage4.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPage4.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPage4.Size = new System.Drawing.Size(398, 341);
			this.tabPage4.TabIndex = 7;
			this.tabPage4.Title = "Loaded Plugins";
			this.tabPage4.ToolTip = "Page";
			this.toolTip1.SetToolTip(this.tabPage4, "Loaded Plugins");
			// 
			// lvPlugins
			// 
			this.lvPlugins.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader5,
            this.columnHeader2,
            this.columnHeader6,
            this.columnHeader3,
            this.columnHeader4});
			this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPlugins.FullRowSelect = true;
			this.lvPlugins.Location = new System.Drawing.Point(0, 0);
			this.lvPlugins.Name = "lvPlugins";
			this.lvPlugins.Size = new System.Drawing.Size(398, 328);
			this.lvPlugins.TabIndex = 0;
			this.lvPlugins.UseCompatibleStateImageBehavior = false;
			this.lvPlugins.View = System.Windows.Forms.View.Details;
			this.lvPlugins.ItemActivate += new System.EventHandler(this.lvPlugins_ItemActivate);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 109;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Version";
			this.columnHeader5.Width = 91;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Author";
			this.columnHeader2.Width = 96;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Description";
			this.columnHeader6.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Copyright";
			this.columnHeader3.Width = 126;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Guid";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(0, 328);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(398, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Double-click a plugin for more information";
			// 
			// menuCommand1
			// 
			this.menuCommand1.Description = "Menu";
			this.menuCommand1.Text = "Menu";
			// 
			// lblMemUtil
			// 
			this.lblMemUtil.Location = new System.Drawing.Point(232, 320);
			this.lblMemUtil.Name = "lblMemUtil";
			this.lblMemUtil.Size = new System.Drawing.Size(160, 16);
			this.lblMemUtil.TabIndex = 7;
			this.lblMemUtil.Text = "%";
			this.lblMemUtil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMemUtil.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblMemUtil_MouseDown);
			// 
			// tabControl1
			// 
			this.tabControl1.BackColor = System.Drawing.SystemColors.Control;
			this.tabControl1.ButtonActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabControl1.ButtonInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabControl1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.tabControl1.HotTextColor = System.Drawing.SystemColors.ActiveCaption;
			this.tabControl1.ImageList = null;
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.OfficeDockSides = false;
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowDropSelect = false;
			this.tabControl1.Size = new System.Drawing.Size(400, 368);
			this.tabControl1.Style = Crownwood.DotNetMagic.Common.VisualStyle.IDE2005;
			this.tabControl1.TabIndex = 8;
			this.tabControl1.TabPages.AddRange(new Crownwood.DotNetMagic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage4,
            this.tabPage2,
            this.tabPage3});
			this.tabControl1.TextColor = System.Drawing.SystemColors.ControlText;
			this.tabControl1.TextInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabControl1.TextTips = true;
			this.tabControl1.SelectionChanged += new Crownwood.DotNetMagic.Controls.SelectTabHandler(this.tabControl1_SelectionChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.lblVerString);
			this.tabPage1.Controls.Add(this.lblMemUtil);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.linkLabel2);
			this.tabPage1.Controls.Add(this.linkLabel1);
			this.tabPage1.Controls.Add(this.pictureBox1);
			this.tabPage1.Controls.Add(this.pictureBox2);
			this.tabPage1.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPage1.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPage1.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPage1.Location = new System.Drawing.Point(1, 1);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPage1.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPage1.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPage1.Size = new System.Drawing.Size(398, 341);
			this.tabPage1.TabIndex = 4;
			this.tabPage1.Title = "About";
			this.tabPage1.ToolTip = "About";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblLicLimit);
			this.groupBox1.Controls.Add(this.lblLicType);
			this.groupBox1.Controls.Add(this.lblLicExp);
			this.groupBox1.Controls.Add(this.lblLicSerial);
			this.groupBox1.Controls.Add(this.lblLicCompany);
			this.groupBox1.Controls.Add(this.lblLicName);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(11, 143);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(374, 131);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Licensed To";
			// 
			// lblLicLimit
			// 
			this.lblLicLimit.AutoSize = true;
			this.lblLicLimit.Location = new System.Drawing.Point(6, 112);
			this.lblLicLimit.Name = "lblLicLimit";
			this.lblLicLimit.Size = new System.Drawing.Size(126, 14);
			this.lblLicLimit.TabIndex = 2;
			this.lblLicLimit.Text = "Licensed for 1 Computer";
			// 
			// lblLicType
			// 
			this.lblLicType.Location = new System.Drawing.Point(155, 113);
			this.lblLicType.Name = "lblLicType";
			this.lblLicType.Size = new System.Drawing.Size(213, 13);
			this.lblLicType.TabIndex = 1;
			this.lblLicType.Text = "Standard License";
			this.lblLicType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLicExp
			// 
			this.lblLicExp.Location = new System.Drawing.Point(9, 92);
			this.lblLicExp.Name = "lblLicExp";
			this.lblLicExp.Size = new System.Drawing.Size(359, 13);
			this.lblLicExp.TabIndex = 0;
			this.lblLicExp.Text = "License Does Not Expire";
			this.lblLicExp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLicSerial
			// 
			this.lblLicSerial.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblLicSerial.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLicSerial.Location = new System.Drawing.Point(6, 61);
			this.lblLicSerial.Name = "lblLicSerial";
			this.lblLicSerial.Size = new System.Drawing.Size(362, 21);
			this.lblLicSerial.TabIndex = 0;
			this.lblLicSerial.Text = "CWF-134444-00UL";
			this.lblLicSerial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLicCompany
			// 
			this.lblLicCompany.Location = new System.Drawing.Point(10, 32);
			this.lblLicCompany.Name = "lblLicCompany";
			this.lblLicCompany.Size = new System.Drawing.Size(359, 13);
			this.lblLicCompany.TabIndex = 0;
			this.lblLicCompany.Text = "Company";
			this.lblLicCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLicName
			// 
			this.lblLicName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLicName.Location = new System.Drawing.Point(9, 17);
			this.lblLicName.Name = "lblLicName";
			this.lblLicName.Size = new System.Drawing.Size(359, 16);
			this.lblLicName.TabIndex = 0;
			this.lblLicName.Text = "User";
			this.lblLicName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(24, 72);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::TSDev.Properties.Resources.cw_about_std;
			this.pictureBox2.Location = new System.Drawing.Point(-1, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(408, 58);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 10;
			this.pictureBox2.TabStop = false;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage2.Controls.Add(this.textBox1);
			this.tabPage2.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPage2.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPage2.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPage2.Location = new System.Drawing.Point(1, 1);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPage2.Selected = false;
			this.tabPage2.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPage2.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPage2.Size = new System.Drawing.Size(398, 341);
			this.tabPage2.TabIndex = 5;
			this.tabPage2.Title = "Contributors";
			this.tabPage2.ToolTip = "Contributors";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(398, 341);
			this.textBox1.TabIndex = 0;
			this.textBox1.TabStop = false;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage3.Controls.Add(this.textBox2);
			this.tabPage3.Controls.Add(this.markupLabel1);
			this.tabPage3.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPage3.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPage3.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPage3.Location = new System.Drawing.Point(1, 1);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPage3.Selected = false;
			this.tabPage3.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPage3.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPage3.Size = new System.Drawing.Size(398, 341);
			this.tabPage3.TabIndex = 6;
			this.tabPage3.Title = "Thanks To";
			this.tabPage3.ToolTip = "Thanks To";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox2.Location = new System.Drawing.Point(0, 60);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(398, 281);
			this.textBox2.TabIndex = 1;
			this.textBox2.TabStop = false;
			this.textBox2.Text = resources.GetString("textBox2.Text");
			this.textBox2.Enter += new System.EventHandler(this.textBox1_Enter);
			// 
			// markupLabel1
			// 
			this.markupLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.markupLabel1.Location = new System.Drawing.Point(0, 0);
			this.markupLabel1.MaxWidth = 2147483647;
			this.markupLabel1.Name = "markupLabel1";
			this.markupLabel1.Size = new System.Drawing.Size(398, 60);
			this.markupLabel1.TabIndex = 14;
			this.markupLabel1.Text = resources.GetString("markupLabel1.Text");
			this.markupLabel1.LinkClick += new ActiproSoftware.MarkupLabel.MarkupLabelLinkClickEventHandler(this.markupLabel1_LinkClick);
			// 
			// lblMiscMsg
			// 
			this.lblMiscMsg.AutoSize = true;
			this.lblMiscMsg.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMiscMsg.Location = new System.Drawing.Point(8, 392);
			this.lblMiscMsg.Name = "lblMiscMsg";
			this.lblMiscMsg.Size = new System.Drawing.Size(16, 14);
			this.lblMiscMsg.TabIndex = 9;
			this.lblMiscMsg.Text = "%";
			this.lblMiscMsg.Visible = false;
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(417, 415);
			this.Controls.Add(this.lblMiscMsg);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cmdClose);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About TorqueDev";
			this.Closed += new System.EventHandler(this.frmAbout_Closed);
			this.Load += new System.EventHandler(this.frmAbout_Load);
			this.tabPage4.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void LoadPluginInfo() {
			lvPlugins.Items.Clear();
			lvPlugins.BeginUpdate();

			foreach (CWPlugin plugin in g.Plugins) {
				ListViewItem lvi = new ListViewItem(plugin.Name);
				lvi.SubItems.Add(plugin.Version.ToString());
				lvi.SubItems.Add(plugin.Author);
				lvi.SubItems.Add(plugin.Description);
				lvi.SubItems.Add(plugin.Copyright);
				lvi.SubItems.Add(plugin.Guid.ToString());

				lvi.Tag = plugin;

				lvPlugins.Items.Add(lvi);
			}

			lvPlugins.EndUpdate();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/");
		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.garagegames.com/");
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmAbout_Closed(object sender, System.EventArgs e) {
			this.Dispose();
		}

		private void frmAbout_Load(object sender, System.EventArgs e) {
			// List the activation information
			
			lblVerString.Text = Application.ProductVersion + " (Final)";
			this.lblMemUtil.Text = "Memory Usage: " + Convert.ToString(GC.GetTotalMemory(true) / 1048576) + "MB";
			cmdClose.Focus();

			this.tabPage1.Selected = true;
			LoadPluginInfo();
		}

		private void textBox1_Enter(object sender, System.EventArgs e) {
			this.cmdClose.Focus();
		}

		private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (e.Button == MouseButtons.Middle) {
				WebClient client = new WebClient();
				client.QueryString.Add("cliver", Application.ProductVersion);
					
				string motd = System.Text.ASCIIEncoding.ASCII.GetString(client.DownloadData("http://www.torquedev.com/_client/motd.php"));
				g.Config.LastMotd = motd;
			}
		}


		private void tabControl1_SelectionChanged(Crownwood.DotNetMagic.Controls.TabControl sender, Crownwood.DotNetMagic.Controls.TabPage oldPage, Crownwood.DotNetMagic.Controls.TabPage newPage) {
		
		}

		private void markupLabel1_LinkClick(object sender, ActiproSoftware.MarkupLabel.MarkupLabelLinkClickEventArgs e) {
			System.Diagnostics.Process.Start(e.Element.HRef);
		}

		private void lblMemUtil_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (e.Button == MouseButtons.Middle)
				throw new Exception("ThrowNewException -- do not report this; this is triggered by testing the exceptions system.");
		}

		private void lvPlugins_ItemActivate(object sender, EventArgs e) {
			if (lvPlugins.SelectedItems.Count == 0)
				return;

			// Show plugin about dialog
			try {
				(lvPlugins.SelectedItems[0].Tag as CWPlugin).Plugin.CWAboutDialog();
			} catch (Exception exc) {
				g.PluginException(exc, (CWPlugin)lvPlugins.SelectedItems[0].Tag);
			}
		}
	}
}
