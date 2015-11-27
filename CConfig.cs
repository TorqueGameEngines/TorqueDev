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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TSDev
{
	[Serializable]
	public enum ColorPref {
		Professional,
		Standard
	}

	[Serializable]
	internal class CConfig : ICloneable
	{
		public const int CONFIGURATION_VERSION = 5;

		public CConfig() {
			
			// Enumerate through the menu items on the main form and init the config with them
			foreach (System.Windows.Forms.ToolStripItem item in g.Main.mnuMain.Items) {
				AddLocalMenus(item, this.menu_shortcuts);
			}

			/*foreach (System.Windows.Forms.ToolStripItem item in g.Main.mnuMain.Items) {
				MenuShortcut ms = new MenuShortcut(item.Text, Keys.None, null);
				ms.MenuConfigurable = false;

				AddLocalMenus(item, ms);
			}*/

			colors.Add("ReservedWordStyle", new HighlightEntry(Color.Blue));
			colors.Add("FunctionStyle", new HighlightEntry(Color.Red));
			colors.Add("LocalVariableStyle", new HighlightEntry(Color.Teal));
			colors.Add("GlobalVariableStyle", new HighlightEntry(Color.DarkRed));
			colors.Add("OperatorStyle", new HighlightEntry(Color.Gray));
			colors.Add("NumberStyle", new HighlightEntry(Color.Olive));
			colors.Add("StringDelimiterStyle", new HighlightEntry(Color.Magenta));
			colors.Add("StringDefaultStyle", new HighlightEntry(Color.Magenta));
			colors.Add("RegionStyle", new HighlightEntry(Color.Black, Color.LightGray));
			colors.Add("TaggedStringDelimiterStyle", new HighlightEntry(Color.Purple));
			colors.Add("TaggedStringDefaultStyle", new HighlightEntry(Color.Purple));
			colors.Add("CommentDelimiterStyle", new HighlightEntry(Color.DarkGreen));
			colors.Add("CommentDefaultStyle", new HighlightEntry(Color.DarkGreen));
			colors.Add("SpecialCommentDelimiterStyle", new HighlightEntry(Color.DarkGray));
			colors.Add("SpecialCommentDefaultStyle", new HighlightEntry(Color.DarkGray));
			colors.Add("MemberTokenStyle", new HighlightEntry(Color.Blue));
			colors.Add("DefaultStyle", new HighlightEntry(Color.Black, Color.White));
			colors.Add("_LineNumbers", new HighlightEntry(Color.Gray, Color.White));
			colors.Add("_LineNumbersBorder", new HighlightEntry(Color.Teal));
			colors.Add("_IndentGuidelines", new HighlightEntry(Color.LightGray));
			
		}

		// Menu shortcuts
		[Serializable]
		internal class MenuShortcut {
			public MenuShortcut(string MenuText, Keys MenuShortcut, MenuShortcut MenuParent) {
				this.MenuText = MenuText;
				this.MenuShortcutKey = MenuShortcut;
				this.MenuParent = MenuParent;
			}

			public override string ToString() {
				// Recursive pathing of the menu
				if (this.MenuParent == null)
					return MenuText;

				return MenuParent.ToString() + " -> " + MenuText;
			}


			public string MenuText = "";
			public Keys MenuShortcutKey = Keys.None;
			public MenuShortcut MenuParent = null;

			public bool MenuConfigurable = true;
		}

		[Serializable]
		internal class MenuShortcutCollection : CollectionBase {
			public MenuShortcutCollection() { }

			public virtual void Add(MenuShortcut menu) {
				if (!this.List.Contains(menu))
					this.List.Add(menu);
			}

			public virtual void Remove(MenuShortcut menu) {
				if (this.List.Contains(menu))
					this.List.Remove(menu);
			}

			public virtual MenuShortcut this[int Index] {
				get {
					return (MenuShortcut)this.List[Index];
				}
				set {
					this.List[Index] = value;
				}
			}
		}

		// Class declarations
		[Serializable]
		internal class DebugExecEntry {
			public DebugExecEntry(string path, string parameters) {
				this.path = path;
				this.parameters = parameters;
			}

			public string path = "";
			public string parameters = "";
		}

		[Serializable]
		internal class DebugExecEntryCollection : CollectionBase {
			public virtual void Add(DebugExecEntry entry) {
				this.List.Add(entry);
			}

			public virtual void Remove(DebugExecEntry entry) {
				this.List.Remove(entry);
			}

			public virtual new void RemoveAt(int index) {
				this.List.RemoveAt(index);
			}

			public virtual DebugExecEntry this[int Index] {
				get {
					return (DebugExecEntry)this.List[Index];
				}

				set {
					this.List[Index] = value;
				}
			}

			public virtual bool Contains(DebugExecEntry entry) {
				if (this.List.Contains(entry))
					return true;
				else
					return false;
			}

			public static DebugExecEntryCollection operator &(DebugExecEntryCollection a, DebugExecEntryCollection b) {
				DebugExecEntryCollection c = (DebugExecEntryCollection)b.MemberwiseClone();
				
				foreach(DebugExecEntry entry in a) {
					if (!b.List.Contains(entry))
						c.List.Add(a);
				}

				return c;
			}
		}

		// Static members
		public static string GlobalUserpath = "";

		public static void ClearActivation(CConfig config) {
			// Clears activation info on the passed config
			config.ActivationEmail = "";
			config.ActivationHasT2D = false;
			config.ActivationHasTGE = false;
			config.ActivationHasTSE = false;
			config.ActivationLastHardkey = "";
			config.ActivationLocal = false;
			config.ActivationNextCheck = 0;
			config.ActivationResponse = "";
			config.ActivationUser = "";
			config.bIsActivated = false;
		}

		public static string GetHardKey() {
			// Get a hardware key hash
			string tohash = "";

			// For now, just enumerate all hard disks in the system
			/*try {
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

				foreach(ManagementObject obj in searcher.Get()) {
					tohash += ((obj["SerialNumber"] == null) ? "NULL//" : obj["SerialNumber"]);
				}
			} catch (System.Management.ManagementException exc) {*/
				// For compatability reasons, it appears on some system an error occurs and we have
				// to resort to getting the NIC's MAC address
			try {
				ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
				
				foreach(ManagementObject mo in mc.GetInstances()) {
					try {
						if (mo["MacAddress"] != null) {
							tohash += mo["MacAddress"].ToString();
							break;
						}
					} catch {}
				}
			} catch { tohash = ""; }

			/*} catch { tohash = ""; }*/

			if (tohash == "") {
				tohash = "//NULLKEY//___?__??___";
			}	

			// Hash the string with SHA1
			return SHA1(tohash);
		}

		public static string SHA1(string data) {
			// Hash the input string using SHA1 and output a base64 answer

			byte[] bdata = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
			byte[] result = null;

			System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
			result = sha.ComputeHash(bdata);

			sha.Clear();

			return Convert.ToBase64String(result);
		}

		public static string SHA1_Real(string data) {
			// Hash the input string using SHA1 and output a hexadecimal answer
			byte[] bdata = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
			byte[] result = null;

			System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
			result = sha.ComputeHash(bdata);

			sha.Clear();

			string result_s = "";

			foreach(byte b in result)
				result_s += String.Format("{0:x2}", b);

			return result_s;
		}

		private static void AddLocalMenus(System.Windows.Forms.ToolStripItem menu, Hashtable ht) {
			//if (menu.Text == "(Plugin-Specific Items)" || menu.Text == "&Plugins")
			//	return;

			if (menu is ToolStripMenuItem) {
				if (!ht.ContainsKey(menu.Name)) {
					ht.Add(menu.Name, (menu as ToolStripMenuItem).ShortcutKeys);
				}

				foreach (ToolStripItem item in (menu as ToolStripMenuItem).DropDownItems) {
					if (item == null || item.Text == "")
						continue;

					if (!ht.ContainsKey(menu.Name))
						ht.Add(menu.Name, (item as ToolStripMenuItem).ShortcutKeys);

					AddLocalMenus(item, ht);
				}
			}

			
		}

		public static void SaveConfig(string filename, CConfig config) {
			FileStream fstream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
			BinaryFormatter fmtBinary = new BinaryFormatter();
			fmtBinary.Serialize(fstream, config);
			fstream.Close();
		}

		public static CConfig LoadConfig(string filename) {
			FileStream fstream;
			try {
				fstream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
			} catch { return new CConfig(); }

			BinaryFormatter fmtBinary = new BinaryFormatter();

			try {
				CConfig config = (CConfig)fmtBinary.Deserialize(fstream);
				fstream.Close();

				// Check the version
				if (config.CONFIG_VERSION != CONFIGURATION_VERSION)
					return new CConfig();
				else
					return config;
			} catch { fstream.Close(); return new CConfig(); }
			
		}
		
		[Serializable]
		internal class HighlightEntry : ICloneable {
			public HighlightEntry(System.Drawing.Font HighlightFont, System.Drawing.Color ForeColor, System.Drawing.Color BackColor) {
				this.HighlightFont = HighlightFont;
				this.ForeColor = ForeColor;
				this.BackColor = BackColor;
			}

			public HighlightEntry(System.Drawing.Color fg) {
				this.HighlightFont = new Font("Courier New", 10f);
				this.ForeColor = fg;
				this.BackColor = Color.White;
			}

			public HighlightEntry(System.Drawing.Color fg, System.Drawing.Color bg) {
				this.HighlightFont = new Font("Courier New", 10f);
				this.ForeColor = fg;
				this.BackColor = bg;
			}

			public System.Drawing.Font HighlightFont;
			public System.Drawing.Color ForeColor;
			public System.Drawing.Color BackColor;

			#region ICloneable Members

			public object Clone() {
				return this.MemberwiseClone();
			}

			#endregion
		}
	

		public Hashtable colors = new Hashtable();
		public Hashtable recent_files = new Hashtable();
		public byte[] DockbarData;
		public byte[] DebugBarData;
		public byte[] FindResultsBarData;
		public bool bFirstrun = false;
		public string LastProjectRoot = "";
		public string LastMotd = "";
		public int DebugTimeout = 3;

		public int CONFIG_VERSION = CONFIGURATION_VERSION;
		
		// Activation stuff
		public bool bIsActivated = true;
		public string ActivationResponse = "";
		public string ActivationLastHardkey = "";
		public string ActivationEmail = "";
		public long ActivationNextCheck = (new DateTime(2006, 12, 1)).Ticks;
		public string ActivationUser = "Temporary Regged User";
		public bool ActivationHasTGE = true;
		public bool ActivationHasTSE = true;
		public bool ActivationHasT2D = true;
		public bool ActivationLocal = false;

		public long NextUpdate = 0;
		public bool bCheckUpdates = true;

		public bool b_AC_Infopop = true;
		public bool b_AC_ObjectML = true;
		public bool b_AC_VariableML = true;
		public bool b_AC_TypeAsYouGo = true;

		public bool b_Err_BeforeCompile = true;
		public bool b_Err_WhileTyping = true;

		public bool b_Ed_Virtlines = false;
		public bool b_Ed_ShowLines = true;
		public bool b_Ed_ShowWhitespace = false;
		public bool b_Ed_ShowNewline = false;
		public bool b_Ed_Wordwrap = false;
		public bool b_Ed_BracketHighlight = true;
		public bool b_Ed_ConvertTabsToSpaces = false;
		public bool b_Ed_ShowTabs = false;
		public int i_Ed_TabSize = 4;
		public bool b_Ed_AutoIndent = true;
		public bool b_Ed_CodeFold = true;
		public bool b_Ed_IndentGuides = true;

		public Dictionary<string, object> PluginConfigs = new Dictionary<string, object>();

		public ColorPref ColorPref = ColorPref.Standard;

		public bool b_DeleteDSO = true;
		public string DSOExtension = "dso";
		public DebugExecEntryCollection DebugRun = new DebugExecEntryCollection();

		public bool bShowDebugSummary = false;

		public Point main_location = new Point(0, 0);
		public Size main_size = new Size(800, 600);
		public System.Windows.Forms.FormWindowState main_state = System.Windows.Forms.FormWindowState.Normal;

		public bool bAutoCollapse = true;
		public bool bScrollHint = false;

		public Hashtable menu_shortcuts = new Hashtable();
		//public Hashtable plugin_configopts = new Hashtable();
		//public MenuShortcutCollection menu_shortcuts = new MenuShortcutCollection();

		#region Color Schemes / Menus
		public void LoadKeymap_TD() {
			// TorqueDev / Visual Studio (VB) Keymapings
			menu_shortcuts["mnuDebug_StepInto"] = Keys.F8;
			menu_shortcuts["mnuDebug_StepOut"] = Keys.Control | Keys.Shift | Keys.F8;
			menu_shortcuts["mnuDebug_StepOver"] = Keys.Shift | Keys.F8;
		}

		public void LoadKeymap_Default() {
			// Torsion, VC#, VC++, and default keymappings
			menu_shortcuts["mnuDebug_StepInto"] = Keys.F11;
			menu_shortcuts["mnuDebug_StepOut"] = Keys.Shift | Keys.F11;
			menu_shortcuts["mnuDebug_StepOver"] = Keys.F10;
		}

		public void LoadScheme_Default() {
			colors.Clear();

			colors.Add("ReservedWordStyle", new HighlightEntry(Color.Blue));
			colors.Add("FunctionStyle", new HighlightEntry(Color.Red));
			colors.Add("LocalVariableStyle", new HighlightEntry(Color.Teal));
			colors.Add("GlobalVariableStyle", new HighlightEntry(Color.DarkRed));
			colors.Add("OperatorStyle", new HighlightEntry(Color.Gray));
			colors.Add("NumberStyle", new HighlightEntry(Color.Olive));
			colors.Add("StringDelimiterStyle", new HighlightEntry(Color.Magenta));
			colors.Add("StringDefaultStyle", new HighlightEntry(Color.Magenta));
			colors.Add("RegionStyle", new HighlightEntry(Color.Black, Color.LightGray));
			colors.Add("TaggedStringDelimiterStyle", new HighlightEntry(Color.Purple));
			colors.Add("TaggedStringDefaultStyle", new HighlightEntry(Color.Purple));
			colors.Add("CommentDelimiterStyle", new HighlightEntry(Color.DarkGreen));
			colors.Add("CommentDefaultStyle", new HighlightEntry(Color.DarkGreen));
			colors.Add("SpecialCommentDelimiterStyle", new HighlightEntry(Color.DarkGray));
			colors.Add("SpecialCommentDefaultStyle", new HighlightEntry(Color.DarkGray));
			colors.Add("MemberTokenStyle", new HighlightEntry(Color.Blue));
			colors.Add("DefaultStyle", new HighlightEntry(Color.Black, Color.White));
			colors.Add("DeclareStyle", new HighlightEntry(Color.DarkBlue, Color.White));
			colors.Add("_LineNumbers", new HighlightEntry(Color.Gray, Color.White));
			colors.Add("_LineNumbersBorder", new HighlightEntry(Color.Teal));
			colors.Add("_IndentGuidelines", new HighlightEntry(Color.LightGray));
		}

		public void LoadScheme_Noolness() {
			colors.Clear();

			colors.Add("ReservedWordStyle", new HighlightEntry(Color.FromArgb(255, 255, 100), Color.Black));
			colors.Add("FunctionStyle", new HighlightEntry(Color.FromArgb(255, 255, 100), Color.Black));
			colors.Add("LocalVariableStyle", new HighlightEntry(Color.Gray, Color.Black));
			colors.Add("GlobalVariableStyle", new HighlightEntry(Color.Red, Color.Black));
			colors.Add("OperatorStyle", new HighlightEntry(Color.FromArgb(0, 255, 0), Color.Black));
			colors.Add("NumberStyle", new HighlightEntry(Color.FromArgb(255, 160, 160), Color.Black));
			colors.Add("StringDelimiterStyle", new HighlightEntry(Color.FromArgb(255, 160, 160), Color.Black));
			colors.Add("StringDefaultStyle", new HighlightEntry(Color.FromArgb(255, 160, 160), Color.Black));
			colors.Add("RegionStyle", new HighlightEntry(Color.Gray, Color.Black));
			colors.Add("TaggedStringDelimiterStyle", new HighlightEntry(Color.Purple, Color.Black));
			colors.Add("TaggedStringDefaultStyle", new HighlightEntry(Color.Purple, Color.Black));
			colors.Add("CommentDelimiterStyle", new HighlightEntry(Color.FromArgb(160, 160, 255), Color.Black));
			colors.Add("CommentDefaultStyle", new HighlightEntry(Color.FromArgb(160, 160, 255), Color.Black));
			colors.Add("SpecialCommentDelimiterStyle", new HighlightEntry(Color.FromArgb(0, 128, 0), Color.Black));
			colors.Add("SpecialCommentDefaultStyle", new HighlightEntry(Color.FromArgb(0, 128, 0), Color.Black));
			colors.Add("MemberTokenStyle", new HighlightEntry(Color.FromArgb(255, 255, 100), Color.Black));
			colors.Add("DefaultStyle", new HighlightEntry(Color.White, Color.Black));
			colors.Add("DeclareStyle", new HighlightEntry(Color.DarkBlue, Color.Black));
			colors.Add("_LineNumbers", new HighlightEntry(Color.Gray, Color.Black));
			colors.Add("_LineNumbersBorder", new HighlightEntry(Color.DarkGray));
			colors.Add("_IndentGuidelines", new HighlightEntry(Color.DarkGray));
		}
		#endregion

		#region Load Color Data
		public void LoadColorData(ActiproSoftware.SyntaxEditor.SyntaxEditor SyntaxEd) {
			SyntaxEd.Document.LoadLanguageFromXml(System.Windows.Forms.Application.StartupPath + "\\highlight.bin", 5000);
			//SyntaxEd.Document.LoadLanguageFromXml(System.Windows.Forms.Application.StartupPath + "\\highlight.xml", 0);
			//SyntaxEd.Document.SaveLanguageToXml(System.Windows.Forms.Application.StartupPath + "\\highlight.bin", 5000);
			
			SyntaxEd.Document.Language.IsUpdating = true;

			IEnumerator enumer = SyntaxEd.Document.Language.HighlightingStyles.GetEnumerator();

			while(enumer.MoveNext()) {
				string key = (enumer.Current as ActiproSoftware.SyntaxEditor.HighlightingStyle).Key;
				if (colors.Contains(key)) {
					HighlightEntry he = (HighlightEntry)colors[key];

					SyntaxEd.Document.Language.HighlightingStyles[key].BackColor = he.BackColor;
					SyntaxEd.Document.Language.HighlightingStyles[key].ForeColor = he.ForeColor;
					SyntaxEd.Document.Language.HighlightingStyles[key].Bold = he.HighlightFont.Bold;
					SyntaxEd.Document.Language.HighlightingStyles[key].Italic = he.HighlightFont.Italic;
					SyntaxEd.Document.Language.HighlightingStyles[key].Underline = he.HighlightFont.Underline;
				}
			}

			
			SyntaxEd.Font = (colors["DefaultStyle"] as HighlightEntry).HighlightFont;
			SyntaxEd.Document.Language.BackColor = (colors["DefaultStyle"] as HighlightEntry).BackColor;

			SyntaxEd.TextAreaBackgroundFill = new ActiproSoftware.Drawing.SolidColorBackgroundFill((colors["DefaultStyle"] as HighlightEntry).BackColor);
			SyntaxEd.LineNumberMarginBackgroundFill = new ActiproSoftware.Drawing.SolidColorBackgroundFill((colors["_LineNumbers"] as HighlightEntry).BackColor);
			SyntaxEd.LineNumberMarginForeColor = (colors["_LineNumbers"] as HighlightEntry).ForeColor;
			SyntaxEd.LineNumberMarginFont = (colors["_LineNumbers"] as HighlightEntry).HighlightFont;
			SyntaxEd.LineNumberMarginBorderColor = (colors["_LineNumbersBorder"] as HighlightEntry).ForeColor;
			SyntaxEd.IndentationGuideColor = (colors["_IndentGuidelines"] as HighlightEntry).ForeColor;

			SyntaxEd.SelectionMarginBackgroundFill = new ActiproSoftware.Drawing.SolidColorBackgroundFill((colors["DefaultStyle"] as HighlightEntry).BackColor);

			SyntaxEd.Document.Language.IsUpdating = false;

			SyntaxEd.VerticalScrollBarHintEnabled = this.bScrollHint;
			SyntaxEd.WordWrap = (this.b_Ed_Wordwrap == true) ? ActiproSoftware.SyntaxEditor.WordWrapType.Word : ActiproSoftware.SyntaxEditor.WordWrapType.None;
			SyntaxEd.LineNumberMarginVisible = this.b_Ed_ShowLines;
			SyntaxEd.WhitespaceSpacesVisible = this.b_Ed_ShowWhitespace;
			SyntaxEd.WhitespaceLineEndsVisible = this.b_Ed_ShowNewline;
			SyntaxEd.WhitespaceTabsVisible = this.b_Ed_ShowTabs;
			SyntaxEd.BracketHighlightingVisible = this.b_Ed_BracketHighlight;
			SyntaxEd.Document.SpacesInTab = this.i_Ed_TabSize;
			SyntaxEd.ConvertTabsToSpaces = this.b_Ed_ConvertTabsToSpaces;
			SyntaxEd.IndentationGuidesVisible = this.b_Ed_IndentGuides;

		}
		#endregion

		#region ICloneable Members

		public object Clone() {
			//return this.MemberwiseClone();
			CConfig temp = new CConfig();

			temp = (CConfig)this.MemberwiseClone();
			temp.bFirstrun = this.bFirstrun;
			temp.colors = (Hashtable)this.colors.Clone();

			temp.DockbarData = this.DockbarData;
			temp.recent_files = this.recent_files;
			temp.bAutoCollapse = this.bAutoCollapse;
			temp.bCheckUpdates = this.bCheckUpdates;
			temp.bScrollHint = this.bScrollHint;
			temp.bShowDebugSummary = this.bShowDebugSummary;
			temp.menu_shortcuts = (Hashtable)this.menu_shortcuts.Clone();

			temp.b_AC_Infopop = this.b_AC_Infopop;
			temp.b_AC_ObjectML = this.b_AC_ObjectML;
			temp.b_AC_VariableML = this.b_AC_VariableML;
			temp.b_AC_TypeAsYouGo = this.b_AC_TypeAsYouGo;

			temp.b_Ed_BracketHighlight = this.b_Ed_BracketHighlight;
			temp.b_Ed_ConvertTabsToSpaces = this.b_Ed_ConvertTabsToSpaces;
			temp.b_Ed_ShowLines = this.b_Ed_ShowLines;
			temp.b_Ed_ShowNewline = this.b_Ed_ShowNewline;
			temp.b_Ed_ShowWhitespace = this.b_Ed_ShowWhitespace;
			temp.b_Ed_Virtlines = this.b_Ed_Virtlines;
			temp.b_Ed_Wordwrap = this.b_Ed_Wordwrap;
			temp.b_Ed_ShowTabs = this.b_Ed_ShowTabs;
			temp.i_Ed_TabSize = this.i_Ed_TabSize;
			temp.LastMotd = this.LastMotd;
			temp.DebugTimeout = this.DebugTimeout;
			temp.b_Ed_CodeFold = this.b_Ed_CodeFold;
			temp.b_Ed_IndentGuides = this.b_Ed_IndentGuides;

			temp.ActivationEmail = this.ActivationEmail;
			temp.ActivationHasT2D = this.ActivationHasT2D;
			temp.ActivationHasTGE = this.ActivationHasTGE;
			temp.ActivationHasTSE = this.ActivationHasTSE;
			temp.ActivationLastHardkey = this.ActivationLastHardkey;
			temp.ActivationLocal = this.ActivationLocal;
			temp.ActivationNextCheck = this.ActivationNextCheck;
			temp.ActivationResponse = this.ActivationResponse;
			temp.ActivationUser = this.ActivationUser;
			temp.bIsActivated = this.bIsActivated;

			temp.b_DeleteDSO = this.b_DeleteDSO;
			temp.DSOExtension = this.DSOExtension;
			temp.DebugRun = this.DebugRun;

			temp.b_Err_BeforeCompile = this.b_Err_BeforeCompile;
			temp.b_Err_WhileTyping = this.b_Err_WhileTyping;

			return temp;
		}

		#endregion
	}
}
