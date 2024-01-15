namespace DataBase
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.delete_User = new System.Windows.Forms.Button();
            this.add_User = new System.Windows.Forms.Button();
            this.number_textBox = new System.Windows.Forms.TextBox();
            this.address_textBox = new System.Windows.Forms.TextBox();
            this.fullname_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GasFirm_textBox = new System.Windows.Forms.TextBox();
            this.GasAddress_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.delete_GasStation = new System.Windows.Forms.Button();
            this.add_GasStation = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.saleQuantity_textbox = new System.Windows.Forms.TextBox();
            this.fuelType_combobox = new System.Windows.Forms.ComboBox();
            this.saleSTCode_textbox = new System.Windows.Forms.TextBox();
            this.saleCard_textbox = new System.Windows.Forms.TextBox();
            this.saleTime_textbox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.delete_Sale = new System.Windows.Forms.Button();
            this.add_Sale = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.back_adm_Button = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Controls.Add(this.tabPage3);
            this.tab.Controls.Add(this.tabPage4);
            this.tab.Location = new System.Drawing.Point(12, 12);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(776, 426);
            this.tab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.delete_User);
            this.tabPage1.Controls.Add(this.add_User);
            this.tabPage1.Controls.Add(this.number_textBox);
            this.tabPage1.Controls.Add(this.address_textBox);
            this.tabPage1.Controls.Add(this.fullname_textBox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Клиенты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // delete_User
            // 
            this.delete_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_User.Location = new System.Drawing.Point(587, 337);
            this.delete_User.Name = "delete_User";
            this.delete_User.Size = new System.Drawing.Size(153, 39);
            this.delete_User.TabIndex = 8;
            this.delete_User.Text = "Удалить";
            this.delete_User.UseVisualStyleBackColor = true;
            this.delete_User.Click += new System.EventHandler(this.delete_User_Click);
            // 
            // add_User
            // 
            this.add_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_User.Location = new System.Drawing.Point(587, 285);
            this.add_User.Name = "add_User";
            this.add_User.Size = new System.Drawing.Size(153, 39);
            this.add_User.TabIndex = 7;
            this.add_User.Text = "Добавить";
            this.add_User.UseVisualStyleBackColor = true;
            this.add_User.Click += new System.EventHandler(this.add_User_Click);
            // 
            // number_textBox
            // 
            this.number_textBox.Location = new System.Drawing.Point(245, 348);
            this.number_textBox.Name = "number_textBox";
            this.number_textBox.Size = new System.Drawing.Size(296, 20);
            this.number_textBox.TabIndex = 6;
            // 
            // address_textBox
            // 
            this.address_textBox.Location = new System.Drawing.Point(246, 314);
            this.address_textBox.Name = "address_textBox";
            this.address_textBox.Size = new System.Drawing.Size(296, 20);
            this.address_textBox.TabIndex = 5;
            // 
            // fullname_textBox
            // 
            this.fullname_textBox.Location = new System.Drawing.Point(246, 285);
            this.fullname_textBox.Name = "fullname_textBox";
            this.fullname_textBox.Size = new System.Drawing.Size(296, 20);
            this.fullname_textBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(59, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Адрес";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(59, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Номер телефона";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(59, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ФИО";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(241, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Информация о клиентах";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 87);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(756, 182);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GasFirm_textBox);
            this.tabPage2.Controls.Add(this.GasAddress_textBox);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.delete_GasStation);
            this.tabPage2.Controls.Add(this.add_GasStation);
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Автозаправки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GasFirm_textBox
            // 
            this.GasFirm_textBox.Location = new System.Drawing.Point(241, 344);
            this.GasFirm_textBox.Name = "GasFirm_textBox";
            this.GasFirm_textBox.Size = new System.Drawing.Size(296, 20);
            this.GasFirm_textBox.TabIndex = 13;
            // 
            // GasAddress_textBox
            // 
            this.GasAddress_textBox.Location = new System.Drawing.Point(241, 297);
            this.GasAddress_textBox.Name = "GasAddress_textBox";
            this.GasAddress_textBox.Size = new System.Drawing.Size(296, 20);
            this.GasAddress_textBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(48, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Фирма-поставщик";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(48, 297);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Адрес";
            // 
            // delete_GasStation
            // 
            this.delete_GasStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_GasStation.Location = new System.Drawing.Point(583, 344);
            this.delete_GasStation.Name = "delete_GasStation";
            this.delete_GasStation.Size = new System.Drawing.Size(153, 39);
            this.delete_GasStation.TabIndex = 9;
            this.delete_GasStation.Text = "Удалить";
            this.delete_GasStation.UseVisualStyleBackColor = true;
            this.delete_GasStation.Click += new System.EventHandler(this.delete_Gasstation_Click);
            // 
            // add_GasStation
            // 
            this.add_GasStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_GasStation.Location = new System.Drawing.Point(583, 286);
            this.add_GasStation.Name = "add_GasStation";
            this.add_GasStation.Size = new System.Drawing.Size(153, 39);
            this.add_GasStation.TabIndex = 8;
            this.add_GasStation.Text = "Добавить";
            this.add_GasStation.UseVisualStyleBackColor = true;
            this.add_GasStation.Click += new System.EventHandler(this.add_GasStation_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(9, 79);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(756, 191);
            this.dataGridView3.TabIndex = 3;
            this.dataGridView3.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView3_CellBeginEdit);
            this.dataGridView3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellEndEdit);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(179, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(376, 29);
            this.label6.TabIndex = 2;
            this.label6.Text = "Информация об автозаправках";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.saleQuantity_textbox);
            this.tabPage3.Controls.Add(this.fuelType_combobox);
            this.tabPage3.Controls.Add(this.saleSTCode_textbox);
            this.tabPage3.Controls.Add(this.saleCard_textbox);
            this.tabPage3.Controls.Add(this.saleTime_textbox);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.delete_Sale);
            this.tabPage3.Controls.Add(this.add_Sale);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 400);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Продажи";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // saleQuantity_textbox
            // 
            this.saleQuantity_textbox.Location = new System.Drawing.Point(422, 339);
            this.saleQuantity_textbox.Name = "saleQuantity_textbox";
            this.saleQuantity_textbox.Size = new System.Drawing.Size(128, 20);
            this.saleQuantity_textbox.TabIndex = 21;
            // 
            // fuelType_combobox
            // 
            this.fuelType_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fuelType_combobox.FormattingEnabled = true;
            this.fuelType_combobox.Items.AddRange(new object[] {
            "АИ-76",
            "АИ-92",
            "АИ-95",
            "АИ-96",
            "Газ",
            "Дизель"});
            this.fuelType_combobox.Location = new System.Drawing.Point(439, 272);
            this.fuelType_combobox.Name = "fuelType_combobox";
            this.fuelType_combobox.Size = new System.Drawing.Size(111, 21);
            this.fuelType_combobox.TabIndex = 20;
            // 
            // saleSTCode_textbox
            // 
            this.saleSTCode_textbox.Location = new System.Drawing.Point(154, 341);
            this.saleSTCode_textbox.Name = "saleSTCode_textbox";
            this.saleSTCode_textbox.Size = new System.Drawing.Size(128, 20);
            this.saleSTCode_textbox.TabIndex = 19;
            // 
            // saleCard_textbox
            // 
            this.saleCard_textbox.Location = new System.Drawing.Point(154, 306);
            this.saleCard_textbox.Name = "saleCard_textbox";
            this.saleCard_textbox.Size = new System.Drawing.Size(128, 20);
            this.saleCard_textbox.TabIndex = 18;
            // 
            // saleTime_textbox
            // 
            this.saleTime_textbox.Location = new System.Drawing.Point(154, 274);
            this.saleTime_textbox.Name = "saleTime_textbox";
            this.saleTime_textbox.Size = new System.Drawing.Size(128, 20);
            this.saleTime_textbox.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(302, 339);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 20);
            this.label13.TabIndex = 16;
            this.label13.Text = "Объём";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(302, 272);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 20);
            this.label14.TabIndex = 15;
            this.label14.Text = "Вид топлива";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(6, 339);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "Код заправки";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(6, 306);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 20);
            this.label11.TabIndex = 12;
            this.label11.Text = "Карточный счёт";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(6, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "Время продажи";
            // 
            // delete_Sale
            // 
            this.delete_Sale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_Sale.Location = new System.Drawing.Point(585, 328);
            this.delete_Sale.Name = "delete_Sale";
            this.delete_Sale.Size = new System.Drawing.Size(153, 39);
            this.delete_Sale.TabIndex = 10;
            this.delete_Sale.Text = "Удалить";
            this.delete_Sale.UseVisualStyleBackColor = true;
            this.delete_Sale.Click += new System.EventHandler(this.delete_Sale_Click);
            // 
            // add_Sale
            // 
            this.add_Sale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_Sale.Location = new System.Drawing.Point(585, 272);
            this.add_Sale.Name = "add_Sale";
            this.add_Sale.Size = new System.Drawing.Size(153, 39);
            this.add_Sale.TabIndex = 9;
            this.add_Sale.Text = "Добавить";
            this.add_Sale.UseVisualStyleBackColor = true;
            this.add_Sale.Click += new System.EventHandler(this.add_Sale_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(187, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(305, 29);
            this.label9.TabIndex = 5;
            this.label9.Text = "Информация о продажах";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView4.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(6, 61);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.Size = new System.Drawing.Size(756, 191);
            this.dataGridView4.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView2);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 400);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Фирмы";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 69);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 60;
            this.dataGridView2.Size = new System.Drawing.Size(756, 182);
            this.dataGridView2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(231, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(282, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Информация о фирмах";
            // 
            // back_adm_Button
            // 
            this.back_adm_Button.Location = new System.Drawing.Point(702, 10);
            this.back_adm_Button.Name = "back_adm_Button";
            this.back_adm_Button.Size = new System.Drawing.Size(85, 24);
            this.back_adm_Button.TabIndex = 1;
            this.back_adm_Button.Text = "Назад";
            this.back_adm_Button.UseVisualStyleBackColor = true;
            this.back_adm_Button.Click += new System.EventHandler(this.back_adm_Button_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.back_adm_Button);
            this.Controls.Add(this.tab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автозаправки. Меню администратора";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button add_User;
        private System.Windows.Forms.TextBox number_textBox;
        private System.Windows.Forms.TextBox address_textBox;
        private System.Windows.Forms.TextBox fullname_textBox;
        private System.Windows.Forms.Button delete_User;
        private System.Windows.Forms.Button back_adm_Button;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button add_GasStation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button delete_GasStation;
        private System.Windows.Forms.TextBox GasFirm_textBox;
        private System.Windows.Forms.TextBox GasAddress_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button delete_Sale;
        private System.Windows.Forms.Button add_Sale;
        private System.Windows.Forms.TextBox saleQuantity_textbox;
        private System.Windows.Forms.ComboBox fuelType_combobox;
        private System.Windows.Forms.TextBox saleSTCode_textbox;
        private System.Windows.Forms.TextBox saleCard_textbox;
        private System.Windows.Forms.TextBox saleTime_textbox;
    }
}