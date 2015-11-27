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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSDev {
	public partial class frmEula : Form {
		public frmEula() {
			InitializeComponent();

			// Initialize the typetext array
			typetext_array = new char[typetext.Length];

			int i = 0;
			foreach (char c in typetext) {
				typetext_array[i] = c;
				i++;
			}

			txtEula.LoadFile(Application.StartupPath + "\\EULA.rtf");

			DialogResult = DialogResult.Cancel;

		}

		int printchar = 0;

		string typetext = "A new license agreement has been integrated into TorqueDev.  The important portions will be highlighted in this area. " +
			"The rest of it is available in an RTF textbox below.\r\n\r\n" +
			"Credit.  If you or your company's use of TorqueDev (formerly Codeweaver) has contributed to your project " +
			"in any way, you are required to visibly credit \"Sam Bacsa / netMercs Group\" under either a \"Thanks To\", \"Special Thanks To\" " +
			"or a \"Utilities\" section of your game's credits, whether in print or in the actual application.\r\n\r\n" +
			"** The rest of the license agreement, including the license agreement for the usage of plugins (and their development)" +
			", is available in the textbox below. ***";

		char[] typetext_array = null;

		private void tmrTyper_Tick(object sender, EventArgs e) {
			if (printchar >= typetext_array.Length) {
				tmrTyper.Enabled = false;

				optAccept.Enabled = true;
				optDecline.Enabled = true;
				txtEula.Focus();
				txtEula.Select();
				return;
			}

			txtTyper.AppendText(typetext_array[printchar++].ToString());
			txtTyper.ScrollToCaret();


		}

		private void optAccept_CheckedChanged(object sender, EventArgs e) {
			cmdOK.Enabled = true;
		}

		private void optDecline_CheckedChanged(object sender, EventArgs e) {
			cmdOK.Enabled = true;
		}

		private void txtTyper_Enter(object sender, EventArgs e) {
			txtEula.Focus();
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			if (optAccept.Checked)
				DialogResult = DialogResult.OK;
			else
				DialogResult = DialogResult.Cancel;

			this.Close();
		}

		private void frmEula_FormClosing(object sender, FormClosingEventArgs e) {
			
		}
	}
}