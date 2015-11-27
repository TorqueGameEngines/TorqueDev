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
	/// Summary description for frmConfigColor.
	/// </summary>
	internal class frmConfigColor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.FontDialog dlgFont;
		private System.Windows.Forms.Button cmdChFont;
		private System.Windows.Forms.Button cmdChBg;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.ColorDialog dlgColor;
		private IContainer components;

		private CConfig.HighlightEntry old_he;
		private System.Windows.Forms.Label lblFont;
		private ContextMenuStrip ctxColors;
		private ToolStripMenuItem mnuChgFore;
		private ToolStripMenuItem mnuChgBkg;
		private CConfig.HighlightEntry he;

		public frmConfigColor(string he_key)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			this.old_he = (CConfig.HighlightEntry)(g.Config.colors[he_key]);
			this.he = (CConfig.HighlightEntry)this.old_he.Clone();
			//this.he = (CConfig.HighlightEntry)he.Clone();
			

			this.lblFont.ForeColor = he.ForeColor;
			this.lblFont.Font = he.HighlightFont;
			this.lblFont.BackColor = he.BackColor;

			lblFont.Text = this.he.HighlightFont.FontFamily.Name + ", " + this.he.HighlightFont.SizeInPoints.ToString() + "pt";

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
			this.components = new System.ComponentModel.Container();
			this.dlgFont = new System.Windows.Forms.FontDialog();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.dlgColor = new System.Windows.Forms.ColorDialog();
			this.lblFont = new System.Windows.Forms.Label();
			this.ctxColors = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuChgFore = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuChgBkg = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdChFont = new System.Windows.Forms.Button();
			this.cmdChBg = new System.Windows.Forms.Button();
			this.ctxColors.SuspendLayout();
			this.SuspendLayout();
			// 
			// dlgFont
			// 
			this.dlgFont.FontMustExist = true;
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(224, 104);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(320, 104);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// dlgColor
			// 
			this.dlgColor.Color = System.Drawing.Color.Transparent;
			this.dlgColor.FullOpen = true;
			// 
			// lblFont
			// 
			this.lblFont.BackColor = System.Drawing.SystemColors.Control;
			this.lblFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFont.Location = new System.Drawing.Point(8, 8);
			this.lblFont.Name = "lblFont";
			this.lblFont.Size = new System.Drawing.Size(392, 88);
			this.lblFont.TabIndex = 5;
			this.lblFont.Text = "ABC abc";
			this.lblFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctxColors
			// 
			this.ctxColors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChgFore,
            this.mnuChgBkg});
			this.ctxColors.Name = "ctxColors";
			this.ctxColors.Size = new System.Drawing.Size(222, 48);
			// 
			// mnuChgFore
			// 
			this.mnuChgFore.Name = "mnuChgFore";
			this.mnuChgFore.Size = new System.Drawing.Size(221, 22);
			this.mnuChgFore.Text = "&Change Foreground Color...";
			this.mnuChgFore.Click += new System.EventHandler(this.mnuChgFore_Click);
			// 
			// mnuChgBkg
			// 
			this.mnuChgBkg.Name = "mnuChgBkg";
			this.mnuChgBkg.Size = new System.Drawing.Size(221, 22);
			this.mnuChgBkg.Text = "Change &Background Color...";
			this.mnuChgBkg.Click += new System.EventHandler(this.mnuChgBkg_Click);
			// 
			// cmdChFont
			// 
			this.cmdChFont.Image = global::TSDev.Properties.Resources.font;
			this.cmdChFont.Location = new System.Drawing.Point(8, 104);
			this.cmdChFont.Name = "cmdChFont";
			this.cmdChFont.Size = new System.Drawing.Size(26, 24);
			this.cmdChFont.TabIndex = 2;
			this.cmdChFont.Click += new System.EventHandler(this.cmdChFont_Click);
			// 
			// cmdChBg
			// 
			this.cmdChBg.Image = global::TSDev.Properties.Resources.window_colors;
			this.cmdChBg.Location = new System.Drawing.Point(40, 104);
			this.cmdChBg.Name = "cmdChBg";
			this.cmdChBg.Size = new System.Drawing.Size(26, 24);
			this.cmdChBg.TabIndex = 2;
			this.cmdChBg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdChBg_MouseUp);
			// 
			// frmConfigColor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(410, 134);
			this.ControlBox = false;
			this.Controls.Add(this.lblFont);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdChFont);
			this.Controls.Add(this.cmdChBg);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmConfigColor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Adjust Colors";
			this.Load += new System.EventHandler(this.frmConfigColor_Load);
			this.ctxColors.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdChFont_Click(object sender, System.EventArgs e) {
			dlgFont.Font = this.lblFont.Font;
			dlgFont.Color = this.lblFont.ForeColor;
			DialogResult result = dlgFont.ShowDialog();

			if (result == DialogResult.Cancel)
				return;
			
			this.lblFont.Font = dlgFont.Font;
			this.lblFont.Text = dlgFont.Font.FontFamily.Name + ", " + dlgFont.Font.SizeInPoints.ToString() + "pt";
			this.he.HighlightFont = dlgFont.Font;
		}

		private void cmdChBg_Click(object sender, System.EventArgs e) {
			
			
		}

		private void frmConfigColor_Load(object sender, System.EventArgs e) {
		
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			this.old_he.BackColor  = this.he.BackColor;
			this.old_he.ForeColor = this.he.ForeColor;
			this.old_he.HighlightFont = this.he.HighlightFont;
			this.Close();
		}

		private void mnuChgFore_Click(object sender, EventArgs e) {
			dlgColor.Color = this.lblFont.ForeColor;
			DialogResult result = dlgColor.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			this.lblFont.ForeColor = dlgColor.Color;
			this.he.ForeColor = dlgColor.Color;
		}

		private void mnuChgBkg_Click(object sender, EventArgs e) {
			dlgColor.Color = this.lblFont.BackColor;
			DialogResult result = dlgColor.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			this.lblFont.BackColor = dlgColor.Color;
			this.he.BackColor = dlgColor.Color;
		}

		private void cmdChBg_MouseUp(object sender, MouseEventArgs e) {
			ctxColors.Show(cmdChBg, e.X, e.Y);
		}
	}
}
