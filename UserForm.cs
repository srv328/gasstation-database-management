using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = DBConnection.GetConnection();
            string query = "SELECT card_account as 'Карточный счёт', customer_full_name as 'ФИО',customer_address as 'Адрес', customer_phone as 'Номер телефона' from Customer WHERE card_account = " + "'" + Authorization.Name + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView.DataSource = dataTable;
            loadingSale();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AuthorizationForm authorizationForm = new AuthorizationForm();
            authorizationForm.Show();
        }

        private void loadingSale()
        {
            try
            {
                MySqlConnection connection = DBConnection.GetConnection();

                // Запрос для получения всех покупок пользователя
                string purchasesQuery = "SELECT fs.sale_date AS 'Дата покупки', " +
                                        "f.fuel_type AS 'Вид топлива', " +
                                        "f.fuel_price AS 'Цена', " +
                                        "fs.quantity AS 'Количество', " +
                                        "(f.fuel_price * fs.quantity) AS 'Стоимость', " +
                                        "gs.gasStation_address AS 'Адрес заправки', " +
                                        "fr.firm_name AS 'Название фирмы' " +
                                        "FROM FuelSale fs " +
                                        "JOIN Fuel f ON fs.fuel_id = f.fuel_id " +
                                        "JOIN GasStation gs ON fs.gasStation_id = gs.gasStation_id " +
                                        "JOIN Firm fr ON gs.firm_id = fr.firm_id " +
                                        "WHERE fs.card_account = " + "'" + Authorization.Name + "';";

                // Запрос для получения общего количества и стоимости покупок пользователя
                string summaryQuery = "SELECT " +
                                      "SUM(fs.quantity) AS 'Общее количество', " +
                                      "SUM(f.fuel_price * fs.quantity) AS 'Общая стоимость' " +
                                      "FROM FuelSale fs " +
                                      "JOIN Fuel f ON fs.fuel_id = f.fuel_id " +
                                      "WHERE fs.card_account = " + "'" + Authorization.Name + "';";

                using (MySqlDataAdapter purchasesAdapter = new MySqlDataAdapter(purchasesQuery, connection))
                using (MySqlDataAdapter summaryAdapter = new MySqlDataAdapter(summaryQuery, connection))
                {
                    DataTable purchasesTable = new DataTable();
                    purchasesAdapter.Fill(purchasesTable);

                    DataTable summaryTable = new DataTable();
                    summaryAdapter.Fill(summaryTable);

                    dataGridView1.DataSource = purchasesTable;

                    if (purchasesTable.Rows.Count > 0)
                    {
                        dataGridView1.Visible = true;
                        label3.Visible = false;
                        label4.Visible = true;
                        label5.Visible = true;
                    }
                    else
                    {
                        dataGridView1.Visible = false;
                        label3.Visible = true;
                        label4.Visible = false;
                        label5.Visible = false;
                    }
                    if (summaryTable.Rows.Count > 0)
                    {
                        label4.Text = "Общее количество: " + summaryTable.Rows[0]["Общее количество"].ToString();
                        label5.Text = "Общая стоимость: " + summaryTable.Rows[0]["Общая стоимость"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
