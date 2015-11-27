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
using System.Collections;
using System.Diagnostics;
using ActiproSoftware.SyntaxEditor;
using System.Reflection;
using System.Drawing;
using System.IO;

namespace TSDev
{
	/// <summary>
	/// Summary description for CIndicators.
	/// </summary>
	internal class CIndicators
	{
		
		internal class BookmarkIndicator : ActiproSoftware.SyntaxEditor.LineIndicator { //: ActiproSoftware.SyntaxEditor.BookmarkIndicator {
			public BookmarkIndicator(int line_num) : base("__bookmark", 0) {
				this.LineNumber = line_num;
			}
			public BookmarkIndicator(string name, int displayPriority, int line_num) : base(name, displayPriority) {
				this.LineNumber = line_num;
			}

			public BookmarkIndicator() : base("__bookmark", 0) {
			}

			public int LineNumber = -1;

			protected override void DrawGlyph(System.Drawing.Graphics g, System.Drawing.Rectangle bounds) {
				Image bookmark_icon = ReadImageFromResource("bookmark.png");

				if (bookmark_icon == null)
					return;

				//g.DrawRectangle(new Pen(Color.Red, 3), bounds);
				g.DrawImage(bookmark_icon, bounds);
			}

		}

		internal class CustomIconIndicator : ActiproSoftware.SyntaxEditor.LineIndicator {
			public CustomIconIndicator(string name, Image icon)
				: base(name, 10) {
				this.name = name;
				this.icon = icon;
			}

			protected override void DrawGlyph(Graphics g, Rectangle bounds) {
				if (icon == null)
					return;

				g.DrawImage(icon, bounds);
			}

			private string name;
			private Image icon;
		}

		internal class CustomIndicator : ActiproSoftware.SyntaxEditor.SpanIndicator {
			public CustomIndicator(string name, Image icon, Color forecolor, Color backcolor, bool bold, bool italic, bool underline) : base(name, 10, true, IndicatorMarks.StyleAssignment) {
				this.name = name;
				this.icon = icon;
				this.forecolor = forecolor;
				this.bold = bold;
				this.italic = italic;
				this.underline = underline;
				this.backcolor = backcolor;
			}

			private string name;
			private Image icon;
			private Color forecolor;
			private Color backcolor;
			private bool bold;
			private bool italic;
			private bool underline;

			protected override HighlightingStyle GetHighlightingStyle() {
				return new HighlightingStyle("__" + name, name, forecolor, backcolor, bold, italic, underline);
			}

			protected override void DrawGlyph(Graphics g, Rectangle bounds) {
				if (icon == null)
					return;

				g.DrawImage(icon, bounds);
			}

			protected override void DrawMarks(Graphics g, Rectangle bounds) {
				return;
			}



		}

		internal class InstrPtr : ActiproSoftware.SyntaxEditor.SpanIndicator {
			public InstrPtr() : base("__ip", 10, true, IndicatorMarks.StyleAssignment) {}

			protected override HighlightingStyle GetHighlightingStyle() {
				return new HighlightingStyle("__ip", "ip", Color.Black, Color.Yellow, false, false, false);
			}

			protected override void DrawGlyph(Graphics g, Rectangle bounds) {
				Image instrptr_icon = ReadImageFromResource("line.png");

				if (instrptr_icon == null)
					return;

				g.DrawImage(instrptr_icon, bounds);
			}

			protected override void DrawMarks(Graphics g, Rectangle bounds) {
				return;
			}
		}

		internal class HaltSpan : ActiproSoftware.SyntaxEditor.SpanIndicator {
			public HaltSpan() : base("__halt", 1, true, IndicatorMarks.StyleAssignment) {
				this.BackColor = Color.DarkRed;
				this.ForeColor = Color.White;
			}

			public HaltSpan(Color bgcolor, Color fgcolor) : base("__halt", 1, true, IndicatorMarks.StyleAssignment) {
				this.BackColor = bgcolor;
				this.ForeColor = fgcolor;
			}

			private Color BackColor;
			private Color ForeColor;

			protected override HighlightingStyle GetHighlightingStyle() {
				return new HighlightingStyle("__halt", "halt", this.ForeColor, this.BackColor, false, false, false);
			}

			protected override void DrawGlyph(Graphics g, Rectangle bounds) {
				Image halt_icon = ReadImageFromResource("breakpoint.png");

				if (halt_icon == null)
					return;

				g.DrawImage(halt_icon, bounds);
			}

			protected override void DrawMarks(Graphics g, Rectangle bounds) {
				return;
			}



		}

		internal class HaltIndicator : ActiproSoftware.SyntaxEditor.LineIndicator {
			public HaltIndicator(int line_num) : base("__halt", 1) {
				LineNumber = line_num;
			}

			public int LineNumber = -1;

			protected override void DrawGlyph(Graphics g, Rectangle bounds) {
				Image halt_icon = ReadImageFromResource("breakpoint.png");

				if (halt_icon == null)
					return;

				g.DrawImage(halt_icon, bounds);
				
			}


		}

		public static Image ReadImageFromResource(string file_name) {
			Assembly curasm = Assembly.GetExecutingAssembly();

			string[] resnames = curasm.GetManifestResourceNames();

			foreach(string res in resnames) {
				if (res.ToLower().EndsWith(file_name.ToLower())) {
					Stream res_stream = curasm.GetManifestResourceStream(res);
					if (res_stream != null) {
						Image out_ico = Image.FromStream(res_stream);
						res_stream.Close();
						res_stream = null;
						return out_ico;
					}
				}
			}

			return null;
		}
	}
}
