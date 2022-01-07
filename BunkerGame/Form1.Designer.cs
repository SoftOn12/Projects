namespace Bunker_Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCataclysm = new System.Windows.Forms.TextBox();
            this.textBoxBunker = new System.Windows.Forms.TextBox();
            this.buttonBunker = new System.Windows.Forms.Button();
            this.buttonCataclysm = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonCreatePerson = new System.Windows.Forms.Button();
            this.labelNumberOfPersons = new System.Windows.Forms.Label();
            this.textBoxNumberOfPersons = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox1.Controls.Add(this.textBoxCataclysm);
            this.groupBox1.Controls.Add(this.textBoxBunker);
            this.groupBox1.Controls.Add(this.buttonBunker);
            this.groupBox1.Controls.Add(this.buttonCataclysm);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.buttonCreatePerson);
            this.groupBox1.Controls.Add(this.labelNumberOfPersons);
            this.groupBox1.Controls.Add(this.textBoxNumberOfPersons);
            this.groupBox1.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(100, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1080, 540);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Меню";
            // 
            // textBoxCataclysm
            // 
            this.textBoxCataclysm.Location = new System.Drawing.Point(21, 279);
            this.textBoxCataclysm.Multiline = true;
            this.textBoxCataclysm.Name = "textBoxCataclysm";
            this.textBoxCataclysm.ReadOnly = true;
            this.textBoxCataclysm.Size = new System.Drawing.Size(204, 222);
            this.textBoxCataclysm.TabIndex = 7;
            this.textBoxCataclysm.Visible = false;
            // 
            // textBoxBunker
            // 
            this.textBoxBunker.Location = new System.Drawing.Point(370, 279);
            this.textBoxBunker.Multiline = true;
            this.textBoxBunker.Name = "textBoxBunker";
            this.textBoxBunker.ReadOnly = true;
            this.textBoxBunker.Size = new System.Drawing.Size(204, 222);
            this.textBoxBunker.TabIndex = 6;
            this.textBoxBunker.Visible = false;
            // 
            // buttonBunker
            // 
            this.buttonBunker.Location = new System.Drawing.Point(414, 213);
            this.buttonBunker.Name = "buttonBunker";
            this.buttonBunker.Size = new System.Drawing.Size(126, 60);
            this.buttonBunker.TabIndex = 5;
            this.buttonBunker.Text = "Создать Бункер\r\n";
            this.buttonBunker.UseVisualStyleBackColor = true;
            this.buttonBunker.Click += new System.EventHandler(this.buttonBunker_Click);
            // 
            // buttonCataclysm
            // 
            this.buttonCataclysm.Location = new System.Drawing.Point(58, 213);
            this.buttonCataclysm.Name = "buttonCataclysm";
            this.buttonCataclysm.Size = new System.Drawing.Size(126, 60);
            this.buttonCataclysm.TabIndex = 4;
            this.buttonCataclysm.Text = "Создать Катаклизм\r\n";
            this.buttonCataclysm.UseVisualStyleBackColor = true;
            this.buttonCataclysm.Click += new System.EventHandler(this.buttonCataclysm_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(580, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(495, 514);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // buttonCreatePerson
            // 
            this.buttonCreatePerson.Location = new System.Drawing.Point(233, 125);
            this.buttonCreatePerson.Name = "buttonCreatePerson";
            this.buttonCreatePerson.Size = new System.Drawing.Size(126, 60);
            this.buttonCreatePerson.TabIndex = 2;
            this.buttonCreatePerson.Text = "Создать";
            this.buttonCreatePerson.UseVisualStyleBackColor = true;
            this.buttonCreatePerson.Click += new System.EventHandler(this.buttonCreatePerson_Click);
            // 
            // labelNumberOfPersons
            // 
            this.labelNumberOfPersons.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelNumberOfPersons.AutoSize = true;
            this.labelNumberOfPersons.Location = new System.Drawing.Point(89, 96);
            this.labelNumberOfPersons.Name = "labelNumberOfPersons";
            this.labelNumberOfPersons.Size = new System.Drawing.Size(418, 26);
            this.labelNumberOfPersons.TabIndex = 1;
            this.labelNumberOfPersons.Text = "Введите кол-во необходимых персонажей";
            // 
            // textBoxNumberOfPersons
            // 
            this.textBoxNumberOfPersons.Location = new System.Drawing.Point(258, 58);
            this.textBoxNumberOfPersons.Name = "textBoxNumberOfPersons";
            this.textBoxNumberOfPersons.Size = new System.Drawing.Size(75, 33);
            this.textBoxNumberOfPersons.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Bunker Game";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCreatePerson;
        private System.Windows.Forms.Label labelNumberOfPersons;
        private System.Windows.Forms.TextBox textBoxNumberOfPersons;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxCataclysm;
        private System.Windows.Forms.TextBox textBoxBunker;
        private System.Windows.Forms.Button buttonBunker;
        private System.Windows.Forms.Button buttonCataclysm;
    }
}

