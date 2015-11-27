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
using System.IO;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmConfigDebugExec.
	/// </summary>
	internal class frmConfigDebugExec : System.Windows.Forms.Form
	{
		private CConfig loconf = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cmdNew;
		private System.Windows.Forms.Button cmdEdit;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.TextBox txtExecutable;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.TextBox txtParams;
		private System.Windows.Forms.CheckBox chkDelDso;
		private System.Windows.Forms.TextBox txtDsoExt;
		private System.Windows.Forms.ListView lvProgs;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtWaitSecs;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmConfigDebugExec(CConfig conf)
		{
			this.loconf = conf;

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
			this.label1 = new System.Windows.Forms.Label();
			this.chkDelDso = new System.Windows.Forms.CheckBox();
			this.txtDsoExt = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lvProgs = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.label3 = new System.Windows.Forms.Label();
			this.txtExecutable = new System.Windows.Forms.TextBox();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtParams = new System.Windows.Forms.TextBox();
			this.cmdNew = new System.Windows.Forms.Button();
			this.cmdEdit = new System.Windows.Forms.Button();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtWaitSecs = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(288, 48);
			this.label1.TabIndex = 0;
			this.label1.Text = "This page configures actions that will take place when debugging is started. You " +
				"can configure custom actions below:";
			// 
			// chkDelDso
			// 
			this.chkDelDso.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDelDso.Location = new System.Drawing.Point(24, 64);
			this.chkDelDso.Name = "chkDelDso";
			this.chkDelDso.Size = new System.Drawing.Size(144, 16);
			this.chkDelDso.TabIndex = 1;
			this.chkDelDso.Text = "Delete all";
			this.chkDelDso.CheckedChanged += new System.EventHandler(this.chkDelDso_CheckedChanged);
			// 
			// txtDsoExt
			// 
			this.txtDsoExt.Location = new System.Drawing.Point(96, 64);
			this.txtDsoExt.Name = "txtDsoExt";
			this.txtDsoExt.Size = new System.Drawing.Size(32, 20);
			this.txtDsoExt.TabIndex = 2;
			this.txtDsoExt.Text = "dso";
			this.txtDsoExt.TextChanged += new System.EventHandler(this.txtDsoExt_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(136, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "files before debugging.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lvProgs
			// 
			this.lvProgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2});
			this.lvProgs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvProgs.Location = new System.Drawing.Point(8, 96);
			this.lvProgs.Name = "lvProgs";
			this.lvProgs.Size = new System.Drawing.Size(288, 120);
			this.lvProgs.TabIndex = 4;
			this.lvProgs.View = System.Windows.Forms.View.Details;
			this.lvProgs.SelectedIndexChanged += new System.EventHandler(this.lvProgs_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Program";
			this.columnHeader1.Width = 167;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Parameters";
			this.columnHeader2.Width = 73;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Executable:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtExecutable
			// 
			this.txtExecutable.Location = new System.Drawing.Point(80, 224);
			this.txtExecutable.Name = "txtExecutable";
			this.txtExecutable.Size = new System.Drawing.Size(144, 20);
			this.txtExecutable.TabIndex = 6;
			this.txtExecutable.Text = "";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowse.Location = new System.Drawing.Point(232, 224);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(64, 24);
			this.cmdBrowse.TabIndex = 7;
			this.cmdBrowse.Text = "Browse";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 256);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Parameters:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtParams
			// 
			this.txtParams.Location = new System.Drawing.Point(80, 256);
			this.txtParams.Name = "txtParams";
			this.txtParams.Size = new System.Drawing.Size(216, 20);
			this.txtParams.TabIndex = 6;
			this.txtParams.Text = "";
			// 
			// cmdNew
			// 
			this.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNew.Location = new System.Drawing.Point(40, 288);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(80, 24);
			this.cmdNew.TabIndex = 9;
			this.cmdNew.Text = "Add";
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// cmdEdit
			// 
			this.cmdEdit.Enabled = false;
			this.cmdEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdEdit.Location = new System.Drawing.Point(128, 288);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Size = new System.Drawing.Size(80, 24);
			this.cmdEdit.TabIndex = 9;
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Enabled = false;
			this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDelete.Location = new System.Drawing.Point(216, 288);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(80, 24);
			this.cmdDelete.TabIndex = 9;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(208, 360);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(88, 24);
			this.cmdClose.TabIndex = 10;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 328);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Wait";
			// 
			// txtWaitSecs
			// 
			this.txtWaitSecs.Location = new System.Drawing.Point(40, 328);
			this.txtWaitSecs.Name = "txtWaitSecs";
			this.txtWaitSecs.Size = new System.Drawing.Size(32, 20);
			this.txtWaitSecs.TabIndex = 12;
			this.txtWaitSecs.Text = "3";
			this.txtWaitSecs.TextChanged += new System.EventHandler(this.txtWaitSecs_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(80, 328);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(144, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "seconds before connecting";
			// 
			// frmConfigDebugExec
			// 
			this.AcceptButton = this.cmdClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(306, 391);
			this.ControlBox = false;
			this.Controls.Add(this.txtWaitSecs);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdNew);
			this.Controls.Add(this.txtParams);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtExecutable);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lvProgs);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDsoExt);
			this.Controls.Add(this.chkDelDso);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdEdit);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.label6);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmConfigDebugExec";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configure Debug Execution";
			this.Load += new System.EventHandler(this.frmConfigDebugExec_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmConfigDebugExec_Load(object sender, System.EventArgs e) {
			// Populate the current list
			PopulateList();

			// Get the current DSO check information
			this.chkDelDso.Checked = loconf.b_DeleteDSO;
			this.txtDsoExt.Text = loconf.DSOExtension;
			this.txtWaitSecs.Text = loconf.DebugTimeout.ToString();
		}

		private void PopulateList() {
			this.lvProgs.Items.Clear();
			this.lvProgs.BeginUpdate();

			foreach(CConfig.DebugExecEntry entry in this.loconf.DebugRun) {
				ListViewItem item = new ListViewItem(entry.path);
				item.SubItems.Add(entry.parameters);
				item.Tag = entry;

				this.lvProgs.Items.Add(item);
			}

			this.lvProgs.EndUpdate();
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Select Executable";
			ofd.Filter = "Executable Files (*exe; *.bat)|*.exe;*.bat|All Files (*.*)|*.*";
			ofd.InitialDirectory = g.Project.ProjectPath;
			ofd.CheckFileExists = true;

			DialogResult result = ofd.ShowDialog();

			if (result == DialogResult.Cancel)
				return;

			this.txtExecutable.Text = ofd.FileName;
		}

		private void lvProgs_SelectedIndexChanged(object sender, System.EventArgs e) {
			if ((sender as ListView).SelectedItems.Count == 0)
				return;

			this.cmdEdit.Enabled = true;
			this.cmdDelete.Enabled = true;

			CConfig.DebugExecEntry entry = (CConfig.DebugExecEntry)this.lvProgs.SelectedItems[0].Tag;
			
			txtExecutable.Text = entry.path;
			txtParams.Text = entry.parameters;
		}

		private void cmdNew_Click(object sender, System.EventArgs e) {
			if (this.txtExecutable.Text.Trim() == "") {
				MessageBox.Show(this, "Please fill in a path to the executable to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (!File.Exists(this.txtExecutable.Text.Trim())) {
				MessageBox.Show(this, "File \"" + txtExecutable.Text + "\" not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			this.loconf.DebugRun.Add(new CConfig.DebugExecEntry(this.txtExecutable.Text, this.txtParams.Text));
			this.txtExecutable.Text = "";
			this.txtParams.Text = "";

			PopulateList();

		}

		private void cmdEdit_Click(object sender, System.EventArgs e) {
			if (this.lvProgs.SelectedItems.Count == 0)
				return;

			if (this.txtExecutable.Text.Trim() == "") {
				MessageBox.Show(this, "Please fill in a path to the executable to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else if (!File.Exists(this.txtExecutable.Text.Trim())) {
				MessageBox.Show(this, "File \"" + txtExecutable.Text + "\" not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			CConfig.DebugExecEntry entry = (CConfig.DebugExecEntry)this.lvProgs.SelectedItems[0].Tag;
			
			entry.path = txtExecutable.Text;
			entry.parameters = txtParams.Text;

			this.txtExecutable.Text = "";
			this.txtParams.Text = "";

            PopulateList();
		}

		private void cmdDelete_Click(object sender, System.EventArgs e) {
			if (this.lvProgs.SelectedItems.Count == 0)
				return;

			this.loconf.DebugRun.Remove((CConfig.DebugExecEntry)this.lvProgs.SelectedItems[0].Tag);

			this.txtExecutable.Text = "";
			this.txtParams.Text = "";

			PopulateList();
		}

		private void txtDsoExt_TextChanged(object sender, System.EventArgs e) {
			this.loconf.DSOExtension = this.txtDsoExt.Text;
		}

		private void chkDelDso_CheckedChanged(object sender, System.EventArgs e) {
			this.loconf.b_DeleteDSO = this.chkDelDso.Checked;
			this.txtDsoExt.Enabled = this.chkDelDso.Checked;
		}

		private void txtWaitSecs_TextChanged(object sender, System.EventArgs e) {
			double d = 0.0d;

			if (!Double.TryParse(txtWaitSecs.Text, System.Globalization.NumberStyles.Integer, new System.Globalization.CultureInfo("en-US"), out d)) {
				txtWaitSecs.Text = "3";
				return;
			}

			loconf.DebugTimeout = Convert.ToInt32(txtWaitSecs.Text);
		}
	}
}
