namespace TSDev {
	partial class frmStartupWizard {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.Label label4;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStartupWizard));
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pnlStep1 = new System.Windows.Forms.Panel();
			this.lstSelKeys = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlStep2 = new System.Windows.Forms.Panel();
			this.optStyleProfessional = new System.Windows.Forms.RadioButton();
			this.optStyleStd = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.imgSchemeLook = new System.Windows.Forms.PictureBox();
			this.lstScheme = new System.Windows.Forms.ListBox();
			this.cmdNext = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdPrevious = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pnlStep1.SuspendLayout();
			this.pnlStep2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgSchemeLook)).BeginInit();
			this.SuspendLayout();
			// 
			// label4
			// 
			label4.Location = new System.Drawing.Point(0, 0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(481, 36);
			label4.TabIndex = 0;
			label4.Text = "Please select the default syntax editor color scheme you wish to use for Codeweav" +
				"er, as well as your prefered look for the IDE in general:";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.lblTitle);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(505, 70);
			this.panel1.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(139, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(324, 69);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Step 1 - Select your Environment";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TSDev.Properties.Resources.startupbanner;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(138, 69);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pnlStep1
			// 
			this.pnlStep1.Controls.Add(this.lstSelKeys);
			this.pnlStep1.Controls.Add(this.label3);
			this.pnlStep1.Controls.Add(this.label2);
			this.pnlStep1.Location = new System.Drawing.Point(12, 82);
			this.pnlStep1.Name = "pnlStep1";
			this.pnlStep1.Size = new System.Drawing.Size(481, 237);
			this.pnlStep1.TabIndex = 1;
			// 
			// lstSelKeys
			// 
			this.lstSelKeys.FormattingEnabled = true;
			this.lstSelKeys.ItemHeight = 14;
			this.lstSelKeys.Items.AddRange(new object[] {
            "TorqueDev Series 1.2",
            "Microsoft Visual Studio (C++/C#)",
            "Microsoft Visual Studio (Visual Basic)",
            "Torsion"});
			this.lstSelKeys.Location = new System.Drawing.Point(6, 99);
			this.lstSelKeys.Name = "lstSelKeys";
			this.lstSelKeys.Size = new System.Drawing.Size(472, 116);
			this.lstSelKeys.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(3, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(475, 36);
			this.label3.TabIndex = 0;
			this.label3.Text = "Please select the sort of development environment you are accustomed to below.  A" +
				" configuration will automatically be generated to match the environment selected" +
				":";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(475, 36);
			this.label2.TabIndex = 0;
			this.label2.Text = "Welcome to TorqueDev!  We\'re absolutely thrilled that you\'ve chosen this developm" +
				"ent environment to do your coding.";
			// 
			// pnlStep2
			// 
			this.pnlStep2.Controls.Add(this.optStyleProfessional);
			this.pnlStep2.Controls.Add(this.optStyleStd);
			this.pnlStep2.Controls.Add(this.label5);
			this.pnlStep2.Controls.Add(this.imgSchemeLook);
			this.pnlStep2.Controls.Add(this.lstScheme);
			this.pnlStep2.Controls.Add(label4);
			this.pnlStep2.Location = new System.Drawing.Point(12, 83);
			this.pnlStep2.Name = "pnlStep2";
			this.pnlStep2.Size = new System.Drawing.Size(481, 236);
			this.pnlStep2.TabIndex = 3;
			this.pnlStep2.Visible = false;
			// 
			// optStyleProfessional
			// 
			this.optStyleProfessional.AutoSize = true;
			this.optStyleProfessional.Location = new System.Drawing.Point(130, 199);
			this.optStyleProfessional.Name = "optStyleProfessional";
			this.optStyleProfessional.Size = new System.Drawing.Size(85, 18);
			this.optStyleProfessional.TabIndex = 4;
			this.optStyleProfessional.Text = "Professional";
			this.optStyleProfessional.UseVisualStyleBackColor = true;
			this.optStyleProfessional.Visible = false;
			// 
			// optStyleStd
			// 
			this.optStyleStd.AutoSize = true;
			this.optStyleStd.Checked = true;
			this.optStyleStd.Location = new System.Drawing.Point(55, 199);
			this.optStyleStd.Name = "optStyleStd";
			this.optStyleStd.Size = new System.Drawing.Size(69, 18);
			this.optStyleStd.TabIndex = 4;
			this.optStyleStd.TabStop = true;
			this.optStyleStd.Text = "Standard";
			this.optStyleStd.UseVisualStyleBackColor = true;
			this.optStyleStd.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 201);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 14);
			this.label5.TabIndex = 3;
			this.label5.Text = "UI Style:";
			this.label5.Visible = false;
			// 
			// imgSchemeLook
			// 
			this.imgSchemeLook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imgSchemeLook.Location = new System.Drawing.Point(307, 39);
			this.imgSchemeLook.Name = "imgSchemeLook";
			this.imgSchemeLook.Size = new System.Drawing.Size(161, 144);
			this.imgSchemeLook.TabIndex = 2;
			this.imgSchemeLook.TabStop = false;
			// 
			// lstScheme
			// 
			this.lstScheme.FormattingEnabled = true;
			this.lstScheme.ItemHeight = 14;
			this.lstScheme.Items.AddRange(new object[] {
            "Default White",
            "Noolness Dark"});
			this.lstScheme.Location = new System.Drawing.Point(6, 39);
			this.lstScheme.Name = "lstScheme";
			this.lstScheme.Size = new System.Drawing.Size(295, 144);
			this.lstScheme.TabIndex = 1;
			this.lstScheme.SelectedIndexChanged += new System.EventHandler(this.lstScheme_SelectedIndexChanged);
			// 
			// cmdNext
			// 
			this.cmdNext.Location = new System.Drawing.Point(319, 326);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(75, 23);
			this.cmdNext.TabIndex = 2;
			this.cmdNext.Text = "&Next ►";
			this.cmdNext.UseVisualStyleBackColor = true;
			this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(418, 326);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdPrevious
			// 
			this.cmdPrevious.Enabled = false;
			this.cmdPrevious.Location = new System.Drawing.Point(238, 325);
			this.cmdPrevious.Name = "cmdPrevious";
			this.cmdPrevious.Size = new System.Drawing.Size(75, 23);
			this.cmdPrevious.TabIndex = 2;
			this.cmdPrevious.Text = "◄ &Previous";
			this.cmdPrevious.UseVisualStyleBackColor = true;
			this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
			// 
			// frmStartupWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 358);
			this.Controls.Add(this.pnlStep2);
			this.Controls.Add(this.pnlStep1);
			this.Controls.Add(this.cmdPrevious);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdNext);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmStartupWizard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Welcome to TorqueDev";
			this.Load += new System.EventHandler(this.frmStartupWizard_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pnlStep1.ResumeLayout(false);
			this.pnlStep2.ResumeLayout(false);
			this.pnlStep2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgSchemeLook)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel pnlStep1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstSelKeys;
		private System.Windows.Forms.Panel pnlStep2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox imgSchemeLook;
		private System.Windows.Forms.ListBox lstScheme;
		private System.Windows.Forms.RadioButton optStyleProfessional;
		private System.Windows.Forms.RadioButton optStyleStd;
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdPrevious;
	}
}