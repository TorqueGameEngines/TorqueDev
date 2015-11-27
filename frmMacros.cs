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
	/// Summary description for frmMacros.
	/// </summary>
	internal class frmMacros : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ComboBox cboMacroSlots;
		private System.Windows.Forms.ListBox lstMacros;
		private System.Windows.Forms.Button cmdAssign;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdDel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMacros()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMacros));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.cboMacroSlots = new System.Windows.Forms.ComboBox();
			this.lstMacros = new System.Windows.Forms.ListBox();
			this.cmdAssign = new System.Windows.Forms.Button();
			this.cmdDel = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select a quick-macro slot below and assign a macro to it from the list:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// cboMacroSlots
			// 
			this.cboMacroSlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMacroSlots.Items.AddRange(new object[] {
															   "Slot 1 - Empty",
															   "Slot 2 - Empty",
															   "Slot 3 - Empty",
															   "Slot 4 - Empty",
															   "Slot 5 - Empty",
															   "Slot 6 - Empty",
															   "Slot 7 - Empty",
															   "Slot 8 - Empty",
															   "Slot 9 - Empty",
															   "Slot 10 - Empty"});
			this.cboMacroSlots.Location = new System.Drawing.Point(72, 48);
			this.cboMacroSlots.Name = "cboMacroSlots";
			this.cboMacroSlots.Size = new System.Drawing.Size(232, 22);
			this.cboMacroSlots.TabIndex = 2;
			// 
			// lstMacros
			// 
			this.lstMacros.ItemHeight = 14;
			this.lstMacros.Location = new System.Drawing.Point(72, 80);
			this.lstMacros.Name = "lstMacros";
			this.lstMacros.Size = new System.Drawing.Size(232, 102);
			this.lstMacros.TabIndex = 3;
			// 
			// cmdAssign
			// 
			this.cmdAssign.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAssign.Location = new System.Drawing.Point(16, 192);
			this.cmdAssign.Name = "cmdAssign";
			this.cmdAssign.Size = new System.Drawing.Size(88, 24);
			this.cmdAssign.TabIndex = 4;
			this.cmdAssign.Text = "Assign to Slot";
			this.cmdAssign.Click += new System.EventHandler(this.cmdAssign_Click);
			// 
			// cmdDel
			// 
			this.cmdDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDel.Location = new System.Drawing.Point(112, 192);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Size = new System.Drawing.Size(88, 24);
			this.cmdDel.TabIndex = 4;
			this.cmdDel.Text = "Delete Macro";
			this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(232, 192);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(88, 24);
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// frmMacros
			// 
			this.AcceptButton = this.cmdClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(338, 223);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdAssign);
			this.Controls.Add(this.lstMacros);
			this.Controls.Add(this.cboMacroSlots);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdDel);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMacros";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Macro Bin";
			this.Load += new System.EventHandler(this.frmMacros_Load);
			this.Closed += new System.EventHandler(this.frmMacros_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmMacros_Load(object sender, System.EventArgs e) {
			this.cboMacroSlots.SelectedIndex = 0;

			foreach(CProject.Macro macro in g.Project.MacroList.Values)
				lstMacros.Items.Add(macro.MacroName);

			InitSlots();
		}

		private void cmdDel_Click(object sender, System.EventArgs e) {
			if (lstMacros.SelectedIndex == -1)
				return;

			if (g.Project.MacroList.ContainsKey(lstMacros.SelectedItem)) {
				g.Project.MacroList.Remove(lstMacros.SelectedItem);
				lstMacros.Items.Remove(lstMacros.SelectedItem);

				InitSlots();
			}
		}

		private void InitSlots() {
			string[] macrolist = new string[10];

			foreach(CProject.Macro macro in g.Project.MacroList.Values) {
				if (macro.MacroNum != -1)
					macrolist[macro.MacroNum] = macro.MacroName;
			}

			int curindex = this.cboMacroSlots.SelectedIndex;
			
			if (curindex == -1)
				curindex = 0;

			cboMacroSlots.Items.Clear();
			
			int i = 1;
			foreach(string macro_entry in macrolist) {
				cboMacroSlots.Items.Add("Slot " + i.ToString() + " - " + ((macro_entry == "") ? "Empty" : macro_entry));
				i++;
			}

			cboMacroSlots.SelectedIndex = curindex;

		}

		private void cmdAssign_Click(object sender, System.EventArgs e) {
			if (lstMacros.SelectedIndex == -1)
				return;

			if (!g.Project.MacroList.ContainsKey(lstMacros.SelectedItem))
				return;

			foreach(CProject.Macro macro in g.Project.MacroList.Values) {
				if (macro.MacroNum == lstMacros.SelectedIndex)
					macro.MacroNum = -1;
			}

			((CProject.Macro)g.Project.MacroList[lstMacros.SelectedItem]).MacroNum = lstMacros.SelectedIndex;

			InitSlots();
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmMacros_Closed(object sender, System.EventArgs e) {
			this.Dispose();
		}
	}
}
