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
using System.Collections;
using TSDev.Plugins;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace TSDev
{
	internal static class g
	{
		public static CProject Project = null;
		public static string ProjectFN = "";

		public static CConfig Config = null;
		public static LicenseManager License = null;

		public static bool SourceControlEnabled = false;
		public static ICodeweaverSCCPlugin SourceControlPlugin = null;

		public static string Branding = "netMercs TorqueDev";

		private static StreamWriter DebugFile = null;
		private static DateTime DebugProgramStart;
		public static bool DebugModeEnabled = false;

		public static bool IsDebugging = false;
		public static bool IsBroken = false;

		public static frmMain Main = null;

		public static ArrayList CustomEditorContextMenus = new ArrayList();

		public static RegistryKey AppRegistry = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("netMercs Group LLC").CreateSubKey("TorqueDev IDE");

		public static List<CWPlugin> Plugins = new List<CWPlugin>();
		public static IDEControl IDEControl = new IDEControl();

		public static PrimaryTabCollection<UCEditor> Editors = new PrimaryTabCollection<UCEditor>();
		public static PrimaryTabCollection<UCBrowser> Browsers = new PrimaryTabCollection<UCBrowser>();
		public static PrimaryTabCollection<Control> OtherTabs = new PrimaryTabCollection<Control>();

		public static WorkspaceWindowCollection WorkspaceWindows = new WorkspaceWindowCollection();
		public static EditorTriggerCollection EditorTriggers = new EditorTriggerCollection();

		public static CustomConfigTabCollection CustomConfigTabs = new CustomConfigTabCollection();
		public static CustomConfigTabCollection CustomSccConfigTabs = new CustomConfigTabCollection();

		private static ArrayList SortedTabs = new ArrayList();

		public static void LogDebug(string text) {
			if (!DebugModeEnabled)
				return;

			DebugFile.WriteLine(((TimeSpan)(DateTime.Now -DebugProgramStart)).ToString() + "   -->   " + text);
			DebugFile.Flush();
		}

		public static void InitDebug() {
			if (DebugModeEnabled)
				return;

			DebugProgramStart = DateTime.Now;

			if (!Directory.Exists(Application.StartupPath + "\\debug_logs"))
				Directory.CreateDirectory(Application.StartupPath + "\\debug_logs");

			// Create a new debug log file
			DebugFile = new StreamWriter(Application.StartupPath + "\\debug_logs\\" +
				DateTime.Now.ToString("yyyy-MM-dd hh mm ss") + ".txt");

			DebugModeEnabled = true;
		}

		public static ArrayList SortedTabList {
			get {
				g.CleanSortedTabs();
				g.SortSortedTabs();
				return SortedTabs;
			}
		}

		public static void GlobalMessageBox(string text, string title, MessageBoxButtons buttons, MessageBoxIcon icon) {
			g.Main.InvokeThreadedMessageBox(text, title, buttons, icon);
		}

        public static void Assert(bool conditional, string failure_message) {
            if (conditional)
                return;

            // Assertion failure
            StackTrace st = new StackTrace(1, true);
            System.Windows.Forms.MessageBox.Show("ASSERTION FAILURE:\n\n" + failure_message + "\n\n\n----\n" +
                st.ToString(), "Assertion Failure", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop,
                System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.ServiceNotification);

            // Terminate the process
            System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		public static void PluginException(Exception exc, CWPlugin plugin) {
			DialogResult result = MessageBox.Show(
							"PLUGIN EXCEPTION: " + exc.Message + "\n\n" + exc.StackTrace +
							"\n\n-----\n" + plugin.Guid + "\n" + plugin.Name + "\n\n" +
							"Do you want to ignore this error?  If NO, this program will terminate, and you will lose all unsaved data. If YES, " +
							"the program may become unstable.", "PLUGIN ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Stop
						);

			if (result == DialogResult.No)
				System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		#region Sorted Tabs
		public static void MakeSortedTabActive(Crownwood.DotNetMagic.Controls.TabPage page) {
			foreach (SortedTab tab in SortedTabs) {
				if (tab.Page == page) {
					tab.Index = -1;
					break;
				}
			}

			SortSortedTabs();
		}
		private static void CleanSortedTabs() {
			ArrayList keeptabs = new ArrayList();
			foreach (SortedTab tab in SortedTabs) {
				if (g.Editors.FindByTab(tab.Page) == null && g.Browsers.FindByTab(tab.Page) == null && g.OtherTabs.FindByTab(tab.Page) == null)
					continue;

				keeptabs.Add(tab);
			}

			// Sort the keep tabs
			keeptabs.Sort();

			// Re-assign ourselves
			SortedTabs = keeptabs;
		}

		private static void SortSortedTabs() {
			// Sort the SortedTabs listing
			SortedTabs.Sort();

			int i = 0;
			foreach (SortedTab tab in SortedTabs) {
				tab.Index = i;
				i++;
			}
		}
		#endregion

		#region Tab Stuff
		public static object[] GetAllTabObjects() {
			object[] output = new object[Editors.Count + Browsers.Count + OtherTabs.Count];

			int i = 0;

			foreach (PrimaryTab<UCEditor> t in Editors)
				output[i++] = t;

			foreach (PrimaryTab<UCBrowser> t in Browsers)
				output[i++] = t;

			foreach (PrimaryTab<object> t in OtherTabs)
				output[i++] = t;

			return output;
		}
		#endregion
	}

	internal interface RenderSet {
		void SetRenderMode(ColorPref cp);
	}

	internal class SortedTab : IComparable {
		public SortedTab(Crownwood.DotNetMagic.Controls.TabPage page, int Index) {
			this.Page = page;
			this.Index = Index;
		}

		public Crownwood.DotNetMagic.Controls.TabPage Page = null;
		public int Index = 0;

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj is SortedTab) {
				if (this.Index > (obj as SortedTab).Index)
					return 1;
				else if (this.Index < (obj as SortedTab).Index)
					return -1;
				else
					return 0;
			}

			return 0;
		}

		#endregion
	}

	#region Workspace Windows
	internal class WorkspaceWindow : IComparable {

		public WorkspaceWindow(Crownwood.DotNetMagic.Docking.Content Content, string captext, Image winicon) {
			this.Content = Content;
			CaptionText = captext;
			WindowIcon = winicon;
		}
		
		public Crownwood.DotNetMagic.Docking.Content Content = null;
		public string CaptionText = "";
		public Image WindowIcon = null;

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj is WorkspaceWindow) {
				return String.Compare(CaptionText, (obj as WorkspaceWindow).CaptionText, true);
			} else {
				return 0;
			}
		}

		#endregion
	}

	internal class WorkspaceWindowCollection : CollectionBase {
		public virtual void Add(WorkspaceWindow Content) {
			if (!this.List.Contains(Content)) {
				this.List.Add(Content);
				g.Main.RefreshWorkspaceWindows();
			}
		}

		public virtual void Remove(WorkspaceWindow Content) {
			if (this.List.Contains(Content)) {
				this.List.Remove(Content);
				g.Main.RefreshWorkspaceWindows();
			}
		}

		public WorkspaceWindow this[int Index] {
			get { return (WorkspaceWindow)this.List[Index]; g.Main.RefreshWorkspaceWindows(); }
			set { this.List[Index] = value; }
		}

		public void Sort() {
			this.InnerList.Sort();
		}

		public virtual bool Contains(WorkspaceWindow Content) {
			return this.List.Contains(Content);
		}

		public WorkspaceWindow FindWorkspaceWindow(Control WindowControl) {
			foreach (WorkspaceWindow ww in this.List) {
				if (ww.Content.Control == WindowControl)
					return ww;
			}

			return null;
		}

		public bool Contains(Control WindowControl) {
			foreach (WorkspaceWindow ww in this.List) {
				if (ww.Content.Control == WindowControl)
					return true;
			}

			return false;
		}
	}
	#endregion

	#region Tab Collection Generics
	internal class PrimaryTabCollection<T> : CollectionBase {
		public PrimaryTabCollection() { }	// Blank constructor

		public virtual void Add(PrimaryTab<T> entry) {
			if (!this.List.Contains(entry)) {
				// Handle addition to sorted tabs listing
				g.SortedTabList.Add(new SortedTab(entry.page, 37000));
				this.List.Add(entry);
			}
		}

		public virtual void Remove(PrimaryTab<T> entry) {
			if (this.List.Contains(entry)) {
				this.List.Remove(entry);
			}
		}

		public PrimaryTab<T> FindByTab(Crownwood.DotNetMagic.Controls.TabPage tp) {

			foreach (PrimaryTab<T> tab in this.List) {
				if (tab.page == tp)
					return tab;
			}

			return null;
		}

		public PrimaryTab<T> FindByControl(T Control) {
			foreach (PrimaryTab<T> tab in this.List) {
				if (tab.Control.Equals(Control))
					return tab;
			}

			return null;
		}

		public virtual PrimaryTab<T> this[int Index] {
			get {
				return (PrimaryTab<T>)this.List[Index];
			}
		}
	}

	internal sealed class PrimaryTab<T> {
		public PrimaryTab(Crownwood.DotNetMagic.Controls.TabPage NewPage, T Control) {
			this._control = Control;
			this.page = NewPage;
		}

		public PrimaryTab(Crownwood.DotNetMagic.Controls.TabPage NewPage, T Control, ICodeweaverPlugin plugin) {
			this._control = Control;
			this.page = NewPage;
			this.plugin = plugin;
		}

		private T _control = default(T);
		public Crownwood.DotNetMagic.Controls.TabPage page = null;
		public ICodeweaverPlugin plugin = null;
		public float index = 0;

		public T Control {
			get {
				return _control;
			}
		}
	}
	#endregion

	#region Editor Trigger Collections
	internal class EditorTrigger {
		public EditorTrigger(ICodeweaverPlugin plugin, Keys keys, Keys mods, string name, TriggerStateWhen when) {
			this.plugin = plugin;
			TriggerKeys = keys;
			TriggerModifiers = mods;
			TriggerName = name;
			TriggerWhen = when;
		}

		public ICodeweaverPlugin plugin;
		public Keys TriggerKeys;
		public Keys TriggerModifiers;
		public string TriggerName;
		public TriggerStateWhen TriggerWhen;
	}

	internal class EditorTriggerCollection : CollectionBase {
		public virtual void Add(EditorTrigger trigger) {
			this.List.Add(trigger);
		}

		public virtual void Remove(string trigger_key) {
			foreach (EditorTrigger trigger in this.List) {
				if (trigger.TriggerName == trigger_key) {
					this.List.Remove(trigger);
					return;
				}
			}
		}

		public EditorTrigger this[int Index] {
			get { return (EditorTrigger)this.List[Index]; }
			set { this.List[Index] = value; }
		}

		public bool HasKey(string trigger_key) {
			foreach (EditorTrigger trigger in this.List) {
				if (trigger.TriggerName == trigger_key)
					return true;
			}

			return false;
		}
	}
	#endregion

	#region Custom Configuration Tabs
	internal class CustomConfigTab {
		public CustomConfigTab(ICodeweaverPlugin Plugin, Control TabControl, string TabText) {
			this.TabControl = TabControl;
			this.TabText = TabText;
			this.Plugin = Plugin;
		}

		public Control TabControl = null;
		public string TabText = "";
		public ICodeweaverPlugin Plugin = null;
	}

	internal class CustomConfigTabCollection : CollectionBase {
		public virtual void Add(CustomConfigTab Tab) {
			this.List.Add(Tab);
		}

		public virtual void Remove(CustomConfigTab Tab) {
			this.List.Remove(Tab);
		}

		public CustomConfigTab FindByControl(Control Tab) {
			foreach (CustomConfigTab cct in this.List) {
				if (cct.TabControl == Tab)
					return cct;
			}

			return null;
		}
	}
	#endregion

	#region Plugin Object
	public class CWPlugin {
		private string _name;
		private Version _version;
		private string _description;
		private string _author;
		private string _copyright;
		private Version _valid_until;
		private Guid _guid;
		private ICodeweaverPlugin _plugin;

		public CWPlugin(string name, Version version, string description, string author, string copyright, Version validUntil, Guid guid, ICodeweaverPlugin plugin) {
			this._name = name;
			this._version = version;
			this._description = description;
			this._author = author;
			this._copyright = copyright;
			this._valid_until = validUntil;
			this._guid = guid;
			this._plugin = plugin;
		}

		public string Name {
			get { return _name; }
		}

		public Version Version {
			get { return _version; }
		}

		public string Description {
			get { return _description; }
		}

		public string Author {
			get { return _author; }
		}

		public string Copyright {
			get { return _copyright; }
		}

		public Version ValidUntil {
			get { return _valid_until; }
		}

		public Guid Guid {
			get { return _guid; }
		}

		public ICodeweaverPlugin Plugin {
			get { return _plugin; }
		}
	}
	#endregion
}
