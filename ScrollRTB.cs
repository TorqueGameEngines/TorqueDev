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
using System.Windows.Forms;

namespace TSDev {
	/// <summary>
	/// Summary description for ScrollableRichTextbox.
	/// </summary>
	internal class ScrollRTB : System.Windows.Forms.RichTextBox { 
		// constants for the message sending 
		const int WM_VSCROLL = 0x0115; 
		const int WM_LBUTTONDOWN = 0x0201; 
		const int WM_SETFOCUS = 0x0007; 
		const int WM_KILLFOCUS = 0x0008; 
		readonly IntPtr SB_ENDSCROLL = (IntPtr)8; 
		readonly IntPtr SB_BOTTOM = (IntPtr)7; 
		// flag we use to determine if we can scroll 
		bool _scrollable = true; 
		// locking object 
		object _scrollLock = new object();
		public void AppendText(string text, bool scrollToEnd) { 
			lock(_scrollLock) { 
				if(IntPtr.Zero != base.Handle) { 
					decimal length = base.Text.Length + text.Length; 
					if(length >= base.MaxLength) base.Clear(); 
					//base.Text += text; 
					base.AppendText(text);
					if(_scrollable && scrollToEnd) { 
						if(IntPtr.Zero != base.Handle) { 
							base.SelectionStart = base.Text.Length; 
							Message m = Message.Create(base.Handle, WM_VSCROLL, SB_BOTTOM, IntPtr.Zero); 
							base.WndProc(ref m); 
						} 
					} 
				} 
			} 
		} 

		protected override void WndProc(ref Message m) { 
			// if we're in a scroll set the scrolling flag to false & skip the 
			// auto scroll 
			if((m.Msg == WM_LBUTTONDOWN) || m.Msg == WM_VSCROLL && m.WParam != SB_BOTTOM) { 
				_scrollable = false; 
			} 

			// if we are done scrolling, set the falg to true & do the scrolling 
			if(m.Msg == WM_VSCROLL && m.WParam == SB_ENDSCROLL) { 
				_scrollable = true; 
			} 
			// this keeps the user from setting the cursor in the textbox 
			//  because that causes problems if they do 
			//if(m.Msg == WM_SETFOCUS && base.ReadOnly) m.Msg = WM_KILLFOCUS; 

			base.WndProc (ref m); 
		} 
	} 
}
