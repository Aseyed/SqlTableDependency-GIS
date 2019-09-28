namespace GIS
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GetData = new System.Windows.Forms.Button();
            this.CityCode = new System.Windows.Forms.TextBox();
            this.Discionnect = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.Database = new System.Windows.Forms.TextBox();
            this.Host = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GetData);
            this.groupBox1.Controls.Add(this.CityCode);
            this.groupBox1.Controls.Add(this.Discionnect);
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Controls.Add(this.Database);
            this.groupBox1.Controls.Add(this.Host);
            this.groupBox1.Location = new System.Drawing.Point(25, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // GetData
            // 
            this.GetData.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GetData.Location = new System.Drawing.Point(218, 45);
            this.GetData.Name = "GetData";
            this.GetData.Size = new System.Drawing.Size(100, 20);
            this.GetData.TabIndex = 7;
            this.GetData.Text = "Get All Data";
            this.GetData.UseVisualStyleBackColor = false;
            this.GetData.Click += new System.EventHandler(this.GetData_Click);
            // 
            // CityCode
            // 
            this.CityCode.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.CityCode.Location = new System.Drawing.Point(218, 19);
            this.CityCode.Name = "CityCode";
            this.CityCode.Size = new System.Drawing.Size(100, 20);
            this.CityCode.TabIndex = 6;
            this.CityCode.Text = "OmoorCode";
            // 
            // Discionnect
            // 
            this.Discionnect.BackColor = System.Drawing.Color.Red;
            this.Discionnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Discionnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Discionnect.Location = new System.Drawing.Point(112, 71);
            this.Discionnect.Margin = new System.Windows.Forms.Padding(0);
            this.Discionnect.Name = "Discionnect";
            this.Discionnect.Size = new System.Drawing.Size(100, 20);
            this.Discionnect.TabIndex = 5;
            this.Discionnect.Text = "Disconnect";
            this.Discionnect.UseVisualStyleBackColor = false;
            this.Discionnect.Click += new System.EventHandler(this.Discionnect_Click);
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.Color.Lime;
            this.Connect.Location = new System.Drawing.Point(6, 71);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(100, 20);
            this.Connect.TabIndex = 4;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = false;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Password
            // 
            this.Password.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Password.Location = new System.Drawing.Point(112, 45);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(100, 20);
            this.Password.TabIndex = 3;
            this.Password.Text = "Password";
            // 
            // Username
            // 
            this.Username.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Username.Location = new System.Drawing.Point(6, 45);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 20);
            this.Username.TabIndex = 2;
            this.Username.Text = "Username";
            // 
            // Database
            // 
            this.Database.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Database.Location = new System.Drawing.Point(112, 19);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(100, 20);
            this.Database.TabIndex = 1;
            this.Database.Text = "DB Name";
            // 
            // Host
            // 
            this.Host.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Host.Location = new System.Drawing.Point(6, 19);
            this.Host.Name = "Host";
            this.Host.Size = new System.Drawing.Size(100, 20);
            this.Host.TabIndex = 0;
            this.Host.Text = "IP Address";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GIS_NotifyIcon";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 158);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GIS";
            this.SizeChanged += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Database;
        private System.Windows.Forms.TextBox Host;
        private System.Windows.Forms.Button Discionnect;
        private System.Windows.Forms.TextBox CityCode;
        private System.Windows.Forms.Button GetData;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

