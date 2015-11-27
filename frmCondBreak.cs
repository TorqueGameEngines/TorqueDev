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
using System.Text.RegularExpressions;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmCondBreak.
	/// </summary>
	internal class frmCondBreak : System.Windows.Forms.Form
	{
		private CProject.Breakpoint breakpt = null;

		private System.Windows.Forms.CheckBox chkReqPass;
		private System.Windows.Forms.TextBox txtPasses;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkEvalCond;
		private System.Windows.Forms.TextBox txtConditional;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCondBreak(CProject.Breakpoint breakpt)
		{
			this.breakpt = breakpt;


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
			this.chkReqPass = new System.Windows.Forms.CheckBox();
			this.txtPasses = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkEvalCond = new System.Windows.Forms.CheckBox();
			this.txtConditional = new System.Windows.Forms.TextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chkReqPass
			// 
			this.chkReqPass.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkReqPass.Location = new System.Drawing.Point(8, 16);
			this.chkReqPass.Name = "chkReqPass";
			this.chkReqPass.Size = new System.Drawing.Size(96, 16);
			this.chkReqPass.TabIndex = 0;
			this.chkReqPass.Text = "Require";
			this.chkReqPass.CheckedChanged += new System.EventHandler(this.chkReqPass_CheckedChanged);
			// 
			// txtPasses
			// 
			this.txtPasses.Enabled = false;
			this.txtPasses.Location = new System.Drawing.Point(72, 16);
			this.txtPasses.Name = "txtPasses";
			this.txtPasses.Size = new System.Drawing.Size(32, 20);
			this.txtPasses.TabIndex = 1;
			this.txtPasses.Text = "0";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(112, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "passes before triggering";
			// 
			// chkEvalCond
			// 
			this.chkEvalCond.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkEvalCond.Location = new System.Drawing.Point(8, 48);
			this.chkEvalCond.Name = "chkEvalCond";
			this.chkEvalCond.Size = new System.Drawing.Size(320, 16);
			this.chkEvalCond.TabIndex = 3;
			this.chkEvalCond.Text = "Evaluate the following conditional as true before triggering:";
			this.chkEvalCond.CheckedChanged += new System.EventHandler(this.chkEvalCond_CheckedChanged);
			// 
			// txtConditional
			// 
			this.txtConditional.Enabled = false;
			this.txtConditional.Location = new System.Drawing.Point(24, 72);
			this.txtConditional.Name = "txtConditional";
			this.txtConditional.Size = new System.Drawing.Size(304, 20);
			this.txtConditional.TabIndex = 4;
			this.txtConditional.Text = "true";
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(144, 112);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(240, 112);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// frmCondBreak
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(338, 143);
			this.ControlBox = false;
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.txtConditional);
			this.Controls.Add(this.chkEvalCond);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPasses);
			this.Controls.Add(this.chkReqPass);
			this.Controls.Add(this.cmdCancel);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmCondBreak";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Breakpoint";
			this.Load += new System.EventHandler(this.frmCondBreak_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void chkEvalCond_CheckedChanged(object sender, System.EventArgs e) {
			this.txtConditional.Enabled = chkEvalCond.Checked;
		}

		private void chkReqPass_CheckedChanged(object sender, System.EventArgs e) {
			this.txtPasses.Enabled = chkReqPass.Checked;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			// Test to see if the data is valid first
			if (this.chkReqPass.Checked == false)
				this.txtPasses.Text = "0";
			if (this.chkEvalCond.Checked == false)
				this.txtConditional.Text = "true";

			// Check if the pass is an integer
			Regex rx = new Regex("^[0-9]+$");
			if (!rx.IsMatch(txtPasses.Text)) {
				MessageBox.Show(this, "Error: Invalid data in \"pass count\" field.  Must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Check if the passes is not negative
			if (Convert.ToInt32(txtPasses.Text) < 0) {
				MessageBox.Show(this, "Error: Invalid data in \"pass count\" field.  Must be a positive integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Change the conditional data and write it back to the class
			breakpt.Conditional = txtConditional.Text;
			breakpt.PassCount = Convert.ToInt32(txtPasses.Text);

			// This will automatically delete the previous entry and re-add it, sending any relevant data to the 
			// engine in case we're debugging
			g.Project.ProjectBreaks.Add(breakpt);

			// Close this form
			this.Close();
		}

		private void frmCondBreak_Load(object sender, System.EventArgs e) {
			if (breakpt.Conditional != "true")
				this.chkEvalCond.Checked = true;
			if (breakpt.PassCount != 0)
				this.chkReqPass.Checked = true;

			this.txtConditional.Text = breakpt.Conditional;
			this.txtPasses.Text = breakpt.PassCount.ToString();
		}
	}
}
