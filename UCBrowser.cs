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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace TSDev
{
	/// <summary>
	/// Summary description for UCBrowser.
	/// </summary>
	internal class UCBrowser : System.Windows.Forms.UserControl, RenderSet
	{
		public Crownwood.DotNetMagic.Controls.TabPage _parent_tab;
		private System.Windows.Forms.ImageList ilToolbar;
		private System.Windows.Forms.ToolTip toolTip1;
		public WebBrowser wb;
		private StatusStrip sbMain;
		private ToolStripStatusLabel lblStatus;
		private ToolStripStatusLabel toolStripStatusLabel2;
		private ToolStripStatusLabel lblSecure;
		private ToolStripButton tsbBack;
		private ToolStripButton tsbForward;
		private ToolStripButton tsbStop;
		private ToolStripButton tsbRefresh;
		private ToolStrip tsAddress;
		private ToolStripLabel lblAddress;
		private ToolStripButton cmdGo;
		private ToolStrip tsButtons;
        private ToolStripTextBox txtAddress;
        private ToolStripProgressBar pbPage;
		private System.ComponentModel.IContainer components;

		public UCBrowser(string url, bool hideall)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.SetRenderMode(g.Config.ColorPref);

			wb.StatusTextChanged += new EventHandler(wb_StatusTextChanged);
            wb.EncryptionLevelChanged += new EventHandler(wb_EncryptionLevelChanged);
            wb.ProgressChanged += new WebBrowserProgressChangedEventHandler(wb_ProgressChanged);
            wb.DocumentTitleChanged += new EventHandler(wb_DocumentTitleChanged);
			

			if (hideall) {
				this.sbMain.Visible = false;
				this.tsAddress.Visible = false;
				this.tsButtons.Visible = false;
				this.wb.Dock = DockStyle.Fill;
				this.wb.IsWebBrowserContextMenuEnabled = false;
			}
			
			if (url == "welcome") {
				// Attempt to open template file
				string contents = "";
				try {
					StreamReader reader = new StreamReader(Application.StartupPath + "\\templates\\welcome_template.html");
					contents = reader.ReadToEnd();

					reader.Close();
				} catch (Exception exc) {
					Navigate("about:<i>" + exc.Message + "</i>");
					return;
				}

				contents = contents.Replace("{{APP_PATH}}", "file:///" + Application.StartupPath);

				// Enumerate recent files:
				IDictionaryEnumerator enumer = g.Config.recent_files.GetEnumerator();

				string recent_files = "";
				while(enumer.MoveNext())
					recent_files += "\t\t\t\t\t<li> <a href=\"torquedev://" + enumer.Key.ToString() + "\"" +
						" style=\"text-decoration:none\" title=\"" + enumer.Key.ToString() + "\">" + enumer.Value.ToString() + "</a>";

				contents = contents.Replace("{{RECENTFILES}}", recent_files);
				contents = contents.Replace("{{MOTD}}", g.Config.LastMotd);

				wb.Navigate("about:blank");
				wb.Document.Write(contents);
			} else if (url == "firstrun") {
				// Navigate to the firstrun page
				Navigate("http://www.torquedev.com/firstrun.php?version=" + Application.ProductVersion);
			} else if (url.StartsWith("http://") == false) {
				Navigate(url);
			} else {
				Navigate(url, "", "", "", "");
			}
		}

        void wb_DocumentTitleChanged(object sender, EventArgs e) {
            this._parent_tab.Title = wb.DocumentTitle;
            this._parent_tab.ToolTip = wb.Url.ToString();

            txtAddress.Text = wb.Document.Url.ToString();
        }

        void wb_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) {
            if (e.CurrentProgress == e.MaximumProgress) {
                pbPage.Visible = false;
                pbPage.Value = 0;
            } else {
                pbPage.Visible = true;
                pbPage.Value = (int)((Convert.ToDouble(e.CurrentProgress) / Convert.ToDouble(e.MaximumProgress)) * 100);
            }
        }

        void wb_EncryptionLevelChanged(object sender, EventArgs e) {
            if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Insecure) {
                this.txtAddress.BackColor = Color.White;
                this.lblSecure.Visible = false;
            } else if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit128 ||
                wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit40 ||
                wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit56 ||
                wb.EncryptionLevel == WebBrowserEncryptionLevel.Fortezza) {
                this.txtAddress.BackColor = Color.LightYellow;
                this.lblSecure.Visible = true;

                if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit40)
                    this.lblSecure.Text = "SSL Secured (40-Bit)";
                else if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit56)
                    this.lblSecure.Text = "SSL Secured (56-Bit)";
                else if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Bit128)
                    this.lblSecure.Text = "SSL Secured (128-Bit)";
                else if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Fortezza)
                    this.lblSecure.Text = "SSL Secured (Fortezza)";
            } else if (wb.EncryptionLevel == WebBrowserEncryptionLevel.Mixed) {
                this.txtAddress.BackColor = Color.LightSalmon;
                this.lblSecure.Visible = false;
            }
        }

		void wb_StatusTextChanged(object sender, EventArgs e) {
			lblStatus.Text = wb.StatusText;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				//this.wb.Dispose();

				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBrowser));
			this.ilToolbar = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.wb = new System.Windows.Forms.WebBrowser();
			this.sbMain = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.pbPage = new System.Windows.Forms.ToolStripProgressBar();
			this.lblSecure = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsButtons = new System.Windows.Forms.ToolStrip();
			this.tsbBack = new System.Windows.Forms.ToolStripButton();
			this.tsbForward = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsAddress = new System.Windows.Forms.ToolStrip();
			this.lblAddress = new System.Windows.Forms.ToolStripLabel();
			this.txtAddress = new System.Windows.Forms.ToolStripTextBox();
			this.cmdGo = new System.Windows.Forms.ToolStripButton();
			this.sbMain.SuspendLayout();
			this.tsButtons.SuspendLayout();
			this.tsAddress.SuspendLayout();
			this.SuspendLayout();
			// 
			// ilToolbar
			// 
			this.ilToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolbar.ImageStream")));
			this.ilToolbar.TransparentColor = System.Drawing.Color.Transparent;
			this.ilToolbar.Images.SetKeyName(0, "");
			this.ilToolbar.Images.SetKeyName(1, "");
			this.ilToolbar.Images.SetKeyName(2, "");
			this.ilToolbar.Images.SetKeyName(3, "");
			// 
			// wb
			// 
			this.wb.AllowWebBrowserDrop = false;
			this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wb.Location = new System.Drawing.Point(0, 50);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.ScriptErrorsSuppressed = true;
			this.wb.Size = new System.Drawing.Size(644, 407);
			this.wb.TabIndex = 7;
			this.wb.TabStop = false;
			this.wb.WebBrowserShortcutsEnabled = false;
			this.wb.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wb_Navigated);
			this.wb.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wb_Navigating);
			this.wb.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wb_PreviewKeyDown);
			this.wb.NewWindow += new System.ComponentModel.CancelEventHandler(this.wb_NewWindow);
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// sbMain
			// 
			this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripStatusLabel2,
            this.pbPage,
            this.lblSecure});
			this.sbMain.Location = new System.Drawing.Point(0, 457);
			this.sbMain.Name = "sbMain";
			this.sbMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.sbMain.Size = new System.Drawing.Size(644, 22);
			this.sbMain.SizingGrip = false;
			this.sbMain.TabIndex = 8;
			this.sbMain.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Image = global::TSDev.Properties.Resources.window_earth;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(54, 17);
			this.lblStatus.Text = "Ready";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(575, 17);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// pbPage
			// 
			this.pbPage.Name = "pbPage";
			this.pbPage.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
			this.pbPage.Size = new System.Drawing.Size(125, 16);
			this.pbPage.Visible = false;
			// 
			// lblSecure
			// 
			this.lblSecure.Image = global::TSDev.Properties.Resources._lock;
			this.lblSecure.Name = "lblSecure";
			this.lblSecure.Size = new System.Drawing.Size(113, 17);
			this.lblSecure.Text = "Secure SSL 128-Bit";
			this.lblSecure.Visible = false;
			// 
			// tsButtons
			// 
			this.tsButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward,
            this.tsbStop,
            this.tsbRefresh});
			this.tsButtons.Location = new System.Drawing.Point(0, 0);
			this.tsButtons.Name = "tsButtons";
			this.tsButtons.Size = new System.Drawing.Size(644, 25);
			this.tsButtons.TabIndex = 9;
			this.tsButtons.Text = "toolStrip1";
			this.tsButtons.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tsButtons_PreviewKeyDown);
			// 
			// tsbBack
			// 
			this.tsbBack.Image = global::TSDev.Properties.Resources.arrow_left_blue;
			this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbBack.Name = "tsbBack";
			this.tsbBack.Size = new System.Drawing.Size(49, 22);
			this.tsbBack.Text = "Back";
			this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
			// 
			// tsbForward
			// 
			this.tsbForward.Image = global::TSDev.Properties.Resources.arrow_right_blue;
			this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbForward.Name = "tsbForward";
			this.tsbForward.Size = new System.Drawing.Size(67, 22);
			this.tsbForward.Text = "Forward";
			this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
			// 
			// tsbStop
			// 
			this.tsbStop.Image = global::TSDev.Properties.Resources.stop;
			this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStop.Name = "tsbStop";
			this.tsbStop.Size = new System.Drawing.Size(49, 22);
			this.tsbStop.Text = "Stop";
			this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.Image = global::TSDev.Properties.Resources.refresh;
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(65, 22);
			this.tsbRefresh.Text = "Refresh";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// tsAddress
			// 
			this.tsAddress.CanOverflow = false;
			this.tsAddress.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAddress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAddress,
            this.txtAddress,
            this.cmdGo});
			this.tsAddress.Location = new System.Drawing.Point(0, 25);
			this.tsAddress.Name = "tsAddress";
			this.tsAddress.Size = new System.Drawing.Size(644, 25);
			this.tsAddress.TabIndex = 10;
			this.tsAddress.Text = "toolStrip2";
			this.tsAddress.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tsAddress_PreviewKeyDown);
			// 
			// lblAddress
			// 
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.lblAddress.Size = new System.Drawing.Size(50, 22);
			this.lblAddress.Text = "Ad&dress:";
			// 
			// txtAddress
			// 
			this.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.txtAddress.AutoSize = false;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.txtAddress.Size = new System.Drawing.Size(100, 25);
			this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
			this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
			this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
			// 
			// cmdGo
			// 
			this.cmdGo.Image = global::TSDev.Properties.Resources.nav_right_green;
			this.cmdGo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.cmdGo.Size = new System.Drawing.Size(40, 22);
			this.cmdGo.Text = "Go";
			this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
			// 
			// UCBrowser
			// 
			this.Controls.Add(this.wb);
			this.Controls.Add(this.tsAddress);
			this.Controls.Add(this.tsButtons);
			this.Controls.Add(this.sbMain);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "UCBrowser";
			this.Size = new System.Drawing.Size(644, 479);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UCBrowser_PreviewKeyDown);
			this.Load += new System.EventHandler(this.UCBrowser_Load);
			this.Resize += new System.EventHandler(this.UCBrowser_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCBrowser_KeyDown);
			this.sbMain.ResumeLayout(false);
			this.sbMain.PerformLayout();
			this.tsButtons.ResumeLayout(false);
			this.tsButtons.PerformLayout();
			this.tsAddress.ResumeLayout(false);
			this.tsAddress.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		public void Navigate(string URL, string flags, string targetFrame, string postData, string httpHeaders) {
			wb.Navigate(URL);
		}

		public void Navigate(string URL) {
			
			wb.Navigate(URL);
			wb.Focus();
		}



		private void txtAddress_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				string url = txtAddress.Text;

				Navigate(url);
				e.Handled = true;
				return;
			} else if (e.KeyCode == Keys.Escape) {
				txtAddress.Text = wb.Url.ToString();
				e.Handled = true;
			}
		}


		

		private void txtAddress_Enter(object sender, System.EventArgs e) {
			txtAddress.SelectionStart = 0;
			txtAddress.SelectionLength = txtAddress.Text.Length;
		}

		private void txtAddress_Leave(object sender, System.EventArgs e) {
			txtAddress.SelectionStart = 0;
			txtAddress.SelectionLength = txtAddress.Text.Length;
		}

		private void cmdGo_Click(object sender, System.EventArgs e) {
			Navigate(txtAddress.Text);
		}

		

		

		private void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			string url = e.Url.ToString();

			if (e.TargetFrameName.ToLower() == "_blank") {
				g.Main.SpawnBrowser(url, false);
				e.Cancel = true;
				return;
			}

			if (url.StartsWith("torquedev://")) {
				// Perform a custom action:
				url = url.Substring(0, url.Length - 1).Replace("torquedev://", "");

				if (url == "newproject") {
					g.Main.mnuFile_NewProject_Click(null, null);
				} else if (url == "openproject") {
					g.Main.mnuFile_OpenProj_Click(null, null);
				} else if (url == "welcome") {
					g.Main.SpawnBrowser("welcome", true);
				} else if (url == "firstrun") {
					g.Main.SpawnBrowser("firstrun", false);
				} else {
					// If it's anything else, it's a recent project... parse the
					// project filename and open it, but first decode the URL
					url = url.Remove(0, 1);
					url = System.Web.HttpUtility.UrlDecode(url).Replace("/", "\\");

					// Check if projects are open
					if (g.Project != null) {
						if (!g.Main.PerformCloseOperations())
							return;
					}

					// Open project
					g.Main.OpenProject(url);
				}

				//g.Main.tabMain.LeafForPage(this._parent_tab).TabPages.Remove(this._parent_tab);
				//g.Main._al_browsers.Remove(this._parent_tab);
				g.Main.CloseTab<UCBrowser>(g.Browsers.FindByTab(this._parent_tab));
				g.Browsers.Remove(g.Browsers.FindByTab(this._parent_tab));

				this._parent_tab = null;
				this.Dispose();
				return;
			} else if (url.StartsWith("extern://")) {
				// Spawn an external browser
				url = url.Replace("extern://", "http://");
				System.Diagnostics.Process.Start(url);
			}

		}

		private void wb_NewWindow(object sender, CancelEventArgs e) {
		}

		private void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e) {


            this.txtAddress.Text = wb.Document.Url.ToString();
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			lblStatus.Text = "Done.";

			if (this._parent_tab == null)
				return;

			string pagetitle = wb.Document.Title;

			if (pagetitle.Length > 20)
				pagetitle = pagetitle.Substring(0, 20) + "...";

			this._parent_tab.Title = pagetitle;
		}

		private void UCBrowser_Load(object sender, EventArgs e) {

		}

		private void tsbBack_Click(object sender, EventArgs e) {
			wb.GoBack();
		}

		private void tsbForward_Click(object sender, EventArgs e) {
			wb.GoForward();
		}

		private void tsbStop_Click(object sender, EventArgs e) {
			wb.Stop();
		}

		private void tsbRefresh_Click(object sender, EventArgs e) {
			wb.Refresh();
		}

		private void UCBrowser_Resize(object sender, EventArgs e) {
			this.txtAddress.Width = (this.tsAddress.Width - this.cmdGo.Width - this.lblAddress.Width - 50);
		}

		#region RenderSet Members

		public void SetRenderMode(ColorPref cp) {
			this.tsButtons.RenderMode = ((cp == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.tsAddress.RenderMode = ((cp == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);

			this.sbMain.RenderMode = ((cp == ColorPref.Professional) ? ToolStripRenderMode.Professional :
				ToolStripRenderMode.System);
		}

		#endregion

		private void UCBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			e.IsInputKey = true;
		}

		private void UCBrowser_KeyDown(object sender, KeyEventArgs e) {
			return;
			if (e.KeyCode == Keys.Tab && (e.Modifiers == Keys.Control)) {
				frmFastTab fFastTab = new frmFastTab(false, false);
				fFastTab.Show(this);

				e.SuppressKeyPress = true;
			} else if (e.KeyCode == Keys.Tab && (e.Modifiers == (Keys.Control | Keys.Shift))) {
				frmFastTab fFastTab = new frmFastTab(false, true);
				fFastTab.Show(this);

				e.SuppressKeyPress = true;
			}
		}

		private void tsAddress_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			e.IsInputKey = true;
		}

		private void tsButtons_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			e.IsInputKey = true;
		}

		private void wb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			if (e.KeyCode == Keys.Tab && ((e.Modifiers & Keys.Control) == Keys.Control)) {
				e.IsInputKey = true;

				frmFastTab fFastTab = new frmFastTab(false, e.Shift);
				fFastTab.ShowDialog();
			}
		}

	

    }
}
