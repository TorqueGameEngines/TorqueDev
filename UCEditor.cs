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
using System.Text.RegularExpressions;
using ActiproSoftware.WinUICore;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Commands;
using TSDev.Plugins;


namespace TSDev
{
	/// <summary>
	/// Summary description for UCEditor.
	/// </summary>
	internal class UCEditor : System.Windows.Forms.UserControl
	{
		public delegate void _dDropdownMake();
		public _dDropdownMake _DropDownMake;

		public ActiproSoftware.SyntaxEditor.SyntaxEditor txtEditor;
		private System.ComponentModel.IContainer components;

		private CAutoComplete ac = frmMain.ac;
		private System.Windows.Forms.ImageList ilMainMenu;
		private XParser parser = frmMain.parser;

		public CAutoComplete.ClassEntry prevclass;
		public CAutoComplete.ClassEntry.FuncEntry prevfunc;

		public DateTime LastWritten = new DateTime();

		public CProject.File g_curFile;

		public bool isIndependent = false;
		public bool isSaved = false;
		public bool isDropdownSwitched = false;

		private string _LastText = "";
		private bool _LastMoved = false;
		private System.Windows.Forms.ImageList ilToolbar;

		private bool _isDirty = false;
		private string _sTitle = "";
		private System.Windows.Forms.StatusBar sbMain;
		private System.Windows.Forms.StatusBarPanel sbpRow;
		private System.Windows.Forms.StatusBarPanel sbpCol;
		private System.Windows.Forms.StatusBarPanel sbpTextInfo;
		public System.Windows.Forms.Timer tmrPosNotify;
		private System.Windows.Forms.StatusBarPanel sbpFileName;
		private System.Windows.Forms.StatusBarPanel sbpSelection;
		//private netMercs.Controls.ImageComboBox.ImageComboBox cboCurFileFuncs;
		private ComboBoxEx cboCurFileFuncs;
		public System.Windows.Forms.Timer tmrErrorMonitor;
		private Timer tmrFileWatcher;
		public ContextMenuStrip ctxEditor;
		private ToolStripMenuItem mnuCt_Cut;
		private ToolStripMenuItem mnuCt_Copy;
		private ToolStripMenuItem mnuCt_Paste;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem mnuCt_GoToDef;
		private ToolStripMenuItem mnuCt_CommentSel;
		private ToolStripMenuItem mnuCt_UncommentSel;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem mnuCt_ToggleBreak;
		private ToolStripMenuItem mnuCt_BreakProps;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem mnuCt_Reference;
		public Crownwood.DotNetMagic.Controls.TabPage _ParentTab = null;

		private bool bDisableAdvancedCompletion = false;
		private Panel pnlFileChanged;
		private LinkLabel lnkCancelReload;
		private LinkLabel lnkReloadContents;
		private Label label1;
		private bool bDisableAllCompletion = false;

		#region Property Declarations
		public bool isDirty {
			get {
				return _isDirty;
			}
			set {
				_isDirty = value;
				int imgidx = ((_isDirty) ? 1 : 0);

				foreach (PrimaryTab<UCEditor> ed in g.Editors) {
					if (ed.Control.Equals(this)) {
						ed.page.ImageIndex = imgidx;
						break;
					}
				}

				/*foreach(Crownwood.DotNetMagic.Controls.TabPage page in g.Main._al_Documents) {
					if (page.Control.Equals(this)) {
						page.ImageIndex = imgidx;
						break;
					}
				}*/

				if (_isDirty)
					frmMain.stc_bIsProjectDirty = true;
			}
		}

		public override string Text {
			get {
				return _sTitle;
			}
			set {
				try {
					//_ParentTab.Title = value;
				} catch {}
				this.sbpFileName.Text = value;
				this._sTitle = value;
			}
		}
		#endregion

		public UCEditor(CProject.File file, bool isSaved) {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.g_curFile = file;
			this.isSaved = isSaved;

			if (!file.isText) {
				this._DropDownMake = new _dDropdownMake(DropdownMake);
				g.Config.LoadColorData(txtEditor);
			} else {
				// Disable completion for text files
				bDisableAllCompletion = true;
				bDisableAdvancedCompletion = true;
				cboCurFileFuncs.Visible = false;
			}

			// If we're a foreign file, disable "advanced" completion,
			// (so basically, anything not engine-function)
			if (file.isForeign) {
				bDisableAdvancedCompletion = true;
				cboCurFileFuncs.Visible = false;
			}

			//this.Owner = g.Main;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			this.tmrErrorMonitor.Enabled = false;
			this.tmrPosNotify.Enabled = false;

			if( disposing ) {
				if(components != null) {
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
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEditor));
			this.txtEditor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.ctxEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuCt_Cut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCt_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCt_Paste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuCt_GoToDef = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCt_CommentSel = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCt_UncommentSel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuCt_ToggleBreak = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCt_BreakProps = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuCt_Reference = new System.Windows.Forms.ToolStripMenuItem();
			this.ilMainMenu = new System.Windows.Forms.ImageList(this.components);
			this.ilToolbar = new System.Windows.Forms.ImageList(this.components);
			this.sbMain = new System.Windows.Forms.StatusBar();
			this.sbpRow = new System.Windows.Forms.StatusBarPanel();
			this.sbpCol = new System.Windows.Forms.StatusBarPanel();
			this.sbpTextInfo = new System.Windows.Forms.StatusBarPanel();
			this.sbpSelection = new System.Windows.Forms.StatusBarPanel();
			this.sbpFileName = new System.Windows.Forms.StatusBarPanel();
			this.tmrPosNotify = new System.Windows.Forms.Timer(this.components);
			this.tmrErrorMonitor = new System.Windows.Forms.Timer(this.components);
			this.tmrFileWatcher = new System.Windows.Forms.Timer(this.components);
			this.cboCurFileFuncs = new TSDev.ComboBoxEx();
			this.pnlFileChanged = new System.Windows.Forms.Panel();
			this.lnkCancelReload = new System.Windows.Forms.LinkLabel();
			this.lnkReloadContents = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.ctxEditor.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpRow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpCol)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpTextInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSelection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpFileName)).BeginInit();
			this.pnlFileChanged.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtEditor
			// 
			this.txtEditor.AllowDrop = true;
			this.txtEditor.BracketHighlightBackColor = System.Drawing.Color.Aqua;
			this.txtEditor.BracketHighlightBorderColor = System.Drawing.Color.Transparent;
			this.txtEditor.BracketHighlightForeColor = System.Drawing.Color.Black;
			this.txtEditor.BracketHighlightingInclusive = true;
			this.txtEditor.BracketHighlightingVisible = true;
			this.txtEditor.ContextMenuStrip = this.ctxEditor;
			this.txtEditor.DefaultContextMenuEnabled = false;
			this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEditor.Document = document1;
			this.txtEditor.LineNumberMarginBorderColor = System.Drawing.Color.Gainsboro;
			this.txtEditor.LineNumberMarginForeColor = System.Drawing.Color.DarkGray;
			this.txtEditor.LineNumberMarginVisible = true;
			this.txtEditor.Location = new System.Drawing.Point(0, 45);
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.PrintSettings.DocumentTitle = "netMercs TSDev - torque.netmercs.net";
			this.txtEditor.PrintSettings.LineNumberMarginVisible = true;
			this.txtEditor.Size = new System.Drawing.Size(704, 418);
			this.txtEditor.TabIndex = 0;
			this.txtEditor.UnicodeEnabled = true;
			this.txtEditor.WordWrapGlyphVisible = true;
			this.txtEditor.WordWrapMarginBorderDashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
			this.txtEditor.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.txtEditor_KeyTyped);
			this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
			this.txtEditor.DocumentTextChanged += new ActiproSoftware.SyntaxEditor.DocumentModificationEventHandler(this.txtEditor_DocumentTextChanged);
			this.txtEditor.KeyTyping += new ActiproSoftware.SyntaxEditor.KeyTypingEventHandler(this.txtEditor_KeyTyping);
			this.txtEditor.TokenMouseHover += new ActiproSoftware.SyntaxEditor.TokenMouseEventHandler(this.txtEditor_TokenMouseHover);
			this.txtEditor.SmartIndent += new ActiproSoftware.SyntaxEditor.SmartIndentEventHandler(this.txtEditor_SmartIndent);
			this.txtEditor.TokenMouseLeave += new ActiproSoftware.SyntaxEditor.TokenMouseEventHandler(this.txtEditor_TokenMouseLeave);
			this.txtEditor.DocumentIndicatorRemoved += new ActiproSoftware.SyntaxEditor.IndicatorEventHandler(this.txtEditor_DocumentIndicatorRemoved);
			this.txtEditor.IndicatorMarginClick += new ActiproSoftware.SyntaxEditor.IndicatorMarginClickEventHandler(this.txtEditor_IndicatorMarginClick);
			this.txtEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditor_KeyDown);
			this.txtEditor.TokenMouseEnter += new ActiproSoftware.SyntaxEditor.TokenMouseEventHandler(this.txtEditor_TokenMouseEnter);
			this.txtEditor.DocumentIndicatorAdded += new ActiproSoftware.SyntaxEditor.IndicatorEventHandler(this.txtEditor_DocumentIndicatorAdded);
			this.txtEditor.Trigger += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.txtEditor_Trigger);
			// 
			// ctxEditor
			// 
			this.ctxEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCt_Cut,
            this.mnuCt_Copy,
            this.mnuCt_Paste,
            this.toolStripMenuItem1,
            this.mnuCt_GoToDef,
            this.mnuCt_CommentSel,
            this.mnuCt_UncommentSel,
            this.toolStripMenuItem2,
            this.mnuCt_ToggleBreak,
            this.mnuCt_BreakProps,
            this.toolStripMenuItem3,
            this.mnuCt_Reference});
			this.ctxEditor.Name = "ctxEditor";
			this.ctxEditor.Size = new System.Drawing.Size(190, 220);
			this.ctxEditor.Opening += new System.ComponentModel.CancelEventHandler(this.ctxEditor_Opening);
			// 
			// mnuCt_Cut
			// 
			this.mnuCt_Cut.Image = global::TSDev.Properties.Resources.cut;
			this.mnuCt_Cut.Name = "mnuCt_Cut";
			this.mnuCt_Cut.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_Cut.Text = "&Cut";
			this.mnuCt_Cut.Click += new System.EventHandler(this.mnuCt_Cut_Click);
			// 
			// mnuCt_Copy
			// 
			this.mnuCt_Copy.Image = global::TSDev.Properties.Resources.copy;
			this.mnuCt_Copy.Name = "mnuCt_Copy";
			this.mnuCt_Copy.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_Copy.Text = "C&opy";
			this.mnuCt_Copy.Click += new System.EventHandler(this.mnuCt_Copy_Click);
			// 
			// mnuCt_Paste
			// 
			this.mnuCt_Paste.Image = global::TSDev.Properties.Resources.paste;
			this.mnuCt_Paste.Name = "mnuCt_Paste";
			this.mnuCt_Paste.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_Paste.Text = "&Paste";
			this.mnuCt_Paste.Click += new System.EventHandler(this.mnuCt_Paste_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuCt_GoToDef
			// 
			this.mnuCt_GoToDef.Image = global::TSDev.Properties.Resources.arrow_right_blue;
			this.mnuCt_GoToDef.Name = "mnuCt_GoToDef";
			this.mnuCt_GoToDef.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_GoToDef.Text = "&Go to Definition...";
			this.mnuCt_GoToDef.Click += new System.EventHandler(this.mnuCt_GoToDef_Click);
			// 
			// mnuCt_CommentSel
			// 
			this.mnuCt_CommentSel.Name = "mnuCt_CommentSel";
			this.mnuCt_CommentSel.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_CommentSel.Text = "&Comment Selection";
			this.mnuCt_CommentSel.Click += new System.EventHandler(this.mnuCt_CommentSel_Click);
			// 
			// mnuCt_UncommentSel
			// 
			this.mnuCt_UncommentSel.Name = "mnuCt_UncommentSel";
			this.mnuCt_UncommentSel.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_UncommentSel.Text = "&Uncomment Selection";
			this.mnuCt_UncommentSel.Click += new System.EventHandler(this.mnuCt_UncommentSel_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuCt_ToggleBreak
			// 
			this.mnuCt_ToggleBreak.Image = global::TSDev.Properties.Resources.stop;
			this.mnuCt_ToggleBreak.Name = "mnuCt_ToggleBreak";
			this.mnuCt_ToggleBreak.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_ToggleBreak.Text = "&Toggle Breakpoint";
			this.mnuCt_ToggleBreak.Click += new System.EventHandler(this.mnuCt_ToggleBreak_Click);
			// 
			// mnuCt_BreakProps
			// 
			this.mnuCt_BreakProps.Name = "mnuCt_BreakProps";
			this.mnuCt_BreakProps.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_BreakProps.Text = "Brea&kpoint Properties...";
			this.mnuCt_BreakProps.Click += new System.EventHandler(this.mnuCt_BreakProps_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuCt_Reference
			// 
			this.mnuCt_Reference.Image = global::TSDev.Properties.Resources.unknown;
			this.mnuCt_Reference.Name = "mnuCt_Reference";
			this.mnuCt_Reference.Size = new System.Drawing.Size(189, 22);
			this.mnuCt_Reference.Text = "&Reference...";
			this.mnuCt_Reference.Click += new System.EventHandler(this.mnuCt_Reference_Click);
			// 
			// ilMainMenu
			// 
			this.ilMainMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMainMenu.ImageStream")));
			this.ilMainMenu.TransparentColor = System.Drawing.Color.Transparent;
			this.ilMainMenu.Images.SetKeyName(0, "");
			this.ilMainMenu.Images.SetKeyName(1, "Assembly.ico");
			this.ilMainMenu.Images.SetKeyName(2, "PublicEvent.ico");
			// 
			// ilToolbar
			// 
			this.ilToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolbar.ImageStream")));
			this.ilToolbar.TransparentColor = System.Drawing.Color.Transparent;
			this.ilToolbar.Images.SetKeyName(0, "");
			this.ilToolbar.Images.SetKeyName(1, "");
			this.ilToolbar.Images.SetKeyName(2, "");
			this.ilToolbar.Images.SetKeyName(3, "");
			this.ilToolbar.Images.SetKeyName(4, "");
			this.ilToolbar.Images.SetKeyName(5, "");
			this.ilToolbar.Images.SetKeyName(6, "");
			this.ilToolbar.Images.SetKeyName(7, "");
			this.ilToolbar.Images.SetKeyName(8, "");
			this.ilToolbar.Images.SetKeyName(9, "");
			this.ilToolbar.Images.SetKeyName(10, "");
			this.ilToolbar.Images.SetKeyName(11, "");
			this.ilToolbar.Images.SetKeyName(12, "");
			this.ilToolbar.Images.SetKeyName(13, "");
			this.ilToolbar.Images.SetKeyName(14, "");
			this.ilToolbar.Images.SetKeyName(15, "");
			this.ilToolbar.Images.SetKeyName(16, "");
			this.ilToolbar.Images.SetKeyName(17, "");
			this.ilToolbar.Images.SetKeyName(18, "");
			this.ilToolbar.Images.SetKeyName(19, "");
			// 
			// sbMain
			// 
			this.sbMain.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sbMain.Location = new System.Drawing.Point(0, 463);
			this.sbMain.Name = "sbMain";
			this.sbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpRow,
            this.sbpCol,
            this.sbpTextInfo,
            this.sbpSelection,
            this.sbpFileName});
			this.sbMain.ShowPanels = true;
			this.sbMain.Size = new System.Drawing.Size(704, 22);
			this.sbMain.TabIndex = 2;
			this.sbMain.Visible = false;
			// 
			// sbpRow
			// 
			this.sbpRow.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpRow.Icon = ((System.Drawing.Icon)(resources.GetObject("sbpRow.Icon")));
			this.sbpRow.Name = "sbpRow";
			this.sbpRow.Text = "0";
			this.sbpRow.Width = 169;
			// 
			// sbpCol
			// 
			this.sbpCol.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpCol.Icon = ((System.Drawing.Icon)(resources.GetObject("sbpCol.Icon")));
			this.sbpCol.Name = "sbpCol";
			this.sbpCol.Text = "0";
			this.sbpCol.Width = 169;
			// 
			// sbpTextInfo
			// 
			this.sbpTextInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpTextInfo.Icon = ((System.Drawing.Icon)(resources.GetObject("sbpTextInfo.Icon")));
			this.sbpTextInfo.Name = "sbpTextInfo";
			this.sbpTextInfo.Text = "N/A";
			this.sbpTextInfo.Width = 169;
			// 
			// sbpSelection
			// 
			this.sbpSelection.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpSelection.Icon = ((System.Drawing.Icon)(resources.GetObject("sbpSelection.Icon")));
			this.sbpSelection.Name = "sbpSelection";
			this.sbpSelection.Text = "N/A";
			this.sbpSelection.Width = 169;
			// 
			// sbpFileName
			// 
			this.sbpFileName.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.sbpFileName.Name = "sbpFileName";
			this.sbpFileName.Width = 10;
			// 
			// tmrPosNotify
			// 
			this.tmrPosNotify.Interval = 10;
			this.tmrPosNotify.Tick += new System.EventHandler(this.tmrPosNotify_Tick);
			// 
			// tmrErrorMonitor
			// 
			this.tmrErrorMonitor.Enabled = true;
			this.tmrErrorMonitor.Interval = 1500;
			this.tmrErrorMonitor.Tick += new System.EventHandler(this.tmrErrorMonitor_Tick);
			// 
			// tmrFileWatcher
			// 
			this.tmrFileWatcher.Interval = 1000;
			this.tmrFileWatcher.Tick += new System.EventHandler(this.tmrFileWatcher_Tick);
			// 
			// cboCurFileFuncs
			// 
			this.cboCurFileFuncs.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboCurFileFuncs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboCurFileFuncs.DropDownHeight = 500;
			this.cboCurFileFuncs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCurFileFuncs.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboCurFileFuncs.ImageList = this.ilMainMenu;
			this.cboCurFileFuncs.IntegralHeight = false;
			this.cboCurFileFuncs.ItemHeight = 15;
			this.cboCurFileFuncs.Location = new System.Drawing.Point(0, 24);
			this.cboCurFileFuncs.Name = "cboCurFileFuncs";
			this.cboCurFileFuncs.Size = new System.Drawing.Size(704, 21);
			this.cboCurFileFuncs.Sorted = true;
			this.cboCurFileFuncs.TabIndex = 4;
			this.cboCurFileFuncs.SelectedIndexChanged += new System.EventHandler(this.cboCurFileFuncs_SelectedIndexChanged);
			// 
			// pnlFileChanged
			// 
			this.pnlFileChanged.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.pnlFileChanged.Controls.Add(this.lnkCancelReload);
			this.pnlFileChanged.Controls.Add(this.lnkReloadContents);
			this.pnlFileChanged.Controls.Add(this.label1);
			this.pnlFileChanged.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFileChanged.Location = new System.Drawing.Point(0, 0);
			this.pnlFileChanged.Name = "pnlFileChanged";
			this.pnlFileChanged.Size = new System.Drawing.Size(704, 24);
			this.pnlFileChanged.TabIndex = 5;
			this.pnlFileChanged.Visible = false;
			// 
			// lnkCancelReload
			// 
			this.lnkCancelReload.AutoSize = true;
			this.lnkCancelReload.Location = new System.Drawing.Point(395, 6);
			this.lnkCancelReload.Name = "lnkCancelReload";
			this.lnkCancelReload.Size = new System.Drawing.Size(40, 13);
			this.lnkCancelReload.TabIndex = 1;
			this.lnkCancelReload.TabStop = true;
			this.lnkCancelReload.Text = "Cancel";
			this.lnkCancelReload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCancelReload_LinkClicked);
			// 
			// lnkReloadContents
			// 
			this.lnkReloadContents.AutoSize = true;
			this.lnkReloadContents.Location = new System.Drawing.Point(348, 6);
			this.lnkReloadContents.Name = "lnkReloadContents";
			this.lnkReloadContents.Size = new System.Drawing.Size(41, 13);
			this.lnkReloadContents.TabIndex = 1;
			this.lnkReloadContents.TabStop = true;
			this.lnkReloadContents.Text = "Reload";
			this.lnkReloadContents.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReloadContents_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(339, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "The contents of this file have changed outside of this editor:";
			// 
			// UCEditor
			// 
			this.Controls.Add(this.txtEditor);
			this.Controls.Add(this.cboCurFileFuncs);
			this.Controls.Add(this.pnlFileChanged);
			this.Controls.Add(this.sbMain);
			this.Name = "UCEditor";
			this.Size = new System.Drawing.Size(704, 485);
			this.Tag = "";
			this.Load += new System.EventHandler(this.frmEditor_Load);
			this.ctxEditor.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpRow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpCol)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpTextInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSelection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpFileName)).EndInit();
			this.pnlFileChanged.ResumeLayout(false);
			this.pnlFileChanged.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void frmEditor_Load(object sender, System.EventArgs e) {
			//txtEditor.Document.LoadLanguageFromXml(Application.StartupPath + "\\highlight.xml", 0);
			//txtEditor.Document.SaveLanguageToXml(Application.StartupPath + "\\highlight.bin", 5000);
			//txtEditor.Document.LoadLanguageFromXml(Application.StartupPath + "\\highlight.bin", 5000);

			//g.Config.LoadColorData(txtEditor);

			// Don't apply syntax highlighting (or anything) to plaintext files
			if (!g_curFile.isText) {
				txtEditor.Document.Language.SemanticParser = new CSemanticParser();
				txtEditor.Document.Outlining.Mode = OutliningMode.Automatic;

				// Initialize breakpoints and whatever
				InitIndicators();
			}

			if (g.IsDebugging)
				txtEditor.Document.ReadOnly = true;

			// Set the color of the context menu
			ctxEditor.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			// Set any "extra" menus on it
			if (g.CustomEditorContextMenus.Count > 0)
				ctxEditor.Items.Add(new ToolStripSeparator());

			foreach (ToolStripMenuItem custmenu in g.CustomEditorContextMenus)
				ctxEditor.Items.Add(custmenu);

			//GC.Collect(GC.MaxGeneration);
		}

		public bool PromptAndSave() {
			// Checks if the file is dirty, prompts the user to save
			// accordingly, and returns the user's choice as TRUE or 
			// FALSE

			if (this.isDirty) {
				DialogResult result = MessageBox.Show("Would you like to save changes to '" + this.g_curFile.RelativePath + "'?", "TSDev", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
				if (result == DialogResult.No)
					return true;
				else if (result == DialogResult.Cancel)
					return false;
				else
					return SaveFile();
			} else {
				return true;
			}
		}

		public void BreakpointRender() {
			// Render all active breakpoints in the file
			
			// Clear all breakpoint indicators first
			this.txtEditor.Document.Indicators.Clear("__halt");

			// Add each indicator
			foreach(CProject.Breakpoint brk in g.Project.ProjectBreaks) {
				if (brk.file != this.g_curFile)
					continue;

				if ((brk.PassCount != 0) || (brk.Conditional != "true")) {
					// Create a different indicator color
					try {
						txtEditor.Document.Indicators.Add(new CIndicators.HaltSpan(Color.DarkOrange, Color.White), txtEditor.Document.PositionToOffset(new Position(brk.LineNumber, 0)), txtEditor.Document.Lines[brk.LineNumber].Length);
					} catch {}
				} else {
					// Create a standard indicator color
					try {
						txtEditor.Document.Indicators.Add(new CIndicators.HaltSpan(), txtEditor.Document.PositionToOffset(new Position(brk.LineNumber, 0)), txtEditor.Document.Lines[brk.LineNumber].Length);
					} catch {}
				}
			}
		}

		public void DropdownMake() {
			this.cboCurFileFuncs.Items.Clear();

			//this.cboCurFileFuncs.Items.Add(new ImageComboBoxItem(2, "(Outlying Script)", 0));
			this.cboCurFileFuncs.Items.Add(new ComboBoxExItem("(Outlying Scripts)", 2));

			try {
				foreach(CProject.TokenKey tok in this.g_curFile.TokenList.Values) {
					//ImageComboBoxItem item = new ImageComboBoxItem(0, tok.FuncName, 0);
					ComboBoxExItem item = new ComboBoxExItem(tok.FuncName, 0);
					this.cboCurFileFuncs.Items.Add(item);
				}

                foreach (CProject.TokenObject tokobj in g.Project.TokenObjList.Values) {
                    foreach (CProject.TokenObject.ObjectDescr objdescr in tokobj.ObjectFunctions.Values) {
                        if (objdescr.FuncFile == g_curFile) {
                            //ImageComboBoxItem item = new ImageComboBoxItem(1, tokobj.ObjectName + "::" + objdescr.FuncName, 0);
							ComboBoxExItem item = new ComboBoxExItem(tokobj.ObjectName + "::" + objdescr.FuncName, 1);
                            this.cboCurFileFuncs.Items.Add(item);
                        }
                    }
                }
			} catch {}
			
			this.isDropdownSwitched = true;

			if (this.cboCurFileFuncs.Items.Count > 0)
				this.cboCurFileFuncs.SelectedIndex = 0;
		}

		public void DropdownSelectCurrent() {
			if (this.cboCurFileFuncs.Items.Count == 0)
				return;

			TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);
			if (!ts.GoToPreviousToken("FunctionStartToken"))
				return;

			// Jump to next token which is probably whitespace
			ts.GoToNextToken();

            string func_call = "";

			while(ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken")
				ts.GoToNextToken();

            // Get this token's value
            func_call += txtEditor.Document.GetTokenText(ts.CurrentToken);

            // Now move through the next coming tokens, and make sure it's either a) an identifier, or b) a member function
            // token.  If it's neither, we need to break
            while (ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken" ||
                ts.CurrentToken.Key == "MemberToken" || ts.CurrentToken.Key == "IdentifierToken") {
                ts.GoToNextToken();

                if (ts.CurrentToken.Key == "MemberToken") {
                    func_call += "::";
                    //ts.GoToNextToken();
                    continue;
                } else if (ts.CurrentToken.Key == "IdentifierToken") {
                    func_call += txtEditor.Document.GetTokenText(ts.CurrentToken);
                    break;          // Break on the identifier
                }
            }

			for(int i = 0; i < this.cboCurFileFuncs.Items.Count; i++) {
				if (this.cboCurFileFuncs.Items[i].ToString() == func_call) {
					if (this.cboCurFileFuncs.SelectedIndex != i) {


						isDropdownSwitched = true;

						try {
							this.cboCurFileFuncs.SelectedIndex = i;
						} catch {
							isDropdownSwitched = false;
						}
					}
					return;
				}
			}

            // Null the display; we're not in a function
			isDropdownSwitched = true;

			try {
				this.cboCurFileFuncs.SelectedIndex = 0;
			} catch {
				isDropdownSwitched = false;
			}

            
		}

		public bool SaveFile() {
			// Saves the current file, if it's named
			if (this.isSaved) {
				return CommitSave(this.g_curFile);
			} else {
				CProject.File new_fn;
				if ((new_fn = PromptForSave()) == null)
					return false;
				else
					return CommitSave(new_fn);
			}
		}

		public CProject.File PromptForSave() {
			SaveFileDialog save = new SaveFileDialog();
			
			save.Title = "Save File As";
			save.OverwritePrompt = true;
			save.InitialDirectory = g.Project.ProjectPath;
			save.ValidateNames = true;
			save.CheckPathExists = true;
			save.Filter = "TorqueScript Files (*.cs)|*.cs|User Interface Scripts (*.gui)|*.gui|netMercs TorqueScript Files (*.ns)|*.ns|All Files (*.*)|*.*";

			DialogResult result = save.ShowDialog(this);

			if (result == DialogResult.Cancel) {
				return null;
			}
			
			string full_file = save.FileName;

			try {
				txtEditor.Document.SaveFile(full_file, LineEndStyle.CarriageReturnNewline);
			} catch (Exception exc) {
				MessageBox.Show("An error occurred while attempting to save your file:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}

			txtEditor.Document.Modified = false;
		getrelpath:

			string relative_path = CProject.PathGetRelative(full_file, g.Project.ProjectPath);
			string filename = Path.GetFileName(full_file);

			if (relative_path == "") {
				result = MessageBox.Show(this, "Fatal: Failed to retrieve relative path to saved file.", "Critical Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);

				if (result == DialogResult.Abort)
					return null;
				else if (result == DialogResult.Retry)
					goto getrelpath;
				else if (result == DialogResult.Ignore)
					MessageBox.Show(this, "Ignoring error.  Please note, project definitions may become corrupt.", "Ignore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			CProject.File file = new CProject.File(filename, relative_path, null);
			g.Project.FileList.Add(file);
			g.Main.InitProject();

			this.g_curFile = file;
			this.Text = filename;
			this.isDirty = false;
			this.isSaved = true;
			this._ParentTab.Title = filename;
			this._ParentTab.ToolTip = relative_path;

			g.Main.InitExplorer();

			return file;

		}

		public bool CommitSave(CProject.File file) {

			Directory.SetCurrentDirectory(g.Project.ProjectPath);
			try {
				// Set the isPendingReload flag
				g_curFile.isPendingReload = true;
				txtEditor.Document.SaveFile(file.RelativePath, LineEndStyle.CarriageReturnNewline);
			} catch (Exception exc) {
				MessageBox.Show("An error occurred while attempting to save your file:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				// Reset the pending reload
				g_curFile.isPendingReload = false;
				return false;
			}

			this.isDirty = false;

			// Tokenize file
			CProject.TokenizerQueue tq = new CProject.TokenizerQueue();
			tq.code = txtEditor.Document.Text;
			tq.file = this.g_curFile;

			g.Project._Queue(tq);

			
			DropdownMake();
			DropdownSelectCurrent();

			// PLUGINS
			foreach (CWPlugin plugin in g.Plugins) {
				try {
					plugin.Plugin.CWFileAfterSave(g_curFile.ToCWFile());
				} catch (Exception exc) {
					g.PluginException(exc, plugin);
				}
			}

			return true;
		}

		private void InitIndicators() {
			if (g.Project == null)
				return;

			foreach(string indline in this.g_curFile.IndicatorList) 
				txtEditor.Document.Indicators.Add(new CIndicators.BookmarkIndicator(Convert.ToInt32(indline)), Convert.ToInt32(indline));
		}

		private void txtEditor_KeyTyped(object sender, ActiproSoftware.SyntaxEditor.KeyTypedEventArgs e) {
			// Check error squiggles and clear them if we're doing any typing
			if (g.Config.b_Err_WhileTyping) {
				if (this.txtEditor.Document.Indicators.Contains("__error"))
					this.txtEditor.Document.Indicators.Clear("__error");

				this._LastMoved = true;
			}

			
			if (e.KeyData == Keys.Escape || e.KeyData == Keys.Tab) {
				if (txtEditor.IntelliPrompt.InfoTip.Visible == true)
					txtEditor.IntelliPrompt.InfoTip.Hide();
			} else if (e.KeyData == Keys.Enter) {
				if (txtEditor.IntelliPrompt.InfoTip.Visible == true)
					txtEditor.IntelliPrompt.InfoTip.Hide();
			}
		}

		private void txtEditor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
				return;

			TextStream curstr = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);
			
			if (e.KeyCode == Keys.F4) {
				// Find the previous variable, if there is any:
				if (bDisableAdvancedCompletion)
					return;

				TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);
				string var = "";

				if (ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken") {
					var = txtEditor.Document.GetTokenText(ts.CurrentToken);
				} else {
					while(ts.GoToPreviousToken()) {
						if (ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken") {
							var = txtEditor.Document.GetTokenText(ts.CurrentToken);
							break;
						} else if (ts.CurrentToken.Key != "WhiteSpaceToken") {
							var = "";
							break;
						}
					}
				}

				// Show the intellisense box
				Rectangle rect = txtEditor.SelectedView.GetCharacterBounds(txtEditor.SelectedView.Selection.StartOffset);
				Point scrloc = txtEditor.PointToScreen(new Point(rect.X, rect.Y));

				frmIntellisense fIntellisense = new frmIntellisense(this, var, rect.Height);
				fIntellisense.Owner = g.Main;

				fIntellisense.Location = new Point(scrloc.X, scrloc.Y + rect.Height);
				fIntellisense.Show();
			}
			if (e.KeyCode == Keys.F3) {
				if (bDisableAllCompletion)
					return;

				// Display snippets
				bool asdf = txtEditor.IntelliPrompt.CodeSnippets.ShowInsertSnippetPopup(txtEditor.Caret.Offset,
					"Insert Snippet:", new CodeSnippetFolder("Root", Application.StartupPath + "\\snippets"), CodeSnippetTypes.Expansion);

				//MessageBox.Show(asdf.ToString());
				return;
			}
			
			

			// Take care of Ctrl+Shift+Space completion
			if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.Space) {
				txtEditor.IntelliPrompt.InfoTip.Info.Clear();
				
				//string cc = parser.CodeCompletion(txtEditor.Document.Lines[txtEditor.SelectedView.Selection.StartPosition.Line].Text, txtEditor.SelectedView.Selection.StartPosition.Character, 1);
				CAutoComplete.ACRet ret = parser.CodeCompletion(txtEditor.Document.Lines[txtEditor.SelectedView.Selection.StartPosition.Line].Text, txtEditor.SelectedView.Selection.StartPosition.Character, 1);

				InfopopCollection output;
				if ((output = this.GetVarInfopop(ret.VariableName, ret.FunctionName, -1, txtEditor.SelectedView.Selection.StartOffset)).Count > 0) {
					if (bDisableAdvancedCompletion)
						return;

					foreach(Infopop pop in output)
						txtEditor.IntelliPrompt.InfoTip.Info.Add(pop.FinalDisplay);
				} else if ((output = this.GetObjectInfopop(ret.ObjectName, ret.FunctionName)).Count > 0) {
					foreach(Infopop pop in output)
						txtEditor.IntelliPrompt.InfoTip.Info.Add(pop.FinalDisplay);
				} else if ((output = this.GetCustomInfopop(ret.FunctionName)).Count > 0) {
					foreach(Infopop pop in output)
						txtEditor.IntelliPrompt.InfoTip.Info.Add(pop.FinalDisplay);
				}

				if (output == null || output.Count == 0)
					return;

				txtEditor.IntelliPrompt.InfoTip.Show(txtEditor.SelectedView.Selection.EndOffset);
				return;
			}

			// If we're triggering any editors, now's the time to
			// do so
			foreach (EditorTrigger trigger in g.EditorTriggers) {
				if (trigger.TriggerKeys == e.KeyCode && trigger.TriggerModifiers == e.Modifiers) {
					// Are we triggering this now, or whenever?
					if (trigger.TriggerWhen == TriggerStateWhen.AnyState)
						trigger.plugin.CWEditorTriggerFire(g_curFile.ToCWFile(), trigger.TriggerName);
					else if (curstr.CurrentToken.LexicalState.Key == trigger.TriggerWhen.ToString())
						trigger.plugin.CWEditorTriggerFire(g_curFile.ToCWFile(), trigger.TriggerName);
				}
			}
		}

		private void PopulateMemList(IntelliPromptMemberList ml) {
			/*
			 * iec.Add(new IntellicodeEntry(func.func_name,
					"<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> (<i>" + func.func_params.Replace("<", "&lt;") + "</i>)" +
					((func.func_descr == "") ? "" : "<br />" + func.func_descr),
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
					func.func_name, ""));
			 */

			// Grab all the functions in the project
			foreach (CProject.File file in g.Project.FileList) {
				foreach (CProject.TokenKey tok in file.TokenList.Values) {
					ml.Add(new IntelliPromptMemberListItem(
						tok.FuncName,
						Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.InternalMethod),
						"<b>" + tok.FuncName + "</b> (<i>" + String.Join(", ", tok.FuncParams).Replace("<", "&lt;") + "</i>)" +
						((tok.FuncDescr == null || tok.FuncDescr == "") ? "" : "<br />" + tok.FuncDescr),
						tok.FuncName, ""));
				}
			}

			// Grab all the functions from the engine
			foreach (CAutoComplete.ACEntry ac in frmMain.ac.ReturnAll().Values) {
				ml.Add(new IntelliPromptMemberListItem(
					ac.UnformattedName[0].ToString(),
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
					"<b>" + ac.FormattedName[0].ToString() + "</b> (<i>" + ac.FormattedParameters[0].ToString() + "</i>)" +
					((ac.FormattedDescription[0].ToString() != "") ? "<br />" + ac.FormattedDescription[0].ToString().Replace("<", "&lt;") : ""),
					ac.UnformattedName[0].ToString(), ""));
			}

			// Grab all the custom objects defined
			foreach (CProject.TokenObject tokobj in g.Project.TokenObjList.Values) {
				ml.Add(new IntelliPromptMemberListItem(
					tokobj.ObjectName,
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateClass),
					"<b>class</b> " + tokobj.ObjectName + " " + ((tokobj.ObjectType != "") ? "(Inherits <u>" + tokobj.ObjectType + "</u>)" : ""),
					tokobj.ObjectName,
					""));
			}

			// Grab all the variables thus far defined
			TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.CurrentDocumentLine.EndOffset);

			ArrayList donevars = new ArrayList();

			while (ts.CurrentToken.Key != "FunctionStartToken") {
				if (!ts.GoToPreviousToken())
					break;

				if (donevars.Contains(txtEditor.Document.GetTokenText(ts.CurrentToken)))
					continue;

				if (ts.CurrentToken.Key == "LocalVariableToken") {
					ml.Add(new IntelliPromptMemberListItem(
						txtEditor.Document.GetTokenText(ts.CurrentToken),
						Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateField),
						"Local Variable <b>" + txtEditor.Document.GetTokenText(ts.CurrentToken) + "</b>",
						txtEditor.Document.GetTokenText(ts.CurrentToken), ""));
				} else if (ts.CurrentToken.Key == "GlobalVariableToken") {
					ml.Add(new IntelliPromptMemberListItem(
						txtEditor.Document.GetTokenText(ts.CurrentToken),
						Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField),
						"Global Variable <b>" + txtEditor.Document.GetTokenText(ts.CurrentToken) + "</b>",
						txtEditor.Document.GetTokenText(ts.CurrentToken), ""));
				}

				donevars.Add(txtEditor.Document.GetTokenText(ts.CurrentToken));
			}

			// Add some keywords and operators
			ml.Add(new IntelliPromptMemberListItem("function", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("for", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("if", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("break", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("default", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("switch", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("case", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("return", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("else", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("new", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("datablock", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("package", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("or", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("switch$", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("while", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("continue", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("true", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));
			ml.Add(new IntelliPromptMemberListItem("false", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)));

			ml.Add(new IntelliPromptMemberListItem("SPC", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Operator)));
			ml.Add(new IntelliPromptMemberListItem("TAB", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Operator)));
			ml.Add(new IntelliPromptMemberListItem("NL", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Operator)));
			ml.Add(new IntelliPromptMemberListItem("%", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Operator)));
			ml.Add(new IntelliPromptMemberListItem("$=", Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.Operator)));
		}

		public bool IsLetter(char key) {
			if (((int)key >= 65 && (int)key <= 90) || ((int)key >= 97 && (int)key <= 122))
				return true;
			else
				return false;
		}

		
		public bool IsVar(char op, Keys mods) {
			if (op == '%' || op == '$')
				return true;
			else
				return false;
		}

		public static string StripBadChars(string instr) {
			for (int i = 0; i <= 31; i++)
				instr = instr.Replace((char)i, '\0');

			instr = instr.Replace("<", "&lt;");
			instr = instr.Replace("\0", "<u>?</u>");
			return instr;
		}

		private void txtEditor_TokenMouseHover(object sender, ActiproSoftware.SyntaxEditor.TokenMouseEventArgs e) {
			if (bDisableAllCompletion)
				return;

			try {
				if ((e.Token.Key == "GlobalVariableToken" || e.Token.Key == "LocalVariableToken") && g.IsDebugging == true) {
					if (bDisableAdvancedCompletion)
						return;

					if (!g.IsBroken) {
						e.ToolTipText = "(variable) [Not in Break Mode]";
					} else {
						if (frmMain.stc_DebugCurTokVal != "") {
							try {
								e.ToolTipText =  frmMain.stc_DebugCurTokVal;
							} catch {
								e.ToolTipText = "(Variable contains binary data)";
							}
						}
					}
					return;
				} else if ((e.Token.Key == "LocalVariableToken" || e.Token.Key == "GlobalVariableToken") && g.IsDebugging == false) {
					if (bDisableAdvancedCompletion)
						return;

					CAutoComplete.ClassEntry cls = this.GetObjDeclare(e.Token.StartOffset, txtEditor.Document.GetTokenText(e.Token));
					if (cls != null)
						e.ToolTipText = "declared <b><u>" + cls.ClassName + "</u></b> " + ((cls.ClassInheritsFrom == "")? "" : (" inherits " + cls.ClassInheritsFrom)) + "<br/>" + cls.func_list.Count.ToString() + " function(s), " + cls.prop_list.Count.ToString() + " properties";
				} else if (e.Token.Key == "IdentifierToken") {
					string text = this.TriggerInfopop(e.Token.StartOffset, true);

					if (text != "")
						e.ToolTipText = text;

					return;
				}
			} catch {}
		}

		public void CommentSel() {
			txtEditor.SelectedView.CommentLines("//");
		}

		public void UncommentSel() {
			txtEditor.SelectedView.UncommentLines("//");
		}

		

		private string PromptSaveFilename(string title, string filter) {
			SaveFileDialog save = new SaveFileDialog();
			save.CheckPathExists = true;
			save.OverwritePrompt = true;
			save.Filter = filter;
			save.InitialDirectory = g.Project.ProjectPath;
			save.Title = title;

			DialogResult result = save.ShowDialog();

			if (result == DialogResult.Cancel)
				return "";
			else
				return save.FileName;
		}

		private void txtEditor_TextChanged(object sender, System.EventArgs e) {
			
		}

		public void OpenFile() {
			// Set the current LastWritten time to the correct one
			LastWritten = File.GetLastWriteTime(Path.GetFullPath(g_curFile.RelativePath));
			
			// Enable the check timer
			tmrFileWatcher.Enabled = true;

			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			txtEditor.Document.LoadFile(this.g_curFile.RelativePath);
			this.Text = this.g_curFile.RelativePath;

			if (this._ParentTab != null)
				this._ParentTab.Title = this.g_curFile.SimpleName;

			this.isDirty = false;
			//GC.Collect(GC.MaxGeneration);
		}

		private void txtEditor_DocumentTextChanged(object sender, ActiproSoftware.SyntaxEditor.DocumentModificationEventArgs e) {
			if (this.isDirty == false) {
				this.isDirty = true;
				
				/*foreach(Plugins.IPlugin plugin in frmMain.stc_Plugins)
					plugin.onDocumentDirty(CPlugins.GetFileSpec(this.g_curFile));*/
			}
		}

		private void mnuCt_Cut_Click(object sender, System.EventArgs e) {
			txtEditor.SelectedView.CutToClipboard();
		}

		private void mnuCt_Copy_Click(object sender, System.EventArgs e) {
			txtEditor.SelectedView.CopyToClipboard();
		}

		private void mnuCt_Paste_Click(object sender, System.EventArgs e) {
			txtEditor.SelectedView.PasteFromClipboard();
		}

		private void mnuCt_CommentSel_Click(object sender, System.EventArgs e) {
			this.CommentSel();
		}

		private void mnuCt_UncommentSel_Click(object sender, System.EventArgs e) {
			this.UncommentSel();
		}

		private void mnuCt_GoToDef_Click(object sender, System.EventArgs e) {
			Token tok = txtEditor.SelectedView.GetCurrentToken();
			TextStream ts = txtEditor.Document.GetTextStream(tok.StartOffset);

			if (tok.Key == "IdentifierToken") {
				CProject.File file;
				ts.GoToPreviousToken("IdentifierToken");

				// Get the previous token for object-finding fun
				string prevtok = txtEditor.Document.GetTokenText(ts.CurrentToken);

				int file_offset = frmMain.parser.FindToken(txtEditor.Document.GetTokenText(tok), prevtok, out file);

				if (file_offset == -1) {
					MessageBox.Show(this, "No definition exists for this identifier.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				g.Main.OpenFile(file, file_offset, true);
			}
		}

		private bool TriggerMemberlist(int start_offset, int type) {
			TextStream ts = txtEditor.Document.GetTextStream(start_offset);

			// Find the last identifier token
			while(ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken" ||
					ts.CurrentToken.Key == "OpenParenthesisToken" || ts.CurrentToken.Key == "EndOfDocumentToken" ||
					ts.CurrentToken.Key == "MemberToken" )
				ts.GoToPreviousToken();

			if (type == 1) {
				if (ts.CurrentToken.Key != "IdentifierToken")
					return false;
			} else if (type == 2) {
				if (!(ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken"))
					return false;
			}

			string ident_tok = txtEditor.Document.GetTokenText(ts.CurrentToken);

			txtEditor.IntelliPrompt.MemberList.Clear();
			txtEditor.IntelliPrompt.MemberList.ImageList = SyntaxEditor.ReflectionImageList;
			
			if (type == 2)
				GetVariableMemberList(txtEditor.Document.GetTokenText(ts.CurrentToken), start_offset);
			else if (type == 1)
				GetObjectMemberList(txtEditor.Document.GetTokenText(ts.CurrentToken), start_offset);

			return true;

		}

		private void GetObjectMemberList(string obj, int start_offset) {
			// Grab the memberlist for the said object
			if (!g.Project.TokenObjList.ContainsKey(obj.ToLower()))
				return;

            IntellicodeEntryCollection iec = new IntellicodeEntryCollection();

			CProject.TokenObject tokobj = (CProject.TokenObject)g.Project.TokenObjList[obj.ToLower()];
			
			foreach(CProject.TokenObject.ObjectDescr funcs in tokobj.ObjectFunctions.Values) {
				string fparams = "";
				if (funcs.FuncParams == null || funcs.FuncParams.Length == 0)
					fparams = "void";
				else
					fparams = String.Join(", ", funcs.FuncParams);

				iec.Add(new IntellicodeEntry(funcs.FuncName, 
					"<b>" + funcs.FuncName + "</b> (<i>" + fparams + "</i>)" +
					((funcs.FuncDescr == "") ? "" : "<br />" + funcs.FuncDescr), 
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.InternalMethod),
					funcs.FuncName, ""));

				/*txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(funcs.FuncName,
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.InternalMethod),
					"<b>" + funcs.FuncName + "</b> (<i>" + fparams + "</i>)" +
					((funcs.FuncDescr == "") ? "" : "<br/>" + funcs.FuncDescr),
					funcs.FuncName, ""));*/
			}

			// Search the other classes
			if (tokobj.ObjectType != null) {
				CAutoComplete.ClassEntry cls = ac.ReturnClassObject(tokobj.ObjectType.ToLower());
				if (cls != null) {
					foreach(CAutoComplete.ClassEntry.FuncEntry func in cls.func_list.Values) {
						iec.Add(new IntellicodeEntry(func.func_name, 
							"<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> (<i>" +
							func.func_params.Replace("<", "&lt;") +  "</i>)" +
							((func.func_descr == "") ? "" : "<br />" + func.func_descr),
							Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
							func.func_name, ""));
						/*txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(func.func_name,
							Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
							"<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> (<i>" +
							func.func_params.Replace("<", "&lt;") + "</i>)" +
							((func.func_descr == "") ? "" : "<br/>" + func.func_descr),
							func.func_name, ""));*/
					}

					foreach(CAutoComplete.ClassEntry.PropEntry prop in cls.prop_list.Values) {
						iec.Add(new IntellicodeEntry(prop.prop_name, " " + prop.prop_type + " <b>" + prop.prop_name + "</b>",
							Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty),
							prop.prop_name, ""));
					}
				}
			}

			// Run it through to the plugins
			/*foreach(IPlugin plugin in frmMain.stc_Plugins)
				plugin.onBeforeDisplayIntellicode(iec, IntellicodeType.DeclaredObjectCompletion, "", obj);*/

			// Add it to the intelliprompt list
			foreach(IntellicodeEntry entry in iec)
				txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(entry.EntryName,
					entry.EntryIconIndex, entry.EntryDescription, entry.EntryInsertBefore, entry.EntryInsertAfter));

			txtEditor.IntelliPrompt.MemberList.Show();
		}

		private void GetVariableMemberList(string var, int start_offset) {
			// Grab the memberlist for the said variable
			CAutoComplete.ClassEntry cls = GetObjDeclare(start_offset, var);

			if (cls == null)
				return;

			IntellicodeEntryCollection iec = new IntellicodeEntryCollection();

			foreach(CAutoComplete.ClassEntry.FuncEntry func in cls.func_list.Values) {
				iec.Add(new IntellicodeEntry(func.func_name,
					"<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> (<i>" + func.func_params.Replace("<", "&lt;") + "</i>)" +
					((func.func_descr == "") ? "" : "<br />" + func.func_descr),
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
					func.func_name, ""));

				/*txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(func.func_name,
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod),
					"<u>" + func.func_ret + "</u> " + "<b>" + func.func_name + "</b> (<i>" + func.func_params.Replace("<", "&lt;") + "</i>)" +
					((func.func_descr == "") ? "" : "<br/>" + func.func_descr), func.func_name, ""));*/
			}

			foreach(CAutoComplete.ClassEntry.PropEntry prop in cls.prop_list.Values) {
				iec.Add(new IntellicodeEntry(prop.prop_name, prop.prop_type + " " + prop.prop_name,
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty),
					prop.prop_name, ""));

				/*txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(prop.prop_name,
					Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty),
					prop.prop_type + " " + prop.prop_name, prop.prop_name, ""));*/
			}

			// Run it through to the plugins
			/*foreach(IPlugin plugin in frmMain.stc_Plugins)
				plugin.onBeforeDisplayIntellicode(iec, IntellicodeType.DeclaredVariableCompletion, var, "");*/

			// Add it to the intelliprompt list
			foreach(IntellicodeEntry entry in iec)
				txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(entry.EntryName,
					entry.EntryIconIndex, entry.EntryDescription, entry.EntryInsertBefore, entry.EntryInsertAfter));

			txtEditor.IntelliPrompt.MemberList.Show();
		}

		private string TriggerInfopop(int start_offset, bool hover) {
			TextStream ts = txtEditor.Document.GetTextStream(start_offset);
			
			// We need to find out what the previous token was to figure out
			// what we're constructing (member-completion or otherwise)
			while(ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken" ||
				ts.CurrentToken.Key == "OpenParenthesisToken" || ts.CurrentToken.Key == "EndOfDocumentToken" ||
				ts.CurrentToken.Key == "CloseParenthesisToken" || ts.CurrentToken.Key == "DefaultToken")
				ts.GoToPreviousToken();

			// OK we've reached this... Make sure we've got an ident token;
			// if not, we should not even be here.
			if (ts.CurrentToken.Key != "IdentifierToken")
				return "";

			string ident_tok = txtEditor.Document.GetTokenText(ts.CurrentToken);
			ts.GoToPreviousToken();
			
			// Now that we have the identifier, we need to search for the token
			// *before* the identifier to see what kind it is:
			while(ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken" ||
				ts.CurrentToken.Key == "MemberToken" || ts.CurrentToken.Key == "EndOfDocumentToken" )
				ts.GoToPreviousToken();

			this.txtEditor.IntelliPrompt.InfoTip.Info.Clear();

			InfopopCollection infopop;
			if (ts.CurrentToken.Key == "LocalVariableToken")
				infopop = GetVarInfopop(txtEditor.Document.GetTokenText(ts.CurrentToken), ident_tok, 1, start_offset);
			else if (ts.CurrentToken.Key == "GlobalVariableToken")
				infopop = GetVarInfopop(txtEditor.Document.GetTokenText(ts.CurrentToken), ident_tok, 2, start_offset);
			else if (ts.CurrentToken.Key == "IdentifierToken")
				infopop = GetObjectInfopop(txtEditor.Document.GetTokenText(ts.CurrentToken), ident_tok);
			else 
				infopop = GetCustomInfopop(ident_tok);

			if (infopop.Count == 0)
				return "";

			if (hover) {
				return infopop[0].FinalDisplay + ((infopop.Count > 1) ? "<br/>+" + Convert.ToString(infopop.Count - 1) + " overloads" : "");
			} else {
				foreach(Infopop pop in infopop)
					this.txtEditor.IntelliPrompt.InfoTip.Info.Add(pop.FinalDisplay);
				
				this.txtEditor.IntelliPrompt.InfoTip.Show(start_offset);
			}

			return "";
		}

		private InfopopCollection GetCustomInfopop(string function) {
			// Search user-defined functions or console exported functions
			InfopopCollection toret;
			toret = GetCustomInfopopUser(function);

			if (toret.Count == 0)
				return GetCustomInfopopBuiltin(function);
			else
				return toret;
		}

		private InfopopCollection GetCustomInfopopBuiltin(string function) {
			// Check the built-in function list
			if (!ac.HasParamObject(function.ToLower()))
				return new InfopopCollection();

			CAutoComplete.ACEntry acentry = ac.ReturnParamObject(function.ToLower());
			InfopopCollection ipc = new InfopopCollection();

			//string[] output = new string[acentry.FormattedParameters.Count];

			for (int i = 0; i < acentry.FormattedName.Count; i++) {
				string infotip = acentry.FormattedName[i].ToString() + " (" +
					acentry.FormattedParameters[i].ToString() + ")<br/>" +
					acentry.FormattedDescription[i].ToString();

				//output[i] = infotip;

				// Create the infopop collection
				ipc.Add(new Infopop(acentry.UnformattedName[i].ToString(), acentry.UnformattedParameters[i].ToString(),
					"", acentry.UnformattedDescription[i].ToString(), infotip));
			}

			// Execute some functions for the plugin:
			/*if (frmMain.stc_Plugins.Count > 0) {
				foreach(IPlugin plugin in frmMain.stc_Plugins)
					plugin.onBeforeDisplayInfopop(ipc, function, "", "", true);
			}*/
			

			//return output;
			return ipc;
		}

		private InfopopCollection GetCustomInfopopUser(string function) {
			// Check user-defined function definitions

			InfopopCollection ipc = new InfopopCollection();
			foreach(CProject.File file in g.Project.FileList) {
				if (!file.TokenList.ContainsKey(function.ToLower()))
					continue;

				CProject.TokenKey token = (CProject.TokenKey)file.TokenList[function.ToLower()];

				string paramlist = "";
				if (token.FuncParams == null)
					paramlist = "void";
				else
					paramlist = String.Join(", ", token.FuncParams);

				string infotip = "<b>" + token.FuncName + "</b> (<i>" + paramlist + "</i>)" +
					((token.FuncDescr == "") ? "" : "<br/>" + token.FuncDescr) +
					"<br/><span style=\"font-size: 7pt\">" +
					"Defined in " + file.RelativePath + "." + 
					"</span>";
				
				ipc.Add(new Infopop(token.FuncName, paramlist, "", token.FuncDescr, infotip));

				// Run through all the plugins
				/*if (frmMain.stc_Plugins.Count > 0) {
					foreach(IPlugin plugin in frmMain.stc_Plugins)
						plugin.onBeforeDisplayInfopop(ipc, token.FuncName, "", "", false);
				}*/
			}

			return ipc;
		}

		private InfopopCollection GetObjectInfopop(string obj, string function) {

			InfopopCollection ipc = new InfopopCollection();

			if (!g.Project.TokenObjList.ContainsKey(obj.ToLower()))
				return ipc;

			CProject.TokenObject tokobj = (CProject.TokenObject)g.Project.TokenObjList[obj.ToLower()];

			string infotip = "";

			if (tokobj.ObjectFunctions.ContainsKey(function.ToLower())) {
				// Check user-defined object functions first
				CProject.TokenObject.ObjectDescr func = (CProject.TokenObject.ObjectDescr)tokobj.ObjectFunctions[function.ToLower()];
				string paramlist = "";
			
				if (func.FuncParams == null || func.FuncParams.Length == 0)
					paramlist = "void";
				else
					paramlist = String.Join(", ", func.FuncParams);

				infotip = tokobj.ObjectName + "::<b>" + func.FuncName + "</b> (<i>" +
					paramlist + "</i>)" + 
					((func.FuncDescr == "") ? "" : "<br/>" + func.FuncDescr) +
					"<br/><span style=\"font-size: 7pt\">" +
					"Defined in " + func.FuncFile.RelativePath + "." +
					"</span>";

				ipc.Add(new Infopop(func.FuncName, paramlist, "", func.FuncDescr, infotip));

				// PLUGINS
				/*foreach (CWPlugin plugin in g.Plugins) {
					try {
						plugin.Plugin.CWBeforeDisplayInfopop(this.g_curFile.ToCWFile(), ref ipc);
					} catch (Exception exc) {
						g.PluginException(exc, plugin);
					}
				}*/
				
			} else {
				// Check built-in object functions next
				if (tokobj.ObjectType == null)
					return ipc;

				if (!ac.HasClassObject(tokobj.ObjectType.ToLower()))
					return ipc;

				CAutoComplete.ClassEntry cls = ac.ReturnClassObject(tokobj.ObjectType.ToLower());

				if (!cls.func_list.ContainsKey(function.ToLower()))
					return ipc;

				CAutoComplete.ClassEntry.FuncEntry func = (CAutoComplete.ClassEntry.FuncEntry)cls.func_list[function.ToLower()];

				infotip = "<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b>" +
					" (<i>" + func.func_params.Replace("<", "&lt;") + "</i>)" +
					((func.func_descr == "") ? "" : "<br/> " + func.func_descr);

				ipc.Add(new Infopop(func.func_name, func.func_params, func.func_ret, func.func_descr, infotip));

				// PLUGINS
				/*foreach (CWPlugin plugin in g.Plugins) {
					try {
						plugin.Plugin.CWBeforeDisplayInfopop(g_curFile.ToCWFile(), ref ipc);
					} catch (Exception exc) {
						g.PluginException(exc, plugin);
					}
				}*/
				/*if (frmMain.stc_Plugins.Count > 0) {
					foreach(IPlugin plugin in frmMain.stc_Plugins)
						plugin.onBeforeDisplayInfopop(ipc, function, obj, "", true);
				}*/
			}

			return ipc;

		}

		private InfopopCollection GetVarInfopop(string variable, string function, int var_type, int startpos) {
			CAutoComplete.ClassEntry cls = GetObjDeclare(startpos, variable);
            
			if (cls == null)
				return new InfopopCollection();

			if (!cls.func_list.ContainsKey(function.ToLower()))
				return new InfopopCollection();

			InfopopCollection ipc = new InfopopCollection();

			CAutoComplete.ClassEntry.FuncEntry func = (CAutoComplete.ClassEntry.FuncEntry)cls.func_list[function.ToLower()];
			string infotip = "[" + cls.ClassName + "] <u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> " +
				"(<i>" + func.func_params.Replace("<", "&lt;") + "</i>)" +
				((func.func_descr == "") ? "" : "<br/>" + func.func_descr);

			ipc.Add(new Infopop(func.func_name, func.func_params, func.func_ret, func.func_descr, infotip));

			/*if (frmMain.stc_Plugins.Count > 0) {
				foreach(IPlugin plugin in frmMain.stc_Plugins)
					plugin.onBeforeDisplayInfopop(ipc, function, "", variable, false);
			}*/

			return ipc;
		}

		private void txtEditor_Trigger(object sender, ActiproSoftware.SyntaxEditor.TriggerEventArgs e) {
			if (bDisableAllCompletion)
				return;

			try {
				if (e.Trigger.Key == "MemberListTrigger") {
					if (g.Config.b_AC_ObjectML)
						this.TriggerMemberlist(txtEditor.SelectedView.GetCurrentToken().StartOffset, 1);
				} else if (e.Trigger.Key == "ObjectMemberTrigger") {
					if (g.Config.b_AC_VariableML) {
						if (!this.TriggerMemberlist(txtEditor.SelectedView.GetCurrentToken().StartOffset, 2))
							this.TriggerMemberlist(txtEditor.SelectedView.GetCurrentToken().StartOffset, 1);
					}
				} else if (e.Trigger.Key == "InfopopOpenTrigger") {
					if (g.Config.b_AC_Infopop)
						this.TriggerInfopop(txtEditor.SelectedView.GetCurrentToken().StartOffset, false);
				} else if (e.Trigger.Key == "InfopopCloseTrigger") {
					txtEditor.IntelliPrompt.InfoTip.Hide();
				} else if (e.Trigger.Key == "AutoOutdentTrigger") {
					if (g.Config.b_Ed_AutoIndent) {
						TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.GetCurrentToken().StartOffset);
						ts.GoToPreviousToken();
					
						// Don't outdent if the { is on the same line as the }
						while(ts.CurrentToken.Key != "LineTerminatorToken") {
							if (ts.CurrentToken.Key == "OpenCurlyBraceToken")
								return;

							if (!ts.GoToPreviousToken())
								break;
						}

						txtEditor.SelectedView.Outdent();
						/*foreach(Plugins.IPlugin plugin in frmMain.stc_Plugins)
							plugin.onEditorSmartIndentExecute();*/
					}
				}
			} catch { return; }
		}

		private void txtEditor_SmartIndent(object sender, ActiproSoftware.SyntaxEditor.SmartIndentEventArgs e) {
			if (bDisableAllCompletion)
				return;

			if (!g.Config.b_Ed_AutoIndent)
				return;

			TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.GetCurrentToken().StartOffset);
			
			int numindents = 0;

			while(ts.CurrentToken.Key == "WhitespaceToken" || ts.CurrentToken.Key == "LineTerminatorToken"
                || ts.CurrentToken.Key == "EndOfDocumentToken") {
				string text = txtEditor.Document.GetTokenText(ts.CurrentToken);
				if (txtEditor.Document.GetTokenText(ts.CurrentToken).StartsWith("\t")) {
					numindents = txtEditor.Document.GetTokenText(ts.CurrentToken).Split('\t').Length;
				}

				ts.GoToPreviousToken();
			}
			
			Token tok = ts.CurrentToken;

			if (tok.Key == "OpenCurlyBraceToken") {
				if (numindents <= e.IndentAmount) {
					e.IndentAmount++;
					e.PerformIndent = true;
				}
			}
		}

		private void txtEditor_IndicatorMarginClick(object sender, ActiproSoftware.SyntaxEditor.IndicatorMarginClickEventArgs e) {
			if (g_curFile.isText || g_curFile.isForeign) {
				MessageBox.Show("You can only add breakpoints to code files, or files currently in your project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			
			if (g.IsDebugging && g.IsBroken == false) {
				MessageBox.Show("You can only add/remove breakpoints in BREAK mode", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (g.Project.ProjectBreaks.HasBreakpointAt(this.g_curFile, e.LineIndex)) {
				g.Project.ProjectBreaks.RemoveAtLine(this.g_curFile, e.LineIndex);
			} else {
				g.Project.ProjectBreaks.Add(new CProject.Breakpoint(this.g_curFile, e.LineIndex));
			}

			BreakpointRender();
			//if (txtEditor.Document.Lines[e.LineIndex].Indicators.Contains("__halt"))
			//	SetHalt(-1, e.LineIndex, false, true);
			//else
			//	SetHalt(-1, e.LineIndex, false, false);
		}

		/*public void SetHalt(int prevline, int line, bool clearall, bool remove) {
			/*if (clearall) {
				CProject.BreakpointCollection temp = (BreakpointCollection)g.Project.ProjectBreaks.Clone();

				// Clear all indicators for the file
				foreach(CProject.Breakpoint brk in temp) {
					if (brk.file == this.g_curFile && brk.LineNumber == line)
						g.Project.ProjectBreaks.Remove(brk);
				}

				return;
			}

			if (remove) {
				// Clear the said breakpoint at "line"
				foreach(CProject.Breakpoint brk in g.Project.ProjectBreaks) {
					if (brk.file == this.g_curFile && brk.LineNumber == line) {
						g.Project.ProjectBreaks.Remove(brk);
						break;
					}
				}

				return;
			}

			if (line > -1) {
				// If there's any line to add
				if (prevline > -1) {
					// Remove a previous line fom the breaklist
					SetHalt(-1, prevline, false, true);
				}

				// Add the 
			}
			try {
				if (clearall)
					txtEditor.Document.Indicators.Clear("__halt");

				if (prevline > 0) // && txtEditor.Document.Lines[prevline].Indicators.Contains("__halt"))
					txtEditor.Document.Lines[prevline].Indicators.Clear("__halt");

				if (!remove) {
					if (!txtEditor.Document.Lines[line].Indicators.Contains("__halt")) {
						txtEditor.Document.Indicators.Add(new CIndicators.HaltSpan(), txtEditor.Document.Lines[line].StartOffset, txtEditor.Document.Lines[line].EndOffset - txtEditor.Document.Lines[line].StartOffset);
						if (g.IsDebugging) {
							string f_path = CProject.PathGetRelative(Path.GetFullPath(this.g_curFile.RelativePath), Path.GetDirectoryName(g.Project.DebugExe));
							frmMain.stc_DebugQueue.Add("BRKSET " + f_path.Substring(2, f_path.Length - 2).Replace("\\", "/") + " " + Convert.ToString(line + 1) + " false 0 true\n");
						}
					}
				} else {
					txtEditor.Document.Lines[line].Indicators.Clear("__halt");
					if (g.IsDebugging) {
						string f_path = CProject.PathGetRelative(Path.GetFullPath(this.g_curFile.RelativePath), Path.GetDirectoryName(g.Project.DebugExe));
						frmMain.stc_DebugQueue.Add("BRKCLR " + f_path.Substring(2, f_path.Length - 2).Replace("\\", "/") + " " + Convert.ToString(line + 1) + "\n");
					}
				}
			} catch {}
		}*/

		public void ToggleHalt() {
			if (g.IsDebugging && g.IsBroken == false) {
				MessageBox.Show("You can only add/remove breakpoints in BREAK mode", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (bDisableAdvancedCompletion || bDisableAllCompletion) {
				MessageBox.Show("You can only add breakpoints in code files, or files that are apart of your project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			if (g.Project.ProjectBreaks.HasBreakpointAt(this.g_curFile, txtEditor.SelectedView.CurrentDocumentLine.Index))
				g.Project.ProjectBreaks.RemoveAtLine(this.g_curFile, txtEditor.SelectedView.CurrentDocumentLine.Index);
			else
				g.Project.ProjectBreaks.Add(new CProject.Breakpoint(this.g_curFile, txtEditor.SelectedView.CurrentDocumentLine.Index));

			BreakpointRender();
		}

		public void DrawLineIndicator(int line) {
            txtEditor.Document.Indicators.Clear("__ip");		// Clear all instruction pointer indicators

			if (line == -1)
				return;

			int s_offset = txtEditor.Document.Lines[line].StartOffset;
			int f_offset = txtEditor.Document.Lines[line].EndOffset;

			// Add the new indicator
			try {
				txtEditor.Document.Indicators.Add(new CIndicators.InstrPtr(), s_offset, f_offset - s_offset);
			} catch { return; }

			// Parse all the tokens in the current function and request all the
			// variables:
			TextStream ts = txtEditor.Document.GetTextStream(txtEditor.Document.Lines[line].StartOffset);

			// Navigate to the beginning
			if (!ts.GoToPreviousToken("FunctionStartToken"))
				while(ts.GoToPreviousToken());

			ts.GoToNextToken();

			// Loop through all the tokens in the current function.
			ArrayList havequeried = new ArrayList();
			while(ts.CurrentToken.Key != "FunctionStartToken") {
				string finalquery = "";
				if (ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken") {
					while(ts.CurrentToken.Key == "IdentifierToken" || ts.CurrentToken.Key == "MemberToken" || ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken") {
						if (txtEditor.Document.GetTokenText(ts.CurrentToken).IndexOf("(") == -1)
							finalquery += txtEditor.Document.GetTokenText(ts.CurrentToken);

						if (!ts.GoToNextToken())
							break;
					}

					if (!havequeried.Contains(finalquery)) {
						frmMain.stc_DebugQueue.Add("EVAL !" + finalquery + " 0 " + finalquery + "\n");
						havequeried.Add(finalquery);
					}
				}

				if (!ts.GoToNextToken())
					break;
			}
		}



		public void BookmarkAdd() {
			if (!txtEditor.SelectedView.CurrentDocumentLine.Indicators.Contains("__bookmark")) {
				txtEditor.Document.Indicators.Add(new CIndicators.BookmarkIndicator(txtEditor.SelectedView.CurrentDocumentLine.Index), txtEditor.SelectedView.CurrentDocumentLine.Index);
			}
		}

		public void BookmarkDel() {
			if (txtEditor.SelectedView.CurrentDocumentLine.Indicators.Contains("__bookmark")) {
				txtEditor.SelectedView.CurrentDocumentLine.Indicators.Clear("__bookmark");
			}
		}

		public void BookmarkClearAll() {
			txtEditor.Document.Indicators.Clear("__bookmark");
		}

		public void BookmarkJump(int dir) {
			if (dir == -1)
				txtEditor.SelectedView.GotoPreviousLineIndicator("__bookmark");
			else
				txtEditor.SelectedView.GotoNextLineIndicator("__bookmark");
		}

		public void BeginRecordMacro() {
			if (txtEditor.MacroRecording.State == MacroRecordingState.Stopped) {
				g.Main.mnuMacro_BeginRec.Enabled = false;
				g.Main.mnuMacro_EndRec.Enabled = true;
				g.Main.mnuMacro_Dir.Enabled = false;
				txtEditor.MacroRecording.Record();
			}
		}

        public bool isRecordingMacro() {
            return (txtEditor.MacroRecording.State == MacroRecordingState.Recording);
        }

		public void StopRecordMacro() {
			if (txtEditor.MacroRecording.State == MacroRecordingState.Recording) {
				txtEditor.MacroRecording.Stop();
				g.Main.mnuMacro_BeginRec.Enabled = true;
				g.Main.mnuMacro_EndRec.Enabled = false;
				g.Main.mnuMacro_Dir.Enabled = true;

				MacroCommand lastcmd = txtEditor.MacroRecording.LastMacroCommand;

			getmacroname:
				string macro_name = frmMain.ShowPrompt("Macro Name", "Please type a name for this macro, or press cancel to discard macro.", "Enter macro name:");

				if (macro_name == "")
					return;

				if (g.Project.MacroList.ContainsKey(macro_name)) {
					MessageBox.Show(this, "The macro name you have specified is already in use. Please try again.", "TSDev", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					goto getmacroname;
				}

				CProject.Macro newmacro = new CProject.Macro(macro_name, txtEditor.MacroRecording.LastMacroCommand);				
				g.Project.MacroList.Add(macro_name, newmacro);
			}
		}

		
		private Shortcut GetMacroShortcut(int macronum) {
			switch (macronum) {
				case 10:
					return Shortcut.Alt0;
				case 1:
					return Shortcut.Alt1;
				case 2:
					return Shortcut.Alt2;
				case 3:
					return Shortcut.Alt3;
				case 4:
					return Shortcut.Alt4;
				case 5:
					return Shortcut.Alt5;
				case 6:
					return Shortcut.Alt6;
				case 7:
					return Shortcut.Alt7;
				case 8:
					return Shortcut.Alt8;
				case 9:
					return Shortcut.Alt9;
				default:
					return Shortcut.None;
			}
		}

		private string GetMacroText(int macronum) {
			if (g.Project == null)
				return "";

			foreach(CProject.Macro macro in g.Project.MacroList.Values) {
				if (macro.MacroNum == macronum)
					return macro.MacroName;
			}

			return "";
		}

		private void MacroBin() {
			frmMacros fMacros = new frmMacros();
			//fMacros.Owner = this;

			fMacros.ShowDialog();

			fMacros.Dispose();

			//InitMacros();
		}

		public void PrintPreview() {
			txtEditor.PrintPreview();
		}
		
		public void Print() {
			txtEditor.Print(true);
		}

		
		private void txtEditor_DocumentIndicatorAdded(object sender, ActiproSoftware.SyntaxEditor.IndicatorEventArgs e) {
			if (e.Indicator.Name != "__bookmark")
				return;

			if (this.g_curFile.IndicatorList.Contains(((CIndicators.BookmarkIndicator)e.Indicator).LineIndex.ToString()))
				return;
			else
				this.g_curFile.IndicatorList.Add(((CIndicators.BookmarkIndicator)e.Indicator).LineIndex.ToString());
		}

		private void txtEditor_DocumentIndicatorRemoved(object sender, ActiproSoftware.SyntaxEditor.IndicatorEventArgs e) {
			if (e.Indicator.Name != "__bookmark")
				return;

			this.g_curFile.IndicatorList.Remove(((CIndicators.BookmarkIndicator)e.Indicator).LineIndex.ToString());
		}

		private void txtEditor_TokenMouseEnter(object sender, ActiproSoftware.SyntaxEditor.TokenMouseEventArgs e) {
			if (bDisableAdvancedCompletion)
				return;

			if (e.Token.Key == "GlobalVariableToken" || e.Token.Key == "LocalVariableToken") {
				if (g.IsDebugging && g.IsBroken) {
					string finalquery = ""; //txtEditor.Document.GetTokenText(e.Token);
					TextStream ts = txtEditor.Document.GetTextStream(e.Token.EndOffset);

					while(ts.CurrentToken.Key == "IdentifierToken" || ts.CurrentToken.Key == "MemberToken" || ts.CurrentToken.Key == "GlobalVariableToken" || ts.CurrentToken.Key == "LocalVariableToken") {
						finalquery += txtEditor.Document.GetTokenText(ts.CurrentToken);
						if (!ts.GoToNextToken())
							break;
					}

					frmMain.stc_DebugQueue.Add("EVAL " + finalquery + " 0 " + finalquery + "\n");
				}
			}
		}

		private void txtEditor_TokenMouseLeave(object sender, ActiproSoftware.SyntaxEditor.TokenMouseEventArgs e) {
			frmMain.stc_DebugCurTokVal = "";
		}



		private void tmrPosNotify_Tick(object sender, System.EventArgs e) {
			if (Convert.ToInt32(this.sbpRow.Text) != (this.txtEditor.SelectedView.CurrentDocumentLine.Index + 1) || Convert.ToInt32(this.sbpCol.Text) != (this.txtEditor.SelectedView.Selection.EndPosition.Character + 1)) {
				this.sbpRow.Text = Convert.ToString(this.txtEditor.SelectedView.CurrentDocumentLine.Index + 1);
				this.sbpCol.Text = Convert.ToString(this.txtEditor.SelectedView.Selection.EndPosition.Character + 1);
				try {
					string chara = this.txtEditor.Document.GetSubstring(this.txtEditor.SelectedView.Selection.EndOffset, 1);
					this.sbpTextInfo.Text = Convert.ToString((int)chara[0]) + " (0x" + Convert.ToString((int)chara[0], 16) + ")";
					this.sbpSelection.Text = Convert.ToString(Math.Abs(this.txtEditor.SelectedView.Selection.EndOffset - this.txtEditor.SelectedView.Selection.StartOffset)) + " char(s)";
				} catch { this.sbpTextInfo.Text = "N/A"; }
			}
		}

		private CAutoComplete.ClassEntry GetObjDeclare(int offset_start, string varname) {
			// Parse all the tokens in the current function and request all the
			// variables:
			TextStream ts = txtEditor.Document.GetTextStream(offset_start);

			// Navigate to the beginning
			if (!ts.GoToPreviousToken("FunctionStartToken"))
				while(ts.GoToPreviousToken());

			ts.GoToNextToken();

			// Loop through all the tokens in the current function.
			while(ts.CurrentToken.Key != "FunctionStartToken") {
				string finalquery = "";
				
				//if (ts.CurrentToken.Key == "PreProcStartToken") {
				if (ts.CurrentToken.Key == "DeclareStartToken") {
					//while(ts.CurrentToken.Key == "PreProcStartToken") {
					while (ts.CurrentToken.Key == "DeclareStartToken") {
						finalquery += txtEditor.Document.GetTokenText(ts.CurrentToken) + " ";
						ts.GoToNextToken();
					}

					Regex rx = new Regex(@"\/\/#\s*\bdeclare\b\s*((\%|\$)\b[A-Z_][A-Z0-9_]*\b)\s*\bas\b\s*(\b[A-Z_][A-Z0-9_]*\b)(\s*)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);

					//Regex rx = new Regex(@"\/\/__decl\s*(\b[A-Z_][A-Z0-9_]*\b)\s*((\%|\$)\b[A-Z_][A-Z0-9_]*\b)(\s*)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
					Match m = rx.Match(finalquery);

					if (m.Success == false)
						continue;
					
					//if (m.Groups[2].Value.ToLower() != varname.ToLower())
					if (m.Groups[1].Value.ToLower() != varname.ToLower())
						continue;

					return (ac.ReturnClassObject(m.Groups[3].Value.ToLower())); // Groups[1]
				}

				if (!ts.GoToNextToken())
					break;
			}

			return null;
		}

		private void mnuCt_ToggleBreak_Click(object sender, System.EventArgs e) {
			this.ToggleHalt();
		}

		private void mnuCt_Reference_Click(object sender, System.EventArgs e) {
			TextStream ts = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);

			if (ts.CurrentToken.Key == "IdentifierToken") {
				g.Main.SpawnBrowser("http://www.torquedev.com/redir_ref.php?id=" + txtEditor.Document.GetTokenText(ts.CurrentToken), false);
			} else {
				MessageBox.Show(g.Main, "There is no help available for this token.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void cboCurFileFuncs_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (isDropdownSwitched) {
				isDropdownSwitched = false;
				return;
			}

			if (cboCurFileFuncs.SelectedIndex == 0) {
				// Go to the top of the file
				txtEditor.SelectedView.Selection.StartOffset = 0;

				txtEditor.Select();
				txtEditor.Focus();
				return;
			}

            string func = this.cboCurFileFuncs.Text.ToLower();

            if (func.IndexOf("::") != -1) {
                // This is a class thing
                string[] objsplit = func.Split(new string[] { "::" }, StringSplitOptions.None);

                if (!g.Project.TokenObjList.Contains(objsplit[0].ToLower()))
                    return;

                CProject.TokenObject tokobj = (CProject.TokenObject)g.Project.TokenObjList[objsplit[0].ToLower()];

                if (!tokobj.ObjectFunctions.Contains(objsplit[1].ToLower()))
                    return;

                CProject.TokenObject.ObjectDescr objdescr = (CProject.TokenObject.ObjectDescr)tokobj.ObjectFunctions[objsplit[1].ToLower()];

                try {
                    txtEditor.SelectedView.Selection.StartOffset = objdescr.FuncOffset;
                } catch { }
            } else {
                // This is a function thing
                if (!this.g_curFile.TokenList.ContainsKey(this.cboCurFileFuncs.Text.ToLower()))
                    return;

                CProject.TokenKey tok = (CProject.TokenKey)this.g_curFile.TokenList[this.cboCurFileFuncs.Text.ToLower()];

                try {
                    txtEditor.SelectedView.Selection.StartOffset = tok.LineNumber;
                } catch { }
            }

			txtEditor.Select();
		}

		private void mnuCt_BreakProps_Click(object sender, System.EventArgs e) {
			// Bring up the breakpoint properties page.  First, find the breakpoint
			// in the files list
			CProject.Breakpoint breakpt = g.Project.ProjectBreaks[this.g_curFile, txtEditor.SelectedView.CurrentDocumentLine.Index];

			if (breakpt == null)
				return;

			// Show the conditional form
			frmCondBreak fCondBreak = new frmCondBreak(breakpt);
			fCondBreak.ShowDialog();

			// Destroy the form
			fCondBreak.Dispose();
			fCondBreak = null;

			// Refresh the breakpoints
			BreakpointRender();
		}

		private void mnuCt_CreateSnippet_Click(object sender, System.EventArgs e) {
			// Create a snippet of code from the current selection
			frmSnippetNew fSnippetNew = new frmSnippetNew(this.txtEditor.SelectedView.SelectedText);
			fSnippetNew.Show();
		}

		public void ErrorRender() {
			// Clear all spans first
			this.txtEditor.Document.Indicators.Clear("__error");

			// Enumerate the errorcount for this file, if there
			// is any
			if (!g.Project.ErrorList.Contains(this.g_curFile))
				return;

			netMercs.TorqueDev.Lexer.ErrorCollection coll = (netMercs.TorqueDev.Lexer.ErrorCollection)g.Project.ErrorList[this.g_curFile];
			
			foreach(netMercs.TorqueDev.Lexer.Error err in coll) {
				try {
					int offset = this.txtEditor.Document.PositionToOffset(new Position(err.Line - 1, err.Column - 1));

					// Get the length of the word at the current offset
					int wordlen = this.txtEditor.Document.GetWordText(offset).Length;

                    if (wordlen == 0) {
                        wordlen = this.txtEditor.Document.GetWordText(offset - 1).Length;

                        if (wordlen == 0)
                            wordlen = 1;
                    }

					this.txtEditor.Document.Indicators.Add(new SquiggleLineIndicator("__error", 10, Color.Red), offset - wordlen, wordlen);
				} catch {}
			}
		}

		private void tmrErrorMonitor_Tick(object sender, System.EventArgs e) {
			// If this is a text file, don't scan for errors
			if (g_curFile.isText) {
				this.tmrErrorMonitor.Enabled = false;
				return;
			}

			// Disable the timer if we're not requiring it
			if (g.Config.b_Err_WhileTyping == false) {
				this.tmrErrorMonitor.Enabled = false;
				return;
			}

			if (this._LastMoved) {
				this._LastMoved = false;
				return;
			}

			if (this.txtEditor.Text != this._LastText) {				
				MemoryStream mem = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(txtEditor.Document.GetText(LineEndStyle.CarriageReturnNewline)));
				mem.Position = 0;
				
				netMercs.TorqueDev.Lexer.Scanner scanner = new netMercs.TorqueDev.Lexer.Scanner(mem);
				netMercs.TorqueDev.Lexer.Parser parser = new netMercs.TorqueDev.Lexer.Parser(scanner);

				parser.Parse();

				netMercs.TorqueDev.Lexer.ErrorCollection errcoll = (netMercs.TorqueDev.Lexer.ErrorCollection)parser.errors.ErrCollection.Clone();

				parser = null;
				scanner = null;

				mem.Close();

				if (g.Project.ErrorList.Contains(this.g_curFile))
					g.Project.ErrorList[this.g_curFile] = errcoll;
				else
					g.Project.ErrorList.Add(this.g_curFile, errcoll);

				
				this._LastText = this.txtEditor.Text;

				// Render the squiggles
				ErrorRender();

				// Render the treeview
				g.Main.ScanBuildTree();
			} else {
				// Just render the error if the text didn't change
				ErrorRender();
			}
		}

		private void txtEditor_KeyTyping(object sender, ActiproSoftware.SyntaxEditor.KeyTypingEventArgs e) {
			if (e.KeyData == Keys.Tab && (txtEditor.SelectedView.Selection.EndOffset > 0)) {
				if (bDisableAllCompletion)
					return;

				// Check if the previous word was a snippet, if we're
				// in the proper mode
				if (txtEditor.SelectedView.GetCurrentToken().LexicalState.Key == "DefaultState") {
					int offset = txtEditor.Document.GetWordRange(txtEditor.SelectedView.Selection.EndOffset - 1).Min;
					int length = txtEditor.SelectedView.Selection.EndOffset - offset;
					string prevword = txtEditor.Document.GetSubstring(offset, length);
					
					CodeSnippet cs = null;
					cs = new CodeSnippetFolder("Root", Application.StartupPath + "\\snippets").FindCodeSnippetWithShortcut(prevword, true);

					if (cs != null) {
						e.Cancel = true;
						txtEditor.IntelliPrompt.CodeSnippets.Activate(cs, offset, length);
					}
				}
			}

			TextStream prevstr = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);
			prevstr.GoToPreviousToken();

			// Check to see if we're completing-as-we-go:
			if (bDisableAllCompletion)
				return;

			if (g.Config.b_AC_TypeAsYouGo && (prevstr.CurrentToken.Key == "WhitespaceToken" || prevstr.CurrentToken.Key == "LineTerminatorToken"
					|| prevstr.CurrentToken.Key == "OpenParenthesisToken" || prevstr.CurrentToken.Key == "CloseParenthesisToken" ||
					prevstr.CurrentToken.Key == "OpenCurlyBraceToken" || prevstr.CurrentToken.Key == "CloseCurlyBraceToken")
					&& prevstr.CurrentToken.LexicalState.Key == "DefaultState" && (IsLetter(e.KeyChar) || IsVar(e.KeyChar, e.KeyData))) {

				if (txtEditor.IntelliPrompt.MemberList.Visible)
					return;

				if (!txtEditor.IntelliPrompt.MemberList.AllowedCharacters.Contains('%')) {
					txtEditor.IntelliPrompt.MemberList.AllowedCharacters.Add('%');
					txtEditor.IntelliPrompt.MemberList.AllowedCharacters.Add('$');
				}

				txtEditor.IntelliPrompt.MemberList.Clear();
				txtEditor.IntelliPrompt.MemberList.ImageList = SyntaxEditor.ReflectionImageList;

				PopulateMemList(txtEditor.IntelliPrompt.MemberList);

				// If we're in a specific class or datablock, show the information
				if (prevstr.CurrentToken.Key != "DatablockStartToken") {
					if (!prevstr.GoToPreviousToken("DatablockStartToken")) {
						txtEditor.IntelliPrompt.MemberList.Show();
						goto class_toks;
					}
				}

				string curdatablock = txtEditor.Document.GetTokenText(prevstr.CurrentToken).ToLower().Replace("datablock ", "");

				// Populate the memberlist with the properties of this object, if it can be found
				CAutoComplete.ClassEntry ce = ac.ReturnClassObject(curdatablock.ToLower());

				if (ce != null) {
					foreach (CAutoComplete.ClassEntry.PropEntry prop in ce.prop_list.Values) {
						txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(
							prop.prop_name,
							Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedProperty),
							"<u>" + prop.prop_type + "</u> <b>" + prop.prop_name + "</b>",
							prop.prop_name, ""));
					}
				}

			class_toks:

				// Do the same thing for object declares
				prevstr = txtEditor.Document.GetTextStream(txtEditor.SelectedView.Selection.StartOffset);
				prevstr.GoToPreviousToken();

				if (prevstr.CurrentToken.Key != "ClassStartToken") {
					if (!prevstr.GoToPreviousToken("ClassStartToken")) {
						txtEditor.IntelliPrompt.MemberList.Show();
						return;
					}
				}

				string curclassblock = txtEditor.Document.GetTokenText(prevstr.CurrentToken).ToLower().Replace("new ", "");

				// Populate the memberlist with the properties of this object, if it can be found
				ce = ac.ReturnClassObject(curclassblock.ToLower());

				if (ce != null) {
					foreach (CAutoComplete.ClassEntry.PropEntry prop in ce.prop_list.Values) {
						txtEditor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(
							prop.prop_name,
							Convert.ToInt32(ActiproSoftware.Products.SyntaxEditor.IconResource.ProtectedProperty),
							"<u>" + prop.prop_type + "</u> <b>" + prop.prop_name + "</b>",
							prop.prop_name, ""));
					}
				}

				txtEditor.IntelliPrompt.MemberList.Show();

				return;
			}
		}

		private void tmrFileWatcher_Tick(object sender, EventArgs e) {
			// Check to see if the write time of the file
			// has exceeded our expectations (haha)
			if (File.GetLastWriteTime(Path.GetFullPath(g_curFile.RelativePath)) > LastWritten) {
				// First reset the last write time to the correct one
				LastWritten = File.GetLastWriteTime(Path.GetFullPath(g_curFile.RelativePath));

				// Auto-reload?
				if (g_curFile.isForcedReload) {
					g_curFile.isForcedReload = false;
					g_curFile.isPendingReload = false;
					OpenFile();
					return;
				}

				// Now see our flags on what we can do
				if (g_curFile.isPendingReload) {
					g_curFile.isPendingReload = false;
					return;						// Ignore this
				}

				// Now we're confirming
				txtEditor.Enabled = false;
				pnlFileChanged.Visible = true;
			}
		}

		private void ctxEditor_Opening(object sender, CancelEventArgs e) {
			if (txtEditor.SelectedView.CurrentDocumentLine.Indicators.Contains("__halt"))
				this.mnuCt_BreakProps.Enabled = true;
			else
				this.mnuCt_BreakProps.Enabled = false;

			// PLUGINS
			// There are 12 menu items by default.  When showing this screen, delete any 
			// existing ones and re-add them
			if (ctxEditor.Items.Count > 12) {
				for (int i = ctxEditor.Items.Count - 1; i >= 12; i--)
					ctxEditor.Items.RemoveAt(i);

				// Re-add plugin items
				ctxEditor.Items.Add(new ToolStripSeparator());

				foreach (ToolStripMenuItem item in g.CustomEditorContextMenus)
					ctxEditor.Items.Add(item);
			}
		}

		private void lnkReloadContents_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			pnlFileChanged.Visible = false;
			OpenFile();

			txtEditor.Enabled = true;
		}

		private void lnkCancelReload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			pnlFileChanged.Visible = false;
			txtEditor.Enabled = true;
		}

	}
}
