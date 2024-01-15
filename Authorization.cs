using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBase
{

    internal class Authorization
    {
        static public string Role, Name, FullName;

        static public void Authorization_act(string login, string password)
        {
            try
            {
                DBConnection.msCommand.CommandText = @"SELECT role FROM user_firm_view WHERE username = " + "'" + login + "'" + " and password = " + "'" + password + "'";
                Object result = DBConnection.msCommand.ExecuteScalar();

                if (result != null)
                {
                    Role = result.ToString();
                    Name = login;
                }
                else
                {
                    Role = null;
                    Name = null;
                }
            }
            catch 
            {
                Role = Name = null;
                MessageBox.Show("Ошибка при авторизации!","Невалидные данные!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
        }
        static public string AuthorizationName(string login)
        {
            try
            {
                DBConnection.msCommand.CommandText = @"SELECT username FROM user_firm_view WHERE username = " + "'" + login + "'";
                Object result = DBConnection.msCommand.ExecuteScalar();
                Name = result.ToString();
                return Name;

            }
            catch
            {
                return null;
            }
        }

        static public string AuthorizationFullName(string login)
        {
            try
            {
                DBConnection.msCommand.CommandText = @"SELECT customer_full_name FROM customer WHERE card_account = " + "'" + login + "'";
                Object fullNameResult = DBConnection.msCommand.ExecuteScalar();
                FullName = fullNameResult.ToString();
                return FullName;

            }
            catch
            {
                return null;
            }
        }

    }
}
