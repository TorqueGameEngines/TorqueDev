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
	/// Summary description for frmSelProjType.
	/// </summary>
	internal class frmSelProjType : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblDescr;
		public System.Windows.Forms.ListView lvTemplates;
		private System.Windows.Forms.ImageList ilListView;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.ComponentModel.IContainer components;

		public frmSelProjType()
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("TGE Project", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("TSE Project", 1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("T2D Project (TGB)", 2);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelProjType));
			this.lblDescr = new System.Windows.Forms.Label();
			this.lvTemplates = new System.Windows.Forms.ListView();
			this.ilListView = new System.Windows.Forms.ImageList(this.components);
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblDescr
			// 
			this.lblDescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDescr.Location = new System.Drawing.Point(8, 216);
			this.lblDescr.Name = "lblDescr";
			this.lblDescr.Size = new System.Drawing.Size(288, 24);
			this.lblDescr.TabIndex = 5;
			// 
			// lvTemplates
			// 
			this.lvTemplates.HideSelection = false;
			listViewItem1.Tag = "0";
			listViewItem2.Tag = "1";
			listViewItem3.Tag = "2";
			this.lvTemplates.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lvTemplates.LargeImageList = this.ilListView;
			this.lvTemplates.Location = new System.Drawing.Point(8, 8);
			this.lvTemplates.MultiSelect = false;
			this.lvTemplates.Name = "lvTemplates";
			this.lvTemplates.Size = new System.Drawing.Size(288, 200);
			this.lvTemplates.TabIndex = 4;
			this.lvTemplates.UseCompatibleStateImageBehavior = false;
			this.lvTemplates.SelectedIndexChanged += new System.EventHandler(this.lvTemplates_SelectedIndexChanged);
			// 
			// ilListView
			// 
			this.ilListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilListView.ImageStream")));
			this.ilListView.TransparentColor = System.Drawing.Color.Transparent;
			this.ilListView.Images.SetKeyName(0, "");
			this.ilListView.Images.SetKeyName(1, "");
			this.ilListView.Images.SetKeyName(2, "");
			this.ilListView.Images.SetKeyName(3, "");
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(216, 248);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(128, 248);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(80, 24);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmSelProjType
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(306, 280);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.lvTemplates);
			this.Controls.Add(this.lblDescr);
			this.Controls.Add(this.cmdOK);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmSelProjType";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Project Type";
			this.Load += new System.EventHandler(this.frmSelProjType_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void lvTemplates_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (lvTemplates.SelectedItems.Count != 1)
				return;

			switch (lvTemplates.SelectedItems[0].Tag.ToString()) {
				case "0":
					lblDescr.Text = "A Torque Game Engine project.";
					break;
				case "1":
					lblDescr.Text = "A Torque Shader Engine project.";
					break;
				case "2":
					lblDescr.Text = "A Torque 2D project.";
					break;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			// Verify the project type is licensed
			if (lvTemplates.SelectedItems.Count != 1)
				return;

			if (lvTemplates.SelectedItems[0].Tag.ToString() == "0" && g.Config.ActivationHasTGE == false) {
				MessageBox.Show(this, "Error: You are not licensed to use TGE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (lvTemplates.SelectedItems[0].Tag.ToString() == "1" && g.Config.ActivationHasTSE == false) {
				MessageBox.Show(this, "Error: You are not licensed to use TSE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (lvTemplates.SelectedItems[0].Tag.ToString() == "2" && g.Config.ActivationHasT2D == false) {
				MessageBox.Show(this, "Error: You are not licensed to use T2D projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			this.Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmSelProjType_Load(object sender, System.EventArgs e) {
		
		}
	}
}
