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
	/// Summary description for CNewProject.
	/// </summary>
	internal class frmNewProject : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList ilProject;
		private System.Windows.Forms.ImageList ilListView;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.ListView lvTemplates;
		private System.Windows.Forms.Label lblDescr;
		private System.Windows.Forms.TextBox txtProjectName;
		private System.Windows.Forms.TextBox txtProjectPath;
		private System.Windows.Forms.FolderBrowserDialog cdFolderBox;
		private System.Windows.Forms.CheckBox chkAutoImport;
		private System.Windows.Forms.TextBox txtFilter;
		private ImageList ilSmallListView;
		private System.ComponentModel.IContainer components;

		public frmNewProject()
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("TorqueDev Projects");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Torque", new System.Windows.Forms.TreeNode[] {
            treeNode3});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewProject));
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("TorqueDev Templates", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Empty TGE Project", 0);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Empty TSE Project", 1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Empty T2D Project (TGB)", 2);
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.ilProject = new System.Windows.Forms.ImageList(this.components);
			this.ilListView = new System.Windows.Forms.ImageList(this.components);
			this.lvTemplates = new System.Windows.Forms.ListView();
			this.ilSmallListView = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblDescr = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtProjectName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtProjectPath = new System.Windows.Forms.TextBox();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cdFolderBox = new System.Windows.Forms.FolderBrowserDialog();
			this.chkAutoImport = new System.Windows.Forms.CheckBox();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(12, 28);
			this.treeView1.Name = "treeView1";
			treeNode3.Name = "Node1";
			treeNode3.Text = "TorqueDev Projects";
			treeNode4.Name = "Node0";
			treeNode4.Text = "Torque";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
			this.treeView1.Scrollable = false;
			this.treeView1.Size = new System.Drawing.Size(262, 200);
			this.treeView1.TabIndex = 1;
			// 
			// ilProject
			// 
			this.ilProject.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilProject.ImageStream")));
			this.ilProject.TransparentColor = System.Drawing.Color.Transparent;
			this.ilProject.Images.SetKeyName(0, "");
			this.ilProject.Images.SetKeyName(1, "");
			// 
			// ilListView
			// 
			this.ilListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilListView.ImageStream")));
			this.ilListView.TransparentColor = System.Drawing.Color.Transparent;
			this.ilListView.Images.SetKeyName(0, "");
			this.ilListView.Images.SetKeyName(1, "");
			this.ilListView.Images.SetKeyName(2, "");
			this.ilListView.Images.SetKeyName(3, "");
			// 
			// lvTemplates
			// 
			listViewGroup2.Header = "TorqueDev Templates";
			listViewGroup2.Name = "listViewGroup1";
			this.lvTemplates.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
			this.lvTemplates.HideSelection = false;
			listViewItem4.Group = listViewGroup2;
			listViewItem4.Tag = "0";
			listViewItem5.Group = listViewGroup2;
			listViewItem5.Tag = "1";
			listViewItem6.Group = listViewGroup2;
			listViewItem6.Tag = "2";
			this.lvTemplates.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.lvTemplates.LargeImageList = this.ilListView;
			this.lvTemplates.Location = new System.Drawing.Point(280, 28);
			this.lvTemplates.MultiSelect = false;
			this.lvTemplates.Name = "lvTemplates";
			this.lvTemplates.Size = new System.Drawing.Size(294, 200);
			this.lvTemplates.SmallImageList = this.ilSmallListView;
			this.lvTemplates.TabIndex = 3;
			this.lvTemplates.UseCompatibleStateImageBehavior = false;
			this.lvTemplates.View = System.Windows.Forms.View.SmallIcon;
			this.lvTemplates.SelectedIndexChanged += new System.EventHandler(this.lvTemplates_SelectedIndexChanged);
			// 
			// ilSmallListView
			// 
			this.ilSmallListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallListView.ImageStream")));
			this.ilSmallListView.TransparentColor = System.Drawing.Color.Transparent;
			this.ilSmallListView.Images.SetKeyName(0, "window_environment.png");
			this.ilSmallListView.Images.SetKeyName(1, "window_earth.png");
			this.ilSmallListView.Images.SetKeyName(2, "window_gear.png");
			this.ilSmallListView.Images.SetKeyName(3, "window_star.png");
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "P&roject types:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(277, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Project t&emplates:";
			// 
			// lblDescr
			// 
			this.lblDescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDescr.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDescr.Location = new System.Drawing.Point(12, 231);
			this.lblDescr.Name = "lblDescr";
			this.lblDescr.Size = new System.Drawing.Size(562, 24);
			this.lblDescr.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(9, 272);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "&Name:";
			// 
			// txtProjectName
			// 
			this.txtProjectName.Location = new System.Drawing.Point(112, 269);
			this.txtProjectName.Name = "txtProjectName";
			this.txtProjectName.Size = new System.Drawing.Size(374, 21);
			this.txtProjectName.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(9, 296);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "&Save Project:";
			// 
			// txtProjectPath
			// 
			this.txtProjectPath.Location = new System.Drawing.Point(112, 296);
			this.txtProjectPath.Name = "txtProjectPath";
			this.txtProjectPath.Size = new System.Drawing.Size(374, 21);
			this.txtProjectPath.TabIndex = 8;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowse.Location = new System.Drawing.Point(492, 296);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(76, 24);
			this.cmdBrowse.TabIndex = 9;
			this.cmdBrowse.Text = "&Browse...";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(492, 368);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(76, 24);
			this.cmdCancel.TabIndex = 13;
			this.cmdCancel.Text = "&Cancel";
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(410, 368);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(76, 24);
			this.cmdOK.TabIndex = 12;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cdFolderBox
			// 
			this.cdFolderBox.Description = "Please select a folder below to use as the base path for your project:";
			// 
			// chkAutoImport
			// 
			this.chkAutoImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAutoImport.Location = new System.Drawing.Point(112, 330);
			this.chkAutoImport.Name = "chkAutoImport";
			this.chkAutoImport.Size = new System.Drawing.Size(160, 16);
			this.chkAutoImport.TabIndex = 10;
			this.chkAutoImport.Text = "A&utomatically Import Files:";
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(288, 328);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(198, 21);
			this.txtFilter.TabIndex = 11;
			this.txtFilter.Text = "*.cs;*.t2d;*.gui;*.mis";
			// 
			// frmNewProject
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(586, 400);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.txtProjectPath);
			this.Controls.Add(this.txtProjectName);
			this.Controls.Add(this.chkAutoImport);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblDescr);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lvTemplates);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdOK);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewProject";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New TorqueDev Project";
			this.Closed += new System.EventHandler(this.frmNewProject_Closed);
			this.Load += new System.EventHandler(this.frmNewProject_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void lvTemplates_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (lvTemplates.SelectedItems.Count != 1)
				return;

			switch (lvTemplates.SelectedItems[0].Tag.ToString()) {
				case "0":
					lblDescr.Text = "An empty Torque Game Engine project.";
					break;
				case "1":
					lblDescr.Text = "An empty Torque Shader Engine project.";
					break;
				case "2":
					lblDescr.Text = "An empty Torque 2D project.";
					break;
			}
		}

		private void frmNewProject_Load(object sender, System.EventArgs e) {
			lvTemplates.Items[0].Selected = true;
			//this.txtProjectPath.Text = g.Config.LastProjectRoot;

			lvTemplates.Items[0].ImageIndex = 0;
			lvTemplates.Items[1].ImageIndex = 1;
			lvTemplates.Items[2].ImageIndex = 2;

			treeView1.ExpandAll();
			treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e) {
			/*if (Directory.Exists(this.txtProjectPath.Text))
				cdFolderBox.SelectedPath = this.txtProjectPath.Text;

			DialogResult result = cdFolderBox.ShowDialog(this);

			if (result == DialogResult.Cancel)
				return;

			this.txtProjectPath.Text = cdFolderBox.SelectedPath;*/

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Title = "Save Project";
			sfd.Filter = "TorqueDev Projects (*.tsdev)|*.tsdev";
			sfd.OverwritePrompt = true;
			sfd.CheckPathExists = true;

			DialogResult result = sfd.ShowDialog(this);

			if (result == DialogResult.Cancel)
				return;

			this.txtProjectPath.Text = sfd.FileName;
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			if (lvTemplates.SelectedItems.Count == 0) {
				MessageBox.Show(this, "Please select a project type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Verify the project type is licensed
			//if (lvTemplates.SelectedItems[0].Tag.ToString() == "0" && g.Config.ActivationHasTGE == false) {
			//	MessageBox.Show(this, "Error: You are not licensed to create TGE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//} else if (lvTemplates.SelectedItems[0].Tag.ToString() == "1" && g.Config.ActivationHasTSE == false) {
			//	MessageBox.Show(this, "Error: You are not licensed to create TSE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			if (lvTemplates.SelectedItems[0].Tag.ToString() == "2" && g.Config.ActivationHasT2D == false) {
				MessageBox.Show(this, "Error: You are not licensed to create T2D projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (txtProjectName.Text.Trim() == "" || txtProjectPath.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter a project name and a root path.", "TSDev", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (!Directory.Exists(Path.GetDirectoryName(txtProjectPath.Text))) {
				MessageBox.Show(this, "Project root path folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (this.chkAutoImport.Checked == true && this.txtFilter.Text.Trim() == "") {
				MessageBox.Show(this, "Please specify a valid file filter.", "TSDev", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			g.Main.CreateProject(txtProjectName.Text.Trim(), Path.GetDirectoryName(txtProjectPath.Text.Trim()), Convert.ToInt16(lvTemplates.SelectedItems[0].Tag));

			if (this.chkAutoImport.Checked) {
				ImportDirectory(Path.GetDirectoryName(txtProjectPath.Text), null);
				g.Main.InitProject();
			}

			CProject.SaveProject(this.txtProjectPath.Text, g.Project);

			g.Config.LastProjectRoot = Path.GetDirectoryName(txtProjectPath.Text);

			this.Close();

		}

		private void ImportDirectory(string dir, CProject.Directory parent_dir) {
			string [] subdirs = Directory.GetDirectories(dir, "*");
			
			foreach(string file_entry in this.txtFilter.Text.ToLower().Replace(" ", "").Split(';')) {
				foreach(string file in Directory.GetFiles(dir, file_entry))
					g.Main.AddExistingFile(file, parent_dir);
			}

			foreach(string subdir in subdirs) {
				string[] dir_name = subdir.Split('\\');
				CProject.Directory d_subdir = new CProject.Directory(dir_name[dir_name.Length - 1], parent_dir, false);
				g.Project.DirList.Add(d_subdir);

				ImportDirectory(subdir, d_subdir);
			}
				
		}

		private void frmNewProject_Closed(object sender, System.EventArgs e) {
			this.Dispose();
		}
	}
}
