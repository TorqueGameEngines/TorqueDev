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
	/// Summary description for frmTestForm.
	/// </summary>
	internal class frmTestForm : System.Windows.Forms.Form
	{
		private TSDev.UCDirectoryCtrl ucDirectoryCtrl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmTestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.ucDirectoryCtrl1 = new TSDev.UCDirectoryCtrl();
			this.SuspendLayout();
			// 
			// ucDirectoryCtrl1
			// 
			this.ucDirectoryCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDirectoryCtrl1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ucDirectoryCtrl1.Location = new System.Drawing.Point(0, 0);
			this.ucDirectoryCtrl1.Name = "ucDirectoryCtrl1";
			this.ucDirectoryCtrl1.RootPath = "C:\\Documents and Settings\\Administrator\\My Documents\\icons_x";
			this.ucDirectoryCtrl1.Size = new System.Drawing.Size(616, 445);
			this.ucDirectoryCtrl1.TabIndex = 0;
			// 
			// frmTestForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 445);
			this.Controls.Add(this.ucDirectoryCtrl1);
			this.Name = "frmTestForm";
			this.Text = "frmTestForm";
			this.Load += new System.EventHandler(this.frmTestForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmTestForm_Load(object sender, System.EventArgs e) {
			this.ucDirectoryCtrl1.RefreshListing();
		}
	}
}
