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
	internal class frmIntellisensePrompt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblPromptLabel;
		private ActiproSoftware.MarkupLabel.MarkupLabel lblML;
		private System.ComponentModel.Container components = null;

		public frmIntellisensePrompt(Point location)
		{

			InitializeComponent();

			this.Location = location;

		}


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
			this.lblPromptLabel = new System.Windows.Forms.Label();
			this.lblML = new ActiproSoftware.MarkupLabel.MarkupLabel();
			this.SuspendLayout();
			// 
			// lblPromptLabel
			// 
			this.lblPromptLabel.AutoSize = true;
			this.lblPromptLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPromptLabel.Location = new System.Drawing.Point(0, 0);
			this.lblPromptLabel.Name = "lblPromptLabel";
			this.lblPromptLabel.Size = new System.Drawing.Size(14, 16);
			this.lblPromptLabel.TabIndex = 0;
			this.lblPromptLabel.Text = "%";
			this.lblPromptLabel.Visible = false;
			// 
			// lblML
			// 
			this.lblML.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblML.DockPadding.Left = 5;
			this.lblML.DockPadding.Right = 5;
			this.lblML.Location = new System.Drawing.Point(0, 0);
			this.lblML.MaxWidth = 2147483647;
			this.lblML.Name = "lblML";
			this.lblML.Size = new System.Drawing.Size(102, 14);
			this.lblML.TabIndex = 1;
			// 
			// frmIntellisensePrompt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Info;
			this.ClientSize = new System.Drawing.Size(102, 14);
			this.ControlBox = false;
			this.Controls.Add(this.lblML);
			this.Controls.Add(this.lblPromptLabel);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmIntellisensePrompt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		public void SetPrompt(string prompt) {
			this.lblML.Text = prompt;
			
			this.Size = lblML.GetPreferredSize();
			this.Width += 10;
		}

	}
}
