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
	/// Summary description for frmDelPrompt.
	/// </summary>
	internal class frmDelPrompt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.RadioButton optYes;
		private System.Windows.Forms.RadioButton optNo;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label lblPrompt;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDelPrompt(string prompt, string option1, string option2)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.lblPrompt.Text = prompt;
			this.optNo.Text = option2;
			this.optYes.Text = option1;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmDelPrompt));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblPrompt = new System.Windows.Forms.Label();
			this.optYes = new System.Windows.Forms.RadioButton();
			this.optNo = new System.Windows.Forms.RadioButton();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(16, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblPrompt
			// 
			this.lblPrompt.Location = new System.Drawing.Point(72, 16);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(288, 48);
			this.lblPrompt.TabIndex = 1;
			this.lblPrompt.Text = "%";
			// 
			// optYes
			// 
			this.optYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optYes.Location = new System.Drawing.Point(72, 80);
			this.optYes.Name = "optYes";
			this.optYes.Size = new System.Drawing.Size(272, 16);
			this.optYes.TabIndex = 2;
			// 
			// optNo
			// 
			this.optNo.Checked = true;
			this.optNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optNo.Location = new System.Drawing.Point(72, 104);
			this.optNo.Name = "optNo";
			this.optNo.Size = new System.Drawing.Size(272, 16);
			this.optNo.TabIndex = 2;
			this.optNo.TabStop = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(272, 144);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(176, 144);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmDelPrompt
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(370, 183);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.optYes);
			this.Controls.Add(this.lblPrompt);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.optNo);
			this.Controls.Add(this.cmdOK);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDelPrompt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Delete";
			this.Load += new System.EventHandler(this.frmDelPrompt_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmDelPrompt_Load(object sender, System.EventArgs e) {
		
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			if (optYes.Checked)
				this.DialogResult = DialogResult.Yes;
			else
				this.DialogResult = DialogResult.No;
		}
	}
}
