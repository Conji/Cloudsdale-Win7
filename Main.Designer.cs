namespace Cloudsdale
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.Register = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ViewTimer = new System.Windows.Forms.Timer(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.CloudIco = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.CloudList = new System.Windows.Forms.ListView();
            this.MessageUI = new System.Windows.Forms.Panel();
            this.MessageGroup = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SendMessage = new System.Windows.Forms.Button();
            this.Subscriber = new System.Windows.Forms.NotifyIcon(this.components);
            this.Password = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.MessagesSource = new System.Windows.Forms.BindingSource(this.components);
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.messageBox1 = new Cloudsdale.lib.MessageUI.MessageBox();
            this.LoginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.MessageUI.SuspendLayout();
            this.MessageGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesSource)).BeginInit();
            this.MessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(160)))), ((int)(((byte)(208)))));
            this.LoginPanel.Controls.Add(this.Register);
            this.LoginPanel.Controls.Add(this.Login);
            this.LoginPanel.Controls.Add(this.label2);
            this.LoginPanel.Controls.Add(this.label1);
            this.LoginPanel.Controls.Add(this.Password);
            this.LoginPanel.Controls.Add(this.Email);
            this.LoginPanel.Controls.Add(this.pictureBox1);
            this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginPanel.Location = new System.Drawing.Point(0, 0);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(692, 438);
            this.LoginPanel.TabIndex = 0;
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(331, 300);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(168, 36);
            this.Register.TabIndex = 6;
            this.Register.Text = "Register";
            this.Register.UseVisualStyleBackColor = true;
            this.Register.Click += new System.EventHandler(this.LaunchReg);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(153, 300);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(168, 36);
            this.Login.TabIndex = 5;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username or Email:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Cloudsdale.Properties.Resources.cloudsdale_thin_bright_logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(686, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // CloudIco
            // 
            this.CloudIco.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.CloudIco.ImageSize = new System.Drawing.Size(16, 16);
            this.CloudIco.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 439);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.CloudList);
            this.panel2.Location = new System.Drawing.Point(2, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 438);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.button1.Location = new System.Drawing.Point(-1, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Home";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // CloudList
            // 
            this.CloudList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CloudList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.CloudList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CloudList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.CloudList.FullRowSelect = true;
            this.CloudList.GridLines = true;
            this.CloudList.Location = new System.Drawing.Point(-1, 45);
            this.CloudList.MultiSelect = false;
            this.CloudList.Name = "CloudList";
            this.CloudList.ShowItemToolTips = true;
            this.CloudList.Size = new System.Drawing.Size(203, 393);
            this.CloudList.SmallImageList = this.CloudIco;
            this.CloudList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CloudList.TabIndex = 0;
            this.CloudList.UseCompatibleStateImageBehavior = false;
            this.CloudList.View = System.Windows.Forms.View.Tile;
            this.CloudList.SelectedIndexChanged += new System.EventHandler(this.CloudList_SelectedIndexChanged);
            this.CloudList.Click += new System.EventHandler(this.CloudList_SelectedIndexChanged);
            // 
            // MessageUI
            // 
            this.MessageUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageUI.Controls.Add(this.MessageGroup);
            this.MessageUI.Controls.Add(this.textBox1);
            this.MessageUI.Controls.Add(this.SendMessage);
            this.MessageUI.Location = new System.Drawing.Point(198, 0);
            this.MessageUI.Name = "MessageUI";
            this.MessageUI.Size = new System.Drawing.Size(494, 439);
            this.MessageUI.TabIndex = 2;
            // 
            // MessageGroup
            // 
            this.MessageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageGroup.Controls.Add(this.MessagePanel);
            this.MessageGroup.Location = new System.Drawing.Point(3, 0);
            this.MessageGroup.Name = "MessageGroup";
            this.MessageGroup.Size = new System.Drawing.Size(479, 400);
            this.MessageGroup.TabIndex = 2;
            this.MessageGroup.TabStop = false;
            this.MessageGroup.Text = "Welcome back!";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 406);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(394, 27);
            this.textBox1.TabIndex = 1;
            // 
            // SendMessage
            // 
            this.SendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendMessage.Location = new System.Drawing.Point(406, 406);
            this.SendMessage.Name = "SendMessage";
            this.SendMessage.Size = new System.Drawing.Size(75, 27);
            this.SendMessage.TabIndex = 0;
            this.SendMessage.Text = "Send\r\n";
            this.SendMessage.UseVisualStyleBackColor = true;
            // 
            // Subscriber
            // 
            this.Subscriber.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Subscriber.BalloonTipText = "[:user] posted on [:cloud]!\r\n";
            this.Subscriber.BalloonTipTitle = "Cloudsdale Subscriber";
            this.Subscriber.Text = "[USER] posted in [CLOUD]!";
            this.Subscriber.Visible = true;
            // 
            // Password
            // 
            this.Password.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Cloudsdale.Properties.Settings.Default, "PreviousPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Password.Location = new System.Drawing.Point(153, 267);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(346, 27);
            this.Password.TabIndex = 2;
            this.Password.Text = global::Cloudsdale.Properties.Settings.Default.PreviousPassword;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Email
            // 
            this.Email.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Cloudsdale.Properties.Settings.Default, "PreviousEmail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Email.Location = new System.Drawing.Point(153, 209);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(346, 27);
            this.Email.TabIndex = 1;
            this.Email.Text = global::Cloudsdale.Properties.Settings.Default.PreviousEmail;
            // 
            // MessagesSource
            // 
            this.MessagesSource.DataSource = typeof(Cloudsdale.connection.MessageSource);
            // 
            // MessagePanel
            // 
            this.MessagePanel.Controls.Add(this.messageBox1);
            this.MessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagePanel.Location = new System.Drawing.Point(3, 23);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(473, 374);
            this.MessagePanel.TabIndex = 0;
            // 
            // messageBox1
            // 
            this.messageBox1.DataMember = null;
            this.messageBox1.DataSource = null;
            this.messageBox1.FontLine1 = new System.Drawing.Font("Tahoma", 8F);
            this.messageBox1.FontLine2 = new System.Drawing.Font("Tahoma", 8F);
            this.messageBox1.Input = ((System.Collections.ArrayList)(resources.GetObject("messageBox1.Input")));
            this.messageBox1.ItemImage = null;
            this.messageBox1.ItemImageDisplayMember = "Picture";
            this.messageBox1.Line1DisplayMember = "Name";
            this.messageBox1.Line2DisplayMember = "Time";
            this.messageBox1.Location = new System.Drawing.Point(142, 23);
            this.messageBox1.Name = "messageBox1";
            this.messageBox1.ScrollBarWidth = 14;
            this.messageBox1.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.messageBox1.SelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.messageBox1.SelectedIndex = -1;
            this.messageBox1.SeparatorColor = System.Drawing.SystemColors.ActiveBorder;
            this.messageBox1.Size = new System.Drawing.Size(224, 200);
            this.messageBox1.TabIndex = 0;
            this.messageBox1.Text = "messageBox1";
            this.messageBox1.TransparentColor = System.Drawing.Color.Magenta;
            this.messageBox1.UseTransparentColor = false;
            // 
            // Main
            // 
            this.AcceptButton = this.Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 438);
            this.Controls.Add(this.MessageUI);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoginPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.Text = "Cloudsdale";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.MessageUI.ResumeLayout(false);
            this.MessageUI.PerformLayout();
            this.MessageGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesSource)).EndInit();
            this.MessagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer ViewTimer;
        internal System.Windows.Forms.TextBox Email;
        public System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ImageList CloudIco;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListView CloudList;
        private System.Windows.Forms.Panel MessageUI;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SendMessage;
        public System.Windows.Forms.NotifyIcon Subscriber;
        public System.Windows.Forms.BindingSource MessagesSource;
        private System.Windows.Forms.GroupBox MessageGroup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel MessagePanel;
        private lib.MessageUI.MessageBox messageBox1;
        
    }
}

