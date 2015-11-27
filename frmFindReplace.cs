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
	internal class frmFindReplace : System.Windows.Forms.Form {

		private SyntaxEditor		editor;
		private FindReplaceOptions	options;
		private Control			owner;
		private int ActiveDocument = 0;

		//
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox searchTypeCheckBox;
		private System.Windows.Forms.ComboBox searchTypeDropDownList;
		private System.Windows.Forms.CheckBox matchCaseCheckBox;
		private System.Windows.Forms.CheckBox matchWholeWordCheckBox;
		private System.Windows.Forms.CheckBox searchUpCheckBox;
		private System.Windows.Forms.Button replaceButton;
		private System.Windows.Forms.Button replaceAllButton;
		private System.Windows.Forms.Button markAllButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox markWithCheckBox;
		private System.Windows.Forms.CheckBox searchInSelectionCheckBox;
		private System.Windows.Forms.CheckBox searchHiddenTextCheckBox;
		private System.Windows.Forms.Button findInsertButton;
		private System.Windows.Forms.RadioButton optCurFile;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton optSelection;
		private System.Windows.Forms.RadioButton optAllFiles;
		private ComboBox findTextBox;
		private ComboBox replaceTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmFindReplace(Control owner, SyntaxEditor editor, FindReplaceOptions options, string DefaultText) {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			replaceTextBox.Text						= options.ReplaceText;
			matchCaseCheckBox.Checked				= options.MatchCase;
			matchWholeWordCheckBox.Checked			= options.MatchWholeWord;
			searchUpCheckBox.Checked				= options.SearchUp;
			searchHiddenTextCheckBox.Checked		= options.SearchHiddenText;
			searchInSelectionCheckBox.Checked		= options.SearchInSelection;
			searchTypeCheckBox.Checked				= (options.SearchType != FindReplaceSearchType.Normal);
			searchTypeDropDownList.SelectedIndex	= (options.SearchType != FindReplaceSearchType.Wildcard ? 0 : 1);

			this.findTextBox.Text = DefaultText;
			RehashRecent();
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
			this.replaceButton = new System.Windows.Forms.Button();
			this.replaceAllButton = new System.Windows.Forms.Button();
			this.markAllButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.markWithCheckBox = new System.Windows.Forms.CheckBox();
			this.searchInSelectionCheckBox = new System.Windows.Forms.CheckBox();
			this.searchHiddenTextCheckBox = new System.Windows.Forms.CheckBox();
			this.findInsertButton = new System.Windows.Forms.Button();
			this.optCurFile = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.optSelection = new System.Windows.Forms.RadioButton();
			this.optAllFiles = new System.Windows.Forms.RadioButton();
			this.findTextBox = new System.Windows.Forms.ComboBox();
			this.replaceTextBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// findButton
			// 
			this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.findButton.Enabled = false;
			this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.findButton.Location = new System.Drawing.Point(376, 8);
			this.findButton.Name = "findButton";
			this.findButton.Size = new System.Drawing.Size(75, 23);
			this.findButton.TabIndex = 17;
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
			this.searchTypeCheckBox.Location = new System.Drawing.Point(8, 192);
			this.searchTypeCheckBox.Name = "searchTypeCheckBox";
			this.searchTypeCheckBox.Size = new System.Drawing.Size(58, 24);
			this.searchTypeCheckBox.TabIndex = 15;
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
			this.searchTypeDropDownList.Location = new System.Drawing.Point(56, 192);
			this.searchTypeDropDownList.Name = "searchTypeDropDownList";
			this.searchTypeDropDownList.Size = new System.Drawing.Size(267, 22);
			this.searchTypeDropDownList.TabIndex = 16;
			// 
			// matchCaseCheckBox
			// 
			this.matchCaseCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.matchCaseCheckBox.Location = new System.Drawing.Point(8, 64);
			this.matchCaseCheckBox.Name = "matchCaseCheckBox";
			this.matchCaseCheckBox.Size = new System.Drawing.Size(260, 24);
			this.matchCaseCheckBox.TabIndex = 5;
			this.matchCaseCheckBox.Text = "Match case";
			// 
			// matchWholeWordCheckBox
			// 
			this.matchWholeWordCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.matchWholeWordCheckBox.Location = new System.Drawing.Point(8, 112);
			this.matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
			this.matchWholeWordCheckBox.Size = new System.Drawing.Size(260, 24);
			this.matchWholeWordCheckBox.TabIndex = 9;
			this.matchWholeWordCheckBox.Text = "Match whole word";
			// 
			// searchUpCheckBox
			// 
			this.searchUpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchUpCheckBox.Location = new System.Drawing.Point(8, 88);
			this.searchUpCheckBox.Name = "searchUpCheckBox";
			this.searchUpCheckBox.Size = new System.Drawing.Size(260, 24);
			this.searchUpCheckBox.TabIndex = 7;
			this.searchUpCheckBox.Text = "Search up";
			// 
			// replaceButton
			// 
			this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.replaceButton.Enabled = false;
			this.replaceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.replaceButton.Location = new System.Drawing.Point(376, 40);
			this.replaceButton.Name = "replaceButton";
			this.replaceButton.Size = new System.Drawing.Size(75, 23);
			this.replaceButton.TabIndex = 18;
			this.replaceButton.Text = "&Replace";
			this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
			// 
			// replaceAllButton
			// 
			this.replaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.replaceAllButton.Enabled = false;
			this.replaceAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.replaceAllButton.Location = new System.Drawing.Point(376, 72);
			this.replaceAllButton.Name = "replaceAllButton";
			this.replaceAllButton.Size = new System.Drawing.Size(75, 23);
			this.replaceAllButton.TabIndex = 19;
			this.replaceAllButton.Text = "Replace &All";
			this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
			// 
			// markAllButton
			// 
			this.markAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.markAllButton.Enabled = false;
			this.markAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.markAllButton.Location = new System.Drawing.Point(120, 304);
			this.markAllButton.Name = "markAllButton";
			this.markAllButton.Size = new System.Drawing.Size(75, 23);
			this.markAllButton.TabIndex = 14;
			this.markAllButton.Text = "&Mark All";
			this.markAllButton.Visible = false;
			this.markAllButton.Click += new System.EventHandler(this.markAllButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.closeButton.Location = new System.Drawing.Point(376, 120);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 20;
			this.closeButton.Text = "Close";
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Replace with:";
			// 
			// markWithCheckBox
			// 
			this.markWithCheckBox.Checked = true;
			this.markWithCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.markWithCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.markWithCheckBox.Location = new System.Drawing.Point(200, 88);
			this.markWithCheckBox.Name = "markWithCheckBox";
			this.markWithCheckBox.Size = new System.Drawing.Size(156, 24);
			this.markWithCheckBox.TabIndex = 8;
			this.markWithCheckBox.Text = "Mark with bookmarks";
			this.markWithCheckBox.Visible = false;
			// 
			// searchInSelectionCheckBox
			// 
			this.searchInSelectionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchInSelectionCheckBox.Location = new System.Drawing.Point(200, 112);
			this.searchInSelectionCheckBox.Name = "searchInSelectionCheckBox";
			this.searchInSelectionCheckBox.Size = new System.Drawing.Size(156, 24);
			this.searchInSelectionCheckBox.TabIndex = 10;
			this.searchInSelectionCheckBox.Text = "Search in selection";
			this.searchInSelectionCheckBox.Visible = false;
			// 
			// searchHiddenTextCheckBox
			// 
			this.searchHiddenTextCheckBox.Checked = true;
			this.searchHiddenTextCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.searchHiddenTextCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.searchHiddenTextCheckBox.Location = new System.Drawing.Point(200, 64);
			this.searchHiddenTextCheckBox.Name = "searchHiddenTextCheckBox";
			this.searchHiddenTextCheckBox.Size = new System.Drawing.Size(156, 24);
			this.searchHiddenTextCheckBox.TabIndex = 6;
			this.searchHiddenTextCheckBox.Text = "Search hidden text";
			// 
			// findInsertButton
			// 
			this.findInsertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.findInsertButton.Enabled = false;
			this.findInsertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.findInsertButton.Location = new System.Drawing.Point(347, 8);
			this.findInsertButton.Name = "findInsertButton";
			this.findInsertButton.Size = new System.Drawing.Size(20, 20);
			this.findInsertButton.TabIndex = 2;
			this.findInsertButton.Text = ">";
			this.findInsertButton.Click += new System.EventHandler(this.findInsertButton_Click);
			// 
			// optCurFile
			// 
			this.optCurFile.Checked = true;
			this.optCurFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optCurFile.Location = new System.Drawing.Point(72, 144);
			this.optCurFile.Name = "optCurFile";
			this.optCurFile.Size = new System.Drawing.Size(88, 16);
			this.optCurFile.TabIndex = 12;
			this.optCurFile.TabStop = true;
			this.optCurFile.Text = "Current File";
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(8, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Search in:";
			// 
			// optSelection
			// 
			this.optSelection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optSelection.Location = new System.Drawing.Point(72, 168);
			this.optSelection.Name = "optSelection";
			this.optSelection.Size = new System.Drawing.Size(88, 16);
			this.optSelection.TabIndex = 14;
			this.optSelection.Text = "Selection";
			// 
			// optAllFiles
			// 
			this.optAllFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optAllFiles.Location = new System.Drawing.Point(168, 144);
			this.optAllFiles.Name = "optAllFiles";
			this.optAllFiles.Size = new System.Drawing.Size(160, 16);
			this.optAllFiles.TabIndex = 13;
			this.optAllFiles.Text = "All Open Files";
			// 
			// findTextBox
			// 
			this.findTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.findTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.findTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.findTextBox.FormattingEnabled = true;
			this.findTextBox.Location = new System.Drawing.Point(101, 7);
			this.findTextBox.Name = "findTextBox";
			this.findTextBox.Size = new System.Drawing.Size(240, 22);
			this.findTextBox.TabIndex = 1;
			this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
			// 
			// replaceTextBox
			// 
			this.replaceTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.replaceTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.replaceTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.replaceTextBox.FormattingEnabled = true;
			this.replaceTextBox.Location = new System.Drawing.Point(101, 33);
			this.replaceTextBox.Name = "replaceTextBox";
			this.replaceTextBox.Size = new System.Drawing.Size(266, 22);
			this.replaceTextBox.TabIndex = 4;
			// 
			// frmFindReplace
			// 
			this.AcceptButton = this.replaceAllButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(457, 223);
			this.Controls.Add(this.replaceTextBox);
			this.Controls.Add(this.findTextBox);
			this.Controls.Add(this.optCurFile);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.optSelection);
			this.Controls.Add(this.optAllFiles);
			this.Controls.Add(this.findInsertButton);
			this.Controls.Add(this.searchHiddenTextCheckBox);
			this.Controls.Add(this.searchInSelectionCheckBox);
			this.Controls.Add(this.markWithCheckBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.markAllButton);
			this.Controls.Add(this.replaceAllButton);
			this.Controls.Add(this.replaceButton);
			this.Controls.Add(this.searchUpCheckBox);
			this.Controls.Add(this.matchWholeWordCheckBox);
			this.Controls.Add(this.matchCaseCheckBox);
			this.Controls.Add(this.searchTypeDropDownList);
			this.Controls.Add(this.searchTypeCheckBox);
			this.Controls.Add(this.findButton);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFindReplace";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Replace";
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

			if (!g.Project.Replaces.Contains(replaceTextBox.Text)) {
				// Remove old replaces
				if (g.Project.Replaces.Count == 10)
					g.Project.Replaces.RemoveAt(0);

				g.Project.Replaces.Add(replaceTextBox.Text);

				RehashRecent();
			}

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
				
			}
		}

		/// <summary>
		/// Occurs when the Text property is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void findTextBox_TextChanged(object sender, System.EventArgs e) {
            findButton.Enabled = (findTextBox.Text.Length > 0);
            replaceButton.Enabled = findButton.Enabled;
            replaceAllButton.Enabled = (findTextBox.Text.Length > 0);
            markAllButton.Enabled = replaceAllButton.Enabled;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void markAllButton_Click(object sender, System.EventArgs e) {
			// Update find/replace options
			this.UpdateFindReplaceOptions();
		
			// Perform a mark all operation
			FindReplaceResultSet resultSet;
			try {
				if (markWithCheckBox.Checked)
					//resultSet = editor.SelectedView.FindReplace.MarkAll(options);
					resultSet = editor.SelectedView.FindReplace.MarkAll(options, typeof(CIndicators.BookmarkIndicator));
				else
					resultSet = editor.SelectedView.FindReplace.MarkAll(options, typeof(GrammarErrorIndicator));
			}
			catch (Exception ex) {
				MessageBox.Show(this, "An error occurred:\r\n" + ex.Message, "Find/Replace Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// If no matches were found
			if (resultSet.Count == 0)
				MessageBox.Show(this, "The specified text was not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

			if (!g.Project.Replaces.Contains(replaceTextBox.Text)) {
				// Remove old replaces
				if (g.Project.Replaces.Count == 10)
					g.Project.Replaces.RemoveAt(0);

				g.Project.Replaces.Add(replaceTextBox.Text);

				RehashRecent();
			}

			if (this.optCurFile.Checked || this.optSelection.Checked) {
				// Perform find operation on currently open file
				FindReplaceResultSet resultSet = null;

				options.SearchInSelection = this.optSelection.Checked;

				try {
					resultSet = editor.SelectedView.FindReplace.Replace(options);
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
					resultSet = actdoc.SelectedView.FindReplace.Replace(options);
				} catch { return; }

				if (resultSet.PastEndOfDocument || resultSet.Count == 0) {
					if (g.Editors.Count == (ActiveDocument + 1)) {
						MessageBox.Show(this, "All open files searched; jumping to beginning.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
						ActiveDocument = 0;
					} else {
						ActiveDocument++;
					}

					if (resultSet.Count == 0)
						replaceButton_Click(null, null);
				}
				
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void replaceAllButton_Click(object sender, System.EventArgs e) {
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

			if (!g.Project.Replaces.Contains(replaceTextBox.Text)) {
				// Remove old replaces
				if (g.Project.Replaces.Count == 10)
					g.Project.Replaces.RemoveAt(0);

				g.Project.Replaces.Add(replaceTextBox.Text);

				RehashRecent();
			}
		
			if (this.optCurFile.Checked || this.optSelection.Checked) {
				// Perform a mark all operation

				options.SearchInSelection = this.optSelection.Checked;

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
			} else if (this.optAllFiles.Checked) {
				int num_success = 0;
				foreach (PrimaryTab<UCEditor> ed in g.Editors) {
					FindReplaceResultSet resultSet;

					try {
						resultSet = ed.Control.txtEditor.SelectedView.FindReplace.ReplaceAll(options);
					} catch { continue; }

					num_success += resultSet.Count;
				}

				
				if (num_success == 0)
					MessageBox.Show(this, "Specified text was not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show(this, num_success.ToString() + " occurrence(c)s were replaced.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
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
			options.ReplaceText			= replaceTextBox.Text;
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

		private void RehashRecent() {
			findTextBox.Items.Clear();
			replaceTextBox.Items.Clear();

			g.Main.cboRecentSearches.Items.Clear();

			foreach (string recent in g.Project.Finds) {
				findTextBox.Items.Add(recent);
				g.Main.cboRecentSearches.Items.Add(recent);
			}

			foreach (string recent in g.Project.Replaces) {
				replaceTextBox.Items.Add(recent);
			}
		}


	}
}
