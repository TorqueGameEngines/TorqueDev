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
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.WinUICore;

namespace TSDev {

	/// <summary>
	/// Represents a Find and Replace form.
	/// </summary>
	internal class frmFind : System.Windows.Forms.Form {

		private SyntaxEditor		editor;
		private FindReplaceOptions	options;
		private Control			owner;
		private int ActiveDocument = 0;
		private bool bOnlyFiles = false;

		//
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox searchTypeCheckBox;
		private System.Windows.Forms.ComboBox searchTypeDropDownList;
		private System.Windows.Forms.CheckBox matchCaseCheckBox;
		private System.Windows.Forms.CheckBox matchWholeWordCheckBox;
		private System.Windows.Forms.CheckBox searchUpCheckBox;
		private System.Windows.Forms.Button markAllButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.CheckBox markWithCheckBox;
		private System.Windows.Forms.CheckBox searchInSelectionCheckBox;
		private System.Windows.Forms.CheckBox searchHiddenTextCheckBox;
		private System.Windows.Forms.Button findInsertButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton optCurFile;
		private System.Windows.Forms.RadioButton optSelection;
		private System.Windows.Forms.RadioButton optAllFiles;
		private System.Windows.Forms.RadioButton optEntireProject;
		private ComboBox findTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmFind(Control owner, SyntaxEditor editor, FindReplaceOptions options, bool OnlyFiles, string SearchStr) {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			RehashRecent();

			//
			// Add any constructor code after InitializeComponent call
			//
			this.owner		= owner;
			this.editor		= editor;
			this.options	= options;
			
			// Set the form owner
			this.Owner = g.Main;

			// Clear the status
			//owner.SetStatusMessage("Ready");

	        // Select the first search type
		    searchTypeDropDownList.SelectedIndex = 0;

			// Update options
			findTextBox.Text						= options.FindText;
			matchCaseCheckBox.Checked				= options.MatchCase;
			matchWholeWordCheckBox.Checked			= options.MatchWholeWord;
			searchUpCheckBox.Checked				= options.SearchUp;
			searchHiddenTextCheckBox.Checked		= options.SearchHiddenText;
			searchInSelectionCheckBox.Checked		= options.SearchInSelection;
			searchTypeCheckBox.Checked				= (options.SearchType != FindReplaceSearchType.Normal);
			searchTypeDropDownList.SelectedIndex	= (options.SearchType != FindReplaceSearchType.Wildcard ? 0 : 1);

			this.findTextBox.Text = SearchStr;

			if (OnlyFiles) {
				this.optAllFiles.Enabled = false;
				this.optCurFile.Enabled = false;
				this.optEntireProject.Checked = true;
				this.optSelection.Enabled = false;

				this.markAllButton.Enabled = false;
				this.Text = "Find in Project";
				this.markWithCheckBox.Enabled = false;
				this.markWithCheckBox.Checked = false;
				this.searchUpCheckBox.Enabled = false;
				this.searchHiddenTextCheckBox.Enabled = false;

				this.bOnlyFiles = OnlyFiles;
			}
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
			this.findButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.searchTypeCheckBox = new System.Windows.Forms.CheckBox();
			this.searchTypeDropDownList = new System.Windows.Forms.ComboBox();
			this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.matchWholeWordCheckBox = new System.Windows.Forms.CheckBox();
			this.searchUpCheckBox = new System.Windows.Forms.CheckBox();
			this.markAllButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.markWithCheckBox = new System.Windows.Forms.CheckBox();
			this.searchInSelectionCheckBox = new System.Windows.Forms.CheckBox();
			this.searchHiddenTextCheckBox = new System.Windows.Forms.CheckBox();
			this.findInsertButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.optCurFile = new System.Windows.Forms.RadioButton();
			this.optSelection = new System.Windows.Forms.RadioButton();
			this.optAllFiles = new System.Windows.Forms.RadioButton();
			this.optEntireProject = new System.Windows.Forms.RadioButton();
			this.findTextBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// findButton
			// 
			this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.findButton.Enabled = false;
			this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.findButton.Location = new System.Drawing.Point(377, 8);
			this.findButton.Name = "findButton";
			this.findButton.Size = new System.Drawing.Size(75, 23);
			this.findButton.TabIndex = 16;
			this.findButton.Text = "&Find Next";
			this.findButton.Click += new System.EventHandler(this.findButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Find what:";
			// 
			// searchTypeCheckBox
			// 
			this.searchTypeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchTypeCheckBox.Location = new System.Drawing.Point(16, 160);
			this.searchTypeCheckBox.Name = "searchTypeCheckBox";
			this.searchTypeCheckBox.Size = new System.Drawing.Size(58, 24);
			this.searchTypeCheckBox.TabIndex = 14;
			this.searchTypeCheckBox.Text = "Use";
			this.searchTypeCheckBox.CheckedChanged += new System.EventHandler(this.searchTypeCheckBox_CheckedChanged);
			// 
			// searchTypeDropDownList
			// 
			this.searchTypeDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.searchTypeDropDownList.Enabled = false;
			this.searchTypeDropDownList.Items.AddRange(new object[] {
            "Regular expressions",
            "Wildcards"});
			this.searchTypeDropDownList.Location = new System.Drawing.Point(64, 160);
			this.searchTypeDropDownList.Name = "searchTypeDropDownList";
			this.searchTypeDropDownList.Size = new System.Drawing.Size(267, 22);
			this.searchTypeDropDownList.TabIndex = 15;
			// 
			// matchCaseCheckBox
			// 
			this.matchCaseCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.matchCaseCheckBox.Location = new System.Drawing.Point(16, 32);
			this.matchCaseCheckBox.Name = "matchCaseCheckBox";
			this.matchCaseCheckBox.Size = new System.Drawing.Size(260, 24);
			this.matchCaseCheckBox.TabIndex = 3;
			this.matchCaseCheckBox.Text = "Match case";
			// 
			// matchWholeWordCheckBox
			// 
			this.matchWholeWordCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.matchWholeWordCheckBox.Location = new System.Drawing.Point(16, 56);
			this.matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
			this.matchWholeWordCheckBox.Size = new System.Drawing.Size(260, 24);
			this.matchWholeWordCheckBox.TabIndex = 5;
			this.matchWholeWordCheckBox.Text = "Match whole word";
			// 
			// searchUpCheckBox
			// 
			this.searchUpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchUpCheckBox.Location = new System.Drawing.Point(16, 80);
			this.searchUpCheckBox.Name = "searchUpCheckBox";
			this.searchUpCheckBox.Size = new System.Drawing.Size(260, 24);
			this.searchUpCheckBox.TabIndex = 7;
			this.searchUpCheckBox.Text = "Search up";
			// 
			// markAllButton
			// 
			this.markAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.markAllButton.Enabled = false;
			this.markAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.markAllButton.Location = new System.Drawing.Point(377, 48);
			this.markAllButton.Name = "markAllButton";
			this.markAllButton.Size = new System.Drawing.Size(75, 23);
			this.markAllButton.TabIndex = 17;
			this.markAllButton.Text = "&Mark All";
			this.markAllButton.Click += new System.EventHandler(this.markAllButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.closeButton.Location = new System.Drawing.Point(377, 80);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 18;
			this.closeButton.Text = "Close";
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// markWithCheckBox
			// 
			this.markWithCheckBox.Checked = true;
			this.markWithCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.markWithCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.markWithCheckBox.Location = new System.Drawing.Point(208, 56);
			this.markWithCheckBox.Name = "markWithCheckBox";
			this.markWithCheckBox.Size = new System.Drawing.Size(156, 24);
			this.markWithCheckBox.TabIndex = 6;
			this.markWithCheckBox.Text = "Mark with bookmarks";
			// 
			// searchInSelectionCheckBox
			// 
			this.searchInSelectionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchInSelectionCheckBox.Location = new System.Drawing.Point(208, 80);
			this.searchInSelectionCheckBox.Name = "searchInSelectionCheckBox";
			this.searchInSelectionCheckBox.Size = new System.Drawing.Size(156, 24);
			this.searchInSelectionCheckBox.TabIndex = 8;
			this.searchInSelectionCheckBox.Text = "Search in selection";
			this.searchInSelectionCheckBox.Visible = false;
			// 
			// searchHiddenTextCheckBox
			// 
			this.searchHiddenTextCheckBox.Checked = true;
			this.searchHiddenTextCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.searchHiddenTextCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchHiddenTextCheckBox.Location = new System.Drawing.Point(208, 32);
			this.searchHiddenTextCheckBox.Name = "searchHiddenTextCheckBox";
			this.searchHiddenTextCheckBox.Size = new System.Drawing.Size(156, 24);
			this.searchHiddenTextCheckBox.TabIndex = 4;
			this.searchHiddenTextCheckBox.Text = "Search hidden text";
			// 
			// findInsertButton
			// 
			this.findInsertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.findInsertButton.Enabled = false;
			this.findInsertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.findInsertButton.Location = new System.Drawing.Point(348, 8);
			this.findInsertButton.Name = "findInsertButton";
			this.findInsertButton.Size = new System.Drawing.Size(20, 20);
			this.findInsertButton.TabIndex = 2;
			this.findInsertButton.Text = ">";
			this.findInsertButton.Click += new System.EventHandler(this.findInsertButton_Click);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(16, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Search in:";
			// 
			// optCurFile
			// 
			this.optCurFile.Checked = true;
			this.optCurFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optCurFile.Location = new System.Drawing.Point(80, 112);
			this.optCurFile.Name = "optCurFile";
			this.optCurFile.Size = new System.Drawing.Size(88, 16);
			this.optCurFile.TabIndex = 10;
			this.optCurFile.TabStop = true;
			this.optCurFile.Text = "Current File";
			// 
			// optSelection
			// 
			this.optSelection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optSelection.Location = new System.Drawing.Point(80, 136);
			this.optSelection.Name = "optSelection";
			this.optSelection.Size = new System.Drawing.Size(88, 16);
			this.optSelection.TabIndex = 12;
			this.optSelection.Text = "Selection";
			// 
			// optAllFiles
			// 
			this.optAllFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optAllFiles.Location = new System.Drawing.Point(176, 112);
			this.optAllFiles.Name = "optAllFiles";
			this.optAllFiles.Size = new System.Drawing.Size(160, 16);
			this.optAllFiles.TabIndex = 11;
			this.optAllFiles.Text = "All Open Files";
			// 
			// optEntireProject
			// 
			this.optEntireProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optEntireProject.Location = new System.Drawing.Point(176, 136);
			this.optEntireProject.Name = "optEntireProject";
			this.optEntireProject.Size = new System.Drawing.Size(160, 16);
			this.optEntireProject.TabIndex = 13;
			this.optEntireProject.Text = "Entire Project";
			// 
			// findTextBox
			// 
			this.findTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.findTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.findTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.findTextBox.FormattingEnabled = true;
			this.findTextBox.Location = new System.Drawing.Point(93, 7);
			this.findTextBox.Name = "findTextBox";
			this.findTextBox.Size = new System.Drawing.Size(249, 22);
			this.findTextBox.TabIndex = 1;
			this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
			// 
			// frmFind
			// 
			this.AcceptButton = this.findButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(458, 191);
			this.Controls.Add(this.findTextBox);
			this.Controls.Add(this.optCurFile);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.findInsertButton);
			this.Controls.Add(this.searchHiddenTextCheckBox);
			this.Controls.Add(this.searchInSelectionCheckBox);
			this.Controls.Add(this.markWithCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.markAllButton);
			this.Controls.Add(this.searchUpCheckBox);
			this.Controls.Add(this.matchWholeWordCheckBox);
			this.Controls.Add(this.matchCaseCheckBox);
			this.Controls.Add(this.searchTypeDropDownList);
			this.Controls.Add(this.searchTypeCheckBox);
			this.Controls.Add(this.findButton);
			this.Controls.Add(this.optSelection);
			this.Controls.Add(this.optAllFiles);
			this.Controls.Add(this.optEntireProject);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFind";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			this.Load += new System.EventHandler(this.frmFindReplace_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Occurs when the form is closed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		protected override void OnClosed(EventArgs e) {
			// Clear the status
			//owner.SetStatusMessage("Ready");

			base.OnClosed(e);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void closeButton_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void findButton_Click(object sender, System.EventArgs e) {
			// Update find/replace options
			//if ((this.owner as UCEditor)._ParentTab.Selected != true)
			//	(this.owner as UCEditor)._ParentTab.Selected = true;

			this.UpdateFindReplaceOptions();

			if (editor == null && optEntireProject.Checked == false) {
				MessageBox.Show("No open windows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Save this last search in the list
			if (!g.Project.Finds.Contains(findTextBox.Text)) {
				// Remove old stuff
				if (g.Project.Finds.Count == 10)
					g.Project.Finds.RemoveAt(0);

				g.Project.Finds.Add(findTextBox.Text);
				g.Main.cboRecentSearches.Text = findTextBox.Text;

				RehashRecent();
			}

			// Set the status
			//owner.SetStatusMessage("Find: \"" + options.FindText + "\"");
			if (this.optCurFile.Checked || this.optSelection.Checked) {
				// Perform find operation on currently open file
				FindReplaceResultSet resultSet = null;

				options.SearchInSelection = this.optSelection.Checked;

				try {
					resultSet = editor.SelectedView.FindReplace.Find(options);
				} catch { return; }

				if (resultSet.PastEndOfDocument)
					MessageBox.Show(this, "Search past end of file; jumping to beginning.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);

				if (resultSet.Count == 0)
					MessageBox.Show(this, "Cannot find search string.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
				
			} else if (this.optAllFiles.Checked) {
				// Perform search on all open files
				PrimaryTab<UCEditor> ed = g.Editors[ActiveDocument];
				SyntaxEditor actdoc = ed.Control.txtEditor;
				ed.page.Selected = true;

				FindReplaceResultSet resultSet = null;

				try {
					resultSet = actdoc.SelectedView.FindReplace.Find(options);
				} catch { return; }

				if (resultSet.PastEndOfDocument || resultSet.Count == 0) {
					if (g.Editors.Count == (ActiveDocument + 1)) {
						MessageBox.Show(this, "All open files searched; jumping to beginning.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
						ActiveDocument = 0;
						return;
					} else {
						ActiveDocument++;
					}

					if (resultSet.Count == 0)
						findButton_Click(null, null);
				}
				
			} else if (this.optEntireProject.Checked) {
				// Perform search on all files
				UCFindResults fFindResults = new UCFindResults();
				fFindResults.lvFind.BeginUpdate();

				System.IO.Directory.SetCurrentDirectory(g.Project.ProjectPath);

				foreach(CProject.File file in g.Project.FileList) {
					Document doc = new Document();
					try {
						doc.LoadFile(System.IO.Path.GetFullPath(file.RelativePath));
					} catch { continue; }

					FindReplaceResultSet results = doc.FindReplace.FindAll(options);

					foreach(FindReplaceResult result in results) {
						ListViewItem item = new ListViewItem(file.RelativePath);
						item.SubItems.Add(Convert.ToString(doc.OffsetToPosition(result.Offset).Line + 1));
						item.SubItems.Add(doc.Lines[doc.OffsetToPosition(result.Offset).Line].Text);

						item.Tag = file;
						fFindResults.lvFind.Items.Add(item);
					}

					doc.Dispose();
					doc = null;
				}

				fFindResults.lvFind.EndUpdate();
				
				g.Main.ShowFindResults(fFindResults);
				this.Close();
			}
		}

		private void RehashRecent() {
			findTextBox.Items.Clear();
			g.Main.cboRecentSearches.Items.Clear();

			foreach (string recent in g.Project.Finds) {
				findTextBox.Items.Add(recent);
				g.Main.cboRecentSearches.Items.Add(recent);
			}
		}

		/// <summary>
		/// Occurs when the Text property is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void findTextBox_TextChanged(object sender, System.EventArgs e) {
            findButton.Enabled = (findTextBox.Text.Length > 0);

			if (!this.bOnlyFiles)
				markAllButton.Enabled = (findTextBox.Text.Length > 0);

			ActiveDocument = 0;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void markAllButton_Click(object sender, System.EventArgs e) {
			//if ((owner as UCEditor)._ParentTab.Selected != true)
			//	(owner as UCEditor)._ParentTab.Selected = true;
			
			// Update find/replace options
			this.UpdateFindReplaceOptions();

			if (editor == null) {
				MessageBox.Show("No open windows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (!g.Project.Finds.Contains(findTextBox.Text)) {
				// Remove old stuff
				if (g.Project.Finds.Count == 10)
					g.Project.Finds.RemoveAt(0);

				g.Project.Finds.Add(findTextBox.Text);
				g.Main.cboRecentSearches.Text = findTextBox.Text;

				RehashRecent();
			}

			
			if (this.optCurFile.Checked || this.optSelection.Checked) {
				options.SearchInSelection = this.optSelection.Checked;

				FindReplaceResultSet resultSet;

				try {
					if (markWithCheckBox.Checked)
						resultSet = editor.SelectedView.FindReplace.MarkAll(options, typeof(CIndicators.BookmarkIndicator));
					else
						resultSet = editor.SelectedView.FindReplace.MarkAll(options, typeof(GrammarErrorIndicator));
				} catch { return; }

				if (resultSet.Count == 0)
					MessageBox.Show(this, "Specified text was not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);

			} else if (this.optAllFiles.Checked) {
				// Flag all open files
				int num_success = 0;
				foreach (PrimaryTab<UCEditor> ed in g.Editors) {
					FindReplaceResultSet resultSet;

					try {
						if (markWithCheckBox.Checked)
							resultSet = ed.Control.txtEditor.SelectedView.FindReplace.MarkAll(options, typeof(CIndicators.BookmarkIndicator));
						else
							resultSet = ed.Control.txtEditor.SelectedView.FindReplace.MarkAll(options, typeof(GrammarErrorIndicator));
					} catch { continue; }

					num_success += resultSet.Count;
				}

				if (num_success == 0)
					MessageBox.Show(this, "Specified text was not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show(this, "Text flagged in " + num_success.ToString() + " place(s).", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else if (this.optEntireProject.Checked) {
				MessageBox.Show(this, "Marking text in entire project not supported.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void findInsertButton_Click(object sender, System.EventArgs e) {
            OwnerDrawContextMenu menu = new OwnerDrawContextMenu();
			if (searchTypeDropDownList.SelectedIndex == 0) {
				menu.MenuItems.Add(new OwnerDrawMenuItem(". Any single character", -1, new EventHandler(menuItem_Click), Shortcut.None, "."));
				menu.MenuItems.Add(new OwnerDrawMenuItem("* Zero or more", -1, new EventHandler(menuItem_Click), Shortcut.None, "*"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("+ One or more", -1, new EventHandler(menuItem_Click), Shortcut.None, "+"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("-"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("^ Beginning of line", -1, new EventHandler(menuItem_Click), Shortcut.None, "^"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("$ End of line", -1, new EventHandler(menuItem_Click), Shortcut.None, "$"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("\\b Word boundary", -1, new EventHandler(menuItem_Click), Shortcut.None, "\\b"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("\\s Whitespace", -1, new EventHandler(menuItem_Click), Shortcut.None, "\\s"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("\\n Line break", -1, new EventHandler(menuItem_Click), Shortcut.None, "\\n"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("-"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("[ ] Any one character in the set", -1, new EventHandler(menuItem_Click), Shortcut.None, "[ ]"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("[^ ] Any one character not in the set", -1, new EventHandler(menuItem_Click), Shortcut.None, "[^ ]"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("| Or", -1, new EventHandler(menuItem_Click), Shortcut.None, "|"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("\\ Escape special character", -1, new EventHandler(menuItem_Click), Shortcut.None, "\\"));
			}
			else {
				menu.MenuItems.Add(new OwnerDrawMenuItem("* Zero or more of any character", -1, new EventHandler(menuItem_Click), Shortcut.None, "*"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("? Any single character", -1, new EventHandler(menuItem_Click), Shortcut.None, "?"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("# Any single digit", -1, new EventHandler(menuItem_Click), Shortcut.None, "#"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("[ ] Any one character in the set", -1, new EventHandler(menuItem_Click), Shortcut.None, "[ ]"));
				menu.MenuItems.Add(new OwnerDrawMenuItem("[! ] Any one character not in the set", -1, new EventHandler(menuItem_Click), Shortcut.None, "[! ]"));
			}
			menu.Show(findInsertButton, new Point(findInsertButton.ClientRectangle.Right, findInsertButton.ClientRectangle.Top));
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void menuItem_Click(object sender, System.EventArgs e) {
			findTextBox.SelectedText = ((OwnerDrawMenuItem)sender).Tag.ToString();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void replaceButton_Click(object sender, System.EventArgs e) {
			// Update find/replace options
			this.UpdateFindReplaceOptions();

			// Set the status
			//owner.SetStatusMessage("Replace: \"" + options.FindText + "\", with: \"" + options.ReplaceText + "\"");

			// Perform a find operation
			FindReplaceResultSet resultSet;
			try {
				resultSet = editor.SelectedView.FindReplace.Replace(options);
			}
			catch (Exception ex) {
				MessageBox.Show(this, "An error occurred:\r\n" + ex.Message, "Find/Replace Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Set the status
			//if (resultSet.PastEndOfDocument)
			//	owner.SetStatusMessage("Past the end of the document");

			// If no matches were found...			
			if (resultSet.Count == 0) {
				if ((resultSet.PastEndOfDocument) && (resultSet.ReplaceOccurred))
					MessageBox.Show(this, "Find reached the starting point of the search.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show(this, "The specified text was not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void replaceAllButton_Click(object sender, System.EventArgs e) {
			// Update find/replace options
			this.UpdateFindReplaceOptions();
		
			// Perform a mark all operation
			FindReplaceResultSet resultSet;
			try {
				resultSet = editor.SelectedView.FindReplace.ReplaceAll(options);
			}
			catch (Exception ex) {
				MessageBox.Show(this, "An error occurred:\r\n" + ex.Message, "Find/Replace Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// If no matches were found...
			if (resultSet.Count == 0) {
				MessageBox.Show(this, "The specified text was not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			// Display the number of replacements
			MessageBox.Show(this, resultSet.Count + " occurrence(s) replaced.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Occurs when the Checked property is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void searchTypeCheckBox_CheckedChanged(object sender, System.EventArgs e) {
			findInsertButton.Enabled = searchTypeCheckBox.Checked;
			searchTypeDropDownList.Enabled = searchTypeCheckBox.Checked;
		}

		/// <summary>
		/// Updates the find/replace options.
		/// </summary>
		private void UpdateFindReplaceOptions() {
			//this.editor					= (owner as UCEditor).txtEditor;

			if (g.Main.GetActiveEditor() != null)
				this.editor					= g.Main.GetActiveEditor().txtEditor;
			else
				this.editor = null;

			options.FindText			= findTextBox.Text;
			options.MatchCase			= matchCaseCheckBox.Checked;
			options.MatchWholeWord		= matchWholeWordCheckBox.Checked;
			options.SearchHiddenText	= searchHiddenTextCheckBox.Checked;
			options.SearchInSelection	= searchInSelectionCheckBox.Checked;
			options.SearchUp			= searchUpCheckBox.Checked;
			options.SearchType			= (!searchTypeCheckBox.Checked ? FindReplaceSearchType.Normal :
											(searchTypeDropDownList.SelectedIndex == 0 ? FindReplaceSearchType.RegularExpression : FindReplaceSearchType.Wildcard));
		}

		private void frmFindReplace_Load(object sender, System.EventArgs e) {
			findTextBox.Focus();
			findTextBox.Select();
		}



	}
}
