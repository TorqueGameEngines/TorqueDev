namespace TSDev {
	partial class frmEula {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEula));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtTyper = new System.Windows.Forms.TextBox();
			this.tmrTyper = new System.Windows.Forms.Timer(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtEula = new System.Windows.Forms.RichTextBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.optAccept = new System.Windows.Forms.RadioButton();
			this.optDecline = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtTyper);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(415, 210);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Important Changes";
			// 
			// txtTyper
			// 
			this.txtTyper.BackColor = System.Drawing.SystemColors.Control;
			this.txtTyper.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTyper.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.txtTyper.Location = new System.Drawing.Point(6, 19);
			this.txtTyper.Multiline = true;
			this.txtTyper.Name = "txtTyper";
			this.txtTyper.ReadOnly = true;
			this.txtTyper.Size = new System.Drawing.Size(403, 185);
			this.txtTyper.TabIndex = 0;
			this.txtTyper.Enter += new System.EventHandler(this.txtTyper_Enter);
			// 
			// tmrTyper
			// 
			this.tmrTyper.Enabled = true;
			this.tmrTyper.Interval = 15;
			this.tmrTyper.Tick += new System.EventHandler(this.tmrTyper_Tick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtEula);
			this.groupBox2.Location = new System.Drawing.Point(12, 239);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(415, 245);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "End User License Agreement";
			// 
			// txtEula
			// 
			this.txtEula.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.txtEula.Location = new System.Drawing.Point(6, 19);
			this.txtEula.Name = "txtEula";
			this.txtEula.ReadOnly = true;
			this.txtEula.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtEula.Size = new System.Drawing.Size(403, 220);
			this.txtEula.TabIndex = 0;
			this.txtEula.Text = "";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(352, 497);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new System.Drawing.Point(271, 497);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// optAccept
			// 
			this.optAccept.AutoSize = true;
			this.optAccept.Enabled = false;
			this.optAccept.Location = new System.Drawing.Point(18, 499);
			this.optAccept.Name = "optAccept";
			this.optAccept.Size = new System.Drawing.Size(65, 18);
			this.optAccept.TabIndex = 3;
			this.optAccept.TabStop = true;
			this.optAccept.Text = "I Accept";
			this.optAccept.UseVisualStyleBackColor = true;
			this.optAccept.CheckedChanged += new System.EventHandler(this.optAccept_CheckedChanged);
			// 
			// optDecline
			// 
			this.optDecline.AutoSize = true;
			this.optDecline.Enabled = false;
			this.optDecline.Location = new System.Drawing.Point(104, 499);
			this.optDecline.Name = "optDecline";
			this.optDecline.Size = new System.Drawing.Size(65, 18);
			this.optDecline.TabIndex = 3;
			this.optDecline.TabStop = true;
			this.optDecline.Text = "I Decline";
			this.optDecline.UseVisualStyleBackColor = true;
			this.optDecline.CheckedChanged += new System.EventHandler(this.optDecline_CheckedChanged);
			// 
			// frmEula
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 530);
			this.Controls.Add(this.optDecline);
			this.Controls.Add(this.optAccept);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEula";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TorqueDev - End User License Agreement";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEula_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtTyper;
		private System.Windows.Forms.Timer tmrTyper;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RichTextBox txtEula;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.RadioButton optAccept;
		private System.Windows.Forms.RadioButton optDecline;
	}
}