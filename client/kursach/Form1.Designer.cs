namespace kursach
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.initToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox_Output = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initToolStripMenuItem,
            this.connectTokenToolStripMenuItem,
            this.deletConnectToolStripMenuItem,
            this.ServSetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(911, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // initToolStripMenuItem
            // 
            this.initToolStripMenuItem.Name = "initToolStripMenuItem";
            this.initToolStripMenuItem.Size = new System.Drawing.Size(154, 29);
            this.initToolStripMenuItem.Text = "инициализация";
            this.initToolStripMenuItem.Click += new System.EventHandler(this.initToolStripMenuItem_Click);
            // 
            // connectTokenToolStripMenuItem
            // 
            this.connectTokenToolStripMenuItem.Name = "connectTokenToolStripMenuItem";
            this.connectTokenToolStripMenuItem.Size = new System.Drawing.Size(190, 29);
            this.connectTokenToolStripMenuItem.Text = "поключения токена";
            this.connectTokenToolStripMenuItem.Click += new System.EventHandler(this.connectTokenToolStripMenuItem_Click);
            // 
            // deletConnectToolStripMenuItem
            // 
            this.deletConnectToolStripMenuItem.Name = "deletConnectToolStripMenuItem";
            this.deletConnectToolStripMenuItem.Size = new System.Drawing.Size(230, 29);
            this.deletConnectToolStripMenuItem.Text = "разорвать подключение";
            this.deletConnectToolStripMenuItem.Click += new System.EventHandler(this.deletConnectToolStripMenuItem_Click);
            // 
            // ServSetToolStripMenuItem
            // 
            this.ServSetToolStripMenuItem.Name = "ServSetToolStripMenuItem";
            this.ServSetToolStripMenuItem.Size = new System.Drawing.Size(185, 29);
            this.ServSetToolStripMenuItem.Text = "настройки сервера";
            this.ServSetToolStripMenuItem.Click += new System.EventHandler(this.ServSetToolStripMenuItem_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 738);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(741, 26);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(762, 727);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox_Output
            // 
            this.listBox_Output.FormattingEnabled = true;
            this.listBox_Output.ItemHeight = 20;
            this.listBox_Output.Location = new System.Drawing.Point(12, 36);
            this.listBox_Output.Name = "listBox_Output";
            this.listBox_Output.Size = new System.Drawing.Size(887, 684);
            this.listBox_Output.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 787);
            this.Controls.Add(this.listBox_Output);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem initToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectTokenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletConnectToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem ServSetToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox_Output;
    }
}

