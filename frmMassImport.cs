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
	/// Summary description for frmMassImport.
	/// </summary>
	internal class frmMassImport : System.Windows.Forms.Form
	{
		private CProject.Directory parent_dir;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDir;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtExts;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdImport;
		private System.Windows.Forms.FolderBrowserDialog dlgDirSel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMassImport(CProject.Directory parent_dir)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.parent_dir = parent_dir;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMassImport));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDir = new System.Windows.Forms.TextBox();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtExts = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdImport = new System.Windows.Forms.Button();
			this.dlgDirSel = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(280, 72);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Directory:";
			// 
			// txtDir
			// 
			this.txtDir.Location = new System.Drawing.Point(88, 88);
			this.txtDir.Name = "txtDir";
			this.txtDir.Size = new System.Drawing.Size(200, 20);
			this.txtDir.TabIndex = 3;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowse.Location = new System.Drawing.Point(296, 88);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(64, 24);
			this.cmdBrowse.TabIndex = 4;
			this.cmdBrowse.Text = "Browse";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Extensions:";
			// 
			// txtExts
			// 
			this.txtExts.Location = new System.Drawing.Point(88, 120);
			this.txtExts.Name = "txtExts";
			this.txtExts.Size = new System.Drawing.Size(200, 20);
			this.txtExts.TabIndex = 3;
			this.txtExts.Text = "*.cs;*.gui;*.mis;*.t2d";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(88, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(224, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Enter extensions separated by semicolons.";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(272, 176);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdImport
			// 
			this.cmdImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdImport.Location = new System.Drawing.Point(176, 176);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(80, 24);
			this.cmdImport.TabIndex = 7;
			this.cmdImport.Text = "Import";
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// dlgDirSel
			// 
			this.dlgDirSel.Description = "Select a directory to begin importing:";
			this.dlgDirSel.ShowNewFolderButton = false;
			// 
			// frmMassImport
			// 
			this.AcceptButton = this.cmdImport;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(370, 215);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtDir);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtExts);
			this.Controls.Add(this.cmdImport);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMassImport";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bulk Import";
			this.Load += new System.EventHandler(this.frmMassImport_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void cmdImport_Click(object sender, System.EventArgs e) {
			if (this.txtDir.Text == "" || Directory.Exists(this.txtDir.Text) == false) {
				MessageBox.Show(this, "Specified directory does not exist.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtExts.Text == "") {
				MessageBox.Show(this, "Please specify at least one extension in the format indicated by the default template.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Create the parent directory

			ImportDirectory(this.txtDir.Text, this.parent_dir);
			g.Main.InitProject();

			// Dirty the project
			frmMain.stc_bIsProjectDirty = true;


			this.Close();
		}

		private void ImportDirectory(string dir, CProject.Directory parent_dir) {
			string [] subdirs = Directory.GetDirectories(dir, "*");
			
			foreach(string file_entry in this.txtExts.Text.ToLower().Replace(" ", "").Split(';')) {
				foreach(string file in Directory.GetFiles(dir, file_entry)) {
					if ((File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden)
						continue;

					g.Main.AddExistingFile(file, parent_dir);
				}
			}

			foreach(string subdir in subdirs) {
				DirectoryInfo di = new DirectoryInfo(subdir);

				if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					continue;

				string[] dir_name = subdir.Split('\\');
				CProject.Directory d_subdir = new CProject.Directory(dir_name[dir_name.Length - 1], parent_dir, false);
				g.Project.DirList.Add(d_subdir);

				ImportDirectory(subdir, d_subdir);
			}
				
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e) {
			if (Directory.Exists(this.txtDir.Text))
				this.dlgDirSel.SelectedPath = this.txtDir.Text;

			DialogResult result = dlgDirSel.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			this.txtDir.Text = this.dlgDirSel.SelectedPath;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmMassImport_Load(object sender, System.EventArgs e) {
			this.txtDir.Text = g.Project.ProjectPath;
		}
	}
}
