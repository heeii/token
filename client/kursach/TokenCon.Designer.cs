namespace kursach
{
    partial class TokenCon
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
            this.comboBoxHandshake = new System.Windows.Forms.ComboBox();
            this.labelHandshake = new System.Windows.Forms.Label();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.labelStopBits = new System.Windows.Forms.Label();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.labelDataBits = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.labelParity = new System.Windows.Forms.Label();
            this.labelBauds = new System.Windows.Forms.Label();
            this.textBoxBaudsRate = new System.Windows.Forms.TextBox();
            this.labelPorts = new System.Windows.Forms.Label();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxHandshake
            // 
            this.comboBoxHandshake.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxHandshake.FormattingEnabled = true;
            this.comboBoxHandshake.Location = new System.Drawing.Point(60, 560);
            this.comboBoxHandshake.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxHandshake.Name = "comboBoxHandshake";
            this.comboBoxHandshake.Size = new System.Drawing.Size(199, 33);
            this.comboBoxHandshake.TabIndex = 23;
            this.comboBoxHandshake.SelectedIndexChanged += new System.EventHandler(this.comboBoxHandshake_SelectedIndexChanged);
            // 
            // labelHandshake
            // 
            this.labelHandshake.AutoSize = true;
            this.labelHandshake.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHandshake.Location = new System.Drawing.Point(28, 510);
            this.labelHandshake.Name = "labelHandshake";
            this.labelHandshake.Size = new System.Drawing.Size(257, 25);
            this.labelHandshake.TabIndex = 22;
            this.labelHandshake.Text = "Протокол управления:";
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Location = new System.Drawing.Point(60, 449);
            this.comboBoxStopBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(199, 33);
            this.comboBoxStopBits.TabIndex = 21;
            this.comboBoxStopBits.SelectedIndexChanged += new System.EventHandler(this.comboBoxStopBits_SelectedIndexChanged);
            // 
            // labelStopBits
            // 
            this.labelStopBits.AutoSize = true;
            this.labelStopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStopBits.Location = new System.Drawing.Point(28, 403);
            this.labelStopBits.Name = "labelStopBits";
            this.labelStopBits.Size = new System.Drawing.Size(263, 25);
            this.labelStopBits.TabIndex = 20;
            this.labelStopBits.Text = "Число стоповых битов:";
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Location = new System.Drawing.Point(60, 351);
            this.comboBoxDataBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(199, 33);
            this.comboBoxDataBits.TabIndex = 19;
            this.comboBoxDataBits.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataBits_SelectedIndexChanged);
            // 
            // labelDataBits
            // 
            this.labelDataBits.AutoSize = true;
            this.labelDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDataBits.Location = new System.Drawing.Point(2, 302);
            this.labelDataBits.Name = "labelDataBits";
            this.labelDataBits.Size = new System.Drawing.Size(330, 25);
            this.labelDataBits.TabIndex = 18;
            this.labelDataBits.Text = "Число битов данных в байте:";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Location = new System.Drawing.Point(60, 249);
            this.comboBoxParity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(199, 33);
            this.comboBoxParity.TabIndex = 17;
            this.comboBoxParity.SelectedIndexChanged += new System.EventHandler(this.comboBoxParity_SelectedIndexChanged);
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelParity.Location = new System.Drawing.Point(2, 206);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(335, 25);
            this.labelParity.TabIndex = 16;
            this.labelParity.Text = "Протокол контроля четности:";
            // 
            // labelBauds
            // 
            this.labelBauds.AutoSize = true;
            this.labelBauds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBauds.Location = new System.Drawing.Point(12, 110);
            this.labelBauds.Name = "labelBauds";
            this.labelBauds.Size = new System.Drawing.Size(323, 25);
            this.labelBauds.TabIndex = 15;
            this.labelBauds.Text = "Скорость общения в БОДах:";
            // 
            // textBoxBaudsRate
            // 
            this.textBoxBaudsRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBaudsRate.Location = new System.Drawing.Point(60, 160);
            this.textBoxBaudsRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBaudsRate.Name = "textBoxBaudsRate";
            this.textBoxBaudsRate.Size = new System.Drawing.Size(199, 31);
            this.textBoxBaudsRate.TabIndex = 14;
            // 
            // labelPorts
            // 
            this.labelPorts.AutoSize = true;
            this.labelPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPorts.Location = new System.Drawing.Point(55, 9);
            this.labelPorts.Name = "labelPorts";
            this.labelPorts.Size = new System.Drawing.Size(212, 25);
            this.labelPorts.TabIndex = 13;
            this.labelPorts.Text = "Доступные порты:";
            this.labelPorts.Click += new System.EventHandler(this.labelPorts_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(60, 62);
            this.comboBoxPorts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(199, 33);
            this.comboBoxPorts.TabIndex = 12;
            this.comboBoxPorts.Text = "COM3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 617);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 32);
            this.button1.TabIndex = 24;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TokenCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 661);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxHandshake);
            this.Controls.Add(this.labelHandshake);
            this.Controls.Add(this.comboBoxStopBits);
            this.Controls.Add(this.labelStopBits);
            this.Controls.Add(this.comboBoxDataBits);
            this.Controls.Add(this.labelDataBits);
            this.Controls.Add(this.comboBoxParity);
            this.Controls.Add(this.labelParity);
            this.Controls.Add(this.labelBauds);
            this.Controls.Add(this.textBoxBaudsRate);
            this.Controls.Add(this.labelPorts);
            this.Controls.Add(this.comboBoxPorts);
            this.Name = "TokenCon";
            this.Text = "TokenCon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHandshake;
        private System.Windows.Forms.Label labelHandshake;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.Label labelStopBits;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Label labelDataBits;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.Label labelParity;
        private System.Windows.Forms.Label labelBauds;
        private System.Windows.Forms.TextBox textBoxBaudsRate;
        private System.Windows.Forms.Label labelPorts;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button button1;
    }
}