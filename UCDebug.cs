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
using System.Text.RegularExpressions;

namespace TSDev
{
	/// <summary>
	/// Summary description for UCDebug.
	/// </summary>
	internal class UCDebug : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabCallStack;
		private System.Windows.Forms.TabPage tabVariables;
		private System.Windows.Forms.TabPage tabOutput;
		public System.Windows.Forms.ListView lvCallStack;
		public System.Windows.Forms.ListView lvVars;
		public ScrollRTB txtOutput;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.TabPage tabWatch;
		public System.Windows.Forms.ListView lvWatch;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ContextMenu ctmWatch;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuCTWatch_AddVar;
		private ActiproSoftware.WinUICore.OwnerDrawMenuItem mnuCTWatch_DelVar;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCDebug()
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabCallStack = new System.Windows.Forms.TabPage();
			this.lvCallStack = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.tabVariables = new System.Windows.Forms.TabPage();
			this.lvVars = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.tabOutput = new System.Windows.Forms.TabPage();
			this.txtOutput = new TSDev.ScrollRTB();
			this.tabWatch = new System.Windows.Forms.TabPage();
			this.lvWatch = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.ctmWatch = new System.Windows.Forms.ContextMenu();
			this.mnuCTWatch_AddVar = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.mnuCTWatch_DelVar = new ActiproSoftware.WinUICore.OwnerDrawMenuItem();
			this.tabControl1.SuspendLayout();
			this.tabCallStack.SuspendLayout();
			this.tabVariables.SuspendLayout();
			this.tabOutput.SuspendLayout();
			this.tabWatch.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabCallStack);
			this.tabControl1.Controls.Add(this.tabVariables);
			this.tabControl1.Controls.Add(this.tabOutput);
			this.tabControl1.Controls.Add(this.tabWatch);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(584, 216);
			this.tabControl1.TabIndex = 0;
			// 
			// tabCallStack
			// 
			this.tabCallStack.Controls.Add(this.lvCallStack);
			this.tabCallStack.Location = new System.Drawing.Point(4, 23);
			this.tabCallStack.Name = "tabCallStack";
			this.tabCallStack.Size = new System.Drawing.Size(576, 189);
			this.tabCallStack.TabIndex = 0;
			this.tabCallStack.Text = "Call Stack";
			// 
			// lvCallStack
			// 
			this.lvCallStack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lvCallStack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCallStack.GridLines = true;
			this.lvCallStack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvCallStack.Location = new System.Drawing.Point(0, 0);
			this.lvCallStack.Name = "lvCallStack";
			this.lvCallStack.Size = new System.Drawing.Size(576, 189);
			this.lvCallStack.TabIndex = 0;
			this.lvCallStack.UseCompatibleStateImageBehavior = false;
			this.lvCallStack.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Function";
			this.columnHeader1.Width = 220;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Line";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "File";
			this.columnHeader3.Width = 285;
			// 
			// tabVariables
			// 
			this.tabVariables.Controls.Add(this.lvVars);
			this.tabVariables.Location = new System.Drawing.Point(4, 22);
			this.tabVariables.Name = "tabVariables";
			this.tabVariables.Size = new System.Drawing.Size(576, 190);
			this.tabVariables.TabIndex = 1;
			this.tabVariables.Text = "Variables";
			// 
			// lvVars
			// 
			this.lvVars.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.lvVars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
			this.lvVars.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvVars.FullRowSelect = true;
			this.lvVars.GridLines = true;
			this.lvVars.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvVars.Location = new System.Drawing.Point(0, 0);
			this.lvVars.Name = "lvVars";
			this.lvVars.Size = new System.Drawing.Size(576, 190);
			this.lvVars.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvVars.TabIndex = 0;
			this.lvVars.UseCompatibleStateImageBehavior = false;
			this.lvVars.View = System.Windows.Forms.View.Details;
			this.lvVars.ItemActivate += new System.EventHandler(this.lvVars_ItemActivate);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Variable";
			this.columnHeader4.Width = 186;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Value";
			this.columnHeader5.Width = 380;
			// 
			// tabOutput
			// 
			this.tabOutput.Controls.Add(this.txtOutput);
			this.tabOutput.Location = new System.Drawing.Point(4, 22);
			this.tabOutput.Name = "tabOutput";
			this.tabOutput.Size = new System.Drawing.Size(576, 190);
			this.tabOutput.TabIndex = 2;
			this.tabOutput.Text = "Output";
			// 
			// txtOutput
			// 
			this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOutput.Location = new System.Drawing.Point(0, 0);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.txtOutput.Size = new System.Drawing.Size(576, 190);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			// 
			// tabWatch
			// 
			this.tabWatch.Controls.Add(this.lvWatch);
			this.tabWatch.Location = new System.Drawing.Point(4, 23);
			this.tabWatch.Name = "tabWatch";
			this.tabWatch.Size = new System.Drawing.Size(576, 189);
			this.tabWatch.TabIndex = 3;
			this.tabWatch.Text = "Watch";
			// 
			// lvWatch
			// 
			this.lvWatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
			this.lvWatch.ContextMenu = this.ctmWatch;
			this.lvWatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvWatch.FullRowSelect = true;
			this.lvWatch.GridLines = true;
			this.lvWatch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvWatch.Location = new System.Drawing.Point(0, 0);
			this.lvWatch.Name = "lvWatch";
			this.lvWatch.Size = new System.Drawing.Size(576, 189);
			this.lvWatch.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvWatch.TabIndex = 1;
			this.lvWatch.UseCompatibleStateImageBehavior = false;
			this.lvWatch.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Variable";
			this.columnHeader6.Width = 186;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Value";
			this.columnHeader7.Width = 380;
			// 
			// ctmWatch
			// 
			this.ctmWatch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCTWatch_AddVar,
            this.mnuCTWatch_DelVar});
			// 
			// mnuCTWatch_AddVar
			// 
			this.mnuCTWatch_AddVar.Index = 0;
			this.mnuCTWatch_AddVar.OwnerDraw = true;
			this.mnuCTWatch_AddVar.Text = "&Add Variable";
			this.mnuCTWatch_AddVar.Click += new System.EventHandler(this.mnuCTWatch_AddVar_Click);
			// 
			// mnuCTWatch_DelVar
			// 
			this.mnuCTWatch_DelVar.Index = 1;
			this.mnuCTWatch_DelVar.OwnerDraw = true;
			this.mnuCTWatch_DelVar.Text = "D&elete Variable";
			this.mnuCTWatch_DelVar.Click += new System.EventHandler(this.mnuCTWatch_DelVar_Click);
			// 
			// UCDebug
			// 
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "UCDebug";
			this.Size = new System.Drawing.Size(600, 232);
			this.Load += new System.EventHandler(this.UCDebug_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabCallStack.ResumeLayout(false);
			this.tabVariables.ResumeLayout(false);
			this.tabOutput.ResumeLayout(false);
			this.tabWatch.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void UCDebug_Load(object sender, System.EventArgs e) {
		
		}

		private void lvVars_ItemActivate(object sender, System.EventArgs e) {
			if (lvVars.SelectedItems.Count > 0) {
				string result = ShowPrompt("Assign Variable", "Please type your assignment of the variable below.  Remember to use quotes for anything but assignment to objects.", lvVars.SelectedItems[0].Text, lvVars.SelectedItems[0].SubItems[1].Text);

				if (result == "")
					return;

				frmMain.stc_DebugQueue.Add("EVAL !" + lvVars.SelectedItems[0].Text + " 0 " + lvVars.SelectedItems[0].Text + " = " + result + "\n");
				lvVars.Items.Remove(lvVars.SelectedItems[0]);
			}
		}

		public static string ShowPrompt(string DialogTitle, string Description, string Prompt, string DefaultVal) {
			frmInput fInput = new frmInput(DialogTitle, Description, Prompt);
			fInput.txtInput.Text = DefaultVal;
			fInput.txtInput.SelectionStart = 0;
			fInput.txtInput.SelectionStart = fInput.txtInput.Text.Length;
			fInput.ShowDialog();

			string input = fInput.Tag.ToString();
			fInput.Dispose();

			return input;
		}

		private void mnuCTWatch_AddVar_Click(object sender, System.EventArgs e) {
			string newvar = ShowPrompt("Add Watch Variable", "Enter the name of the variable you wish to watch below", "Variable:", "");

			// Verify the variable input
			Regex rx = new Regex("^([%$])([A-Z0-9.:]+)$", RegexOptions.IgnoreCase);

			if (!rx.IsMatch(newvar)) {
				MessageBox.Show(this, "Invalid variable format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (g.Project.VarWatchList.Contains(newvar.ToLower()))
				return;			
			
			// Add the new variable to the watch list
			g.Project.VarWatchList.Add(newvar.ToLower());

			// Ask for another list of watch variables
			WatchAskFor();

			// Dirty the project
            frmMain.stc_bIsProjectDirty = true;
		}

		public void WatchAskFor() {
			// Clear the view
			this.lvWatch.Items.Clear();

			// Add a new request for each item
			foreach(string entry in g.Project.VarWatchList) {
				frmMain.stc_DebugQueue.Add("EVAL #" + entry + " 0 " + entry + "\n");
			}
		}

		private void mnuCTWatch_DelVar_Click(object sender, System.EventArgs e) {
			if (this.lvWatch.SelectedItems.Count == 0)
				return;

			// Remove the entry from the arraylist, if it's there
			if (g.Project.VarWatchList.Contains(this.lvWatch.SelectedItems[0].Text.ToLower()))
				g.Project.VarWatchList.Remove(this.lvWatch.SelectedItems[0].Text.ToLower());

			// Remove the entry from the listview
			this.lvWatch.SelectedItems[0].Remove();

			// Dirty the project
			frmMain.stc_bIsProjectDirty = true;
		}
	}
}
