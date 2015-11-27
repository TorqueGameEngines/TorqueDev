using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.MarkupLabel;
using ActiproSoftware.WinUICore;
using System.Threading;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Menus;
using Crownwood.DotNetMagic.Docking;
using Crownwood.DotNetMagic.Controls;
using Crownwood.DotNetMagic.Controls.Command;
using TSDev.Plugins;
using netMercs.TorqueDev.Lexer;

namespace TSDev {
	partial class frmMain {
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private System.ComponentModel.IContainer components;

		/* Begin Private Function Declares */
		private Content ContentExports;
		private Content ContentSnippetter;
		private Content ContentErrors;
		private System.Windows.Forms.Timer tmrMenuMonitor;

		public static ArrayList stc_EdTriggers = new ArrayList();

		public Hashtable _ht_customtabs = new Hashtable();

		public static ArrayList stc_Plugins = new ArrayList();
		public static IDEControl stc_PluginsCtrl = new IDEControl();

		private delegate int __del_AddNode(TreeNode node);
		private delegate void __del_ClearNodes();
		private delegate void __del_DebugProcessCmd(string indata);
		private delegate void __del_DebugWriteLog(string indata);
		private delegate void __del_ThreadedMessageBox(string text, string title, MessageBoxButtons buttons, MessageBoxIcon icon);
		private __del_ClearNodes __ClearNodes;
		private __del_AddNode __AddNode;
		private __del_DebugProcessCmd __DebugProcessCmd;
		private __del_DebugWriteLog __DebugWriteLog;
		private __del_ThreadedMessageBox __ThreadedMessageBox;

		public WindowContent ProjectWindowContent;
		public Crownwood.DotNetMagic.Controls.TabPage _PrevPage;
		

		public static TcpClient stc_tcp;
		public static System.Diagnostics.Process stc_DebugProc;
		public static Thread stc_DebugThread;
		public static volatile ArrayList stc_DebugQueue;
		public static string[] stc_Args;
		public static string stc_DebugCurTokVal = "";
		public static bool stc_bDebugMode = false;
		internal static CSnippetter stc_Snippets = new CSnippetter();

		public static bool stc_bFileAdded = false;
		public static bool stc_bEditAdded = false;
		public static bool stc_bMacrosAdded = false;
		public static bool stc_bDebugAdded = false;
		public static bool stc_bWindowAdded = false;
		public static bool stc_bHelpAdded = false;
        public static bool stc_bIsProjectDirty = false;

		private DockingManager dockDebug;
		private DockingManager dockFindResults;

		public DockingManager dockBottom;
		public WindowContent BottomWindowContent;
		
		private Content ContentFindResults;
		public UCDebug DebugUC;
		//internal UCSnippetter SnippetterUC;
		internal UCError ErrorUC;
		private volatile ArrayList DebugPrint = new ArrayList();
		/***********************************/

		internal static CAutoComplete ac = new CAutoComplete();
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.ImageList ilMainMenu;
		internal static XParser parser = new XParser();
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		public System.Windows.Forms.ImageList ilProject;
		private System.Windows.Forms.TreeView tvProject;

		public static ToolBar stc_tbFileList;
		public System.Windows.Forms.TreeView tvExplorer;
		private System.Windows.Forms.ImageList ilExplorer;
		private System.Windows.Forms.ToolTip toolTip1;

		private System.Windows.Forms.TabPage tabPage3;
		public System.Windows.Forms.TreeView tvExports;
		
		public DockingManager dockMgr;

		private Content ContentProject;
		private Content ContentExplorer;
		public Crownwood.DotNetMagic.Controls.TabbedGroups tabMain;
		private System.Windows.Forms.Panel pnlExplorer;
		private ActiproSoftware.MarkupLabel.MarkupLabel lblExpDescr;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel pnlExports;
		private System.Windows.Forms.Splitter splitter3;
		private ActiproSoftware.MarkupLabel.MarkupLabel lblExportDescr;
		private System.Windows.Forms.Timer tmrTitleMonitor;
		public MenuStrip mnuMain;
		public ToolStripMenuItem mnuFile;
		private ToolStripMenuItem mnuFile_NewProject;
		private ToolStripMenuItem mnuFile_Open;
		private ToolStripMenuItem mnuFile_SaveProject;
		private ToolStripMenuItem mnuFile_SaveProjectAs;
		private ToolStripMenuItem mnuFile_CloseProject;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem mnuFile_AddFile;
		private ToolStripMenuItem mnuFile_AddExisting;
		private ToolStripMenuItem mnuFile_Import;
		private ToolStripMenuItem mnuFile_RescanProject;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem mnuFile_SaveFile;
		private ToolStripMenuItem mnuFile_SaveAs;
		private ToolStripMenuItem mnuFile_SaveAll;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem mnuFile_CloseFile;
		private ToolStripMenuItem mnuFile_CloseAll;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripMenuItem mnuFile_ExportRTF;
		private ToolStripMenuItem mnuFile_ExportHTML;
		private ToolStripSeparator toolStripMenuItem5;
		private ToolStripMenuItem mnuFile_ShowWelcome;
		private ToolStripMenuItem mnuFile_Exit;
		public ToolStripMenuItem mnuEdit;
		private ToolStripMenuItem mnuEdit_Undo;
		private ToolStripMenuItem mnuEdit_Redo;
		private ToolStripSeparator toolStripMenuItem6;
		private ToolStripMenuItem mnuEdit_Cut;
		private ToolStripMenuItem mnuEdit_Copy;
		private ToolStripMenuItem mnuEdit_Paste;
		private ToolStripSeparator toolStripMenuItem7;
		private ToolStripMenuItem mnuEdit_Search;
		private ToolStripMenuItem mnuEdit_SearchReplace;
		private ToolStripMenuItem mnuEdit_GoTo;
		private ToolStripSeparator toolStripMenuItem8;
		private ToolStripMenuItem mnuEdit_Bookmarks;
		private ToolStripMenuItem mnuEdit_BKAdd;
		private ToolStripMenuItem mnuEdit_BKPrev;
		private ToolStripMenuItem mnuEdit_BKNext;
		private ToolStripMenuItem mnuEdit_BKDel;
		private ToolStripMenuItem mnuEdit_BreakPoint;
		private ToolStripSeparator toolStripMenuItem9;
		private ToolStripMenuItem mnuEdit_CommentSel;
		private ToolStripMenuItem mnuEdit_UncommentSel;
		private ToolStripMenuItem mnuProject;
		private ToolStripMenuItem mnuEdit_SearchFiles;
		private ToolStripMenuItem mnuEdit_Intellicode;
		private ToolStripMenuItem mnuEdit_IC_ScanFile;
		private ToolStripMenuItem mnuEdit_IC_ScanProj;
		private ToolStripSeparator toolStripMenuItem10;
		private ToolStripMenuItem mnuEdit_Prefs;
		public ToolStripMenuItem mnuMacros;
		public ToolStripMenuItem mnuMacro_Dir;
		public ToolStripMenuItem mnuMacro_BeginRec;
		public ToolStripMenuItem mnuMacro_EndRec;
		private ToolStripSeparator toolStripMenuItem11;
		public ToolStripMenuItem mnuDebug;
		private ToolStripMenuItem mnuDebug_Start;
		private ToolStripMenuItem mnuDebug_StartNoDebug;
		private ToolStripMenuItem mnuDebug_Break;
		private ToolStripMenuItem mnuDebug_Stop;
		private ToolStripSeparator toolStripMenuItem12;
		private ToolStripMenuItem mnuDebug_StepInto;
		private ToolStripMenuItem mnuDebug_StepOver;
		private ToolStripMenuItem mnuDebug_StepOut;
		private ToolStripMenuItem mnuWindow;
		private ToolStripMenuItem mnuWindow_NewBrowser;
		private ToolStripMenuItem mnuWindow_ShowBar;
		private ToolStripMenuItem pluginSpecificItemsToolStripMenuItem;
		public ToolStripMenuItem mnuHelp;
		private ToolStripMenuItem mnuHelp_Website;
		private ToolStripMenuItem mnuHelp_Forums;
		private ToolStripMenuItem mnuHelp_ReportBugs;
		private ToolStripMenuItem mnuHelp_Learn;
		private ToolStripSeparator toolStripMenuItem13;
		private ToolStripMenuItem mnuHelp_Donate;
		private ToolStripSeparator toolStripMenuItem14;
		private ToolStripMenuItem mnuAbout_Update;
		private ToolStripMenuItem mnuHelp_About;
		private ToolStrip tbEditBar;
		private ToolStripButton tbbNewProject;
		private ToolStripButton tbbOpenProject;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton tbbNewItem;
		private ToolStripButton tbbExistingItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton tbbSave;
		private ToolStripButton tbbSaveAs;
		private ToolStripButton tbbSaveAll;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripButton tbbPrint;
		private ToolStripButton tbbPrintPreview;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton tbbCut;
		private ToolStripButton tbbCopy;
		private ToolStripButton tbbPaste;
		private ToolStripButton tbbDelete;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripButton tbbUndo;
		private ToolStripButton tbbRedo;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripButton tbbSearch;
		private ToolStripButton tbbFind;
		private ToolStripButton tbbFindFiles;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripButton tbbMarkAdd;
		private ToolStripButton tbbMarkUp;
		private ToolStripButton tbbMarkDown;
		private ToolStripButton tbbMarkDel;
		private ToolStripSeparator toolStripSeparator8;
		private ToolStripButton tbbDebugStart;
		private ToolStripButton tbbDebugBreak;
		private ToolStripButton tbbDebugStop;
		private ToolStripSeparator toolStripSeparator9;
		private ToolStripButton tbbDebugStepInto;
		private ToolStripButton tbbDebugStepOver;
		private ToolStripButton tbbDebugStepOut;
		private ToolStripSeparator toolStripSeparator10;
		private ToolStripLabel toolStripLabel1;
		private StatusStrip sbMain;
		private ToolStripStatusLabel sbpFileName;
		private ToolStripProgressBar toolStripProgressBar1;
		private ToolStripStatusLabel sbpRow;
		private ToolStripStatusLabel sbpCol;
		private ToolStripStatusLabel sbpTextInfo;
		private ToolStripStatusLabel sbpSelection;
		private ToolStripStatusLabel toolStripStatusLabel6;
        private ToolStripButton tbbDebugRun;
		private ToolStripSeparator toolStripSeparator11;
		private ToolStripMenuItem mnuFile_Print;
		private ToolStripMenuItem mnuFile_PrintPreview;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.ilMainMenu = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tvProject = new System.Windows.Forms.TreeView();
			this.ctmProject = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject_AddNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject_AddExist = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuProject_Rename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject_Del = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject_Properties = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSCC = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSCC_CheckIn = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSCC_CheckOut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSCC_UndoCheckOut = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSCC_GetLatest = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSCC_AddToScc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSCC_DelFromScc = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSCC_Diff = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSCC_History = new System.Windows.Forms.ToolStripMenuItem();
			this.ilProject = new System.Windows.Forms.ImageList(this.components);
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.pnlExplorer = new System.Windows.Forms.Panel();
			this.tvExplorer = new System.Windows.Forms.TreeView();
			this.ilExplorer = new System.Windows.Forms.ImageList(this.components);
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.lblExpDescr = new ActiproSoftware.MarkupLabel.MarkupLabel();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.pnlExports = new System.Windows.Forms.Panel();
			this.tvExports = new System.Windows.Forms.TreeView();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.lblExportDescr = new ActiproSoftware.MarkupLabel.MarkupLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabMain = new Crownwood.DotNetMagic.Controls.TabbedGroups();
			this.ilDocBar = new System.Windows.Forms.ImageList(this.components);
			this.tmrMenuMonitor = new System.Windows.Forms.Timer(this.components);
			this.tmrTitleMonitor = new System.Windows.Forms.Timer(this.components);
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_NewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_Open_ProjectFromFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_Open_ProjectFromScc = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_Open_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_SaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_SaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_CloseProject = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_AddFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_AddExisting = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_RescanProject = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_SaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_SaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_CloseFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_CloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_ExportHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_ExportRTF = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_Print = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_PrintPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFile_ShowWelcome = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Undo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Redo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEdit_Cut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Paste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEdit_Search = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_SearchReplace = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_GoTo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEdit_Bookmarks = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BKAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BKPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BKNext = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BKDel = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BKClearAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_BreakPoint = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEdit_CommentSel = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_UncommentSel = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_SearchFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Intellicode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_IC_ScanFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_IC_ScanProj = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
			this.addProjectToSourceControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeProjectFromSourceControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
			this.sourceControlPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit_Prefs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMacros = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMacro_Dir = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMacro_BeginRec = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMacro_EndRec = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_Start = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_StartNoDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_Break = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_Stop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDebug_StepInto = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_StepOver = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDebug_StepOut = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDebug_Wizard = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow_NewBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow_WorkspaceWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginSpecificItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow_ShowBar = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp_Website = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp_Forums = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp_ReportBugs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp_Learn = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuHelp_Donate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuAbout_Update = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAbout_License = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
			this.tbEditBar = new System.Windows.Forms.ToolStrip();
			this.tbbNewProject = new System.Windows.Forms.ToolStripButton();
			this.tbbOpenProject = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbNewItem = new System.Windows.Forms.ToolStripButton();
			this.tbbExistingItem = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbSave = new System.Windows.Forms.ToolStripButton();
			this.tbbSaveAs = new System.Windows.Forms.ToolStripButton();
			this.tbbSaveAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbPrint = new System.Windows.Forms.ToolStripButton();
			this.tbbPrintPreview = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbCut = new System.Windows.Forms.ToolStripButton();
			this.tbbCopy = new System.Windows.Forms.ToolStripButton();
			this.tbbPaste = new System.Windows.Forms.ToolStripButton();
			this.tbbDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbUndo = new System.Windows.Forms.ToolStripButton();
			this.tbbRedo = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.cboRecentSearches = new System.Windows.Forms.ToolStripComboBox();
			this.tbbSearch = new System.Windows.Forms.ToolStripButton();
			this.tbbFind = new System.Windows.Forms.ToolStripButton();
			this.tbbFindFiles = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbMarkAdd = new System.Windows.Forms.ToolStripButton();
			this.tbbMarkUp = new System.Windows.Forms.ToolStripButton();
			this.tbbMarkDown = new System.Windows.Forms.ToolStripButton();
			this.tbbMarkDel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbDebugStart = new System.Windows.Forms.ToolStripButton();
			this.tbbDebugRun = new System.Windows.Forms.ToolStripButton();
			this.tbbDebugBreak = new System.Windows.Forms.ToolStripButton();
			this.tbbDebugStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tbbDebugStepInto = new System.Windows.Forms.ToolStripButton();
			this.tbbDebugStepOver = new System.Windows.Forms.ToolStripButton();
			this.tbbDebugStepOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.sbMain = new System.Windows.Forms.StatusStrip();
			this.sbpFileName = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.sbpRow = new System.Windows.Forms.ToolStripStatusLabel();
			this.sbpCol = new System.Windows.Forms.ToolStripStatusLabel();
			this.sbpTextInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.sbpSelection = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlToolbars = new System.Windows.Forms.Panel();
			this.tscToolbars = new System.Windows.Forms.ToolStripContainer();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.ctmProject.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.pnlExplorer.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.pnlExports.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
			this.mnuMain.SuspendLayout();
			this.tbEditBar.SuspendLayout();
			this.sbMain.SuspendLayout();
			this.tscToolbars.TopToolStripPanel.SuspendLayout();
			this.tscToolbars.SuspendLayout();
			this.SuspendLayout();
			// 
			// ilMainMenu
			// 
			this.ilMainMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMainMenu.ImageStream")));
			this.ilMainMenu.TransparentColor = System.Drawing.Color.Transparent;
			this.ilMainMenu.Images.SetKeyName(0, "");
			this.ilMainMenu.Images.SetKeyName(1, "");
			this.ilMainMenu.Images.SetKeyName(2, "");
			this.ilMainMenu.Images.SetKeyName(3, "");
			this.ilMainMenu.Images.SetKeyName(4, "");
			this.ilMainMenu.Images.SetKeyName(5, "");
			this.ilMainMenu.Images.SetKeyName(6, "");
			this.ilMainMenu.Images.SetKeyName(7, "");
			this.ilMainMenu.Images.SetKeyName(8, "");
			this.ilMainMenu.Images.SetKeyName(9, "");
			this.ilMainMenu.Images.SetKeyName(10, "");
			this.ilMainMenu.Images.SetKeyName(11, "");
			this.ilMainMenu.Images.SetKeyName(12, "");
			this.ilMainMenu.Images.SetKeyName(13, "");
			this.ilMainMenu.Images.SetKeyName(14, "");
			this.ilMainMenu.Images.SetKeyName(15, "");
			this.ilMainMenu.Images.SetKeyName(16, "");
			this.ilMainMenu.Images.SetKeyName(17, "");
			this.ilMainMenu.Images.SetKeyName(18, "");
			this.ilMainMenu.Images.SetKeyName(19, "");
			this.ilMainMenu.Images.SetKeyName(20, "");
			this.ilMainMenu.Images.SetKeyName(21, "");
			this.ilMainMenu.Images.SetKeyName(22, "");
			this.ilMainMenu.Images.SetKeyName(23, "");
			this.ilMainMenu.Images.SetKeyName(24, "");
			this.ilMainMenu.Images.SetKeyName(25, "");
			this.ilMainMenu.Images.SetKeyName(26, "");
			this.ilMainMenu.Images.SetKeyName(27, "");
			this.ilMainMenu.Images.SetKeyName(28, "");
			this.ilMainMenu.Images.SetKeyName(29, "");
			this.ilMainMenu.Images.SetKeyName(30, "");
			this.ilMainMenu.Images.SetKeyName(31, "");
			this.ilMainMenu.Images.SetKeyName(32, "");
			this.ilMainMenu.Images.SetKeyName(33, "");
			this.ilMainMenu.Images.SetKeyName(34, "");
			this.ilMainMenu.Images.SetKeyName(35, "");
			this.ilMainMenu.Images.SetKeyName(36, "");
			this.ilMainMenu.Images.SetKeyName(37, "");
			this.ilMainMenu.Images.SetKeyName(38, "");
			this.ilMainMenu.Images.SetKeyName(39, "");
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(232, 50);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 445);
			this.splitter1.TabIndex = 14;
			this.splitter1.TabStop = false;
			this.splitter1.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 50);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(232, 445);
			this.panel1.TabIndex = 13;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(232, 445);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage1.Controls.Add(this.tvProject);
			this.tabPage1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(224, 418);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Project";
			// 
			// tvProject
			// 
			this.tvProject.AllowDrop = true;
			this.tvProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvProject.ContextMenuStrip = this.ctmProject;
			this.tvProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvProject.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvProject.FullRowSelect = true;
			this.tvProject.HideSelection = false;
			this.tvProject.ImageIndex = 2;
			this.tvProject.ImageList = this.ilProject;
			this.tvProject.LabelEdit = true;
			this.tvProject.Location = new System.Drawing.Point(0, 0);
			this.tvProject.Name = "tvProject";
			this.tvProject.SelectedImageIndex = 1;
			this.tvProject.ShowRootLines = false;
			this.tvProject.Size = new System.Drawing.Size(224, 418);
			this.tvProject.TabIndex = 0;
			this.tvProject.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvProject_AfterLabelEdit);
			this.tvProject.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterCollapse);
			this.tvProject.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterExpand);
			this.tvProject.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvProject_ItemDrag);
			this.tvProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterSelect);
			this.tvProject.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProject_NodeMouseDoubleClick);
			this.tvProject.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvProject_DragDrop);
			this.tvProject.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvProject_DragEnter);
			this.tvProject.DragOver += new System.Windows.Forms.DragEventHandler(this.tvProject_DragOver);
			this.tvProject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvProject_KeyDown);
			this.tvProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProject_MouseDown);
			this.tvProject.Validating += new System.ComponentModel.CancelEventHandler(this.tvProject_Validating);
			// 
			// ctmProject
			// 
			this.ctmProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateFolder,
            this.mnuProject_AddNew,
            this.mnuProject_AddExist,
            this.mnuProject_Import,
            this.toolStripSeparator13,
            this.mnuProject_Rename,
            this.mnuProject_Del,
            this.mnuProject_Properties,
            this.toolStripMenuItem15,
            this.mnuSCC});
			this.ctmProject.Name = "ctmProject";
			this.ctmProject.Size = new System.Drawing.Size(214, 192);
			this.ctmProject.Opening += new System.ComponentModel.CancelEventHandler(this.ctmProject_Opening);
			// 
			// mnuCreateFolder
			// 
			this.mnuCreateFolder.Image = global::TSDev.Properties.Resources.folder_new;
			this.mnuCreateFolder.Name = "mnuCreateFolder";
			this.mnuCreateFolder.Size = new System.Drawing.Size(213, 22);
			this.mnuCreateFolder.Text = "&Create Virtual Folder...";
			this.mnuCreateFolder.Click += new System.EventHandler(this.mnuCreateFolder_Click);
			// 
			// mnuProject_AddNew
			// 
			this.mnuProject_AddNew.Image = global::TSDev.Properties.Resources.document_new;
			this.mnuProject_AddNew.Name = "mnuProject_AddNew";
			this.mnuProject_AddNew.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_AddNew.Text = "&Add New File...";
			this.mnuProject_AddNew.Click += new System.EventHandler(this.mnuProject_AddNew_Click);
			// 
			// mnuProject_AddExist
			// 
			this.mnuProject_AddExist.Image = global::TSDev.Properties.Resources.document_add;
			this.mnuProject_AddExist.Name = "mnuProject_AddExist";
			this.mnuProject_AddExist.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_AddExist.Text = "Add E&xisting File...";
			this.mnuProject_AddExist.Click += new System.EventHandler(this.mnuProject_AddExist_Click);
			// 
			// mnuProject_Import
			// 
			this.mnuProject_Import.Image = global::TSDev.Properties.Resources.import2;
			this.mnuProject_Import.Name = "mnuProject_Import";
			this.mnuProject_Import.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_Import.Text = "&Import Existing Directory...";
			this.mnuProject_Import.Click += new System.EventHandler(this.mnuProject_Import_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(210, 6);
			// 
			// mnuProject_Rename
			// 
			this.mnuProject_Rename.Image = global::TSDev.Properties.Resources.document_edit;
			this.mnuProject_Rename.Name = "mnuProject_Rename";
			this.mnuProject_Rename.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_Rename.Text = "Rena&me";
			this.mnuProject_Rename.Click += new System.EventHandler(this.mnuProject_Rename_Click);
			// 
			// mnuProject_Del
			// 
			this.mnuProject_Del.Image = global::TSDev.Properties.Resources.delete2;
			this.mnuProject_Del.Name = "mnuProject_Del";
			this.mnuProject_Del.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_Del.Text = "&Delete";
			this.mnuProject_Del.Click += new System.EventHandler(this.mnuProject_Del_Click);
			// 
			// mnuProject_Properties
			// 
			this.mnuProject_Properties.Image = global::TSDev.Properties.Resources.scroll_view;
			this.mnuProject_Properties.Name = "mnuProject_Properties";
			this.mnuProject_Properties.Size = new System.Drawing.Size(213, 22);
			this.mnuProject_Properties.Text = "Pr&operties...";
			this.mnuProject_Properties.Click += new System.EventHandler(this.mnuProperties_Click);
			// 
			// toolStripMenuItem15
			// 
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			this.toolStripMenuItem15.Size = new System.Drawing.Size(210, 6);
			// 
			// mnuSCC
			// 
			this.mnuSCC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSCC_CheckIn,
            this.mnuSCC_CheckOut,
            this.mnuSCC_UndoCheckOut,
            this.toolStripSeparator14,
            this.mnuSCC_GetLatest,
            this.toolStripSeparator15,
            this.mnuSCC_AddToScc,
            this.mnuSCC_DelFromScc,
            this.toolStripSeparator16,
            this.mnuSCC_Diff,
            this.mnuSCC_History});
			this.mnuSCC.Image = global::TSDev.Properties.Resources._16_torque_scc;
			this.mnuSCC.Name = "mnuSCC";
			this.mnuSCC.Size = new System.Drawing.Size(213, 22);
			this.mnuSCC.Text = "Source Code Control";
			// 
			// mnuSCC_CheckIn
			// 
			this.mnuSCC_CheckIn.Image = global::TSDev.Properties.Resources._16_torque_scc_checkin;
			this.mnuSCC_CheckIn.Name = "mnuSCC_CheckIn";
			this.mnuSCC_CheckIn.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_CheckIn.Text = "Check &In";
			// 
			// mnuSCC_CheckOut
			// 
			this.mnuSCC_CheckOut.Image = global::TSDev.Properties.Resources._16_torque_scc_checkout;
			this.mnuSCC_CheckOut.Name = "mnuSCC_CheckOut";
			this.mnuSCC_CheckOut.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_CheckOut.Text = "Check &Out";
			// 
			// mnuSCC_UndoCheckOut
			// 
			this.mnuSCC_UndoCheckOut.Image = global::TSDev.Properties.Resources._16_torque_scc_undo_checkout;
			this.mnuSCC_UndoCheckOut.Name = "mnuSCC_UndoCheckOut";
			this.mnuSCC_UndoCheckOut.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_UndoCheckOut.Text = "&Undo Check Out";
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(225, 6);
			// 
			// mnuSCC_GetLatest
			// 
			this.mnuSCC_GetLatest.Image = global::TSDev.Properties.Resources._16_torque_scc_get;
			this.mnuSCC_GetLatest.Name = "mnuSCC_GetLatest";
			this.mnuSCC_GetLatest.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_GetLatest.Text = "&Get Latest";
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(225, 6);
			// 
			// mnuSCC_AddToScc
			// 
			this.mnuSCC_AddToScc.Image = global::TSDev.Properties.Resources._16_torque_scc_add;
			this.mnuSCC_AddToScc.Name = "mnuSCC_AddToScc";
			this.mnuSCC_AddToScc.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_AddToScc.Text = "&Add to Source Control";
			// 
			// mnuSCC_DelFromScc
			// 
			this.mnuSCC_DelFromScc.Image = global::TSDev.Properties.Resources._16_torque_scc_delete;
			this.mnuSCC_DelFromScc.Name = "mnuSCC_DelFromScc";
			this.mnuSCC_DelFromScc.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_DelFromScc.Text = "&Remove from Source Control";
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(225, 6);
			// 
			// mnuSCC_Diff
			// 
			this.mnuSCC_Diff.Image = global::TSDev.Properties.Resources._16_torque_scc_diff;
			this.mnuSCC_Diff.Name = "mnuSCC_Diff";
			this.mnuSCC_Diff.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_Diff.Text = "Dif&ference...";
			// 
			// mnuSCC_History
			// 
			this.mnuSCC_History.Image = global::TSDev.Properties.Resources._16_torque_scc_history;
			this.mnuSCC_History.Name = "mnuSCC_History";
			this.mnuSCC_History.Size = new System.Drawing.Size(228, 22);
			this.mnuSCC_History.Text = "&History...";
			// 
			// ilProject
			// 
			this.ilProject.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilProject.ImageStream")));
			this.ilProject.TransparentColor = System.Drawing.Color.Transparent;
			this.ilProject.Images.SetKeyName(0, "");
			this.ilProject.Images.SetKeyName(1, "");
			this.ilProject.Images.SetKeyName(2, "16_code_torque.png");
			this.ilProject.Images.SetKeyName(3, "");
			this.ilProject.Images.SetKeyName(4, "");
			this.ilProject.Images.SetKeyName(5, "");
			this.ilProject.Images.SetKeyName(6, "");
			this.ilProject.Images.SetKeyName(7, "");
			this.ilProject.Images.SetKeyName(8, "");
			this.ilProject.Images.SetKeyName(9, "");
			this.ilProject.Images.SetKeyName(10, "");
			this.ilProject.Images.SetKeyName(11, "");
			this.ilProject.Images.SetKeyName(12, "");
			this.ilProject.Images.SetKeyName(13, "");
			this.ilProject.Images.SetKeyName(14, "");
			this.ilProject.Images.SetKeyName(15, "");
			this.ilProject.Images.SetKeyName(16, "");
			this.ilProject.Images.SetKeyName(17, "");
			this.ilProject.Images.SetKeyName(18, "");
			this.ilProject.Images.SetKeyName(19, "");
			this.ilProject.Images.SetKeyName(20, "");
			this.ilProject.Images.SetKeyName(21, "");
			this.ilProject.Images.SetKeyName(22, "");
			this.ilProject.Images.SetKeyName(23, "");
			this.ilProject.Images.SetKeyName(24, "");
			this.ilProject.Images.SetKeyName(25, "");
			this.ilProject.Images.SetKeyName(26, "");
			this.ilProject.Images.SetKeyName(27, "");
			this.ilProject.Images.SetKeyName(28, "");
			this.ilProject.Images.SetKeyName(29, "16_code_torque_checkout.png");
			this.ilProject.Images.SetKeyName(30, "16_code_torque_add.png");
			this.ilProject.Images.SetKeyName(31, "16_code_torque_checkin.png");
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage2.Controls.Add(this.pnlExplorer);
			this.tabPage2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(224, 419);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Explorer";
			// 
			// pnlExplorer
			// 
			this.pnlExplorer.Controls.Add(this.tvExplorer);
			this.pnlExplorer.Controls.Add(this.splitter2);
			this.pnlExplorer.Controls.Add(this.lblExpDescr);
			this.pnlExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExplorer.Location = new System.Drawing.Point(0, 0);
			this.pnlExplorer.Name = "pnlExplorer";
			this.pnlExplorer.Size = new System.Drawing.Size(224, 419);
			this.pnlExplorer.TabIndex = 4;
			// 
			// tvExplorer
			// 
			this.tvExplorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExplorer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvExplorer.ImageIndex = 0;
			this.tvExplorer.ImageList = this.ilExplorer;
			this.tvExplorer.Location = new System.Drawing.Point(0, 0);
			this.tvExplorer.Name = "tvExplorer";
			this.tvExplorer.SelectedImageIndex = 0;
			this.tvExplorer.ShowRootLines = false;
			this.tvExplorer.Size = new System.Drawing.Size(224, 313);
			this.tvExplorer.Sorted = true;
			this.tvExplorer.TabIndex = 0;
			this.tvExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExplorer_AfterSelect);
			this.tvExplorer.DoubleClick += new System.EventHandler(this.tvExplorer_DoubleClick);
			this.tvExplorer.MouseEnter += new System.EventHandler(this.tvExplorer_MouseEnter);
			this.tvExplorer.MouseLeave += new System.EventHandler(this.tvExplorer_MouseLeave);
			this.tvExplorer.MouseHover += new System.EventHandler(this.tvExplorer_MouseHover);
			this.tvExplorer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvExplorer_MouseMove);
			this.tvExplorer.Validating += new System.ComponentModel.CancelEventHandler(this.tvExplorer_Validating);
			// 
			// ilExplorer
			// 
			this.ilExplorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilExplorer.ImageStream")));
			this.ilExplorer.TransparentColor = System.Drawing.Color.Transparent;
			this.ilExplorer.Images.SetKeyName(0, "");
			this.ilExplorer.Images.SetKeyName(1, "");
			this.ilExplorer.Images.SetKeyName(2, "");
			this.ilExplorer.Images.SetKeyName(3, "");
			this.ilExplorer.Images.SetKeyName(4, "");
			this.ilExplorer.Images.SetKeyName(5, "");
			// 
			// splitter2
			// 
			this.splitter2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 313);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(224, 2);
			this.splitter2.TabIndex = 5;
			this.splitter2.TabStop = false;
			// 
			// lblExpDescr
			// 
			this.lblExpDescr.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblExpDescr.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblExpDescr.Location = new System.Drawing.Point(0, 315);
			this.lblExpDescr.MaxWidth = 2147483647;
			this.lblExpDescr.Name = "lblExpDescr";
			this.lblExpDescr.Size = new System.Drawing.Size(224, 104);
			this.lblExpDescr.TabIndex = 4;
			this.lblExpDescr.Text = "<b>Description</b><br/>";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tabPage3.Controls.Add(this.pnlExports);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(224, 419);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Exports";
			// 
			// pnlExports
			// 
			this.pnlExports.Controls.Add(this.tvExports);
			this.pnlExports.Controls.Add(this.splitter3);
			this.pnlExports.Controls.Add(this.lblExportDescr);
			this.pnlExports.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExports.Location = new System.Drawing.Point(0, 0);
			this.pnlExports.Name = "pnlExports";
			this.pnlExports.Size = new System.Drawing.Size(224, 419);
			this.pnlExports.TabIndex = 7;
			// 
			// tvExports
			// 
			this.tvExports.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvExports.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExports.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvExports.ImageIndex = 0;
			this.tvExports.ImageList = this.ilExplorer;
			this.tvExports.Location = new System.Drawing.Point(0, 0);
			this.tvExports.Name = "tvExports";
			this.tvExports.SelectedImageIndex = 0;
			this.tvExports.ShowRootLines = false;
			this.tvExports.Size = new System.Drawing.Size(224, 305);
			this.tvExports.Sorted = true;
			this.tvExports.TabIndex = 4;
			this.tvExports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExports_AfterSelect);
			// 
			// splitter3
			// 
			this.splitter3.BackColor = System.Drawing.SystemColors.Control;
			this.splitter3.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter3.Location = new System.Drawing.Point(0, 305);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(224, 2);
			this.splitter3.TabIndex = 7;
			this.splitter3.TabStop = false;
			// 
			// lblExportDescr
			// 
			this.lblExportDescr.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblExportDescr.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblExportDescr.Location = new System.Drawing.Point(0, 307);
			this.lblExportDescr.MaxWidth = 2147483647;
			this.lblExportDescr.Name = "lblExportDescr";
			this.lblExportDescr.Size = new System.Drawing.Size(224, 112);
			this.lblExportDescr.TabIndex = 8;
			this.lblExportDescr.Text = "<b>Description</b><br/>";
			// 
			// tabMain
			// 
			this.tabMain.AllowDrop = true;
			this.tabMain.AtLeastOneLeaf = true;
			this.tabMain.DisplayTabMode = Crownwood.DotNetMagic.Controls.DisplayTabModes.ShowActiveAndMouseOver;
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(235, 50);
			this.tabMain.Name = "tabMain";
			this.tabMain.OfficeStyleSelected = Crownwood.DotNetMagic.Controls.OfficeStyle.LightEnhanced;
			this.tabMain.ProminentLeaf = null;
			this.tabMain.ResizeBarColor = System.Drawing.SystemColors.Control;
			this.tabMain.Size = new System.Drawing.Size(565, 445);
			this.tabMain.TabIndex = 17;
			this.tabMain.PageCloseRequest += new Crownwood.DotNetMagic.Controls.TabbedGroups.PageCloseRequestHandler(this.tabMain_PageCloseRequest);
			this.tabMain.PageChanged += new Crownwood.DotNetMagic.Controls.TabbedGroups.PageChangeHandler(this.tabMain_PageChanged);
			this.tabMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tabMain_PreviewKeyDown);
			// 
			// ilDocBar
			// 
			this.ilDocBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilDocBar.ImageStream")));
			this.ilDocBar.TransparentColor = System.Drawing.Color.Transparent;
			this.ilDocBar.Images.SetKeyName(0, "text.png");
			this.ilDocBar.Images.SetKeyName(1, "text_marked.png");
			this.ilDocBar.Images.SetKeyName(2, "window_earth.png");
			// 
			// tmrMenuMonitor
			// 
			this.tmrMenuMonitor.Enabled = true;
			this.tmrMenuMonitor.Tick += new System.EventHandler(this.tmrMenuMonitor_Tick);
			// 
			// tmrTitleMonitor
			// 
			this.tmrTitleMonitor.Enabled = true;
			this.tmrTitleMonitor.Tick += new System.EventHandler(this.tmrTitleMonitor_Tick);
			// 
			// mnuMain
			// 
			this.mnuMain.Dock = System.Windows.Forms.DockStyle.None;
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuProject,
            this.mnuMacros,
            this.mnuDebug,
            this.mnuWindow,
            this.mnuHelp});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(800, 24);
			this.mnuMain.TabIndex = 20;
			this.mnuMain.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_NewProject,
            this.mnuFile_Open,
            this.mnuFile_SaveProject,
            this.mnuFile_SaveProjectAs,
            this.mnuFile_CloseProject,
            this.toolStripMenuItem1,
            this.mnuFile_AddFile,
            this.mnuFile_AddExisting,
            this.mnuFile_Import,
            this.mnuFile_RescanProject,
            this.toolStripMenuItem2,
            this.mnuFile_SaveFile,
            this.mnuFile_SaveAs,
            this.mnuFile_SaveAll,
            this.toolStripMenuItem3,
            this.mnuFile_CloseFile,
            this.mnuFile_CloseAll,
            this.toolStripMenuItem4,
            this.mnuFile_ExportHTML,
            this.mnuFile_ExportRTF,
            this.toolStripSeparator11,
            this.mnuFile_Print,
            this.mnuFile_PrintPreview,
            this.toolStripMenuItem5,
            this.mnuFile_ShowWelcome,
            this.mnuFile_Exit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuFile_NewProject
			// 
			this.mnuFile_NewProject.Image = global::TSDev.Properties.Resources.folder_new;
			this.mnuFile_NewProject.Name = "mnuFile_NewProject";
			this.mnuFile_NewProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.mnuFile_NewProject.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_NewProject.Text = "New &Project";
			this.mnuFile_NewProject.Click += new System.EventHandler(this.mnuFile_NewProject_Click);
			// 
			// mnuFile_Open
			// 
			this.mnuFile_Open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_Open_ProjectFromFile,
            this.mnuFile_Open_ProjectFromScc,
            this.toolStripMenuItem18,
            this.mnuFile_Open_File});
			this.mnuFile_Open.Image = global::TSDev.Properties.Resources.folder;
			this.mnuFile_Open.Name = "mnuFile_Open";
			this.mnuFile_Open.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_Open.Text = "&Open";
			// 
			// mnuFile_Open_ProjectFromFile
			// 
			this.mnuFile_Open_ProjectFromFile.Image = global::TSDev.Properties.Resources.folder_cubes;
			this.mnuFile_Open_ProjectFromFile.Name = "mnuFile_Open_ProjectFromFile";
			this.mnuFile_Open_ProjectFromFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuFile_Open_ProjectFromFile.Size = new System.Drawing.Size(297, 22);
			this.mnuFile_Open_ProjectFromFile.Text = "Project from File...";
			this.mnuFile_Open_ProjectFromFile.Click += new System.EventHandler(this.mnuFile_OpenProj_Click);
			// 
			// mnuFile_Open_ProjectFromScc
			// 
			this.mnuFile_Open_ProjectFromScc.Image = global::TSDev.Properties.Resources.server_client_exchange;
			this.mnuFile_Open_ProjectFromScc.Name = "mnuFile_Open_ProjectFromScc";
			this.mnuFile_Open_ProjectFromScc.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
			this.mnuFile_Open_ProjectFromScc.Size = new System.Drawing.Size(297, 22);
			this.mnuFile_Open_ProjectFromScc.Tag = "scc";
			this.mnuFile_Open_ProjectFromScc.Text = "Project from Source Control...";
			this.mnuFile_Open_ProjectFromScc.Click += new System.EventHandler(this.mnuFile_Open_ProjectFromScc_Click);
			// 
			// toolStripMenuItem18
			// 
			this.toolStripMenuItem18.Name = "toolStripMenuItem18";
			this.toolStripMenuItem18.Size = new System.Drawing.Size(294, 6);
			// 
			// mnuFile_Open_File
			// 
			this.mnuFile_Open_File.Image = global::TSDev.Properties.Resources.document;
			this.mnuFile_Open_File.Name = "mnuFile_Open_File";
			this.mnuFile_Open_File.Size = new System.Drawing.Size(297, 22);
			this.mnuFile_Open_File.Tag = "p";
			this.mnuFile_Open_File.Text = "Code or Text File...";
			this.mnuFile_Open_File.Click += new System.EventHandler(this.mnuFile_Open_File_Click);
			// 
			// mnuFile_SaveProject
			// 
			this.mnuFile_SaveProject.Image = global::TSDev.Properties.Resources.import1;
			this.mnuFile_SaveProject.Name = "mnuFile_SaveProject";
			this.mnuFile_SaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.mnuFile_SaveProject.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_SaveProject.Tag = "p";
			this.mnuFile_SaveProject.Text = "Sa&ve Project";
			this.mnuFile_SaveProject.Click += new System.EventHandler(this.mnuFile_SaveProject_Click);
			// 
			// mnuFile_SaveProjectAs
			// 
			this.mnuFile_SaveProjectAs.Name = "mnuFile_SaveProjectAs";
			this.mnuFile_SaveProjectAs.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_SaveProjectAs.Tag = "p";
			this.mnuFile_SaveProjectAs.Text = "Sav&e Project As...";
			this.mnuFile_SaveProjectAs.Click += new System.EventHandler(this.mnuFile_SaveProjectAs_Click);
			// 
			// mnuFile_CloseProject
			// 
			this.mnuFile_CloseProject.Name = "mnuFile_CloseProject";
			this.mnuFile_CloseProject.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_CloseProject.Tag = "p";
			this.mnuFile_CloseProject.Text = "C&lose Project";
			this.mnuFile_CloseProject.Click += new System.EventHandler(this.mnuFile_CloseProject_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_AddFile
			// 
			this.mnuFile_AddFile.Image = global::TSDev.Properties.Resources.document_new;
			this.mnuFile_AddFile.Name = "mnuFile_AddFile";
			this.mnuFile_AddFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
			this.mnuFile_AddFile.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_AddFile.Tag = "p";
			this.mnuFile_AddFile.Text = "Add &New Item...";
			this.mnuFile_AddFile.Click += new System.EventHandler(this.mnuFile_AddNew_Click);
			// 
			// mnuFile_AddExisting
			// 
			this.mnuFile_AddExisting.Image = global::TSDev.Properties.Resources.document_add;
			this.mnuFile_AddExisting.Name = "mnuFile_AddExisting";
			this.mnuFile_AddExisting.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
			this.mnuFile_AddExisting.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_AddExisting.Tag = "p";
			this.mnuFile_AddExisting.Text = "Add Existing I&tem...";
			this.mnuFile_AddExisting.Click += new System.EventHandler(this.mnuFile_AddExist_Click);
			// 
			// mnuFile_Import
			// 
			this.mnuFile_Import.Image = global::TSDev.Properties.Resources.import2;
			this.mnuFile_Import.Name = "mnuFile_Import";
			this.mnuFile_Import.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_Import.Tag = "p";
			this.mnuFile_Import.Text = "Import Existing &Directory...";
			this.mnuFile_Import.Click += new System.EventHandler(this.mnuFile_Import_Click);
			// 
			// mnuFile_RescanProject
			// 
			this.mnuFile_RescanProject.Image = global::TSDev.Properties.Resources.folder_view;
			this.mnuFile_RescanProject.Name = "mnuFile_RescanProject";
			this.mnuFile_RescanProject.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_RescanProject.Tag = "p";
			this.mnuFile_RescanProject.Text = "Rescan Project...";
			this.mnuFile_RescanProject.Click += new System.EventHandler(this.mnuFile_RescanProject_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_SaveFile
			// 
			this.mnuFile_SaveFile.Image = global::TSDev.Properties.Resources.disk_blue;
			this.mnuFile_SaveFile.Name = "mnuFile_SaveFile";
			this.mnuFile_SaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuFile_SaveFile.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_SaveFile.Tag = "f";
			this.mnuFile_SaveFile.Text = "&Save File";
			this.mnuFile_SaveFile.Click += new System.EventHandler(this.mnuFile_SaveFile_Click);
			// 
			// mnuFile_SaveAs
			// 
			this.mnuFile_SaveAs.Image = global::TSDev.Properties.Resources.save_as;
			this.mnuFile_SaveAs.Name = "mnuFile_SaveAs";
			this.mnuFile_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.A)));
			this.mnuFile_SaveAs.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_SaveAs.Tag = "f";
			this.mnuFile_SaveAs.Text = "Save File &As...";
			this.mnuFile_SaveAs.Click += new System.EventHandler(this.mnuFile_SaveAs_Click);
			// 
			// mnuFile_SaveAll
			// 
			this.mnuFile_SaveAll.Image = global::TSDev.Properties.Resources.disks;
			this.mnuFile_SaveAll.Name = "mnuFile_SaveAll";
			this.mnuFile_SaveAll.ShortcutKeyDisplayString = "";
			this.mnuFile_SaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.mnuFile_SaveAll.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_SaveAll.Tag = "f";
			this.mnuFile_SaveAll.Text = "Save All";
			this.mnuFile_SaveAll.Click += new System.EventHandler(this.mnuFile_SaveAll_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_CloseFile
			// 
			this.mnuFile_CloseFile.Name = "mnuFile_CloseFile";
			this.mnuFile_CloseFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
			this.mnuFile_CloseFile.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_CloseFile.Tag = "f";
			this.mnuFile_CloseFile.Text = "Close File";
			this.mnuFile_CloseFile.Click += new System.EventHandler(this.mnuFile_CloseFile_Click);
			// 
			// mnuFile_CloseAll
			// 
			this.mnuFile_CloseAll.Name = "mnuFile_CloseAll";
			this.mnuFile_CloseAll.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_CloseAll.Tag = "f";
			this.mnuFile_CloseAll.Text = "Close All";
			this.mnuFile_CloseAll.Click += new System.EventHandler(this.mnuFile_CloseAll_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_ExportHTML
			// 
			this.mnuFile_ExportHTML.Name = "mnuFile_ExportHTML";
			this.mnuFile_ExportHTML.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_ExportHTML.Tag = "f";
			this.mnuFile_ExportHTML.Text = "Export to &HTML...";
			this.mnuFile_ExportHTML.Click += new System.EventHandler(this.mnuFile_ExportHTML_Click);
			// 
			// mnuFile_ExportRTF
			// 
			this.mnuFile_ExportRTF.Name = "mnuFile_ExportRTF";
			this.mnuFile_ExportRTF.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_ExportRTF.Tag = "f";
			this.mnuFile_ExportRTF.Text = "Ex&port to Rich Text Format...";
			this.mnuFile_ExportRTF.Click += new System.EventHandler(this.mnuFile_ExportRTF_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_Print
			// 
			this.mnuFile_Print.Image = global::TSDev.Properties.Resources.printer3;
			this.mnuFile_Print.Name = "mnuFile_Print";
			this.mnuFile_Print.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.mnuFile_Print.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_Print.Tag = "f";
			this.mnuFile_Print.Text = "Print...";
			this.mnuFile_Print.Click += new System.EventHandler(this.mnuFile_Print_Click);
			// 
			// mnuFile_PrintPreview
			// 
			this.mnuFile_PrintPreview.Image = global::TSDev.Properties.Resources.printer_view;
			this.mnuFile_PrintPreview.Name = "mnuFile_PrintPreview";
			this.mnuFile_PrintPreview.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_PrintPreview.Tag = "f";
			this.mnuFile_PrintPreview.Text = "Print Preview...";
			this.mnuFile_PrintPreview.Click += new System.EventHandler(this.mnuFile_PrintPreview_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuFile_ShowWelcome
			// 
			this.mnuFile_ShowWelcome.Image = global::TSDev.Properties.Resources.window_star;
			this.mnuFile_ShowWelcome.Name = "mnuFile_ShowWelcome";
			this.mnuFile_ShowWelcome.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
			this.mnuFile_ShowWelcome.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_ShowWelcome.Text = "Show Welcome Dialog";
			this.mnuFile_ShowWelcome.Click += new System.EventHandler(this.mnuFile_ShowWelcome_Click);
			// 
			// mnuFile_Exit
			// 
			this.mnuFile_Exit.Image = global::TSDev.Properties.Resources.door2;
			this.mnuFile_Exit.Name = "mnuFile_Exit";
			this.mnuFile_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuFile_Exit.Size = new System.Drawing.Size(245, 22);
			this.mnuFile_Exit.Text = "E&xit";
			this.mnuFile_Exit.Click += new System.EventHandler(this.mnuFile_Exit_Click);
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_Undo,
            this.mnuEdit_Redo,
            this.toolStripMenuItem6,
            this.mnuEdit_Cut,
            this.mnuEdit_Copy,
            this.mnuEdit_Paste,
            this.toolStripMenuItem7,
            this.mnuEdit_Search,
            this.mnuEdit_SearchReplace,
            this.mnuEdit_GoTo,
            this.toolStripMenuItem8,
            this.mnuEdit_Bookmarks,
            this.mnuEdit_BreakPoint,
            this.toolStripMenuItem9,
            this.mnuEdit_CommentSel,
            this.mnuEdit_UncommentSel});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(39, 20);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEdit_Undo
			// 
			this.mnuEdit_Undo.Image = global::TSDev.Properties.Resources.undo;
			this.mnuEdit_Undo.Name = "mnuEdit_Undo";
			this.mnuEdit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.mnuEdit_Undo.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Undo.Tag = "f";
			this.mnuEdit_Undo.Text = "&Undo";
			this.mnuEdit_Undo.Click += new System.EventHandler(this.mnuEdit_Undo_Click);
			// 
			// mnuEdit_Redo
			// 
			this.mnuEdit_Redo.Image = global::TSDev.Properties.Resources.redo;
			this.mnuEdit_Redo.Name = "mnuEdit_Redo";
			this.mnuEdit_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.mnuEdit_Redo.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Redo.Tag = "f";
			this.mnuEdit_Redo.Text = "&Redo";
			this.mnuEdit_Redo.Click += new System.EventHandler(this.mnuEdit_Redo_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuEdit_Cut
			// 
			this.mnuEdit_Cut.Image = global::TSDev.Properties.Resources.cut;
			this.mnuEdit_Cut.Name = "mnuEdit_Cut";
			this.mnuEdit_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.mnuEdit_Cut.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Cut.Tag = "f";
			this.mnuEdit_Cut.Text = "C&ut";
			this.mnuEdit_Cut.Click += new System.EventHandler(this.mnuEdit_Cut_Click);
			// 
			// mnuEdit_Copy
			// 
			this.mnuEdit_Copy.Image = global::TSDev.Properties.Resources.copy;
			this.mnuEdit_Copy.Name = "mnuEdit_Copy";
			this.mnuEdit_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.mnuEdit_Copy.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Copy.Tag = "f";
			this.mnuEdit_Copy.Text = "&Copy";
			this.mnuEdit_Copy.Click += new System.EventHandler(this.mnuEdit_Copy_Click);
			// 
			// mnuEdit_Paste
			// 
			this.mnuEdit_Paste.Image = global::TSDev.Properties.Resources.paste;
			this.mnuEdit_Paste.Name = "mnuEdit_Paste";
			this.mnuEdit_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.mnuEdit_Paste.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Paste.Tag = "f";
			this.mnuEdit_Paste.Text = "&Paste";
			this.mnuEdit_Paste.Click += new System.EventHandler(this.mnuEdit_Paste_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuEdit_Search
			// 
			this.mnuEdit_Search.Image = global::TSDev.Properties.Resources.find;
			this.mnuEdit_Search.Name = "mnuEdit_Search";
			this.mnuEdit_Search.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.mnuEdit_Search.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Search.Tag = "f";
			this.mnuEdit_Search.Text = "&Search...";
			this.mnuEdit_Search.Click += new System.EventHandler(this.mnuEdit_Search_Click);
			// 
			// mnuEdit_SearchReplace
			// 
			this.mnuEdit_SearchReplace.Image = global::TSDev.Properties.Resources.replace;
			this.mnuEdit_SearchReplace.Name = "mnuEdit_SearchReplace";
			this.mnuEdit_SearchReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.mnuEdit_SearchReplace.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_SearchReplace.Tag = "f";
			this.mnuEdit_SearchReplace.Text = "R&eplace..";
			this.mnuEdit_SearchReplace.Click += new System.EventHandler(this.mnuEdit_SearchReplace_Click);
			// 
			// mnuEdit_GoTo
			// 
			this.mnuEdit_GoTo.Image = global::TSDev.Properties.Resources.arrow_right_blue;
			this.mnuEdit_GoTo.Name = "mnuEdit_GoTo";
			this.mnuEdit_GoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.mnuEdit_GoTo.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_GoTo.Tag = "f";
			this.mnuEdit_GoTo.Text = "&Goto Line...";
			this.mnuEdit_GoTo.Click += new System.EventHandler(this.mnuEdit_GoTo_Click);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuEdit_Bookmarks
			// 
			this.mnuEdit_Bookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_BKAdd,
            this.mnuEdit_BKPrev,
            this.mnuEdit_BKNext,
            this.mnuEdit_BKDel,
            this.mnuEdit_BKClearAll});
			this.mnuEdit_Bookmarks.Image = global::TSDev.Properties.Resources.bookmark;
			this.mnuEdit_Bookmarks.Name = "mnuEdit_Bookmarks";
			this.mnuEdit_Bookmarks.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_Bookmarks.Tag = "f";
			this.mnuEdit_Bookmarks.Text = "&Bookmarks";
			// 
			// mnuEdit_BKAdd
			// 
			this.mnuEdit_BKAdd.Image = global::TSDev.Properties.Resources.bookmark_add;
			this.mnuEdit_BKAdd.Name = "mnuEdit_BKAdd";
			this.mnuEdit_BKAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.mnuEdit_BKAdd.Size = new System.Drawing.Size(234, 22);
			this.mnuEdit_BKAdd.Text = "&Add New";
			this.mnuEdit_BKAdd.Click += new System.EventHandler(this.mnuEdit_BKAdd_Click);
			// 
			// mnuEdit_BKPrev
			// 
			this.mnuEdit_BKPrev.Image = global::TSDev.Properties.Resources.bookmark_up;
			this.mnuEdit_BKPrev.Name = "mnuEdit_BKPrev";
			this.mnuEdit_BKPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.mnuEdit_BKPrev.Size = new System.Drawing.Size(234, 22);
			this.mnuEdit_BKPrev.Text = "&Jump to Previous";
			this.mnuEdit_BKPrev.Click += new System.EventHandler(this.mnuEdit_BKPrev_Click);
			// 
			// mnuEdit_BKNext
			// 
			this.mnuEdit_BKNext.Image = global::TSDev.Properties.Resources.bookmark_down;
			this.mnuEdit_BKNext.Name = "mnuEdit_BKNext";
			this.mnuEdit_BKNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.mnuEdit_BKNext.Size = new System.Drawing.Size(234, 22);
			this.mnuEdit_BKNext.Text = "Jump to &Next";
			this.mnuEdit_BKNext.Click += new System.EventHandler(this.mnuEdit_BKNext_Click);
			// 
			// mnuEdit_BKDel
			// 
			this.mnuEdit_BKDel.Image = global::TSDev.Properties.Resources.bookmark_delete;
			this.mnuEdit_BKDel.Name = "mnuEdit_BKDel";
			this.mnuEdit_BKDel.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
			this.mnuEdit_BKDel.Size = new System.Drawing.Size(234, 22);
			this.mnuEdit_BKDel.Text = "&Remove Current";
			this.mnuEdit_BKDel.Click += new System.EventHandler(this.mnuEdit_BKDel_Click);
			// 
			// mnuEdit_BKClearAll
			// 
			this.mnuEdit_BKClearAll.Name = "mnuEdit_BKClearAll";
			this.mnuEdit_BKClearAll.Size = new System.Drawing.Size(234, 22);
			this.mnuEdit_BKClearAll.Text = "&Clear All";
			this.mnuEdit_BKClearAll.Click += new System.EventHandler(this.mnuEdit_BKClearAll_Click);
			// 
			// mnuEdit_BreakPoint
			// 
			this.mnuEdit_BreakPoint.Image = global::TSDev.Properties.Resources.stop;
			this.mnuEdit_BreakPoint.Name = "mnuEdit_BreakPoint";
			this.mnuEdit_BreakPoint.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.mnuEdit_BreakPoint.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_BreakPoint.Tag = "f";
			this.mnuEdit_BreakPoint.Text = "&Toggle Breakpoint";
			this.mnuEdit_BreakPoint.Click += new System.EventHandler(this.mnuEdit_BreakPoint_Click);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuEdit_CommentSel
			// 
			this.mnuEdit_CommentSel.Name = "mnuEdit_CommentSel";
			this.mnuEdit_CommentSel.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_CommentSel.Tag = "f";
			this.mnuEdit_CommentSel.Text = "Comment Selection";
			this.mnuEdit_CommentSel.Click += new System.EventHandler(this.mnuEdit_CommentSel_Click);
			// 
			// mnuEdit_UncommentSel
			// 
			this.mnuEdit_UncommentSel.Name = "mnuEdit_UncommentSel";
			this.mnuEdit_UncommentSel.Size = new System.Drawing.Size(192, 22);
			this.mnuEdit_UncommentSel.Tag = "f";
			this.mnuEdit_UncommentSel.Text = "Uncomment Selection";
			this.mnuEdit_UncommentSel.Click += new System.EventHandler(this.mnuEdit_UncommentSel_Click);
			// 
			// mnuProject
			// 
			this.mnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_SearchFiles,
            this.mnuEdit_Intellicode,
            this.toolStripMenuItem10,
            this.toolStripMenuItem16,
            this.mnuEdit_Prefs});
			this.mnuProject.Name = "mnuProject";
			this.mnuProject.Size = new System.Drawing.Size(56, 20);
			this.mnuProject.Text = "&Project";
			// 
			// mnuEdit_SearchFiles
			// 
			this.mnuEdit_SearchFiles.Image = global::TSDev.Properties.Resources.document_find;
			this.mnuEdit_SearchFiles.Name = "mnuEdit_SearchFiles";
			this.mnuEdit_SearchFiles.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
			this.mnuEdit_SearchFiles.Size = new System.Drawing.Size(267, 22);
			this.mnuEdit_SearchFiles.Tag = "p";
			this.mnuEdit_SearchFiles.Text = "Search files in &Project...";
			this.mnuEdit_SearchFiles.Click += new System.EventHandler(this.mnuEdit_SearchFiles_Click);
			// 
			// mnuEdit_Intellicode
			// 
			this.mnuEdit_Intellicode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_IC_ScanFile,
            this.mnuEdit_IC_ScanProj});
			this.mnuEdit_Intellicode.Image = global::TSDev.Properties.Resources.text_code_colored;
			this.mnuEdit_Intellicode.Name = "mnuEdit_Intellicode";
			this.mnuEdit_Intellicode.Size = new System.Drawing.Size(267, 22);
			this.mnuEdit_Intellicode.Tag = "p";
			this.mnuEdit_Intellicode.Text = "&Intellicode";
			// 
			// mnuEdit_IC_ScanFile
			// 
			this.mnuEdit_IC_ScanFile.Image = global::TSDev.Properties.Resources.Keyword;
			this.mnuEdit_IC_ScanFile.Name = "mnuEdit_IC_ScanFile";
			this.mnuEdit_IC_ScanFile.Size = new System.Drawing.Size(139, 22);
			this.mnuEdit_IC_ScanFile.Tag = "f";
			this.mnuEdit_IC_ScanFile.Text = "&Scan File";
			this.mnuEdit_IC_ScanFile.Click += new System.EventHandler(this.mnuEdit_IC_ScanFile_Click);
			// 
			// mnuEdit_IC_ScanProj
			// 
			this.mnuEdit_IC_ScanProj.Image = global::TSDev.Properties.Resources.asm;
			this.mnuEdit_IC_ScanProj.Name = "mnuEdit_IC_ScanProj";
			this.mnuEdit_IC_ScanProj.Size = new System.Drawing.Size(139, 22);
			this.mnuEdit_IC_ScanProj.Tag = "p";
			this.mnuEdit_IC_ScanProj.Text = "Scan &Project";
			this.mnuEdit_IC_ScanProj.Click += new System.EventHandler(this.mnuEdit_IC_ScanProj_Click);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(264, 6);
			// 
			// toolStripMenuItem16
			// 
			this.toolStripMenuItem16.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProjectToSourceControlToolStripMenuItem,
            this.removeProjectFromSourceControlToolStripMenuItem,
            this.toolStripMenuItem17,
            this.sourceControlPreferencesToolStripMenuItem});
			this.toolStripMenuItem16.Image = global::TSDev.Properties.Resources._16_torque_scc;
			this.toolStripMenuItem16.Name = "toolStripMenuItem16";
			this.toolStripMenuItem16.Size = new System.Drawing.Size(267, 22);
			this.toolStripMenuItem16.Tag = "scc";
			this.toolStripMenuItem16.Text = "Source Code Control";
			this.toolStripMenuItem16.Visible = false;
			// 
			// addProjectToSourceControlToolStripMenuItem
			// 
			this.addProjectToSourceControlToolStripMenuItem.Image = global::TSDev.Properties.Resources.server_add;
			this.addProjectToSourceControlToolStripMenuItem.Name = "addProjectToSourceControlToolStripMenuItem";
			this.addProjectToSourceControlToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.addProjectToSourceControlToolStripMenuItem.Tag = "scc_p";
			this.addProjectToSourceControlToolStripMenuItem.Text = "Add Project to Source Control";
			// 
			// removeProjectFromSourceControlToolStripMenuItem
			// 
			this.removeProjectFromSourceControlToolStripMenuItem.Image = global::TSDev.Properties.Resources.server_delete;
			this.removeProjectFromSourceControlToolStripMenuItem.Name = "removeProjectFromSourceControlToolStripMenuItem";
			this.removeProjectFromSourceControlToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.removeProjectFromSourceControlToolStripMenuItem.Tag = "scc_p";
			this.removeProjectFromSourceControlToolStripMenuItem.Text = "Remove Project from Source Control";
			// 
			// toolStripMenuItem17
			// 
			this.toolStripMenuItem17.Name = "toolStripMenuItem17";
			this.toolStripMenuItem17.Size = new System.Drawing.Size(265, 6);
			// 
			// sourceControlPreferencesToolStripMenuItem
			// 
			this.sourceControlPreferencesToolStripMenuItem.Image = global::TSDev.Properties.Resources._16_torque_scc_prefs;
			this.sourceControlPreferencesToolStripMenuItem.Name = "sourceControlPreferencesToolStripMenuItem";
			this.sourceControlPreferencesToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.sourceControlPreferencesToolStripMenuItem.Text = "Source Control Preferences...";
			// 
			// mnuEdit_Prefs
			// 
			this.mnuEdit_Prefs.Image = global::TSDev.Properties.Resources.preferences;
			this.mnuEdit_Prefs.Name = "mnuEdit_Prefs";
			this.mnuEdit_Prefs.Size = new System.Drawing.Size(267, 22);
			this.mnuEdit_Prefs.Text = "&Preferences";
			this.mnuEdit_Prefs.Click += new System.EventHandler(this.mnuEdit_Prefs_Click);
			// 
			// mnuMacros
			// 
			this.mnuMacros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMacro_Dir,
            this.mnuMacro_BeginRec,
            this.mnuMacro_EndRec,
            this.toolStripMenuItem11});
			this.mnuMacros.Name = "mnuMacros";
			this.mnuMacros.Size = new System.Drawing.Size(58, 20);
			this.mnuMacros.Tag = "f";
			this.mnuMacros.Text = "&Macros";
			// 
			// mnuMacro_Dir
			// 
			this.mnuMacro_Dir.Image = global::TSDev.Properties.Resources.folder_gear;
			this.mnuMacro_Dir.Name = "mnuMacro_Dir";
			this.mnuMacro_Dir.Size = new System.Drawing.Size(161, 22);
			this.mnuMacro_Dir.Tag = "p";
			this.mnuMacro_Dir.Text = "Macro &Directory";
			this.mnuMacro_Dir.Click += new System.EventHandler(this.mnuMacro_Dir_Click);
			// 
			// mnuMacro_BeginRec
			// 
			this.mnuMacro_BeginRec.Image = global::TSDev.Properties.Resources.nav_plain_red;
			this.mnuMacro_BeginRec.Name = "mnuMacro_BeginRec";
			this.mnuMacro_BeginRec.Size = new System.Drawing.Size(161, 22);
			this.mnuMacro_BeginRec.Tag = "macro_not_record";
			this.mnuMacro_BeginRec.Text = "&Begin Recording";
			this.mnuMacro_BeginRec.Click += new System.EventHandler(this.mnuMacro_BeginRec_Click);
			// 
			// mnuMacro_EndRec
			// 
			this.mnuMacro_EndRec.Image = global::TSDev.Properties.Resources.stop;
			this.mnuMacro_EndRec.Name = "mnuMacro_EndRec";
			this.mnuMacro_EndRec.Size = new System.Drawing.Size(161, 22);
			this.mnuMacro_EndRec.Tag = "macro_record";
			this.mnuMacro_EndRec.Text = "&End Recording";
			this.mnuMacro_EndRec.Click += new System.EventHandler(this.mnuMacro_EndRec_Click);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(158, 6);
			// 
			// mnuDebug
			// 
			this.mnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDebug_Start,
            this.mnuDebug_StartNoDebug,
            this.mnuDebug_Break,
            this.mnuDebug_Stop,
            this.toolStripMenuItem12,
            this.mnuDebug_StepInto,
            this.mnuDebug_StepOver,
            this.mnuDebug_StepOut,
            this.toolStripSeparator12,
            this.mnuDebug_Wizard});
			this.mnuDebug.Name = "mnuDebug";
			this.mnuDebug.Size = new System.Drawing.Size(54, 20);
			this.mnuDebug.Tag = "d";
			this.mnuDebug.Text = "&Debug";
			// 
			// mnuDebug_Start
			// 
			this.mnuDebug_Start.Image = global::TSDev.Properties.Resources.debug_run;
			this.mnuDebug_Start.Name = "mnuDebug_Start";
			this.mnuDebug_Start.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.mnuDebug_Start.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_Start.Tag = "d";
			this.mnuDebug_Start.Text = "&Start";
			this.mnuDebug_Start.Click += new System.EventHandler(this.mnuDebug_Start_Click);
			// 
			// mnuDebug_StartNoDebug
			// 
			this.mnuDebug_StartNoDebug.Image = global::TSDev.Properties.Resources.warning;
			this.mnuDebug_StartNoDebug.Name = "mnuDebug_StartNoDebug";
			this.mnuDebug_StartNoDebug.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.mnuDebug_StartNoDebug.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_StartNoDebug.Tag = "ndr";
			this.mnuDebug_StartNoDebug.Text = "Start &Without Debugging";
			this.mnuDebug_StartNoDebug.Click += new System.EventHandler(this.mnuDebug_StartNoDebug_Click);
			// 
			// mnuDebug_Break
			// 
			this.mnuDebug_Break.Image = global::TSDev.Properties.Resources.debug_pause;
			this.mnuDebug_Break.Name = "mnuDebug_Break";
			this.mnuDebug_Break.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
			this.mnuDebug_Break.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_Break.Tag = "ndb";
			this.mnuDebug_Break.Text = "&Break";
			this.mnuDebug_Break.Click += new System.EventHandler(this.mnuDebug_Break_Click);
			// 
			// mnuDebug_Stop
			// 
			this.mnuDebug_Stop.Image = global::TSDev.Properties.Resources.debug_stop;
			this.mnuDebug_Stop.Name = "mnuDebug_Stop";
			this.mnuDebug_Stop.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F12)));
			this.mnuDebug_Stop.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_Stop.Tag = "dr";
			this.mnuDebug_Stop.Text = "S&top Debugging";
			this.mnuDebug_Stop.Click += new System.EventHandler(this.mnuDebug_Stop_Click);
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			this.toolStripMenuItem12.Size = new System.Drawing.Size(249, 6);
			// 
			// mnuDebug_StepInto
			// 
			this.mnuDebug_StepInto.Image = global::TSDev.Properties.Resources.debug_into;
			this.mnuDebug_StepInto.Name = "mnuDebug_StepInto";
			this.mnuDebug_StepInto.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.mnuDebug_StepInto.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_StepInto.Tag = "drb";
			this.mnuDebug_StepInto.Text = "Step &Into";
			this.mnuDebug_StepInto.Click += new System.EventHandler(this.mnuDebug_StepInto_Click);
			// 
			// mnuDebug_StepOver
			// 
			this.mnuDebug_StepOver.Image = global::TSDev.Properties.Resources.debug_step_forward;
			this.mnuDebug_StepOver.Name = "mnuDebug_StepOver";
			this.mnuDebug_StepOver.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.mnuDebug_StepOver.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_StepOver.Tag = "drb";
			this.mnuDebug_StepOver.Text = "Step &Over";
			this.mnuDebug_StepOver.Click += new System.EventHandler(this.mnuDebug_StepOver_Click);
			// 
			// mnuDebug_StepOut
			// 
			this.mnuDebug_StepOut.Image = global::TSDev.Properties.Resources.debug_reset;
			this.mnuDebug_StepOut.Name = "mnuDebug_StepOut";
			this.mnuDebug_StepOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F11)));
			this.mnuDebug_StepOut.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_StepOut.Tag = "drb";
			this.mnuDebug_StepOut.Text = "Step O&ut";
			this.mnuDebug_StepOut.Click += new System.EventHandler(this.mnuDebug_StepOut_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(249, 6);
			// 
			// mnuDebug_Wizard
			// 
			this.mnuDebug_Wizard.Image = global::TSDev.Properties.Resources.debug;
			this.mnuDebug_Wizard.Name = "mnuDebug_Wizard";
			this.mnuDebug_Wizard.Size = new System.Drawing.Size(252, 22);
			this.mnuDebug_Wizard.Tag = "p";
			this.mnuDebug_Wizard.Text = "Debug Setup &Wizard...";
			this.mnuDebug_Wizard.Click += new System.EventHandler(this.mnuDebug_Wizard_Click);
			// 
			// mnuWindow
			// 
			this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindow_NewBrowser,
            this.mnuWindow_WorkspaceWindows,
            this.mnuPlugins,
            this.mnuWindow_ShowBar});
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Size = new System.Drawing.Size(63, 20);
			this.mnuWindow.Text = "&Window";
			// 
			// mnuWindow_NewBrowser
			// 
			this.mnuWindow_NewBrowser.Image = global::TSDev.Properties.Resources.window_earth;
			this.mnuWindow_NewBrowser.Name = "mnuWindow_NewBrowser";
			this.mnuWindow_NewBrowser.Size = new System.Drawing.Size(204, 22);
			this.mnuWindow_NewBrowser.Text = "&New Browser Window...";
			this.mnuWindow_NewBrowser.Click += new System.EventHandler(this.mnuWindow_NewBrowser_Click);
			// 
			// mnuWindow_WorkspaceWindows
			// 
			this.mnuWindow_WorkspaceWindows.Image = global::TSDev.Properties.Resources.windows;
			this.mnuWindow_WorkspaceWindows.Name = "mnuWindow_WorkspaceWindows";
			this.mnuWindow_WorkspaceWindows.Size = new System.Drawing.Size(204, 22);
			this.mnuWindow_WorkspaceWindows.Text = "Workspace Windows";
			// 
			// mnuPlugins
			// 
			this.mnuPlugins.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginSpecificItemsToolStripMenuItem});
			this.mnuPlugins.Image = global::TSDev.Properties.Resources.war;
			this.mnuPlugins.Name = "mnuPlugins";
			this.mnuPlugins.Size = new System.Drawing.Size(204, 22);
			this.mnuPlugins.Text = "&Plugins";
			// 
			// pluginSpecificItemsToolStripMenuItem
			// 
			this.pluginSpecificItemsToolStripMenuItem.Enabled = false;
			this.pluginSpecificItemsToolStripMenuItem.Name = "pluginSpecificItemsToolStripMenuItem";
			this.pluginSpecificItemsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.pluginSpecificItemsToolStripMenuItem.Text = "(Plugin Specific Items)";
			// 
			// mnuWindow_ShowBar
			// 
			this.mnuWindow_ShowBar.Name = "mnuWindow_ShowBar";
			this.mnuWindow_ShowBar.Size = new System.Drawing.Size(204, 22);
			this.mnuWindow_ShowBar.Text = "Show All &Developer Tabs";
			this.mnuWindow_ShowBar.Visible = false;
			this.mnuWindow_ShowBar.Click += new System.EventHandler(this.mnuWindow_ShowBar_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp_Website,
            this.mnuHelp_Forums,
            this.mnuHelp_ReportBugs,
            this.mnuHelp_Learn,
            this.toolStripMenuItem13,
            this.mnuHelp_Donate,
            this.toolStripMenuItem14,
            this.mnuAbout_Update,
            this.mnuAbout_License,
            this.mnuHelp_About});
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(44, 20);
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelp_Website
			// 
			this.mnuHelp_Website.Image = global::TSDev.Properties.Resources.cw_nor;
			this.mnuHelp_Website.Name = "mnuHelp_Website";
			this.mnuHelp_Website.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_Website.Text = "&TorqueDev Website...";
			this.mnuHelp_Website.Click += new System.EventHandler(this.mnuHelp_Website_Click);
			// 
			// mnuHelp_Forums
			// 
			this.mnuHelp_Forums.Image = global::TSDev.Properties.Resources.data_table;
			this.mnuHelp_Forums.Name = "mnuHelp_Forums";
			this.mnuHelp_Forums.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_Forums.Text = "TorqueDev &Forums...";
			this.mnuHelp_Forums.Visible = false;
			this.mnuHelp_Forums.Click += new System.EventHandler(this.mnuHelp_Forums_Click);
			// 
			// mnuHelp_ReportBugs
			// 
			this.mnuHelp_ReportBugs.Image = global::TSDev.Properties.Resources.debug;
			this.mnuHelp_ReportBugs.Name = "mnuHelp_ReportBugs";
			this.mnuHelp_ReportBugs.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_ReportBugs.Text = "&Report Bugs...";
			this.mnuHelp_ReportBugs.Visible = false;
			this.mnuHelp_ReportBugs.Click += new System.EventHandler(this.mnuHelp_ReportBugs_Click);
			// 
			// mnuHelp_Learn
			// 
			this.mnuHelp_Learn.Image = global::TSDev.Properties.Resources.scroll_view;
			this.mnuHelp_Learn.Name = "mnuHelp_Learn";
			this.mnuHelp_Learn.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_Learn.Text = "&Scripting Basics...";
			this.mnuHelp_Learn.Visible = false;
			this.mnuHelp_Learn.Click += new System.EventHandler(this.mnuHelp_Learn_Click);
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			this.toolStripMenuItem13.Size = new System.Drawing.Size(183, 6);
			this.toolStripMenuItem13.Visible = false;
			// 
			// mnuHelp_Donate
			// 
			this.mnuHelp_Donate.Image = global::TSDev.Properties.Resources.currency_dollar;
			this.mnuHelp_Donate.Name = "mnuHelp_Donate";
			this.mnuHelp_Donate.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_Donate.Text = "&Donate...";
			this.mnuHelp_Donate.Visible = false;
			this.mnuHelp_Donate.Click += new System.EventHandler(this.mnuHelp_Donate_Click);
			// 
			// toolStripMenuItem14
			// 
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			this.toolStripMenuItem14.Size = new System.Drawing.Size(183, 6);
			// 
			// mnuAbout_Update
			// 
			this.mnuAbout_Update.Image = global::TSDev.Properties.Resources.download;
			this.mnuAbout_Update.Name = "mnuAbout_Update";
			this.mnuAbout_Update.Size = new System.Drawing.Size(186, 22);
			this.mnuAbout_Update.Text = "&Update Wizard...";
			this.mnuAbout_Update.Visible = false;
			this.mnuAbout_Update.Click += new System.EventHandler(this.mnuAbout_Update_Click);
			// 
			// mnuAbout_License
			// 
			this.mnuAbout_License.Image = global::TSDev.Properties.Resources.application_enterprise_certificate;
			this.mnuAbout_License.Name = "mnuAbout_License";
			this.mnuAbout_License.Size = new System.Drawing.Size(186, 22);
			this.mnuAbout_License.Text = "License Manager...";
			this.mnuAbout_License.Visible = false;
			this.mnuAbout_License.Click += new System.EventHandler(this.mnuAbout_License_Click);
			// 
			// mnuHelp_About
			// 
			this.mnuHelp_About.Image = global::TSDev.Properties.Resources.box_software;
			this.mnuHelp_About.Name = "mnuHelp_About";
			this.mnuHelp_About.Size = new System.Drawing.Size(186, 22);
			this.mnuHelp_About.Text = "&About TorqueDev...";
			this.mnuHelp_About.Click += new System.EventHandler(this.mnuHelp_About_Click);
			// 
			// tbEditBar
			// 
			this.tbEditBar.AllowItemReorder = true;
			this.tbEditBar.Dock = System.Windows.Forms.DockStyle.None;
			this.tbEditBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbNewProject,
            this.tbbOpenProject,
            this.toolStripSeparator1,
            this.tbbNewItem,
            this.tbbExistingItem,
            this.toolStripSeparator2,
            this.tbbSave,
            this.tbbSaveAs,
            this.tbbSaveAll,
            this.toolStripSeparator3,
            this.tbbPrint,
            this.tbbPrintPreview,
            this.toolStripSeparator4,
            this.tbbCut,
            this.tbbCopy,
            this.tbbPaste,
            this.tbbDelete,
            this.toolStripSeparator5,
            this.tbbUndo,
            this.tbbRedo,
            this.toolStripSeparator6,
            this.toolStripLabel1,
            this.cboRecentSearches,
            this.tbbSearch,
            this.tbbFind,
            this.tbbFindFiles,
            this.toolStripSeparator7,
            this.tbbMarkAdd,
            this.tbbMarkUp,
            this.tbbMarkDown,
            this.tbbMarkDel,
            this.toolStripSeparator8,
            this.tbbDebugStart,
            this.tbbDebugRun,
            this.tbbDebugBreak,
            this.tbbDebugStop,
            this.toolStripSeparator9,
            this.tbbDebugStepInto,
            this.tbbDebugStepOver,
            this.tbbDebugStepOut,
            this.toolStripSeparator10});
			this.tbEditBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tbEditBar.Location = new System.Drawing.Point(3, 24);
			this.tbEditBar.Name = "tbEditBar";
			this.tbEditBar.Size = new System.Drawing.Size(797, 25);
			this.tbEditBar.TabIndex = 0;
			this.tbEditBar.Text = "toolStrip1";
			// 
			// tbbNewProject
			// 
			this.tbbNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbNewProject.Image = global::TSDev.Properties.Resources.folder_new;
			this.tbbNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbNewProject.Name = "tbbNewProject";
			this.tbbNewProject.Size = new System.Drawing.Size(23, 22);
			this.tbbNewProject.Text = "New Project";
			this.tbbNewProject.Click += new System.EventHandler(this.tbbNewProject_Click);
			// 
			// tbbOpenProject
			// 
			this.tbbOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbOpenProject.Image = global::TSDev.Properties.Resources.folder;
			this.tbbOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbOpenProject.Name = "tbbOpenProject";
			this.tbbOpenProject.Size = new System.Drawing.Size(23, 22);
			this.tbbOpenProject.Text = "Open Project";
			this.tbbOpenProject.Click += new System.EventHandler(this.tbbOpenProject_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbNewItem
			// 
			this.tbbNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbNewItem.Image = global::TSDev.Properties.Resources.document_new;
			this.tbbNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbNewItem.Name = "tbbNewItem";
			this.tbbNewItem.Size = new System.Drawing.Size(23, 22);
			this.tbbNewItem.Tag = "p";
			this.tbbNewItem.Text = "Add New Item...";
			this.tbbNewItem.Click += new System.EventHandler(this.tbbNewItem_Click);
			// 
			// tbbExistingItem
			// 
			this.tbbExistingItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbExistingItem.Image = global::TSDev.Properties.Resources.document_add;
			this.tbbExistingItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbExistingItem.Name = "tbbExistingItem";
			this.tbbExistingItem.Size = new System.Drawing.Size(23, 22);
			this.tbbExistingItem.Tag = "p";
			this.tbbExistingItem.Text = "Add Existing Item...";
			this.tbbExistingItem.Click += new System.EventHandler(this.tbbExistingItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbSave
			// 
			this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbSave.Image = global::TSDev.Properties.Resources.disk_blue;
			this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbSave.Name = "tbbSave";
			this.tbbSave.Size = new System.Drawing.Size(23, 22);
			this.tbbSave.Tag = "f";
			this.tbbSave.Text = "Save";
			this.tbbSave.Click += new System.EventHandler(this.tbbSave_Click);
			// 
			// tbbSaveAs
			// 
			this.tbbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbSaveAs.Image = global::TSDev.Properties.Resources.save_as;
			this.tbbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbSaveAs.Name = "tbbSaveAs";
			this.tbbSaveAs.Size = new System.Drawing.Size(23, 22);
			this.tbbSaveAs.Tag = "f";
			this.tbbSaveAs.Text = "Save As...";
			this.tbbSaveAs.Click += new System.EventHandler(this.tbbSaveAs_Click);
			// 
			// tbbSaveAll
			// 
			this.tbbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbSaveAll.Image = global::TSDev.Properties.Resources.disks;
			this.tbbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbSaveAll.Name = "tbbSaveAll";
			this.tbbSaveAll.Size = new System.Drawing.Size(23, 22);
			this.tbbSaveAll.Tag = "f";
			this.tbbSaveAll.Text = "Save All";
			this.tbbSaveAll.Click += new System.EventHandler(this.tbbSaveAll_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbPrint
			// 
			this.tbbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbPrint.Image = global::TSDev.Properties.Resources.printer3;
			this.tbbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbPrint.Name = "tbbPrint";
			this.tbbPrint.Size = new System.Drawing.Size(23, 22);
			this.tbbPrint.Tag = "f";
			this.tbbPrint.Text = "Print...";
			this.tbbPrint.Click += new System.EventHandler(this.tbbPrint_Click);
			// 
			// tbbPrintPreview
			// 
			this.tbbPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbPrintPreview.Image = global::TSDev.Properties.Resources.printer_view;
			this.tbbPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbPrintPreview.Name = "tbbPrintPreview";
			this.tbbPrintPreview.Size = new System.Drawing.Size(23, 22);
			this.tbbPrintPreview.Tag = "f";
			this.tbbPrintPreview.Text = "Print Preview";
			this.tbbPrintPreview.Click += new System.EventHandler(this.tbbPrintPreview_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbCut
			// 
			this.tbbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbCut.Image = global::TSDev.Properties.Resources.cut;
			this.tbbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbCut.Name = "tbbCut";
			this.tbbCut.Size = new System.Drawing.Size(23, 22);
			this.tbbCut.Tag = "f";
			this.tbbCut.Text = "Cut";
			this.tbbCut.Click += new System.EventHandler(this.tbbCut_Click);
			// 
			// tbbCopy
			// 
			this.tbbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbCopy.Image = global::TSDev.Properties.Resources.copy;
			this.tbbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbCopy.Name = "tbbCopy";
			this.tbbCopy.Size = new System.Drawing.Size(23, 22);
			this.tbbCopy.Tag = "f";
			this.tbbCopy.Text = "Copy";
			this.tbbCopy.Click += new System.EventHandler(this.tbbCopy_Click);
			// 
			// tbbPaste
			// 
			this.tbbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbPaste.Image = global::TSDev.Properties.Resources.paste;
			this.tbbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbPaste.Name = "tbbPaste";
			this.tbbPaste.Size = new System.Drawing.Size(23, 22);
			this.tbbPaste.Tag = "f";
			this.tbbPaste.Text = "Paste";
			this.tbbPaste.Click += new System.EventHandler(this.tbbPaste_Click);
			// 
			// tbbDelete
			// 
			this.tbbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDelete.Image = global::TSDev.Properties.Resources.delete2;
			this.tbbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDelete.Name = "tbbDelete";
			this.tbbDelete.Size = new System.Drawing.Size(23, 22);
			this.tbbDelete.Tag = "f";
			this.tbbDelete.Text = "Delete";
			this.tbbDelete.Click += new System.EventHandler(this.tbbDelete_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbUndo
			// 
			this.tbbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbUndo.Image = global::TSDev.Properties.Resources.undo;
			this.tbbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbUndo.Name = "tbbUndo";
			this.tbbUndo.Size = new System.Drawing.Size(23, 22);
			this.tbbUndo.Tag = "f";
			this.tbbUndo.Text = "Undo";
			this.tbbUndo.Click += new System.EventHandler(this.tbbUndo_Click);
			// 
			// tbbRedo
			// 
			this.tbbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbRedo.Image = global::TSDev.Properties.Resources.redo;
			this.tbbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbRedo.Name = "tbbRedo";
			this.tbbRedo.Size = new System.Drawing.Size(23, 22);
			this.tbbRedo.Tag = "f";
			this.tbbRedo.Text = "Redo";
			this.tbbRedo.Click += new System.EventHandler(this.tbbRedo_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
			this.toolStripLabel1.Text = "Find";
			// 
			// cboRecentSearches
			// 
			this.cboRecentSearches.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cboRecentSearches.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboRecentSearches.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboRecentSearches.Name = "cboRecentSearches";
			this.cboRecentSearches.Size = new System.Drawing.Size(121, 25);
			// 
			// tbbSearch
			// 
			this.tbbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbSearch.Image = global::TSDev.Properties.Resources.find;
			this.tbbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbSearch.Name = "tbbSearch";
			this.tbbSearch.Size = new System.Drawing.Size(23, 22);
			this.tbbSearch.Tag = "f";
			this.tbbSearch.Text = "Find...";
			this.tbbSearch.Click += new System.EventHandler(this.tbbSearch_Click);
			// 
			// tbbFind
			// 
			this.tbbFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbFind.Image = global::TSDev.Properties.Resources.replace;
			this.tbbFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbFind.Name = "tbbFind";
			this.tbbFind.Size = new System.Drawing.Size(23, 22);
			this.tbbFind.Tag = "f";
			this.tbbFind.Text = "Replace...";
			this.tbbFind.Click += new System.EventHandler(this.tbbFind_Click);
			// 
			// tbbFindFiles
			// 
			this.tbbFindFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbFindFiles.Image = global::TSDev.Properties.Resources.document_find;
			this.tbbFindFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbFindFiles.Name = "tbbFindFiles";
			this.tbbFindFiles.Size = new System.Drawing.Size(23, 22);
			this.tbbFindFiles.Tag = "p";
			this.tbbFindFiles.Text = "Find in Project...";
			this.tbbFindFiles.Click += new System.EventHandler(this.tbbFindFiles_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbMarkAdd
			// 
			this.tbbMarkAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbMarkAdd.Image = global::TSDev.Properties.Resources.bookmark_add;
			this.tbbMarkAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbMarkAdd.Name = "tbbMarkAdd";
			this.tbbMarkAdd.Size = new System.Drawing.Size(23, 22);
			this.tbbMarkAdd.Tag = "f";
			this.tbbMarkAdd.Text = "Add Bookmark";
			this.tbbMarkAdd.Click += new System.EventHandler(this.tbbMarkAdd_Click);
			// 
			// tbbMarkUp
			// 
			this.tbbMarkUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbMarkUp.Image = global::TSDev.Properties.Resources.bookmark_up;
			this.tbbMarkUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbMarkUp.Name = "tbbMarkUp";
			this.tbbMarkUp.Size = new System.Drawing.Size(23, 22);
			this.tbbMarkUp.Tag = "f";
			this.tbbMarkUp.Text = "Jump to Previous Bookmark";
			this.tbbMarkUp.Click += new System.EventHandler(this.tbbMarkUp_Click);
			// 
			// tbbMarkDown
			// 
			this.tbbMarkDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbMarkDown.Image = global::TSDev.Properties.Resources.bookmark_down;
			this.tbbMarkDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbMarkDown.Name = "tbbMarkDown";
			this.tbbMarkDown.Size = new System.Drawing.Size(23, 22);
			this.tbbMarkDown.Tag = "f";
			this.tbbMarkDown.Text = "Jump to Next Bookmark";
			this.tbbMarkDown.Click += new System.EventHandler(this.tbbMarkDown_Click);
			// 
			// tbbMarkDel
			// 
			this.tbbMarkDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbMarkDel.Image = global::TSDev.Properties.Resources.bookmark_delete;
			this.tbbMarkDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbMarkDel.Name = "tbbMarkDel";
			this.tbbMarkDel.Size = new System.Drawing.Size(23, 22);
			this.tbbMarkDel.Tag = "f";
			this.tbbMarkDel.Text = "Delete Bookmark";
			this.tbbMarkDel.Click += new System.EventHandler(this.tbbMarkDel_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbDebugStart
			// 
			this.tbbDebugStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugStart.Image = global::TSDev.Properties.Resources.debug_run;
			this.tbbDebugStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugStart.Name = "tbbDebugStart";
			this.tbbDebugStart.Size = new System.Drawing.Size(23, 22);
			this.tbbDebugStart.Tag = "d";
			this.tbbDebugStart.Text = "Start Debugging";
			this.tbbDebugStart.Click += new System.EventHandler(this.tbbDebugStart_Click);
			// 
			// tbbDebugRun
			// 
			this.tbbDebugRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugRun.Image = global::TSDev.Properties.Resources.warning;
			this.tbbDebugRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugRun.Name = "tbbDebugRun";
			this.tbbDebugRun.Size = new System.Drawing.Size(23, 22);
			this.tbbDebugRun.Tag = "ndr";
			this.tbbDebugRun.Text = "Run without Debugging";
			this.tbbDebugRun.Click += new System.EventHandler(this.tbbDebugRun_Click);
			// 
			// tbbDebugBreak
			// 
			this.tbbDebugBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugBreak.Image = global::TSDev.Properties.Resources.debug_pause;
			this.tbbDebugBreak.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugBreak.Name = "tbbDebugBreak";
			this.tbbDebugBreak.Size = new System.Drawing.Size(23, 20);
			this.tbbDebugBreak.Tag = "ndb";
			this.tbbDebugBreak.Text = "Break";
			this.tbbDebugBreak.Click += new System.EventHandler(this.tbbDebugBreak_Click);
			// 
			// tbbDebugStop
			// 
			this.tbbDebugStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugStop.Image = global::TSDev.Properties.Resources.debug_stop;
			this.tbbDebugStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugStop.Name = "tbbDebugStop";
			this.tbbDebugStop.Size = new System.Drawing.Size(23, 20);
			this.tbbDebugStop.Tag = "dr";
			this.tbbDebugStop.Text = "Stop Debugging";
			this.tbbDebugStop.Click += new System.EventHandler(this.tbbDebugStop_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
			// 
			// tbbDebugStepInto
			// 
			this.tbbDebugStepInto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugStepInto.Image = global::TSDev.Properties.Resources.debug_into;
			this.tbbDebugStepInto.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugStepInto.Name = "tbbDebugStepInto";
			this.tbbDebugStepInto.Size = new System.Drawing.Size(23, 20);
			this.tbbDebugStepInto.Tag = "drb";
			this.tbbDebugStepInto.Text = "Step Into";
			this.tbbDebugStepInto.Click += new System.EventHandler(this.tbbDebugStepInto_Click);
			// 
			// tbbDebugStepOver
			// 
			this.tbbDebugStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugStepOver.Image = global::TSDev.Properties.Resources.debug_step_forward;
			this.tbbDebugStepOver.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugStepOver.Name = "tbbDebugStepOver";
			this.tbbDebugStepOver.Size = new System.Drawing.Size(23, 20);
			this.tbbDebugStepOver.Tag = "drb";
			this.tbbDebugStepOver.Text = "Step Over";
			this.tbbDebugStepOver.Click += new System.EventHandler(this.tbbDebugStepOver_Click);
			// 
			// tbbDebugStepOut
			// 
			this.tbbDebugStepOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbbDebugStepOut.Image = global::TSDev.Properties.Resources.debug_reset;
			this.tbbDebugStepOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbbDebugStepOut.Name = "tbbDebugStepOut";
			this.tbbDebugStepOut.Size = new System.Drawing.Size(23, 20);
			this.tbbDebugStepOut.Tag = "drb";
			this.tbbDebugStepOut.Text = "Step Out";
			this.tbbDebugStepOut.Click += new System.EventHandler(this.tbbDebugStepOut_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
			// 
			// sbMain
			// 
			this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbpFileName,
            this.toolStripStatusLabel6,
            this.toolStripProgressBar1,
            this.sbpRow,
            this.sbpCol,
            this.sbpTextInfo,
            this.sbpSelection});
			this.sbMain.Location = new System.Drawing.Point(0, 495);
			this.sbMain.Name = "sbMain";
			this.sbMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.sbMain.Size = new System.Drawing.Size(800, 22);
			this.sbMain.TabIndex = 21;
			this.sbMain.Text = "statusStrip1";
			// 
			// sbpFileName
			// 
			this.sbpFileName.Image = global::TSDev.Properties.Resources.document;
			this.sbpFileName.Name = "sbpFileName";
			this.sbpFileName.Size = new System.Drawing.Size(102, 17);
			this.sbpFileName.Text = "No File Loaded";
			this.sbpFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel6
			// 
			this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
			this.toolStripStatusLabel6.Size = new System.Drawing.Size(463, 17);
			this.toolStripStatusLabel6.Spring = true;
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.toolStripProgressBar1.Size = new System.Drawing.Size(110, 16);
			this.toolStripProgressBar1.Visible = false;
			// 
			// sbpRow
			// 
			this.sbpRow.Image = global::TSDev.Properties.Resources.window_split_ver;
			this.sbpRow.Name = "sbpRow";
			this.sbpRow.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.sbpRow.Size = new System.Drawing.Size(55, 17);
			this.sbpRow.Text = "N/A";
			// 
			// sbpCol
			// 
			this.sbpCol.Image = global::TSDev.Properties.Resources.window_split_hor;
			this.sbpCol.Name = "sbpCol";
			this.sbpCol.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.sbpCol.Size = new System.Drawing.Size(55, 17);
			this.sbpCol.Text = "N/A";
			// 
			// sbpTextInfo
			// 
			this.sbpTextInfo.Image = global::TSDev.Properties.Resources.view;
			this.sbpTextInfo.Name = "sbpTextInfo";
			this.sbpTextInfo.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.sbpTextInfo.Size = new System.Drawing.Size(55, 17);
			this.sbpTextInfo.Text = "N/A";
			// 
			// sbpSelection
			// 
			this.sbpSelection.Image = global::TSDev.Properties.Resources.selection_view;
			this.sbpSelection.Name = "sbpSelection";
			this.sbpSelection.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.sbpSelection.Size = new System.Drawing.Size(55, 17);
			this.sbpSelection.Text = "N/A";
			// 
			// pnlToolbars
			// 
			this.pnlToolbars.AutoSize = true;
			this.pnlToolbars.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlToolbars.Location = new System.Drawing.Point(0, 50);
			this.pnlToolbars.Name = "pnlToolbars";
			this.pnlToolbars.Size = new System.Drawing.Size(800, 0);
			this.pnlToolbars.TabIndex = 25;
			// 
			// tscToolbars
			// 
			this.tscToolbars.BottomToolStripPanelVisible = false;
			// 
			// tscToolbars.ContentPanel
			// 
			this.tscToolbars.ContentPanel.Size = new System.Drawing.Size(800, 1);
			this.tscToolbars.Dock = System.Windows.Forms.DockStyle.Top;
			this.tscToolbars.LeftToolStripPanelVisible = false;
			this.tscToolbars.Location = new System.Drawing.Point(0, 0);
			this.tscToolbars.Name = "tscToolbars";
			this.tscToolbars.RightToolStripPanelVisible = false;
			this.tscToolbars.Size = new System.Drawing.Size(800, 50);
			this.tscToolbars.TabIndex = 26;
			this.tscToolbars.Text = "toolStripContainer2";
			// 
			// tscToolbars.TopToolStripPanel
			// 
			this.tscToolbars.TopToolStripPanel.Controls.Add(this.mnuMain);
			this.tscToolbars.TopToolStripPanel.Controls.Add(this.tbEditBar);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(800, 517);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pnlToolbars);
			this.Controls.Add(this.tscToolbars);
			this.Controls.Add(this.sbMain);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.mnuMain;
			this.Name = "frmMain";
			this.Text = "TorqueDev";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.Closed += new System.EventHandler(this.frmMain_Closed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ctmProject.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.pnlExplorer.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.pnlExports.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.tbEditBar.ResumeLayout(false);
			this.tbEditBar.PerformLayout();
			this.sbMain.ResumeLayout(false);
			this.sbMain.PerformLayout();
			this.tscToolbars.TopToolStripPanel.ResumeLayout(false);
			this.tscToolbars.TopToolStripPanel.PerformLayout();
			this.tscToolbars.ResumeLayout(false);
			this.tscToolbars.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private ToolStripMenuItem mnuDebug_Wizard;
		private ToolStripSeparator toolStripSeparator12;
		public ToolStripComboBox cboRecentSearches;
		private ToolStripMenuItem mnuAbout_License;
		private Panel pnlToolbars;
		private ToolStripMenuItem mnuWindow_WorkspaceWindows;
		public ImageList ilDocBar;
		public ToolStripContainer tscToolbars;
		private ToolStripMenuItem mnuCreateFolder;
		private ToolStripMenuItem mnuProject_AddNew;
		private ToolStripMenuItem mnuProject_AddExist;
		private ToolStripMenuItem mnuProject_Import;
		private ToolStripSeparator toolStripSeparator13;
		private ToolStripMenuItem mnuProject_Rename;
		private ToolStripMenuItem mnuProject_Del;
		private ToolStripMenuItem mnuProject_Properties;
		public ContextMenuStrip ctmProject;
		public ToolStripMenuItem mnuPlugins;
		private ToolStripSeparator toolStripMenuItem15;
		private ToolStripMenuItem mnuSCC;
		private ToolStripMenuItem mnuSCC_CheckIn;
		private ToolStripMenuItem mnuSCC_CheckOut;
		private ToolStripMenuItem mnuSCC_UndoCheckOut;
		private ToolStripSeparator toolStripSeparator14;
		private ToolStripMenuItem mnuSCC_GetLatest;
		private ToolStripSeparator toolStripSeparator15;
		private ToolStripMenuItem mnuSCC_AddToScc;
		private ToolStripMenuItem mnuSCC_DelFromScc;
		private ToolStripSeparator toolStripSeparator16;
		private ToolStripMenuItem mnuSCC_Diff;
		private ToolStripMenuItem mnuSCC_History;
		private ToolStripMenuItem toolStripMenuItem16;
		private ToolStripMenuItem addProjectToSourceControlToolStripMenuItem;
		private ToolStripMenuItem removeProjectFromSourceControlToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem17;
		private ToolStripMenuItem sourceControlPreferencesToolStripMenuItem;
		private ToolStripMenuItem mnuFile_Open_ProjectFromFile;
		private ToolStripMenuItem mnuFile_Open_ProjectFromScc;
		private ToolStripSeparator toolStripMenuItem18;
		private ToolStripMenuItem mnuFile_Open_File;
		private ToolStripMenuItem mnuEdit_BKClearAll;
	}
}