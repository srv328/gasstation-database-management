namespace DataBase
{
    partial class AuthorizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.login_label = new System.Windows.Forms.Label();
            this.psw_label = new System.Windows.Forms.Label();
            this.login_textbox = new System.Windows.Forms.TextBox();
            this.psw_textbox = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.clear_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(106, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Добро пожаловать в АвтоЗаправки!";
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_label.Location = new System.Drawing.Point(107, 126);
            this.login_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(63, 20);
            this.login_label.TabIndex = 1;
            this.login_label.Text = "Логин: ";
            // 
            // psw_label
            // 
            this.psw_label.AutoSize = true;
            this.psw_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.psw_label.Location = new System.Drawing.Point(107, 167);
            this.psw_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.psw_label.Name = "psw_label";
            this.psw_label.Size = new System.Drawing.Size(71, 20);
            this.psw_label.TabIndex = 2;
            this.psw_label.Text = "Пароль:";
            // 
            // login_textbox
            // 
            this.login_textbox.Location = new System.Drawing.Point(238, 128);
            this.login_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.login_textbox.Name = "login_textbox";
            this.login_textbox.Size = new System.Drawing.Size(259, 20);
            this.login_textbox.TabIndex = 3;
            // 
            // psw_textbox
            // 
            this.psw_textbox.Location = new System.Drawing.Point(238, 171);
            this.psw_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.psw_textbox.Name = "psw_textbox";
            this.psw_textbox.Size = new System.Drawing.Size(259, 20);
            this.psw_textbox.TabIndex = 4;
            this.psw_textbox.UseSystemPasswordChar = true;
            // 
            // login_btn
            // 
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_btn.Location = new System.Drawing.Point(238, 225);
            this.login_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(136, 45);
            this.login_btn.TabIndex = 5;
            this.login_btn.Text = "Авторизоваться";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear_btn.Location = new System.Drawing.Point(388, 225);
            this.clear_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(113, 45);
            this.clear_btn.TabIndex = 6;
            this.clear_btn.Text = "Очистить";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 385);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.psw_textbox);
            this.Controls.Add(this.login_textbox);
            this.Controls.Add(this.psw_label);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AuthorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автозаправки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthorizationForm_FormClosing);
            this.Load += new System.EventHandler(this.AuthorizationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.Label psw_label;
        private System.Windows.Forms.TextBox login_textbox;
        private System.Windows.Forms.TextBox psw_textbox;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.Button clear_btn;
    }
}

