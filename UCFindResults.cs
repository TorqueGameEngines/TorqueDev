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

namespace TSDev
{
	/// <summary>
	/// Summary description for UCFindResults.
	/// </summary>
	internal class UCFindResults : System.Windows.Forms.UserControl
	{
		public System.Windows.Forms.ListView lvFind;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCFindResults()
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
			this.lvFind = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvFind
			// 
			this.lvFind.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					 this.columnHeader1,
																					 this.columnHeader3,
																					 this.columnHeader2});
			this.lvFind.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFind.FullRowSelect = true;
			this.lvFind.GridLines = true;
			this.lvFind.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvFind.HideSelection = false;
			this.lvFind.Location = new System.Drawing.Point(0, 0);
			this.lvFind.Name = "lvFind";
			this.lvFind.Size = new System.Drawing.Size(744, 264);
			this.lvFind.TabIndex = 1;
			this.lvFind.View = System.Windows.Forms.View.Details;
			this.lvFind.DoubleClick += new System.EventHandler(this.lvFind_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "File";
			this.columnHeader1.Width = 214;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Line";
			this.columnHeader3.Width = 75;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Text";
			this.columnHeader2.Width = 366;
			// 
			// UCFindResults
			// 
			this.Controls.Add(this.lvFind);
			this.Name = "UCFindResults";
			this.Size = new System.Drawing.Size(744, 264);
			this.ResumeLayout(false);

		}
		#endregion

		private void lvFind_DoubleClick(object sender, System.EventArgs e) {
			if (this.lvFind.SelectedItems.Count == 0)
				return;

			CProject.File file = (CProject.File)this.lvFind.SelectedItems[0].Tag;
			int line = Convert.ToInt32(this.lvFind.SelectedItems[0].SubItems[1].Text);

			g.Main.OpenFile(file, line, false);
		}
	}
}
