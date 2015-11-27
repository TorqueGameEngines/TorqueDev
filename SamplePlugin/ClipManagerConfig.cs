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
using TSDev.Plugins;

namespace ClipManagerPlugin {
	/*
	 * IMPORTANT NOTE: Config pages must derive from ICodeweaverConfigPage so that the
	 * proper Load and Commit methods can be called.  Load is called when the properties
	 * dialog is opened; Commit is called when the user presses OK.  There is no event
	 * for CANCEL, so you can assume that when LOAD is called, you can discard any
	 * present data in your config page
	 */

	public partial class ClipManagerConfig : UserControl, ICodeweaverConfigPage {
		public ClipManagerConfig() {
			InitializeComponent();
		}

		#region ICodeweaverConfigPage Members

		public void CWOnConfigCommit() {
			// Save current config
			ClipManagerPlugin.ControlObject.ConfigSetValue(
				ClipManagerPlugin.Self, "Enable", chkAllowDupes.Checked
			);
		}

		public void CWOnConfigLoad() {
			// Load current config
			chkAllowDupes.Checked = (bool)ClipManagerPlugin.ControlObject.ConfigGetValue(
				ClipManagerPlugin.Self, "Enable", false
			);
		}

		#endregion
	}
}
