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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TSDev {
	public partial class frmNewItem : Form {
		public frmNewItem(CProject.Directory ParentDirectory) {
			InitializeComponent();

			_parentDir = ParentDirectory;
		}

		CProject.Directory _parentDir = null;

		private void frmNewItem_Load(object sender, EventArgs e) {
			// Initialize the file templates
			tvCategories.BeginUpdate();

			foreach (string dir in Directory.GetDirectories(Application.StartupPath + "\\file_templates")) {
				ScanDirectory(dir, null);
			}

			tvCategories.EndUpdate();

			
		}

		private void ScanDirectory(string directory, TreeNode parent) {
			// Add the files in the directory first
			string[] files = Directory.GetFiles(directory);
			ListViewItem[] lvi = new ListViewItem[files.Length];

			// Create a treenode
			string[] dirname = directory.Split('\\');
			TreeNode node = new TreeNode(dirname[dirname.Length - 1]);

			// See if there's a parent
			if (parent == null)
				tvCategories.Nodes.Add(node);
			else
				parent.Nodes.Add(node);

			for (int i = 0; i < lvi.Length; i++) {
				// Open the file first and grab the first line
				StreamReader sr = new StreamReader(files[i]);

				string firstline = sr.ReadLine();
				sr.Close();

				// Does the first line start with four ####?
				if (firstline.StartsWith("####")) {
					string[] lineparts = firstline.Split(new string[] { "####" }, StringSplitOptions.None);
					
					lvi[i] = new ListViewItem(lineparts[1]);
					lvi[i].Tag = lineparts[2] + "|" + files[i];

					// Put it in the right group
					foreach (ListViewGroup grp in lvTemplates.Groups) {
						if (grp.Header == lineparts[3]) {
							lvi[i].Group = grp;
							break;
						}
					}

					// Check to see if we put it in a group
					if (lvi[i].Group == null) {
						// Add a group, if not
						ListViewGroup newgroup = new ListViewGroup(lineparts[3]);
						lvTemplates.Groups.Add(newgroup);
						lvi[i].Group = newgroup;
					}

					// Determine the icon by the extension
					if (Path.GetExtension(files[i]) == ".cs" || Path.GetExtension(files[i]) == ".t2d")
						lvi[i].ImageIndex = 0;
					else if (Path.GetExtension(files[i]) == ".gui")
						lvi[i].ImageIndex = 1;
					else
						lvi[i].ImageIndex = 2;

				}
			}

			// Add the listview array to the node tag
			node.Tag = lvi;

			// Scan any subdirectories if they exist
			foreach (string subdir in Directory.GetDirectories(directory))
				ScanDirectory(subdir, node);
		}

		private void tvCategories_AfterSelect(object sender, TreeViewEventArgs e) {
			// Load up the view in the listview
			lvTemplates.BeginUpdate();
			lvTemplates.Items.Clear();

			foreach (ListViewItem lvi in (ListViewItem[])e.Node.Tag)
				lvTemplates.Items.Add(lvi);

			if (lvTemplates.Items.Count > 0)
				lvTemplates.Items[0].Selected = true;

			lvTemplates.EndUpdate();
		}

		private void lvTemplates_SelectedIndexChanged(object sender, EventArgs e) {
			if (lvTemplates.SelectedItems.Count == 0)
				return;

			// Just display the description in the box below
			if (!(lvTemplates.SelectedItems[0].Tag is string))
				return;

			lblInfo.Text = lvTemplates.SelectedItems[0].Tag.ToString().Split('|')[0];
		}

		private void cmdBrowse_Click(object sender, EventArgs e) {
			// Show the save box
			if (lvTemplates.SelectedItems.Count == 0) {
				MessageBox.Show("Please select a template to use before setting the file to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			string fileExtension = Path.GetExtension(lvTemplates.SelectedItems[0].Tag.ToString().Split('|')[1]);

			Directory.SetCurrentDirectory(g.Project.ProjectPath);
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.RestoreDirectory = true;
			sfd.Title = "Add New Item";
			sfd.Filter = "*" + fileExtension + " Files|*" + fileExtension + "|All Files (*.*)|*.*";
			sfd.OverwritePrompt = true;
			sfd.CheckPathExists = true;

			DialogResult result = sfd.ShowDialog(this);

			if (result == DialogResult.Cancel)
				return;

			txtSaveTo.Text = sfd.FileName;
			sfd.Reset();
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			// If we have a filename and a template selected, we're gold
			if (txtSaveTo.Text.Trim() == "") {
				MessageBox.Show("Please select a filename to save your file as.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (lvTemplates.SelectedItems.Count == 0) {
				MessageBox.Show("Please select a template to create the new file from", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Construct a relative path
			string fileRelativePath = CProject.PathGetRelative(txtSaveTo.Text, g.Project.ProjectPath);

			if (fileRelativePath == "")
				fileRelativePath = txtSaveTo.Text;

			// Check if the file already exists in the project
			if (g.Project.FileList.ContainsFile(fileRelativePath)) {
				// If it's open, close it
				g.Main.CloseFile(g.Project.FileList.GetFile(fileRelativePath), false, false);

				// Remove it from the file list
				g.Project.FileList.Remove(g.Project.FileList.GetFile(fileRelativePath));
			}

			// Open the template file
			StreamReader templateFile = new StreamReader(lvTemplates.SelectedItems[0].Tag.ToString().Split('|')[1]);

			// Chop off the first line by doing some backasswards string manipulation
			string template = templateFile.ReadToEnd().Split(new string[] { "\r\n" }, 2, StringSplitOptions.None)[1];
			templateFile.Close();

			// Write the template
			template = template.Replace("{{PROJECT_NAME}}", g.Project.ProjectName);
			template = template.Replace("{{FILE_RELATIVE_PATH}}", fileRelativePath);
			template = template.Replace("{{FILE_ABSOLUTE_PATH}}", txtSaveTo.Text);
			template = template.Replace("{{SYSUSER}}", Environment.UserName);
			template = template.Replace("{{SYSMACHINE}}", Environment.MachineName);
			template = template.Replace("{{CW_VERSION}}", Application.ProductVersion);
			template = template.Replace("{{DATE}}", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString());

			// Create the destination file and write the template
			StreamWriter destinationFile = new StreamWriter(txtSaveTo.Text);
			destinationFile.Write(template);
			destinationFile.Close();

			// Create a file
			CProject.File newFile = new CProject.File(Path.GetFileName(txtSaveTo.Text), fileRelativePath, false, false, _parentDir);
			newFile.isDirty = true;

			// Add to the file list
			g.Project.FileList.Add(newFile);

			// Make the project dirty
			frmMain.stc_bIsProjectDirty = true;

			// Open the file and re-init the left bar
			g.Main.OpenFile(newFile, 0, false);
			g.Main.InitProject();

			this.Close();

		}
	}
}