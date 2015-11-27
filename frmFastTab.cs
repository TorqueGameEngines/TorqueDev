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
    public partial class frmFastTab : Form {

		private bool isStatic = false;
		private bool isShown = false;

        public frmFastTab(bool isStatic, bool backsel) {
            InitializeComponent();

			if (g.SortedTabList.Count == 0) {
				return;
			}

			// Initialize the listview
			ListViewGroup grp = new ListViewGroup("Active Tabs");
			lvWindows.Groups.Add(grp);
			lvWindows.BeginUpdate();

			int i = 0;
			foreach (SortedTab tab in g.SortedTabList) {

				if (tab.Page.Icon == null) {
					if (tab.Page.Image == null) {
						imageList1.Images.Add(tab.Page.ImageList.Images[tab.Page.ImageIndex]);
					} else {
						imageList1.Images.Add(tab.Page.Image);
					}
				} else {
					imageList1.Images.Add(tab.Page.Icon);
				}

				ListViewItem lvi = new ListViewItem(tab.Page.Title, i, grp);
				lvWindows.Items.Add(lvi);

				i++;
			}

			if (lvWindows.Items.Count > 1) {
				if (!backsel)
					lvWindows.Items[1].Selected = true;
				else
					lvWindows.Items[lvWindows.Items.Count - 1].Selected = true;
			} else {
				lvWindows.Items[0].Selected = true;
			}

			lvWindows.EndUpdate();

			lvWindows.Select();
			lvWindows.Focus();
			
        }

        private void lvWindows_DrawItem(object sender, DrawListViewItemEventArgs e) {
			SolidBrush background = new SolidBrush(Color.FromArgb(193, 210, 238));
			SolidBrush foreground = new SolidBrush(Color.FromArgb(49, 106, 197));

			if (e.Item.Selected) {
				e.Graphics.FillRectangle(background, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
				e.Graphics.DrawRectangle(new Pen(foreground, 1), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
			} else {
				e.Graphics.FillRectangle(Brushes.White, e.Bounds);
			}

            e.Graphics.DrawImage(imageList1.Images[e.Item.ImageIndex], new Point(e.Bounds.Location.X + 3, e.Bounds.Location.Y + 3));

            if (e.Item.Text.Length > 19)
                e.Item.Text = e.Item.Text.Substring(0, 19) + "...";

            
            e.Graphics.DrawString(e.Item.Text, new Font("Arial", 8.25f), Brushes.Black, new PointF(e.Bounds.X +
                imageList1.Images[e.Item.ImageIndex].Width + 6,
                e.Bounds.Y + 4));

        }

		private void lvWindows_SelectedIndexChanged(object sender, EventArgs e) {
			if (lvWindows.SelectedItems.Count > 0) {
				lvWindows.SelectedItems[0].EnsureVisible();

				SortedTab tab = (SortedTab)g.SortedTabList[lvWindows.SelectedItems[0].Index];

				if (tab.Page.Control is UCEditor) {
					lblWindowType.Text = "TorqueScript Code Window";
					lblWindowLine1.Text = (tab.Page.Control as UCEditor).g_curFile.SimpleName;
					lblWindowLine2.Text = System.IO.Path.GetFullPath((tab.Page.Control as UCEditor).g_curFile.RelativePath);
				} else if (tab.Page.Control is UCBrowser) {
					lblWindowType.Text = "Browser Window";
					lblWindowLine1.Text = (tab.Page.Control as UCBrowser).wb.DocumentTitle;
					lblWindowLine2.Text = (tab.Page.Control as UCBrowser).wb.Document.Url.ToString();
				} else {
					PrimaryTab<Control> plugin = g.OtherTabs.FindByControl(tab.Page.Control);

					if (plugin == null) {
						lblWindowType.Text = "Plugin Window";
						lblWindowLine1.Text = "";
						lblWindowLine2.Text = "";
					} else {
						lblWindowType.Text = "Plugin Window";
						lblWindowLine1.Text = plugin.plugin.CWPluginGuid.ToString();
						lblWindowLine2.Text = "";
					}
				}
			}
		}

		private void lvWindows_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.ControlKey) {
				// Select the selected window
				if (lvWindows.SelectedItems.Count > 0) {
					Crownwood.DotNetMagic.Controls.TabPage Page = (g.SortedTabList[lvWindows.SelectedItems[0].Index] as SortedTab).Page;

					Page.Selected = true;

					// Focus if we're a text editor
					if (Page.Control is UCEditor)
						(Page.Control as UCEditor).txtEditor.Focus();
				}

				

				this.Close();

				
			}
		}

		private void lvWindows_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Tab) {
				if (e.Shift == true) {
					if (lvWindows.SelectedItems[0].Index == 0)
						lvWindows.Items[lvWindows.Items.Count - 1].Selected = true;
					else
						lvWindows.Items[lvWindows.SelectedItems[0].Index - 1].Selected = true;
				} else {
					if (lvWindows.SelectedItems[0].Index >= (lvWindows.Items.Count - 1))
						lvWindows.Items[0].Selected = true;
					else
						lvWindows.Items[(lvWindows.SelectedItems[0].Index + 1)].Selected = true;
				}
			} else if (e.KeyCode == Keys.Escape) {
				this.Close();
			}
		}

		private void frmFastTab_Load(object sender, EventArgs e) {
			this.Visible = true;

			lvWindows.Select();
			lvWindows.Focus();

			isShown = true;
		}

		private void lvWindows_Leave(object sender, EventArgs e) {
			if (isShown)
				this.Close();
		}

		private void frmFastTab_Deactivate(object sender, EventArgs e) {
			if (isShown)
				this.Close();
		}

		private void lvWindows_ItemActivate(object sender, EventArgs e) {
			if (lvWindows.SelectedItems.Count > 0) {
				Crownwood.DotNetMagic.Controls.TabPage Page = (g.SortedTabList[lvWindows.SelectedItems[0].Index] as SortedTab).Page;

				Page.Selected = true;
				(Page.Control as UCEditor).txtEditor.Focus();
			}

			this.Close();
		}

		private void frmFastTab_FormClosed(object sender, FormClosedEventArgs e) {
			this.Dispose();
		}
    }
}