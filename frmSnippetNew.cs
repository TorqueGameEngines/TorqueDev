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

namespace TSDev
{
	/// <summary>
	/// Summary description for frmSnippetNew.
	/// </summary>
	internal class frmSnippetNew : System.Windows.Forms.Form
	{
		private bool isEditing = false;
		private CSnippetter.CodeEntry code_e = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdCreate;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtDescr;
		private System.Windows.Forms.TextBox txtKeywords;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor txtCode;
		private System.Windows.Forms.TextBox txtCategory;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSnippetNew(CSnippetter.CodeEntry ExistingEntry, bool imported, bool viewonly) {
			// Constructor for editing existing snippets
			InitializeComponent();

			this.isEditing = true;
			this.code_e = ExistingEntry;
			this.cmdCreate.Text = "Edit";

			// Bind the event handler for the edit event
			this.cmdCreate.Click += new EventHandler(cmdEdit_Click);

			// Turn the keywords into a readable listing
			string keywords = "";

			foreach(string keyword in ExistingEntry.CodeKeywords)
				keywords += keyword + " ";

			// Populate the fields
			this.txtCategory.Text = ExistingEntry.CodeCategory;
			this.txtCode.Document.Text = ExistingEntry.CodeContents;
			this.txtDescr.Text = ExistingEntry.CodeDescr;
			this.txtKeywords.Text = keywords.Trim();
			this.txtTitle.Text = ExistingEntry.CodeTitle;

			// Check if we're only viewing
			if (viewonly) {
				this.txtCategory.ReadOnly = true;
				this.txtCode.Document.ReadOnly = true;
				this.txtDescr.ReadOnly = true;
				this.txtKeywords.ReadOnly = true;
				this.txtTitle.ReadOnly = true;

				this.cmdCreate.Visible = false;
				this.cmdCancel.Text = "Close";

				this.Text = "Snippet Imported";
			} else {
				this.Text = "Edit Snippet";
			}

			if (imported) {
				this.Text = "Import Snippet";
				this.cmdCreate.Text = "Import";
			}
		}

		public frmSnippetNew(string ExistingCode)
		{
			// Constructor for new snippets
			InitializeComponent();

			this.txtCode.Document.Text = ExistingCode;

			// Bind the event handler for the create event
			this.cmdCreate.Click += new EventHandler(cmdCreate_Click);
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSnippetNew));
			this.label1 = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtDescr = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtKeywords = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCode = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdCreate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Snippet Title:";
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.Location = new System.Drawing.Point(128, 8);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(536, 20);
			this.txtTitle.TabIndex = 0;
			this.txtTitle.Text = "";
			// 
			// txtDescr
			// 
			this.txtDescr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescr.Location = new System.Drawing.Point(128, 40);
			this.txtDescr.Name = "txtDescr";
			this.txtDescr.Size = new System.Drawing.Size(536, 20);
			this.txtDescr.TabIndex = 1;
			this.txtDescr.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Snippet Description:";
			// 
			// txtKeywords
			// 
			this.txtKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeywords.Location = new System.Drawing.Point(128, 104);
			this.txtKeywords.Name = "txtKeywords";
			this.txtKeywords.Size = new System.Drawing.Size(536, 20);
			this.txtKeywords.TabIndex = 3;
			this.txtKeywords.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Snippet Keywords:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(320, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(344, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "(Separate keywords using spaces only)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.Document = document1;
			this.txtCode.Location = new System.Drawing.Point(8, 152);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(656, 392);
			this.txtCode.SplitType = ActiproSoftware.SyntaxEditor.SyntaxEditorSplitType.None;
			this.txtCode.TabIndex = 4;
			// 
			// txtCategory
			// 
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCategory.Location = new System.Drawing.Point(128, 72);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(536, 20);
			this.txtCategory.TabIndex = 2;
			this.txtCategory.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Snippet Category:";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(568, 552);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(96, 24);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCreate
			// 
			this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCreate.Location = new System.Drawing.Point(456, 552);
			this.cmdCreate.Name = "cmdCreate";
			this.cmdCreate.Size = new System.Drawing.Size(96, 24);
			this.cmdCreate.TabIndex = 5;
			this.cmdCreate.Text = "Create";
			// 
			// frmSnippetNew
			// 
			this.AcceptButton = this.cmdCreate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(672, 589);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDescr);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtKeywords);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmdCreate);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSnippetNew";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Code Snippet";
			this.Load += new System.EventHandler(this.frmSnippetNew_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSnippetNew_Load(object sender, System.EventArgs e) {
			// Load syntax highlighting XML
			txtCode.Document.LoadLanguageFromXml(Application.StartupPath + "\\highlight.bin", 5000);

			// Load proper formatting
			g.Config.LoadColorData(this.txtCode);
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
			this.txtCode.Dispose();
			this.Dispose();
		}

		private void cmdEdit_Click(object sender, System.EventArgs e) {
			if (this.txtCategory.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter a category to assign to this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtTitle.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter a title to assign to this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtCode.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter some code for this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Replace all the current variables
			this.code_e.CodeCategory = this.txtCategory.Text;
			this.code_e.CodeContents = this.txtCode.Text;
			this.code_e.CodeDescr = this.txtDescr.Text;
			this.code_e.CodeTitle = this.txtTitle.Text;

			// Reset the expiration so we have to re-upload
			this.code_e.CodeExpires = 0;

			// Create the keywords
			txtKeywords.Text = txtKeywords.Text.Replace(",", "");

			// Delete the existing keywords
			this.code_e.CodeKeywords = new ArrayList();

			foreach(string keyword in txtKeywords.Text.Split(' ')) {
				code_e.CodeKeywords.Add(keyword);
			}

			// Refresh the main treeview
			//g.Main.SnippetterUC.RefreshTree();

			// Close this window and dispose it
			this.DialogResult = DialogResult.OK;

			this.Close();
			this.txtCode.Dispose();
			this.Dispose();
		}

		private void cmdCreate_Click(object sender, System.EventArgs e) {
			if (this.txtCategory.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter a category to assign to this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtTitle.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter a title to assign to this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtCode.Text.Trim() == "") {
				MessageBox.Show(this, "Please enter some code for this snippet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Create the snippet object
			CSnippetter.CodeEntry entry = new TSDev.CSnippetter.CodeEntry(txtTitle.Text, txtDescr.Text, txtCode.Text, txtCategory.Text);
			
			// Create the keywords
			txtKeywords.Text = txtKeywords.Text.Replace(",", "");

			foreach(string keyword in txtKeywords.Text.Split(' ')) {
				entry.CodeKeywords.Add(keyword);
			}

			// Insert the snippet into the collection
			frmMain.stc_Snippets.CodeSnippets.Add(entry);

			// Refresh the main treeview
			//g.Main.SnippetterUC.RefreshTree();

			// Close this window and dispose it
			this.DialogResult = DialogResult.OK;

			this.Close();
			this.txtCode.Dispose();
			this.Dispose();

		}

	}
}
