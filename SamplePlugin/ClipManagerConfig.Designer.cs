namespace ClipManagerPlugin {
	partial class ClipManagerConfig {
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
			this.chkAllowDupes = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkAllowDupes
			// 
			this.chkAllowDupes.AutoSize = true;
			this.chkAllowDupes.Location = new System.Drawing.Point(13, 18);
			this.chkAllowDupes.Name = "chkAllowDupes";
			this.chkAllowDupes.Size = new System.Drawing.Size(123, 17);
			this.chkAllowDupes.TabIndex = 0;
			this.chkAllowDupes.Text = "&Enable Clip Manager";
			this.chkAllowDupes.UseVisualStyleBackColor = true;
			// 
			// ClipManagerConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Controls.Add(this.chkAllowDupes);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ClipManagerConfig";
			this.Size = new System.Drawing.Size(218, 156);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkAllowDupes;
	}
}
