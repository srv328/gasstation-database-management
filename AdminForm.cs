using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public partial class AdminForm : Form
    {
        private string originalCellValue;

        public AdminForm()
        {
            InitializeComponent();
        }

        #region BackButton
        private void back_adm_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AuthorizationForm AuthorizationF = new AuthorizationForm();
            AuthorizationF.Show();
        }
        #endregion

        #region ОТКРЫТИЕ ФОРМЫ, ДАННЫЕ
        private void AdminForm_Load(object sender, EventArgs e)
        {
            RefreshAllGrids();
            fuelType_combobox.SelectedIndex = 0;
        }

       
        private bool CheckAssociatedData(string tableName, string fieldName, string value)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                // Проверяем наличие связанных данных в указанной таблице
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {fieldName} = @value";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Добавляем параметр
                    command.Parameters.AddWithValue("@value", value);

                    // Открываем соединение
                    connection.Open();

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке связанных данных в {tableName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #region Обновление гридов

        private void RefreshAllGrids()
        {
            RefreshFuelSale();
            RefreshFirms();
            RefreshUsers();
            RefreshGasStation();
        }


        private void RefreshDataGridView(DataGridView dataGridView, string query)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }

        private void RefreshFuelSale()
        {
            string query = "SELECT FuelSale.sale_id as 'ID', FuelSale.sale_date as 'Время продажи', Customer.customer_full_name as 'ФИО покупателя', FuelSale.gasStation_id as 'Код автозаправки', Fuel.fuel_type as 'Вид топлива', FuelSale.quantity as 'Объём' FROM FuelSale JOIN Customer ON FuelSale.card_account = Customer.card_account JOIN GasStation ON FuelSale.gasStation_id = GasStation.gasStation_id JOIN Fuel ON FuelSale.fuel_id = Fuel.fuel_id ORDER BY FuelSale.sale_id;";
            RefreshDataGridView(dataGridView4, query);
        }

        private void RefreshFirms()
        {
            string query = "SELECT firm_id as 'Код фирмы', firm_name as 'Название фирмы', firm_address as 'Адрес фирмы', firm_phone as 'Номер телефона' FROM Firm;";
            RefreshDataGridView(dataGridView2, query);
        }

        private void RefreshUsers()
        {
            string query = "SELECT card_account as 'Карточный счёт', customer_full_name as 'ФИО',customer_address as 'Адрес', customer_phone as 'Номер телефона' from Customer;";
            RefreshDataGridView(dataGridView1, query);
            dataGridView1.Columns["Карточный счёт"].ReadOnly = true;
        }

        private void RefreshGasStation()
        {
            string query = "SELECT GasStation.gasStation_id as 'Код автозаправки', GasStation.gasStation_address as 'Адрес автозаправки', Firm.firm_name as 'Название фирмы' FROM GasStation JOIN Firm ON GasStation.firm_id = Firm.firm_id ORDER BY GasStation.gasStation_id;";
            RefreshDataGridView(dataGridView3, query);
            dataGridView3.Columns["Код автозаправки"].ReadOnly = true;
        }

        #endregion

        #region Users

        #region Validating DATA
        private bool ValidateAddress(string address)
        {
            return Regex.IsMatch(address, @"^[А-ЯЁ][а-яё]+(?:,\s*(ул\.|пр\.|просп\.|проспект)\s*[А-ЯЁ][а-яё]+)?,\s*\d+[А-ЯЁ]*$") || address == "";
        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^7\d{10}$") || phoneNumber == "";
        }


        private bool IsValidData(string columnName, string value)
        {
            switch (columnName)
            {
                case "customer_full_name":
                    return ValidateFullName(value);
                case "customer_address":
                    return ValidateAddress(value);
                case "customer_phone":
                    return ValidatePhoneNumber(value);
                // Добавьте другие проверки для необходимых столбцов
                default:
                    return true;
            }
        }
        private bool ValidateFullName(string fullName)
        {
            return Regex.IsMatch(fullName, @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+(\s[А-ЯЁ][а-яё]+)?$");
        }
        #endregion

        #region Updating User

        private string GetColumnNameInDatabase(string columnNameInDataGridView)
        {
            if (columnMappings.TryGetValue(columnNameInDataGridView, out string columnNameInDatabase))
            {
                return columnNameInDatabase;
            }
            return columnNameInDataGridView;
        }

        readonly Dictionary<string, string> columnMappings = new Dictionary<string, string>
        {
            { "Карточный счёт", "card_account" },
            { "ФИО", "customer_full_name" },
            { "Адрес", "customer_address" },
            { "Номер телефона", "customer_phone" }
        };

        private void UpdateRecordInDatabase(string cardAccount, string columnName, string updatedValue, string currentColumnValue)
        {
            MySqlConnection connection = null;
            try
            {
                if (updatedValue == currentColumnValue)
                {
                    return;
                }

                connection = DBConnection.GetConnection();


                using (MySqlCommand command = connection.CreateCommand())
                {
                    string updateQuery = $"UPDATE Customer SET {columnName} = @updatedValue WHERE card_account = @cardAccount;";
                    command.CommandText = updateQuery;

                    // Добавляем параметры, только если значение не пустое
                    if (!string.IsNullOrEmpty(updatedValue))
                    {
                        command.Parameters.AddWithValue("@updatedValue", updatedValue);
                    }

                    command.Parameters.AddWithValue("@cardAccount", cardAccount);

                    // Проверяем уникальность перед обновлением
                    if (columnName == "customer_phone" && !IsPhoneNumberUnique(updatedValue, currentColumnValue))
                    {
                        MessageBox.Show("Номер телефона уже используется. Введите уникальный номер телефона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Выполняем запрос
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close(); // Закрыть соединение в блоке finally
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                string columnNameInDataGridView = column.Name;
                originalCellValue = row.Cells[columnNameInDataGridView].Value.ToString();
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

                // Проверка на уникальность только если значение изменилось
                if (cellValue != originalCellValue)
                {
                    // Проверяем уникальность перед обновлением
                    if (columnNameInDatabase == "customer_phone" && !IsPhoneNumberUnique(cellValue, originalCellValue))
                    {
                        MessageBox.Show("Номер телефона уже используется. Введите уникальный номер телефона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[columnNameInDataGridView].Value = originalCellValue; // Восстанавливаем предыдущее значение
                        return;
                    }

                    // Проверка на валидность данных
                    if (!IsValidData(columnNameInDatabase, cellValue))
                    {
                        MessageBox.Show($"Некорректное значение для {columnNameInDataGridView}. Введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[columnNameInDataGridView].Value = originalCellValue; // Восстанавливаем предыдущее значение
                        return;
                    }

                    // Обновляем запись в базе данных
                    BeginInvoke((Action)(() => UpdateRecordInDatabase(row.Cells["Карточный счёт"].Value.ToString(), columnNameInDatabase, cellValue, originalCellValue)));
                }
            }
        }


        #endregion

        #region Adding User
        private void AddUserToDatabase(string fullName, string address, string phoneNumber)
        {
            MySqlConnection connection = null;

            try
            {
                connection = DBConnection.GetConnection();

                // Проверяем уникальность номера телефона и адреса перед добавлением
                if (!IsPhoneNumberUnique(phoneNumber, ""))
                {
                    MessageBox.Show("Номер телефона уже используется. Введите уникальный номер телефона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    number_textBox.Text = "";
                    return;
                }

                // Используем параметризованный запрос для безопасности
                string insertQuery = "INSERT INTO Customer (customer_full_name";
                string values = "@fullName";

                if (!string.IsNullOrEmpty(address))
                {
                    insertQuery += ", customer_address";
                    values += ", @address";
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    insertQuery += ", customer_phone";
                    values += ", @phoneNumber";
                }

                insertQuery += ") VALUES (" + values + ")";

                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue("@fullName", fullName);

                if (!string.IsNullOrEmpty(address))
                {
                    command.Parameters.AddWithValue("@address", address);
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                }

                // Открываем соединение
                connection.Open();

                // Выполняем запрос
                command.ExecuteNonQuery();

                ClearTextFields();
                MessageBox.Show($"Пользователь {fullName} успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                // Закрываем соединение в блоке finally, чтобы гарантированно выполнить закрытие
                connection?.Close();
            }
        }
        private void add_User_Click(object sender, EventArgs e)
        {

            // Получение значений из текстовых полей
            string fullName = fullname_textBox.Text;
            string address = address_textBox.Text;
            string phoneNumber = number_textBox.Text;

            if (!ValidateFullName(fullName))
            {
                MessageBox.Show("Некорректное ФИО. Введите три (два, если отсутствует отчество) слова, начинающихся с заглавных букв.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ValidateAddress(address))
            {
                MessageBox.Show("Некорректный Адрес. Введите адрес: Начинается с города (заглавная буква, далее буквы);\r\nЗа городом может следовать запятая или пробел;\r\nЗа запятой или пробелом следует улица (заглавная буква, далее буквы);\r\nЭтот процесс может повторяться для дополнительных частей адреса, разделенных запятой или пробелом;\r\nЗавершается цифровым номером дома.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Некорректный номер телефона. Введите 11 цифр, начиная с 7.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Добавление пользователя в базу данных
            AddUserToDatabase(fullName, address, phoneNumber);

        }

        private void ClearTextFields()
        {
            // Очистка полей после успешного добавления
            fullname_textBox.Clear();
            address_textBox.Clear();
            number_textBox.Clear();
        }
        private bool IsPhoneNumberUnique(string newPhoneNumber, string currentPhoneNumber)
        {
            if (newPhoneNumber == "")
            {
                return true;
            }

            // Если номер телефона не изменился, считаем его уникальным
            if (newPhoneNumber == currentPhoneNumber)
            {
                return true;
            }

            MySqlConnection connection = DBConnection.GetConnection();

            // Открываем соединение, если оно еще не открыто
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                // Проверяем уникальность нового номера телефона
                string query = $"SELECT COUNT(*) FROM Customer WHERE customer_phone = '{newPhoneNumber}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0; // Если count равен 0, то новый номер телефона уникален
            }
            finally
            {
                // Всегда закрываем соединение, даже если произошла ошибка
                connection.Close();
            }
        }

        
        #endregion

        #region Deleting User
        private void delete_User_Click(object sender, EventArgs e)
        {
            // Получение выбранной строки в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Получение значения карточного счета выбранного пользователя
                string cardAccount = selectedRow.Cells["Карточный счёт"].Value.ToString();

                // Проверка связанных данных
                bool hasAssociatedDataInFuelSale = CheckAssociatedData("FuelSale", "card_account", cardAccount);

                // Подтверждение удаления
                DialogResult result;
                if (hasAssociatedDataInFuelSale)
                {
                    result = MessageBox.Show("Пользователь связан с другими данными. Вы уверены, что хотите удалить пользователя со всеми связанными данными?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                else
                {
                    result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (result == DialogResult.Yes)
                {
                    // Удаление пользователя
                    if (hasAssociatedDataInFuelSale)
                    {
                        // Используйте CASCADE DELETE, если есть связанные данные
                        DeleteUserWithAssociatedData(cardAccount);
                    }
                    else
                    {
                        // Просто удалите пользователя
                        DeleteUser(cardAccount);
                    }
                    RefreshAllGrids();
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteUser(string cardAccount)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                // Проверка связанных данных в таблице FuelSale
                if (CheckAssociatedData("FuelSale", "card_account", cardAccount))
                {
                    // Если есть связанные данные, запрос подтверждения удаления
                    DialogResult result = MessageBox.Show("Пользователь связан с продажами топлива. Вы уверены, что хотите удалить пользователя со всеми связанными данными?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return; // Пользователь отменил удаление
                    }
                }

                using (MySqlCommand command = connection.CreateCommand())
                {
                    // Запрос на удаление пользователя
                    string deleteQuery = "DELETE FROM Customer WHERE card_account = @cardAccount";
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@cardAccount", cardAccount);

                    // Открываем соединение
                    connection.Open();

                    // Выполняем запрос
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Пользователь с карточным счетом {cardAccount} успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }


        private void DeleteUserWithAssociatedData(string cardAccount)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    // Запрос на удаление пользователя с использованием CASCADE DELETE
                    string deleteQuery = "DELETE FROM Customer WHERE card_account = @cardAccount";
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@cardAccount", cardAccount);

                    // Открываем соединение
                    connection.Open();

                    // Выполняем запрос
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Пользователь с карточным счетом {cardAccount} и все связанные данные успешно удалены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #endregion

        #region GasStation
        
        #region Adding GasStation

        private void add_GasStation_Click(object sender, EventArgs e)
        {
            AddGasStation(GasAddress_textBox.Text, GasFirm_textBox.Text);
        }
        private void AddGasStation(string gasAddress, string gasFirm)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                if (gasAddress == "")
                {
                    MessageBox.Show("Поле адрес не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateAddress(gasAddress))
                {
                    MessageBox.Show("Некорректный формат адреса. Введите корректный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Проверка на уникальность адреса автозаправки
                if (!IsGasAddressUnique(gasAddress))
                {
                    MessageBox.Show("Автозаправка с таким адресом уже существует. Введите уникальный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка наличия фирмы с указанным названием или номером
                int firmId;
                if (int.TryParse(gasFirm, out firmId))
                {
                    // Если введенное значение - число, проверяем, существует ли фирма с таким номером
                    if (!IsFirmExistsById(firmId))
                    {
                        MessageBox.Show("Фирма с указанным номером не существует. Введите существующий номер или название фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (gasFirm == "")
                    {
                        MessageBox.Show("Поле фирма не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Если введенное значение - не число, проверяем, существует ли фирма с таким названием
                    if (!IsFirmExistsByName(gasFirm))
                    {
                        MessageBox.Show("Фирма с указанным названием не существует. Введите существующий номер или название фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Вставка данных об автозаправке
                string insertQuery = "INSERT INTO GasStation (gasStation_address, firm_id) VALUES (@gasAddress, @firmId);";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue("@gasAddress", gasAddress);

                if (int.TryParse(gasFirm, out firmId))
                {
                    command.Parameters.AddWithValue("@firmId", firmId);
                }
                else
                {
                    // Получаем номер фирмы по названию
                    int firmNumber = GetFirmNumberByName(gasFirm);
                    command.Parameters.AddWithValue("@firmId", firmNumber);
                }

                // Открываем соединение
                connection.Open();

                // Выполняем запрос
                command.ExecuteNonQuery();

                // Очищаем текстовые поля после успешного добавления
                GasAddress_textBox.Clear();
                GasFirm_textBox.Clear();

                MessageBox.Show("Автозаправка успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении автозаправки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрываем соединение
                connection?.Close();
            }
        }

        #endregion

        #region Validating DATA
        private bool IsGasAddressUnique(string gasAddress)
        {
            MySqlConnection connection = DBConnection.GetConnection();

            // Открываем соединение
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                // Проверяем уникальность адреса автозаправки
                string query = $"SELECT COUNT(*) FROM GasStation WHERE gasStation_address = '{gasAddress}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0; // Если count равен 0, то новый адрес уникален
            }
            finally
            {
                // Всегда закрываем соединение
                connection.Close();
            }
        }

        private bool IsFirmExistsById(int firmId)
        {
            MySqlConnection connection = DBConnection.GetConnection();

            // Открываем соединение
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                // Проверяем существование фирмы по номеру
                string query = $"SELECT COUNT(*) FROM Firm WHERE firm_id = {firmId}";
                MySqlCommand command = new MySqlCommand(query, connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Если count больше 0, то фирма существует
            }
            finally
            {
                // Всегда закрываем соединение
                connection.Close();
            }
        }

        private bool IsFirmExistsByName(string firmName)
        {
            MySqlConnection connection = DBConnection.GetConnection();

            // Открываем соединение
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                // Проверяем существование фирмы по названию
                string query = $"SELECT COUNT(*) FROM Firm WHERE firm_name = '{firmName}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Если count больше 0, то фирма существует
            }
            finally
            {
                // Всегда закрываем соединение
                connection.Close();
            }
        }

        private int GetFirmNumberByName(string firmName)
        {
            MySqlConnection connection = DBConnection.GetConnection();

            // Открываем соединение
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                // Получаем номер фирмы по названию
                string query = $"SELECT firm_id FROM Firm WHERE firm_name = '{firmName}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            finally
            {
                // Всегда закрываем соединение
                connection.Close();
            }
        }
        #endregion

        #region Updating GasStation
        private string originalGasFirm, originalGasAddress;

        private void dataGridView3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView3.Columns[e.ColumnIndex];

                if (column.Name == "Название фирмы")
                {
                    originalGasFirm = row.Cells[column.Name].Value.ToString();
                }
                if (column.Name == "Адрес автозаправки")
                {
                    originalGasAddress = row.Cells[column.Name].Value.ToString();
                }
            }
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                DataGridViewColumn column = dataGridView3.Columns[e.ColumnIndex];

                if (column.Name == "Название фирмы" || column.Name == "Адрес автозаправки")
                {
                    string newValue = row.Cells[column.Name].Value.ToString();
                    string columnName = (column.Name == "Название фирмы") ? "firm_id" : "gasStation_address";

                    // Если введенное значение - число, то считаем ID фирмы
                    if (columnName == "firm_id" && int.TryParse(newValue, out int firmId))

                    {
                        // Проверяем существование фирмы по ID
                        if (columnName == "firm_id" && !IsFirmExistsById(firmId))
                        {
                            MessageBox.Show("Фирма с указанным номером не существует. Введите существующий номер фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[column.Name].Value = originalGasFirm; // Восстанавливаем предыдущее значение
                            return;
                        }
                        if (int.TryParse(newValue, out int firm))
                        {
                            if (GetFirmNumberByName(originalGasFirm) == firm)
                            {
                                row.Cells[column.Name].Value = originalGasFirm;
                                return;
                            }
                        }
                        BeginInvoke((Action)(() => UpdateGasStationInDatabase(row, columnName, firmId.ToString(), (columnName == "firm_id") ? GetFirmNumberByName(originalGasFirm).ToString() : originalGasAddress)));
                    }
                    else
                    {
                        // Если введенное значение - не число, то считаем, что введено название фирмы
                        if (newValue == "")
                        {
                            MessageBox.Show("Название фирмы не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[column.Name].Value = originalGasFirm; // Восстанавливаем предыдущее значение
                            return;
                        }
                        
                        if (columnName == "firm_id" && !IsFirmExistsByName(newValue))
                        {
                            MessageBox.Show("Фирма с указанным названием не существует. Введите существующий номер фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[column.Name].Value = originalGasFirm; // Восстанавливаем предыдущее значение
                            return;
                        }
                    }
                        // Проверка на корректность формата адреса
                    if (columnName == "gasStation_address" && !ValidateAddress(newValue))
                    {
                        MessageBox.Show("Некорректный формат адреса. Введите корректный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[column.Name].Value = originalGasAddress; // Восстанавливаем предыдущее значение
                        return;
                    }

                    // Проверка на уникальность адреса автозаправки
                    if (columnName == "gasStation_address" && newValue != originalGasAddress && !IsGasAddressUnique(newValue))
                    {
                        MessageBox.Show("Автозаправка с таким адресом уже существует. Введите уникальный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[column.Name].Value = originalGasAddress; // Восстанавливаем предыдущее значение
                        return;
                    }

                    BeginInvoke((Action)(() => UpdateGasStationInDatabase(row, columnName, newValue, (columnName == "firm_id") ? GetFirmNumberByName(originalGasFirm).ToString() : originalGasAddress)));
                    
                }
            }
        }

        private void UpdateGasStationInDatabase(DataGridViewRow row, string columnName, string updatedValue, string currentColumnValue)
        {
            MySqlConnection connection = null;
            try
            {
                if (updatedValue == currentColumnValue)
                {
                    return; 
                }
                
                connection = DBConnection.GetConnection();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    string updateQuery = $"UPDATE GasStation SET {columnName} = @updatedValue WHERE gasStation_id = @gasStationId;";
                    command.CommandText = updateQuery;

                    // Добавляем параметры
                    command.Parameters.AddWithValue("@gasStationId", row.Cells["Код автозаправки"].Value.ToString());

                    // Если обновляем "Название фирмы", проверяем существование фирмы
                    if (columnName == "firm_id")
                    {
                        // Проверяем, является ли введенное значение числом
                        if (int.TryParse(updatedValue, out int firmId))
                        {
                            // Если это число, то считаем ID фирмы

                            // Проверяем существование фирмы по ID
                            if (!IsFirmExistsById(firmId))
                            {
                                MessageBox.Show("Фирма с указанным номером не существует. Введите существующий номер фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                                return;
                            }
                            command.Parameters.AddWithValue("@updatedValue", firmId);
                        }
                        else
                        {
                            if (updatedValue == "")
                            {
                                MessageBox.Show("Название фирмы не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                                return;
                            }

                            firmId = GetFirmNumberByName(updatedValue);

                            // Check if the firm exists by name
                            if (firmId == -1)
                            {
                                MessageBox.Show("Фирма с указанным названием не существует. Введите существующее название фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                                return;
                            }

                            // Use the obtained firm_id for the database update
                            // Проверяем существование фирмы по названию
                            if (!IsFirmExistsByName(updatedValue))
                            {
                                MessageBox.Show("Фирма с указанным названием не существует. Введите существующее название фирмы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                                return;
                            }
                            command.Parameters.AddWithValue("@updatedValue", firmId);

                        }
                    }
                    else if (columnName == "gasStation_address")
                    {
                        // Если обновляем "Адрес автозаправки", проверяем корректность адреса
                        if (!ValidateAddress(updatedValue))
                        {
                            MessageBox.Show("Некорректный формат адреса. Введите корректный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                            return;
                        }
                        // Проверяем уникальность адреса автозаправки
                        if (updatedValue != currentColumnValue && !IsGasAddressUnique(updatedValue))
                        {
                            MessageBox.Show("Автозаправка с таким адресом уже существует. Введите уникальный адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[columnName].Value = currentColumnValue; // Восстанавливаем предыдущее значение
                            return;
                        }
                        command.Parameters.AddWithValue("@updatedValue", $"{updatedValue}");
                    }
                    // Выполняем запрос
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close(); // Закрыть соединение в блоке finally
            }
        }

    


        #endregion

        #region Deleting GasStation
        private void delete_Gasstation_Click(object sender, EventArgs e)
        {
            // Получение выбранной строки в DataGridView
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Получение значения карточного счета выбранного пользователя
                string gasID = selectedRow.Cells["Код автозаправки"].Value.ToString();

                // Проверка связанных данных
                bool hasAssociatedDataInGasStation = CheckAssociatedData("fuelsale", "gaSstation_id", gasID);

                // Подтверждение удаления
                DialogResult result;
                if (hasAssociatedDataInGasStation)
                {
                    result = MessageBox.Show("Автозаправка связана с другими данными. Вы уверены, что хотите удалить автозаправку со всеми связанными данными?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                else
                {
                    result = MessageBox.Show("Вы уверены, что хотите удалить автозаправку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (result == DialogResult.Yes)
                {
                    // Удаление пользователя
                    if (hasAssociatedDataInGasStation)
                    {
                        // Используйте CASCADE DELETE, если есть связанные данные
                        DeleteGasStationWithAssociatedData(gasID);
                    }
                    else
                    {
                        // Просто удалите пользователя
                        DeleteGasStation(gasID);
                    }

                    RefreshAllGrids();
                }
            }
            else
            {
                MessageBox.Show("Выберите автозаправку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteGasStation(string gasID)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                bool hasAssociatedDataInGasStation = CheckAssociatedData("FuelSale", "gasStation_id", gasID);

                // Проверка связанных данных в таблице FuelSale
                if (hasAssociatedDataInGasStation)
                {
                    // Если есть связанные данные, запрос подтверждения удаления
                    DialogResult result = MessageBox.Show("Автозаправка связана с продажами топлива. Вы уверены, что хотите удалить автозаправку со всеми связанными данными?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return; // Пользователь отменил удаление
                    }
                }

                using (MySqlCommand command = connection.CreateCommand())
                {
                    // Запрос на удаление автозаправки
                    string deleteQuery = "DELETE FROM GasStation WHERE gasStation_id = @gasID";
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@gasID", gasID);

                    // Открываем соединение
                    connection.Open();

                    // Выполняем запрос
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Автозаправка с кодом {gasID} успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении автозаправки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void DeleteGasStationWithAssociatedData(string gasID)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    // Запрос на удаление пользователя с использованием CASCADE DELETE
                    string deleteQuery = "DELETE FROM gasstation WHERE gasstation_id = @gasID";
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@gasID", gasID);

                    // Открываем соединение
                    connection.Open();

                    // Выполняем запрос
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Автозаправка с кодом {gasID} и все связанные данные успешно удалены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении автозаправки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #endregion

        #region FuelSale 

        #region Adding Sale
        private void add_Sale_Click(object sender, EventArgs e)
        {
            try
            {
                // Получение данных из формы
                string saleTime = saleTime_textbox.Text;

                // Check if saleSTCode_textbox is empty
                if (string.IsNullOrWhiteSpace(saleSTCode_textbox.Text))
                {
                    MessageBox.Show("Введите код заправки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Attempt to parse saleSTCode_textbox.Text to integer
                if (!int.TryParse(saleSTCode_textbox.Text, out int saleSTcode))
                {
                    MessageBox.Show("Некорректное значение для кода заправки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string fuelType = fuelType_combobox.SelectedItem?.ToString();

                // Check if fuelType is not selected
                if (string.IsNullOrWhiteSpace(fuelType))
                {
                    MessageBox.Show("Выберите вид топлива.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Attempt to parse saleCard_textbox.Text to integer
                if (!int.TryParse(saleCard_textbox.Text, out int saleCard))
                {
                    MessageBox.Show("Некорректное значение для карточного счета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Attempt to parse saleQuantity_textbox.Text to decimal
                decimal saleQuantity = 0;
                try
                {
                    if (!decimal.TryParse(saleQuantity_textbox.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,out saleQuantity))
                    {
                        MessageBox.Show("Некорректное значение для количества продажи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (saleQuantity <= 0)
                        {
                            MessageBox.Show("Нельзя создать продажу меньше чем на 0 литров топлива!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }
                catch (FormatException)
                {
                    MessageBox.Show("Некорректный формат ввода для количества продажи. Используйте запятую вместо точки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                // Check if any field is empty
                if (string.IsNullOrWhiteSpace(saleSTCode_textbox.Text) ||
                    string.IsNullOrWhiteSpace(fuelType) || string.IsNullOrWhiteSpace(saleCard_textbox.Text) ||
                    string.IsNullOrWhiteSpace(saleQuantity_textbox.Text))
                {
                    MessageBox.Show("Заполните все обязательные поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (saleTime != "")
                {
                    // Проверка времени продажи с использованием регулярного выражения
                    if (!Regex.IsMatch(saleTime, @"^\d{2}\.\d{2}.\d{4} \d{2}:\d{2}$"))
                    {
                        MessageBox.Show("Некорректный формат времени продажи. Используйте формат: ДД.ММ.ГГГГ ЧЧ:ММ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
               
                // Проверка существования карточного счета в таблице Customer
                if (!CheckIfExistsInTable("Customer", "card_account", saleCard))
                {
                    MessageBox.Show($"Карточного счета {saleCard} не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проверка существования кода заправки в таблице GasStation
                if (!CheckIfExistsInTable("GasStation", "gasstation_id", saleSTcode))
                {
                    MessageBox.Show($"Заправки с кодом {saleSTcode} не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проверка соответствия вида топлива и поставщика
                if (!CheckFuelSupplier(saleSTcode, fuelType))
                {
                    MessageBox.Show($"Топливо {fuelType} не предоставляется поставщиком заправки с кодом {saleSTcode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Добавление данных о продаже в таблицу FuelSale
                AddSaleToTable(saleTime, saleCard, saleSTcode, fuelType, saleQuantity);
                MessageBox.Show("Данные о продаже успешно добавлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении данных о продаже: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool CheckIfExistsInTable(string tableName, string columnName, int value)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = {value}";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке существования записи в таблице {tableName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        private bool CheckFuelSupplier(int gasStationId, string fuelType)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();
                string query = @"
            SELECT F.fuel_id
            FROM Fuel F
            JOIN GasStation G ON F.firm_id = G.firm_id 
            WHERE G.gasStation_id = @gasStationId AND F.fuel_type = @fuelType
            LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@gasStationId", gasStationId);
                    command.Parameters.AddWithValue("@fuelType", fuelType);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке поставщика топлива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        private void AddSaleToTable(string saleTime, int saleCard, int saleSTcode, string fuelType, decimal saleQuantity)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                // Check if fuel exists for the gas station and fuel type
                if (!CheckFuelSupplier(saleSTcode, fuelType))
                {
                    MessageBox.Show($"Топливо {fuelType} не предоставляется поставщиком заправки с кодом {saleSTcode}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // If saleTime is empty, use the default value in the query
                string insertQuery = string.IsNullOrWhiteSpace(saleTime)
                    ? @"
                INSERT INTO FuelSale (card_account, gasStation_id, fuel_id, quantity)
                SELECT @saleCard, @saleSTcode, F.fuel_id, @saleQuantity
                FROM Fuel F
                JOIN GasStation G ON F.firm_id = G.firm_id 
                WHERE G.gasStation_id = @saleSTcode AND F.fuel_type = @fuelType"
                    : @"
                INSERT INTO FuelSale (sale_date, card_account, gasStation_id, fuel_id, quantity)
                SELECT @saleTime, @saleCard, @saleSTcode, F.fuel_id, @saleQuantity
                FROM Fuel F
                JOIN GasStation G ON F.firm_id = G.firm_id 
                WHERE G.gasStation_id = @saleSTcode AND F.fuel_type = @fuelType";


                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    if (!string.IsNullOrWhiteSpace(saleTime))
                    {
                        command.Parameters.AddWithValue("@saleTime", DateTime.ParseExact(saleTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture));
                    }

                    command.Parameters.AddWithValue("@saleCard", saleCard);
                    command.Parameters.AddWithValue("@saleSTcode", saleSTcode);
                    command.Parameters.AddWithValue("@fuelType", fuelType);
                    command.Parameters.AddWithValue("@saleQuantity", saleQuantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении данных о продаже: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #region Deliting Sale
        private void delete_Sale_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure at least one row is selected in the DataGridView
                if (dataGridView4.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите запись для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get selected sale details from the selected row in the DataGridView
                int saleId = Convert.ToInt32(dataGridView4.SelectedRows[0].Cells["ID"].Value);

                // Confirm deletion with the user
                string confirmationMessage = $"Вы уверены, что хотите удалить продажу с ID {saleId}?";
                DialogResult confirmationResult = MessageBox.Show(confirmationMessage, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmationResult == DialogResult.Yes)
                {
                    // Proceed with deletion
                    DeleteSaleFromTable(saleId);
                    MessageBox.Show("Продажа успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAllGrids();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении продажи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSaleFromTable(int saleId)
        {
            MySqlConnection connection = null;
            try
            {
                connection = DBConnection.GetConnection();

                // Delete the sale record based on sale_id
                string deleteQuery = "DELETE FROM FuelSale WHERE sale_id = @saleId";

                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@saleId", saleId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении продажи из таблицы FuelSale: {ex.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #endregion

    }
}
