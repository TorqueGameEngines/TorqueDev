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
	/// Summary description for frmLoad.
	/// </summary>
	internal class frmLoad : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblVersion;
		private Label lblLicCompany;
		private Label lblLicUser;
		private Label lblLicSerial;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmLoad()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if (g.License["MANAGED"] == true)
				pictureBox1.Image = global::TSDev.Properties.Resources.cw_load_mng;
			else if (g.License["PRO"] == true)
				pictureBox1.Image = global::TSDev.Properties.Resources.cw_load_rc_pro;
			else if (g.License["DONATOR"] == true || g.License["FREE"] == true)
				pictureBox1.Image = global::TSDev.Properties.Resources.cw_load_rc_std;

			lblLicUser.Text = g.License.LicensedUser;
			lblLicCompany.Text = g.License.LicensedCompany;
			lblLicSerial.Text = g.License.LicenseSerial;

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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblLicCompany = new System.Windows.Forms.Label();
			this.lblLicUser = new System.Windows.Forms.Label();
			this.lblLicSerial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TSDev.Properties.Resources.cw_load_rc_std;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(583, 179);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Location = new System.Drawing.Point(297, 116);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(274, 14);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "v. %";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLicCompany
			// 
			this.lblLicCompany.AutoSize = true;
			this.lblLicCompany.Location = new System.Drawing.Point(12, 116);
			this.lblLicCompany.Name = "lblLicCompany";
			this.lblLicCompany.Size = new System.Drawing.Size(23, 14);
			this.lblLicCompany.TabIndex = 4;
			this.lblLicCompany.Text = "%c";
			// 
			// lblLicUser
			// 
			this.lblLicUser.AutoSize = true;
			this.lblLicUser.Location = new System.Drawing.Point(12, 102);
			this.lblLicUser.Name = "lblLicUser";
			this.lblLicUser.Size = new System.Drawing.Size(23, 14);
			this.lblLicUser.TabIndex = 4;
			this.lblLicUser.Text = "%u";
			// 
			// lblLicSerial
			// 
			this.lblLicSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLicSerial.Location = new System.Drawing.Point(478, 102);
			this.lblLicSerial.Name = "lblLicSerial";
			this.lblLicSerial.Size = new System.Drawing.Size(93, 14);
			this.lblLicSerial.TabIndex = 4;
			this.lblLicSerial.Text = "%s";
			this.lblLicSerial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmLoad
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(583, 135);
			this.ControlBox = false;
			this.Controls.Add(this.lblLicUser);
			this.Controls.Add(this.lblLicSerial);
			this.Controls.Add(this.lblLicCompany);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmLoad";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmLoad_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void frmLoad_Load(object sender, System.EventArgs e) {
			// Set the background to the appropriate version --
			this.lblVersion.Text = Application.ProductVersion + " (FINAL)";
		}
	}
}
