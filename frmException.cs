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
using System.Web;
using System.Net;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmException.
	/// </summary>
	internal class frmException : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtEMail;
		private System.Windows.Forms.TextBox txtDescr;
		private System.Windows.Forms.ComboBox cboReproducable;
		private System.Windows.Forms.Button cmdDoNotReport;
		private System.Windows.Forms.Button cmdReport;
		private System.Windows.Forms.LinkLabel lnkPrivPol;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblException;
		private System.Windows.Forms.LinkLabel linkLabel1;

		private Exception ThrownExc = null;

		public frmException(Exception exc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ThrownExc = exc;
			this.lblException.Text = exc.Message;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmException));
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtEMail = new System.Windows.Forms.TextBox();
			this.txtDescr = new System.Windows.Forms.TextBox();
			this.cmdDoNotReport = new System.Windows.Forms.Button();
			this.cmdReport = new System.Windows.Forms.Button();
			this.lnkPrivPol = new System.Windows.Forms.LinkLabel();
			this.cboReproducable = new System.Windows.Forms.ComboBox();
			this.lblException = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(464, 72);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(16, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(80, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(376, 56);
			this.label1.TabIndex = 2;
			this.label1.Text = "An unhandled exception occurred and the program will be shut down.  You will have" +
				" the opportunity to report this error to the TorqueDev team.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Reproducability:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 224);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(288, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "Describe what you were doing when this error occurred:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 160);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "E-Mail Address:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(104, 128);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(272, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			// 
			// txtEMail
			// 
			this.txtEMail.Location = new System.Drawing.Point(104, 160);
			this.txtEMail.Name = "txtEMail";
			this.txtEMail.Size = new System.Drawing.Size(272, 20);
			this.txtEMail.TabIndex = 2;
			this.txtEMail.Text = "";
			// 
			// txtDescr
			// 
			this.txtDescr.AcceptsReturn = true;
			this.txtDescr.AcceptsTab = true;
			this.txtDescr.Location = new System.Drawing.Point(16, 240);
			this.txtDescr.MaxLength = 5000;
			this.txtDescr.Multiline = true;
			this.txtDescr.Name = "txtDescr";
			this.txtDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescr.Size = new System.Drawing.Size(440, 120);
			this.txtDescr.TabIndex = 4;
			this.txtDescr.Text = "";
			// 
			// cmdDoNotReport
			// 
			this.cmdDoNotReport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdDoNotReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDoNotReport.Location = new System.Drawing.Point(360, 376);
			this.cmdDoNotReport.Name = "cmdDoNotReport";
			this.cmdDoNotReport.Size = new System.Drawing.Size(96, 24);
			this.cmdDoNotReport.TabIndex = 6;
			this.cmdDoNotReport.Text = "Do Not Report";
			this.cmdDoNotReport.Click += new System.EventHandler(this.cmdDoNotReport_Click);
			// 
			// cmdReport
			// 
			this.cmdReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdReport.Location = new System.Drawing.Point(256, 376);
			this.cmdReport.Name = "cmdReport";
			this.cmdReport.Size = new System.Drawing.Size(96, 24);
			this.cmdReport.TabIndex = 5;
			this.cmdReport.Text = "Report";
			this.cmdReport.Click += new System.EventHandler(this.cmdReport_Click);
			// 
			// lnkPrivPol
			// 
			this.lnkPrivPol.Location = new System.Drawing.Point(16, 376);
			this.lnkPrivPol.Name = "lnkPrivPol";
			this.lnkPrivPol.Size = new System.Drawing.Size(80, 24);
			this.lnkPrivPol.TabIndex = 7;
			this.lnkPrivPol.TabStop = true;
			this.lnkPrivPol.Text = "Privacy Policy";
			this.lnkPrivPol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkPrivPol.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPrivPol_LinkClicked);
			// 
			// cboReproducable
			// 
			this.cboReproducable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboReproducable.Items.AddRange(new object[] {
																 "Always",
																 "Sometimes",
																 "First Time"});
			this.cboReproducable.Location = new System.Drawing.Point(104, 192);
			this.cboReproducable.Name = "cboReproducable";
			this.cboReproducable.Size = new System.Drawing.Size(272, 22);
			this.cboReproducable.TabIndex = 3;
			// 
			// lblException
			// 
			this.lblException.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblException.Location = new System.Drawing.Point(8, 80);
			this.lblException.Name = "lblException";
			this.lblException.Size = new System.Drawing.Size(448, 32);
			this.lblException.TabIndex = 6;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(112, 376);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(112, 24);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Bug Report System";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// frmException
			// 
			this.AcceptButton = this.cmdReport;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdDoNotReport;
			this.ClientSize = new System.Drawing.Size(464, 415);
			this.ControlBox = false;
			this.Controls.Add(this.lblException);
			this.Controls.Add(this.cboReproducable);
			this.Controls.Add(this.lnkPrivPol);
			this.Controls.Add(this.cmdDoNotReport);
			this.Controls.Add(this.txtEMail);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDescr);
			this.Controls.Add(this.cmdReport);
			this.Controls.Add(this.linkLabel1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmException";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Fatal Exception";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmException_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmException_Load(object sender, System.EventArgs e) {
		
		}

		private void cmdDoNotReport_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdReport_Click(object sender, System.EventArgs e) {
			if (this.txtName.Text.Trim() == "") {
				MessageBox.Show(this, "Please fill in your name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtEMail.Text.Trim() == "") {
				MessageBox.Show(this, "Please fill in your e-mail address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.cboReproducable.SelectedIndex == -1) {
				MessageBox.Show(this, "Please select a reproducability type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (this.txtDescr.Text.Trim() == "") {
				MessageBox.Show(this, "Please fill in any steps to reproduce this error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// We're sending all of this data to the TorqueDev website
			this.cmdReport.Enabled = false;
			this.Refresh();

			WebClient wc = new WebClient();
			System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

			nvc.Add("email", this.txtEMail.Text);
			nvc.Add("name", this.txtName.Text);
			nvc.Add("repro", this.cboReproducable.Text);
			nvc.Add("descr", this.txtDescr.Text);
			nvc.Add("e_msg", this.ThrownExc.Message);
			nvc.Add("e_stack", this.ThrownExc.StackTrace);
			nvc.Add("e_source", this.ThrownExc.Source);
			nvc.Add("misc", 
				"sp: " + Application.StartupPath + "\n" +
				"cp: " + System.IO.Directory.GetCurrentDirectory() + "\n" +
				"pr: " +
				((g.Project == null) ? "{none open}" : g.Project.ProjectName + " / " +
				g.Project.ProjectPath + " / " + g.Project.ProjectType.ToString()) + "\n" +
				"fm: " + ((g.Main != null) ? g.Main.Text : "{null}") + "\n" +
				"vr: " + Application.ProductVersion + "\n"
				);

			if (this.ThrownExc.InnerException != null) {
				nvc.Add("ei_msg", this.ThrownExc.InnerException.Message);
				nvc.Add("ei_stack", this.ThrownExc.InnerException.StackTrace);
				nvc.Add("ei_source", this.ThrownExc.InnerException.Source);
			}

			// Upload the data
			try {
				wc.QueryString.Add("op", "report");
				wc.UploadValues("http://www.torquedev.com/_client/report_exception.php", "POST", nvc);
			} catch {
				MessageBox.Show(this, "Unable to contact the TorqueDev website to report this exception.  Please visit www.torquedev.com and click on the \"Bug Reports\" link at the top to report this error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.cmdReport.Enabled = true;
				return;
			}

			// Say we've succeeded
			MessageBox.Show(this,
				"Thank you for your report!\n\n" + 
				"If this problem keeps occuring, filing a bug report on the bug report system at www.torquedev.com" +
				" would be helpful to us.\n\n" +
				"Clicking 'OK' will shut down the program.  Sorry for any inconvenience this may have caused.", "Report Successful",
				MessageBoxButtons.OK, MessageBoxIcon.Information);

			this.Close();

		}

		private void lnkPrivPol_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			MessageBox.Show(this,
				"In your report, no personally-identifiable information will be transmitted.  The following is a summary " +
				"of what will be transmitted to TorqueDev:\n\n" +
				"\t- Data in the fields you entered above\n" +
				"\t- A stack trace of the code execution which caused the error\n" +
				"\t- The name of the directory in which TorqueDev resides\n" +
				"\t- The name of the directory currently active in TorqueDev\n" +
				"\t- Your project name, root path, and type (if a project is open).\n\n" +
				"If you disagree with the transmission of any of these pieces of information, you may " +
				"choose to not report this error.\n\nAdditionally, your e-mail will only be used " + 
				"to contact you should we require any further information about this report.  It will not " +
				"be leased, sold, or in any way transmitted from the TorqueDev database.",
				"Privacy Policy",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://secure.netmercs.net/bugs");
		}
	}
}
