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
	/// Summary description for UCError.
	/// </summary>
	internal class UCError : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		public System.Windows.Forms.ListView lvErrors;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCError()
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
			this.lvErrors = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvErrors
			// 
			this.lvErrors.AutoArrange = false;
			this.lvErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader1,
																					   this.columnHeader2,
																					   this.columnHeader3});
			this.lvErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvErrors.FullRowSelect = true;
			this.lvErrors.GridLines = true;
			this.lvErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvErrors.HideSelection = false;
			this.lvErrors.Location = new System.Drawing.Point(0, 0);
			this.lvErrors.Name = "lvErrors";
			this.lvErrors.Size = new System.Drawing.Size(664, 248);
			this.lvErrors.TabIndex = 0;
			this.lvErrors.View = System.Windows.Forms.View.Details;
			this.lvErrors.DoubleClick += new System.EventHandler(this.lvErrors_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Error";
			this.columnHeader1.Width = 220;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "File";
			this.columnHeader2.Width = 237;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Line / Column";
			this.columnHeader3.Width = 120;
			// 
			// UCError
			// 
			this.Controls.Add(this.lvErrors);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UCError";
			this.Size = new System.Drawing.Size(664, 248);
			this.ResumeLayout(false);

		}
		#endregion

		private void lvErrors_DoubleClick(object sender, System.EventArgs e) {
			if (this.lvErrors.SelectedItems.Count == 0)
				return;

			if (this.lvErrors.SelectedItems[0].Tag == null)
				return;

			CProject.File file = (CProject.File)this.lvErrors.SelectedItems[0].Tag;

			string[] linesplit = this.lvErrors.SelectedItems[0].SubItems[2].Text.Replace(" ", "").Split('/');
			int line_num = Convert.ToInt32(linesplit[0]);
			
			g.Main.OpenFile(file, line_num - 1, false);

			// Set focus on the editor
			g.Main.GetActiveEditor().txtEditor.Select();
			g.Main.GetActiveEditor().txtEditor.Focus();

            // Select the column in question
            int fileoffset = g.Main.GetActiveEditor().txtEditor.Document.PositionToOffset(new ActiproSoftware.SyntaxEditor.Position(line_num - 1, Convert.ToInt32(linesplit[1]) - 1));
            g.Main.GetActiveEditor().txtEditor.SelectedView.Selection.SelectRange(fileoffset, 0);
		}
	}
}
