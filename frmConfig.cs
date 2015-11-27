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
using TSDev.Plugins;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmConfig.
	/// </summary>
	internal class frmConfig : System.Windows.Forms.Form
	{
		private class ShortcutConfig {
			public ShortcutConfig(Keys keys, Keys modifiers) {
				this.keys = keys;
				this.modifiers = modifiers;
			}

			public Keys keys = Keys.None;
			public Keys modifiers = Keys.None;

			public override string ToString() {
				// Convert the thing to a string
				string output = "";

				if ((modifiers & Keys.Control) == Keys.Control)
					output += "Ctrl+";
				if ((modifiers & Keys.Alt) == Keys.Alt)
					output += "Alt+";
				if ((modifiers & Keys.Shift) == Keys.Shift)
					output += "Shift+";

				output += keys.ToString();

				return output;
			}

			public Keys ToKeys() {
				// Convert the thing to keys
				return keys | modifiers;
			}
		}

		private CConfig loconf;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label2;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor txtSynEd;
		private System.Windows.Forms.ListBox lstColorDefs;
		private System.Windows.Forms.Button cmdAdColor;
		private System.Windows.Forms.CheckBox chkScrollHint;
		private System.Windows.Forms.CheckBox chkAutoColl;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		public Crownwood.DotNetMagic.Controls.TabControl tabMain;
		private Crownwood.DotNetMagic.Controls.TabPage tpHighlighting;
		private System.Windows.Forms.CheckBox chkCheckUpdates;
		private System.Windows.Forms.CheckBox chkDebugSummary;
		private System.Windows.Forms.GroupBox groupBox2;
		private Crownwood.DotNetMagic.Controls.TabPage tpGeneral;
		private Crownwood.DotNetMagic.Controls.TabPage tpShortcut;
		private Crownwood.DotNetMagic.Controls.TabPage tpEditor;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lvShortcuts;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkAC_Infopop;
		private System.Windows.Forms.CheckBox chkAC_Obj;
		private System.Windows.Forms.CheckBox chkAC_Var;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkConvTabToSpc;
		private System.Windows.Forms.TextBox txtTabSpace;
		private System.Windows.Forms.CheckBox chk_Ed_Virtlines;
		private System.Windows.Forms.CheckBox chk_Ed_ShowLines;
		private System.Windows.Forms.CheckBox chk_Ed_ShowWhitespace;
		private System.Windows.Forms.CheckBox chk_Ed_ShowNewline;
		private System.Windows.Forms.CheckBox chk_Ed_ShowBrackets;
		private System.Windows.Forms.CheckBox chk_Ed_Wordwrap;
		private System.Windows.Forms.CheckBox chk_Ed_ShowTabs;
		private System.Windows.Forms.Button cmdClearRecent;
		private System.Windows.Forms.Button cmdFlushAuth;
		private System.Windows.Forms.Button cmdOnDebugExec;
		private System.Windows.Forms.CheckBox chk_Ed_Errs;
		private System.Windows.Forms.CheckBox chkErrChkDbg;
		private System.Windows.Forms.CheckBox chk_Ed_AutoIndent;
		private RadioButton optRenderStd;
		private RadioButton optRenderPro;
		private Label label7;
		private TextBox txtAssgnShortcut;
		private Button cmdApplyShortcut;
		private CheckBox chk_Ed_CodeFold;
		private ToolTip toolTip1;
		private Button cmdResetColors;
		private CheckBox chk_E_IndentGuides;
		private ContextMenuStrip ctxReset;
		private ToolStripMenuItem ctxReset_DefaultClr;
		private ToolStripMenuItem ctxReset_NoolnessDarkClr;
		private CheckBox chkAC_TypeAsYouGo;
		private TreeView tvMenus;
		private ImageList imageList1;
		private IContainer components;

		public frmConfig(CConfig prevconf)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.loconf = (CConfig)prevconf.Clone();

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			this.tabMain = new Crownwood.DotNetMagic.Controls.TabControl();
			this.tpGeneral = new Crownwood.DotNetMagic.Controls.TabPage();
			this.optRenderStd = new System.Windows.Forms.RadioButton();
			this.optRenderPro = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.cmdClearRecent = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkAC_Infopop = new System.Windows.Forms.CheckBox();
			this.chkAC_Obj = new System.Windows.Forms.CheckBox();
			this.chkAC_TypeAsYouGo = new System.Windows.Forms.CheckBox();
			this.chkAC_Var = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdOnDebugExec = new System.Windows.Forms.Button();
			this.chkDebugSummary = new System.Windows.Forms.CheckBox();
			this.chkErrChkDbg = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
			this.tpHighlighting = new Crownwood.DotNetMagic.Controls.TabPage();
			this.cmdResetColors = new System.Windows.Forms.Button();
			this.cmdAdColor = new System.Windows.Forms.Button();
			this.lstColorDefs = new System.Windows.Forms.ListBox();
			this.txtSynEd = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tpShortcut = new Crownwood.DotNetMagic.Controls.TabPage();
			this.tvMenus = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.cmdApplyShortcut = new System.Windows.Forms.Button();
			this.txtAssgnShortcut = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lvShortcuts = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.tpEditor = new Crownwood.DotNetMagic.Controls.TabPage();
			this.chkConvTabToSpc = new System.Windows.Forms.CheckBox();
			this.txtTabSpace = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.chk_Ed_Virtlines = new System.Windows.Forms.CheckBox();
			this.chk_Ed_ShowLines = new System.Windows.Forms.CheckBox();
			this.chk_Ed_ShowWhitespace = new System.Windows.Forms.CheckBox();
			this.chk_Ed_ShowNewline = new System.Windows.Forms.CheckBox();
			this.chk_Ed_ShowBrackets = new System.Windows.Forms.CheckBox();
			this.chk_Ed_Wordwrap = new System.Windows.Forms.CheckBox();
			this.chk_Ed_ShowTabs = new System.Windows.Forms.CheckBox();
			this.chk_Ed_Errs = new System.Windows.Forms.CheckBox();
			this.chkScrollHint = new System.Windows.Forms.CheckBox();
			this.chkAutoColl = new System.Windows.Forms.CheckBox();
			this.chk_E_IndentGuides = new System.Windows.Forms.CheckBox();
			this.chk_Ed_CodeFold = new System.Windows.Forms.CheckBox();
			this.chk_Ed_AutoIndent = new System.Windows.Forms.CheckBox();
			this.cmdFlushAuth = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.ctxReset = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctxReset_DefaultClr = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxReset_NoolnessDarkClr = new System.Windows.Forms.ToolStripMenuItem();
			this.tabMain.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tpHighlighting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.tpShortcut.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.tpEditor.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.ctxReset.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabMain
			// 
			this.tabMain.BackColor = System.Drawing.SystemColors.Control;
			this.tabMain.ButtonActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabMain.ButtonInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabMain.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.tabMain.HotTextColor = System.Drawing.SystemColors.ActiveCaption;
			this.tabMain.ImageList = null;
			this.tabMain.Location = new System.Drawing.Point(8, 8);
			this.tabMain.Name = "tabMain";
			this.tabMain.OfficeDockSides = false;
			this.tabMain.SelectedIndex = 0;
			this.tabMain.ShowArrows = true;
			this.tabMain.ShowDropSelect = false;
			this.tabMain.ShrinkPagesToFit = false;
			this.tabMain.Size = new System.Drawing.Size(376, 384);
			this.tabMain.Style = Crownwood.DotNetMagic.Common.VisualStyle.IDE2005;
			this.tabMain.TabIndex = 1;
			this.tabMain.TabPages.AddRange(new Crownwood.DotNetMagic.Controls.TabPage[] {
            this.tpGeneral,
            this.tpHighlighting,
            this.tpShortcut,
            this.tpEditor});
			this.tabMain.TextColor = System.Drawing.SystemColors.ControlText;
			this.tabMain.TextInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.tabMain.TextTips = true;
			// 
			// tpGeneral
			// 
			this.tpGeneral.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tpGeneral.Controls.Add(this.optRenderStd);
			this.tpGeneral.Controls.Add(this.optRenderPro);
			this.tpGeneral.Controls.Add(this.label7);
			this.tpGeneral.Controls.Add(this.cmdClearRecent);
			this.tpGeneral.Controls.Add(this.groupBox3);
			this.tpGeneral.Controls.Add(this.groupBox2);
			this.tpGeneral.Controls.Add(this.label1);
			this.tpGeneral.Controls.Add(this.pictureBox1);
			this.tpGeneral.Controls.Add(this.chkCheckUpdates);
			this.tpGeneral.InactiveBackColor = System.Drawing.Color.Empty;
			this.tpGeneral.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tpGeneral.InactiveTextColor = System.Drawing.Color.Empty;
			this.tpGeneral.Location = new System.Drawing.Point(1, 1);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.SelectBackColor = System.Drawing.Color.Empty;
			this.tpGeneral.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tpGeneral.SelectTextColor = System.Drawing.Color.Empty;
			this.tpGeneral.Size = new System.Drawing.Size(374, 357);
			this.tpGeneral.TabIndex = 4;
			this.tpGeneral.Title = "General Preferences";
			this.tpGeneral.ToolTip = "Page";
			// 
			// optRenderStd
			// 
			this.optRenderStd.AutoSize = true;
			this.optRenderStd.Enabled = false;
			this.optRenderStd.Location = new System.Drawing.Point(191, 283);
			this.optRenderStd.Name = "optRenderStd";
			this.optRenderStd.Size = new System.Drawing.Size(69, 17);
			this.optRenderStd.TabIndex = 8;
			this.optRenderStd.TabStop = true;
			this.optRenderStd.Text = "Standard";
			this.toolTip1.SetToolTip(this.optRenderStd, "Classic Windows environment display.");
			this.optRenderStd.UseVisualStyleBackColor = true;
			this.optRenderStd.CheckedChanged += new System.EventHandler(this.optRenderStd_CheckedChanged);
			// 
			// optRenderPro
			// 
			this.optRenderPro.AutoSize = true;
			this.optRenderPro.Enabled = false;
			this.optRenderPro.Location = new System.Drawing.Point(102, 283);
			this.optRenderPro.Name = "optRenderPro";
			this.optRenderPro.Size = new System.Drawing.Size(83, 17);
			this.optRenderPro.TabIndex = 8;
			this.optRenderPro.TabStop = true;
			this.optRenderPro.Text = "Professional";
			this.toolTip1.SetToolTip(this.optRenderPro, "Microsoft Office 2003-style \"blue skin\" display.");
			this.optRenderPro.UseVisualStyleBackColor = true;
			this.optRenderPro.CheckedChanged += new System.EventHandler(this.optRenderPro_CheckedChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Enabled = false;
			this.label7.Location = new System.Drawing.Point(13, 285);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(83, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Display Render:";
			// 
			// cmdClearRecent
			// 
			this.cmdClearRecent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClearRecent.Location = new System.Drawing.Point(240, 318);
			this.cmdClearRecent.Name = "cmdClearRecent";
			this.cmdClearRecent.Size = new System.Drawing.Size(120, 24);
			this.cmdClearRecent.TabIndex = 6;
			this.cmdClearRecent.Text = "Clear Recent Items";
			this.toolTip1.SetToolTip(this.cmdClearRecent, "Cleares recently-opened projects in welcome screen.");
			this.cmdClearRecent.Click += new System.EventHandler(this.cmdClearRecent_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkAC_Infopop);
			this.groupBox3.Controls.Add(this.chkAC_Obj);
			this.groupBox3.Controls.Add(this.chkAC_TypeAsYouGo);
			this.groupBox3.Controls.Add(this.chkAC_Var);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(16, 152);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(344, 120);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Auto-Complete";
			// 
			// chkAC_Infopop
			// 
			this.chkAC_Infopop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAC_Infopop.Location = new System.Drawing.Point(8, 24);
			this.chkAC_Infopop.Name = "chkAC_Infopop";
			this.chkAC_Infopop.Size = new System.Drawing.Size(200, 16);
			this.chkAC_Infopop.TabIndex = 0;
			this.chkAC_Infopop.Text = "Enable Automatic Function Infopop";
			this.toolTip1.SetToolTip(this.chkAC_Infopop, "Enables function tooltips to be displayed on open parenthese.");
			this.chkAC_Infopop.CheckedChanged += new System.EventHandler(this.chkAC_Infopop_CheckedChanged);
			// 
			// chkAC_Obj
			// 
			this.chkAC_Obj.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAC_Obj.Location = new System.Drawing.Point(8, 48);
			this.chkAC_Obj.Name = "chkAC_Obj";
			this.chkAC_Obj.Size = new System.Drawing.Size(280, 16);
			this.chkAC_Obj.TabIndex = 0;
			this.chkAC_Obj.Text = "Enable Automatic Object Memberlist (:: or .)";
			this.toolTip1.SetToolTip(this.chkAC_Obj, "Displays object memberlist in dropdown on :: or .");
			this.chkAC_Obj.CheckedChanged += new System.EventHandler(this.chkAC_Obj_CheckedChanged);
			// 
			// chkAC_TypeAsYouGo
			// 
			this.chkAC_TypeAsYouGo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAC_TypeAsYouGo.Location = new System.Drawing.Point(8, 94);
			this.chkAC_TypeAsYouGo.Name = "chkAC_TypeAsYouGo";
			this.chkAC_TypeAsYouGo.Size = new System.Drawing.Size(280, 16);
			this.chkAC_TypeAsYouGo.TabIndex = 0;
			this.chkAC_TypeAsYouGo.Text = "Enable Type-As-You-Go Completion";
			this.toolTip1.SetToolTip(this.chkAC_TypeAsYouGo, "Enables display of valid functions and variables as you type them.");
			this.chkAC_TypeAsYouGo.CheckedChanged += new System.EventHandler(this.chkAC_TypeAsYouGo_CheckedChanged);
			// 
			// chkAC_Var
			// 
			this.chkAC_Var.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAC_Var.Location = new System.Drawing.Point(8, 72);
			this.chkAC_Var.Name = "chkAC_Var";
			this.chkAC_Var.Size = new System.Drawing.Size(280, 16);
			this.chkAC_Var.TabIndex = 0;
			this.chkAC_Var.Text = "Enable Automatic Variable Memberlist (__decl)";
			this.toolTip1.SetToolTip(this.chkAC_Var, "Enables declared variable memberlist on dot-operator.");
			this.chkAC_Var.CheckedChanged += new System.EventHandler(this.chkAC_Var_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmdOnDebugExec);
			this.groupBox2.Controls.Add(this.chkDebugSummary);
			this.groupBox2.Controls.Add(this.chkErrChkDbg);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(16, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(344, 72);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Debugger";
			// 
			// cmdOnDebugExec
			// 
			this.cmdOnDebugExec.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOnDebugExec.Location = new System.Drawing.Point(208, 32);
			this.cmdOnDebugExec.Name = "cmdOnDebugExec";
			this.cmdOnDebugExec.Size = new System.Drawing.Size(120, 24);
			this.cmdOnDebugExec.TabIndex = 2;
			this.cmdOnDebugExec.Text = "On Debug Execute";
			this.cmdOnDebugExec.Click += new System.EventHandler(this.cmdOnDebugExec_Click);
			// 
			// chkDebugSummary
			// 
			this.chkDebugSummary.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDebugSummary.Location = new System.Drawing.Point(8, 24);
			this.chkDebugSummary.Name = "chkDebugSummary";
			this.chkDebugSummary.Size = new System.Drawing.Size(144, 16);
			this.chkDebugSummary.TabIndex = 1;
			this.chkDebugSummary.Text = "Show Debug Summary";
			this.toolTip1.SetToolTip(this.chkDebugSummary, "Displays console output in a browser window when debugging is completed.");
			this.chkDebugSummary.CheckedChanged += new System.EventHandler(this.chkDebugSummary_CheckedChanged);
			// 
			// chkErrChkDbg
			// 
			this.chkErrChkDbg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkErrChkDbg.Location = new System.Drawing.Point(8, 48);
			this.chkErrChkDbg.Name = "chkErrChkDbg";
			this.chkErrChkDbg.Size = new System.Drawing.Size(192, 16);
			this.chkErrChkDbg.TabIndex = 1;
			this.chkErrChkDbg.Text = "Check for errors before debugging";
			this.toolTip1.SetToolTip(this.chkErrChkDbg, "Scans project for syntax errors before entering debug mode.");
			this.chkErrChkDbg.CheckedChanged += new System.EventHandler(this.chkErrChkDbg_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(288, 48);
			this.label1.TabIndex = 2;
			this.label1.Text = "This page allows you to configure the basic functionality of the TorqueDev develo" +
    "pment environment:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(16, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// chkCheckUpdates
			// 
			this.chkCheckUpdates.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkCheckUpdates.Location = new System.Drawing.Point(16, 323);
			this.chkCheckUpdates.Name = "chkCheckUpdates";
			this.chkCheckUpdates.Size = new System.Drawing.Size(160, 16);
			this.chkCheckUpdates.TabIndex = 1;
			this.chkCheckUpdates.Text = "Check for Updates Daily";
			this.toolTip1.SetToolTip(this.chkCheckUpdates, "Checks for CW updates once per day.");
			this.chkCheckUpdates.Visible = false;
			this.chkCheckUpdates.CheckedChanged += new System.EventHandler(this.chkCheckUpdates_CheckedChanged);
			// 
			// tpHighlighting
			// 
			this.tpHighlighting.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tpHighlighting.Controls.Add(this.cmdResetColors);
			this.tpHighlighting.Controls.Add(this.cmdAdColor);
			this.tpHighlighting.Controls.Add(this.lstColorDefs);
			this.tpHighlighting.Controls.Add(this.txtSynEd);
			this.tpHighlighting.Controls.Add(this.pictureBox2);
			this.tpHighlighting.Controls.Add(this.label2);
			this.tpHighlighting.InactiveBackColor = System.Drawing.Color.Empty;
			this.tpHighlighting.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tpHighlighting.InactiveTextColor = System.Drawing.Color.Empty;
			this.tpHighlighting.Location = new System.Drawing.Point(1, 1);
			this.tpHighlighting.Name = "tpHighlighting";
			this.tpHighlighting.SelectBackColor = System.Drawing.Color.Empty;
			this.tpHighlighting.Selected = false;
			this.tpHighlighting.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tpHighlighting.SelectTextColor = System.Drawing.Color.Empty;
			this.tpHighlighting.Size = new System.Drawing.Size(374, 357);
			this.tpHighlighting.TabIndex = 5;
			this.tpHighlighting.Title = "Syntax Highlighting";
			this.tpHighlighting.ToolTip = "Page";
			// 
			// cmdResetColors
			// 
			this.cmdResetColors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdResetColors.Location = new System.Drawing.Point(288, 64);
			this.cmdResetColors.Name = "cmdResetColors";
			this.cmdResetColors.Size = new System.Drawing.Size(72, 24);
			this.cmdResetColors.TabIndex = 9;
			this.cmdResetColors.Text = "Reset";
			this.cmdResetColors.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdResetColors_MouseUp);
			// 
			// cmdAdColor
			// 
			this.cmdAdColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdColor.Location = new System.Drawing.Point(288, 104);
			this.cmdAdColor.Name = "cmdAdColor";
			this.cmdAdColor.Size = new System.Drawing.Size(72, 24);
			this.cmdAdColor.TabIndex = 9;
			this.cmdAdColor.Text = "Adjust";
			this.cmdAdColor.Click += new System.EventHandler(this.cmdAdColor_Click);
			// 
			// lstColorDefs
			// 
			this.lstColorDefs.Items.AddRange(new object[] {
            "Built-In Preprocessors",
            "Comment Contents",
            "Comment Delimiters",
            "Default Font and Colors",
            "Engine Functions",
            "Global Variables",
            "Indent Guidelines",
            "Line Numbers",
            "Line Number Margin",
            "Local Variables",
            "Numbers",
            "Operators",
            "Reserved Words",
            "Scope Resolution Operators",
            "Special Comment Contents",
            "Special Comment Delimiters",
            "String Delimiters",
            "String Contents",
            "Tagged String Delimiters",
            "Tagged String Contents",
            "Variable Declarations"});
			this.lstColorDefs.Location = new System.Drawing.Point(16, 64);
			this.lstColorDefs.Name = "lstColorDefs";
			this.lstColorDefs.Size = new System.Drawing.Size(264, 69);
			this.lstColorDefs.TabIndex = 8;
			this.lstColorDefs.DoubleClick += new System.EventHandler(this.lstColorDefs_DoubleClick);
			// 
			// txtSynEd
			// 
			this.txtSynEd.Document = document1;
			this.txtSynEd.Location = new System.Drawing.Point(8, 144);
			this.txtSynEd.Name = "txtSynEd";
			this.txtSynEd.Size = new System.Drawing.Size(352, 200);
			this.txtSynEd.SplitType = ActiproSoftware.SyntaxEditor.SyntaxEditorSplitType.None;
			this.txtSynEd.TabIndex = 7;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(16, 8);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(48, 48);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(72, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 48);
			this.label2.TabIndex = 6;
			this.label2.Text = "You may adjust the colors of the syntax editor below.";
			// 
			// tpShortcut
			// 
			this.tpShortcut.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tpShortcut.Controls.Add(this.tvMenus);
			this.tpShortcut.Controls.Add(this.cmdApplyShortcut);
			this.tpShortcut.Controls.Add(this.txtAssgnShortcut);
			this.tpShortcut.Controls.Add(this.label4);
			this.tpShortcut.Controls.Add(this.lvShortcuts);
			this.tpShortcut.Controls.Add(this.label3);
			this.tpShortcut.Controls.Add(this.pictureBox3);
			this.tpShortcut.InactiveBackColor = System.Drawing.Color.Empty;
			this.tpShortcut.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tpShortcut.InactiveTextColor = System.Drawing.Color.Empty;
			this.tpShortcut.Location = new System.Drawing.Point(1, 1);
			this.tpShortcut.Name = "tpShortcut";
			this.tpShortcut.SelectBackColor = System.Drawing.Color.Empty;
			this.tpShortcut.Selected = false;
			this.tpShortcut.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tpShortcut.SelectTextColor = System.Drawing.Color.Empty;
			this.tpShortcut.Size = new System.Drawing.Size(374, 357);
			this.tpShortcut.TabIndex = 6;
			this.tpShortcut.Title = "Shortcuts";
			this.tpShortcut.ToolTip = "Page";
			// 
			// tvMenus
			// 
			this.tvMenus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvMenus.ImageIndex = 0;
			this.tvMenus.ImageList = this.imageList1;
			this.tvMenus.Location = new System.Drawing.Point(8, 64);
			this.tvMenus.Name = "tvMenus";
			this.tvMenus.SelectedImageIndex = 0;
			this.tvMenus.Size = new System.Drawing.Size(360, 200);
			this.tvMenus.TabIndex = 7;
			this.tvMenus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMenus_AfterSelect);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "folder_closed.png");
			this.imageList1.Images.SetKeyName(1, "blank_16x16.png");
			// 
			// cmdApplyShortcut
			// 
			this.cmdApplyShortcut.Enabled = false;
			this.cmdApplyShortcut.Location = new System.Drawing.Point(293, 289);
			this.cmdApplyShortcut.Name = "cmdApplyShortcut";
			this.cmdApplyShortcut.Size = new System.Drawing.Size(75, 23);
			this.cmdApplyShortcut.TabIndex = 6;
			this.cmdApplyShortcut.Text = "Apply";
			this.cmdApplyShortcut.UseVisualStyleBackColor = true;
			this.cmdApplyShortcut.Click += new System.EventHandler(this.cmdApplyShortcut_Click);
			// 
			// txtAssgnShortcut
			// 
			this.txtAssgnShortcut.AcceptsReturn = true;
			this.txtAssgnShortcut.AcceptsTab = true;
			this.txtAssgnShortcut.Location = new System.Drawing.Point(16, 291);
			this.txtAssgnShortcut.Name = "txtAssgnShortcut";
			this.txtAssgnShortcut.Size = new System.Drawing.Size(271, 21);
			this.txtAssgnShortcut.TabIndex = 5;
			this.txtAssgnShortcut.Text = "None";
			this.txtAssgnShortcut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtAssgnShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAssgnShortcut_KeyDown);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 272);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(232, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Assigned shortcut:";
			// 
			// lvShortcuts
			// 
			this.lvShortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lvShortcuts.FullRowSelect = true;
			this.lvShortcuts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvShortcuts.HideSelection = false;
			this.lvShortcuts.Location = new System.Drawing.Point(8, 64);
			this.lvShortcuts.MultiSelect = false;
			this.lvShortcuts.Name = "lvShortcuts";
			this.lvShortcuts.Size = new System.Drawing.Size(360, 200);
			this.lvShortcuts.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvShortcuts.TabIndex = 2;
			this.lvShortcuts.UseCompatibleStateImageBehavior = false;
			this.lvShortcuts.View = System.Windows.Forms.View.Details;
			this.lvShortcuts.Visible = false;
			this.lvShortcuts.SelectedIndexChanged += new System.EventHandler(this.lvShortcuts_SelectedIndexChanged);
			this.lvShortcuts.Click += new System.EventHandler(this.lvShortcuts_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Menu";
			this.columnHeader1.Width = 173;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Shortcut";
			this.columnHeader2.Width = 174;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(72, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(296, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "You can configure the menu shortcut keys below:";
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(16, 8);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(48, 48);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox3.TabIndex = 0;
			this.pictureBox3.TabStop = false;
			// 
			// tpEditor
			// 
			this.tpEditor.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tpEditor.Controls.Add(this.chkConvTabToSpc);
			this.tpEditor.Controls.Add(this.txtTabSpace);
			this.tpEditor.Controls.Add(this.label6);
			this.tpEditor.Controls.Add(this.label5);
			this.tpEditor.Controls.Add(this.pictureBox4);
			this.tpEditor.Controls.Add(this.chk_Ed_Virtlines);
			this.tpEditor.Controls.Add(this.chk_Ed_ShowLines);
			this.tpEditor.Controls.Add(this.chk_Ed_ShowWhitespace);
			this.tpEditor.Controls.Add(this.chk_Ed_ShowNewline);
			this.tpEditor.Controls.Add(this.chk_Ed_ShowBrackets);
			this.tpEditor.Controls.Add(this.chk_Ed_Wordwrap);
			this.tpEditor.Controls.Add(this.chk_Ed_ShowTabs);
			this.tpEditor.Controls.Add(this.chk_Ed_Errs);
			this.tpEditor.Controls.Add(this.chkScrollHint);
			this.tpEditor.Controls.Add(this.chkAutoColl);
			this.tpEditor.Controls.Add(this.chk_E_IndentGuides);
			this.tpEditor.Controls.Add(this.chk_Ed_CodeFold);
			this.tpEditor.Controls.Add(this.chk_Ed_AutoIndent);
			this.tpEditor.InactiveBackColor = System.Drawing.Color.Empty;
			this.tpEditor.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tpEditor.InactiveTextColor = System.Drawing.Color.Empty;
			this.tpEditor.Location = new System.Drawing.Point(1, 1);
			this.tpEditor.Name = "tpEditor";
			this.tpEditor.SelectBackColor = System.Drawing.Color.Empty;
			this.tpEditor.Selected = false;
			this.tpEditor.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tpEditor.SelectTextColor = System.Drawing.Color.Empty;
			this.tpEditor.Size = new System.Drawing.Size(374, 357);
			this.tpEditor.TabIndex = 7;
			this.tpEditor.Title = "Editor";
			this.tpEditor.ToolTip = "Page";
			// 
			// chkConvTabToSpc
			// 
			this.chkConvTabToSpc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkConvTabToSpc.Location = new System.Drawing.Point(152, 72);
			this.chkConvTabToSpc.Name = "chkConvTabToSpc";
			this.chkConvTabToSpc.Size = new System.Drawing.Size(144, 16);
			this.chkConvTabToSpc.TabIndex = 6;
			this.chkConvTabToSpc.Text = "Convert tabs to spaces";
			this.toolTip1.SetToolTip(this.chkConvTabToSpc, "Converts tabs to spaces upon saving a file.");
			this.chkConvTabToSpc.CheckedChanged += new System.EventHandler(this.chkConvTabToSpc_CheckedChanged);
			// 
			// txtTabSpace
			// 
			this.txtTabSpace.Location = new System.Drawing.Point(80, 72);
			this.txtTabSpace.MaxLength = 2;
			this.txtTabSpace.Name = "txtTabSpace";
			this.txtTabSpace.Size = new System.Drawing.Size(56, 21);
			this.txtTabSpace.TabIndex = 5;
			this.txtTabSpace.TextChanged += new System.EventHandler(this.txtTabSpace_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 16);
			this.label6.TabIndex = 4;
			this.label6.Text = "Tab space:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(72, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(296, 16);
			this.label5.TabIndex = 3;
			this.label5.Text = "You can configure basic editor preferences here:";
			// 
			// pictureBox4
			// 
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(16, 8);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(48, 48);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox4.TabIndex = 2;
			this.pictureBox4.TabStop = false;
			// 
			// chk_Ed_Virtlines
			// 
			this.chk_Ed_Virtlines.Enabled = false;
			this.chk_Ed_Virtlines.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_Virtlines.Location = new System.Drawing.Point(16, 328);
			this.chk_Ed_Virtlines.Name = "chk_Ed_Virtlines";
			this.chk_Ed_Virtlines.Size = new System.Drawing.Size(144, 16);
			this.chk_Ed_Virtlines.TabIndex = 6;
			this.chk_Ed_Virtlines.Text = "Virtual Space";
			this.toolTip1.SetToolTip(this.chk_Ed_Virtlines, "Enables accessing of \"virtual\" lines past the end of the file. (Feature not avail" +
        "able)");
			this.chk_Ed_Virtlines.CheckedChanged += new System.EventHandler(this.chk_Ed_Virtlines_CheckedChanged);
			// 
			// chk_Ed_ShowLines
			// 
			this.chk_Ed_ShowLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_ShowLines.Location = new System.Drawing.Point(16, 232);
			this.chk_Ed_ShowLines.Name = "chk_Ed_ShowLines";
			this.chk_Ed_ShowLines.Size = new System.Drawing.Size(144, 16);
			this.chk_Ed_ShowLines.TabIndex = 6;
			this.chk_Ed_ShowLines.Text = "Show Line Numbers";
			this.toolTip1.SetToolTip(this.chk_Ed_ShowLines, "Shows line numbers in the left column.");
			this.chk_Ed_ShowLines.CheckedChanged += new System.EventHandler(this.chk_Ed_ShowLines_CheckedChanged);
			// 
			// chk_Ed_ShowWhitespace
			// 
			this.chk_Ed_ShowWhitespace.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_ShowWhitespace.Location = new System.Drawing.Point(16, 304);
			this.chk_Ed_ShowWhitespace.Name = "chk_Ed_ShowWhitespace";
			this.chk_Ed_ShowWhitespace.Size = new System.Drawing.Size(168, 16);
			this.chk_Ed_ShowWhitespace.TabIndex = 6;
			this.chk_Ed_ShowWhitespace.Text = "Show Whitespace Glyphs";
			this.toolTip1.SetToolTip(this.chk_Ed_ShowWhitespace, "Shows a glyph for every whitespace (space bar) character.");
			this.chk_Ed_ShowWhitespace.CheckedChanged += new System.EventHandler(this.chk_Ed_ShowWhitespace_CheckedChanged);
			// 
			// chk_Ed_ShowNewline
			// 
			this.chk_Ed_ShowNewline.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_ShowNewline.Location = new System.Drawing.Point(16, 256);
			this.chk_Ed_ShowNewline.Name = "chk_Ed_ShowNewline";
			this.chk_Ed_ShowNewline.Size = new System.Drawing.Size(168, 16);
			this.chk_Ed_ShowNewline.TabIndex = 6;
			this.chk_Ed_ShowNewline.Text = "Show Newline Glyphs";
			this.toolTip1.SetToolTip(this.chk_Ed_ShowNewline, "Displays a glyph for every carriage return/line feed combination.");
			this.chk_Ed_ShowNewline.CheckedChanged += new System.EventHandler(this.chk_Ed_ShowNewline_CheckedChanged);
			// 
			// chk_Ed_ShowBrackets
			// 
			this.chk_Ed_ShowBrackets.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_ShowBrackets.Location = new System.Drawing.Point(16, 160);
			this.chk_Ed_ShowBrackets.Name = "chk_Ed_ShowBrackets";
			this.chk_Ed_ShowBrackets.Size = new System.Drawing.Size(168, 16);
			this.chk_Ed_ShowBrackets.TabIndex = 6;
			this.chk_Ed_ShowBrackets.Text = "Enable Bracket Highlighting";
			this.toolTip1.SetToolTip(this.chk_Ed_ShowBrackets, "Automatically highlights matching parentheses, brackets, or braces while the curs" +
        "or is adjacent to it.");
			this.chk_Ed_ShowBrackets.CheckedChanged += new System.EventHandler(this.chk_Ed_ShowBrackets_CheckedChanged);
			// 
			// chk_Ed_Wordwrap
			// 
			this.chk_Ed_Wordwrap.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_Wordwrap.Location = new System.Drawing.Point(16, 208);
			this.chk_Ed_Wordwrap.Name = "chk_Ed_Wordwrap";
			this.chk_Ed_Wordwrap.Size = new System.Drawing.Size(168, 16);
			this.chk_Ed_Wordwrap.TabIndex = 6;
			this.chk_Ed_Wordwrap.Text = "Enable Wordwrap";
			this.toolTip1.SetToolTip(this.chk_Ed_Wordwrap, "Wraps text that passes the horizontal boundary of the text editor to the next lin" +
        "e.");
			this.chk_Ed_Wordwrap.CheckedChanged += new System.EventHandler(this.chk_Ed_Wordwrap_CheckedChanged);
			// 
			// chk_Ed_ShowTabs
			// 
			this.chk_Ed_ShowTabs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_ShowTabs.Location = new System.Drawing.Point(16, 280);
			this.chk_Ed_ShowTabs.Name = "chk_Ed_ShowTabs";
			this.chk_Ed_ShowTabs.Size = new System.Drawing.Size(168, 16);
			this.chk_Ed_ShowTabs.TabIndex = 6;
			this.chk_Ed_ShowTabs.Text = "Show Tab Glyphs";
			this.toolTip1.SetToolTip(this.chk_Ed_ShowTabs, "Shows a glyph for all TAB characters.");
			this.chk_Ed_ShowTabs.CheckedChanged += new System.EventHandler(this.chk_Ed_ShowTabs_CheckedChanged);
			// 
			// chk_Ed_Errs
			// 
			this.chk_Ed_Errs.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_Errs.Location = new System.Drawing.Point(16, 136);
			this.chk_Ed_Errs.Name = "chk_Ed_Errs";
			this.chk_Ed_Errs.Size = new System.Drawing.Size(184, 16);
			this.chk_Ed_Errs.TabIndex = 6;
			this.chk_Ed_Errs.Text = "Check for errors while typing";
			this.toolTip1.SetToolTip(this.chk_Ed_Errs, "Underlines detected syntax errors in red while typing.");
			this.chk_Ed_Errs.CheckedChanged += new System.EventHandler(this.chk_Ed_Errs_CheckedChanged);
			// 
			// chkScrollHint
			// 
			this.chkScrollHint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkScrollHint.Location = new System.Drawing.Point(16, 184);
			this.chkScrollHint.Name = "chkScrollHint";
			this.chkScrollHint.Size = new System.Drawing.Size(112, 16);
			this.chkScrollHint.TabIndex = 1;
			this.chkScrollHint.Text = "Enable Scroll Hint";
			this.toolTip1.SetToolTip(this.chkScrollHint, "Displays passing line numbers in a tooltip while scrolling.");
			this.chkScrollHint.CheckedChanged += new System.EventHandler(this.chkScrollHint_CheckedChanged);
			// 
			// chkAutoColl
			// 
			this.chkAutoColl.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAutoColl.Location = new System.Drawing.Point(16, 112);
			this.chkAutoColl.Name = "chkAutoColl";
			this.chkAutoColl.Size = new System.Drawing.Size(136, 16);
			this.chkAutoColl.TabIndex = 1;
			this.chkAutoColl.Text = "Auto-Collapse Regions";
			this.toolTip1.SetToolTip(this.chkAutoColl, "Automatically collapses __REGION and __END blocks when a file is opened.");
			this.chkAutoColl.CheckedChanged += new System.EventHandler(this.chkAutoColl_CheckedChanged);
			// 
			// chk_E_IndentGuides
			// 
			this.chk_E_IndentGuides.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_E_IndentGuides.Location = new System.Drawing.Point(216, 180);
			this.chk_E_IndentGuides.Name = "chk_E_IndentGuides";
			this.chk_E_IndentGuides.Size = new System.Drawing.Size(136, 32);
			this.chk_E_IndentGuides.TabIndex = 1;
			this.chk_E_IndentGuides.Text = "Enable Indent Guides";
			this.toolTip1.SetToolTip(this.chk_E_IndentGuides, "Enables or disables the vertical guidelines displayed on indents.");
			this.chk_E_IndentGuides.CheckedChanged += new System.EventHandler(this.chk_E_IndentGuides_CheckedChanged);
			// 
			// chk_Ed_CodeFold
			// 
			this.chk_Ed_CodeFold.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_CodeFold.Location = new System.Drawing.Point(216, 142);
			this.chk_Ed_CodeFold.Name = "chk_Ed_CodeFold";
			this.chk_Ed_CodeFold.Size = new System.Drawing.Size(136, 32);
			this.chk_Ed_CodeFold.TabIndex = 1;
			this.chk_Ed_CodeFold.Text = "Enable Automatic Code Folding";
			this.toolTip1.SetToolTip(this.chk_Ed_CodeFold, "Enables non-region codeblocks to be collapsed.");
			this.chk_Ed_CodeFold.CheckedChanged += new System.EventHandler(this.chk_Ed_CodeFold_CheckedChanged);
			// 
			// chk_Ed_AutoIndent
			// 
			this.chk_Ed_AutoIndent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Ed_AutoIndent.Location = new System.Drawing.Point(216, 104);
			this.chk_Ed_AutoIndent.Name = "chk_Ed_AutoIndent";
			this.chk_Ed_AutoIndent.Size = new System.Drawing.Size(136, 32);
			this.chk_Ed_AutoIndent.TabIndex = 1;
			this.chk_Ed_AutoIndent.Text = "Auto Indent/Outdent on Brace";
			this.toolTip1.SetToolTip(this.chk_Ed_AutoIndent, "Automatically tabs in or untabs on open brace and close brace respectively.");
			this.chk_Ed_AutoIndent.CheckedChanged += new System.EventHandler(this.chk_Ed_AutoIndent_CheckedChanged);
			// 
			// cmdFlushAuth
			// 
			this.cmdFlushAuth.Enabled = false;
			this.cmdFlushAuth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFlushAuth.Location = new System.Drawing.Point(9, 398);
			this.cmdFlushAuth.Name = "cmdFlushAuth";
			this.cmdFlushAuth.Size = new System.Drawing.Size(152, 24);
			this.cmdFlushAuth.TabIndex = 6;
			this.cmdFlushAuth.Text = "Flush Authorization Cache";
			this.cmdFlushAuth.Visible = false;
			this.cmdFlushAuth.Click += new System.EventHandler(this.cmdFlushAuth_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(304, 400);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(216, 400);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// ctxReset
			// 
			this.ctxReset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxReset_DefaultClr,
            this.ctxReset_NoolnessDarkClr});
			this.ctxReset.Name = "ctxReset";
			this.ctxReset.Size = new System.Drawing.Size(211, 48);
			// 
			// ctxReset_DefaultClr
			// 
			this.ctxReset_DefaultClr.Name = "ctxReset_DefaultClr";
			this.ctxReset_DefaultClr.Size = new System.Drawing.Size(210, 22);
			this.ctxReset_DefaultClr.Text = "TorqueDev Default Colors";
			this.ctxReset_DefaultClr.Click += new System.EventHandler(this.ctxReset_DefaultClr_Click);
			// 
			// ctxReset_NoolnessDarkClr
			// 
			this.ctxReset_NoolnessDarkClr.Name = "ctxReset_NoolnessDarkClr";
			this.ctxReset_NoolnessDarkClr.Size = new System.Drawing.Size(210, 22);
			this.ctxReset_NoolnessDarkClr.Text = "Noolness Dark Colors";
			this.ctxReset_NoolnessDarkClr.Click += new System.EventHandler(this.ctxReset_NoolnessDarkClr_Click);
			// 
			// frmConfig
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(394, 431);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdFlushAuth);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmConfig";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Preferences";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfig_FormClosing);
			this.Load += new System.EventHandler(this.frmConfig_Load);
			this.tabMain.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.tpGeneral.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tpHighlighting.ResumeLayout(false);
			this.tpHighlighting.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.tpShortcut.ResumeLayout(false);
			this.tpShortcut.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.tpEditor.ResumeLayout(false);
			this.tpEditor.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.ctxReset.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void cmdAdColor_Click(object sender, System.EventArgs e) {
			frmConfigColor fConfigColor = null;

			if (lstColorDefs.SelectedIndex == -1)
				return;

			bool allbg = false;

			switch(lstColorDefs.SelectedItem.ToString()) {
				case "Built-In Preprocessors":
					fConfigColor = new frmConfigColor("RegionStyle");
					break;
				case "Comment Contents":
					fConfigColor = new frmConfigColor("CommentDefaultStyle");
					break;
				case "Comment Delimiters":
					fConfigColor = new frmConfigColor("CommentDelimiterStyle");
					break;
				case "Engine Functions":
					fConfigColor = new frmConfigColor("FunctionStyle");
					break;
				case "Global Variables":
					fConfigColor = new frmConfigColor("GlobalVariableStyle");
					break;
				case "Local Variables":
					fConfigColor = new frmConfigColor("LocalVariableStyle");
					break;
				case "Numbers":
					fConfigColor = new frmConfigColor("NumberStyle");
					break;
				case "Operators":
					fConfigColor = new frmConfigColor("OperatorStyle");
					break;
				case "Reserved Words":
					fConfigColor = new frmConfigColor("ReservedWordStyle");
					break;
				case "Scope Resolution Operators":
					fConfigColor = new frmConfigColor("MemberTokenStyle");
					break;
				case "String Delimiters":
					fConfigColor = new frmConfigColor("StringDelimiterStyle");
					break;
				case "String Contents":
					fConfigColor = new frmConfigColor("StringDefaultStyle");
					break;
				case "Tagged String Delimiters":
					fConfigColor = new frmConfigColor("TaggedStringDelimiterStyle");
					break;
				case "Tagged String Contents":
					fConfigColor = new frmConfigColor("TaggedStringDefaultStyle");
					break;
				case "Default Font and Colors":
					fConfigColor = new frmConfigColor("DefaultStyle");
					allbg = true;
					break;
				case "Special Comment Delimiters":
					fConfigColor = new frmConfigColor("SpecialCommentDelimiterStyle");
					break;
				case "Special Comment Contents":
					fConfigColor = new frmConfigColor("SpecialCommentDefaultStyle");
					break;
				case "Variable Declarations":
					fConfigColor = new frmConfigColor("DeclareStyle");
					break;
				case "Line Numbers":
					fConfigColor = new frmConfigColor("_LineNumbers");
					break;
				case "Line Number Margin":
					fConfigColor = new frmConfigColor("_LineNumbersBorder");
					break;
				case "Indent Guidelines":
					fConfigColor = new frmConfigColor("_IndentGuidelines");
					break;
			}
			DialogResult result = fConfigColor.ShowDialog();
			
			if (result == DialogResult.OK) {
				// If we're setting the background colors for all
				// the things, let's do it here
				if (allbg) {
					IDictionaryEnumerator enumer = loconf.colors.GetEnumerator();
					while(enumer.MoveNext()) {
						if (enumer.Key.ToString() == "RegionStyle" || enumer.Key.ToString().StartsWith("_") == true
							|| enumer.Key.ToString() == "DefaultStyle")
							continue;

						(enumer.Value as CConfig.HighlightEntry).BackColor = (loconf.colors["DefaultStyle"] as CConfig.HighlightEntry).BackColor;
					}
				}

				this.loconf.LoadColorData(txtSynEd);
			}

			fConfigColor.Dispose();
			fConfigColor = null;
		}

		private void frmConfig_Load(object sender, System.EventArgs e) {
			//txtSynEd.Document.LoadLanguageFromXml(Application.StartupPath + "\\highlight.xml", 0);
			this.Show();

			if (g.Project == null)
				cmdOnDebugExec.Enabled = false;

			txtSynEd.Document.LoadLanguageFromXml(Application.StartupPath + "\\highlight.bin", 5000);

			txtSynEd.Document.Text = "// Torque Game Engine\n\n/// This function returns something\nfunction returnSomething(%client, %locvar) {\n\tif (%locvar $= \"\")\n\t\treturn;\n\n\techo(\"Begin; standby\");\n\tfor (%i=0; %i<3; %i++)\n\t\tcommandToClient(%client, 'Test', $globvar)\n\n\t//__region test\n\tMyObject::MyFunction();\n\tMyObject.setValue(\"asdf\");\n\t\n\n\t//# DECLARE %mylocalvar AS SimObject\n\t%mylocalvar.save(\"testing.txt\")\n\n\t//__end\n\n}";
			txtSynEd.Document.ReadOnly = true;

			this.loconf.LoadColorData(txtSynEd);

            this.chkAutoColl.Checked = this.loconf.bAutoCollapse;
			this.chkScrollHint.Checked = this.loconf.bScrollHint;
			this.chkCheckUpdates.Checked = this.loconf.bCheckUpdates;
			this.chkDebugSummary.Checked = this.loconf.bShowDebugSummary;

			this.chkAC_Infopop.Checked = this.loconf.b_AC_Infopop;
			this.chkAC_Obj.Checked = this.loconf.b_AC_ObjectML;
			this.chkAC_Var.Checked = this.loconf.b_AC_VariableML;
			this.chkAC_TypeAsYouGo.Checked = this.loconf.b_AC_TypeAsYouGo;

			this.chk_Ed_ShowBrackets.Checked = loconf.b_Ed_BracketHighlight;
			this.chk_Ed_ShowLines.Checked = loconf.b_Ed_ShowLines;
			this.chk_Ed_ShowNewline.Checked = loconf.b_Ed_ShowNewline;
			this.chk_Ed_ShowWhitespace.Checked = loconf.b_Ed_ShowNewline;
			this.chk_Ed_Virtlines.Checked = loconf.b_Ed_Virtlines;
			this.chk_Ed_Wordwrap.Checked = loconf.b_Ed_Wordwrap;
			this.chkConvTabToSpc.Checked = loconf.b_Ed_ConvertTabsToSpaces;
			this.chk_Ed_ShowTabs.Checked = loconf.b_Ed_ShowTabs;
			this.chk_Ed_CodeFold.Checked = loconf.b_Ed_CodeFold;
			this.chk_E_IndentGuides.Checked = loconf.b_Ed_IndentGuides;
			this.txtTabSpace.Text = loconf.i_Ed_TabSize.ToString();

			this.chk_Ed_AutoIndent.Checked = loconf.b_Ed_AutoIndent;

			this.chkErrChkDbg.Checked = loconf.b_Err_BeforeCompile;
			this.chk_Ed_Errs.Checked = loconf.b_Err_WhileTyping;

			if (loconf.ColorPref == ColorPref.Professional)
				this.optRenderPro.Checked = true;
			else
				this.optRenderStd.Checked = true;

			//this.txtDelExt.Text = loconf.DSOExtension;
			//this.chkDelDSO.Checked = this.loconf.b_DeleteDSO;

			this.tpGeneral.Selected = true;
			this.cmdOK.Focus();

			// Enumerate each menu shortcut and add it to the shortcut list
			/*IDictionaryEnumerator enumer = this.loconf.menu_shortcuts.GetEnumerator();
			while(enumer.MoveNext()) {
				ListViewItem item = new ListViewItem(enumer.Key.ToString());
				item.SubItems.Add(ParseKeycode((Keys)enumer.Value).ToString());
				item.Tag = (Keys)enumer.Value;

				this.lvShortcuts.Items.Add(item);
			}*/
			
			// Enumerate all the menu items recursively on the main thing, and
			// add it to the list.  Assign it a shortcut if it
			// has one

			frmWait fWait = new frmWait("Loading configuration page...");
			fWait.Show(this);

			tvMenus.BeginUpdate();

			foreach (ToolStripItem item in g.Main.mnuMain.Items)
				EnumMenuItems(item, null);

			tvMenus.EndUpdate();


			// PLUGINS load custom config pages
			foreach (CustomConfigTab tab in g.CustomConfigTabs) {
				if (!(tab.TabControl is ICodeweaverConfigPage)) {
					g.PluginException(
						new Exception("Custom config page must derive from ICodeweaverConfigPage"),
						new CWPlugin(tab.Plugin.CWPluginGuid.ToString(), new Version("0.0"), "", "", "", new Version("0.0"), tab.Plugin.CWPluginGuid, tab.Plugin)
					);
				} else {
					Crownwood.DotNetMagic.Controls.TabPage newPage = new Crownwood.DotNetMagic.Controls.TabPage(tab.TabText, tab.TabControl);
					tabMain.TabPages.Add(newPage);

					// Call initialization
					try {
						(tab.TabControl as ICodeweaverConfigPage).CWOnConfigLoad();
					} catch (Exception exc) {
						g.PluginException(exc, new CWPlugin(tab.Plugin.CWPluginGuid.ToString(), new Version("0.0"), "", "", "", new Version("0.0"), tab.Plugin.CWPluginGuid, tab.Plugin));
					}
				}
			}


			fWait.Close();
			fWait.Dispose();

			fWait = null;

		}

		private void EnumMenuItems(ToolStripItem tsi, TreeNode parent) {
			if (tsi is ToolStripMenuItem) {
				TreeNode newnode = new TreeNode();

				// Add the image to the listview if it has one
				if ((tsi as ToolStripMenuItem).Image != null) {
					imageList1.Images.Add((tsi as ToolStripMenuItem).Image);
					newnode.ImageIndex = imageList1.Images.Count - 1;
					newnode.SelectedImageIndex = imageList1.Images.Count - 1;
				} else {
					if (parent == null)
						newnode.SelectedImageIndex = newnode.ImageIndex = 0;
					else
						newnode.SelectedImageIndex = newnode.ImageIndex = 1;
				}

				newnode.Text = tsi.Text.Replace("&", "");
				newnode.Tag = tsi;

				if ((tsi as ToolStripMenuItem).ShortcutKeys != Keys.None)
					newnode.Text += " (" + ParseKeycode((tsi as ToolStripMenuItem).ShortcutKeys).ToString() + ")";

				if (parent == null)
					tvMenus.Nodes.Add(newnode);
				else
					parent.Nodes.Add(newnode);

				if ((tsi as ToolStripMenuItem).DropDownItems.Count > 0) {
					foreach (ToolStripItem subitem in (tsi as ToolStripMenuItem).DropDownItems) {
						EnumMenuItems(subitem, newnode);
					}
				}
			}
		}

		private ShortcutConfig ParseKeycode(Keys keys) {

			ShortcutConfig output = new ShortcutConfig(Keys.None, Keys.None);

			if ((keys & Keys.Control) == Keys.Control) {
				output.modifiers |= Keys.Control;
				keys -= Keys.Control;
			} if ((keys & Keys.Alt) == Keys.Alt) {
				output.modifiers |= Keys.Alt;
				keys -= Keys.Alt;
			} if ((keys & Keys.Shift) == Keys.Shift) {
				output.modifiers |= Keys.Shift;
				keys -= Keys.Shift;
			}

			output.keys = keys;

			return output;
		}

		private void lstColorDefs_DoubleClick(object sender, System.EventArgs e) {
			this.cmdAdColor_Click(sender, e);
		}

		private void chkAutoColl_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.bAutoCollapse = this.chkAutoColl.Checked;
		}

		private void chkScrollHint_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.bScrollHint = this.chkScrollHint.Checked;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			// PLUGINS commit
			foreach (CustomConfigTab tab in g.CustomConfigTabs) {
				// They all derive from ICodeweaverConfigPage by now, so we're not going to waste
				// time doing a sanity check
				try {
					(tab.TabControl as ICodeweaverConfigPage).CWOnConfigCommit();
				} catch (Exception exc) {
					g.PluginException(exc, new CWPlugin(tab.Plugin.CWPluginGuid.ToString(), new Version("0.0"), "", "", "", new Version("0.0"), tab.Plugin.CWPluginGuid, tab.Plugin));
				}
			}

			g.Config = this.loconf;
			this.Close();
		}

		private void chkCheckUpdates_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.bCheckUpdates = this.chkCheckUpdates.Checked;
		}

		private void chkDebugSummary_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.bShowDebugSummary = this.chkDebugSummary.Checked;
		}

		private void lvShortcuts_Click(object sender, System.EventArgs e) {
			if (lvShortcuts.SelectedItems.Count == 0)
				return;

			/*for (int i = 0; i < this.cboShortcuts.Items.Count; i++) {
				if (lvShortcuts.SelectedItems[0].SubItems[1].Text == this.cboShortcuts.Items[i].ToString()) {
					this.cboShortcuts.SelectedIndex = i;
					break;
				}
			}*/
			txtAssgnShortcut.Text = ParseKeycode((Keys)lvShortcuts.SelectedItems[0].Tag).ToString();
		}

		private Shortcut FindShortcut(string text) {
			foreach (Shortcut sc in Enum.GetValues(typeof(Shortcut))) {
				if (sc.ToString() == text)
					return sc;
			}

			return Shortcut.None;
		}

		/*private void cboShortcuts_SelectionChangeCommitted(object sender, System.EventArgs e) {
			if (lvShortcuts.SelectedItems.Count == 0) {
				cboShortcuts.SelectedIndex = 0;
				return;
			}

			Shortcut sc = FindShortcut(cboShortcuts.SelectedItem.ToString());

		
			if (!this.loconf.menu_shortcuts.ContainsKey(lvShortcuts.SelectedItems[0].Text)) {
				cboShortcuts.SelectedIndex = 0;
				return;
			}

			this.loconf.menu_shortcuts[lvShortcuts.SelectedItems[0].Text] = sc;
			lvShortcuts.SelectedItems[0].SubItems[1].Text = sc.ToString();
		}*/

		private void chkAC_Infopop_CheckedChanged(object sender, System.EventArgs e) {
			loconf.b_AC_Infopop = this.chkAC_Infopop.Checked;
		}

		private void chkAC_Obj_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_AC_ObjectML = this.chkAC_Obj.Checked;
		}

		private void chkAC_Var_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_AC_VariableML = this.chkAC_Var.Checked;
		}

		private void txtTabSpace_TextChanged(object sender, System.EventArgs e) {
			double d = 0.0d;

			if (!Double.TryParse(txtTabSpace.Text, System.Globalization.NumberStyles.Integer, new System.Globalization.CultureInfo("en-US"), out d)) {
				txtTabSpace.Text = "4";
				return;
			}

			try {
				if (txtTabSpace.Text == "" || Convert.ToInt32(txtTabSpace.Text) < 1) {
					this.loconf.i_Ed_TabSize = 4;
				} else {
					this.loconf.i_Ed_TabSize = Convert.ToInt32(txtTabSpace.Text);
				}
			} catch {
				this.loconf.i_Ed_TabSize = 4;
				txtTabSpace.Text = "4";
			}
		}

		private void chkConvTabToSpc_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_ConvertTabsToSpaces = chkConvTabToSpc.Checked;
		}

		private void chk_Ed_Virtlines_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_Virtlines = chk_Ed_Virtlines.Checked;
		}

		private void chk_Ed_ShowLines_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_ShowLines = chk_Ed_ShowLines.Checked;
		}

		private void chk_Ed_ShowWhitespace_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_ShowWhitespace = chk_Ed_ShowWhitespace.Checked;
		}

		private void chk_Ed_ShowNewline_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_ShowNewline = this.chk_Ed_ShowNewline.Checked;
		}

		private void chk_Ed_ShowBrackets_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_BracketHighlight = this.chk_Ed_ShowBrackets.Checked;
		}

		private void chk_Ed_Wordwrap_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_Wordwrap = this.chk_Ed_Wordwrap.Checked;
		}

		private void chk_Ed_ShowTabs_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_ShowTabs = chk_Ed_ShowTabs.Checked;
		}

		private void cmdClearRecent_Click(object sender, System.EventArgs e) {
			this.loconf.recent_files = new Hashtable();
			this.cmdClearRecent.Enabled = false;
		}

		private void cmdFlushAuth_Click(object sender, System.EventArgs e) {
			MessageBox.Show(this, "You must restart the application for these changes to take effect.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			this.loconf.ActivationNextCheck = 0;
			this.cmdFlushAuth.Enabled = false;
		}

		private void cmdOnDebugExec_Click(object sender, System.EventArgs e) {
			frmConfigDebugExec fConfDebugExec = new frmConfigDebugExec(this.loconf);
			fConfDebugExec.ShowDialog();

			fConfDebugExec.Dispose();
			fConfDebugExec = null;
		}

		private void chk_Ed_Errs_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Err_WhileTyping = this.chk_Ed_Errs.Checked;
		}

		private void chkErrChkDbg_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Err_BeforeCompile = this.chkErrChkDbg.Checked;
		}

		private void chk_Ed_AutoIndent_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_Ed_AutoIndent = this.chk_Ed_AutoIndent.Checked;
		}

		private void optRenderPro_CheckedChanged(object sender, EventArgs e) {
			if (this.optRenderPro.Checked == true)
				loconf.ColorPref = ColorPref.Professional;
		}

		private void optRenderStd_CheckedChanged(object sender, EventArgs e) {
			if (this.optRenderStd.Checked == true)
				loconf.ColorPref = ColorPref.Standard;
		}

		private void txtAssgnShortcut_KeyDown(object sender, KeyEventArgs e) {
			// We're processing input to the assigned shortcut now:
			string output = "";

			if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape) {
				output = "None";
				txtAssgnShortcut.Tag = new ShortcutConfig(Keys.None, Keys.None);
			} else {
				if ((e.Modifiers & Keys.Control) == Keys.Control)
					output += "Ctrl+";
				if ((e.Modifiers & Keys.Alt) == Keys.Alt)
					output += "Alt+";
				if ((e.Modifiers & Keys.Shift) == Keys.Shift)
					output += "Shift+";


				if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
					output += "???";
				else
					output += e.KeyCode.ToString();

				txtAssgnShortcut.Tag = new ShortcutConfig(e.KeyCode, e.Modifiers);
			}

			e.SuppressKeyPress = true;
			e.Handled = true;

			cmdApplyShortcut.Enabled = true;
			txtAssgnShortcut.Text = output;
		}

		private void cmdApplyShortcut_Click(object sender, EventArgs e) {
			//if (lvShortcuts.SelectedItems.Count == 0 || txtAssgnShortcut.Tag == null)
			//	return;
			if (tvMenus.SelectedNode == null || tvMenus.SelectedNode.Tag == null)
				return;


			ShortcutConfig sk = (ShortcutConfig)txtAssgnShortcut.Tag;

			// Check for duplicate first
			foreach (Keys k in loconf.menu_shortcuts.Values) {
				if (k == (sk.keys | sk.modifiers)) {
					MessageBox.Show("This shortcut has already been assigned to another menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			if (sk.keys == Keys.None || sk.keys == Keys.ControlKey || sk.keys == Keys.ShiftKey || sk.keys == Keys.Menu)
				return;

			if (!this.loconf.menu_shortcuts.ContainsKey((tvMenus.SelectedNode.Tag as ToolStripMenuItem).Name)) {
				return;
			}

			this.loconf.menu_shortcuts[(tvMenus.SelectedNode.Tag as ToolStripMenuItem).Name] = sk.ToKeys();
			//lvShortcuts.SelectedItems[0].SubItems[1].Text = sk.ToString();
			tvMenus.SelectedNode.Text = (tvMenus.SelectedNode.Tag as ToolStripMenuItem).Text.Replace("&", "") + " (" + sk.ToString() + ")";

			cmdApplyShortcut.Enabled = false;
		}

		private void lvShortcuts_SelectedIndexChanged(object sender, EventArgs e) {
			if (lvShortcuts.SelectedItems.Count == 0 || lvShortcuts.SelectedItems[0].Tag == null)
				return;

			cmdApplyShortcut.Enabled = true;
			txtAssgnShortcut.Select();
			txtAssgnShortcut.Focus();
		}

		private void chk_Ed_CodeFold_CheckedChanged(object sender, EventArgs e) {
			this.loconf.b_Ed_CodeFold = chk_Ed_CodeFold.Checked;
		}

		private void chk_E_IndentGuides_CheckedChanged(object sender, EventArgs e) {
			this.loconf.b_Ed_IndentGuides = chk_E_IndentGuides.Checked;
		}

		private void ctxReset_DefaultClr_Click(object sender, EventArgs e) {
			this.loconf.LoadScheme_Default();
			this.loconf.LoadColorData(txtSynEd);
		}

		private void ctxReset_NoolnessDarkClr_Click(object sender, EventArgs e) {
			this.loconf.LoadScheme_Noolness();
			this.loconf.LoadColorData(txtSynEd);
		}

		private void cmdResetColors_MouseUp(object sender, MouseEventArgs e) {
			// Show a dropdown list that will reset colors
			ctxReset.Show(cmdResetColors, e.X, e.Y);
		}

		private void chkAC_TypeAsYouGo_CheckedChanged(object sender, EventArgs e) {
			this.loconf.b_AC_TypeAsYouGo = chkAC_TypeAsYouGo.Checked;
		}

		private void tvMenus_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node == null)
				return;

			// Retrieve the shortcut
			if (!this.loconf.menu_shortcuts.ContainsKey((e.Node.Tag as ToolStripMenuItem).Name))
				this.loconf.menu_shortcuts.Add((e.Node.Tag as ToolStripMenuItem).Name, Keys.None);

			txtAssgnShortcut.Text = ParseKeycode((Keys)this.loconf.menu_shortcuts[(e.Node.Tag as ToolStripMenuItem).Name]).ToString();

			cmdApplyShortcut.Enabled = true;
			txtAssgnShortcut.Select();
			txtAssgnShortcut.Focus();
		}

		private void frmConfig_FormClosing(object sender, FormClosingEventArgs e) {
			// PLUGINS remove any custom tabs so they don't get disposed
			foreach (Crownwood.DotNetMagic.Controls.TabPage tab in this.tabMain.TabPages) {
				if (tab.Control is ICodeweaverConfigPage)
					tab.Control = null;
			}
		}

		
	}
}
