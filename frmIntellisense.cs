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
using ActiproSoftware.SyntaxEditor;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmIntellisense.
	/// </summary>
	internal class frmIntellisense : System.Windows.Forms.Form
	{
		private frmIntellisensePrompt fIP;
		private UCEditor editor;
		private CAutoComplete.ClassEntry prevclass;
		private CAutoComplete.ClassEntry.FuncEntry prevfunc;
		private string var;
		private int up;
		private bool bDisplayingPrompt = false;

		private System.Windows.Forms.ListView lvAutoComplete;
		private System.Windows.Forms.ImageList ilList;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ColumnHeader col1;

		public frmIntellisense(UCEditor editor, string var, int up)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.editor = editor;
			this.prevclass = editor.prevclass;
			this.prevfunc = editor.prevfunc;
			this.var = var;
			this.up = up;

			//this.fIP = new frmIntellisensePrompt(new Point(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Bottom));
			this.fIP = new frmIntellisensePrompt(new Point(0,0));

			//this.fIP.Show();
			//this.fIP.Hide();

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntellisense));
			this.lvAutoComplete = new System.Windows.Forms.ListView();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.ilList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lvAutoComplete
			// 
			this.lvAutoComplete.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.lvAutoComplete.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvAutoComplete.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1});
			this.lvAutoComplete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvAutoComplete.FullRowSelect = true;
			this.lvAutoComplete.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvAutoComplete.LabelWrap = false;
			this.lvAutoComplete.LargeImageList = this.ilList;
			this.lvAutoComplete.Location = new System.Drawing.Point(0, 0);
			this.lvAutoComplete.MultiSelect = false;
			this.lvAutoComplete.Name = "lvAutoComplete";
			this.lvAutoComplete.Size = new System.Drawing.Size(186, 138);
			this.lvAutoComplete.SmallImageList = this.ilList;
			this.lvAutoComplete.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvAutoComplete.TabIndex = 0;
			this.lvAutoComplete.TabStop = false;
			this.lvAutoComplete.UseCompatibleStateImageBehavior = false;
			this.lvAutoComplete.View = System.Windows.Forms.View.Details;
			this.lvAutoComplete.DoubleClick += new System.EventHandler(this.lvAutoComplete_DoubleClick);
			this.lvAutoComplete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvAutoComplete_KeyDown);
			this.lvAutoComplete.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvAutoComplete_KeyUp);
			this.lvAutoComplete.Leave += new System.EventHandler(this.lvAutoComplete_Leave);
			// 
			// col1
			// 
			this.col1.Text = "col1";
			this.col1.Width = 48;
			// 
			// ilList
			// 
			this.ilList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilList.ImageStream")));
			this.ilList.TransparentColor = System.Drawing.Color.Transparent;
			this.ilList.Images.SetKeyName(0, "");
			this.ilList.Images.SetKeyName(1, "");
			this.ilList.Images.SetKeyName(2, "");
			this.ilList.Images.SetKeyName(3, "");
			this.ilList.Images.SetKeyName(4, "PublicProperty.ico");
			// 
			// frmIntellisense
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(186, 138);
			this.ControlBox = false;
			this.Controls.Add(this.lvAutoComplete);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmIntellisense";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Deactivate += new System.EventHandler(this.frmIntellisense_Deactivate);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmIntellisense_Closing);
			this.Load += new System.EventHandler(this.frmIntellisense_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmIntellisense_Load(object sender, System.EventArgs e) {
			if (g.Project == null)
				return;

			UpdateMainClassList();

			if ((this.Location.Y + this.Height) > Screen.FromControl(this.lvAutoComplete).Bounds.Height)
				this.Location = new Point(this.Location.X, this.Location.Y - (this.Height + up + 3));
			
			this.Show();
			SelectPrompt();
		}

		private void SelectPrompt() {
			if (this.lvAutoComplete.SelectedItems.Count == 0)
				return;

			bDisplayingPrompt = true;

			ListViewItem selitem = this.lvAutoComplete.SelectedItems[0];
			Rectangle rect = selitem.GetBounds(ItemBoundsPortion.Entire);
			Point cltpt = new Point(lvAutoComplete.Width + 3, rect.Top);
			this.fIP.Location = this.lvAutoComplete.PointToScreen(cltpt);

			if (selitem.Tag is CAutoComplete.ClassEntry) {
				CAutoComplete.ClassEntry cls = (CAutoComplete.ClassEntry)selitem.Tag;
				this.fIP.SetPrompt("<b>" + cls.ClassName + "</b>" + ((cls.ClassInheritsFrom == "") ? "" : " Inherits <u>" + cls.ClassInheritsFrom + "</u>")
					+ "<br /><i>(" + cls.func_list.Count.ToString() + " functions)</i>");
			} else if (selitem.Tag is CAutoComplete.ClassEntry.FuncEntry) {
				CAutoComplete.ClassEntry.FuncEntry func = (CAutoComplete.ClassEntry.FuncEntry)selitem.Tag;
				this.fIP.SetPrompt("<u>" + func.func_ret + "</u> <b>" + func.func_name + "</b> (" + func.func_params.Replace("<", "&lt;") + ")" + ((func.func_descr != "") ? "<br />[" + func.func_descr.Replace("<", "&lt;") + "]" : ""));
			} else if (selitem.Tag is CAutoComplete.ClassEntry.PropEntry) {
				CAutoComplete.ClassEntry.PropEntry prop = (CAutoComplete.ClassEntry.PropEntry)selitem.Tag;
				this.fIP.SetPrompt(prop.prop_type + " <b>" + prop.prop_name + "</b>");
			} else {
				this.fIP.SetPrompt("<i>" + selitem.Text + "</i>");
			}
			
			this.fIP.Show();
			this.fIP.BringToFront();

			this.lvAutoComplete.Focus();

			bDisplayingPrompt = false;
		}

		private void UpdateMainClassList() {
			this.lvAutoComplete.Focus();
			this.lvAutoComplete.Select();

			this.lvAutoComplete.Items.Clear();
			this.lvAutoComplete.BeginUpdate();

			int longest_member = 0;
			
			foreach(CAutoComplete.ClassEntry cls in frmMain.ac.ReturnAllClasses().Values) {
				
				ListViewItem newitem = new ListViewItem(cls.ClassName, 1);
				newitem.Tag = cls;

				if (prevclass != null) {
					if (cls == prevclass)
						newitem.Selected = true;
				}

				this.lvAutoComplete.Items.Add(newitem);
			
				if (cls.ClassName.Length > longest_member)
					longest_member = cls.ClassName.Length;

			}

			this.col1.Width =  -1;
			this.Width = col1.Width + 25;

			this.lvAutoComplete.EndUpdate();

			if (this.lvAutoComplete.SelectedItems.Count > 0) {
				this.lvAutoComplete.SelectedItems[0].EnsureVisible();
				this.lvAutoComplete.SelectedItems[0].Focused = true;
				this.lvAutoComplete.SelectedItems[0].Selected = true;
			} else {
				this.lvAutoComplete.Items[0].EnsureVisible();
				this.lvAutoComplete.Items[0].Selected = true;
				this.lvAutoComplete.Items[0].Focused = true;
			}

			this.lvAutoComplete.Focus();
			SelectPrompt();
		}

		private void UpdateClassMembers(CAutoComplete.ClassEntry cls) {
			this.lvAutoComplete.Focus();
			this.lvAutoComplete.Select();

			this.lvAutoComplete.Items.Clear();
			this.lvAutoComplete.BeginUpdate();

			string item_header = "! " + cls.ClassName + ((cls.ClassInheritsFrom == "") ? "" : " : " + cls.ClassInheritsFrom) + " Members";
			int longest_member = item_header.Length;

			this.lvAutoComplete.Items.Add(new ListViewItem(item_header, 3));
			
			// Add all the functions
			foreach(CAutoComplete.ClassEntry.FuncEntry func in cls.func_list.Values) {
				
				string entryname = func.func_name;
				ListViewItem newitem = new ListViewItem(entryname, 0);
				newitem.Tag = func;

				if (this.prevfunc != null) {
					if (func == prevfunc) {
						newitem.Focused = true;
						newitem.Selected = true;
					}
				}

				this.lvAutoComplete.Items.Add(newitem);
			
				if (entryname.Length > longest_member)
					longest_member = entryname.Length;

			}

			// Add all the properties
			foreach(CAutoComplete.ClassEntry.PropEntry prop in cls.prop_list.Values) {
				string entryname = prop.prop_name;
				ListViewItem newitem = new ListViewItem(entryname, 4);
				newitem.Tag = prop;

				this.lvAutoComplete.Items.Add(newitem);

				if (entryname.Length > longest_member)
					longest_member = entryname.Length;
			}

			this.col1.Width =  -1;
			this.Width = col1.Width + 25;

			this.lvAutoComplete.EndUpdate();

			if (this.lvAutoComplete.SelectedItems.Count > 0) {
				this.lvAutoComplete.SelectedItems[0].EnsureVisible();
				this.lvAutoComplete.SelectedItems[0].Focused = true;
				this.lvAutoComplete.SelectedItems[0].Selected = true;
			} else {
				this.lvAutoComplete.Items[0].EnsureVisible();
				this.lvAutoComplete.Items[0].Selected = true;
				this.lvAutoComplete.Items[0].Focused = true;
			}

			this.lvAutoComplete.Focus();
			SelectPrompt();
		}

		private void lvAutoComplete_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			this.fIP.Hide();

			if (e.KeyCode == Keys.Escape) {
				editor.txtEditor.Focus();
				this.Close();
				this.Dispose();
			} else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.End || e.KeyCode == Keys.Home) {
				  // Update the side listing
				  SelectPrompt();
			} else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Right) {
				// Displays the members of the selected object, if it's a class
				if (this.lvAutoComplete.SelectedItems.Count == 0)
					return;

				if (!(this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry))
					return;

				this.prevclass = (CAutoComplete.ClassEntry)this.lvAutoComplete.SelectedItems[0].Tag;

				UpdateClassMembers((CAutoComplete.ClassEntry)this.lvAutoComplete.SelectedItems[0].Tag);					
			} else if (e.KeyCode == Keys.Left) {
				// Displays the previous class's information
				if (this.lvAutoComplete.SelectedItems.Count > 0 && this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry.FuncEntry) {
					this.prevfunc = (CAutoComplete.ClassEntry.FuncEntry)lvAutoComplete.SelectedItems[0].Tag;
				}

				UpdateMainClassList();
			} else if (e.KeyCode == Keys.Enter) {
				// Commit an item to the active editor
				if (this.lvAutoComplete.SelectedItems.Count == 0)
					return;

				if (this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry) {
					this.prevclass = (CAutoComplete.ClassEntry)lvAutoComplete.SelectedItems[0].Tag;

					if (var != "") {
						int line = editor.txtEditor.SelectedView.Selection.EndPosition.Line;
						int chr = editor.txtEditor.SelectedView.Selection.EndPosition.Character;

						TextStream ts = editor.txtEditor.Document.GetTextStream(editor.txtEditor.SelectedView.Selection.StartOffset);
						ts.GoToPreviousToken("LineTerminatorToken");

						editor.txtEditor.SelectedView.Selection.StartOffset = ts.CurrentToken.EndOffset;
						editor.txtEditor.SelectedView.InsertLineBreak();
						ts = editor.txtEditor.Document.GetTextStream(ts.CurrentToken.StartOffset);
						ts.GoToNextToken("LineTerminatorToken");
						editor.txtEditor.SelectedView.Selection.StartOffset = ts.CurrentToken.EndOffset;
						
						editor.txtEditor.SelectedView.Selection.StartOffset = ts.CurrentToken.EndOffset;
						editor.txtEditor.SelectedView.InsertText("//# DECLARE " + var + " as " + (this.lvAutoComplete.SelectedItems[0].Tag as CAutoComplete.ClassEntry).ClassName);

						editor.txtEditor.SelectedView.Selection.StartOffset = editor.txtEditor.Document.PositionToOffset(new Position(line + 1, chr));
					} else {
						editor.txtEditor.SelectedView.InsertText((this.lvAutoComplete.SelectedItems[0].Tag as CAutoComplete.ClassEntry).ClassName + " ");
					}
				} else if (this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry.FuncEntry) {
					this.prevfunc = (CAutoComplete.ClassEntry.FuncEntry)lvAutoComplete.SelectedItems[0].Tag;
					editor.txtEditor.SelectedView.InsertText((this.lvAutoComplete.SelectedItems[0].Tag as CAutoComplete.ClassEntry.FuncEntry).func_name);
				} else if (this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry.PropEntry) {
					editor.txtEditor.SelectedView.InsertText((this.lvAutoComplete.SelectedItems[0].Tag as CAutoComplete.ClassEntry.PropEntry).prop_name);
				} else
					return;
				
				editor.txtEditor.Focus();
				this.Close();
				this.Dispose();

				this.fIP.Close();
				this.fIP.Dispose();
			}
		}


		private void lvAutoComplete_Leave(object sender, System.EventArgs e) {
			this.Close();
			this.Dispose();
		}

		private void frmIntellisense_Deactivate(object sender, System.EventArgs e) {
			if (bDisplayingPrompt)
				return;
			
			editor.txtEditor.Focus();
			this.Close();
			this.Dispose();

			this.fIP.Close();
			this.fIP.Dispose();
		}

		private void frmIntellisense_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			editor.prevclass = this.prevclass;
			editor.prevfunc = this.prevfunc;

			if (!fIP.Disposing) {
				this.fIP.Close();
				this.fIP.Dispose();
			}
		}

		private void lvAutoComplete_DoubleClick(object sender, System.EventArgs e) {
			if (this.lvAutoComplete.SelectedItems.Count == 0)
				return;

			if (this.lvAutoComplete.SelectedItems[0].Tag is CAutoComplete.ClassEntry) {
				if (this.var == "")
					this.lvAutoComplete_KeyDown(sender, new KeyEventArgs(Keys.Tab));
				else
					this.lvAutoComplete_KeyDown(sender, new KeyEventArgs(Keys.Enter));
			} else {
				this.lvAutoComplete_KeyDown(sender, new KeyEventArgs(Keys.Enter));
			}
		}

		private void lvAutoComplete_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.End || e.KeyCode == Keys.Home) {
				// Update the side listing
				SelectPrompt();
			} 
		}

		
	}
}
