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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TSDev {

	internal partial class frmMain : System.Windows.Forms.Form {

		[StructLayout(LayoutKind.Sequential)]
		private struct COPYDATASTRUCT {
			public IntPtr dwData;
			public int cbData;
			public IntPtr lpData;
		}

		[DllImport("user32", CharSet = CharSet.Auto)]
		private extern static int SendMessage(
			IntPtr hwnd,
			int wMsg,
			int wParam,
			ref COPYDATASTRUCT lParam
			);
		

		public frmMain() {
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();

			g.Main = this;

			g.LogDebug("INIT0: Initializing delegates");
			this.__AddNode = new __del_AddNode(tvExplorer.Nodes.Add);
			this.__ClearNodes = new __del_ClearNodes(tvExplorer.Nodes.Clear);
			this.__DebugProcessCmd = new __del_DebugProcessCmd(DebugParseData);
			this.__DebugWriteLog = new __del_DebugWriteLog(DebugWriteLog);
			this.__ThreadedMessageBox = new __del_ThreadedMessageBox(ThreadedMessageBox);

			// Initialize snippetter
			//this.SnippetterUC = new UCSnippetter();

			// Initialize error watcher
			this.ErrorUC = new UCError();

			g.LogDebug("INIT0: Initializing dockmanager");
			dockMgr = new DockingManager(this, VisualStyle.Office2003);

			dockMgr.OuterControl = this.tscToolbars;
			dockMgr.InnerControl = this.tabMain;

			ContentProject = dockMgr.Contents.Add(this.tvProject, "Project");
			ContentExplorer = dockMgr.Contents.Add(this.pnlExplorer, "Explorer");
			ContentExports = dockMgr.Contents.Add(this.pnlExports, "Exports");
			ContentErrors = dockMgr.Contents.Add(this.ErrorUC, "Errors");

			ContentProject.ImageList = this.ilProject;
			ContentExplorer.ImageList = this.ilProject;
			ContentExports.ImageList = this.ilProject;
			ContentErrors.ImageList = this.ilProject;

			ContentProject.ImageIndex = 5;
			ContentExplorer.ImageIndex = 3;
			ContentExports.ImageIndex = 6;
			ContentErrors.ImageIndex = 22;

			WindowContent wc = dockMgr.AddContentWithState(ContentProject, State.DockLeft);
			
			dockMgr.AddContentToWindowContent(ContentExplorer, wc);
			dockMgr.AddContentToWindowContent(ContentExports, wc);
			
			dockMgr.AddContentWithState(ContentErrors, State.DockBottom);

			ProjectWindowContent = wc;

			tscToolbars.Height = (tbEditBar.Height + mnuMain.Height);

			// Load up the workspace windows into the global
			// variable
			g.LogDebug("INIT0: Initializing workspaces");
			g.WorkspaceWindows.Add(new WorkspaceWindow(ContentProject, "Project", ilProject.Images[5]));
			g.WorkspaceWindows.Add(new WorkspaceWindow(ContentExplorer, "Explorer", ilProject.Images[3]));
			g.WorkspaceWindows.Add(new WorkspaceWindow(ContentExports, "Exports", ilProject.Images[6]));
			g.WorkspaceWindows.Add(new WorkspaceWindow(ContentErrors, "Errors", ilProject.Images[22]));

			this.panel1.Visible = false;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] argv) {
			// Check for debug mode
			if (argv.Length > 0 && argv[0] == "--debug-mode") {
				g.InitDebug();
				g.LogDebug("----- TorqueDev Integrated Development Environment -----");
				g.LogDebug("Version " + Application.ProductVersion + " Init");
				g.LogDebug("Copyright (c) 2005 - 2009 netMercs Group LLC (www.torquedev.com)");
				g.LogDebug("All rights reserved.\r\n\r\n");
			}
			// Load dependencies into memory:
			try {
				//AppDomain.CurrentDomain.Load(ReadAssemblyResource("ActiproSoftware.WinUICore.dll"));
				//AppDomain.CurrentDomain.Load(ReadAssemblyResource("ActiproSoftware.Shared.dll"));
				//AppDomain.CurrentDomain.Load(ReadAssemblyResource("ActiproSoftware.syntaxeditor.dll"));
				//AppDomain.CurrentDomain.Load(ReadAssemblyResource("dotnetmagic.dll"));
				g.LogDebug("PRE-INIT: netMercs.Loader.Embed.Init() - Loader initializing embedded libraries...");
				netMercs.Loader.Embed.Init();
			} catch (Exception exc) {
				g.LogDebug("PRE-INIT: Failure " + exc.Message);
				MessageBox.Show("Failed to load dependency: " + exc.Message);
				Application.Exit();
				return;
			}

			g.LogDebug("PRE-INIT: Binding exception handler");
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

			// Set up the user's global path, and cut out the version
			// information:			
			//CConfig.GlobalUserpath = Application.StartupPath; //((Application.LocalUserAppDataPath.IndexOf(Application.ProductVersion) != -1) ? Application.LocalUserAppDataPath.Replace("\\" + Application.ProductVersion, "") : Application.LocalUserAppDataPath);
			//CConfig.GlobalUserpath = ((Application.LocalUserAppDataPath.IndexOf(Application.ProductVersion) != -1) ? Application.LocalUserAppDataPath.Replace("\\" + Application.ProductVersion, "") : Application.LocalUserAppDataPath);

			CConfig.GlobalUserpath = Application.UserAppDataPath.Replace("\\" + Application.ProductVersion, "");
			g.LogDebug("PRE-INIT: Init global user path: " + CConfig.GlobalUserpath);

			// Delete the *other* directory
			g.LogDebug("PRE-INIT: Cleanup " + Application.UserAppDataPath);
			Directory.Delete(Application.UserAppDataPath, true);

			g.LogDebug("PRE-INIT: Priority boost / HIGH PRI set");
			System.Diagnostics.Process.GetCurrentProcess().PriorityBoostEnabled = true;
			System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;

			frmMain.stc_Args = argv;

			// If the parameters are NOT blank AND include something that's NOT a project,
			// check to see if there's a previous instance of CW open and open the file in that
			// instance
			if (argv.Length > 0) {
				if (Path.GetExtension(argv[0]) != ".tsdev") {
					// Find a previous instance of CW
					g.LogDebug("PRE-INIT: CLP shows it wants to load " + argv[0] + "; looking for prev instance...");
					Process[] prevcw = Process.GetProcessesByName("tsdev");

					if (prevcw.Length > 1) {
						g.LogDebug("PRE-INIT::PrevInstanceSearch: Found. Sending window message");
						IntPtr ptrFilename = Marshal.AllocCoTaskMem(argv[0].Length + 5);
						Marshal.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("FILE|" + argv[0]), 0, ptrFilename, argv[0].Length + 5);

						COPYDATASTRUCT cds = new COPYDATASTRUCT();
						cds.cbData = argv[0].Length + 5;
						cds.dwData = IntPtr.Zero;
						cds.lpData = ptrFilename;

						SendMessage(prevcw[0].MainWindowHandle, 0x4A, 0, ref cds);

						g.LogDebug("PRE-INIT::PrevInstanceSearch: Done. Quit.");
						return;
					}

					g.LogDebug("PRE-INIT::PrevInstanceSearch: None found. Continuing");
				}
			}

			g.LogDebug("PRE-INIT: Init visual styles");
			Application.EnableVisualStyles();
			Application.DoEvents();

			g.LogDebug("PRE-INIT: Application.Run; Activating main message pump");
			Application.Run(new frmMain());

			g.LogDebug("EXIT: Application End");
		}

		public static byte[] ReadAssemblyResource(string asm_name) {
			Assembly curasm = Assembly.GetExecutingAssembly();

			string[] resnames = curasm.GetManifestResourceNames();

			foreach(string res in resnames) {
				if (res.ToLower().EndsWith(asm_name.ToLower())) {
					Stream res_stream = curasm.GetManifestResourceStream(res);
					if (res_stream != null) {
						byte[] alldata = new byte[res_stream.Length];
						res_stream.Read(alldata, 0, Convert.ToInt32(res_stream.Length));
						res_stream.Close();
						res_stream = null;
						return alldata;
					}
				}
			}

			return null;
		}

		private void frmMain_Load(object sender, System.EventArgs e) {
			//if (DateTime.Now.Ticks > (new DateTime(2005, 09, 01, 0, 0, 0, 0).Ticks)) {
			//	MessageBox.Show("Beta version expired.  Please visit http://torque.netmercs.net/ for a newer version.", "TSDev", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	Application.Exit();
			//	return;
			//}

			g.LogDebug("INIT: Pre-init complete. Main object load. Welcome to TorqueDev");

			// Check to see if the license exists
			/*g.LogDebug("INIT: Checking for license");
			if (!File.Exists(Application.StartupPath + "\\license.xml"))
				LicenseManager.WriteDefaultLicense(Application.StartupPath + "\\license.xml");

			// Load the license
			try {
				g.LogDebug("INIT: Loading license XML");
				g.License = new LicenseManager(Application.StartupPath + "\\license.xml");
				g.LogDebug("INIT: License XML loaded successfully");
			} catch (Exception exc) {
				g.LogDebug("INIT: License XML failed to load: " + exc.Message);
				MessageBox.Show("Unable to load license from license file: " + exc.Message, "License Load Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Application.Exit();
				return;
			}

			// Verify the license
			if (!g.License.LicenseValid) {
				g.LogDebug("INIT: License invalid; Default LicenseManager is now being loaded.");

				// Check to see if they've had this error before
				if ((Convert.ToBoolean(g.AppRegistry.GetValue("HasSeenLicenseError", false))) == false) {

					g.LogDebug("INIT: Displaying license warning and setting HasSeenLicenseError");

					MessageBox.Show("Your license file is invalid.  If you did not purchase a professional or managed license, or if you did not receive a donator's license, " +
						"(in other words, if you are using the standard version of TorqueDev), please delete the 'license.xml' file from the TorqueDev directory to re-initialize the " +
						"standard license.\n\nTorqueDev will now restore the standard license, and you will not see this error message again.  Please note that it has come to our " +
						"attention that TorqueDev shows this error message on all 64 bit systems, and licensing does not work correctly on those architectures.\n\n" +
						"We apologize for the inconvenience.  TorqueDev will now continue to load."
						, "License Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					// Write new registry value
					g.AppRegistry.SetValue("HasSeenLicenseError", true);
				}

				g.License = new LicenseManager();
				//return;
			}*/

			// Disabled licensing
			g.License = new LicenseManager();
			g.LogDebug("INIT: License passed signature verification!");

			// Check the license version
			/*if ((new Version(Application.ProductVersion)) >= g.License.LicenseVersionLimit) {
				g.LogDebug("INIT: License version limit reached");
				MessageBox.Show("Your license file is invalid.  It was made for a previous version of TorqueDev and is now incompatible.  Please contact technical support " +
					"for information about obtaining a new license key.  You can switch back into standard edition by deleting the 'license.xml' file in the TorqueDev directory.",
					"License Invalid", MessageBoxButtons.OK, MessageBoxIcon.Stop);

				Application.Exit();
				return;
			}

			// Check the license expiration
			if (g.License.LicenseExpires.Year > 2000 && (DateTime.Now > g.License.LicenseExpires)) {
				g.LogDebug("INIT: License expired");

				MessageBox.Show("Your license file has expired.",
					"License Expired", MessageBoxButtons.OK, MessageBoxIcon.Stop);

				Application.Exit();
				return;
			}*/

			// Set the branding based on the version
			if (g.License["PRO"] == true) {
				g.LogDebug("INIT: -- Professional Edition Activated");
				g.Branding = "netMercs TorqueDev Professional";
			} else if (g.License["MANAGED"] == true) {
				g.LogDebug("INIT: -- Managed Edition Activated");
				g.Branding = "netMercs TorqueDev Professional (Managed)";
			} else if (g.License["DONATOR"] == true) {
				g.LogDebug("INIT: -- Donator Edition Activated");
				g.Branding = "netMercs TorqueDev (Donator)";
			}

			// Disable the license manager if we're already licensed
			//if (g.License["FREE"] == false)									// Always disable the licensing functionality


				this.mnuAbout_License.Enabled = false;

			g.Main = this;

			g.LogDebug("INIT: Showing splash");
			frmLoad fLoad = new frmLoad();
			fLoad.Show();
			fLoad.BringToFront();
			fLoad.Focus();
			fLoad.Refresh();

			// Initialize proxy
			g.LogDebug("INIT: Loading proxy settings");
			LoadProxySettings();

			g.LogDebug("INIT: Loading configuration");
			if (File.Exists(CConfig.GlobalUserpath + "\\config.dat")) {
				g.Config = CConfig.LoadConfig(CConfig.GlobalUserpath + "\\config.dat");
				g.LogDebug("INIT: Previous config loaded successfully");
			} else {
				g.LogDebug("INIT: New config required");
				g.Config = new CConfig();
			}

			// Display the EULA
			/*if (!g.Config.bFirstrun) {
				frmEula fEula = new frmEula();
				DialogResult result = fEula.ShowDialog();

				if (result != DialogResult.OK) {
					Application.Exit();
					return;
				}
			}*/

			// Show the startup wizard
			if (!g.Config.bFirstrun) {
				g.LogDebug("INIT: Firstrun; showing startup wizard");
				frmStartupWizard fStartupWizard = new frmStartupWizard();
				fStartupWizard.ShowDialog();

				fStartupWizard.Dispose();
				fStartupWizard = null;
				g.LogDebug("INIT: Startup wizard complete");
			}

			// Verify activation
			/*
			if (!VerifyActivation()) {
				// Write config data back to the disk
				CConfig.SaveConfig(CConfig.GlobalUserpath + "\\config.dat", g.Config);

				// Exit the application
				Application.Exit();
				return;
			}*/
			
			// Check for updates
			/*if (g.Config.bCheckUpdates == true && DateTime.Now.Ticks >= g.Config.NextUpdate) {
				g.LogDebug("INIT: Running updater");
				if (File.Exists(Application.StartupPath + "\\TSDevUpdater.exe")) {
					// Set our next update time
					g.Config.NextUpdate = (DateTime.Now.Ticks + (new TimeSpan(0, 12, 0, 0)).Ticks);
					CConfig.SaveConfig(CConfig.GlobalUserpath + "\\config.dat", g.Config);

					// Run the updater
					System.Diagnostics.Process.Start(Application.StartupPath + "\\TSDevUpdater.exe");

					Application.Exit();
					return;
				}*/

				/*try {                   
					// Check for updates
					WebClient client = new WebClient();
					string[] result = System.Text.ASCIIEncoding.ASCII.GetString(client.DownloadData("http://www.torquedev.com/auto_update.txt")).Split('|');
					
					Version newver = new Version(result[0]);
					Version oldver = new Version(Application.ProductVersion);

					if (newver > oldver) {
						DialogResult update_result = MessageBox.Show("Update Available (v. " + newver.ToString() + ")\n\n" + result[1] + "\n\nWould you like to start the auto-update program?", "Codeweaver IDE", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

						if (update_result == DialogResult.Yes) {
							System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");
							Application.Exit();
							return;
						}
					}

					g.Config.NextUpdate = (DateTime.Now.Ticks + (new TimeSpan(1, 0, 0, 0, 0).Ticks));
				} catch {}*/
			/*}*/

			//g.LogDebug("INIT: Updater skipped. EN? " + g.Config.bCheckUpdates.ToString() + " ; NU: " + new DateTime(g.Config.NextUpdate).ToString());

			// Check the motd
			/*try {
				g.LogDebug("INIT: Checking MOTD");
				WebClient client = new WebClient();
				client.QueryString.Add("cliver", Application.ProductVersion);
					
				string motd = System.Text.ASCIIEncoding.ASCII.GetString(client.DownloadData("http://www.torquedev.com/_client/motd.php"));
				g.Config.LastMotd = motd;
				g.LogDebug("INIT: Motd loaded");
			} catch (Exception exc) {
				g.LogDebug("INIT: MOTD failed: " + exc.Message);
			}*/

			// Static MOTD.  You can change this to pull from whatever
			g.Config.LastMotd = "Thanks for using TorqueDev!<br /><br /><b>TorqueDev is now open source. <a href=\"extern://www.torquedev.com/\">Grab the code</a>.</b><br /><br /><br /><br /><br /><br /><br /><br /><br />";

			try {
				g.LogDebug("INIT: Attempting to load autocomplete.txt");
				ac.LoadAutoComplete(Application.StartupPath + "\\autocomplete.txt");
				//ac.LoadClassData(Application.StartupPath + "\\classinfo.txt");
			} catch (Exception exc) {
				g.LogDebug("INIT: Critfail on autocomplete: " + exc.Message);
				MessageBox.Show("Critical failure:\n\n" + exc.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Application.Exit();
				return;
			}

			g.LogDebug("INIT: Explorer");
			InitExplorer();

			// Load snippetter
			//LoadSnippetter();

			// Load color preferences
			g.LogDebug("INIT: Color prefs");
			InitColorPrefs();

            // Initialize Macros
			g.LogDebug("INIT: Macros");
            BuildMacroMenu();

			// Restore form state and position
			g.LogDebug("INIT: Restoring window state");
			this.Location = g.Config.main_location;
			this.Size = g.Config.main_size;
			this.WindowState = g.Config.main_state;

			g.LogDebug("INIT: Closing splash");
			fLoad.Hide();
			fLoad.Close();
			fLoad.Dispose();
			fLoad = null;

			// Load Plugins
			g.LogDebug("INIT: Loading plugins architecture");
			this.LoadPluginCtrl();
			this.LoadPlugins();

            // Parse the command-line parameters
			if (frmMain.stc_Args.Length != 0) {
				g.LogDebug("INIT: CLP is: " + frmMain.stc_Args[0]);

				if (frmMain.stc_Args[0] == "--debug-mode") {
					g.LogDebug("INIT: CLP is actually --debug-mode; ignoring");
				} else if (File.Exists(stc_Args[0]))  {
					if (stc_Args[0].ToLower().EndsWith(".tsdev"))
						this.OpenProject(stc_Args[0]);
					else if (stc_Args[0].ToLower().EndsWith(".cs") || 
						stc_Args[0].ToLower().EndsWith(".mis") || 
						stc_Args[0].ToLower().EndsWith(".t2d") ||
						stc_Args[0].ToLower().EndsWith(".gui") )
						this.OpenFile(new CProject.File(Path.GetFileName(stc_Args[0]),
							stc_Args[0], true), 0, true);
					else {
						g.LogDebug("INIT: Not a file that's handleable");

						MessageBox.Show("TorqueDev cannot handle the file \"" +
							stc_Args[0] + "\"", "Error", MessageBoxButtons.OK,
							MessageBoxIcon.Exclamation);
					}
				} else {
					g.LogDebug("INIT: File CLP wants is not available");
					MessageBox.Show("The file \"" + stc_Args[0] + "\" could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				
			} else {
				if (g.Config.bFirstrun == false) {
					g.LogDebug("INIT: Firstrun");
					SpawnBrowser("firstrun", false);
					g.Config.bFirstrun = true;
				} else {
					g.LogDebug("INIT: Welcome screen");
					this.SpawnBrowser("welcome", true);
				}
			}

			if (g.Config.DockbarData != null) {
				g.LogDebug("INIT: Loading dockbar prefs");
				this.dockMgr.LoadConfigFromArray(g.Config.DockbarData);
			}

			// Load menu preferences
			g.LogDebug("INIT: Loading menu shortcuts");
			LoadMenuPrefs(g.Config.menu_shortcuts);

			g.LogDebug("INIT: Done");
			this.Show();

		}

		public void InitColorPrefs() {
			// Initialize the color preferences
			g.LogDebug("InitColorPrefs: Setting render modes");
			this.tbEditBar.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.mnuMain.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.ctmProject.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.tscToolbars.TopToolStripPanel.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.sbMain.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.dockMgr.Style = ((g.Config.ColorPref == ColorPref.Professional) ? VisualStyle.Office2003 :
				VisualStyle.IDE2005);

			// Loop through all the web browsers
			foreach (PrimaryTab<UCBrowser> browser in g.Browsers) {
				browser.Control.SetRenderMode(g.Config.ColorPref);
			}

			// Loop through all the editor controls and set the context
			// menu
			foreach (PrimaryTab<UCEditor> editor in g.Editors) {
				editor.Control.ctxEditor.RenderMode = ((g.Config.ColorPref == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);
			}

			// Change the local tab control
			this.tabMain.Style = ((g.Config.ColorPref == ColorPref.Professional) ? VisualStyle.Office2003 :
				VisualStyle.IDE);

		}

		public void LoadProxySettings() {
			g.LogDebug("LoadProxySettings: Setting default proxy");
			System.Net.GlobalProxySelection.Select = System.Net.WebProxy.GetDefaultProxy();
		}

		private bool VerifyLocalActivation() {
			/* Deprecated */
			return true;
		}

		private void LoadSnippetter() {
			/* DEPRECATED */
			return;

			// Load snippetter data
			/*frmMain.stc_Snippets = CSnippetter.Load(CConfig.GlobalUserpath + "\\snippetter.xml");

			// Refresh tree
			this.SnippetterUC.RefreshTree();*/
		}

		private void SaveSnippetter() {
			// Save snippetter data
			//CSnippetter.Save(frmMain.stc_Snippets, CConfig.GlobalUserpath + "\\snippetter.xml");
		}

		private bool VerifyActivation() {
			/* Deprecated */
			return true;
		}

		private bool VerifyActivationEx(string email, string password) {
			/* Deprecated */
			return true;
		}

		public void LoadPlugins() {
			// Load the plugin DLLs
			if (!Directory.Exists(Application.StartupPath + "\\plugins")) {
				g.LogDebug("LoadPlugins: Nothing to load");
				return;
			}

			g.LogDebug("LoadPlugins: Getting list of files to load");
			string[] plugins = Directory.GetFiles(Application.StartupPath + "\\plugins", "tdp_*.dll");

			foreach (string dll in plugins) {
				try {
					g.LogDebug("LoadPlugins: Trying to reflect " + Path.GetFileName(dll));
					Type pluginType = null;

					System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dll);
					ICodeweaverPlugin plugin = (ICodeweaverPlugin)FindInterface(assembly, "ICodeweaverPlugin", ref pluginType);
					CWPlugin pluginWrapper = null;



					foreach (Attribute attr in pluginType.GetCustomAttributes(typeof(CodeweaverPluginAttribute), true)) {

						g.LogDebug("LoadPlugins: Searching for CodeweaverPluginAttribute");
						if (attr is CodeweaverPluginAttribute) {
							CodeweaverPluginAttribute pluginAttributes = (CodeweaverPluginAttribute)attr;

							pluginWrapper = new CWPlugin(
								pluginAttributes.PluginName, pluginAttributes.PluginVersion, pluginAttributes.PluginDescription,
								pluginAttributes.PluginAuthor, pluginAttributes.PluginCopyright, pluginAttributes.PluginValidWithCW,
								pluginAttributes.PluginGuid, plugin
							);

							g.LogDebug("LoadPlugins: Load plugin: " + pluginAttributes.PluginName + " v. " + pluginAttributes.PluginVersion +
								" written by " + pluginAttributes.PluginAuthor + " (GUID: " + pluginAttributes.PluginGuid + ")");
						}
						
					}

					if (pluginWrapper == null) {
						g.LogDebug("LoadPlugins: Attr. not found; skipping to next");
						MessageBox.Show("Plugin load failure: " + dll + "\n\nClass that derives from ICodeweaverPlugin must implement attribute CodeweaverPluginAttribute", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						continue;
					}

					// Add to our global class
					g.LogDebug("LoadPlugins: Load OK");
					g.Plugins.Add(pluginWrapper);

					// Call initialization function
					g.LogDebug("LoadPlugins: CWPluginInit()");
					pluginWrapper.Plugin.CWPluginInit(new Version(Application.ProductVersion), CWLicenseType.StandardLicense, g.IDEControl);

					// Free up some objects
					assembly = null;
					pluginWrapper = null;
					plugins = null;

				} catch (Exception exc) {
					g.LogDebug("LoadPlugins: Load failed: " + exc.Message + "; skipping to next");
					MessageBox.Show("Plugin load failure: " + dll + "\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					continue;
				}
			}

			g.LogDebug("LoadPlugins: Done");
		}

		private object FindInterface(System.Reflection.Assembly dll, string InterfaceName, ref Type typeOut) {
			foreach(Type t in dll.GetTypes()) {
				if (t.GetInterface(InterfaceName, true) != null) {
					try {
						typeOut = t;
						return dll.CreateInstance(t.FullName, true, System.Reflection.BindingFlags.Default, null, null, null, null);
					} catch { return null; }
				}
			}

			return null;
		}

		public void LoadPluginCtrl() {
			// Initializes the plugin control object.  This needs to happen before any of the plugins are loaded.
			g.LogDebug("LoadPluginCtrl: Binding delegates");
			#region Delegate Assignments
			g.IDEControl.__AddDirectory = new IDEControl.___AddDirectory(CPlugins.AddDirectory);
			g.IDEControl.__AddFile = new IDEControl.___AddFile(CPlugins.AddFile);
			g.IDEControl.__AddIndicatorIcon = new IDEControl.___AddIndicatorIcon(CPlugins.AddIndicatorIcon);
			g.IDEControl.__AddIndicatorSpan = new IDEControl.___AddIndicatorSpan(CPlugins.AddIndicatorSpan);
			g.IDEControl.__AddProjectTreeIconResource = new IDEControl.___AddProjectTreeIconResource(CPlugins.AddProjectTreeIconResource);
			g.IDEControl.__AddWindowIconResource = new IDEControl.___AddWindowIconResource(CPlugins.AddWindowIconResource);
			g.IDEControl.__AddWorkspaceTabIconResource = new IDEControl.___AddWorkspaceTabIconResource(CPlugins.AddWorkspaceTabIconResource);
			g.IDEControl.__CloseFile = new IDEControl.___CloseFile(CPlugins.CloseFile);
			g.IDEControl.__CloseProject = new IDEControl.___CloseProject(CPlugins.CloseProject);
			g.IDEControl.__ConfigGetValue = new IDEControl.___ConfigGetValue(CPlugins.ConfigGetValue);
			g.IDEControl.__ConfigSetValue = new IDEControl.___ConfigSetValue(CPlugins.ConfigSetValue);
			g.IDEControl.__CreateProject = new IDEControl.___CreateProject(CPlugins.CreateProject);
			g.IDEControl.__DebugAddBreakpoint = new IDEControl.___DebugAddBreakpoint(CPlugins.DebugAddBreakpoint);
			g.IDEControl.__DebugDelBreakpoint = new IDEControl.___DebugDelBreakpoint(CPlugins.DebugDelBreakpoint);
			g.IDEControl.__DebugExecute = new IDEControl.___DebugExecute(CPlugins.DebugExecute);
			g.IDEControl.__DebugGetBreakpoints = new IDEControl.___DebugGetBreakpoints(CPlugins.DebugGetBreakpoints);
			g.IDEControl.__DeregisterCustomConfigTab = new IDEControl.___DeregisterCustomConfigTab(CPlugins.DeregisterCustomConfigTab);
			g.IDEControl.__DeregisterCustomEditorContextMenu = new IDEControl.___DeregisterCustomEditorContextMenu(CPlugins.DeregisterCustomEditorContextMenu);
			g.IDEControl.__DeregisterCustomPluginMenu = new IDEControl.___DeregisterCustomPluginMenu(CPlugins.DeregisterCustomPluginMenu);
			g.IDEControl.__DeregisterCustomProjectMenu = new IDEControl.___DeregisterCustomProjectMenu(CPlugins.DergisterCustomProjectMenu);
			g.IDEControl.__DeregisterCustomTopMenu = new IDEControl.___DeregisterCustomTopMenu(CPlugins.DeregisterCustomTopMenu);
			g.IDEControl.__DeregisterEditorTrigger = new IDEControl.___DeregisterEditorTrigger(CPlugins.DeregisterEditorTrigger);
			g.IDEControl.__DeregisterEnvironmentTab = new IDEControl.___DeregisterEnvironmentTab(CPlugins.DeregisterEnvironmentTab);
			g.IDEControl.__DeregisterToolbar = new IDEControl.___DeregisterToolbar(CPlugins.DeregisterToolbar);
			g.IDEControl.__DeregisterWindow = new IDEControl.___DeregisterWindow(CPlugins.DeregisterWindow);
			g.IDEControl.__EditorGetCaret = new IDEControl.___EditorGetCaret(CPlugins.EditorGetCaret);
			g.IDEControl.__EditorGetSelected = new IDEControl.___EditorGetSelected(CPlugins.EditorGetSelected);
			g.IDEControl.__EditorGetText = new IDEControl.___EditorGetText(CPlugins.EditorGetText);
			g.IDEControl.__EditorInsertText = new IDEControl.___EditorInsertText(CPlugins.EditorInsertText);
			g.IDEControl.__EditorSetCaret = new IDEControl.___EditorSetCaret(CPlugins.EditorSetCaret);
			g.IDEControl.__EditorSetText = new IDEControl.___EditorSetText(CPlugins.EditorSetText);
			g.IDEControl.__GetActiveFile = new IDEControl.___GetActiveFile(CPlugins.GetActiveFile);
			g.IDEControl.__GetDirectories = new IDEControl.___GetDirectories(CPlugins.GetDirectories);
			g.IDEControl.__GetDirectory = new IDEControl.___GetDirectory(CPlugins.GetDirectory);
			g.IDEControl.__GetFile = new IDEControl.___GetFile(CPlugins.GetFile);
			g.IDEControl.__GetFiles = new IDEControl.___GetFiles(CPlugins.GetFiles);
			g.IDEControl.__IntellicodeDisplayInfopop = new IDEControl.___IntellicodeDisplayInfopop(CPlugins.IntellicodeDisplayInfopop);
			g.IDEControl.__IntellicodeDisplayMemberlist = new IDEControl.___IntellicodeDisplayMemberlist(CPlugins.IntellicodeDisplayMemberlist);
			g.IDEControl.__IntellicodeGetEngineObjects = new IDEControl.___IntellicodeGetEngineObjects(CPlugins.IntellicodeGetEngineObjects);
			g.IDEControl.__IntellicodeGetObjectsInFile = new IDEControl.___IntellicodeGetObjectsInFile(CPlugins.IntellicodeObjectsInFile);
			g.IDEControl.__IntellicodeGetObjectsInProject = new IDEControl.___IntellicodeGetObjectsInProject(CPlugins.IntellicodeObjectsInProject);
			g.IDEControl.__IntellicodeScanExternalFile = new IDEControl.___IntellicodeScanExternalFile(CPlugins.IntellicodeScanExternalFile);
			g.IDEControl.__IntellicodeScanFile = new IDEControl.___IntellicodeScanFile(CPlugins.IntellicodeScanFile);
			g.IDEControl.__IntellicodeScanProject = new IDEControl.___IntellicodeScanProject(CPlugins.IntellicodeScanProject);
			g.IDEControl.__IntellicodeScanString = new IDEControl.___IntellicodeScanString(CPlugins.IntellicodeScanString);
			g.IDEControl.__IsFileOpen = new IDEControl.___IsFileOpen(CPlugins.IsFileOpen);
			g.IDEControl.__IsProjectOpen = new IDEControl.___IsProjectOpen(CPlugins.IsProjectOpen);
			g.IDEControl.__OpenFile = new IDEControl.___OpenFile(CPlugins.OpenFile);
			g.IDEControl.__OpenProject = new IDEControl.___OpenProject(CPlugins.OpenProject);
			g.IDEControl.__RefreshProjectList = new IDEControl.___RefreshProjectList(CPlugins.RefreshProjectList);
			g.IDEControl.__RegisterCustomConfigTab = new IDEControl.___RegisterCustomConfigTab(CPlugins.RegisterCustomConfigTab);
			g.IDEControl.__RegisterCustomEditorContextMenu = new IDEControl.___RegisterCustomEditorContextMenu(CPlugins.RegisterCustomEditorContextMenu);
			g.IDEControl.__RegisterCustomPluginMenu = new IDEControl.___RegisterCustomPluginMenu(CPlugins.RegisterCustomPluginMenu);
			g.IDEControl.__RegisterCustomProjectMenu = new IDEControl.___RegisterCustomProjectMenu(CPlugins.RegisterCustomProjectMenu);
			g.IDEControl.__RegisterCustomTopMenu = new IDEControl.___RegisterCustomTopMenu(CPlugins.RegisterCustomTopMenu);
			g.IDEControl.__RegisterEditorTrigger = new IDEControl.___RegisterEditorTrigger(CPlugins.RegisterEditorTrigger);
			g.IDEControl.__RegisterEnvironmentTab = new IDEControl.___RegisterEnvironmentTab(CPlugins.RegisterEnvironmentTab);
			g.IDEControl.__RegisterForAutocompletionUpdates = new IDEControl.___RegisterForAutocompletionUpdates(CPlugins.RegisterForAutoCompletionUpdates);
			g.IDEControl.__RegisterToolbar = new IDEControl.___RegisterToolbar(CPlugins.RegisterToolbar);
			g.IDEControl.__RegisterWindow = new IDEControl.___RegisterWindow(CPlugins.RegisterWindow);
			g.IDEControl.__RemoveDirectory = new IDEControl.___RemoveDirectory(CPlugins.RemoveDirectory);
			g.IDEControl.__RemoveFile = new IDEControl.___RemoveFile(CPlugins.RemoveFile);
			g.IDEControl.__RemoveIndicator = new IDEControl.___RemoveIndicator(CPlugins.RemoveIndicator);
			g.IDEControl.__SaveFile = new IDEControl.___SaveFile(CPlugins.SaveFile);
			g.IDEControl.__SaveProject = new IDEControl.___SaveProject(CPlugins.SaveProject);
			g.IDEControl.__SetDirectory = new IDEControl.___SetDirectory(CPlugins.SetDirectory);
			g.IDEControl.__SetFile = new IDEControl.___SetFile(CPlugins.SetFile);
			g.IDEControl.__SetProject = new IDEControl.___SetProject(CPlugins.SetProject);
			g.IDEControl.__SpawnBrowser = new IDEControl.___SpawnBrowser(CPlugins.SpawnBrowser);
			g.IDEControl.__UpdateEnvironmentTab = new IDEControl.___UpdateEnvironmentTab(CPlugins.UpdateEnvironmentTab);
			g.IDEControl.__UpdateWindow = new IDEControl.___UpdateWindow(CPlugins.UpdateWindow);
			g.IDEControl.__FocusEnvironmentTab = new IDEControl.___FocusEnvironmentTab(CPlugins.FocusEnvironmentTab);
			g.IDEControl.__DebugWatchListGet = new IDEControl.___DebugWatchListGet(CPlugins.DebugWatchListGet);
			g.IDEControl.__DebugWatchListPush = new IDEControl.___DebugWatchListPush(CPlugins.DebugWatchListPush);
			g.IDEControl.__DebugWatchListSet = new IDEControl.___DebugWatchListSet(CPlugins.DebugWatchListSet);
			#endregion
		}

		public void LoadAutoComplete() {
			if (g.Project == null)
				return;

			g.LogDebug("LoadAutoComplete: Loading auto complete for " + g.Project.ProjectType.ToString());

			try {
				switch(g.Project.ProjectType) {
					case 0:
						ac.LoadClassData(Application.StartupPath + "\\class_tge.txt");
						break;
					case 1:
						ac.LoadClassData(Application.StartupPath + "\\class_tse.txt");
						break;
					case 2:
						ac.LoadClassData(Application.StartupPath + "\\class_t2d.txt");
						break;
				}

				g.LogDebug("LoadAutoComplete: Outputting load information");
				g.Project.DisseminateLoadInformation(ac);
			} catch (Exception exc) {
				g.LogDebug("LoadAutoComplete: Failed loading: " + exc.Message);
				MessageBox.Show("Error loading autocomplete for project:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		public void LoadMenuPrefs(Hashtable ht) {
			foreach (ToolStripItem item in this.mnuMain.Items) {
				g.LogDebug("LoadMenuPrefs: " + item.Text);
				LoadSubmenuPrefs(item, ht);
			}
		}

		private void LoadSubmenuPrefs(ToolStripItem item, Hashtable ht) {
			if (item is ToolStripSeparator)
				return;

			try {
				if (ht.ContainsKey(item.Name))
					(item as ToolStripMenuItem).ShortcutKeys = (Keys)ht[item.Name];
			} catch { }

			foreach(ToolStripItem subitem in (item as ToolStripMenuItem).DropDownItems)
				LoadSubmenuPrefs(subitem, ht);
		}

		private void splitter1_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e) {
		
		}

		public static void DepressAllButtons() {
			foreach(ToolBarButton button in stc_tbFileList.Buttons)
				button.Pushed = false;
		}

		private void mnuFile_AddNew_Click(object sender, System.EventArgs e) {
			CProject.Directory parentDirectory = null;

			if (this.tvProject.SelectedNode == null)
				parentDirectory = null;
			else if (tvProject.SelectedNode.Tag is CProject.Directory)
				parentDirectory = (CProject.Directory)tvProject.SelectedNode.Tag;
			else if (tvProject.SelectedNode.Tag is CProject.File)
				parentDirectory = (tvProject.SelectedNode.Tag as CProject.File).ParentDir;

			frmNewItem fNewItem = new frmNewItem(parentDirectory);
			fNewItem.ShowDialog();

			fNewItem.Dispose();
			fNewItem = null;

			

			// TODO_PLUGINS: onDocumentNew

		}

		public void InitProject() {
			if (g.Project == null)
				return;

			g.LogDebug("InitProject: Enter");

			// TODO_PLUGINS: Project list refresh

			// Sort the two arraylists
			g.LogDebug("InitProject: Sorting dirlist");
			g.Project.DirList.Sort();

			g.LogDebug("InitProject: Sorting filelist");
			g.Project.FileList.Sort();

			g.LogDebug("InitProject: Writing topnode");
			TreeNode topnode = new TreeNode(g.Project.ProjectName, 5, 5);

			g.LogDebug("InitProject: Adding directories");
			AddSubDirsToTree(null, topnode);

			g.LogDebug("InitProject: Adding files");
			foreach(CProject.File file in g.Project.FileList) {
                int image_index = 2;
				
				if (file.ParentDir == null) {
					TreeNode subnode = new TreeNode(file.SimpleName, image_index, image_index);
					subnode.Tag = file;
					topnode.Nodes.Add(subnode);
				} else {
					TreeNode node = FindNodeWithParent(file.ParentDir, topnode);
					if (node != null) {
						TreeNode subnode = new TreeNode(file.SimpleName, image_index, image_index);
						subnode.Tag = file;
						node.Nodes.Add(subnode);
					}
				}
			}
			
			topnode.Expand();

			tvProject.Nodes.Clear();
			tvProject.Nodes.Add(topnode);

			this.Text = g.Project.ProjectName + " - netMercs TorqueDev";
		}

		private void AddSubDirsToTree(CProject.Directory ParentDir, TreeNode parentnode) {
			foreach(CProject.Directory subdir in GetDirsWithParent(ParentDir)) {
				TreeNode curdir = new TreeNode(subdir.name, 0, 1);
				curdir.Tag = subdir;

				if (subdir.isExpanded)
					curdir.Expand();
				
				AddSubDirsToTree(subdir, curdir);
				parentnode.Nodes.Add(curdir);
			}
		}

		internal ArrayList GetDirsWithParent(CProject.Directory dir) {
			ArrayList al = new ArrayList();

			foreach(CProject.Directory dirs in g.Project.DirList) {
				if (dir == null && dirs.parent == null) {
					al.Add(dirs);
				} else if (dirs.parent != null && dirs.parent.Equals(dir)) {
					al.Add(dirs);
				}
			}

			return al;
		}
		

		private TreeNode FindNode(CProject.File file, TreeNode node) {
			if (node.Tag is CProject.File) {
				if (node.Tag.Equals(file))
					return node;
			}

			foreach(TreeNode subnode in node.Nodes) {
				TreeNode toret = FindNode(file, subnode);
				if (toret != null)
					return toret;
			}

			return null;
		}
		
		private TreeNode FindNodeWithParent(CProject.Directory dir, TreeNode node) {
			if (node.Tag is CProject.Directory) {
				if (node.Tag.Equals(dir))
					return node;
			}

			foreach(TreeNode subnode in node.Nodes) {
				TreeNode toret = FindNodeWithParent(dir, subnode);
				if (toret != null)
					return toret;
			}

			return null;
		}

		private void tvProject_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e) {
			if (e.Label == null)
				return;

			if (e.Node.Parent == null) {
				g.Project.ProjectName = e.Label;
			} else if (e.Node.Tag is CProject.Directory) {
				((CProject.Directory)e.Node.Tag).name = e.Label;
			} else if (e.Node.Tag is CProject.File) {
				((CProject.File)e.Node.Tag).SimpleName = e.Label;
			}

		}

		public static string ShowPrompt(string DialogTitle, string Description, string Prompt) {
			frmInput fInput = new frmInput(DialogTitle, Description, Prompt);
			fInput.ShowDialog();

			string input = fInput.Tag.ToString();
			fInput.Dispose();

			return input;
		}


		private void tvProject_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//InitProject();
		}

		private void mnuCreateFolder_Click(object sender, System.EventArgs e) {
			if (tvProject.SelectedNode == null)
				return;

			string dirname = ShowPrompt("Create Folder", "Please enter the name of the folder you wish to create.", "Name:");

			if (dirname == "")
				return;
			
			CProject.Directory parent;
			if (tvProject.SelectedNode.Tag is CProject.File)
				parent = ((CProject.File)tvProject.SelectedNode.Tag).ParentDir;
			else if (tvProject.SelectedNode.Tag is CProject.Directory)
				parent = ((CProject.Directory)tvProject.SelectedNode.Tag);
			else
				parent = null;

			CProject.Directory child = new CProject.Directory(dirname, parent);
			g.Project.DirList.Add(child);

			InitProject();
		}

		private void tvProject_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (e.Action == TreeViewAction.Collapse) {
				if (e.Node.Tag is CProject.Directory)
					((CProject.Directory)e.Node.Tag).isExpanded = false;
			}
		}

		private void tvProject_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (e.Action == TreeViewAction.Expand) {
				if (e.Node.Tag is CProject.Directory)
					((CProject.Directory)e.Node.Tag).isExpanded = true;
			}
		}

		private void tvProject_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e) {
			TreeNode draggedNode = (TreeNode)e.Item;

			//if (draggedNode.Tag is CProject.Directory) {
			//	DoDragDrop(e.Item, DragDropEffects.None);
			//	return;
			//}

			if (e.Button == MouseButtons.Left)
				DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void tvProject_DragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			e.Effect = e.AllowedEffect;
		}

		private void tvProject_DragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			Point targetPoint = tvProject.PointToClient(new Point(e.X, e.Y));

			tvProject.SelectedNode = tvProject.GetNodeAt(targetPoint);
		}

		private void tvProject_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			Point targetPoint = tvProject.PointToClient(new Point(e.X, e.Y));

			TreeNode targetNode = tvProject.GetNodeAt(targetPoint);
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

			if ((draggedNode != targetNode) && (!ContainsNode(draggedNode, targetNode))) {

				if (targetNode.Tag is CProject.Directory) {
					if (draggedNode.Tag is CProject.Directory)
						((CProject.Directory)draggedNode.Tag).parent = ((CProject.Directory)targetNode.Tag);
					else if (draggedNode.Tag is CProject.File)
						((CProject.File)draggedNode.Tag).ParentDir = ((CProject.Directory)targetNode.Tag);
				} else if (targetNode.Tag is CProject.File) {
					if (draggedNode.Tag is CProject.Directory)
						((CProject.Directory)draggedNode.Tag).parent = ((CProject.File)targetNode.Tag).ParentDir;
					else if (draggedNode.Tag is CProject.File)
						((CProject.File)draggedNode.Tag).ParentDir = ((CProject.File)targetNode.Tag).ParentDir;
				} else if (targetNode.Tag == null) {
					if (draggedNode.Tag is CProject.Directory)
						((CProject.Directory)draggedNode.Tag).parent = null;
					else if (draggedNode.Tag is CProject.File)
						((CProject.File)draggedNode.Tag).ParentDir = null;
				}

				InitProject();
			}
		}

		private bool ContainsNode(TreeNode node1, TreeNode node2) {
			// Check the parent node of the second node.
			if (node2.Parent == null) return false;
			if (node2.Parent.Equals(node1)) return true;

			// If the parent node is not null or equal to the first node, 
			// call the ContainsNode method recursively using the parent of 
			// the second node.
			return ContainsNode(node1, node2.Parent);
		}

		private void mnuProperties_Click(object sender, System.EventArgs e) {
			if (tvProject.SelectedNode.Parent == null) {
				frmProperties fProperties = new frmProperties(g.Project, null, 1);
				fProperties.Owner = this;
				fProperties.Show();
			} else if (tvProject.SelectedNode.Tag is CProject.File) {
				frmProperties fProperties = new frmProperties(null, (CProject.File)tvProject.SelectedNode.Tag, 0);
				fProperties.Owner = this;
				fProperties.Show();
			}
		}


		public void mnuFileRoot_Popup(object sender, System.EventArgs e) {
			if (g.Project == null) {
				//this.mnuFile_AddExist.Enabled = false;
				//this.mnuFile_AddNew.Enabled = false;
				this.mnuFile_SaveAll.Enabled = false;
				this.mnuFile_SaveProject.Enabled = false;
				this.mnuFile_SaveProjectAs.Enabled = false;
				this.mnuFile_CloseProject.Enabled = false;
			} else {
				//this.mnuFile_AddExist.Enabled = true;
				//this.mnuFile_AddNew.Enabled = true;
				this.mnuFile_SaveAll.Enabled = true;
				this.mnuFile_SaveProject.Enabled = true;
				this.mnuFile_SaveProjectAs.Enabled = true;
				this.mnuFile_CloseProject.Enabled = true;
			}
		}

		private void tbMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			switch (e.Button.Tag.ToString()) {
				case "new_project":
					this.mnuFile_NewProject_Click(sender, null);
					break;
				case "open_project":
					this.mnuFile_OpenProj_Click(sender, null);
					break;
				case "new_item":
					if (g.Project != null)
						this.mnuFile_AddNew_Click(sender, null);
					break;
				case "existing_item":
					if (g.Project != null)
						this.mnuFile_AddExist_Click(sender, null);
					break;
			}
		}

		public void mnuFile_NewProject_Click(object sender, System.EventArgs e) {
			if (!PerformCloseOperations())
				return;

			frmNewProject fNewProject = new frmNewProject();
			fNewProject.ShowDialog();
		}

		public void CreateProject (string ProjectName, string ProjectPath, short ProjectType) {
			CProject cp = new CProject();
			cp.ProjectName = ProjectName;
			cp.ProjectPath = ProjectPath;
			cp.ProjectType = ProjectType;

			g.Project = cp;

			// Load auto-complete
			LoadAutoComplete();

			InitProject();
			InitExplorer();
			InitPublicList();
			this.BuildMacroMenu();

            cp.StartThread();

			// Clear the dirty flag
			frmMain.stc_bIsProjectDirty = false;
		}

		public void InitPublicList() {
			this.tvExports.Nodes.Clear();
			this.tvExports.Nodes.Add(g.Project.PublicTreeListing);
		}

		private void SaveOpenFileList() {
			if (g.Project == null)
				return;

			g.Project.OpenFiles = new CProject.FileCollection();

			foreach (PrimaryTab<UCEditor> editor in g.Editors)
				g.Project.OpenFiles.Add(editor.Control.g_curFile);
		}

		public bool PerformCloseOperations() {
			g.LogDebug("PerformCloseOperations: Enter");

			if (g.Project != null) {
				// Save the currently open files in the project
				g.LogDebug("PerformCloseOperations: Saving open files");
				SaveOpenFileList();

				// Ask if they want to save the project
				g.LogDebug("PerformCloseOperations: Checking with user to save project");
				if (!SaveProject())
					return false;

				// Iterate through all the tabs and prompt for save if necessary
				g.LogDebug("PerformCloseOperations: Prompt and save on all dirty files");
				foreach (PrimaryTab<UCEditor> tab in g.Editors) {
					if (tab.Control.isDirty) {
						if (!tab.Control.PromptAndSave())
							return false;
					}
				}

				
				// Close all open tabs if they're editor tabs
				g.LogDebug("PerformCloseOperations: Closing open files");
				foreach (PrimaryTab<UCEditor> tab in g.Editors) {
					CloseTab<UCEditor>(tab);
					//g.Editors.Remove(tab);
				}

				// Close all open tabs if they're browser tabs
				g.LogDebug("PerformCloseOperations: Closing browser tabs");
				foreach (PrimaryTab<UCBrowser> tab in g.Browsers) {
					CloseTab<UCBrowser>(tab);
					//g.Browsers.Remove(tab);
				}

				g.LogDebug("PerformCloseOperations: Collections reinitializing");
				g.Editors = new PrimaryTabCollection<UCEditor>();
				g.Browsers = new PrimaryTabCollection<UCBrowser>();
			}
			
			// Close the project
			if (g.Project != null) {
				g.LogDebug("PerformCloseOperations: Stopping project thread");
				g.Project.StopThread();
			}

			g.Project = null;
			g.ProjectFN = "";

			this.tvProject.Nodes.Clear();
			this.Text = "netMercs TorqueDev";

			// PLUGINS
			g.LogDebug("PerformCloseOperations: Notifying plugins");
			foreach (CWPlugin plugin in g.Plugins) {
				try {
					g.LogDebug("PerformCloseOperations: PLUGIN " + plugin.Guid.ToString() + " :: CWProjectClose");
					plugin.Plugin.CWProjectClose();
				} catch (Exception exc) {
					g.LogDebug("PerformCloseOperations: Exception: PLUGIN " + plugin.Guid.ToString() + " - " + exc.Message);
					g.PluginException(exc, plugin);
				}
			}

			return true;
		}

		public void CloseTab<T>(PrimaryTab<T> tab) {

			g.LogDebug("CloseTab<" + typeof(T).ToString() + ">: Enter");

			if (typeof(T) == typeof(UCEditor)) {
				(tab.Control as UCEditor).Dispose();
			} else if (typeof(T) == typeof(UCBrowser)) {
				(tab.Control as UCBrowser).Dispose();
			}

			g.LogDebug("CloseTab<" + typeof(T).ToString() + ">: Page dispose");
			tab.page.Dispose();
			tabMain.LeafForPage(tab.page).TabPages.Remove(tab.page);

			
		}


		public bool SaveProject() {
			if (g.Project == null)
				return false;

			g.LogDebug("SaveProject: Enter");

			if (!frmMain.stc_bIsProjectDirty) {
				g.LogDebug("SaveProject: Not dirty; return");
				return true;
			}

			g.LogDebug("SaveProject: Prompt user to save");
			DialogResult proj_result = MessageBox.Show(this, "Would you like to save changes to this project?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

			if (proj_result == DialogResult.Yes) {
				string filename;

				if (g.ProjectFN == "") {
					filename = PromptSaveFilename("Save Project", "netMercs TSDev Projects (*.tsdev)|*.tsdev|All Files (*.*)|*.*");

					if (filename == "")
						return false;
				} else {
					filename = g.ProjectFN;
				}
				
				// Save the project
				g.LogDebug("SaveProject: Committing save");
				SaveProjectCommit(filename);

				return true;
			} else if (proj_result == DialogResult.Cancel) {
				g.LogDebug("SaveProject: Cancel");
				return false;
			} else if (proj_result == DialogResult.No) {
				g.LogDebug("SaveProject: No");
				return true;
			}

			return false;
		}

		public void SaveProjectCommit(string filename) {
			// Commit a save operation
			g.LogDebug("SaveProjectCommit: Enter");
            CProject.SaveProject(filename, g.Project);
		}

		public void mnuFile_OpenProj_Click(object sender, System.EventArgs e) {
			if (!PerformCloseOperations())
				return;

			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Open Project";
			open.CheckFileExists = true;
			open.CheckPathExists = true;
			open.Filter = "netMercs TSDev Projects (*.tsdev)|*.tsdev|All Files (*.*)|*.*";
			open.InitialDirectory = Application.StartupPath;
			
			DialogResult result = open.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			string filename = open.FileName;
			OpenProject(filename);
			
		}

		public void OpenProject(string filename) {
			g.LogDebug("OpenProject: Setting working directory");
			Directory.SetCurrentDirectory(Directory.GetParent(filename).FullName);

			CProject project = null;

			try {
				g.LogDebug("OpenProject: Trying to load project");
				project = CProject.LoadProject(filename);
			} catch (Exception exc) {
				g.LogDebug("OpenProject: Failed: " + exc.Message);
				MessageBox.Show(this, "Unable to open project: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (project == null) {
				g.LogDebug("OpenProject: Failed deserialization");
				MessageBox.Show(this, "Unable to open project.  Invalid format or file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Check project type first to make sure they're authorized
			if (project.ProjectType == 0 && g.Config.ActivationHasTGE == false) {
				g.LogDebug("OpenProject: Unlicensed TGE");
				MessageBox.Show(this, "Error: You are not licensed to open TGE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (project.ProjectType == 1 && g.Config.ActivationHasTSE == false) {
				g.LogDebug("OpenProject: Unlicensed TSE");
				MessageBox.Show(this, "Error: You are not licensed to open TSE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (project.ProjectType == 2 && g.Config.ActivationHasT2D == false) {
				g.LogDebug("OpenProject: Unlicensed T2D");
				MessageBox.Show(this, "Error: You are not licensed to open T2D projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			
			g.Project = project;
			g.ProjectFN = filename;

			// Put into recent files
			g.LogDebug("OpenProject: Inserting into recent files");
			if (!g.Config.recent_files.ContainsKey(filename))
				g.Config.recent_files.Add(filename, project.ProjectName);

			// Rescan the project's objects
			g.LogDebug("OpenProject: Rescan objects");
			project.RescanProjectObjects();

			// Load auto-complete
			g.LogDebug("OpenProject: Loading auto-complete");
			LoadAutoComplete();

			g.LogDebug("OpenProject: InitProject, InitExplorer, InitPublicList, BuildMacroMenu");
			this.InitProject();
			this.InitExplorer();
			this.InitPublicList();
			this.BuildMacroMenu();

			g.LogDebug("OpenProject: Starting BG thread");
			g.Project.StartThread();

			// PLUGINS
			g.LogDebug("OpenProject: Notifying plugins");
			foreach (CWPlugin plugin in g.Plugins) {
				bool cancel = false;
				try {
					g.LogDebug("OpenProject: PLUGIN " + plugin.Guid + " :: CWProjectLoad");
					plugin.Plugin.CWProjectLoad(project.ToCWProject(), ref cancel);
				} catch (Exception exc) {
					g.LogDebug("OpenProject: Exception: PLUGIN " + plugin.Guid + " - " + exc.Message);
					g.PluginException(exc, plugin);
				}
			}

			// Open save-state information
			g.LogDebug("OpenProject: Reopening last open files");
			foreach(CProject.File file in g.Project.OpenFiles) {
				Application.DoEvents();
				OpenFile(file, 1, false, false);
			}

            // Set the dirtyness to false
			g.LogDebug("OpenProject: Dirty = false");
			frmMain.stc_bIsProjectDirty = false;

			// Load the recent finds
			g.LogDebug("OpenProject: Loading recent searches");
			if (project.Finds.Count > 0) {
				cboRecentSearches.Items.Clear();

				foreach (string finds in project.Finds)
					cboRecentSearches.Items.Add(finds);

				cboRecentSearches.SelectedIndex = cboRecentSearches.Items.Count - 1;
			}
			
		}

		private void mnuFile_AddExist_Click(object sender, System.EventArgs e) {
			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Add Existing Item";
			open.CheckFileExists = true;
			open.CheckPathExists = true;
			open.Filter = "TorqueScript Files (*.cs)|*.cs|User Interface Scripts (*.gui)|*.gui|T2D/TGB Files (*.t2d)|*.t2d|netMercs TorqueScript Files (*.ns)|*.ns|All Files (*.*)|*.*";
			open.Multiselect = true;
			open.InitialDirectory = g.Project.ProjectPath;

			DialogResult result = open.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			string [] filenames = open.FileNames;

			if (filenames.Length == 0)
				return;

			CProject.Directory parent_dir = null;

			if (tvProject.SelectedNode == null || tvProject.SelectedNode.Tag == null)
				parent_dir = null;
			else if (tvProject.SelectedNode.Tag is CProject.File)
				parent_dir = ((CProject.File)tvProject.SelectedNode.Tag).ParentDir;
			else if (tvProject.SelectedNode.Tag is CProject.Directory)
				parent_dir = ((CProject.Directory)tvProject.SelectedNode.Tag);

			foreach(string filename in filenames)
				AddExistingFile(filename, parent_dir);
			

			// Reinit project
			this.InitProject();

			// Dirty the project
			frmMain.stc_bIsProjectDirty = true;

			// Reinit explorer
			//this.InitExplorer();
		}

		internal CProject.File AddExistingFile(string filename, CProject.Directory parent) {
			if (filename == "")
				return null;

			g.LogDebug("AddExistingFile: Enter");
			string relative_path = CProject.PathGetRelative(filename, g.Project.ProjectPath);

			g.LogDebug("AddExistingFile: Relative path of file is: '" + relative_path + "'");

			if (relative_path == "") {
				g.LogDebug("AddExistingFile: Resorting to full file name");
				relative_path = filename;		// Resort to the real filename
			}

			// Search existing file list for a file with this path:
			g.LogDebug("AddExistingFile: Searching for duplicate");
			foreach(CProject.File file in g.Project.FileList) {
				if (file.RelativePath == relative_path) {
					//MessageBox.Show(this, relative_path + " is already in your project and will not be added again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					g.LogDebug("AddExistingFile: File already in project; silent skip");
					return null;
				}
			}

			// Create a file object and insert it into the root path
			g.LogDebug("AddExistingFile: Creating file object and setting parent");
			CProject.File newfile = new CProject.File(Path.GetFileName(filename), relative_path, null);	
			newfile.ParentDir = parent;

			// Check to see what kind of file this is
			if (!(Path.GetExtension(filename) == ".cs" || Path.GetExtension(filename) == ".gui" ||
				Path.GetExtension(filename) == "mis")) {
				g.LogDebug("AddExistingFile: Text mode on");
				newfile.isText = true;
			}

			g.LogDebug("AddExistingFile: Adding to file collection");
			g.Project.FileList.Add(newfile);

			g.LogDebug("AddExistingFile: Enqueueing into tokenizer");
			CProject.TokenizerQueue tq = new CProject.TokenizerQueue();
			tq.file = newfile;
			tq.needFile = true;
			tq.code = "";
			
			if (!newfile.isText)
				g.Project._Queue(tq);

			g.LogDebug("AddExistingFile: Exit");
			return newfile;
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

		private string PromptOpenFilename(string filter) {
			OpenFileDialog open = new OpenFileDialog();
			open.Title = "Add Existing Item";
			open.CheckFileExists = true;
			open.CheckPathExists = true;
			open.Filter = filter;
			open.InitialDirectory = g.Project.ProjectPath;

			DialogResult result = open.ShowDialog();

			if (result == DialogResult.Cancel)
				return "";
			else
				return open.FileName;
		}

		private void mnuFile_CloseProject_Click(object sender, System.EventArgs e) {
			this.PerformCloseOperations();
		}

		public void mnuFile_SaveProject_Click(object sender, System.EventArgs e) {
			if (g.ProjectFN == "") {
				mnuFile_SaveProjectAs_Click(sender, e);
				return;
			}

			SaveOpenFileList();
			CProject.SaveProject(g.ProjectFN, g.Project);

			// Set the project dirty flag
			frmMain.stc_bIsProjectDirty = false;
		}

		private void mnuFile_SaveProjectAs_Click(object sender, System.EventArgs e) {
			string filename = PromptSaveFilename("Save Project", "netMercs TSDev Projects (*.tsdev)|*.tsdev|All Files (*.*)|*.*");

			if (filename == "")
				return;

			SaveOpenFileList();
			CProject.SaveProject(filename, g.Project);
			g.ProjectFN = filename;

			if (!g.Config.recent_files.ContainsKey(filename))
				g.Config.recent_files.Add(filename, g.Project.ProjectName);

			// Reset dirty flag
			frmMain.stc_bIsProjectDirty = false;
		}

		internal void OpenFile(CProject.File file, int jumpto, bool isAbsolute) {
			OpenFile(file, jumpto, isAbsolute, true);
		}

		internal void OpenFile(CProject.File file, int jumpto, bool isAbsolute, bool OpenFile) {
			// Change back to default project directory

			g.LogDebug("OpenFile: Enter - " + file.RelativePath + " ( " + jumpto.ToString() + " " + isAbsolute.ToString() + " " + OpenFile.ToString() + ")");

			if (g.Project == null && file.isForeign == true) {
				// Create a dummy project for this file
				g.LogDebug("OpenFile: No project open; creating dummy project");
				CreateProject("Project", Path.GetDirectoryName(file.RelativePath), 0);
			}

			g.LogDebug("OpenFile: Setting working directory to project root");
			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			g.LogDebug("OpenFile: Searching editors for file");
			foreach (PrimaryTab<UCEditor> ed in g.Editors) {
				if (ed.Control.g_curFile.Equals(file)) {
					ed.page.Selected = true;

					g.LogDebug("OpenFile: Jumping to position in open file");
					if (jumpto == -1)
						return;
					try {
						if (!isAbsolute) {
							ed.Control.txtEditor.SelectedView.Selection.SelectRange(ed.Control.txtEditor.Document.PositionToOffset(new ActiproSoftware.SyntaxEditor.Position(jumpto, 0)), 0);
						} else {
							ed.Control.txtEditor.SelectedView.Selection.SelectRange(jumpto, 0);
						}
					} catch {
						g.LogDebug("OpenFile: Position jump failed");
						MessageBox.Show("Could not navigate to requested line number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}

					return;
				}
			}

			g.LogDebug("OpenFile: File not already open; finding file in relative path");
			if (!File.Exists(file.RelativePath)) {
				g.LogDebug("OpenFile: Failed");
				MessageBox.Show(this, "File not found in relative path.  Please edit file properties to fix paths.  Remember to keep paths relative for easy migration.  For more information, please visit the website.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			g.LogDebug("OpenFile: Spawn editor");
			UCEditor edit = new UCEditor(file, true);
			edit.OpenFile();

			try {
				g.LogDebug("OpenFile: Signalling editor to refresh dropdown");
				edit.DropdownMake();
			} catch {}

			// Try-catch this in case the file changed from when we last saw it.
			try {
				g.LogDebug("OpenFile: Trying to navigate to position");
				if (!isAbsolute)
					edit.txtEditor.SelectedView.GoToLine(jumpto);
				else {
					ActiproSoftware.SyntaxEditor.Position posx = edit.txtEditor.Document.OffsetToPosition(jumpto);
					edit.txtEditor.SelectedView.GoToLine(posx.Line);
				}
			} catch {
				g.LogDebug("OpenFile: Navigation failed; resuming");
			}

			g.LogDebug("OpenFile: Creating editor tab");
			Crownwood.DotNetMagic.Controls.TabPage new_tab = new Crownwood.DotNetMagic.Controls.TabPage(file.SimpleName, edit, this.ilDocBar, 0);
			edit.Text = file.RelativePath;
			edit._ParentTab = new_tab;

			g.LogDebug("OpenFile: Adding editor to collection");
			g.Editors.Add(new PrimaryTab<UCEditor>(new_tab, edit));
			this.tabMain.ActiveLeaf.TabPages.Add(new_tab);

			Application.DoEvents();

			new_tab.ToolTip = file.RelativePath;

			if (OpenFile) {
				g.LogDebug("OpenFile: Selecting tab");
				new_tab.Selected = true;
				new_tab.Select();
			}

			// Dirty the project
			g.LogDebug("OpenFile: Marking project as dirty");
			frmMain.stc_bIsProjectDirty = true;

			// PLUGINS
			g.LogDebug("OpenFile: Informing plugins");
			foreach (CWPlugin plugin in g.Plugins) {
				try {
					g.LogDebug("OpenFile: PLUGIN : " + plugin.Guid.ToString() + " :: CWFileAfterLoad");
					plugin.Plugin.CWFileAfterLoad(file.ToCWFile());
				} catch (Exception exc) {
					g.LogDebug("OpenFile: Exception: PLUGIN : " + plugin.Guid.ToString() + " - " + exc.Message);
					g.PluginException(exc, plugin);
				}
			}
			
		}

		public void SpawnBrowser(string URL, bool hideall) {
			g.LogDebug("SpawnBrowser: Spawning browser to URL: " + URL + " (HideUI? " + hideall.ToString());
			UCBrowser browser = new UCBrowser(URL, hideall);
			Crownwood.DotNetMagic.Controls.TabPage new_tab = new Crownwood.DotNetMagic.Controls.TabPage(URL, browser, this.ilDocBar, 2);

			browser._parent_tab = new_tab;
			this.tabMain.ActiveLeaf.TabPages.Add(new_tab);
			new_tab.Selected = true;

			//this._al_browsers.Add(new_tab);
			g.Browsers.Add(new PrimaryTab<UCBrowser>(new_tab, browser));
		}

		private void mnuFileRoot_Click(object sender, System.EventArgs e) {
			mnuFileRoot_Popup(sender, e);
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			g.LogDebug("MainFormClosing: Enter");

			if (this.WindowState == FormWindowState.Minimized) {
				g.LogDebug("MainFormClosing: Restoring normal windowstate");
				this.WindowState = FormWindowState.Normal;
			}

			if (g.IsDebugging) {
				g.LogDebug("MainFormClosing: Exitting debugging");
				SwitchDebug(false);
			}

			g.LogDebug("MainFormClosing: Saving dockbar positions");
			g.Config.DockbarData = this.dockMgr.SaveConfigToArray();

			// Save position, size, and state
			g.LogDebug("MainFormClosing: Saving window state");
			g.Config.main_location = this.Location;
			g.Config.main_size = this.Size;
			g.Config.main_state = this.WindowState;

			// Save configuration
			g.LogDebug("MainFormClosing: Saving configuration");
			CConfig.SaveConfig(CConfig.GlobalUserpath + "\\config.dat", g.Config);

			g.LogDebug("MainFormClosing: Performing closing operations");
			if (!this.PerformCloseOperations()) {
				e.Cancel = true;
				return;
			}

			// PLUGINS
			bool cancelQuitViaPlugin = false;
			g.LogDebug("MainFormClosing: Notifying plugins of quit");
			foreach (CWPlugin plugin in g.Plugins) {
				try {
					g.LogDebug("MainFormClosing: PLUGIN : " + plugin.Guid.ToString() + " :: CWIDEClosing");
					plugin.Plugin.CWIDEClosing(ref cancelQuitViaPlugin);
				} catch (Exception exc) {
					g.LogDebug("MainFormClosing: Exception: PLUGIN : " + plugin.Guid.ToString() + " - " + exc.Message);
					g.PluginException(exc, plugin);
				}
			}

			if (cancelQuitViaPlugin) {
				g.LogDebug("MainFormClosing: Plugins cancelled quit");
				e.Cancel = true;
				return;
			}

			g.LogDebug("MainFormClosing: Hiding main form");
			this.Hide();

			g.LogDebug("MainFormClosing: ApplicationExit");
			Application.Exit();

			g.LogDebug("MainFormClosing: Garbage Collect");
			GC.Collect();

			//System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		private void mnuProject_AddNew_Click(object sender, System.EventArgs e) {
			this.mnuFile_AddNew_Click(sender, e);
		}

		private void mnuProject_AddExist_Click(object sender, System.EventArgs e) {
			this.mnuFile_AddExist_Click(sender, e);
		}

		private void mnuFile_Exit_Click(object sender, System.EventArgs e) {
			//Application.Exit();
			this.Close();
		}

		private void mnuHelp_About_Click(object sender, System.EventArgs e) {
			frmAbout fAbout = new frmAbout();
			fAbout.ShowDialog(this);

			fAbout = null;
		}

		private void tvProject_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
		
		}

		private void tvExplorer_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			
		}

		public void InitExplorer() {
			
			g.LogDebug("InitExplorer: Enter");
			
			if (g.Project == null)
				return;

			try {
				g.LogDebug("InitExplorer: Invoking __ClearNodes");
				tvExplorer.Invoke(this.__ClearNodes);

				//tvExplorer.Invoke(this.__AddNode, new object[1] { g.Project.PublicTreeListing });

				g.LogDebug("InitExplorer: Invoking __AddNode");
				tvExplorer.Invoke(this.__AddNode, new object[1] { g.Project.PrivateTreeListing });
				tvExplorer.SelectedNode = tvExplorer.Nodes[0];
			} catch (Exception exc) {
				g.LogDebug("InitExplorer: Failed: " + exc.Message);
			}
		}

		private void tvExplorer_MouseHover(object sender, System.EventArgs e) {
			
		}

		private void tvExplorer_MouseEnter(object sender, System.EventArgs e) {
			
		}

		private void tvExplorer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {

		}

		private void tvExplorer_MouseLeave(object sender, System.EventArgs e) {

		}

		private void tvExplorer_DoubleClick(object sender, System.EventArgs e) {
			if (tvExplorer.SelectedNode.Tag == null)
				return;

			if (tvExplorer.SelectedNode.Tag is ArrayList) {
				ArrayList al = (ArrayList)tvExplorer.SelectedNode.Tag;

				if (al.Count != 2)
					return;

				OpenFile(((CProject.File)al[1]), ((CProject.TokenKey)al[0]).LineNumber, true);
			} else if (tvExplorer.SelectedNode.Tag is CProject.TokenObject) {
				// This is a token object
				CProject.TokenObject tokobj = (CProject.TokenObject)tvExplorer.SelectedNode.Tag;
				if (tokobj.ObjectFileDecl != null)
					OpenFile(tokobj.ObjectFileDecl, tokobj.ObjectDeclOffset, true);
				else
					MessageBox.Show(this, "This object is a virtual member and has no programmatic declaration.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else if (tvExplorer.SelectedNode.Tag is CProject.TokenObject.ObjectDescr) {
				// This is an object function
				CProject.TokenObject.ObjectDescr objdescr = (CProject.TokenObject.ObjectDescr)tvExplorer.SelectedNode.Tag;
				if (objdescr.FuncFile != null)
					OpenFile(objdescr.FuncFile, objdescr.FuncOffset, true);
			}
		}

		private DialogResult ShowDeletePrompt(string text, string option1, string option2) {
			frmDelPrompt fDelPrompt = new frmDelPrompt(text, option1, option2);
			DialogResult result = fDelPrompt.ShowDialog(this);

			fDelPrompt.Dispose();
			fDelPrompt = null;

			return result;
		}

		internal void CloseFile(CProject.File file, bool promptsave, bool forcesave) {
			g.LogDebug("CloseFile: Enter - " + file.RelativePath + " (PS " + promptsave.ToString() + "  FS " + forcesave.ToString());

			g.LogDebug("CloseFile: Searching editors for file");
			foreach (PrimaryTab<UCEditor> edit in g.Editors) {
				if (edit.Control.g_curFile == file) {
					g.LogDebug("CloseFile: Prompting for save");
					if (promptsave) {
						if (!edit.Control.PromptAndSave())
							return;
					} else if (forcesave) {
						if (!edit.Control.SaveFile())
							return;
					}

					// PLUGINS
					g.LogDebug("CloseFile: Notifying plugins of close");
					foreach (CWPlugin plugin in g.Plugins) {
						try {
							g.LogDebug("CloseFile: PLUGIN : " + plugin.Guid.ToString() + " :: CWFileAfterClose");
							plugin.Plugin.CWFileAfterClose((edit.page.Control as UCEditor).g_curFile.ToCWFile());
						} catch (Exception exc) {
							g.LogDebug("CloseFile: Exception: PLUGIN : " + plugin.Guid.ToString() + " - " + exc.Message);
							g.PluginException(exc, plugin);
						}
					}

					g.LogDebug("CloseFile: Closing tab and removing from collection");

					CloseTab<UCEditor>(edit);
					g.Editors.Remove(edit);
					return;
				}
			}

			g.LogDebug("CloseFile: Not found; exit");
		}

		private void mnuProject_Del_Click(object sender, System.EventArgs e) {
			if (tvProject.SelectedNode == null)
				return;

			if (tvProject.SelectedNode.Tag == null)
				return;

			DialogResult result = DialogResult.Cancel;
			if (tvProject.SelectedNode.Tag is CProject.File)
				result = ShowDeletePrompt("This will remove the reference to the file '" + (tvProject.SelectedNode.Tag as CProject.File).RelativePath + "'", "Remove file from project and delete from disk", "Only remove file from project");
			else
				result = ShowDeletePrompt("This will remove the references to all the files in folder '" + (tvProject.SelectedNode.Tag as CProject.Directory).name + "' and all subfolders", "Remove files from project and delete from disk", "Only remove files from project");

			bool delfiles = false;
			if (result == DialogResult.Yes)
				delfiles = true;
			else if (result == DialogResult.No)
				delfiles = false;
			else
				return;

			if (tvProject.SelectedNode.Tag is CProject.File) {
				DeleteFile((CProject.File)tvProject.SelectedNode.Tag, delfiles);
			} else if (tvProject.SelectedNode.Tag is CProject.Directory) {
				DeleteDirectory(((CProject.Directory)tvProject.SelectedNode.Tag), delfiles);
			}

			InitProject();
			InitExplorer();
		}

		internal void DeleteFile(CProject.File file, bool delfromdrive) {
			g.LogDebug("DeleteFile: Enter (File: " + file.RelativePath + "; DelFromDrive: " + delfromdrive.ToString() + ")");

			g.LogDebug("DeleteFile: Closing file");
			CloseFile(file, false, false);

			g.LogDebug("DeleteFile: Removing objects from project");
			g.Project.RemoveObjectsInFile(file);

			g.LogDebug("DeleteFile: Removing from file list");
			g.Project.FileList.Remove(file);

			g.LogDebug("DeleteFile: Marking project dirty");
			frmMain.stc_bIsProjectDirty = true;

			if (delfromdrive) {
				try {
					g.LogDebug("DeleteFile: Deleting file from FS");
					File.Delete(Path.GetFullPath(file.RelativePath));
				} catch (Exception exc) {
					g.LogDebug("DeleteFile: Failed - " + exc.Message);
					MessageBox.Show(this, "There was an error deleting the file from the filesystem:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		internal void DeleteDirectory(CProject.Directory dir, bool delfiles) {
			g.LogDebug("DeleteDirectory: Enter (Dir: " + dir.name + "; RecDelFiles: " + delfiles.ToString() + ")");

			g.LogDebug("DeleteDirectory: Cloning file and directory objects");
			CProject.FileCollection al_files = (CProject.FileCollection)g.Project.FileList.Clone();
			CProject.DirectoryCollection al_dirs = (CProject.DirectoryCollection)g.Project.DirList.Clone();

			g.LogDebug("DeleteDirectory: Marking project dirty");
			frmMain.stc_bIsProjectDirty = true;

			g.LogDebug("DeleteDirectory: Enumerating files recursively");
            foreach(CProject.File file in al_files) {
				if (file.ParentDir == dir) {
					CloseFile(file, false, false);

					g.Project.RemoveObjectsInFile(file);
					g.Project.FileList.Remove(file);

					if (delfiles) {
						try {
							File.Delete(Path.GetFullPath(file.RelativePath));
						} catch (Exception exc) {
							MessageBox.Show(this, "There was an error deleting the file from the filesystem:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
				}
			}

			g.LogDebug("DeleteDirectory: Enumerating directories recursively");
			foreach(CProject.Directory enum_dir in al_dirs) {
				if (enum_dir.parent == dir)
					DeleteDirectory(enum_dir, delfiles);
			}

			g.LogDebug("DeleteDirectory: Removing from directory list");
			g.Project.DirList.Remove(dir);
		}

		private void mnuFile_SaveAll_Click(object sender, System.EventArgs e) {
			foreach (PrimaryTab<UCEditor> page in g.Editors)
				page.Control.SaveFile();

			/*foreach(Crownwood.DotNetMagic.Controls.TabPage page in this._al_Documents) {
				if (page.Control is UCEditor)
					(page.Control as UCEditor).SaveFile();
			}*/

		}

		private void mnuFile_ShowWelcome_Click(object sender, System.EventArgs e) {
			//if (!this.PerformCloseOperations())
			//	return;

			SpawnBrowser("welcome", true);

			//frmStartup fStartup = new frmStartup();
			//fStartup.ShowDialog();
		}

		private void tvExplorer_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (e.Node.Tag is CAutoComplete.ACEntry) {
				this.lblExpDescr.Text = "<b>Console Exported Function</b><br/>"
					+ ((CAutoComplete.ACEntry)e.Node.Tag).UnformattedDescription[0].ToString();
			} else if (e.Node.Tag is ArrayList) {
				CProject.TokenKey token = (CProject.TokenKey)((ArrayList)e.Node.Tag)[0];
				CProject.File file = (CProject.File)((ArrayList)e.Node.Tag)[1];
				this.lblExpDescr.Text = "<b>User-Defined Function</b><br/>"
					+ token.FuncDescr + "<br/><br/><span style=\"font-size:7pt\">Defined in " 
					+ file.RelativePath + " on character " + token.LineNumber.ToString() + "</span>";
                
			} else if (e.Node.Tag is CProject.TokenObject) {
				CProject.TokenObject tokobj = (CProject.TokenObject)e.Node.Tag;
				this.lblExpDescr.Text = "<b>User-Defined Object</b><br/>"
					+ tokobj.ObjectType + " " + tokobj.ObjectName + "<br/><br/>"
					+ "<span style=\"font-size:7pt\">Defined in " + tokobj.ObjectFileDecl.RelativePath 
					+ " on character " + tokobj.ObjectDeclOffset.ToString()
					+ "</span>";
			} else if (e.Node.Tag is CProject.TokenObject.ObjectDescr) {
				CProject.TokenObject.ObjectDescr objdescr = (CProject.TokenObject.ObjectDescr)e.Node.Tag;
				this.lblExpDescr.Text = "<b>User-Defined Object Function</b><br/>"
					+ objdescr.FuncDescr + "<br/><br/><span style=\"font-size:7pt\">Defined in "
					+ objdescr.FuncFile.RelativePath + " on character "
					+ objdescr.FuncOffset.ToString() + "</span>";
			}
		}

		//private static void g.Project_onExplorerModified() {
		//	g.Main.InitExplorer();
		//}

		private void tvExports_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (e.Node.Tag is CAutoComplete.ACEntry) {
				this.lblExportDescr.Text = "<b>Console-Exported Function</b><br/>"
					+ ((CAutoComplete.ACEntry)e.Node.Tag).UnformattedDescription[0].ToString();
			} else if (e.Node.Tag is CAutoComplete.ClassEntry) {
				this.lblExportDescr.Text = "<b>Console-Exported Class</b><br/>" +
					(((e.Node.Tag as CAutoComplete.ClassEntry).ClassInheritsFrom == "") ? "" : "Inherits <u>" + (e.Node.Tag as CAutoComplete.ClassEntry).ClassInheritsFrom + "</u>");
			}
		}

		private void tabMain_PageCloseRequest(Crownwood.DotNetMagic.Controls.TabbedGroups tg, Crownwood.DotNetMagic.Controls.TGCloseRequestEventArgs e) {
			g.LogDebug("PageCloseRequest: Enter");

			if (e.TabPage.Control is UCEditor) {
				g.LogDebug("PageCloseRequest: Tab is editor; prompt for save");
				if (!(e.TabPage.Control as UCEditor).PromptAndSave()) {
					g.LogDebug("PageCloseRequest: Cancel");
					e.Cancel = true;
				} else {
					g.LogDebug("PageCloseRequest: Shutting down editor specifics");
					(e.TabPage.Control as UCEditor).tmrPosNotify.Enabled = false;
					(e.TabPage.Control as UCEditor).txtEditor.Document.Text = "";
					(e.TabPage.Control as UCEditor).txtEditor.Document.Reparse();

					g.LogDebug("PageCloseRequest: Disposing objects");
					(e.TabPage.Control as UCEditor).txtEditor.Dispose();
					(e.TabPage.Control as UCEditor).Dispose();

					g.LogDebug("PageCloseRequest: Removing from collection");
					g.Editors.Remove(g.Editors.FindByTab(e.TabPage));

				}
			} else if (e.TabPage.Control is UCBrowser) {
				g.LogDebug("PageCloseRequest: Disposing browser");
				(e.TabPage.Control as UCBrowser).Dispose();

				g.LogDebug("PageCloseRequest: Removing browser from tab listing");
				g.Browsers.Remove(g.Browsers.FindByTab(e.TabPage));
			} else {
				// PLUGINS
				// User-defined page?  Find the plugin it belongs to
				g.LogDebug("PageCloseRequest: Plugin tab; searching for owner");

				PrimaryTab<Control> customtab = g.OtherTabs.FindByControl(e.TabPage.Control);
				if (customtab == null) {
					g.LogDebug("PageCloseRequest: Failed");
					return;
				}

				bool cancel = false;
				g.LogDebug("PageCloseRequest: Tab belongs to " + customtab.plugin.CWPluginGuid);
				g.LogDebug("PageCloseRequest: Calling CWEnvironmentTabClosing");

				try {
					customtab.plugin.CWEnvironmentTabClosing(customtab.Control, ref cancel);
				} catch (Exception exc) {
					g.LogDebug("PageCloseRequest: Exception: " + exc.Message);
					g.PluginException(exc, new CWPlugin("", new Version("1.0"), "", "", "", new Version("1.0"), customtab.plugin.CWPluginGuid, null));
				}
				
				g.LogDebug("PageCloseRequest: Cancel? " + cancel.ToString());
				e.Cancel = cancel;
				
				if (!e.Cancel) {
					g.LogDebug("PageCloseRequest: Removing tab");
					g.OtherTabs.Remove(customtab);
				}
			}
		}

		private void tabMain_PageChanged(Crownwood.DotNetMagic.Controls.TabbedGroups tg, Crownwood.DotNetMagic.Controls.TabPage tp) {
			g.LogDebug("PageChanged: Enter");

			if (tp == null || tp.Control == null) {
				this._PrevPage = null;
				return;
			}

			if ((this._PrevPage != null) && (tp != this._PrevPage)) {
				bool cancel = false;

				if (tp.Control is UCEditor) {
					g.LogDebug("PageChanged: Selecting file in project tree");
					CProject.File file = (tp.Control as UCEditor).g_curFile;
					// Select the specified file from the treeview
					TreeNode node = FindNode(file, this.tvProject.Nodes[0]);

					if (node != null) {
						this.tvProject.SelectedNode = node;
					}
				}
			}

			if (tp.Control is UCEditor) {
				try {
					this.mnuMacro_BeginRec.Enabled = ((tp.Control as UCEditor).txtEditor.MacroRecording.State == MacroRecordingState.Stopped);
					this.mnuMacro_Dir.Enabled = ((tp.Control as UCEditor).txtEditor.MacroRecording.State == MacroRecordingState.Stopped);
					this.mnuMacro_EndRec.Enabled = !((tp.Control as UCEditor).txtEditor.MacroRecording.State == MacroRecordingState.Stopped);
				} catch {}
			}

			if (g.OtherTabs.FindByControl(tp.Control) != null) {
				// This is a plugin tab... notify the plugin that they've been
				// switched to
				g.LogDebug("PageChanged: Notifying plugin");
				PrimaryTab<Control> plugintab = g.OtherTabs.FindByControl(tp.Control);

				g.LogDebug("PageChanged: Plugin is " + plugintab.plugin.CWPluginGuid + " :: Exec CWEnvironmentTabSwitchedTo");

				try {
					plugintab.plugin.CWEnvironmentTabSwitchedTo(plugintab.Control);
				} catch (Exception exc) {
					g.LogDebug("PageChanged: Exception: " + exc.Message);
					g.PluginException(exc, new CWPlugin("", new Version("1.0"), "", "", "", new Version("1.0"), plugintab.plugin.CWPluginGuid, null));
				}
			}

			g.LogDebug("PageChanged: Sorting tabs");
			g.MakeSortedTabActive(tp);
			this._PrevPage = tp;
				
		}

		private void mnuFile_SaveFile_Click(object sender, System.EventArgs e) {
			(this.tabMain.ActiveTabPage.Control as UCEditor).SaveFile();
		}

		private void mnuFile_SaveAs_Click(object sender, System.EventArgs e) {
			CProject.File file = (this.tabMain.ActiveTabPage.Control as UCEditor).PromptForSave();

			if (file != null)
				(this.tabMain.ActiveTabPage.Control as UCEditor).SaveFile();
		}

		private void mnuFile_CloseFile_Click(object sender, System.EventArgs e) {
			//this._al_Documents.Remove(this.tabMain.ActiveTabPage);
			//this.tabMain.ActiveLeaf.TabPages.Remove(this.tabMain.ActiveTabPage);

			if (this.tabMain.ActiveTabPage.Control is UCEditor) {
				PrimaryTab<UCEditor> ed = g.Editors.FindByTab(this.tabMain.ActiveTabPage);

				if (ed == null)
					return;

				CloseFile(ed.Control.g_curFile, true, false);
			} else if (this.tabMain.ActiveTabPage.Control is UCBrowser) {
				CloseTab<UCBrowser>(g.Browsers.FindByTab(this.tabMain.ActiveTabPage));
			} else {
				CloseTab<Control>(g.OtherTabs.FindByTab(this.tabMain.ActiveTabPage));
			}
		}

		private void mnuFile_CloseAll_Click(object sender, System.EventArgs e) {
			foreach (PrimaryTab<UCEditor> ed in g.Editors) {
				CloseTab<UCEditor>(ed);
				//g.Editors.Remove(ed);
			}

			foreach (PrimaryTab<UCBrowser> browse in g.Browsers) {
				CloseTab<UCBrowser>(browse);
				//g.Browsers.Remove(browse);
			}

			g.Editors = new PrimaryTabCollection<UCEditor>();
			g.Browsers = new PrimaryTabCollection<UCBrowser>();
		}

		private void mnuFile_ExportRTF_Click(object sender, System.EventArgs e) {
			string filename = PromptSaveFilename("Export to Rtf", "RichText Documents (*.rtf)|*.rtf|All Files (*.*)|*.*");

			if (filename == "")
				return;

			(this.tabMain.ActiveTabPage.Control as UCEditor).txtEditor.Document.SaveFileAsRtf(filename, new Font("Courier New", 12f), LineEndStyle.CarriageReturnNewline);
		}

		private void mnuFile_ExportHTML_Click(object sender, System.EventArgs e) {
			string filename = PromptSaveFilename("Export to HTML", "Hypertext Markup Documents (*.html)|*.html|All Files (*.*)|*.*");

			if (filename == "")
				return;

			(this.tabMain.ActiveTabPage.Control as UCEditor).txtEditor.Document.SaveFileAsHtml(filename, HtmlExportType.ClassBased, LineEndStyle.CarriageReturnNewline);
		}

		internal UCEditor GetActiveEditor() {
			//if (this.tabMain.ActiveControl == null)
			//	return null;
			if (this.tabMain.ActiveTabPage == null || this.tabMain.ActiveTabPage.Control == null)
				return null;

			if (this.tabMain.ActiveTabPage.Control is UCEditor)
				return (UCEditor)(this.tabMain.ActiveTabPage.Control);
			else
				return null;
		}

		private void mnuEdit_Undo_Click(object sender, System.EventArgs e) {
			GetActiveEditor().txtEditor.Document.UndoRedo.Undo();
		}

		private void mnuEdit_Redo_Click(object sender, System.EventArgs e) {
			GetActiveEditor().txtEditor.Document.UndoRedo.Redo();
		}

		private void mnuEdit_Cut_Click(object sender, System.EventArgs e) {
			GetActiveEditor().txtEditor.SelectedView.CutToClipboard();
		}

		private void mnuEdit_Copy_Click(object sender, System.EventArgs e) {
			GetActiveEditor().txtEditor.SelectedView.CopyToClipboard();
		}

		private void mnuEdit_Paste_Click(object sender, System.EventArgs e) {
			GetActiveEditor().txtEditor.SelectedView.PasteFromClipboard();
			GC.Collect();
		}

		private void mnuEdit_SearchReplace_Click(object sender, System.EventArgs e) {
			if (GetActiveEditor() == null)
				return;

			g.LogDebug("SearchReplace: Enter");

			g.LogDebug("SearchReplace: Getting selection");
			if (GetActiveEditor().txtEditor.SelectedView.Selection.Length > 0) {
				// They've selected some text; put that as the default find stuff
				int length = GetActiveEditor().txtEditor.SelectedView.Selection.EndOffset - GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset;

				string seltext = "";
				if (length < 0)
					seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.EndOffset, Math.Abs(length));
				else
					seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset, length);

				//string seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset, GetActiveEditor().txtEditor.SelectedView.Selection.Length);
				g.LogDebug("SearchReplace: Spawning dialog with selection");
				new frmFindReplace(GetActiveEditor(), GetActiveEditor().txtEditor, new FindReplaceOptions(), seltext).Show();
			} else {
				g.LogDebug("SearchReplace: Spawning dialog");
				new frmFindReplace(GetActiveEditor(), GetActiveEditor().txtEditor, new FindReplaceOptions(), cboRecentSearches.Text).Show();
			}

			//new frmFindReplace(GetActiveEditor(), GetActiveEditor().txtEditor, new FindReplaceOptions()).Show();
		}

		private void mnuEdit_GoTo_Click(object sender, System.EventArgs e) {
			SyntaxEditor txtEditor = this.GetActiveEditor().txtEditor;

			string result = frmMain.ShowPrompt("Go To Line", "Please enter a line number to jump to,"
				+ " between 1 and " + txtEditor.Document.Lines.Count.ToString(),
				"Jump to line:");

			if (result == "")
				return;
			
			try {
				txtEditor.SelectedView.GoToLine(Convert.ToInt32(result) - 1);
			} catch {}
		}

		private void mnuEdit_BKAdd_Click(object sender, System.EventArgs e) {
			GetActiveEditor().BookmarkAdd();
		}

		private void mnuEdit_BKPrev_Click(object sender, System.EventArgs e) {
			GetActiveEditor().BookmarkJump(-1);
		}

		private void mnuEdit_BKNext_Click(object sender, System.EventArgs e) {
			GetActiveEditor().BookmarkJump(1);
		}

		private void mnuEdit_BKDel_Click(object sender, System.EventArgs e) {
			GetActiveEditor().BookmarkDel();
		}

		private void mnuEdit_BreakPoint_Click(object sender, System.EventArgs e) {
			GetActiveEditor().ToggleHalt();
		}

		private void mnuEdit_CommentSel_Click(object sender, System.EventArgs e) {
			GetActiveEditor().CommentSel();
		}

		private void mnuEdit_UncommentSel_Click(object sender, System.EventArgs e) {
			GetActiveEditor().UncommentSel();
		}


		private void CheckMenus(ToolStripMenuItem tsmi) {
			foreach(object subm in tsmi.DropDownItems) {

				if (!(subm is ToolStripMenuItem))
					continue;

				ToolStripMenuItem sub = (ToolStripMenuItem)subm;

				CheckMenus(sub);

				if (sub.Tag == null)
					continue;
				else if (sub.Tag.ToString() == "p")
					sub.Enabled = (g.Project != null);
				else if (sub.Tag.ToString() == "f")
					sub.Enabled = (this.tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true);
				else if (sub.Tag.ToString() == "d")
					sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true);
				else if (sub.Tag.ToString() == "dr")
					sub.Enabled = g.IsDebugging;
				else if (sub.Tag.ToString() == "drb")
					sub.Enabled = (g.IsDebugging && g.IsBroken);
				else if (sub.Tag.ToString() == "ndr")
					sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true && g.IsDebugging == false);
				else if (sub.Tag.ToString() == "ndb")
					sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true && g.IsDebugging == true && g.IsBroken == false);
                else if (sub.Tag.ToString() == "macro_record")
                    sub.Enabled = (g.Project != null && this.tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetActiveEditor().isRecordingMacro() == true);
                else if (sub.Tag.ToString() == "macro_not_record")
					sub.Enabled = (g.Project != null && this.tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetActiveEditor().isRecordingMacro() == false);
                else if (sub.Tag.ToString().StartsWith("m")) {
                    int tag = Convert.ToInt32(sub.Tag.ToString().Remove(0, 1));
					sub.Enabled = (tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetMacroText(tag) != "");
				} else if (sub.Tag.ToString() == "scc_p") {
					sub.Enabled = (g.SourceControlEnabled && g.SourceControlPlugin.CWIsProjectUnderSourceControl());
				} else if (sub.Tag.ToString().StartsWith("scc")) {
					sub.Enabled = g.SourceControlEnabled;
				}

				
			}
		}

		private void CheckMenus(ToolStrip ts) {
			foreach (object subm in ts.Items) {

				if (!(subm is ToolStripButton))
					continue;

				ToolStripButton sub = (ToolStripButton)subm;

                if (sub.Tag == null)
                    continue;
                else if (sub.Tag.ToString() == "p")
                    sub.Enabled = (g.Project != null);
                else if (sub.Tag.ToString() == "f")
                    sub.Enabled = (this.tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true);
                else if (sub.Tag.ToString() == "d")
                    sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true);
                else if (sub.Tag.ToString() == "dr")
                    sub.Enabled = g.IsDebugging;
                else if (sub.Tag.ToString() == "drb")
                    sub.Enabled = (g.IsDebugging && g.IsBroken);
                else if (sub.Tag.ToString() == "ndr")
                    sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true && g.IsDebugging == false && g.IsBroken == false);
                else if (sub.Tag.ToString() == "ndb")
                    sub.Enabled = (g.Project != null && g.Project.DebugEnabled == true && g.IsDebugging == true && g.IsBroken == false);
                else if (sub.Tag.ToString() == "macro_record")
                    sub.Enabled = (g.Project != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetActiveEditor().isRecordingMacro() == true);
                else if (sub.Tag.ToString() == "macro_not_record")
                    sub.Enabled = (g.Project != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetActiveEditor().isRecordingMacro() == false);
                else if (sub.Tag.ToString().StartsWith("m")) {
                    int tag = Convert.ToInt32(sub.Tag.ToString().Remove(0, 1));
                    sub.Enabled = (tabMain.ActiveTabPage != null && this.tabMain.ActiveTabPage.Control is UCEditor == true && GetMacroText(tag) != "");
				} else if (sub.Tag.ToString() == "scc_p") {
					sub.Enabled = g.SourceControlPlugin.CWIsProjectUnderSourceControl();
				} else if (sub.Tag.ToString().StartsWith("scc")) {
					sub.Enabled = g.SourceControlEnabled;
				}
			}
		}

		private void tmrMenuMonitor_Tick(object sender, System.EventArgs e) {
			// Loop through all root menus
			foreach (ToolStripMenuItem tsi in mnuMain.Items)
				CheckMenus(tsi);

			// Loop through toolbar buttons
			CheckMenus(tbEditBar);

			// Verify the debugging
			if (g.Project != null) {
				// Set up the debugging toolbar preferences
				Directory.SetCurrentDirectory(g.Project.ProjectPath);

				if (g.Project.DebugEnabled) {
					if (g.Project.DebugExe == "" || File.Exists(g.Project.DebugExe) != true) {
						g.Project.DebugEnabled = false;
						MessageBox.Show(this, "Debugging has been disabled because the project executable is not defined or does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					} else if (g.Project.DebugPasswd == "") {
						g.Project.DebugEnabled = false;
						MessageBox.Show(this, "Debugging has been disabled because the debug password field has been left blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			} 
		}

		private void mnuFile_Popup(object sender, System.EventArgs e) {
			//CheckMenus();
		}

		private void frmMain_Closed(object sender, System.EventArgs e) {
			Application.Exit();
			GC.Collect();

			//System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		private void mnuWindow_ShowBar_Click(object sender, System.EventArgs e) {
			this.dockMgr.ShowAllContents();
		}

		private void MacroBin() {
			frmMacros fMacros = new frmMacros();
			fMacros.Owner = this;

			fMacros.ShowDialog();
			fMacros.Dispose();

			InitMacros();
		}

		private void InitMacros() {

			g.LogDebug("InitMacros: Enter");

			// First remove current macro menu items
			g.LogDebug("InitMacros: Finding macro menus");
            foreach (ToolStripItem menu in mnuMacros.DropDownItems) {
                if (menu.Tag == null || menu.Tag.ToString().StartsWith("m") == false || menu.Tag.ToString().StartsWith("macro") == true)
                    continue;

				
                int menutag = Convert.ToInt32(menu.Tag.ToString().Remove(0, 1));
                string menuname = GetMacroText(menutag);
                if (menuname == "") {
                    menu.Text = "Custom Macro " + Convert.ToString(menutag + 1);
                    menu.Enabled = false;
                } else {
                    menu.Text = menuname;
                    menu.Enabled = true;
                }

				g.LogDebug("InitMacros: Set: " + menu.Text);
            
            }
		}

		private void BuildMacroMenu() {
			// Rebuild the macro listing

			g.LogDebug("BuildMacroMenu: Enter");

            ArrayList todel = new ArrayList();
			g.LogDebug("BuildMacroMenu: Removing previous macro menus");
            foreach (ToolStripItem menu in this.mnuMacros.DropDownItems)
                if (!(menu.Tag == null || menu.Tag.ToString().StartsWith("m") == false || menu.Tag.ToString().StartsWith("macro") == true))
                    todel.Add(menu);

            foreach (ToolStripItem menu in todel)
                this.mnuMacros.DropDownItems.Remove(menu);

			g.LogDebug("BuildMacroMenu: Reinitializing macro menus");
            for (int i = 0; i < 10; i++) {
                ToolStripMenuItem newmenu = new ToolStripMenuItem();

                string menuname = "";
                menuname = GetMacroText(i);

                if (menuname == "") {
                    menuname = "Custom Macro " + Convert.ToString((i + 1));
                    newmenu.Enabled = false;
                }

                newmenu.ShortcutKeys = GetMacroShortcut(i + 1);
                newmenu.ShowShortcutKeys = true;
                newmenu.Text = menuname;
                newmenu.Click += new EventHandler(MacroProcess_Click);
                newmenu.Tag = "m" + i.ToString();

                this.mnuMacros.DropDownItems.Add(newmenu);
            }
		}

        private Keys GetMacroShortcut(int macronum) {
			g.LogDebug("GetMacroShortcut: Get for " + macronum.ToString());
			switch (macronum) {
				case 10:
					return Keys.Alt | Keys.D0;
				case 1:
                    return Keys.Alt | Keys.D1;
				case 2:
                    return Keys.Alt | Keys.D2;
				case 3:
                    return Keys.Alt | Keys.D3;
				case 4:
                    return Keys.Alt | Keys.D4;
				case 5:
                    return Keys.Alt | Keys.D5;
				case 6:
                    return Keys.Alt | Keys.D6;
				case 7:
                    return Keys.Alt | Keys.D7;
				case 8:
                    return Keys.Alt | Keys.D8;
				case 9:
                    return Keys.Alt | Keys.D9;
				default:
                    return Keys.None; ;
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

		private void MacroProcess_Click(object sender, EventArgs e) {
            int macronum = Convert.ToInt32((sender as ToolStripMenuItem).Tag.ToString().Remove(0, 1));

			foreach(CProject.Macro macro in g.Project.MacroList.Values) {
				if (macro.MacroNum == macronum) {
					GetActiveEditor().txtEditor.SelectedView.RaiseEditCommand(macro.MacroCmd);
					return;
				}
			}
		}

		private void mnuMacro_Dir_Click(object sender, System.EventArgs e) {
			MacroBin();
		}

		private void mnuMacro_BeginRec_Click(object sender, System.EventArgs e) {
			this.GetActiveEditor().BeginRecordMacro();
		}

		private void mnuMacro_EndRec_Click(object sender, System.EventArgs e) {
			this.GetActiveEditor().StopRecordMacro();
		}

		private void tbEditBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			switch (e.Button.Tag.ToString()) {
				case "newproj":
					if (!this.PerformCloseOperations())
						return;
					this.mnuFile_NewProject_Click(null, null);
					break;
				case "openproj":
					if (!this.PerformCloseOperations())
						return;
					this.mnuFile_OpenProj_Click(null, null);
					break;
				case "newitem":
					this.mnuFile_AddNew_Click(null, null);
					break;
				case "olditem":
					this.mnuFile_AddExist_Click(null, null);
					break;
				case "save":
					this.mnuFile_SaveFile_Click(null, null);
					break;
				case "saveas":
					this.mnuFile_SaveAs_Click(null, null);
					break;
				case "print":
					this.GetActiveEditor().Print();
					break;
				case "printprev":
					this.GetActiveEditor().PrintPreview();
					break;
				case "cut":
					this.GetActiveEditor().txtEditor.SelectedView.CutToClipboard();
					break;
				case "copy":
					this.GetActiveEditor().txtEditor.SelectedView.CopyToClipboard();
					break;
				case "paste":
					this.GetActiveEditor().txtEditor.SelectedView.PasteFromClipboard();
					break;
				case "delete":
					this.GetActiveEditor().txtEditor.SelectedView.DeleteToNextWord();
					break;
				case "undo":
					this.GetActiveEditor().txtEditor.Document.UndoRedo.Undo();
					break;
				case "redo":
					this.GetActiveEditor().txtEditor.Document.UndoRedo.Redo();
					break;
				case "find":
					this.mnuEdit_SearchReplace_Click(null, null);
					break;
				case "search":
					this.mnuEdit_Search_Click(null, null);
					break;
				case "markadd":
					this.GetActiveEditor().BookmarkAdd();
					break;
				case "markdel":
					this.GetActiveEditor().BookmarkDel();
					break;
				case "markup":
					this.GetActiveEditor().BookmarkJump(-1);
					break;
				case "markdown":
					this.GetActiveEditor().BookmarkJump(1);
					break;
				case "findfiles":
					this.mnuEdit_SearchFiles_Click(null, null);
					break;
				case "dbg_start":
					this.mnuDebug_Start_Click(null, null);
					break;
				case "dbg_break":
					this.mnuDebug_Break_Click(null, null);
					break;
				case "dbg_stop":
					this.mnuDebug_Stop_Click(null, null);
					break;
				case "dbg_step_into":
					this.mnuDebug_StepInto_Click(null, null);
					break;
				case "dbg_step_over":
					this.mnuDebug_StepOver_Click(null, null);
					break;
				case "dbg_step_out":
					this.mnuDebug_StepOut_Click(null, null);
					break;
				default:
					MessageBox.Show("?? " + e.Button.Tag.ToString());
					break;
			}
		}

		private void mnuHelp_ReportBugs_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/redir_buts.php");
		}

		private void mnuDebug_Start_Click(object sender, System.EventArgs e) {
			// Switch the program into debug mode
			g.LogDebug("StartDebugClick: Enter");
			if (!g.IsDebugging) {
				// Save all files
				g.LogDebug("StartDebugClick: Saving all files");
				this.mnuFile_SaveAll_Click(null, null);

				// Switch to debug mode.
				g.LogDebug("StartDebugClick: Switching modes");
				SwitchDebug(true);
			} else {
				g.LogDebug("StartDebugClick: Clearing IP");
				ClearIP();

				g.LogDebug("StartDebugClick: Sending CONTINUE message");
				frmMain.stc_DebugQueue.Add("CONTINUE\n");
			}
		}

		public void DebugExecPreinit() {
			// Execute any pre-debug commands
			g.LogDebug("DebugExecPreinit: Enter");

			// Do we want to delete DSOs?
			if (g.Config.b_DeleteDSO) {
				g.LogDebug("DebugExecPreinit: Deleting DSOs");
				DeleteDSO(g.Config.DSOExtension, g.Project.ProjectPath);
			}

			// Execute programs
			foreach(CConfig.DebugExecEntry entry in g.Config.DebugRun) {
				g.LogDebug("DebugExecPreinit: Executing " + entry.path);

				if (!File.Exists(entry.path)) {
					g.LogDebug("DebugExecPreinit: Not found; silent skip");
					continue;
				}

				try {
					System.Diagnostics.Process.Start(entry.path, entry.parameters);
				} catch (Exception exc) {
					g.LogDebug("DebugExecPreinit: Failed: " + exc.Message);
					MessageBox.Show(this, "Failed to execute pre-debug program \"" + entry.path + "\":\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		public void DeleteDSO(string extension, string startpath) {
			g.LogDebug("DeleteDSO: " + extension + " in " + startpath);

			// Go to the base directory of the project and delete all DSOs
			foreach(string dir in Directory.GetDirectories(startpath))
				DeleteDSO(extension, dir);

			foreach(string file in Directory.GetFiles(startpath, "*." + extension)) {
				try {
					//if (g.IsDebugging)
					//	this.DebugUC.txtOutput.AppendText("* Deleting " + file, true);

					g.LogDebug("DebugExecPreinit: Deleting " + file);
					File.Delete(file);
				} catch {}
			}
		}

		public void SwitchDebug(bool isDebugging) {
			if (g.Project == null)
				return;

			g.LogDebug("SwitchDebug: Enter (D " + isDebugging.ToString());

			// Switch all the windows to disabled or enabled:
			g.LogDebug("SwitchDebug: Toggling readonly state");
			foreach (PrimaryTab<UCEditor> tab in g.Editors)
				tab.Control.txtEditor.Document.ReadOnly = isDebugging;

			if (isDebugging) {
				g.LogDebug("SwitchDebug: IsDebugging");

				if (g.IsDebugging) {
					g.LogDebug("SwitchDebug: Already debugging; return");
					return;
				}

				// Check for script errors
				g.LogDebug("SwitchDebug: Check for script errors");
				if (g.Config.b_Err_BeforeCompile) {
					if (ScanProject()) {
						g.LogDebug("SwitchDebug: Found. Prompt");
						DialogResult result = MessageBox.Show(this, "There were errors detected in your code.\n\nWould you like to continue with debugging?", "Syntax Scanner - Debug", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

						if (result == DialogResult.No) {
							g.LogDebug("SwitchDebug: Abort");
							SwitchDebug(false);
							return;
						}
					}
				}

				// Execute pre-debug actions
				g.LogDebug("SwitchDebug: Running Execute-On-Debug");
				DebugExecPreinit();

				// Execute the program, if possible
				g.LogDebug("SwitchDebug: Checking for debugger EXE " + g.Project.DebugExe);
				if (!File.Exists(g.Project.DebugExe)) {
					g.LogDebug("SwitchDebug: Not found! Abort");

					SwitchDebug(false);
					return;
				}

				// If we're editing the main.cs file, do it now
				if (g.Project.DebugAutoInsert) {
					g.LogDebug("SwitchDebug: Auto inserting into Main.CS");
					if (!AutoEditMainCs(true)) {
						g.LogDebug("SwitchDebug: Failed to insert; abort");
						MessageBox.Show(this, "There was an error attempting to initialize OneClick debugging.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

						SwitchDebug(false);
						return;
					}
				}

				frmMain.stc_DebugQueue = new ArrayList();

				g.LogDebug("SwitchDebug: Init debug thread");
				frmMain.stc_DebugThread = new Thread(new ThreadStart(DebugThread));
				frmMain.stc_DebugThread.Start();

				// Queue up the password exchange:
				g.LogDebug("SwitchDebug: Enqueuing password exchange");
				frmMain.stc_DebugQueue.Add(g.Project.DebugPasswd + "\n");

				// Send all the breakpoints in all active files
				g.LogDebug("SwitchDebug: Enqueuing breakpoints");
				foreach(CProject.Breakpoint brk in g.Project.ProjectBreaks) {
					g.LogDebug("SwitchDebug: Breakpoint " + brk.file.RelativePath + " Line " + brk.LineNumber + " OK");
					string filerelpath = CProject.Breakpoint.FixPath(CProject.PathGetRelative(Path.GetFullPath(brk.file.RelativePath), Path.GetDirectoryName(Path.GetFullPath(g.Project.DebugExe))));
					frmMain.stc_DebugQueue.Add("BRKSET " + filerelpath + " " + brk.LineNumber.ToString() + " false " + brk.PassCount.ToString() + " " + brk.Conditional + "\n");
				}

				// Signal to the main process that we want to begin:
				g.LogDebug("SwitchDebug: Begin execution");
				frmMain.stc_DebugQueue.Add("CONTINUE\n");

				// Switch into debug interface mode:
				g.LogDebug("SwitchDebug: Initializing debug interface");
				this.DebugUC = new UCDebug();
				this.dockDebug = new DockingManager(this, ((g.Config.ColorPref == ColorPref.Standard) ? VisualStyle.IDE2005 : VisualStyle.Office2003));

				this.dockDebug.InnerControl = this.tabMain;
				this.dockDebug.OuterControl = this.sbMain;
				//this.dockDebug.OuterControl = this.pnlExplorer;

				Content cStack = dockDebug.Contents.Add(this.DebugUC.lvCallStack, "Call Stack", this.ilProject, 7);
				Content cVars = dockDebug.Contents.Add(this.DebugUC.lvVars, "Variables", this.ilProject, 8);
				Content cOutput = dockDebug.Contents.Add(this.DebugUC.txtOutput, "Output", this.ilProject, 9);
				Content cWatch = dockDebug.Contents.Add(this.DebugUC.lvWatch, "Watch", this.ilProject, 20);

				cStack.CloseButton = false;
				cStack.CloseOnHide = false;
				cStack.HideButton = false;
				cVars.CloseButton = false;
				cVars.CloseOnHide = false;
				cVars.HideButton = false;
				cOutput.CloseButton = false;
				cOutput.CloseOnHide = false;
				cOutput.HideButton = false;
				cWatch.CloseButton = false;
				cWatch.CloseOnHide = false;
				cWatch.HideButton = false;


				// Initialize the dockbar
				try {
					g.LogDebug("SwitchDebug: Initializing workspace");
					WindowContent wc = this.dockDebug.AddContentWithState(cStack, State.DockBottom);
					this.dockDebug.AddContentToWindowContent(cVars, wc);
					this.dockDebug.AddContentToWindowContent(cOutput, wc);
					this.dockDebug.AddContentToWindowContent(cWatch, wc);

					// Restore saved preferences
					if (g.Config.DebugBarData != null)
						dockDebug.LoadConfigFromArray(g.Config.DebugBarData);

					this.dockDebug.ShowAllContents();

					// Set the debug variable
					g.IsDebugging = true;

					// Rename the menu
					this.mnuDebug_Start.Text = "&Continue";

					// Print basic status text
					this.DebugUC.txtOutput.AppendText("=========== TorqueDev Integrated Development Environment v. " + Application.ProductVersion + " ===========\nTD: Waiting for application to spawn ... ", true);

					// Set the form name
					try {
						this.Text = g.Project.ProjectName + " - netMercs TorqueDev [DEBUGGING]";
					} catch {
						this.Text = g.Project.ProjectName + " - netMercs TorqueDev [DEBUGGING] - [" + this.tabMain.ActiveTabPage.Title + "]";
					}
				} catch {}
			} else {
				g.LogDebug("SwitchDebug: IsNotDebug");
				// Close the process
				try {
					g.LogDebug("SwitchDebug: Killing engine");
					frmMain.stc_DebugProc.Kill();
					frmMain.stc_DebugProc.Close();
				} catch {}

				// Close the thread
				try {
					g.LogDebug("SwitchDebug: Killing debug thread");
					//frmMain.stc_DebugThread.Abort();
					frmMain.stc_DebugThread.Join(2000);
				} catch {}

				// Abort the connection
				try {
					g.LogDebug("SwitchDebug: Closing TCP socket");
					frmMain.stc_tcp.Close();
				} catch {}

				// Save the dockbar position
				try {
					g.LogDebug("SwitchDebug: Saving debug dockbar settings");
					g.Config.DebugBarData = this.dockDebug.SaveConfigToArray();
				} catch {}
				
				// If we're in debug mode, bring up the debug results window
				try {
					if (g.Config.bShowDebugSummary) {
						g.LogDebug("SwitchDebug: Showing debug summary");
						// If we're showing debug output, spawn a browser window for it --
						// We'll need to write the temp file to disk first
						StreamWriter writer = new StreamWriter(CConfig.GlobalUserpath + "\\_temp_debug.html", false);
						string outtext = "<html><title>Debug Output - " + DateTime.Now.ToString() + "</title>" +
							"<body bgcolor=#ffffff text=#000000 style=\"font-size:12px;font-family:courier new\"><pre>" +
							this.DebugUC.txtOutput.Text + "\r\n\r\n</pre></body></html>";

						writer.Write(outtext);
						writer.Close();

						SpawnBrowser(CConfig.GlobalUserpath + "\\_temp_debug.html", true);
						//frmDebugOutput fDebugOutput = new frmDebugOutput(this.DebugUC.txtOutput.Text);
						//fDebugOutput.Owner = this;
						//fDebugOutput.Show();
						
					}
				} catch {}

				// Close the dockbar
				try {
					g.LogDebug("SwitchDebug: Closing debug dockbar");
					this.dockDebug.HideAllContents();
					this.dockDebug.Dispose();
				} catch {}

				// Dispose the usercontrol
				try {
					g.LogDebug("SwitchDebug: Disposing debug UC");
					this.DebugUC.Dispose();
				} catch {}

				// Nullify the resources
				g.LogDebug("SwitchDebug: Freeing memory");
				frmMain.stc_DebugProc = null;
				frmMain.stc_DebugQueue = null;
				frmMain.stc_DebugThread = null;
				frmMain.stc_tcp = null;
				this.DebugUC = null;
				this.dockDebug = null;

				// Clear instruction pointer
				g.LogDebug("SwitchDebug: Clearing IP");
				ClearIP();

				// Set the debug variable:
				g.LogDebug("SwitchDebug: Setting IsDebugging false");
				g.IsDebugging = false;

				// Rename the menu
				this.mnuDebug_Start.Text = "&Start";

				// Remove the OneClick debug lines from the main form
				if (g.Project.DebugAutoInsert) {
					g.LogDebug("SwitchDebug: Reverting Main.CS changes");
					AutoEditMainCs(false);
				}

				// Rename the form
				try {
					this.Text = g.Project.ProjectName + " - netMercs TorqueDev - [" + this.tabMain.ActiveTabPage.Title + "]";
				} catch {
					this.Text = g.Project.ProjectName + " - netMercs TorqueDev";
				}
			}

			g.IsBroken = false;
		}

		public void InvokeThreadedMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) {
			this.Invoke(__ThreadedMessageBox, message, title, buttons, icon); 
		}

		private void ThreadedMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) {
			MessageBox.Show(this, message, title, buttons, icon);
		}

		private bool AutoEditMainCs(bool inserting) {
			if (!File.Exists(g.Project.DebugMainCs))
				return false;

			g.LogDebug("AutoEditMainCs: Enter (Inserting? " + inserting.ToString());

			// Search all the open windows.  Is it open?
			g.LogDebug("AutoEditMainCs: Search editors");
			foreach (PrimaryTab<UCEditor> ed in g.Editors) {
				if (ed.Control.g_curFile.RelativePath == g.Project.DebugMainCs) {
					// We need to set this file to a "forced reload" state so the
					// IDE doesn't ask the user to authorize the reload
					ed.Control.g_curFile.isForcedReload = true;

					g.LogDebug("AutoEditMainCs: Forcing reload on Main.CS which is open");
					break;
				}
			}

			// The open file is what we want.  Insert (or remove) the information
			// from the file by loading it:
			g.LogDebug("AutoEditMainCs: Opening MainCS and reading contents (" + g.Project.DebugMainCs + ")");
			StreamReader fs = new StreamReader(Path.GetFullPath(g.Project.DebugMainCs));
			string contents = fs.ReadToEnd();
			fs.Close();

			// Depending on what we're doing with it, make the changes:
			if (inserting) {
				g.LogDebug("AutoEditMainCs: Inserting");
				if (!contents.StartsWith("// TorqueDev Debug --")) {
					contents = "// TorqueDev Debug -- DO NOT MODIFY ANYTHING ABOVE OR IN-BETWEEN THIS AND THE CLOSE BLOCK\r\n" +
						"if ($dbgVersion > 0)\r\n" +
						"\tdbgSetParameters(" + g.Project.DebugPort.ToString() + ", \"" + g.Project.DebugPasswd + "\", true);\r\n" +
						"else\r\n" +
						"\tdbgSetParameters(" + g.Project.DebugPort.ToString() + ", \"" + g.Project.DebugPasswd + "\");\r\n" +
						"// End TorqueDev Debug -- YOU MAY NOW MODIFY STUFF BELOW THIS LINE\r\n" +
						contents;
				}
			} else {
				// Delete the top lines from the file, if the first line starts with "// Codeweaver Debug"
				g.LogDebug("AutoEditMainCs: Removing");
				if (contents.StartsWith("// TorqueDev Debug --")) {
					string[] lines = contents.Replace("\r", "").Split('\n');

					int i = 0;
					while (i != (lines.Length - 1)) {
						if (lines[i].StartsWith("// End TorqueDev Debug"))
							break;

						i++;
					}

					contents = String.Join("\r\n", lines, ++i, (lines.Length - i));
				}
			}

			// Write the contents to the file
			g.LogDebug("AutoEditMainCs: Writing file");
			StreamWriter sw = new StreamWriter(Path.GetFullPath(g.Project.DebugMainCs));
			sw.Write(contents);
			sw.Flush();

			g.LogDebug("AutoEditMainCs: Closing file");
			sw.Close();

			return true;
		}

		private void ClearIP() {
			
			g.IsBroken = false;

			// Switch the debug priority back
			if (frmMain.stc_DebugProc != null) {
				g.LogDebug("ClearIP: Resetting engine priority to normal");
				frmMain.stc_DebugProc.PriorityClass = System.Diagnostics.ProcessPriorityClass.Normal;
			}

			//foreach(Crownwood.DotNetMagic.Controls.TabPage page in this._al_Documents) {
			//	if (page.Control is UCEditor)
			//		(page.Control as UCEditor).DrawLineIndicator(-1);
			//}

			g.LogDebug("ClearIP: Clearing active instruction pointers");
			foreach (PrimaryTab<UCEditor> ed in g.Editors)
				ed.Control.DrawLineIndicator(-1);
		}

		private void DebugThread() {
			g.LogDebug("DebugThread: INIT THREAD");

			// Launch the program
			g.LogDebug("DebugThread: Setting directory " + g.Project.ProjectPath);
			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			g.LogDebug("DebugThread: About to start engine process");
			System.Diagnostics.ProcessStartInfo procinfo = new System.Diagnostics.ProcessStartInfo(g.Project.DebugExe, g.Project.DebugParams);
			procinfo.WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(g.Project.DebugExe));

			g.LogDebug("DebugThread: Starting engine process");
			try {
				frmMain.stc_DebugProc = System.Diagnostics.Process.Start(procinfo);
			} catch (Exception exc) {
				g.GlobalMessageBox("Failed to start engine executable: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Thread.CurrentThread.Abort();
				return;
			}
	
			// Sleep for a few seconds until it initalizes
			// Application.DoEvents();
			g.LogDebug("DebugThread: Waiting " + g.Config.DebugTimeout + " secs");
			for (int i = g.Config.DebugTimeout; i > 0; i--) {
				this.Invoke(this.__DebugWriteLog, new object[1] { i.ToString() + " " });
				Thread.Sleep(1000);
			}

			this.Invoke(this.__DebugWriteLog, new object[1] { "\n" });

			// Try and connect to the TCP interface
			g.LogDebug("DebugThread: Spawning TCP object");
			frmMain.stc_tcp = new TcpClient();

			try {
				frmMain.stc_tcp.NoDelay = true;
				frmMain.stc_tcp.SendTimeout = 0;
				frmMain.stc_tcp.ReceiveTimeout = 0;
				frmMain.stc_tcp.LingerState = new LingerOption(false, 0);

				g.LogDebug("DebugThread: Attempting to connect to local port " + g.Project.DebugPort.ToString());
				frmMain.stc_tcp.Connect("127.0.0.1", g.Project.DebugPort);
			} catch {
				g.LogDebug("DebugThread: Failed to connect");
				g.GlobalMessageBox("Failed to connect to local application.  Please make sure you have enabled the debugger in the \"main.cs\" file in the root of your project before contiuing.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//SwitchDebug(false);

				g.LogDebug("DebugThread: Killing engine");
				frmMain.stc_DebugProc.Kill();
				this.Invoke(this.__DebugProcessCmd, new object[1] { "QUIT" });
				return;
			}

			g.LogDebug("DebugThread: Initializing network stream");
			NetworkStream netstream = frmMain.stc_tcp.GetStream();

			g.LogDebug("DebugThread: Setting thread priority");
			Thread.CurrentThread.Priority = ThreadPriority.Highest;

			g.LogDebug("DebugThread: Begin main loop");
			while(Thread.CurrentThread.ThreadState == System.Threading.ThreadState.Running) {
				if (Thread.CurrentThread.ThreadState == System.Threading.ThreadState.AbortRequested) {
					g.LogDebug("DebugThread: Thread abort requested; return");
					return;
				}

				// Sleep a bit
				Thread.Sleep(10);

				if (frmMain.stc_DebugProc.HasExited) {
					g.LogDebug("DebugThread: Debug process has exited; quit");

					this.Invoke(this.__DebugProcessCmd, new object[1] { "QUIT" });

					g.LogDebug("DebugThread: Closing network stream");
					netstream.Close();

					g.LogDebug("DebugThread: Aborting current thread");
					Thread.CurrentThread.Abort();
					return;
				}

				// Check if we're still connected

				// Check the outgoing queue
				while (frmMain.stc_DebugQueue.Count != 0) {
					// We've got data; send it through the pipe
					try {
						string queue_str = frmMain.stc_DebugQueue[0].ToString();
						netstream.BeginWrite(System.Text.ASCIIEncoding.ASCII.GetBytes(queue_str), 0, queue_str.Length, null, null);

						g.LogDebug("DebugThread: FEED << " + frmMain.stc_DebugQueue[0].ToString());

						if (frmMain.stc_bDebugMode)
							this.DebugPrint.Add(frmMain.stc_DebugQueue[0].ToString());
						
						frmMain.stc_DebugQueue.RemoveAt(0);
						Thread.Sleep(1);
					} catch {
						// Stream got closed?
						g.LogDebug("DebugThread: Network stream closed arbitrarily. Abort thread");
						netstream.Close();
						Thread.CurrentThread.Abort();
						return;
					}
				}

				// Check the incoming queue
				if (netstream.DataAvailable) {
					// We've got data!
					string indata = "";

					try {
						byte[] inbuffer = new byte[frmMain.stc_tcp.ReceiveBufferSize];
						netstream.Read(inbuffer, 0, inbuffer.Length);
						indata = System.Text.ASCIIEncoding.ASCII.GetString(inbuffer);
					} catch {
						// Stream got closed?
						netstream.Close();
						Thread.CurrentThread.Abort();
						return;
					}

					// Check if it's not empty
					if (indata == "") {
						g.LogDebug("DebugThread: Netstream reported empty set; abort thread");
						netstream.Close();
						Thread.CurrentThread.Abort();
						return;
					}

					// Parse the input string:
					g.LogDebug("DebugThread: Invoke __DebugProcessCmd");
					this.Invoke(this.__DebugProcessCmd, new object[1] {indata});
				}
			}
		}

		private void DebugWriteLog(string indata) {
			this.DebugUC.txtOutput.AppendText(indata, true);
		}

		private void DebugParseData(string indata) {
			// Parse debug commands and parameters
			indata = indata.Trim('\0').Replace("\r\n", "\n");

			string [] cmdarray = indata.Split('\n');

			if (frmMain.stc_bDebugMode) {
				foreach(string log in this.DebugPrint)
					this.DebugUC.txtOutput.AppendText("~~ DEBUG << " + log, true);

				this.DebugPrint.Clear();
			}

			foreach(string single_cmd in cmdarray) {
				string[] single_array = single_cmd.Split(new char[1] {' '}, 2);

				if (frmMain.stc_bDebugMode)
					this.DebugUC.txtOutput.AppendText("~~ DEBUG >> " + single_cmd + "\n", true);

				g.LogDebug("DebugParseData: FEED >> " + single_cmd);

				try {
					switch(single_array[0]) {
						case "QUIT":
							this.SwitchDebug(false);
							return;
						case "COUT":
							this.DebugUC.txtOutput.AppendText(single_array[1] + "\n", true);
							break;
						case "EVALOUT":
							string[] evalarray = single_array[1].Split(new char[1] { ' ' }, 2);
							if (evalarray[0].StartsWith("!")) {
								// "Variables" listing
								ListViewItem newitem = new ListViewItem(evalarray[0].Substring(1, evalarray[0].Length - 1));
								newitem.SubItems.Add(evalarray[1]);

								this.DebugUC.lvVars.Items.Add(newitem);
							} else if (evalarray[0].StartsWith("#")) {
								// Watch list
								ListViewItem newitem = new ListViewItem(evalarray[0].Substring(1, evalarray[0].Length - 1));
								newitem.SubItems.Add(evalarray[1]);

								this.DebugUC.lvWatch.Items.Add(newitem);
							} else {
								// Mouseover variables request
								frmMain.stc_DebugCurTokVal = "runtime <b>" + evalarray[0] + "</b> = " + UCEditor.StripBadChars(evalarray[1]);
							}
							break;
						case "BREAK":
							// We hit a breakpoint
							g.LogDebug("DebugParseData: Breakpoint hit");
							BreakPoint(single_array[1]);
							break;
						case "BRKMOV":
							// We've moved a breakpoint
							g.LogDebug("DebugParseData: Attempt to move breakpoint");
							string[] movarray = single_array[1].Split(' ');
							foreach (PrimaryTab<UCEditor> ed in g.Editors) {
								if (Path.GetFullPath(ed.Control.g_curFile.RelativePath) == Path.GetFullPath(".\\" + movarray[0].Replace("/", "\\"))) {
									// Alter entry in the project breakpoint listing
									g.Project.ProjectBreaks.RemoveAtLine(ed.Control.g_curFile, Convert.ToInt32(movarray[1]) - 1);
									g.Project.ProjectBreaks.Add(new CProject.Breakpoint(ed.Control.g_curFile, Convert.ToInt32(movarray[2]) - 1));

									// Refresh the breakpoints
									ed.Control.BreakpointRender();
									break;
								}
							}
							break;
						case "PASS":
							// We've got a password notification
							if (single_array[1] == "WrongPassword.") {
								g.LogDebug("DebugParseData: Password wrong; abort debug");

								this.SwitchDebug(false);
								MessageBox.Show(this, "Invalid debugging password specified.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							//Thread.Sleep(1000);
							break;
					}
				} catch {}
			}
		}

		private void BreakPoint(string breakdata) {

			g.LogDebug("BreakPoint: Enter (" + breakdata + ")");

			// Switch process into BelowNormal priority
			g.LogDebug("BreakPoint: Switching engine to low priority");
			frmMain.stc_DebugProc.PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;

			// Set the debug flag and current directory
			g.IsBroken = true;
			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			// Construct the call-stack as necessary:
			g.LogDebug("BreakPoint: Clearing call stack and variable displays");
			this.DebugUC.lvCallStack.Items.Clear();
			this.DebugUC.lvVars.Items.Clear();

			string[] breakd = breakdata.Split(' ');
			string callstack = "";

			g.LogDebug("BreakPoint: Parse callstack");
			for(int i = 0; i < breakd.Length; i+=3) {
				// Backwards-construction of the thingy
				ListViewItem newitem = new ListViewItem(breakd[i+2]);
				newitem.SubItems.Add(breakd[i+1]);
				newitem.SubItems.Add(breakd[i]);

				this.DebugUC.lvCallStack.Items.Add(newitem);

				callstack += breakd[i+2] + ":" + breakd[i+1] + ":" + breakd[i] + " ";
			}

			callstack = callstack.Trim();

			// Open the file, if not already open
			CProject.File n_file = null;

			g.LogDebug("BreakPoint: Find file in project: " + breakd[0]);
			foreach(CProject.File file in g.Project.FileList) {
				if (Path.GetFullPath(file.RelativePath) == Path.GetFullPath(".\\" + breakd[0].Replace("/", "\\"))) {
					n_file = file;
					break;
				}
			}

			if (n_file == null) {
				g.LogDebug("BreakPoint: Must create temp file");
				n_file = DebugTemporaryFile(CProject.PathGetRelative(Path.GetFullPath(breakd[0].Replace("/", "\\")), g.Project.ProjectPath));
			}

			g.LogDebug("BreakPoint: Open file call");
			OpenFile(n_file, Convert.ToInt32(breakd[1]) - 1, false);

			// Remove all the IP lines from all the files
			/*foreach(Crownwood.DotNetMagic.Controls.TabPage page in this._al_Documents) {
				if (page.Control is UCEditor)
					(page.Control as UCEditor).DrawLineIndicator(-1);
			}*/
			g.LogDebug("BreakPoint: Clear instruction pointers");
			foreach (PrimaryTab<UCEditor> ed in g.Editors)
				ed.Control.DrawLineIndicator(-1);
			
			// Tag the line accordingly
			g.LogDebug("BreakPoint: Draw IP");
			(this.tabMain.ActiveTabPage.Control as UCEditor).DrawLineIndicator(Convert.ToInt32(breakd[1]) - 1);

			// Retrieve watched variables
			g.LogDebug("BreakPoint: Calling WatchAskFor in DebugUC");
			this.DebugUC.WatchAskFor();
		}

		private void mnuDebug_Stop_Click(object sender, System.EventArgs e) {
			g.LogDebug("DebugStop: Enter"); 
			SwitchDebug(false);
		}

		private void mnuDebug_StepInto_Click(object sender, System.EventArgs e) {
			g.LogDebug("DebugStepInto: Enter");

			ClearIP();
			frmMain.stc_DebugQueue.Add("STEPIN\n");
		}

		private CProject.File DebugTemporaryFile(string filename) {
			g.LogDebug("DebugTemporaryFile: Enter - " + filename);

			g.LogDebug("DebugTemporaryFile: Finding temp debug directory");
			CProject.Directory parent_dir = null;
			foreach(CProject.Directory dir in g.Project.DirList) {
				if (dir.name == "Temporary Debug" && dir.parent == null) {
					parent_dir = dir;
					break;
				}
			}

			if (parent_dir == null) {
				g.LogDebug("DebugTemporaryFile: Need to create temp debug directory");
				parent_dir = new CProject.Directory("Temporary Debug", null);
				g.Project.DirList.Add(parent_dir);
			}

			g.LogDebug("DebugTemporaryFile: Spawning file object");
			CProject.File file = new CProject.File(Path.GetFileName(filename), filename, parent_dir);

			g.Project.FileList.Add(file);

			g.LogDebug("DebugTemporaryFile: Inserting into tokenizer queue");
			CProject.TokenizerQueue tq = new CProject.TokenizerQueue();
			tq.file = file;
			tq.needFile = true;
			tq.code = "";

			g.Project._Queue(tq);

			g.LogDebug("DebugTemporaryFile: Initializing project");
			this.InitProject();

			return file;
		}

		private void mnuDebug_StepOver_Click(object sender, System.EventArgs e) {
			g.LogDebug("DebugStepOver: Enter");
			ClearIP();
			frmMain.stc_DebugQueue.Add("STEPOVER\n");
		}

		private void mnuDebug_StepOut_Click(object sender, System.EventArgs e) {
			g.LogDebug("DebugStepOut: Enter");
			ClearIP();
			frmMain.stc_DebugQueue.Add("STEPOUT\n");
		}

		private void mnuDebug_Break_Click(object sender, System.EventArgs e) {
			g.LogDebug("DebugBreak: Enter");
			ClearIP();
			frmMain.stc_DebugQueue.Add("BRKNEXT\n");
		}

		private void mnuDebug_StartNoDebug_Click(object sender, System.EventArgs e) {
			try {
				g.LogDebug("DebugStartNoDebug: Attempting to start without debugger");
				Directory.SetCurrentDirectory(Path.GetDirectoryName(g.Project.DebugExe));
				System.Diagnostics.Process.Start(g.Project.DebugExe, g.Project.DebugParams);
			} catch (Exception exc) {
				g.LogDebug("DebugStartNoDebug: Failed: " + exc.Message);
				MessageBox.Show("Error starting without debugger: " + exc.Message, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void mnuEdit_Prefs_Click(object sender, System.EventArgs e) {
			g.LogDebug("EditPrefs: Enter");

			g.LogDebug("EditPrefs: Spawning config dialog");
			frmConfig fConfig = new frmConfig(g.Config);


			fConfig.ShowDialog();

			fConfig.Dispose();
			fConfig = null;

			// Reload coloring
			/*foreach(Crownwood.DotNetMagic.Controls.TabPage page in this._al_Documents) {
				if (page.Control is UCEditor)
					g.Config.LoadColorData((page.Control as UCEditor).txtEditor);
			}*/
			g.LogDebug("EditPrefs: Reload coloring in editors");
			foreach (PrimaryTab<UCEditor> ed in g.Editors)
				g.Config.LoadColorData(ed.Control.txtEditor);

			// Reload menu shortcuts
			g.LogDebug("EditPrefs: Reload menu preferences");
			LoadMenuPrefs(g.Config.menu_shortcuts);

			// Toggle timers for error markup
			g.LogDebug("EditPrefs: Reload error scan timers");
			ResetScanTimers();

			// Reset render modes
			g.LogDebug("EditPrefs: Reload UI renderers");
			InitColorPrefs();
		}

		private void mnuFile_Import_Click(object sender, System.EventArgs e) {
			CProject.Directory parent = null;

			if (this.tvProject.SelectedNode == null) {
				parent = null;
			} else {
				if (this.tvProject.SelectedNode.Tag == null)
					parent = null;
				else if (this.tvProject.SelectedNode.Tag is CProject.Directory)
					parent = (CProject.Directory)this.tvProject.SelectedNode.Tag;
				else if (this.tvProject.SelectedNode.Tag is CProject.File)
					parent = (this.tvProject.SelectedNode.Tag as CProject.File).ParentDir;
			}

			frmMassImport fImport = new frmMassImport(parent);
			fImport.ShowDialog();

			fImport.Dispose();
			fImport = null;
		}

		private void mnuProject_Import_Click(object sender, System.EventArgs e) {
			this.mnuFile_Import_Click(sender, e);
		}

		private void mnuHelp_Donate_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://torque.netmercs.net/donate_redir.php");
		}

		private void mnuHelp_Website_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://torque.netmercs.net/");
		}

		private void mnuHelp_Forums_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://torque.netmercs.net/forum");
		}

		

		private void mnuHelp_Learn_Click(object sender, System.EventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/redir_scripting.php");
		}

		private void tmrTitleMonitor_Tick(object sender, System.EventArgs e) {
			string title = "";

			if (g.Project == null) {
				if (g.DebugModeEnabled)
					title = g.Branding + " (DbgMode " + Application.ProductVersion + ")";
				else
					title = g.Branding;
			} else {
				title = g.Project.ProjectName;

				switch(g.Project.ProjectType.ToString()) {
					case "0":
						title += " [TGE] ";
						break;
					case "1":
						title += " [TSE] ";
						break;
					case "2":
						title += " [T2D/TGB] ";
						break;
				}

				if (g.DebugModeEnabled)
					title += " - " + g.Branding + " (DbgMode " + Application.ProductVersion + ")";
				else
					title += " - " + g.Branding;

				if (g.IsDebugging) {
					title += " [Debugging - ";

					if (g.IsBroken)
						title += "** BREAK **]";
					else
						title += "RUNNING]";
				}

				if (tabMain.ActiveTabPage != null) {
					if (tabMain.ActiveTabPage.Control is UCEditor)
						title += " - [" + (tabMain.ActiveTabPage.Control as UCEditor).g_curFile.SimpleName + "]";
					else if ((tabMain.ActiveTabPage.Control is UCBrowser) && (tabMain.ActiveTabPage.Control as UCBrowser).wb.Disposing == false) {
						if ((tabMain.ActiveTabPage.Control as UCBrowser).Disposing == true)
							title += " - [Web Browser (D)]";
						else if ((tabMain.ActiveTabPage.Control as UCBrowser).wb.Document == null)
							title += " - [Web Browser]";
						else
							title += " - [" + (tabMain.ActiveTabPage.Control as UCBrowser).wb.Document.Title + "]";
					}
				}
			}

			if (this.Text != title)
				this.Text = title;

			
			if (tabMain.ActiveTabPage != null && tabMain.ActiveTabPage.Control != null && (tabMain.ActiveTabPage.Control is UCEditor && (tabMain.ActiveTabPage.Control as UCEditor).txtEditor != null)) {
				// Set the statusbar text

				try {
					UCEditor editor = (UCEditor)tabMain.ActiveTabPage.Control;

					if ((this.sbpRow.Text == "N/A" || this.sbpCol.Text == "N/A") || (Convert.ToInt32(this.sbpRow.Text) != (editor.txtEditor.SelectedView.CurrentDocumentLine.Index + 1) || (Convert.ToInt32(this.sbpCol.Text) != (editor.txtEditor.SelectedView.Selection.EndPosition.Character + 1)) || (this.sbpFileName.Text != editor.g_curFile.RelativePath))) {
						this.sbpRow.Text = Convert.ToString(editor.txtEditor.SelectedView.CurrentDocumentLine.Index + 1);
						this.sbpCol.Text = Convert.ToString(editor.txtEditor.SelectedView.Selection.EndPosition.Character + 1);
						this.sbpFileName.Text = editor.g_curFile.RelativePath;

						// Switch the editor's function dropdown
						editor.DropdownSelectCurrent();

						try {
							string chara = editor.txtEditor.Document.GetSubstring(editor.txtEditor.SelectedView.Selection.EndOffset, 1);
							this.sbpTextInfo.Text = Convert.ToString((int)chara[0]) + " (0x" + Convert.ToString((int)chara[0], 16) + ")";
							this.sbpSelection.Text = Convert.ToString(Math.Abs(editor.txtEditor.SelectedView.Selection.EndOffset - editor.txtEditor.SelectedView.Selection.StartOffset)) + " char(s)";
						} catch { this.sbpTextInfo.Text = "N/A"; }
					}
				} catch {}
			} else {
				this.sbpRow.Text = "N/A";
				this.sbpCol.Text = "N/A";
				this.sbpFileName.Text = "";
				this.sbpSelection.Text = "N/A";
				this.sbpTextInfo.Text = "N/A";
			}

		}

		private void mnuEdit_Search_Click(object sender, System.EventArgs e) {
			if (GetActiveEditor() == null)
				return;

			g.LogDebug("EditSearch: Enter");
			
			if (GetActiveEditor().txtEditor.SelectedView.Selection.Length > 0) {
				// They've selected some text; put that as the default find stuff
				int length = GetActiveEditor().txtEditor.SelectedView.Selection.EndOffset - GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset;

				string seltext = "";
				if (length < 0)
					seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.EndOffset, Math.Abs(length));
				else
					seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset, length);

				g.LogDebug("EditSearch: Showing dialog with selection");
				new frmFind(GetActiveEditor(), GetActiveEditor().txtEditor, new FindReplaceOptions(), false, seltext).Show();
			} else {
				g.LogDebug("EditSearch: Showing dialog with no selection");
				new frmFind(GetActiveEditor(), GetActiveEditor().txtEditor, new FindReplaceOptions(), false, cboRecentSearches.Text).Show();
			}
		}

		private void mnuProject_Rename_Click(object sender, System.EventArgs e) {
			if (this.tvProject.SelectedNode == null)
				return;

			this.tvProject.SelectedNode.BeginEdit();
		}

		private void tvProject_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Delete)
				this.mnuProject_Del_Click(null, null);
		}

		private void mnuEdit_SearchFiles_Click(object sender, System.EventArgs e) {
			if (GetActiveEditor() == null) {
				new frmFind(tabMain, null, new FindReplaceOptions(), true, cboRecentSearches.Text).Show();
			} else {
				string seltext = GetActiveEditor().txtEditor.Document.GetSubstring(GetActiveEditor().txtEditor.SelectedView.Selection.StartOffset, GetActiveEditor().txtEditor.SelectedView.Selection.Length);
				new frmFind(tabMain, null, new FindReplaceOptions(), true, seltext).Show();
			}
			
		}

		internal void ShowFindResults(UCFindResults results) {
			g.LogDebug("ShowFindResults: Enter");

			if (this.dockFindResults != null) {
				g.LogDebug("ShowFindResults: Clearing find results dockbar");
				dockFindResults.Contents.Clear();
				ContentFindResults = null;
			}

			g.LogDebug("ShowFindResults: Spawn find results dockbar");
			dockFindResults = new DockingManager(this, ((g.Config.ColorPref == ColorPref.Standard) ? VisualStyle.IDE2005 : VisualStyle.Office2003));

			this.dockFindResults.InnerControl = this.tabMain;
			this.dockFindResults.OuterControl = this.sbMain;

			g.LogDebug("ShowFindResults: Populating results");
			ContentFindResults = dockFindResults.Contents.Add(results, "Find Results", this.ilProject, 10);

			g.LogDebug("ShowFindResults: Binding dockbar events");
			dockFindResults.ContentHidden += new Crownwood.DotNetMagic.Docking.DockingManager.ContentHandler(dockFindResults_ContentHidden);
		
			dockFindResults.AddContentWithState(ContentFindResults, State.DockBottom);

			// Restore state
			if (g.Config.FindResultsBarData != null) {
				try {
					g.LogDebug("ShowFindResults: Loading previous dockbar config");
					dockFindResults.LoadConfigFromArray(g.Config.FindResultsBarData);
				} catch {}
			}

			// Show all the contents
			dockFindResults.ShowAllContents();
		}

		private void dockFindResults_ContentHidden(Content c, EventArgs cea) {
			g.LogDebug("CloseFindResults: Enter");

			// Save state
			g.LogDebug("CloseFindResults: Save dockbar data");
			g.Config.FindResultsBarData = dockFindResults.SaveConfigToArray();

			// Dispose objects
			g.LogDebug("CloseFindResults: Dispose");
			(c.Control as UCFindResults).Dispose();
			ContentFindResults = null;
			dockFindResults.HideAllContents();
			dockFindResults.Dispose();

			dockFindResults = null;
		}

		private void mnuWindow_NewBrowser_Click(object sender, System.EventArgs e) {
			SpawnBrowser("about:blank", false);
		}

		private void mnuAbout_Update_Click(object sender, System.EventArgs e) {
			DialogResult result = MessageBox.Show(this, "Running the auto-update wizard will close TorqueDev.  Are you sure you want to run the wizard now?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			System.Diagnostics.Process.Start(Application.StartupPath + "\\TSDevUpdater.exe");
			
			this.Close();
			return;
		}

		private void mnuEdit_IC_ScanFile_Click(object sender, System.EventArgs e) {
			if (GetActiveEditor() == null)
				return;

			// Clear the current list first
			this.ErrorUC.lvErrors.Items.Clear();

			ErrorCollection errcoll = ScanFile(GetActiveEditor().g_curFile);
			ScanBuildTree(GetActiveEditor().g_curFile, errcoll);

			GetActiveEditor().ErrorRender();
		}

		internal ErrorCollection ScanFile(CProject.File file) {
			g.LogDebug("ScanFile: Enter - " + file.RelativePath);

			g.LogDebug("ScanFile: Reset current directory");
			Directory.SetCurrentDirectory(g.Project.ProjectPath);

			g.LogDebug("ScanFile: Getting full path");
			if (!File.Exists(Path.GetFullPath(file.RelativePath))) {
				g.LogDebug("ScanFile: Failed");
				return new ErrorCollection(file);
			}

			g.LogDebug("ScanFile: Initializing syntax scanner and parser");
			Scanner scanner = new Scanner(Path.GetFullPath(file.RelativePath));
			Parser parser = new Parser(scanner);

			g.LogDebug("ScanFile: Parsing for errors");
			parser.Parse();
			
			// Get a new instance of the error collection
			ErrorCollection coll = (ErrorCollection)parser.errors.ErrCollection.Clone();

			// Write this to the error hashtable
			g.LogDebug("ScanFile: Populating error UC");
			if (g.Project.ErrorList.Contains(file))
				g.Project.ErrorList[file] = coll;
			else
				g.Project.ErrorList.Add(file, coll);

			scanner = null;
			parser = null;

            return coll;
		}

		public void ScanBuildTree() {
			// Build the scanner tree for each file
			this.ErrorUC.lvErrors.Items.Clear();

			IDictionaryEnumerator enumer = g.Project.ErrorList.GetEnumerator();

			while(enumer.MoveNext())
				ScanBuildTree((CProject.File)enumer.Key, (ErrorCollection)enumer.Value);
		}

		internal void ScanBuildTree(CProject.File file, ErrorCollection coll) {
			// Build the scanner tree
			ListView lv = this.ErrorUC.lvErrors;

			if (coll.Count == 0) {
				// No errors
				return;
				/*
				ListViewItem lvi = new ListViewItem("No errors detected.");
				lvi.SubItems.Add(file.RelativePath);

				lv.Items.Add(lvi);
				return;*/
			}

			// Suspend the draw
			lv.BeginUpdate();

			// Enumerate the errors
			foreach(Error err in coll) {
				ListViewItem item = new ListViewItem(err.Text);
				item.SubItems.Add(file.RelativePath);
				item.SubItems.Add(err.Line + " / " + err.Column);

				item.Tag = file;

				lv.Items.Add(item);
			}

			// Let the component draw again
			lv.EndUpdate();

			// Reset the thingy
			this.ErrorUC.lvErrors = lv;

			// Show the window in case it's hidden
			dockMgr.ShowContent(ContentErrors);
		}

		public void ResetScanTimers() {
			//foreach(Crownwood.DotNetMagic.Controls.TabPage page in this._al_Documents)
			//	(page.Control as UCEditor).tmrErrorMonitor.Enabled = g.Config.b_Err_WhileTyping;
			foreach (PrimaryTab<UCEditor> ed in g.Editors) {
				ed.Control.tmrErrorMonitor.Enabled = g.Config.b_Err_WhileTyping;
			}
		}

		private void mnuEdit_IC_ScanProj_Click(object sender, System.EventArgs e) {
			ScanProject();
		}

		public bool ScanProject() {
			bool errors = false;

			this.ErrorUC.lvErrors.Items.Clear();
			
			foreach(CProject.File file in g.Project.FileList) {
				ErrorCollection coll = ScanFile(file);

				if (coll.Count > 0)
					errors = true;
			}

			ScanBuildTree();

			return errors;
		}

		private void mnuFile_RescanProject_Click(object sender, System.EventArgs e) {
			// Re-import the entire project
			ImportDirectory(g.Project.ProjectPath, null);

			// Re-init the project tree
			InitProject();

			// Re-scan the objects in the project
			g.Project.RescanProjectObjects();

			// Re-init the projects view
			InitExplorer();
		}

		private void ImportDirectory(string dir, CProject.Directory parent_dir) {
			g.LogDebug("ImportDirectory: Enter - " + dir);
			string [] subdirs = Directory.GetDirectories(dir, "*");

			string file_exts = "*.cs;*.t2d;*.gui;*.mis";

			g.LogDebug("ImportDirectory: Scanning files");
			foreach(string file_entry in file_exts.ToLower().Replace(" ", "").Split(';')) {
				foreach(string file in Directory.GetFiles(dir, file_entry)) {
					if ((File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden)
						continue;

					g.LogDebug("ImportDirectory: Add existing file: " + file);
					g.Main.AddExistingFile(file, parent_dir);
				}
			}

			g.LogDebug("ImportDirectory: Scanning and adding subdirectories");
			foreach(string subdir in subdirs) {
				DirectoryInfo di = new DirectoryInfo(subdir);

				if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					continue;

				// See if there's a node in this parent that is
				// already called this
				bool dontadd = true;
				CProject.Directory curdir = null;
				CProject.Directory d_subdir = null;

				foreach(CProject.Directory dirx in g.Project.DirList) {
					if (dirx.parent == parent_dir) {
						if (dirx.name.ToLower() == Path.GetFileName(subdir).ToLower()) {
							curdir = dirx;
							dontadd = true;
							goto dontadd;
						}
					}
				}

				string[] dir_name = subdir.Split('\\');
				d_subdir = new CProject.Directory(dir_name[dir_name.Length - 1], parent_dir, false);
				g.Project.DirList.Add(d_subdir);
				
			dontadd:
				if (dontadd) {
					ImportDirectory(subdir, curdir);
				} else {
					ImportDirectory(subdir, d_subdir);
				}
			}
				
		}

		private void tvProject_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				TreeNode node = this.tvProject.GetNodeAt(e.X, e.Y);

				if (node == null)
					return;

				this.tvProject.SelectedNode = node;
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
			g.LogDebug("ThreadException: Enter");
			g.LogDebug("\r\n\r\n\r\n*** STOP ERROR ***");
			g.LogDebug(e.Exception.Message);
			g.LogDebug(e.Exception.StackTrace);

			g.LogDebug("ThreadException: Showing exception report form");
			frmException fExc = new frmException(e.Exception);
			fExc.ShowDialog();

			g.LogDebug("ThreadException: Killing process!");
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		private void tbbNewProject_Click(object sender, EventArgs e) {
			this.mnuFile_NewProject_Click(sender, e);
		}

		private void tbbOpenProject_Click(object sender, EventArgs e) {
			this.mnuFile_OpenProj_Click(sender, e);
		}

		private void tbbNewItem_Click(object sender, EventArgs e) {
			this.mnuFile_AddNew_Click(sender, e);
		}

		private void tbbExistingItem_Click(object sender, EventArgs e) {
			this.mnuFile_AddExist_Click(sender, e);
		}

		private void tbbSave_Click(object sender, EventArgs e) {
			this.mnuFile_SaveFile_Click(sender, e);
		}

		private void tbbSaveAs_Click(object sender, EventArgs e) {
			this.mnuFile_SaveAs_Click(sender, e);
		}

		private void tbbSaveAll_Click(object sender, EventArgs e) {
			this.mnuFile_SaveAll_Click(sender, e);
		}

		private void tbbPrint_Click(object sender, EventArgs e) {
			this.mnuFile_Print_Click(sender, e);
		}

		private void mnuFile_Print_Click(object sender, EventArgs e) {
			try {
				if (this.GetActiveEditor() != null)
					this.GetActiveEditor().Print();
			} catch (Exception exc) {
				MessageBox.Show("Error displaying print dialog:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void mnuFile_PrintPreview_Click(object sender, EventArgs e) {
			try {
				if (this.GetActiveEditor() != null)
					this.GetActiveEditor().PrintPreview();
			} catch (Exception exc) {
				MessageBox.Show("Error displaying print dialog:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void tbbPrintPreview_Click(object sender, EventArgs e) {
			this.mnuFile_PrintPreview_Click(sender, e);
		}

        private void tbbCut_Click(object sender, EventArgs e) {
            this.mnuEdit_Cut_Click(sender, e);
        }

        private void tbbCopy_Click(object sender, EventArgs e) {
            this.mnuEdit_Copy_Click(sender, e);
        }

        private void tbbPaste_Click(object sender, EventArgs e) {
            this.mnuEdit_Paste_Click(sender, e);
        }

        private void tbbDelete_Click(object sender, EventArgs e) {
            if (this.GetActiveEditor() != null)
                this.GetActiveEditor().txtEditor.SelectedView.DeleteToNextWord();
        }

        private void tbbUndo_Click(object sender, EventArgs e) {
            this.mnuEdit_Undo_Click(sender, e);
        }

        private void tbbRedo_Click(object sender, EventArgs e) {
            this.mnuEdit_Redo_Click(sender, e);
        }

        private void tbbSearch_Click(object sender, EventArgs e) {
            this.mnuEdit_Search_Click(sender, e);
        }

        private void tbbFind_Click(object sender, EventArgs e) {
            this.mnuEdit_SearchReplace_Click(sender, e);
        }

        private void tbbFindFiles_Click(object sender, EventArgs e) {
            this.mnuEdit_SearchFiles_Click(sender, e);
        }

        private void tbbMarkAdd_Click(object sender, EventArgs e) {
            this.mnuEdit_BKAdd_Click(sender, e);
        }

        private void tbbMarkUp_Click(object sender, EventArgs e) {
            this.mnuEdit_BKPrev_Click(sender, e);
        }

        private void tbbMarkDown_Click(object sender, EventArgs e) {
            this.mnuEdit_BKNext_Click(sender, e);
        }

        private void tbbMarkDel_Click(object sender, EventArgs e) {
            this.mnuEdit_BKDel_Click(sender, e);
        }

        private void tbbDebugStart_Click(object sender, EventArgs e) {
            this.mnuDebug_Start_Click(sender, e);
        }

        private void tbbDebugRun_Click(object sender, EventArgs e) {
            this.mnuDebug_StartNoDebug_Click(sender, e);
        }

        private void tbbDebugBreak_Click(object sender, EventArgs e) {
            this.mnuDebug_Break_Click(sender, e);
        }

        private void tbbDebugStop_Click(object sender, EventArgs e) {
            this.mnuDebug_Stop_Click(sender, e);
        }

        private void tbbDebugStepInto_Click(object sender, EventArgs e) {
            this.mnuDebug_StepInto_Click(sender, e);
        }

        private void tbbDebugStepOver_Click(object sender, EventArgs e) {
            this.mnuDebug_StepOver_Click(sender, e);
        }

        private void tbbDebugStepOut_Click(object sender, EventArgs e) {
            this.mnuDebug_StepOut_Click(sender, e);
        }

		private void mnuDebug_Wizard_Click(object sender, EventArgs e) {
			g.LogDebug("DebugWizard: Enter; Showing debug wizard");
			frmDebugWizard fDebugWiz = new frmDebugWizard();
			fDebugWiz.ShowDialog();

			fDebugWiz.Dispose();
			fDebugWiz = null;
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Tab && (e.Modifiers == Keys.Control)) {
				if (tabMain.ActiveTabPage == null)
					return;

				frmFastTab fFastTab = new frmFastTab(false, false);
				fFastTab.Show(this);

				//fFastTab.Dispose();
				//fFastTab = null;
				e.SuppressKeyPress = true;
			} else if (e.KeyCode == Keys.Tab && (e.Modifiers == (Keys.Control | Keys.Shift))) {
				if (tabMain.ActiveTabPage == null)
					return;

				frmFastTab fFastTab = new frmFastTab(false, true);
				fFastTab.Show(this);

				//fFastTab.Dispose();
				//fFastTab = null;
				e.SuppressKeyPress = true;
			} else {
				e.SuppressKeyPress = false;
			}
		}

		private void tabMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			e.IsInputKey = true;
		}

		private void mnuAbout_License_Click(object sender, EventArgs e) {
			frmLicManager fLicManager = new frmLicManager();
			fLicManager.ShowDialog();

			fLicManager.Dispose();
			fLicManager = null;

		}

		public void RefreshWorkspaceWindows() {
			// Enumerate all the custom windows that the user may want to show
			// (first delete them)

			ArrayList todelete = new ArrayList();
			foreach (ToolStripItem menu in mnuWindow_WorkspaceWindows.DropDownItems) {
				if (menu is ToolStripMenuItem) {
					if (menu.Tag != null && menu.Tag is WorkspaceWindow)
						todelete.Add(menu);
				}
			}

			foreach (ToolStripMenuItem menu in todelete)
				mnuWindow_WorkspaceWindows.DropDownItems.Remove(menu);

			// Sort the collection of windows by title
			g.WorkspaceWindows.Sort();

			// Re-add the items
			foreach (WorkspaceWindow ww in g.WorkspaceWindows) {
				ToolStripMenuItem newtsmi = new ToolStripMenuItem(ww.CaptionText, ww.WindowIcon, new EventHandler(WorkspaceWindowEventHandler));
				newtsmi.Tag = ww;
				newtsmi.Name = "mnuWindow_Workspaces_" + ww.CaptionText.Replace(" ", "_");

				mnuWindow_WorkspaceWindows.DropDownItems.Add(newtsmi);
			}
		}

		private void WorkspaceWindowEventHandler(object sender, EventArgs e) {
			((sender as ToolStripMenuItem).Tag as WorkspaceWindow).Content.BringToFront();
		}

		private void ctmProject_Opening(object sender, CancelEventArgs e) {
			this.mnuProject_Properties.Enabled = !(g.Project == null);
			this.mnuCreateFolder.Enabled = !(g.Project == null);
			this.mnuProject_AddExist.Enabled = !(g.Project == null);
			this.mnuProject_AddNew.Enabled = !(g.Project == null);
			this.mnuProject_Del.Enabled = !(g.Project == null);
			this.mnuProject_Import.Enabled = !(g.Project == null);

			this.mnuSCC.Enabled = g.SourceControlEnabled;

		}

		private void __LoadFile(string filename) {
			// A call to load a file from another instance of the IDE
			g.LogDebug("__LoadFile: Enter");
			if (!File.Exists(filename)) {
				g.LogDebug("__LoadFile: File not found; exit");
				return;
			}

			bool isText = false;
			if (Path.GetExtension(filename) == ".cs" ||
				Path.GetExtension(filename) == ".gui" ||
				Path.GetExtension(filename) == ".t2d" ||
				Path.GetExtension(filename) == ".mis")
				isText = false;
			else
				isText = true;

			g.LogDebug("__LoadFile: Treating as text: " + isText.ToString());

			g.LogDebug("__LoadFile: Spawning file object");
			CProject.File file = new CProject.File(
				Path.GetFileName(filename),
				filename,
				true,
				isText,
				null);

			g.LogDebug("__LoadFile: Opening file");
			OpenFile(file, 0, false);
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == 0x4A) {
				// WM_COPYDATA
				g.LogDebug("WndProc: Received WM_COPYDATA");
				COPYDATASTRUCT cds = new COPYDATASTRUCT();
				cds = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));

				if (cds.cbData > 0) {
					byte[] data = new byte[cds.cbData];
					Marshal.Copy(cds.lpData, data, 0, cds.cbData);

					string sData = System.Text.ASCIIEncoding.ASCII.GetString(data);
					if (sData.StartsWith("FILE|")) {
						g.LogDebug("WndProc: Spawning __LoadFile");
						__LoadFile(sData.Split('|')[1]);
					}

					m.Result = (IntPtr)1;
				}
			}
			
			base.WndProc(ref m);
		}

		private void mnuFile_Open_File_Click(object sender, EventArgs e) {
			// Open a foreign text (or code) file
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.RestoreDirectory = true;
			ofd.Filter = "TorqueScript Files (*.cs; *.t2d; *.gui; *.mis)|*.cs;*.t2d;*.gui;*.mis|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			ofd.Title = "Open File";
			ofd.CheckFileExists = true;

			DialogResult result = ofd.ShowDialog(this);

			if (result == DialogResult.Cancel)
				return;

			// Create a new instance of the file they're opening
			CProject.File file = new CProject.File(Path.GetFileName(ofd.FileName),
				ofd.FileName,
				true,
				((Path.GetExtension(ofd.FileName) == ".cs" ||
				  Path.GetExtension(ofd.FileName) == ".gui" ||
				  Path.GetExtension(ofd.FileName) == ".t2d" ||
				  Path.GetExtension(ofd.FileName) == ".mis") ? false : true),
				null);

			OpenFile(file, 0, false);
		}

		private void mnuFile_Open_ProjectFromScc_Click(object sender, EventArgs e) {

		}

		private void tvProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			if (tvProject.SelectedNode == null)
				return;

			if (tvProject.SelectedNode.Tag is CProject.File) {
				OpenFile(((CProject.File)tvProject.SelectedNode.Tag), 0, true);
			}
		}

		private void mnuEdit_BKClearAll_Click(object sender, EventArgs e) {
			GetActiveEditor().BookmarkClearAll();
		}
	}
}
