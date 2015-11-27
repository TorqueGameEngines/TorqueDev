namespace ClipManagerPlugin {
	partial class ClipManagerWindow {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.lstClips = new System.Windows.Forms.ListBox();
			this.txtClipContents = new System.Windows.Forms.TextBox();
			this.ctmRemove = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.ctmRemove.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstClips
			// 
			this.lstClips.ContextMenuStrip = this.ctmRemove;
			this.lstClips.Dock = System.Windows.Forms.DockStyle.Top;
			this.lstClips.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstClips.FormattingEnabled = true;
			this.lstClips.IntegralHeight = false;
			this.lstClips.ItemHeight = 14;
			this.lstClips.Location = new System.Drawing.Point(0, 0);
			this.lstClips.Name = "lstClips";
			this.lstClips.Size = new System.Drawing.Size(514, 193);
			this.lstClips.TabIndex = 0;
			this.lstClips.SelectedIndexChanged += new System.EventHandler(this.lstClips_SelectedIndexChanged);
			// 
			// txtClipContents
			// 
			this.txtClipContents.BackColor = System.Drawing.SystemColors.Window;
			this.txtClipContents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtClipContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtClipContents.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClipContents.Location = new System.Drawing.Point(0, 193);
			this.txtClipContents.Multiline = true;
			this.txtClipContents.Name = "txtClipContents";
			this.txtClipContents.ReadOnly = true;
			this.txtClipContents.Size = new System.Drawing.Size(514, 163);
			this.txtClipContents.TabIndex = 1;
			// 
			// ctmRemove
			// 
			this.ctmRemove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemove});
			this.ctmRemove.Name = "ctmRemove";
			this.ctmRemove.Size = new System.Drawing.Size(153, 48);
			this.ctmRemove.Opening += new System.ComponentModel.CancelEventHandler(this.ctmRemove_Opening);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Name = "mnuRemove";
			this.mnuRemove.Size = new System.Drawing.Size(152, 22);
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// ClipManagerWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtClipContents);
			this.Controls.Add(this.lstClips);
			this.Name = "ClipManagerWindow";
			this.Size = new System.Drawing.Size(514, 356);
			this.ctmRemove.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstClips;
		private System.Windows.Forms.TextBox txtClipContents;
		private System.Windows.Forms.ContextMenuStrip ctmRemove;
		private System.Windows.Forms.ToolStripMenuItem mnuRemove;
	}
}
