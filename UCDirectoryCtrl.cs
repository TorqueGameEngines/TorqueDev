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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace TSDev
{
	/// <summary>
	/// Summary description for UCDirectoryCtrl.
	/// </summary>
	internal class UCDirectoryCtrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TreeView tvFiles;
		private System.ComponentModel.IContainer components;
		private System.IO.FileSystemWatcher fsw;

		private string _RootPath = Directory.GetCurrentDirectory();
		private string _BaseText = "Base Node";
		private int _BaseIndex = 0;

		
		public event dOnItemActivate OnItemActivate;
		public delegate void dOnItemActivate(string filename);

		public event dOnGetItemIcon OnGetItemIcon;
		public delegate void dOnGetItemIcon(string filename, ref int icon_id);

		public string RootPath {
			get {
				return _RootPath;
			} 
			set {
				if (value == "") {
					_RootPath = "";
					fsw.EnableRaisingEvents = false;
					this.tvFiles.Nodes.Clear();
				} else {
					fsw.Path = value;
					_RootPath = value;
					fsw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
					RefreshListing();
				}
			}
		}

		public string BaseText {
			get {
				return _BaseText;
			}
			set {
				_BaseText = value;
			}
		}

		public int BaseIndex {
			get { return _BaseIndex; }
			set { _BaseIndex = value; }
		}

		public ImageList DirectoryImageList {
			get { return this.imageList1; }
			set { this.imageList1 = value; this.tvFiles.ImageList = value; }
		}

		public string SelectedFile {
			get { 
				if (this.tvFiles.SelectedNode == null)
					return null;

				if (this.tvFiles.SelectedNode.Tag == null)
					return null;

				return this.tvFiles.SelectedNode.Tag.ToString(); 
			}
		}

		public ContextMenu ListContextMenu {
			get { return this.tvFiles.ContextMenu; }
			set { this.tvFiles.ContextMenu = value; }
		}

		public UCDirectoryCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UCDirectoryCtrl));
			this.tvFiles = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.fsw = new System.IO.FileSystemWatcher();
			((System.ComponentModel.ISupportInitialize)(this.fsw)).BeginInit();
			this.SuspendLayout();
			// 
			// tvFiles
			// 
			this.tvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvFiles.FullRowSelect = true;
			this.tvFiles.HideSelection = false;
			this.tvFiles.ImageList = this.imageList1;
			this.tvFiles.Location = new System.Drawing.Point(0, 0);
			this.tvFiles.Name = "tvFiles";
			this.tvFiles.Size = new System.Drawing.Size(248, 440);
			this.tvFiles.TabIndex = 0;
			this.tvFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvFiles_KeyDown);
			this.tvFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvFiles_MouseDown);
			this.tvFiles.DoubleClick += new System.EventHandler(this.tvFiles_DoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// fsw
			// 
			this.fsw.EnableRaisingEvents = true;
			this.fsw.IncludeSubdirectories = true;
			this.fsw.NotifyFilter = System.IO.NotifyFilters.FileName;
			this.fsw.SynchronizingObject = this;
			this.fsw.Deleted += new System.IO.FileSystemEventHandler(this.fsw_Deleted);
			this.fsw.Renamed += new System.IO.RenamedEventHandler(this.fsw_Renamed);
			this.fsw.Created += new System.IO.FileSystemEventHandler(this.fsw_Created);
			// 
			// UCDirectoryCtrl
			// 
			this.Controls.Add(this.tvFiles);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UCDirectoryCtrl";
			this.Size = new System.Drawing.Size(248, 440);
			((System.ComponentModel.ISupportInitialize)(this.fsw)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public void RefreshListing() {
			this.tvFiles.Nodes.Clear();
			this.tvFiles.BeginUpdate();

			TreeNode basenode = new TreeNode(_BaseText, _BaseIndex, _BaseIndex);
			basenode.Tag = _RootPath;

			//AddToTree(_RootPath, basenode);

			// Populate the directories first
			foreach(string dir in Directory.GetDirectories(_RootPath))
				AddToTree(dir, basenode);

			// Populate the files in the directory
			foreach(string file in Directory.GetFiles(_RootPath)) {
				int icon = GetIcon(file);

				if (icon == -1)
					continue;

				FileInfo finfo = new FileInfo(file);
				if ((finfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					continue;

				TreeNode filen = new TreeNode(Path.GetFileName(file), icon, icon);
				filen.Tag = file;
				basenode.Nodes.Add(filen);
				//this.tvFiles.Nodes.Add(filen);
			}

			basenode.Expand();
			this.tvFiles.Nodes.Add(basenode);

			this.tvFiles.EndUpdate();
		}

		private void AddToTree(string dir, TreeNode parent) {
			string[] dirname = dir.Split('\\');

			DirectoryInfo info = new DirectoryInfo(dir);
			if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
				return;

			TreeNode node = new TreeNode(dirname[dirname.Length - 1], 0, 1);
			node.Tag = dir;

			foreach(string dirx in Directory.GetDirectories(dir))
				AddToTree(dirx, node);

			// Grab the files from the directory
			foreach(string file in Directory.GetFiles(dir)) {
				FileInfo finfo = new FileInfo(file);
				if ((finfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					continue;

				int icon = GetIcon(file);

				if (icon == -1)
					continue;

				TreeNode filenode = new TreeNode(Path.GetFileName(file), icon, icon);
				filenode.Tag = file;

				node.Nodes.Add(filenode);
			}

			if (parent != null)
				parent.Nodes.Add(node);
			else
				tvFiles.Nodes.Add(node);
		}

		private void fsw_Created(object sender, System.IO.FileSystemEventArgs e) {
			// Check if it's invisible
			if (Directory.Exists(e.FullPath)) {
				DirectoryInfo info = new DirectoryInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			} else {
				FileInfo info = new FileInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			}

			// Add the node to the treeview... get the
			// parent directory by splitting it off the end
			// of the array
			this.tvFiles.BeginUpdate();

			bool succeeded = false;
			while(!succeeded) {
				try {
					bool isDir = Directory.Exists(e.FullPath);

					string[] dirname = e.FullPath.Split('\\');

					// Search for the node
					TreeNode node = Find(String.Join("\\", dirname, 0, dirname.Length - 1));

					if (node == null) {
						this.tvFiles.EndUpdate();
						return;
					}

					// Add a new node describing this file to the parent
					TreeNode newnode = null;
					if (isDir) {
						AddToTree(e.FullPath, node);
					} else {
						int icon = GetIcon(e.FullPath);

						if (icon == -1)
							continue;

						newnode = new TreeNode(Path.GetFileName(e.FullPath), icon, icon);
						newnode.Tag = e.FullPath;
						node.Nodes.Add(newnode);
					}

					succeeded = true;
				} catch {
					System.Threading.Thread.Sleep(1000);
				}
			}

			this.tvFiles.EndUpdate();
		}

		private TreeNode Find(string path) {
			TreeNode node = FindEx(path, this.tvFiles.Nodes[0]);
			return node;
		}

		private TreeNode FindEx(string path, TreeNode parent) {
			if (parent != null) {
				if (parent.Tag.ToString().ToLower() == path.ToLower())
					return parent;
			}

			foreach(TreeNode node in parent.Nodes) {
				TreeNode findnode = FindEx(path, node);

				if (findnode != null)
					return findnode;
			}

			return null;
		}

		private void fsw_Renamed(object sender, System.IO.RenamedEventArgs e) {
			// Check if it's invisible
			if (Directory.Exists(e.FullPath)) {
				DirectoryInfo info = new DirectoryInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			} else {
				FileInfo info = new FileInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			}

			// Rename event... find the previous file
			this.tvFiles.BeginUpdate();

			TreeNode prevfile = Find(e.OldFullPath);

			if (prevfile == null) {
				this.tvFiles.EndUpdate();
				return;
			}

			// Take the node and delete it
			prevfile.Remove();

			// Find the new parent, if any
			string[] parentdir = e.FullPath.Split('\\');

			TreeNode parent = Find(String.Join("\\", parentdir, 0, parentdir.Length - 1));

			if (parent == null) {
				this.tvFiles.EndUpdate();
				return;
			}

			// If we're a file, this part's easy
			if (!Directory.Exists(e.FullPath)) {
				// Take the old node and set the new name
				prevfile.Text = Path.GetFileName(e.FullPath);

				// Set the icons
				int icon = GetIcon(e.FullPath);

				if (icon != -1) {
					prevfile.ImageIndex = icon;
					prevfile.SelectedImageIndex = GetIcon(e.FullPath);

					// Set its new tag
					prevfile.Tag = e.FullPath;

					// Add it back to the parent
					parent.Nodes.Add(prevfile);
				}
			} else {
				// If we're a directory, we've got to scan our contents
				// again, in case we're being moved in from "elsewhere"
				
				// Check if we're a directory *somewhere* in our root
				// path:
				if (IsSubdirectory(e.FullPath)) {
					// Yes.  Do the same thing we did above for the file
					prevfile.Text = Path.GetFileName(e.FullPath);
					prevfile.Tag = e.FullPath;

					parent.Nodes.Add(prevfile);
				} else {
					// No.  Rescan the directory
					AddToTree(e.FullPath, parent);
				}
			}

			this.tvFiles.EndUpdate();

		}

		private bool IsSubdirectory(string path) {
			// Check if the specified path is a subdirectory
			// if our root path
			if (Directory.GetParent(path).FullName == _RootPath)
				return true;

			bool found = false;
			while(!(Directory.GetParent(path) == null || Directory.GetParent(path) == Directory.GetParent(path))) {
				path = Directory.GetParent(path).FullName;

				if (path == _RootPath) {
					found = true;
					break;
				}
			}

			return found;
		}

		private void fsw_Deleted(object sender, System.IO.FileSystemEventArgs e) {
			// Check if it's invisible
			if (Directory.Exists(e.FullPath)) {
				DirectoryInfo info = new DirectoryInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			} else {
				FileInfo info = new FileInfo(e.FullPath);
				if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
					return;
			}

			// Find the node in question
			TreeNode file = Find(e.FullPath);

			// Delete it
			if (file != null)
				file.Remove();


		}

		private int GetIcon(string file) {
			string ext = Path.GetExtension(file);
			
			int outicon = 3;
			if (ext == ".cs" || ext == ".mis")
				outicon = 4;
			else if (ext == ".gui")
				outicon = 5;
			else if (ext == ".txt" || ext == ".log")
				outicon = 2;
			else if (ext == ".exe" || ext == ".scr" || ext == ".pif" || ext == ".bat")
				outicon = 6;
			else if (ext == ".dll" || ext == ".ocx")
				outicon = 7;
			else if (ext == ".ico" || ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".bmp")
				outicon = 8;
			else
				outicon = 3;

			if (this.OnGetItemIcon != null)
				OnGetItemIcon(file, ref outicon);

			return outicon;
		}

		private void tvFiles_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				TreeNode node = this.tvFiles.GetNodeAt(e.X, e.Y);

				if (node == null)
					return;

				this.tvFiles.SelectedNode = node;
			}
		}

		private void tvFiles_DoubleClick(object sender, System.EventArgs e) {
			if (this._RootPath == "")
				return;


			if (this.OnItemActivate != null)
				OnItemActivate(this.SelectedFile);
		}

		private void tvFiles_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				tvFiles_DoubleClick(null, null);
		}

	}
}
