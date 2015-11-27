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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ClipManagerPlugin {
	public partial class ClipManagerWindow : UserControl {
		private class StringObject {
			private string stringValue;

			public StringObject(string value) {
				this.stringValue = value;
			}

			public override string ToString() {
				if (stringValue.Length > 30)
					return stringValue.Substring(0, 25) + "...";
				else
					return stringValue;
			}

			public string StringValue {
				get { return stringValue; }
			}
		}

		public ClipManagerWindow() {
			InitializeComponent();
		}

		public void RefreshClips() {
			lstClips.Items.Clear();

			foreach (string s in ClipManagerPlugin.StringCollection)
				lstClips.Items.Add(new StringObject(s));
		}

		private void lstClips_SelectedIndexChanged(object sender, EventArgs e) {
			if (lstClips.SelectedItem == null)
				return;

			// Set the textbox and format linebreaks
			txtClipContents.Text = (lstClips.SelectedItem as StringObject).StringValue.Replace("\n", "\r\n");
		}

		private void ctmRemove_Opening(object sender, CancelEventArgs e) {
			mnuRemove.Enabled = (lstClips.Items.Count > 0);
		}

		private void mnuRemove_Click(object sender, EventArgs e) {
			// Remove currently selected item
			ClipManagerPlugin.StringCollection.Remove((lstClips.SelectedItem as StringObject).StringValue);
			lstClips.Items.Remove(lstClips.SelectedItem);
		}
	}
}
