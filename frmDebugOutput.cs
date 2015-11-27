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
	/// Summary description for frmDebugOutput.
	/// </summary>
	internal class frmDebugOutput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox txtDebugOut;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDebugOutput(string debugtext)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.txtDebugOut.Text = debugtext;

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
			this.txtDebugOut = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txtDebugOut
			// 
			this.txtDebugOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDebugOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDebugOut.Location = new System.Drawing.Point(0, 0);
			this.txtDebugOut.Name = "txtDebugOut";
			this.txtDebugOut.ReadOnly = true;
			this.txtDebugOut.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.txtDebugOut.Size = new System.Drawing.Size(520, 317);
			this.txtDebugOut.TabIndex = 0;
			this.txtDebugOut.Text = "";
			this.txtDebugOut.WordWrap = false;
			// 
			// frmDebugOutput
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 317);
			this.Controls.Add(this.txtDebugOut);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDebugOutput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Done Debugging";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmDebugOutput_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmDebugOutput_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			this.Dispose();
		}
	}
}
