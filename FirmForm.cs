using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public partial class FirmForm : Form
    {
        public FirmForm()
        {
            InitializeComponent();
        }

        #region Opening form & updating data

        private void FirmForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            RefreshGasstations();
            RefreshSales();
            RefreshFuel();
        }
        public void RefreshFuel()
        {
            MySqlConnection connection = DBConnection.GetConnection();
            string query = "SELECT fuel_id as 'ID топлива', fuel_type as 'Вид топлива',fuel_unit as 'Ед. измерения', fuel_price as 'Цена (за ед. изм.)' FROM fuel WHERE firm_id = (SELECT firm_id FROM firm WHERE firm_name = " + "'" + Authorization.Name + "');";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns["ID топлива"].ReadOnly = true;
            dataGridView1.Columns["Ед. измерения"].ReadOnly = true;
            dataGridView1.Columns["Вид топлива"].ReadOnly = true;
            fuelType_combobox.SelectedIndex = 0;
        }

        public void RefreshGasstations()
        {
            MySqlConnection connection = DBConnection.GetConnection();
            string query = "SELECT gasStation_id as 'ID автозаправки', gasStation_address as 'Адрес автозаправки' FROM gasstation WHERE firm_id = (SELECT firm_id FROM firm WHERE firm_name = " + "'" + Authorization.Name + "');";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView2.DataSource = dataTable;
        }
        public void RefreshSales()
        {
            MySqlConnection connection = DBConnection.GetConnection();
            string query = @"SELECT
                fs.sale_id as 'ID продажи',
                fs.sale_date as 'Время продажи',
                c.customer_full_name as 'ФИО покупателя',
                fs.card_account as 'ID покупателя',
                gs.gasStation_id as 'ID Заправки',
                f.fuel_type as 'Вид топлива',
                fs.quantity as 'Объём'
                FROM
                    fuelsale fs
                JOIN
                    customer c ON fs.card_account = c.card_account
                JOIN
                    gasstation gs ON fs.gasStation_id = gs.gasStation_id
                JOIN
                    fuel f ON fs.fuel_id = f.fuel_id
                JOIN
                    firm fr ON gs.firm_id = fr.firm_id
                WHERE
                    fr.firm_name = " + "'" + Authorization.Name + "' " +
                "ORDER BY fs.sale_id;";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView3.DataSource = dataTable;
        }

        private void back_adm_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AuthorizationForm authorizationForm = new AuthorizationForm();
            authorizationForm.Show();
        }
        #endregion

        #region deleting fuel
        private void delete_Fuel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string fuelId = selectedRow.Cells["ID топлива"].Value.ToString();
                string fuelType = selectedRow.Cells["Вид топлива"].Value.ToString();

                if (HasDependencies(fuelId))
                {
                    DialogResult result = MessageBox.Show($"Вид топлива '{fuelType}' имеет входящие зависимости в таблице FuelSale. Удаление может повлиять на существующие записи. Продолжить удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return; // Пользователь отменил удаление
                    }
                }
                DialogResult resultY = MessageBox.Show($"Вы уверены, что хотите удалить вид топлива '{fuelType}'?","Удаление топлива", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultY == DialogResult.Yes)
                {
                    DeleteFuel(fuelId);
                }
                RefreshFuel();
            }
            else
            {
                MessageBox.Show("Выберите вид топлива для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool HasDependencies(string fuelId)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                string query = $"SELECT COUNT(*) FROM FuelSale WHERE fuel_id = {fuelId}";
                MySqlCommand command = new MySqlCommand(query, connection);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке зависимостей топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        private void DeleteFuel(string fuelId)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                string deleteQuery = $"DELETE FROM Fuel WHERE fuel_id = {fuelId}";

                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show($"Вид топлива успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }
        #endregion

        #region updating fuel


        readonly Dictionary<string, string> columnMappings = new Dictionary<string, string>
        {
            { "fuel_price", "Цена (за ед. изм.)" },
        };

        private string GetColumnNameInDatabase(string columnNameInDataGridView)
        {
            if (columnMappings.TryGetValue(columnNameInDataGridView, out string columnNameInDatabase))
            {
                return columnNameInDatabase;
            }
            return columnNameInDataGridView;
        }

        private decimal originalFuelPrice;

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                string columnNameInDataGridView = column.Name;
                originalFuelPrice = Convert.ToDecimal(row.Cells[columnNameInDataGridView].Value);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                string columnNameInDataGridView = column.Name;
                string columnNameInDatabase = GetColumnNameInDatabase(columnNameInDataGridView);

                string cellValue = row.Cells[columnNameInDataGridView].Value.ToString();

                if (cellValue != originalFuelPrice.ToString())
                {
                    if (ValidateFuelPrice(cellValue, out decimal newFuelPrice))
                    {
                        try
                        {
                            BeginInvoke((Action)(() => UpdateFuelPrice(row.Cells["ID топлива"].Value.ToString(), newFuelPrice, e)));
                        }
                        catch
                        {
                            row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                        }
                    }
                    else
                    {
                        row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    }
                }
                
            }
        }
        private void UpdateFuelPrice(string fuelId, decimal newFuelPrice, DataGridViewCellEventArgs e)
        {
            if (newFuelPrice <= 0)
            {
                MessageBox.Show("Цена топлива должна быть больше 0.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                string columnNameInDataGridView = column.Name;
                // Проверка на валидность введенного значения
                if (!ValidateFuelPrice(newFuelPrice.ToString(), out decimal validatedFuelPrice))
                {
                    MessageBox.Show("Некорректное значение для цены топлива.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    return;
                }

                if (validatedFuelPrice.ToString().StartsWith(",") && validatedFuelPrice.ToString().Length > 3)
                {
                    MessageBox.Show("Некорректное значение для цены топлива. Максимум 2 знака после запятой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    return;
                }

                if (validatedFuelPrice.ToString().EndsWith(",") && validatedFuelPrice.ToString().Length > 4)
                {
                    MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака в целой части!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    return;
                }

                if (!validatedFuelPrice.ToString().Contains(",") && validatedFuelPrice.ToString().Length > 3)
                {
                    MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака в целой части!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    return;
                }

                if (validatedFuelPrice.ToString().Length > 6)
                {
                    MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака до запятой и максимум 2 знака после!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells[columnNameInDataGridView].Value = originalFuelPrice;
                    return;
                }

                string updateQuery = "UPDATE Fuel SET fuel_price = @newFuelPrice WHERE fuel_id = @fuelId;";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@newFuelPrice", validatedFuelPrice);
                    command.Parameters.AddWithValue("@fuelId", fuelId);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show($"Цена топлива успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении цены топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        private bool ValidateFuelPrice(string input, out decimal result)
        {
            string formattedFuelPrice = input.Replace(',', '.');
            if (decimal.TryParse(formattedFuelPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out result) && result > 0)
            {
                return true; 
            }
            MessageBox.Show("Некорректное значение для цены топлива.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = e.Control as System.Windows.Forms.TextBox;
            if (textBox != null)
            {
                textBox.KeyPress -= textBox_KeyPress;
                textBox.KeyPress += textBox_KeyPress;
            }
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == ',')
            {
                if (textBox.Text.Contains(','))
                {
                    e.Handled = true; 
                    return;
                }
            }
        }
        #region adding

        private bool FuelExists(string fuelType)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                string query = $"SELECT COUNT(*) FROM fuel WHERE firm_id = (SELECT firm_id FROM firm WHERE firm_name = '{Authorization.Name}') AND fuel_type = @fuelType";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fuelType", fuelType);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке наличия топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        private void AddFuel(string fuelType, string fuelUnit, decimal fuelPrice)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                string insertQuery = "INSERT INTO fuel (firm_id, fuel_type, fuel_unit, fuel_price) " +
                                     "VALUES ((SELECT firm_id FROM firm WHERE firm_name = @firmName), @fuelType, @fuelUnit, @fuelPrice);";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@firmName", Authorization.Name);
                    command.Parameters.AddWithValue("@fuelType", fuelType);
                    command.Parameters.AddWithValue("@fuelUnit", fuelUnit);
                    command.Parameters.AddWithValue("@fuelPrice", fuelPrice);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show($"Топливо '{fuelType}' успешно добавлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshFuel(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }
        private void add_Fuel_Click(object sender, EventArgs e)
        {
            string fuelType = fuelType_combobox.Text.Trim();
            string fuelUnit = "ЛИТР"; 
            try
            {
                if (textBox_Price.Text == "")
                {
                    MessageBox.Show("Заполните цену!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Regex.IsMatch(textBox_Price.Text, @"\d"))
                {
                    if (textBox_Price.Text.StartsWith(",") && textBox_Price.Text.Length > 3)
                    {
                        MessageBox.Show("Некорректное значение для цены топлива. Максимум 2 знака после запятой!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (textBox_Price.Text.EndsWith(",") && textBox_Price.Text.Length > 4)
                    {
                        MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака в целой части!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!textBox_Price.Text.Contains(",") && textBox_Price.Text.Length > 3)
                    {
                        MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака в целой части!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (textBox_Price.Text.Length > 6)
                    {
                        MessageBox.Show("Некорректное значение для цены топлива. Максимум 3 знака до запятой и максимум 2 знака после!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal fuelPrice = decimal.Parse(textBox_Price.Text); 

                    if (string.IsNullOrWhiteSpace(fuelType))
                    {
                        MessageBox.Show("Введите вид топлива.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (FuelExists(fuelType))
                    {
                        MessageBox.Show($"Топливо '{fuelType}' уже существует в списке.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    AddFuel(fuelType, fuelUnit, fuelPrice);
                }
            }
            catch
            {
                MessageBox.Show("Укажите корректную цену!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        #endregion

        #endregion

        #region report

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            FillStationComboBox();
            station_comboBox.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label10.Text = "Фирма: " + Authorization.Name;
            station_comboBox_SelectedIndexChanged(sender, e);
        }

        private void station_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (station_comboBox.SelectedIndex != -1)
            {
                // Получение номера автозаправки из комбо-бокса
                int gasStationId = Convert.ToInt32(station_comboBox.SelectedItem);

                try
                {
                    // Ваш код для выполнения SQL-запроса к базе данных
                    string query = "SELECT gasStation_address FROM GasStation WHERE gasStation_id = @gasStationId";

                    using (MySqlConnection connection = DBConnection.GetConnection())
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@gasStationId", gasStationId);

                            // Получение адреса из базы данных
                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                // Вывод адреса в нужное место (например, в Label)
                                label12.Text = "Адрес автозаправки: " + result.ToString();
                            }
                            else
                            {
                                label12.Text = "Адрес не найден";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void FillStationComboBox()
        {
            station_comboBox.Items.Clear();
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                string firmQuery = "SELECT gasStation_id FROM gasstation WHERE firm_id = (SELECT firm_id FROM firm WHERE firm_name = @firmName);";
                {
                    using (MySqlCommand command = new MySqlCommand(firmQuery, connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        command.Parameters.AddWithValue("@firmName", Authorization.Name);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                station_comboBox.Items.Add(reader["gasStation_id"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка //... {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
            
        }
        private void generateReport_button_Click(object sender, EventArgs e)
        {
            if (station_comboBox.SelectedItem == null || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(textBox1.Text, @"^\d{4}$"))
            {
                MessageBox.Show("Год должен состоять из 4 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int gasStationId = Convert.ToInt32(station_comboBox.SelectedItem.ToString());
            int monthFrom = comboBox1.SelectedIndex + 1; 
            int monthTo = comboBox2.SelectedIndex + 1;
            int year = Convert.ToInt32(textBox1.Text);

            if (monthFrom > monthTo)
            {
                MessageBox.Show("Месяц 'От' не может быть больше месяца 'До'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string reportQuery = "SELECT fuelsale.sale_date AS 'Дата продажи', " +
                    "fuelsale.card_account AS 'Счёт клиента', " +
                    "fuel.fuel_type AS 'Вид топлива', " +
                    "fuel.fuel_price AS 'Цена (за ед. изм., руб)', " +
                    "fuelsale.quantity AS 'Объём', " +
                    "(fuel.fuel_price * fuelsale.quantity) AS 'Стоимость, руб.' " +
                    "FROM fuelsale " +
                    "JOIN fuel ON fuelsale.fuel_id = fuel.fuel_id " +
                    "WHERE fuelsale.gasStation_id = @gasStationId " +
                    "AND MONTH(fuelsale.sale_date) BETWEEN @monthFrom AND @monthTo " +
                    "AND YEAR(fuelsale.sale_date) = @year;";

                using (MySqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(reportQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gasStationId", gasStationId);
                        command.Parameters.AddWithValue("@monthFrom", monthFrom);
                        command.Parameters.AddWithValue("@monthTo", monthTo);
                        command.Parameters.AddWithValue("@year", year);

                        DataTable dataTable = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Продажи не обнаружены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dataGridView4.DataSource = dataTable;
                            label11.Text = $"с {comboBox1.SelectedItem} по {comboBox2.SelectedItem} {textBox1.Text} года";
                            label11.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string query = "SELECT SUM(fuelsale.quantity) as 'Объём', SUM(fuel.fuel_price * fuelsale.quantity) AS 'Стоимость, руб.' " +
                               "FROM fuelsale " +
                               "JOIN fuel ON fuelsale.fuel_id = fuel.fuel_id " +
                               "WHERE fuelsale.gasStation_id = @gasStationId " +
                               "AND MONTH(fuelsale.sale_date) BETWEEN @startMonth AND @endMonth " +
                               "AND YEAR(fuelsale.sale_date) = @year;";

                using (MySqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gasStationId", gasStationId); 
                        command.Parameters.AddWithValue("@startMonth", monthFrom); 
                        command.Parameters.AddWithValue("@endMonth", monthTo); 
                        command.Parameters.AddWithValue("@year", year); 

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string volume = reader["Объём"].ToString();
                                    string cost = reader["Стоимость, руб."].ToString();
                                    label14.Text = "Объём (литров): " + volume;
                                    label15.Text = "Стоимость, руб.: " + cost;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
