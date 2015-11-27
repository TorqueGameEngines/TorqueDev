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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using System.IO;
using netMercs.Windows.Forms;

namespace TSDev
{
	/// <summary>
	/// Summary description for frmProperties.
	/// </summary>
	internal class frmProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propgrid;

		private PropertyBag propbag = new PropertyBag();

		private CProject.File file;
		private CProject proj;
		private int proptype;
		
		private System.Windows.Forms.Button cmdClose;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmProperties(CProject proj, CProject.File file, int proptype)
		{
			//
			// Required for Windows Form Designer support
			//
			

			if (proptype == 0)
				this.file = file;
			else
				this.proj = proj;

			this.proptype = proptype;

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
			this.propgrid = new System.Windows.Forms.PropertyGrid();
			this.cmdClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// propgrid
			// 
			this.propgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propgrid.CommandsVisibleIfAvailable = true;
			this.propgrid.LargeButtons = false;
			this.propgrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propgrid.Location = new System.Drawing.Point(8, 8);
			this.propgrid.Name = "propgrid";
			this.propgrid.Size = new System.Drawing.Size(278, 486);
			this.propgrid.TabIndex = 0;
			this.propgrid.Text = "PropertyGrid";
			this.propgrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propgrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(198, 502);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(88, 24);
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// frmProperties
			// 
			this.AcceptButton = this.cmdClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(296, 534);
			this.Controls.Add(this.propgrid);
			this.Controls.Add(this.cmdClose);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "frmProperties";
			this.ShowInTaskbar = false;
			this.Text = "Properties";
			this.Load += new System.EventHandler(this.frmProperties_Load);
			this.Closed += new System.EventHandler(this.frmProperties_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private class FileEditorUI : UITypeEditor {
			public FileEditorUI() {}

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
				return System.Drawing.Design.UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Title = "Select Executable";
				ofd.Filter = "Executables (*.exe)|*.exe";
				ofd.CheckFileExists = true;
				DialogResult result = ofd.ShowDialog();

				if (result == DialogResult.Cancel)
					return value;
				else
					return CProject.PathGetRelative(ofd.FileName, g.Project.ProjectPath);
			}
		}

		private class ScriptFileEditorUI : UITypeEditor {
			public ScriptFileEditorUI() { }

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
				return System.Drawing.Design.UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Title = "Select Script File";
				ofd.Filter = "TorqueScript Files (*.cs)|*.cs";
				ofd.CheckFileExists = true;
				DialogResult result = ofd.ShowDialog();

				if (result == DialogResult.Cancel)
					return value;
				else
					return CProject.PathGetRelative(ofd.FileName, g.Project.ProjectPath);
			}
		}

		public static string GetTypeEx(short type) {
			switch (type.ToString()) {
				case "0":
					return "TGE";
				case "1":
					return "TSE";
				case "2":
					return "T2D/TGB";
				default:
					return "?";
			}
		}

		public static short GetTypeEx(string type) {
			switch (type) {
				case "TGE":
					return 0;
				case "TSE":
					return 1;
				case "T2D/TGB":
					return 2;
				default:
					return 0;
			}
		}

		private class ProjectTypeEditorUI : UITypeEditor {
			public ProjectTypeEditorUI() {}

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
				return UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
				frmSelProjType fSel = new frmSelProjType();
				fSel.lvTemplates.Items[Convert.ToInt32(GetTypeEx(value.ToString()))].Selected = true;
				DialogResult result = fSel.ShowDialog();

				short newtype = Convert.ToInt16(fSel.lvTemplates.SelectedIndices[0]);
				fSel.Dispose();
				fSel = null;

				if (result == DialogResult.Cancel)
					return value;
				else
					return GetTypeEx(newtype);
			}

		}

		private class DirectoryEditorUI : UITypeEditor {
			public DirectoryEditorUI() {}

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
				return UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				fbd.SelectedPath = g.Project.ProjectPath;
				fbd.ShowNewFolderButton = false;
				fbd.Description = "Please select a folder below to use as the base path for your project:";
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.Cancel)
					return value;
				else
					return fbd.SelectedPath;
			}
		}

		private void frmProperties_Load(object sender, System.EventArgs e) {
			if (this.proptype == 0) {
				propbag.Properties.Add(new PropertySpec("Relative Path", typeof(string), "Misc", "Specifies the filename relative to the project's root directory.", file.RelativePath));
				propbag.Properties.Add(new PropertySpec("Friendly Name", typeof(string), "Misc", "Friendly name of the file displayed in the project view.", file.SimpleName));
				propbag.Properties.Add(new PropertySpec("Treat as Text", typeof(bool), "Misc", "If true, the file will not be treated as a code file.  This means that the file will not be scanned for functions/objects or errors, nor will it be syntax-highlighted or provide auto-completion.", file.isText));
			} else if (this.proptype == 1) {
				propbag.Properties.Add(new PropertySpec("Project Name", typeof(string), "Project", "Specifies the current project's name.", proj.ProjectName));
				propbag.Properties.Add(new PropertySpec("Debug Parameters", typeof(string), "Debugging", "Specifies the command-line parameters to use when executing the application.", this.proj.DebugParams));
				propbag.Properties.Add(new PropertySpec("Project Path", typeof(string), "Project", "Specifies the absolute path to the root of the project, from which all files derive.", proj.ProjectPath, typeof(frmProperties.DirectoryEditorUI), typeof(System.ComponentModel.StringConverter)));
				propbag.Properties.Add(new PropertySpec("Project Type", typeof(string), "Project", "Specifies the type of project being edited.  This property adjusts the type of auto-complete the editor uses.", GetTypeEx(this.proj.ProjectType), typeof(frmProperties.ProjectTypeEditorUI), typeof(System.ComponentModel.StringConverter)));
				
				propbag.Properties.Add(new PropertySpec("Executable Path", typeof(string), "Debugging", "Specifies the project's executable path and filename (used for debugging)", proj.DebugExe, typeof(frmProperties.FileEditorUI), typeof(System.ComponentModel.StringConverter)));
				propbag.Properties.Add(new PropertySpec("Debug Password", typeof(string), "Debugging", "Specifies the password to use to connect to the debugger interface of the project.", proj.DebugPasswd));
				propbag.Properties.Add(new PropertySpec("Debug Port", typeof(int), "Debugging", "Specifies the port to connect to the debugger interface.", proj.DebugPort));
				propbag.Properties.Add(new PropertySpec("Enable Debugging", typeof(bool), "Debugging", "Specifies whether or not to enable debugging.", proj.DebugEnabled));

				propbag.Properties.Add(new PropertySpec("Debug Startup Script", typeof(string), "OneClick Debugging", "Specifies the engine's primary main.cs file in which to insert the dbgSetParameters automatically.", proj.DebugMainCs, typeof(frmProperties.ScriptFileEditorUI), typeof(System.ComponentModel.StringConverter)));
				propbag.Properties.Add(new PropertySpec("Enable 'OneClick' Debugging", typeof(bool), "OneClick Debugging", "Specifies whether or not to enable automatic insertion of dbgSetParameters into the main.cs file upon debugging.", proj.DebugAutoInsert));
			}

			propbag.SetValue += new PropertySpecEventHandler(propbag_SetValue);
			propbag.GetValue += new PropertySpecEventHandler(propbag_GetValue);

			propgrid.SelectedObject = propbag;
		}

		private void propbag_SetValue(object sender, PropertySpecEventArgs e) {
			if (this.proptype == 0) {
				if (e.Property.Name == "Relative Path")
					file.RelativePath = e.Value.ToString();
				else if (e.Property.Name == "Friendly Name")
					file.SimpleName = e.Value.ToString();
				else if (e.Property.Name == "Treat as Text")
					file.isText = Convert.ToBoolean(e.Value);
			} else if (this.proptype == 1) {
				switch (e.Property.Name) {
					case "Project Name":
						proj.ProjectName = e.Value.ToString();
						break;
					case "Debug Parameters":
						this.proj.DebugParams = e.Value.ToString();
						break;
					case "Project Path":
						if (!Directory.Exists(e.Value.ToString())) {
							MessageBox.Show(this, "Invalid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						proj.ProjectPath = e.Value.ToString();
						break;
					case "Executable Path":
						if (e.Value.ToString() == "") {
							proj.DebugExe = e.Value.ToString();
						} else if (!File.Exists(e.Value.ToString())) {
							MessageBox.Show(this, "Invalid executable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						proj.DebugExe = e.Value.ToString();
						break;
					case "Debug Password":
						proj.DebugPasswd = e.Value.ToString();
						break;
					case "Debug Port":
						if (Convert.ToInt32(e.Value) > 65534 || Convert.ToInt32(e.Value) < 10) {
							MessageBox.Show(this, "Invalid port.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						proj.DebugPort = Convert.ToInt32(e.Value);
						break;
					case "Enable Debugging":
						proj.DebugEnabled = Convert.ToBoolean(e.Value);
						break;
					case "Debug Startup Script":
						proj.DebugMainCs = e.Value.ToString();
						break;
					case "Enable 'OneClick' Debugging":
						proj.DebugAutoInsert = Convert.ToBoolean(e.Value);
						break;
					case "Project Type":
                        if (e.Value.ToString() == "")
							e.Value = ((g.Config.ActivationHasTGE) ? "TGE" : 
								((g.Config.ActivationHasTSE) ? "TSE" :
								((g.Config.ActivationHasT2D) ? "T2D/TGB" : "TGE"
								)));

						// Verify the project type is licensed
						if (e.Value.ToString() == "TGE" && g.Config.ActivationHasTGE == false) {
							MessageBox.Show(this, "Error: You are not licensed to open TGE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						} else if (e.Value.ToString() == "TSE" && g.Config.ActivationHasTSE == false) {
							MessageBox.Show(this, "Error: You are not licensed to open TSE projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						} else if (e.Value.ToString() == "T2D/TGB" && g.Config.ActivationHasT2D == false) {
							MessageBox.Show(this, "Error: You are not licensed to open T2D projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}

						proj.ProjectType = GetTypeEx(e.Value.ToString());

						g.Main.LoadAutoComplete();
						g.Main.InitPublicList();
						break;
				}
			}

			g.Main.InitProject();
		}

		private void propbag_GetValue(object sender, PropertySpecEventArgs e) {
			if (this.proptype == 0) {
				if (e.Property.Name == "Relative Path")
					e.Value = file.RelativePath;
				else if (e.Property.Name == "Friendly Name")
					e.Value = file.SimpleName;
				else if (e.Property.Name == "Treat as Text")
					e.Value = file.isText;
			} else if (this.proptype == 1) {
				switch (e.Property.Name) {
					case "Project Name":
						e.Value = proj.ProjectName;
						break;
					case "Project Path":
						e.Value = proj.ProjectPath;
						break;
					case "Executable Path":
						e.Value = proj.DebugExe;
						break;
					case "Debug Password":
						e.Value = proj.DebugPasswd;
						break;
					case "Debug Port":
						e.Value = proj.DebugPort;
						break;
					case "Debug Parameters":
						e.Value = proj.DebugParams;
						break;
					case "Enable Debugging":
						e.Value = proj.DebugEnabled;
						break;
					case "Project Type":
						e.Value = GetTypeEx(proj.ProjectType);
						break;
					case "Enable 'OneClick' Debugging":
						e.Value = proj.DebugAutoInsert;
						break;
					case "Debug Startup Script":
						e.Value = proj.DebugMainCs;
						break;
				}
			}
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmProperties_Closed(object sender, System.EventArgs e) {
			this.Dispose();
		}
	}
}
