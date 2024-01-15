using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class AuthorizationForm : Form
    {
        static public string loginActive;
        static public string whois;
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            login_textbox.Text = "";
            psw_textbox.Text = "";
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            DBConnection.ConnectionDB();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string loginError = null;
            string passwordError = null;

            // Проверка наличия логина
            if (string.IsNullOrWhiteSpace(login_textbox.Text))
            {
                loginError = "Введите логин.";
            }

            // Проверка наличия пароля
            if (string.IsNullOrWhiteSpace(psw_textbox.Text))
            {
                passwordError = "Введите пароль.";
            }

            if (loginError != null || passwordError != null)
            {
                // Отображение сообщений об ошибках
                MessageBox.Show($"{loginError}\n{passwordError}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Authorization.Authorization_act(login_textbox.Text, psw_textbox.Text);
            switch (Authorization.Role)
            {
                case null:
                    {
                        login_textbox.Text = "";
                        psw_textbox.Text = "";
                        MessageBox.Show("Неверный логин или пароль!", "Невалидные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "administrator":
                    {
                        loginActive = login_textbox.Text;
                        whois = "administrator";
                        Authorization.Name = loginActive;
                        string Name = Authorization.AuthorizationName(loginActive);
                        Authorization.Name = Name;
                        MessageBox.Show(Name + ", добро пожаловать в АвтоЗаправки с правами администратора!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        AdminForm admin = new AdminForm();
                        admin.Show();
                        break;

                    }
                case "customer_role":
                    {
                        loginActive = login_textbox.Text;
                        whois = "пользователь";
                        Authorization.Name = loginActive;
                        string Name = Authorization.AuthorizationName(loginActive);
                        Authorization.Name = Name;
                        string FullName = Authorization.AuthorizationFullName(loginActive);
                        Authorization.FullName = FullName;
                        MessageBox.Show(FullName + ", добро пожаловать в АвтоЗаправки с правами пользователя!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        UserForm user = new UserForm();
                        user.Show();
                        break;
                    }
                case "firm_role":
                    {
                        loginActive = login_textbox.Text;
                        whois = "фирма";
                        Authorization.Name = loginActive;
                        string Name = Authorization.AuthorizationName(loginActive);
                        Authorization.Name = Name;
                        string FullName = Authorization.AuthorizationFullName(loginActive);
                        Authorization.FullName = FullName;
                        MessageBox.Show(Name + ", добро пожаловать в АвтоЗаправки с правами фирмы-поставщика!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        FirmForm firmF = new FirmForm();
                        firmF.Show();
                        break;
                    }
            }
        }

        private void AuthorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBConnection.CloseDB();
        }
    }
}
