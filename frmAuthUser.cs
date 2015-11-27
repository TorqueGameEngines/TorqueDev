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
	/// Summary description for frmAuthUser.
	/// </summary>
	internal class frmAuthUser : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdQuit;
		private System.Windows.Forms.Button cmdAuthorize;
		public System.Windows.Forms.TextBox txtEmail;
		public System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.LinkLabel lnkMoreInfo;
		private System.Windows.Forms.LinkLabel lnkRegister;
		private System.Windows.Forms.LinkLabel lnkLostPasswd;
		private System.Windows.Forms.LinkLabel lnkProxyConfig;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox txtMachineCode;
		private System.Windows.Forms.LinkLabel lnkOfflineAuth;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAuthUser()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAuthUser));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lnkMoreInfo = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lnkRegister = new System.Windows.Forms.LinkLabel();
			this.lnkLostPasswd = new System.Windows.Forms.LinkLabel();
			this.cmdQuit = new System.Windows.Forms.Button();
			this.cmdAuthorize = new System.Windows.Forms.Button();
			this.lnkProxyConfig = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMachineCode = new System.Windows.Forms.TextBox();
			this.lnkOfflineAuth = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lnkMoreInfo
			// 
			this.lnkMoreInfo.LinkArea = new System.Windows.Forms.LinkArea(263, 10);
			this.lnkMoreInfo.Location = new System.Drawing.Point(144, 8);
			this.lnkMoreInfo.Name = "lnkMoreInfo";
			this.lnkMoreInfo.Size = new System.Drawing.Size(264, 96);
			this.lnkMoreInfo.TabIndex = 1;
			this.lnkMoreInfo.TabStop = true;
			this.lnkMoreInfo.Text = @"TorqueDev contains data that, according to the Garage Games engine licenses, may only be accessed by licensees of the engines.  Because of this restriction, users are required to present valid credentials to gain access to these resources.  For more information, click here.";
			this.lnkMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMoreInfo_LinkClicked);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(56, 184);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "E-Mail Address:";
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(56, 200);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(192, 20);
			this.txtEmail.TabIndex = 3;
			this.txtEmail.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(56, 232);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password:";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(56, 248);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '●';
			this.txtPassword.Size = new System.Drawing.Size(192, 20);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// lnkRegister
			// 
			this.lnkRegister.Location = new System.Drawing.Point(256, 200);
			this.lnkRegister.Name = "lnkRegister";
			this.lnkRegister.Size = new System.Drawing.Size(48, 16);
			this.lnkRegister.TabIndex = 4;
			this.lnkRegister.TabStop = true;
			this.lnkRegister.Text = "Register";
			this.lnkRegister.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegister_LinkClicked);
			// 
			// lnkLostPasswd
			// 
			this.lnkLostPasswd.Location = new System.Drawing.Point(256, 248);
			this.lnkLostPasswd.Name = "lnkLostPasswd";
			this.lnkLostPasswd.Size = new System.Drawing.Size(88, 16);
			this.lnkLostPasswd.TabIndex = 4;
			this.lnkLostPasswd.TabStop = true;
			this.lnkLostPasswd.Text = "Lost Password?";
			this.lnkLostPasswd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkLostPasswd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLostPasswd_LinkClicked);
			// 
			// cmdQuit
			// 
			this.cmdQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdQuit.Location = new System.Drawing.Point(304, 288);
			this.cmdQuit.Name = "cmdQuit";
			this.cmdQuit.Size = new System.Drawing.Size(88, 24);
			this.cmdQuit.TabIndex = 5;
			this.cmdQuit.Text = "Quit";
			this.cmdQuit.Click += new System.EventHandler(this.cmdQuit_Click);
			// 
			// cmdAuthorize
			// 
			this.cmdAuthorize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAuthorize.Location = new System.Drawing.Point(208, 288);
			this.cmdAuthorize.Name = "cmdAuthorize";
			this.cmdAuthorize.Size = new System.Drawing.Size(88, 24);
			this.cmdAuthorize.TabIndex = 5;
			this.cmdAuthorize.Text = "Authorize";
			this.cmdAuthorize.Click += new System.EventHandler(this.cmdAuthorize_Click);
			// 
			// lnkProxyConfig
			// 
			this.lnkProxyConfig.Location = new System.Drawing.Point(8, 296);
			this.lnkProxyConfig.Name = "lnkProxyConfig";
			this.lnkProxyConfig.Size = new System.Drawing.Size(104, 16);
			this.lnkProxyConfig.TabIndex = 6;
			this.lnkProxyConfig.TabStop = true;
			this.lnkProxyConfig.Text = "Proxy Configuration";
			this.lnkProxyConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProxyConfig_LinkClicked);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(56, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Machine Code:";
			// 
			// txtMachineCode
			// 
			this.txtMachineCode.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtMachineCode.Location = new System.Drawing.Point(56, 152);
			this.txtMachineCode.Name = "txtMachineCode";
			this.txtMachineCode.ReadOnly = true;
			this.txtMachineCode.Size = new System.Drawing.Size(232, 20);
			this.txtMachineCode.TabIndex = 3;
			this.txtMachineCode.Text = "";
			// 
			// lnkOfflineAuth
			// 
			this.lnkOfflineAuth.Location = new System.Drawing.Point(296, 152);
			this.lnkOfflineAuth.Name = "lnkOfflineAuth";
			this.lnkOfflineAuth.Size = new System.Drawing.Size(112, 16);
			this.lnkOfflineAuth.TabIndex = 6;
			this.lnkOfflineAuth.TabStop = true;
			this.lnkOfflineAuth.Text = "Offline Authorization";
			this.lnkOfflineAuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkOfflineAuth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOfflineAuth_LinkClicked);
			// 
			// frmAuthUser
			// 
			this.AcceptButton = this.cmdAuthorize;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdQuit;
			this.ClientSize = new System.Drawing.Size(410, 327);
			this.ControlBox = false;
			this.Controls.Add(this.txtMachineCode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lnkProxyConfig);
			this.Controls.Add(this.cmdQuit);
			this.Controls.Add(this.lnkLostPasswd);
			this.Controls.Add(this.lnkRegister);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lnkMoreInfo);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.cmdAuthorize);
			this.Controls.Add(this.lnkOfflineAuth);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAuthUser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Network Authorization";
			this.Load += new System.EventHandler(this.frmAuthUser_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdQuit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdAuthorize_Click(object sender, System.EventArgs e) {
			if (txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "") {
				MessageBox.Show(this, "Please specify an e-mail address and password to continue.", "TorqueDev", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Load any new proxy settings
			g.Main.LoadProxySettings();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void frmAuthUser_Load(object sender, System.EventArgs e) {
			this.Show();
			this.txtEmail.Focus();

			this.txtMachineCode.Text = CConfig.GetHardKey();
		}

		private void lnkMoreInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/network/register.php");
		}

		private void lnkLostPasswd_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/network/recover.php");
		}

		private void lnkRegister_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/network/register.php");
		}

		private void lnkProxyConfig_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			MessageBox.Show(this, "TorqueDev will utilize the proxy defined in Internet Explorer's internet options dialog.  Please see your Windows help or contact your system administrator if you need assistance in setting your proxy preferences.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void lnkOfflineAuth_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.torquedev.com/network/offline.php");
		}
	}
}
