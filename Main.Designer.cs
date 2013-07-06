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
            this.Password = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ViewTimer = new System.Windows.Forms.Timer(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.CloudIco = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.CloudList = new System.Windows.Forms.ListView();
            this.CloudContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.postMessageToCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.cloudDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloudRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ownerControlPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MessageUI = new System.Windows.Forms.Panel();
            this.MessageGroup = new System.Windows.Forms.GroupBox();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.h_about = new System.Windows.Forms.TextBox();
            this.cmdDonate = new System.Windows.Forms.Button();
            this.cmdInfo = new System.Windows.Forms.Button();
            this.cmdExplore = new System.Windows.Forms.Button();
            this.h_pony = new System.Windows.Forms.PictureBox();
            this.h_message = new System.Windows.Forms.TextBox();
            this.h_memberSince = new System.Windows.Forms.Label();
            this.h_avatar = new System.Windows.Forms.PictureBox();
            this.m_NewMessage = new System.Windows.Forms.TextBox();
            this.m_SendMessage = new System.Windows.Forms.Button();
            this.Subscriber = new System.Windows.Forms.NotifyIcon(this.components);
            this.settingsIcon = new System.Windows.Forms.PictureBox();
            this.MessagesSource = new System.Windows.Forms.BindingSource(this.components);
            this.LoginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.CloudContext.SuspendLayout();
            this.MessageUI.SuspendLayout();
            this.MessageGroup.SuspendLayout();
            this.MessagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.h_pony)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h_avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesSource)).BeginInit();
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
            this.LoginPanel.Size = new System.Drawing.Size(687, 438);
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
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(153, 267);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(346, 27);
            this.Password.TabIndex = 2;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(153, 209);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(346, 27);
            this.Email.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Cloudsdale.Properties.Resources.cloudsdale_thin_bright_logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(681, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ViewTimer
            // 
            this.ViewTimer.Tick += new System.EventHandler(this.ShowSettings);
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
            this.CloudList.ContextMenuStrip = this.CloudContext;
            this.CloudList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.CloudList.FullRowSelect = true;
            this.CloudList.GridLines = true;
            this.CloudList.Location = new System.Drawing.Point(-1, 45);
            this.CloudList.MultiSelect = false;
            this.CloudList.Name = "CloudList";
            this.CloudList.ShowItemToolTips = true;
            this.CloudList.Size = new System.Drawing.Size(199, 393);
            this.CloudList.SmallImageList = this.CloudIco;
            this.CloudList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CloudList.TabIndex = 0;
            this.CloudList.UseCompatibleStateImageBehavior = false;
            this.CloudList.View = System.Windows.Forms.View.Tile;
            this.CloudList.SelectedIndexChanged += new System.EventHandler(this.CloudList_SelectedIndexChanged);
            this.CloudList.Click += new System.EventHandler(this.CloudList_SelectedIndexChanged);
            // 
            // CloudContext
            // 
            this.CloudContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToCloudToolStripMenuItem,
            this.leaveCloudToolStripMenuItem,
            this.copyLinkToolStripMenuItem,
            this.toolStripSeparator1,
            this.postMessageToCloudToolStripMenuItem,
            this.cloudDescriptionToolStripMenuItem,
            this.cloudRulesToolStripMenuItem,
            this.usersOnlineToolStripMenuItem,
            this.toolStripSeparator2,
            this.ownerControlPanelToolStripMenuItem,
            this.toolStripMenuItem1});
            this.CloudContext.Name = "CloudContext";
            this.CloudContext.Size = new System.Drawing.Size(203, 214);
            this.CloudContext.Opening += new System.ComponentModel.CancelEventHandler(this.CloudContext_Opening);
            // 
            // goToCloudToolStripMenuItem
            // 
            this.goToCloudToolStripMenuItem.Name = "goToCloudToolStripMenuItem";
            this.goToCloudToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.goToCloudToolStripMenuItem.Text = "Go to cloud";
            // 
            // leaveCloudToolStripMenuItem
            // 
            this.leaveCloudToolStripMenuItem.Name = "leaveCloudToolStripMenuItem";
            this.leaveCloudToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.leaveCloudToolStripMenuItem.Text = "Leave cloud";
            // 
            // copyLinkToolStripMenuItem
            // 
            this.copyLinkToolStripMenuItem.Name = "copyLinkToolStripMenuItem";
            this.copyLinkToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.copyLinkToolStripMenuItem.Text = "Copy link";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // postMessageToCloudToolStripMenuItem
            // 
            this.postMessageToCloudToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.postMessageToCloudToolStripMenuItem.Name = "postMessageToCloudToolStripMenuItem";
            this.postMessageToCloudToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.postMessageToCloudToolStripMenuItem.Text = "Post message to cloud...";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // cloudDescriptionToolStripMenuItem
            // 
            this.cloudDescriptionToolStripMenuItem.Name = "cloudDescriptionToolStripMenuItem";
            this.cloudDescriptionToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.cloudDescriptionToolStripMenuItem.Text = "Cloud description";
            // 
            // cloudRulesToolStripMenuItem
            // 
            this.cloudRulesToolStripMenuItem.Name = "cloudRulesToolStripMenuItem";
            this.cloudRulesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.cloudRulesToolStripMenuItem.Text = "Cloud rules";
            // 
            // usersOnlineToolStripMenuItem
            // 
            this.usersOnlineToolStripMenuItem.Name = "usersOnlineToolStripMenuItem";
            this.usersOnlineToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.usersOnlineToolStripMenuItem.Text = "Users online";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // ownerControlPanelToolStripMenuItem
            // 
            this.ownerControlPanelToolStripMenuItem.Name = "ownerControlPanelToolStripMenuItem";
            this.ownerControlPanelToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ownerControlPanelToolStripMenuItem.Text = "Owner control panel";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            // 
            // MessageUI
            // 
            this.MessageUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MessageUI.Controls.Add(this.MessageGroup);
            this.MessageUI.Controls.Add(this.m_NewMessage);
            this.MessageUI.Controls.Add(this.m_SendMessage);
            this.MessageUI.Location = new System.Drawing.Point(198, 0);
            this.MessageUI.Name = "MessageUI";
            this.MessageUI.Size = new System.Drawing.Size(489, 439);
            this.MessageUI.TabIndex = 2;
            // 
            // MessageGroup
            // 
            this.MessageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MessageGroup.Controls.Add(this.SettingsPanel);
            this.MessageGroup.Controls.Add(this.MessagePanel);
            this.MessageGroup.Location = new System.Drawing.Point(3, 0);
            this.MessageGroup.Name = "MessageGroup";
            this.MessageGroup.Size = new System.Drawing.Size(476, 400);
            this.MessageGroup.TabIndex = 2;
            this.MessageGroup.TabStop = false;
            this.MessageGroup.Text = "Welcome back!";
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.SettingsPanel.Location = new System.Drawing.Point(316, 1);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Padding = new System.Windows.Forms.Padding(2);
            this.SettingsPanel.Size = new System.Drawing.Size(170, 0);
            this.SettingsPanel.TabIndex = 8;
            this.SettingsPanel.MouseHover += new System.EventHandler(this.SettingsHover);
            // 
            // MessagePanel
            // 
            this.MessagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagePanel.Controls.Add(this.h_about);
            this.MessagePanel.Controls.Add(this.cmdDonate);
            this.MessagePanel.Controls.Add(this.cmdInfo);
            this.MessagePanel.Controls.Add(this.cmdExplore);
            this.MessagePanel.Controls.Add(this.h_pony);
            this.MessagePanel.Controls.Add(this.h_message);
            this.MessagePanel.Controls.Add(this.h_memberSince);
            this.MessagePanel.Controls.Add(this.h_avatar);
            this.MessagePanel.Location = new System.Drawing.Point(6, 23);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(461, 371);
            this.MessagePanel.TabIndex = 0;
            // 
            // h_about
            // 
            this.h_about.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.h_about.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.h_about.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.h_about.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.h_about.Location = new System.Drawing.Point(458, 3);
            this.h_about.Multiline = true;
            this.h_about.Name = "h_about";
            this.h_about.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.h_about.Size = new System.Drawing.Size(0, 365);
            this.h_about.TabIndex = 7;
            this.h_about.Text = resources.GetString("h_about.Text");
            // 
            // cmdDonate
            // 
            this.cmdDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDonate.Image = global::Cloudsdale.Properties.Resources.credit_card;
            this.cmdDonate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDonate.Location = new System.Drawing.Point(257, 328);
            this.cmdDonate.Name = "cmdDonate";
            this.cmdDonate.Size = new System.Drawing.Size(195, 40);
            this.cmdDonate.TabIndex = 6;
            this.cmdDonate.Text = "           Donate!";
            this.cmdDonate.UseVisualStyleBackColor = true;
            // 
            // cmdInfo
            // 
            this.cmdInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInfo.Image = global::Cloudsdale.Properties.Resources.book;
            this.cmdInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInfo.Location = new System.Drawing.Point(257, 254);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Size = new System.Drawing.Size(195, 40);
            this.cmdInfo.TabIndex = 5;
            this.cmdInfo.Text = "      Info!";
            this.cmdInfo.UseVisualStyleBackColor = true;
            // 
            // cmdExplore
            // 
            this.cmdExplore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExplore.Image = global::Cloudsdale.Properties.Resources.cloud1;
            this.cmdExplore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExplore.Location = new System.Drawing.Point(257, 186);
            this.cmdExplore.Name = "cmdExplore";
            this.cmdExplore.Size = new System.Drawing.Size(195, 40);
            this.cmdExplore.TabIndex = 4;
            this.cmdExplore.Text = "           Explore!";
            this.cmdExplore.UseVisualStyleBackColor = true;
            // 
            // h_pony
            // 
            this.h_pony.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.h_pony.Image = ((System.Drawing.Image)(resources.GetObject("h_pony.Image")));
            this.h_pony.Location = new System.Drawing.Point(10, 184);
            this.h_pony.Name = "h_pony";
            this.h_pony.Size = new System.Drawing.Size(197, 184);
            this.h_pony.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.h_pony.TabIndex = 3;
            this.h_pony.TabStop = false;
            // 
            // h_message
            // 
            this.h_message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.h_message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.h_message.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h_message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.h_message.Location = new System.Drawing.Point(144, 3);
            this.h_message.Multiline = true;
            this.h_message.Name = "h_message";
            this.h_message.ReadOnly = true;
            this.h_message.Size = new System.Drawing.Size(311, 165);
            this.h_message.TabIndex = 1;
            this.h_message.Text = resources.GetString("h_message.Text");
            // 
            // h_memberSince
            // 
            this.h_memberSince.AutoSize = true;
            this.h_memberSince.Font = new System.Drawing.Font("Corbel", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.h_memberSince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.h_memberSince.Location = new System.Drawing.Point(7, 163);
            this.h_memberSince.Name = "h_memberSince";
            this.h_memberSince.Size = new System.Drawing.Size(76, 18);
            this.h_memberSince.TabIndex = 2;
            this.h_memberSince.Text = "date joined";
            // 
            // h_avatar
            // 
            this.h_avatar.ErrorImage = ((System.Drawing.Image)(resources.GetObject("h_avatar.ErrorImage")));
            this.h_avatar.ImageLocation = "";
            this.h_avatar.InitialImage = null;
            this.h_avatar.Location = new System.Drawing.Point(10, 3);
            this.h_avatar.Name = "h_avatar";
            this.h_avatar.Size = new System.Drawing.Size(128, 128);
            this.h_avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.h_avatar.TabIndex = 0;
            this.h_avatar.TabStop = false;
            // 
            // m_NewMessage
            // 
            this.m_NewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_NewMessage.Location = new System.Drawing.Point(10, 406);
            this.m_NewMessage.Name = "m_NewMessage";
            this.m_NewMessage.Size = new System.Drawing.Size(382, 27);
            this.m_NewMessage.TabIndex = 1;
            // 
            // m_SendMessage
            // 
            this.m_SendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_SendMessage.Location = new System.Drawing.Point(401, 406);
            this.m_SendMessage.Name = "m_SendMessage";
            this.m_SendMessage.Size = new System.Drawing.Size(75, 27);
            this.m_SendMessage.TabIndex = 0;
            this.m_SendMessage.Text = "Send\r\n";
            this.m_SendMessage.UseVisualStyleBackColor = true;
            // 
            // Subscriber
            // 
            this.Subscriber.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Subscriber.BalloonTipText = "[:user] posted on [:cloud]!\r\n";
            this.Subscriber.BalloonTipTitle = "Cloudsdale Subscriber";
            this.Subscriber.Text = "[USER] posted in [CLOUD]!";
            this.Subscriber.Visible = true;
            // 
            // settingsIcon
            // 
            this.settingsIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.settingsIcon.Image = global::Cloudsdale.Properties.Resources.icon_auth_gray;
            this.settingsIcon.Location = new System.Drawing.Point(659, 0);
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(28, 31);
            this.settingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.settingsIcon.TabIndex = 1;
            this.settingsIcon.TabStop = false;
            this.settingsIcon.MouseLeave += new System.EventHandler(this.SettingsLeft);
            this.settingsIcon.MouseHover += new System.EventHandler(this.ActivateMenuHover);
            // 
            // MessagesSource
            // 
            this.MessagesSource.DataSource = typeof(Cloudsdale.connection.MessageSource);
            // 
            // Main
            // 
            this.AcceptButton = this.Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 438);
            this.Controls.Add(this.settingsIcon);
            this.Controls.Add(this.MessageUI);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoginPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.Text = "Cloudsdale";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.ResizeCheck);
            this.Resize += new System.EventHandler(this.ResizeCheck);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.CloudContext.ResumeLayout(false);
            this.MessageUI.ResumeLayout(false);
            this.MessageUI.PerformLayout();
            this.MessageGroup.ResumeLayout(false);
            this.MessagePanel.ResumeLayout(false);
            this.MessagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.h_pony)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h_avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //for controls rested in the MessageUI, use "m_ExampleControl". 
        //for controls rested in the HomeTab, use "h_ExampleControl".
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
        private System.Windows.Forms.TextBox m_NewMessage;
        private System.Windows.Forms.Button m_SendMessage;
        public System.Windows.Forms.NotifyIcon Subscriber;
        public System.Windows.Forms.BindingSource MessagesSource;
        private System.Windows.Forms.GroupBox MessageGroup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.Label h_memberSince;
        private System.Windows.Forms.TextBox h_message;
        private System.Windows.Forms.PictureBox h_avatar;
        private System.Windows.Forms.PictureBox settingsIcon;
        private System.Windows.Forms.PictureBox h_pony;
        private System.Windows.Forms.ContextMenuStrip CloudContext;
        private System.Windows.Forms.ToolStripMenuItem goToCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaveCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem postMessageToCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem cloudDescriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloudRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ownerControlPanelToolStripMenuItem;
        private System.Windows.Forms.Button cmdDonate;
        private System.Windows.Forms.Button cmdInfo;
        private System.Windows.Forms.Button cmdExplore;
        private System.Windows.Forms.TextBox h_about;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel SettingsPanel;
        
    }
}

