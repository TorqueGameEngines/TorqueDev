namespace TSDev {
	partial class frmNewItem {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewItem));
			this.label1 = new System.Windows.Forms.Label();
			this.tvCategories = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.Label();
			this.lvTemplates = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lblInfo = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtSaveTo = new System.Windows.Forms.TextBox();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Categories";
			// 
			// tvCategories
			// 
			this.tvCategories.HideSelection = false;
			this.tvCategories.Location = new System.Drawing.Point(12, 25);
			this.tvCategories.Name = "tvCategories";
			this.tvCategories.Size = new System.Drawing.Size(232, 200);
			this.tvCategories.TabIndex = 1;
			this.tvCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCategories_AfterSelect);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(247, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "&Templates:";
			// 
			// lvTemplates
			// 
			this.lvTemplates.HideSelection = false;
			this.lvTemplates.Location = new System.Drawing.Point(250, 25);
			this.lvTemplates.Name = "lvTemplates";
			this.lvTemplates.Size = new System.Drawing.Size(483, 200);
			this.lvTemplates.SmallImageList = this.imageList1;
			this.lvTemplates.TabIndex = 2;
			this.lvTemplates.UseCompatibleStateImageBehavior = false;
			this.lvTemplates.View = System.Windows.Forms.View.SmallIcon;
			this.lvTemplates.SelectedIndexChanged += new System.EventHandler(this.lvTemplates_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "16_code_torque.png");
			this.imageList1.Images.SetKeyName(1, "windows.png");
			this.imageList1.Images.SetKeyName(2, "document.png");
			// 
			// lblInfo
			// 
			this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblInfo.Location = new System.Drawing.Point(12, 228);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(721, 31);
			this.lblInfo.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 273);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "&Save To:";
			// 
			// txtSaveTo
			// 
			this.txtSaveTo.Location = new System.Drawing.Point(88, 270);
			this.txtSaveTo.Name = "txtSaveTo";
			this.txtSaveTo.ReadOnly = true;
			this.txtSaveTo.Size = new System.Drawing.Size(564, 21);
			this.txtSaveTo.TabIndex = 5;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Location = new System.Drawing.Point(658, 268);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
			this.cmdBrowse.TabIndex = 6;
			this.cmdBrowse.Text = "&Browse";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(658, 312);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(577, 312);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// frmNewItem
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(745, 346);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtSaveTo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.lvTemplates);
			this.Controls.Add(this.tvCategories);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewItem";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New File";
			this.Load += new System.EventHandler(this.frmNewItem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView tvCategories;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView lvTemplates;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSaveTo;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
	}
}