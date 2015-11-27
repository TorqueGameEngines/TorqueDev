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
	/// Summary description for frmFirstRun.
	/// </summary>
	internal class frmFirstRun : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdQuit;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.LinkLabel lnkTorqueDev;
		private System.Windows.Forms.LinkLabel lnkGG;
		private System.Windows.Forms.LinkLabel lnkEmail;
		private System.Windows.Forms.LinkLabel lnkBugs;
		private System.Windows.Forms.LinkLabel lnkGGThread;
		private System.Windows.Forms.LinkLabel lnkNetmercs;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmFirstRun()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmFirstRun));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lnkTorqueDev = new System.Windows.Forms.LinkLabel();
			this.lnkGG = new System.Windows.Forms.LinkLabel();
			this.lnkEmail = new System.Windows.Forms.LinkLabel();
			this.lnkBugs = new System.Windows.Forms.LinkLabel();
			this.lnkGGThread = new System.Windows.Forms.LinkLabel();
			this.lnkNetmercs = new System.Windows.Forms.LinkLabel();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdQuit = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(251, 480);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(264, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(376, 48);
			this.label1.TabIndex = 1;
			this.label1.Text = "This Is Beta Software!";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(296, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(328, 56);
			this.label2.TabIndex = 2;
			this.label2.Text = "netMercs, the authors of this software, or anyone affiliated with either take no " +
				"responsibility from your use of this application.  This program is provided \"AS " +
				"IS\" with no warranties, express or implied.";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(296, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(328, 56);
			this.label3.TabIndex = 2;
			this.label3.Text = "This program will expire on September 1st, 2005.  You will need to download the n" +
				"ew version after that date.  Sorry for any inconvenience that may have caused.";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lnkTorqueDev);
			this.groupBox1.Controls.Add(this.lnkGG);
			this.groupBox1.Controls.Add(this.lnkEmail);
			this.groupBox1.Controls.Add(this.lnkBugs);
			this.groupBox1.Controls.Add(this.lnkGGThread);
			this.groupBox1.Controls.Add(this.lnkNetmercs);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(296, 192);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(328, 104);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Resources";
			// 
			// lnkTorqueDev
			// 
			this.lnkTorqueDev.Location = new System.Drawing.Point(16, 24);
			this.lnkTorqueDev.Name = "lnkTorqueDev";
			this.lnkTorqueDev.Size = new System.Drawing.Size(144, 16);
			this.lnkTorqueDev.TabIndex = 0;
			this.lnkTorqueDev.TabStop = true;
			this.lnkTorqueDev.Text = "TorqueDev Website";
			this.lnkTorqueDev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTorqueDev_LinkClicked);
			// 
			// lnkGG
			// 
			this.lnkGG.Location = new System.Drawing.Point(16, 48);
			this.lnkGG.Name = "lnkGG";
			this.lnkGG.Size = new System.Drawing.Size(96, 16);
			this.lnkGG.TabIndex = 0;
			this.lnkGG.TabStop = true;
			this.lnkGG.Text = "Garage Games";
			this.lnkGG.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGG_LinkClicked);
			// 
			// lnkEmail
			// 
			this.lnkEmail.Location = new System.Drawing.Point(16, 72);
			this.lnkEmail.Name = "lnkEmail";
			this.lnkEmail.Size = new System.Drawing.Size(160, 16);
			this.lnkEmail.TabIndex = 0;
			this.lnkEmail.TabStop = true;
			this.lnkEmail.Text = "E-Mail Sam Bacsa, the Author";
			this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
			// 
			// lnkBugs
			// 
			this.lnkBugs.Location = new System.Drawing.Point(176, 24);
			this.lnkBugs.Name = "lnkBugs";
			this.lnkBugs.Size = new System.Drawing.Size(72, 16);
			this.lnkBugs.TabIndex = 0;
			this.lnkBugs.TabStop = true;
			this.lnkBugs.Text = "Report Bugs";
			this.lnkBugs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBugs_LinkClicked);
			// 
			// lnkGGThread
			// 
			this.lnkGGThread.Location = new System.Drawing.Point(176, 48);
			this.lnkGGThread.Name = "lnkGGThread";
			this.lnkGGThread.Size = new System.Drawing.Size(96, 16);
			this.lnkGGThread.TabIndex = 0;
			this.lnkGGThread.TabStop = true;
			this.lnkGGThread.Text = "GG Thread";
			this.lnkGGThread.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGGThread_LinkClicked);
			// 
			// lnkNetmercs
			// 
			this.lnkNetmercs.Location = new System.Drawing.Point(176, 72);
			this.lnkNetmercs.Name = "lnkNetmercs";
			this.lnkNetmercs.Size = new System.Drawing.Size(96, 16);
			this.lnkNetmercs.TabIndex = 0;
			this.lnkNetmercs.TabStop = true;
			this.lnkNetmercs.Text = "Visit netMercs";
			this.lnkNetmercs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNetmercs_LinkClicked);
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(408, 432);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(120, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "I Agree; Continue";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdQuit
			// 
			this.cmdQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdQuit.Location = new System.Drawing.Point(544, 432);
			this.cmdQuit.Name = "cmdQuit";
			this.cmdQuit.Size = new System.Drawing.Size(80, 24);
			this.cmdQuit.TabIndex = 4;
			this.cmdQuit.Text = "Quit";
			this.cmdQuit.Click += new System.EventHandler(this.cmdQuit_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(296, 312);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Last Minute Notes:";
			// 
			// textBox1
			// 
			this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBox1.Location = new System.Drawing.Point(296, 328);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(328, 96);
			this.textBox1.TabIndex = 6;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "(Scroll down for important feature notes and instructions)\r\n\r\n\r\n** THIS IS BETA S" +
				"OFTWARE and is therefore NOT GUARANTEED to work correctly.  Expect crashes, rand" +
				"om exceptions, and other such nuisences.  Although it has been tested and has be" +
				"en deemed relatively stable, every system configuration is different.  Don\'t exp" +
				"ect it to work perfectly.  Your input is always appreciated.  Utilize the bug si" +
				"te for feature requests and bug reports; post on the forum for general questions" +
				", suggestions, or comments.\r\n\r\n\r\nPlease use the bug report site (also accessible" +
				" through the HELP menu) to report any bugs you may encounter.\r\n\r\nREGIONS:\r\n\r\nTo " +
				"define a region, enclose your regionized code between:\r\n\r\n//__region Name of my " +
				"region\r\n\r\n//__end\r\n\r\nIt will automatically be collapsed on subsequent opens of y" +
				"our file.\r\n\r\n\r\n\r\nDEBUGGING:\r\n\r\nDebugging can be configured by right-clicking on " +
				"your project and selecting properties.  Define all the values present and set \"D" +
				"ebugging Enabled\" to \"True\".  If all the values check out, the Debug menu should" +
				" become available.\r\n\r\n\r\n\r\nCONFIGURATION VERSION CONFLICTS:\r\n\r\nIt may be possible" +
				" that between this version, subsequent betas, and the final release of the softw" +
				"are that your configuration may become invalid.  At that point it will be overwr" +
				"itten with a fresh configuration.  You will lose any customizations which you ma" +
				"de to the editor colors as well as other toggles in \"Preferences\".\r\n\r\nAdditional" +
				"ly, projects may also conflict.  You will be warned if this is the case and the " +
				"project will not be loaded.  I am making absolutely every effort to gracefully c" +
				"onvert old projects in the final version; however, it being a low priority, they" +
				" will not be converted in this beta or future betas.  Thanks for understanding.\r" +
				"\n\r\n\r\n\r\nDOT-LEVEL VARIABLE COMPLETION\r\n\r\nYou can have auto-complete functionality" +
				" on LOCAL VARIABLES ONLY by declaring it in scope as follows:\r\n\r\n//__decl Object" +
				" Variable\r\n\r\nFor example, should I want %asdf to be TCPObject, I would write:\r\n\r" +
				"\n//__decl TCPObject asdf\r\n\r\n(Notice the % sign is omitted)\r\n\r\n\r\n\r\n\r\nYour donatio" +
				"ns would be appreciated.  Please register on the forums, and please also post bu" +
				"gs, feature requests, or any other suggestions you may have.\r\n\r\nBest,\r\nSam Bacsa" +
				"";
			this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(264, 432);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(200, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Public Beta Release";
			// 
			// frmFirstRun
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 477);
			this.ControlBox = false;
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdQuit);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmFirstRun";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Welcome to TorqueDev";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e) {
			g.Config.bFirstrun = true;
			this.Close();
		}

		private void cmdQuit_Click(object sender, System.EventArgs e) {
			Application.Exit();
		}

		private void lnkTorqueDev_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://torque.netmercs.net/");
		}

		private void lnkGG_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.garagegames.com/");
		}

		private void lnkEmail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("mailto:sbacsa@netmercs.net");
		}

		private void lnkBugs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://secure.netmercs.net/bugs");
		}

		private void lnkGGThread_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.garagegames.com/mg/forums/result.thread.php?qt=32379");
		}

		private void lnkNetmercs_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.netmercs.net/");
		}

		private void textBox1_Enter(object sender, System.EventArgs e) {
			this.cmdOK.Focus();
		}
	}
}
