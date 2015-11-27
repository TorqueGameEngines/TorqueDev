namespace TSDev {
    partial class frmFastTab {
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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Active Windows", System.Windows.Forms.HorizontalAlignment.Left);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblWindowLine2 = new System.Windows.Forms.Label();
			this.lblWindowLine1 = new System.Windows.Forms.Label();
			this.lblWindowType = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lvWindows = new System.Windows.Forms.ListView();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblWindowLine2);
			this.groupBox1.Controls.Add(this.lblWindowLine1);
			this.groupBox1.Controls.Add(this.lblWindowType);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 379);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(633, 107);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// lblWindowLine2
			// 
			this.lblWindowLine2.AutoSize = true;
			this.lblWindowLine2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWindowLine2.Location = new System.Drawing.Point(6, 69);
			this.lblWindowLine2.Name = "lblWindowLine2";
			this.lblWindowLine2.Size = new System.Drawing.Size(17, 14);
			this.lblWindowLine2.TabIndex = 0;
			this.lblWindowLine2.Text = "%";
			// 
			// lblWindowLine1
			// 
			this.lblWindowLine1.AutoSize = true;
			this.lblWindowLine1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWindowLine1.Location = new System.Drawing.Point(6, 45);
			this.lblWindowLine1.Name = "lblWindowLine1";
			this.lblWindowLine1.Size = new System.Drawing.Size(17, 14);
			this.lblWindowLine1.TabIndex = 0;
			this.lblWindowLine1.Text = "%";
			// 
			// lblWindowType
			// 
			this.lblWindowType.AutoSize = true;
			this.lblWindowType.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWindowType.Location = new System.Drawing.Point(6, 16);
			this.lblWindowType.Name = "lblWindowType";
			this.lblWindowType.Size = new System.Drawing.Size(16, 14);
			this.lblWindowType.TabIndex = 0;
			this.lblWindowType.Text = "%";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::TSDev.Properties.Resources.codeweaver_bg;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = global::TSDev.Properties.Resources.codeweaver_logo;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(633, 77);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// lvWindows
			// 
			this.lvWindows.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvWindows.BackColor = System.Drawing.Color.White;
			this.lvWindows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvWindows.FullRowSelect = true;
			listViewGroup1.Header = "Active Windows";
			listViewGroup1.Name = "grpDef";
			this.lvWindows.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
			this.lvWindows.LargeImageList = this.imageList1;
			this.lvWindows.Location = new System.Drawing.Point(0, 77);
			this.lvWindows.MultiSelect = false;
			this.lvWindows.Name = "lvWindows";
			this.lvWindows.OwnerDraw = true;
			this.lvWindows.Size = new System.Drawing.Size(633, 302);
			this.lvWindows.SmallImageList = this.imageList1;
			this.lvWindows.TabIndex = 1;
			this.lvWindows.TabStop = false;
			this.lvWindows.TileSize = new System.Drawing.Size(150, 25);
			this.lvWindows.UseCompatibleStateImageBehavior = false;
			this.lvWindows.View = System.Windows.Forms.View.Tile;
			this.lvWindows.ItemActivate += new System.EventHandler(this.lvWindows_ItemActivate);
			this.lvWindows.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvWindows_DrawItem);
			this.lvWindows.SelectedIndexChanged += new System.EventHandler(this.lvWindows_SelectedIndexChanged);
			this.lvWindows.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvWindows_KeyUp);
			this.lvWindows.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvWindows_KeyDown);
			// 
			// frmFastTab
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(633, 486);
			this.Controls.Add(this.lvWindows);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmFastTab";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmFastTab";
			this.Deactivate += new System.EventHandler(this.frmFastTab_Deactivate);
			this.Load += new System.EventHandler(this.frmFastTab_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFastTab_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvWindows;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblWindowLine2;
		private System.Windows.Forms.Label lblWindowLine1;
		private System.Windows.Forms.Label lblWindowType;
    }
}