namespace Cloudsdale.lib.MessageUI.Item
{
    partial class Message
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Message));
            this.MessageText = new System.Windows.Forms.TextBox();
            this.UserAvatar = new System.Windows.Forms.PictureBox();
            this.CurrentTime = new System.Windows.Forms.Label();
            this.UserConsole = new System.Windows.Forms.Label();
            this.UserTag = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UserAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // MessageText
            // 
            this.MessageText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MessageText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.MessageText.Location = new System.Drawing.Point(99, 33);
            this.MessageText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MessageText.Multiline = true;
            this.MessageText.Name = "MessageText";
            this.MessageText.Size = new System.Drawing.Size(408, 63);
            this.MessageText.TabIndex = 2;
            this.MessageText.Text = "message";
            // 
            // UserAvatar
            // 
            this.UserAvatar.ErrorImage = ((System.Drawing.Image)(resources.GetObject("UserAvatar.ErrorImage")));
            this.UserAvatar.Image = ((System.Drawing.Image)(resources.GetObject("UserAvatar.Image")));
            this.UserAvatar.InitialImage = ((System.Drawing.Image)(resources.GetObject("UserAvatar.InitialImage")));
            this.UserAvatar.Location = new System.Drawing.Point(3, 4);
            this.UserAvatar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserAvatar.Name = "UserAvatar";
            this.UserAvatar.Size = new System.Drawing.Size(64, 64);
            this.UserAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UserAvatar.TabIndex = 0;
            this.UserAvatar.TabStop = false;
            // 
            // CurrentTime
            // 
            this.CurrentTime.AutoSize = true;
            this.CurrentTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CurrentTime.Location = new System.Drawing.Point(255, 4);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(33, 17);
            this.CurrentTime.TabIndex = 3;
            this.CurrentTime.Text = "time";
            // 
            // UserConsole
            // 
            this.UserConsole.AutoSize = true;
            this.UserConsole.Location = new System.Drawing.Point(531, 46);
            this.UserConsole.Name = "UserConsole";
            this.UserConsole.Size = new System.Drawing.Size(16, 17);
            this.UserConsole.TabIndex = 4;
            this.UserConsole.Text = "C";
            // 
            // Tag
            // 
            this.UserTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.UserTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserTag.Location = new System.Drawing.Point(3, 75);
            this.UserTag.Name = "Tag";
            this.UserTag.Size = new System.Drawing.Size(64, 25);
            this.UserTag.TabIndex = 5;
            this.UserTag.Text = "tag";
            this.UserTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.UserTag);
            this.Controls.Add(this.UserConsole);
            this.Controls.Add(this.CurrentTime);
            this.Controls.Add(this.MessageText);
            this.Controls.Add(this.UserAvatar);
            this.Font = new System.Drawing.Font("Pfennig", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(565, 111);
            ((System.ComponentModel.ISupportInitialize)(this.UserAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox UserAvatar;
        private System.Windows.Forms.TextBox MessageText;
        private System.Windows.Forms.Label CurrentTime;
        private System.Windows.Forms.Label UserConsole;
        public System.Windows.Forms.TextBox UserTag;

    }
}
