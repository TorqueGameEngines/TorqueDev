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
	/// Summary description for frmInput.
	/// </summary>
	internal class frmInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblDescr;
		public System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Label lblPrompt;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmInput(string title, string description, string prompt)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Text = title;
			this.lblDescr.Text = description;
			this.lblPrompt.Text = prompt;

			this.Owner = g.Main;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblDescr = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.lblPrompt = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			// lblDescr
			// 
			this.lblDescr.Location = new System.Drawing.Point(72, 16);
			this.lblDescr.Name = "lblDescr";
			this.lblDescr.Size = new System.Drawing.Size(216, 40);
			this.lblDescr.TabIndex = 1;
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(16, 80);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(272, 20);
			this.txtInput.TabIndex = 2;
			// 
			// lblPrompt
			// 
			this.lblPrompt.Location = new System.Drawing.Point(16, 64);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(272, 16);
			this.lblPrompt.TabIndex = 3;
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(208, 112);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(120, 112);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmInput
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(306, 143);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.lblPrompt);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.lblDescr);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.cmdOK);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmInput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "%";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmInput_Closing);
			this.Load += new System.EventHandler(this.frmInput_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void frmInput_Load(object sender, System.EventArgs e) {
			
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Tag = "";
			this.Hide();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			this.Tag = txtInput.Text;
			this.Hide();
		}

		private void frmInput_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.Tag == null)
				this.Tag = "";

			this.Hide();
			e.Cancel = true;
		}
	}
}
