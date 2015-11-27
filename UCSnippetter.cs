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
using System.Net;

namespace TSDev
{
	/// <summary>
	/// Summary description for UCSnippetter.
	/// </summary>
	internal class UCSnippetter : System.Windows.Forms.UserControl {
		private System.Windows.Forms.TreeView tvSnippets;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ImageList ilSnippets;
		private System.Windows.Forms.Panel panel1;
		private ActiproSoftware.MarkupLabel.MarkupLabel lblDescr;
		private ActiproSoftware.WinUICore.OwnerDrawContextMenu ctmSnippets;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetAdd;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetDel;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem menuItem3;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetCopy;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetEdit;
		private System.Windows.Forms.Panel panel2;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetImport;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Button cmdSearch;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuSnippetToClipboard;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem menuItem1;
		private System.ComponentModel.IContainer components;

		public UCSnippetter() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.tvSnippets.Nodes.Clear();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
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
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UCSnippetter));
			this.tvSnippets = new System.Windows.Forms.TreeView();
			this.ctmSnippets = new ActiproSoftware.WinUICore.OwnerDrawContextMenu();
			this.mnuSnippetAdd = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuSnippetEdit = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuSnippetDel = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.menuItem3 = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuSnippetImport = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuSnippetCopy = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuSnippetToClipboard = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.ilSnippets = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDescr = new ActiproSoftware.MarkupLabel.MarkupLabel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.menuItem1 = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvSnippets
			// 
			this.tvSnippets.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvSnippets.ContextMenu = this.ctmSnippets;
			this.tvSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvSnippets.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tvSnippets.ImageList = this.ilSnippets;
			this.tvSnippets.Location = new System.Drawing.Point(0, 24);
			this.tvSnippets.Name = "tvSnippets";
			this.tvSnippets.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				   new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
																																									  new System.Windows.Forms.TreeNode("Node3")}),
																				   new System.Windows.Forms.TreeNode("Node1"),
																				   new System.Windows.Forms.TreeNode("Node2")});
			this.tvSnippets.ShowPlusMinus = false;
			this.tvSnippets.ShowRootLines = false;
			this.tvSnippets.Size = new System.Drawing.Size(216, 331);
			this.tvSnippets.TabIndex = 0;
			this.tvSnippets.DoubleClick += new System.EventHandler(this.tvSnippets_DoubleClick);
			this.tvSnippets.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSnippets_AfterSelect);
			// 
			// ctmSnippets
			// 
			this.ctmSnippets.MenuItems.AddRange(new ActiproSoftware.WinUICore.OwnerDrawMenuItem[] {
																						this.mnuSnippetAdd,
																						this.mnuSnippetEdit,
																						this.mnuSnippetDel,
																						this.menuItem3,
																						this.mnuSnippetImport,
																						this.menuItem1,
																						this.mnuSnippetToClipboard,
																						this.mnuSnippetCopy});
			// 
			// mnuSnippetAdd
			// 
			this.mnuSnippetAdd.Index = 0;
			this.mnuSnippetAdd.OwnerDraw = true;
			this.mnuSnippetAdd.Text = "&Add New Snippet...";
			this.mnuSnippetAdd.Click += new System.EventHandler(this.mnuSnippetAdd_Click);
			// 
			// mnuSnippetEdit
			// 
			this.mnuSnippetEdit.Index = 1;
			this.mnuSnippetEdit.OwnerDraw = true;
			this.mnuSnippetEdit.Text = "E&dit Snippet";
			this.mnuSnippetEdit.Click += new System.EventHandler(this.mnuSnippetEdit_Click);
			// 
			// mnuSnippetDel
			// 
			this.mnuSnippetDel.Index = 2;
			this.mnuSnippetDel.OwnerDraw = true;
			this.mnuSnippetDel.Text = "D&elete Snippet";
			this.mnuSnippetDel.Click += new System.EventHandler(this.mnuSnippetDel_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.OwnerDraw = true;
			this.menuItem3.Text = "-";
			// 
			// mnuSnippetImport
			// 
			this.mnuSnippetImport.Index = 4;
			this.mnuSnippetImport.OwnerDraw = true;
			this.mnuSnippetImport.Text = "&Import Snippet...";
			this.mnuSnippetImport.Click += new System.EventHandler(this.mnuSnippetImport_Click);
			// 
			// mnuSnippetCopy
			// 
			this.mnuSnippetCopy.Index = 7;
			this.mnuSnippetCopy.OwnerDraw = true;
			this.mnuSnippetCopy.Text = "&Copy Shortcut";
			this.mnuSnippetCopy.Click += new System.EventHandler(this.mnuSnippetCopy_Click);
			// 
			// mnuSnippetToClipboard
			// 
			this.mnuSnippetToClipboard.Index = 6;
			this.mnuSnippetToClipboard.OwnerDraw = true;
			this.mnuSnippetToClipboard.Text = "Copy Co&de to Clipboard";
			this.mnuSnippetToClipboard.Click += new System.EventHandler(this.mnuSnippetToClipboard_Click);
			// 
			// ilSnippets
			// 
			this.ilSnippets.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilSnippets.ImageSize = new System.Drawing.Size(16, 16);
			this.ilSnippets.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSnippets.ImageStream")));
			this.ilSnippets.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 355);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(216, 1);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel1.Controls.Add(this.lblDescr);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 356);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(216, 100);
			this.panel1.TabIndex = 3;
			// 
			// lblDescr
			// 
			this.lblDescr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDescr.DockPadding.All = 10;
			this.lblDescr.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDescr.Location = new System.Drawing.Point(0, 0);
			this.lblDescr.MaxWidth = 2147483647;
			this.lblDescr.Name = "lblDescr";
			this.lblDescr.Size = new System.Drawing.Size(216, 100);
			this.lblDescr.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtSearch);
			this.panel2.Controls.Add(this.cmdSearch);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(216, 24);
			this.panel2.TabIndex = 0;
			// 
			// txtSearch
			// 
			this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSearch.Location = new System.Drawing.Point(0, 0);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(141, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "";
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			// 
			// cmdSearch
			// 
			this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSearch.Location = new System.Drawing.Point(141, 0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(75, 24);
			this.cmdSearch.TabIndex = 1;
			this.cmdSearch.Text = "Search";
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 5;
			this.menuItem1.Text = "-";
			// 
			// UCSnippetter
			// 
			this.Controls.Add(this.tvSnippets);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(178)));
			this.Name = "UCSnippetter";
			this.Size = new System.Drawing.Size(216, 456);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void RefreshTree() {
			// Clear any searches
			this.txtSearch.Enabled = true;
			this.cmdSearch.Text = "Search";

			RefreshTree(frmMain.stc_Snippets.CodeSnippets);
		}

		public void RefreshTree(CSnippetter.CodeEntryCollection codecoll) {
			// Clear the nodes and set the update flag so we don't redraw the control all the time.
			this.tvSnippets.Nodes.Clear();
			this.tvSnippets.BeginUpdate();

			// Loop through all the code snippets and gather a list of categories
			ArrayList cats = new ArrayList();

			foreach(CSnippetter.CodeEntry entry in codecoll) {
				if (!cats.Contains(entry.CodeCategory))
					cats.Add(entry.CodeCategory);
			}

			// Add the categories to the snippetter
			foreach(string entry in cats) {
				TreeNode node = new TreeNode(entry, 0, 0);
				this.tvSnippets.Nodes.Add(node);
			}

			// Run a second pass on all the code snippets and add them to the select
			// categories
			foreach(CSnippetter.CodeEntry entry in codecoll) {
				TreeNode parent = GetCategory(entry.CodeCategory);

				if (parent == null)				// This should never happen
					continue;

				TreeNode node = new TreeNode(entry.CodeTitle, 6, 6);
				node.Tag = entry;

				parent.Nodes.Add(node);
			}

			// Allow updates to the treeview and expand all nodes
			this.tvSnippets.ExpandAll();
			this.tvSnippets.EndUpdate();

			// Sort the TV
			this.tvSnippets.Sorted = true;
		}

		public TreeNode GetCategory(string catname) {
			foreach(TreeNode node in this.tvSnippets.Nodes) {
				if (node.Text.ToLower() == catname.ToLower())
					return node;
			}

			return null;
		}

		private void tvSnippets_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			TreeNode node = this.tvSnippets.SelectedNode;

			if (node.Tag == null) {
				this.lblDescr.Text = "Category <b>" + node.Text + "</b>";
			} else if (node.Tag is CSnippetter.CodeEntry) {
				CSnippetter.CodeEntry entry = (CSnippetter.CodeEntry)node.Tag;
				this.lblDescr.Text = "Snippet <b>" + entry.CodeTitle + "</b><br />" + entry.CodeDescr;
			}
		}

		private void mnuSnippetAdd_Click(object sender, System.EventArgs e) {
			frmSnippetNew fSnippetNew = new frmSnippetNew("");
			fSnippetNew.Show();
		}

		private void mnuSnippetEdit_Click(object sender, System.EventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			if (this.tvSnippets.SelectedNode.Tag == null)
				return;

			frmSnippetNew fSnippetNew = new frmSnippetNew((CSnippetter.CodeEntry)this.tvSnippets.SelectedNode.Tag, false, false);
			fSnippetNew.Show();
		}

		private void mnuSnippetDel_Click(object sender, System.EventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			if (this.tvSnippets.SelectedNode.Tag == null)
				return;

			frmMain.stc_Snippets.CodeSnippets.Remove((CSnippetter.CodeEntry)this.tvSnippets.SelectedNode.Tag);
			RefreshTree();
		}

		private void tvSnippets_DoubleClick(object sender, System.EventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			if (this.tvSnippets.SelectedNode.Tag == null)
				return;

			if (g.Main.GetActiveEditor() == null)
				return;

			// Grab the code we're inserting
			string code = (this.tvSnippets.SelectedNode.Tag as CSnippetter.CodeEntry).CodeContents;

			// Determine the indent level of the current editor
			int indent = g.Main.GetActiveEditor().txtEditor.SelectedView.CurrentDocumentLine.GetTabStopLevel();

			// Loop through each line in CODE and insert that many tabs
			string[] code_array = code.Replace("\r", "").Split('\n');
			string final = "";

			foreach(string line in code_array)
				final += (new string('\t', indent)) + line + "\r\n";

			// Insert the text at the cursor
            g.Main.GetActiveEditor().txtEditor.SelectedView.InsertText(final);
		}

		private void mnuSnippetCopy_Click(object sender, System.EventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			if (this.tvSnippets.SelectedNode.Tag == null)
				return;

			// Grab the instance of the snippet we want
			CSnippetter.CodeEntry code = (CSnippetter.CodeEntry)this.tvSnippets.SelectedNode.Tag;

			// Do we need to fetch it?
			if (!(code.CodeURL == "" || DateTime.Now.Ticks >= code.CodeExpires)) {
				// No.  Paste the current URL onto the clipboard
				Clipboard.SetDataObject(code.CodeURL, true);
				return;
			}

			// Construct a new web request to fetch (and post) the code
			WebClient client = new WebClient();
			System.Collections.Specialized.NameValueCollection postdata = new System.Collections.Specialized.NameValueCollection();
			
			// Write the querystring
			client.QueryString.Add("op", "upload_snippet");

			postdata.Add("code", code.CodeContents);
			postdata.Add("name", code.CodeTitle);
			postdata.Add("descr", code.CodeDescr);
			postdata.Add("category", code.CodeCategory);
			postdata.Add("keywords", CSnippetter.ParseKeywords(code.CodeKeywords));

			// Send the request to the server
			string url = "";
			
			try { 
				url = System.Text.ASCIIEncoding.ASCII.GetString(client.UploadValues("http://www.torquedev.com/network/snippetter.php", "POST", postdata));
			} catch {
				MessageBox.Show(this, "Failed to connect to Snippetter server.  You must be connected to the internet to perform this action.  Please check your connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (url == "") {
				MessageBox.Show(this, "Failed to retrieve URL.  Upload failed for some reason.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Copy the URL to the object and set the expiration
			code.CodeURL = url;
			code.CodeExpires = DateTime.Now.Ticks + (new TimeSpan(30, 0, 0, 0, 0)).Ticks;

			// Copy the URL to the clipboard
			Clipboard.SetDataObject(url, true);
		}

		private void cmdSearch_Click(object sender, System.EventArgs e) {
			if (txtSearch.Text == "" || cmdSearch.Text == "Clear") {
				RefreshTree();
				txtSearch.Enabled = true;
				cmdSearch.Text = "Search";
				return;
			}

			CSnippetter.CodeEntryCollection coll = frmMain.stc_Snippets.CodeSnippets.Search(txtSearch.Text.Split(' '));

			if (coll.Count == 0) {
				MessageBox.Show(this, "Could not find search keywords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			RefreshTree(coll);
			cmdSearch.Text = "Clear";
			txtSearch.Enabled = false;
		}

		private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				cmdSearch_Click(null, null);
		}

		private void mnuSnippetImport_Click(object sender, System.EventArgs e) {
			string id = frmMain.ShowPrompt("Import Snippet", "Please enter the ID of the snippet you wish to import.", "Enter snippet ID:");

			if (id == "")
				return;

			// Construct a web server request
			WebClient client = new WebClient();

			// Populate the querystrings
			client.QueryString.Add("op", "fetch");
			client.QueryString.Add("id", id);

			// Execute the request
			string code = System.Text.ASCIIEncoding.ASCII.GetString(client.DownloadData("http://www.torquedev.com/network/snippetter.php"));

			// Check if we're successful
			if (client.ResponseHeaders["X-Snippet-Success"] == null || client.ResponseHeaders["X-Snippet-Success"] == "False") {
				MessageBox.Show(this, "Failed to retrieve snippet.  Snippet does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Create the new snippet
			CSnippetter.CodeEntry entry = new TSDev.CSnippetter.CodeEntry();

			// Populate the fields
			entry.CodeCategory = client.ResponseHeaders["X-Snippet-Category"];
			entry.CodeTitle = client.ResponseHeaders["X-Snippet-Title"];
			entry.CodeDescr = client.ResponseHeaders["X-Snippet-Description"];
			string keywords = client.ResponseHeaders["X-Snippet-Keywords"];

			// Process keywords
			foreach(string keyword in keywords.Trim().Split(' '))
				entry.CodeKeywords.Add(keyword);
			
			// Write the codeblock
			entry.CodeContents = code;

			// Update the URL and date
			entry.CodeURL = "http://www.torquedev.com/network/snippetter.php?op=showcode&id=" + id;
			entry.CodeExpires = DateTime.Now.Ticks + (new TimeSpan(30, 0, 0, 0, 0)).Ticks;

			// Spawn a snippetter edit window to make any changes
			frmSnippetNew fSnippetNew = new frmSnippetNew(entry, true, false);
			fSnippetNew.ShowInTaskbar = false;

			DialogResult result = fSnippetNew.ShowDialog();

			// If they cancelled, then don't add this to the collection
			if (result == DialogResult.Cancel)
				return;

			// Add it to the collection
			frmMain.stc_Snippets.CodeSnippets.Add(entry);

			// Refresh the list
			RefreshTree();

			
		}

		private void mnuSnippetToClipboard_Click(object sender, System.EventArgs e) {
			if (this.tvSnippets.SelectedNode == null)
				return;

			if (this.tvSnippets.SelectedNode.Tag == null)
				return;

			Clipboard.SetDataObject((this.tvSnippets.SelectedNode.Tag as CSnippetter.CodeEntry).CodeContents);
		}

	}
}
