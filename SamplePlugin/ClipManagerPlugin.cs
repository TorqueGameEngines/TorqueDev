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
using System.Collections.Generic;
using System.Text;
using TSDev.Plugins;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace ClipManagerPlugin {
	
	[CodeweaverPlugin("{B34C9B65-5F5A-41ed-AB4B-DDC31F8DF70E}", "Clip Manager", "Stores clips of selected data from the text editor", "netMercs", "Copyright (c) 2007 netMercs Group", "1.0", "9.0")]
	public class ClipManagerPlugin : ICodeweaverPlugin {


		private static IDEControl controlObject;
		private static ICodeweaverPlugin self;
		private Version cwVersion;
		private CWLicenseType cwLicenseType;
		private int workspaceIconHandle;

		// Our controls
		private ClipManagerWindow clipManagerWindow = null;
		private ToolStripMenuItem editorContext = new ToolStripMenuItem("Clip Manager", global::ClipManagerPlugin.Properties.Resource.clipboard);
		private ToolStripMenuItem editorContextAdd = new ToolStripMenuItem("Add Selection to Clip Manager", global::ClipManagerPlugin.Properties.Resource.add);
		private ToolStripMenuItem pluginMenu = new ToolStripMenuItem("Show Clip Manager", global::ClipManagerPlugin.Properties.Resource.clipboard);

		// Our config page
		private ClipManagerConfig clipManagerConfig = new ClipManagerConfig();

		private static List<string> stringCollection = new List<string>();

		public static List<string> StringCollection {
			get { return stringCollection; }
			set { stringCollection = value; }
		}

		public static IDEControl ControlObject {
			get { return controlObject; }
		}

		public static ICodeweaverPlugin Self {
			get { return self; }
		}

		#region Events

		public bool CWPluginInit(Version CWVersion, CWLicenseType UserLicenseType, IDEControl ControlObject) {
			// We need "Self" to be our current instance so we can be accessed elsewhere.  We also need
			// to set our control object so we can communicate with the IDE
			ClipManagerPlugin.controlObject = ControlObject;
			ClipManagerPlugin.self = this;

			// Set our version info
			this.cwVersion = CWVersion;
			this.cwLicenseType = UserLicenseType;

			// Register our tab icon
			workspaceIconHandle = controlObject.AddWorkspaceTabIconResource(global::ClipManagerPlugin.Properties.Resource.clipboard);

			// Register our editor context menu
			controlObject.RegisterCustomEditorContextMenu(editorContext);

			// Register plugin menu item and handler
			controlObject.RegisterCustomPluginMenu(pluginMenu);
			pluginMenu.Click += new EventHandler(pluginMenu_Click);

			// Register our config page
			controlObject.RegisterCustomConfigTab(this, "Clip Manager", clipManagerConfig);

			// Add our handlers to the context menu
			editorContext.DropDownOpening += new EventHandler(editorContext_DropDownOpening);

			// Our "Add" context menu is bound here
			editorContextAdd.Click += new EventHandler(editorContextAdd_Click);

			return true;
		}

		void pluginMenu_Click(object sender, EventArgs e)
		{
			// If we're null, we need to create the tab
			if (clipManagerWindow == null) {
				clipManagerWindow = new ClipManagerWindow();
				controlObject.RegisterEnvironmentTab(this, "Clip Manager", workspaceIconHandle, clipManagerWindow);
			}

			// Focus it
			controlObject.FocusEnvironmentTab(clipManagerWindow);
		}

		void editorContextAdd_Click(object sender, EventArgs e) {
			// Check if anything is selected
			string selectedText = controlObject.EditorGetSelected(controlObject.GetActiveFile().Value);

			if (selectedText == "")
				return;

			if (!stringCollection.Contains(selectedText))
				stringCollection.Add(selectedText);
		}

		void editorContext_DropDownOpening(object sender, EventArgs e) {
			// Remove all current dropdown items
			editorContext.DropDownItems.Clear();

			// Add our "Add Clip" item
			editorContext.DropDownItems.Add(editorContextAdd);
			editorContext.DropDownItems.Add(new ToolStripSeparator());

			// Repopulate with current collection of strings
			foreach(string s in stringCollection) {
				ToolStripMenuItem newItem = new ToolStripMenuItem();

				// Adjust string length
				if (s.Length > 30)
					newItem.Text = s.Substring(0, 25) + "...";
				else
					newItem.Text = s;

				// Set tag for future use
				newItem.Tag = s;

				// Add our handler
				newItem.Click += new EventHandler(newItem_Click);

				// Add to the menu
				editorContext.DropDownItems.Add(newItem);
			}

			// Are we blank?
			if (stringCollection.Count == 0) {
				// Add an empty item
				ToolStripMenuItem newItem = new ToolStripMenuItem("(Add some clips and they will be displayed here)");
				newItem.Enabled = false;

				editorContext.DropDownItems.Add(newItem);
			}

		}

		void newItem_Click(object sender, EventArgs e) {
			// Get offset (rather than row, col)
			int[] editorOffset = controlObject.EditorGetCaret(
				controlObject.GetActiveFile().Value,
				true
			);

			// Insert into currently active editor (this should only fire if we have an editor open!)
			controlObject.EditorInsertText(
				controlObject.GetActiveFile().Value,
				editorOffset[0], (sender as ToolStripMenuItem).Tag.ToString()
			);
		}

		public void CWProjectLoad(CWProject ProjectDetails, ref bool cancel) {
			
		}

		public void CWFileAfterLoad(CWFile FileDetails) {
			
		}

		public void CWFileAfterClose(CWFile FileDetails) {
			
		}

		public void CWProjectClose() {
			
		}

		public void CWFileAfterSave(CWFile FileDetails) {
			
		}

		public void CWIDEClosing(ref bool cancel) {
			
		}

		public void CWEditorTriggerFire(CWFile FileDetails, string TriggerName) {
			
		}

		public void CWEnvironmentTabClosing(Control Tab, ref bool cancel) {
			// If we're closing our clip manager window tab, set our
			// object to null
			if (Tab == clipManagerWindow)
				clipManagerWindow = null;
		}

		public void CWEnvironmentTabSwitchedTo(Control Tab) {
			if (Tab is ClipManagerWindow)
				(Tab as ClipManagerWindow).RefreshClips();
		}

		public void CWAboutDialog() {
			MessageBox.Show(
				"Clip Manager Plugin\n" +
				"Copyright 2007 netMercs Group\n" +
				"\n" +
				"This plugin demonstrates some basic features of the TorqueDev plugins architecture.  Feel free to add, " +
				"remove, or otherwise change this plugin to suit your needs.\n\n---\n" +
				"TorqueDev Version: " + this.cwVersion.ToString() + "\n" +
				"TorqueDev License: " + this.cwLicenseType.ToString(),
				"About Clip Manager", MessageBoxButtons.OK, MessageBoxIcon.Information
			);
		}

		public Guid CWPluginGuid {
			get { return new Guid("{B34C9B65-5F5A-41ed-AB4B-DDC31F8DF70E}"); }
		}

		#endregion

		
	}
}
