namespace Cloudsdale.lib.MessageUI
{
    partial class CloudMessage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_role = new System.Windows.Forms.Label();
            this.c_name = new System.Windows.Forms.Label();
            this.c_user = new System.Windows.Forms.Label();
            this.c_time = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.c_status = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.c_content = new System.Windows.Forms.RichTextBox();
            this.c_platform = new System.Windows.Forms.PictureBox();
            this.c_avatar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.c_platform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // c_role
            // 
            this.c_role.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_role.Location = new System.Drawing.Point(3, 72);
            this.c_role.Name = "c_role";
            this.c_role.Size = new System.Drawing.Size(64, 18);
            this.c_role.TabIndex = 1;
            this.c_role.Text = "UserTag";
            this.c_role.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // c_name
            // 
            this.c_name.AutoSize = true;
            this.c_name.Location = new System.Drawing.Point(87, 4);
            this.c_name.Name = "c_name";
            this.c_name.Size = new System.Drawing.Size(76, 16);
            this.c_name.TabIndex = 2;
            this.c_name.Text = "Screen Name";
            // 
            // c_user
            // 
            this.c_user.AutoSize = true;
            this.c_user.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.c_user.Location = new System.Drawing.Point(74, 20);
            this.c_user.Name = "c_user";
            this.c_user.Size = new System.Drawing.Size(69, 16);
            this.c_user.TabIndex = 3;
            this.c_user.Text = "@Username";
            // 
            // c_time
            // 
            this.c_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_time.Location = new System.Drawing.Point(169, 4);
            this.c_time.Name = "c_time";
            this.c_time.Size = new System.Drawing.Size(189, 20);
            this.c_time.TabIndex = 4;
            this.c_time.Text = "12:00:00 pm";
            this.c_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(2, 2);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.c_status});
            this.shapeContainer1.Size = new System.Drawing.Size(359, 97);
            this.shapeContainer1.TabIndex = 6;
            this.shapeContainer1.TabStop = false;
            // 
            // c_status
            // 
            this.c_status.BackColor = System.Drawing.Color.Black;
            this.c_status.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.c_status.FillColor = System.Drawing.Color.White;
            this.c_status.FillGradientColor = System.Drawing.Color.Silver;
            this.c_status.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.c_status.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.c_status.Location = new System.Drawing.Point(75, 6);
            this.c_status.Name = "c_status";
            this.c_status.Size = new System.Drawing.Size(9, 9);
            // 
            // c_content
            // 
            this.c_content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_content.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.c_content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c_content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_content.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.c_content.Location = new System.Drawing.Point(77, 39);
            this.c_content.Name = "c_content";
            this.c_content.ReadOnly = true;
            this.c_content.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.c_content.Size = new System.Drawing.Size(265, 43);
            this.c_content.TabIndex = 7;
            this.c_content.Text = "content";
            // 
            // c_platform
            // 
            this.c_platform.Image = global::Cloudsdale.Properties.Resources.phone;
            this.c_platform.Location = new System.Drawing.Point(348, 39);
            this.c_platform.Name = "c_platform";
            this.c_platform.Size = new System.Drawing.Size(10, 18);
            this.c_platform.TabIndex = 5;
            this.c_platform.TabStop = false;
            // 
            // c_avatar
            // 
            this.c_avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_avatar.Image = global::Cloudsdale.Properties.Resources.user;
            this.c_avatar.Location = new System.Drawing.Point(3, 4);
            this.c_avatar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c_avatar.Name = "c_avatar";
            this.c_avatar.Size = new System.Drawing.Size(64, 64);
            this.c_avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.c_avatar.TabIndex = 0;
            this.c_avatar.TabStop = false;
            // 
            // CloudMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.c_content);
            this.Controls.Add(this.c_platform);
            this.Controls.Add(this.c_time);
            this.Controls.Add(this.c_user);
            this.Controls.Add(this.c_name);
            this.Controls.Add(this.c_role);
            this.Controls.Add(this.c_avatar);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("Pfennig", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CloudMessage";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(363, 101);
            ((System.ComponentModel.ISupportInitialize)(this.c_platform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox c_avatar;
        public System.Windows.Forms.Label c_role;
        public System.Windows.Forms.Label c_name;
        public System.Windows.Forms.Label c_user;
        public System.Windows.Forms.Label c_time;
        public System.Windows.Forms.PictureBox c_platform;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        public Microsoft.VisualBasic.PowerPacks.OvalShape c_status;
        public System.Windows.Forms.RichTextBox c_content;

    }
}
