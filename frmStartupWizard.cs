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
	internal partial class frmStartupWizard : Form {
		private int step = 1;

		public frmStartupWizard() {
			InitializeComponent();

			
		}

		private void frmStartupWizard_Load(object sender, EventArgs e) {
			this.pnlStep1.Visible = true;
			this.lstScheme.SelectedIndex = 0;
			this.lstSelKeys.SelectedIndex = 0;
		}

		private void cmdNext_Click(object sender, EventArgs e) {
			if (step == 1) {
				// Going to step 2
				this.pnlStep1.Visible = false;
				this.pnlStep2.Visible = true;

				this.lblTitle.Text = "Step 2 - Select Color Scheme";
				this.cmdPrevious.Enabled = true;
				step = 2;
			} else if (step == 2) {
				// Finishing up
				if (lstScheme.SelectedIndex == 0)
					g.Config.LoadScheme_Default();
				else if (lstScheme.SelectedIndex == 1)
					g.Config.LoadScheme_Noolness();

				if (lstSelKeys.SelectedIndex == 0 || lstSelKeys.SelectedIndex == 2)
					g.Config.LoadKeymap_TD();
				else if (lstSelKeys.SelectedIndex == 1 || lstSelKeys.SelectedIndex == 3)
					g.Config.LoadKeymap_Default();

				this.Close();
			}
		}

		private void cmdPrevious_Click(object sender, EventArgs e) {
			if (step == 1) {
				// What?!
				return;
			} else if (step == 2) {
				// Going to step 1
				this.pnlStep2.Visible = false;
				this.pnlStep1.Visible = true;

				this.lblTitle.Text = "Step 1 - Select your Environment";
				this.cmdPrevious.Enabled = false;
				step = 1;
			}
		}

		private void lstScheme_SelectedIndexChanged(object sender, EventArgs e) {
			if (lstScheme.SelectedIndex == 0) {
				// Load the default picture
				imgSchemeLook.Image = global::TSDev.Properties.Resources.default_colors;
			} else if (lstScheme.SelectedIndex == 1) {
				// Load the Noolness picture
				imgSchemeLook.Image = global::TSDev.Properties.Resources.noolness_colors;
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}