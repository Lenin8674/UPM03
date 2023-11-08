using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ForSanya2
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sqliteConnection;

        public Form1()
        {
            InitializeComponent();

            sqliteConnection = new SQLiteConnection("Data Source=Database.db;Version=3;");
            sqliteConnection.Open();
            CreateTablesAndInsertDataIfNeeded();
            LoadPortsIntoComboBox(); 
            LoadComboBoxes();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqliteConnection != null)
            {
                sqliteConnection.Close();
            }
        }

        #region Load

        private void CreateTablesAndInsertDataIfNeeded()
        {
            // Проверяем, существуют ли таблицы, и создаем их при необходимости
            CreateTablesIfNotExists();

            // Проверяем существование данных в таблицах "Ships," "Ports," и "Combo"
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Ships;", sqliteConnection))
            {
                int shipCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (shipCount == 0)
                {
                    // Таблица "Ships" пуста, вставляем примерные данные
                    InsertSampleData();
                }
            }

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Ports;", sqliteConnection))
            {
                int portCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (portCount == 0)
                {
                    // Таблица "Ports" пуста, вставляем примерные данные
                    InsertSampleData();
                }
            }

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Data;", sqliteConnection))
            {
                int comboCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (comboCount == 0)
                {
                    // Таблица "Combo" пуста, вставляем примерные данные
                    InsertSampleData();
                }
            }
        }

        private void CreateTablesIfNotExists()
        {
            using (SQLiteCommand cmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", sqliteConnection))
            {
                cmd.ExecuteNonQuery();
            }

            // Проверяем существование таблицы "Ships"
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Ships';", sqliteConnection))
            {
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    // Таблица "Ships" не существует, создаем её
                    using (SQLiteCommand createCmd = new SQLiteCommand("CREATE TABLE Ships (ID INTEGER PRIMARY KEY AUTOINCREMENT, ShipName TEXT, Displacement REAL, HomePort INTEGER, Captain TEXT, FOREIGN KEY (HomePort) REFERENCES Ports (ID) ON DELETE CASCADE);", sqliteConnection))
                    {
                        createCmd.ExecuteNonQuery();
                    }
                }
            }

            // Проверяем существование таблицы "Ports"
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Ports';", sqliteConnection))
            {
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    // Таблица "Ports" не существует, создаем её
                    using (SQLiteCommand createCmd = new SQLiteCommand("CREATE TABLE Ports (ID INTEGER PRIMARY KEY AUTOINCREMENT, PortName TEXT, Country TEXT, Category TEXT);", sqliteConnection))
                    {
                        createCmd.ExecuteNonQuery();
                    }
                }
            }

            // Проверяем существование таблицы "Combo"
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Data';", sqliteConnection))
            {
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    // Таблица "Combo" не существует, создаем её
                    using (SQLiteCommand createCmd = new SQLiteCommand("CREATE TABLE Data (ID INTEGER PRIMARY KEY AUTOINCREMENT, ShipID INTEGER, PortID INTEGER, VisitDate DATE, DepartureDate DATE, DockNumber INTEGER, PurposeOfVisit TEXT, FOREIGN KEY (ShipID) REFERENCES Ships (ID) ON DELETE CASCADE, FOREIGN KEY (PortID) REFERENCES Ports (ID) ON DELETE CASCADE);", sqliteConnection))
                    {
                        createCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertSampleData()
        {
            using (SQLiteTransaction transaction = sqliteConnection.BeginTransaction())
            {
                // Добавляем данные в таблицу "Ports" с указанием ID для каждого порта
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Ports " +
                    "(PortName, City, Category) " +
                    "VALUES " +
                    "('Мурманск', 'Мурманск', 'Ледокол'), " +
                    "('Новоросийск', 'Новоросийск', 'Грузовое судно'), " +
                    "('Махачкала', 'Махачкала', 'Рыболовное судно'), " +
                    "('Калининград', 'Калининград', 'Грузовое судно'), " +
                    "('Тикси', 'Тикси', 'Ледокол');"+
                    "('Ванино', 'Ванино', 'Рыболовное судно');", sqliteConnection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Добавляем данные в таблицу "Ships" с ссылками на порты (PortID)
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Ships " +
                    "(ShipName, Displacement, HomePort, Captain) " +
                    "VALUES " +
                    "('Аврора', 1000.0, 1, 'Капитан Павлов'), " +
                    "('Север', 1500.0, 2, 'Капитан Антонов'), " +
                    "('Вавилон', 1200.0, 4, 'Капитан Шутов'), " +
                    "('Жаворонок', 1800.0, 3, 'Капитан Суворов'), " +
                    "('Дулисов', 900.0, 6, 'Капитан Мореходов');" +
                    "('Венера', 1300.0, 2, 'Капитан Романов');", sqliteConnection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Добавляем данные в таблицу "Data" с использованием корректных ShipID и PortID
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Data " +
                    "(ShipID, PortID, VisitDate, DepartureDate, DockNumber, PurposeOfVisit) " +
                    "VALUES " +
                    "(1, 1, '2023-12-15', '2024-01-05', 2, 'Поломка судна'), " +
                    "(2, 3, '2023-09-15', '2023-09-20', 1, 'Выгрузка товара'), " +
                    "(3, 2, '2023-11-10', '2023-11-12', 3, 'Дозаправка'), " +
                    "(4, 4, '2023-12-05', '2023-12-10', 4, 'Поломка судна'), " +
                    "(5, 5, '2023-08-01', '2023-08-03', 5, 'Загрузка товара');"+
                    "(4, 1, '2023-07-02', '2023-07-10', 3, 'Поломка судна');"+
                    "(1, 4, '2022-12-05', '2022-12-05', 6, 'Выгрузка товара');"+
                    "(2, 2, '2023-01-01', '2023-01-03', 3, 'Загрузка товара');", sqliteConnection))
                {
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        private void CreateAndShowContextMenu(TextBox textBox, Button button, string tableName, string columnName)
        {
            // Создайте новое контекстное меню
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            using (SQLiteConnection connection = new SQLiteConnection(sqliteConnection))
            {

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT {columnName} FROM {tableName}";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cellValue = reader[columnName].ToString();
                            ToolStripMenuItem item = new ToolStripMenuItem(cellValue);

                            // Обработчик события для выбора значения
                            item.Click += (sender, e) =>
                            {
                                // Записать выбранное значение в TextBox
                                textBox.Text = cellValue;
                            };

                            contextMenuStrip.Items.Add(item);
                        }
                    }
                }
            }

            // Отобразите контекстное меню
            contextMenuStrip.Show(button, new Point(0, button.Height));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CreateAndShowContextMenu(textBox5, button5, "sqlite_sequence", "name");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string tableName = textBox5.Text;

            // Проверяем, существует ли указанная таблица
            if (TableExistsMenu(tableName))
            {
                LoadDataToDataGridView(tableName);

                // Растянуть столбцы равномерно
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            else
            {
                // Если таблица не существует, выводим сообщение об ошибке
                MessageBox.Show("Указанная таблица не существует.");
            }
        }

        private bool TableExistsMenu(string tableName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(sqliteConnection))
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["name"].ToString() == tableName)
                            {
                                return true; // Таблица существует
                            }
                        }
                    }
                }
            }
            return false; // Таблица не существует
        }

        private void LoadDataToDataGridView(string tableName)
        {
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", sqliteConnection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
        }
        #endregion

        #region Zaprosi
        private void LoadPortsIntoComboBox()
        {
            // Получаем текущую дату
            DateTime currentDate = DateTime.Now;

            // Создаем объект для хранения сезонов
            Season[] seasons = new Season[]
            {
                new Season("Весна 2021", new DateTime(2021, 3, 20), new DateTime(2021, 6, 20)),
                new Season("Лето 2021", new DateTime(2021, 6, 21), new DateTime(2021, 9, 22)),
                new Season("Осень 2021", new DateTime(2021, 9, 23), new DateTime(2021, 12, 20)),
                new Season("Зима 2021-2022", new DateTime(2021, 12, 21), new DateTime(2022, 3, 19)),
                new Season("Весна 2022", new DateTime(2022, 3, 20), new DateTime(2022, 6, 20)),
                new Season("Лето 2022", new DateTime(2022, 6, 21), new DateTime(2022, 9, 22)),
                new Season("Осень 2022", new DateTime(2022, 9, 23), new DateTime(2022, 12, 20)),
                new Season("Зима 2022-2023", new DateTime(2022, 12, 21), new DateTime(2023, 3, 19)),
                new Season("Весна 2023", new DateTime(2023, 3, 20), new DateTime(2023, 6, 20)),
                new Season("Лето 2023", new DateTime(2023, 6, 21), new DateTime(2023, 9, 22)),
                new Season("Осень 2023", new DateTime(2023, 9, 23), new DateTime(2023, 12, 20)),
                new Season("Зима 2023-2024", new DateTime(2023, 12, 21), new DateTime(2024, 3, 19)),
            };

            // Заполняем ComboBox сезонами
            comboBox1.Items.AddRange(seasons);

            // Выбираем текущий сезон
            for (int i = 0; i < seasons.Length; i++)
            {
                if (currentDate >= seasons[i].StartDate && currentDate <= seasons[i].EndDate)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
        }

        private void LoadComboBoxes()
        {
            LoadComboBoxItems("SELECT DISTINCT PortName FROM Ports", comboBox2);
            LoadComboBoxItems("SELECT DISTINCT ShipName FROM Ships", comboBox3);
            LoadComboBoxItems("SELECT DISTINCT PortName FROM Ports", comboBox4);
            LoadComboBoxItems("SELECT DISTINCT PurposeOfVisit FROM Data", comboBox5);
            LoadComboBoxItems("SELECT DISTINCT PurposeOfVisit FROM Data", comboBox6);
            LoadComboBoxItems("SELECT DISTINCT PortName FROM Ports", comboBox8);
        }

        private void LoadComboBoxItems(string query, ComboBox comboBox)
        {
            comboBox.Items.Clear(); // Очищаем список перед заполнением
            HashSet<string> uniqueItems = new HashSet<string>();

            using (SQLiteCommand cmd = new SQLiteCommand(query, sqliteConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = reader[0].ToString();
                        uniqueItems.Add(item);
                    }
                }
            }

            comboBox.Items.AddRange(uniqueItems.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); // Очистите richTextBox1 перед отображением новых данных.

            if (comboBox1.SelectedItem is Season selectedSeason && comboBox2.SelectedItem is string selectedPort)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(
                    "SELECT Ships.ShipName FROM Ships " +
                    "INNER JOIN Data ON Ships.ID = Data.ShipID " +
                    "INNER JOIN Ports ON Data.PortID = Ports.ID " +
                    "WHERE Ports.PortName = @PortName " +
                    "AND (strftime('%Y-%m', Data.VisitDate) = @StartDate OR strftime('%Y-%m', Data.DepartureDate) = @EndDate);", sqliteConnection))
                {
                    cmd.Parameters.AddWithValue("@PortName", selectedPort);
                    cmd.Parameters.AddWithValue("@StartDate", selectedSeason.StartDate.ToString("yyyy-MM"));
                    cmd.Parameters.AddWithValue("@EndDate", selectedSeason.EndDate.ToString("yyyy-MM"));

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        bool found = false;
                        while (reader.Read())
                        {
                            string shipName = reader["ShipName"].ToString();
                            richTextBox1.AppendText(shipName + Environment.NewLine);
                            found = true;
                        }

                        if (!found)
                        {
                            richTextBox1.AppendText("Значений нет.");
                        }
                    }
                }
            }
            else
            {
                richTextBox1.AppendText("Выберите порт.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedShip = comboBox3.Text;
            string selectedPort = comboBox4.Text;
            string selectedPurpose = comboBox5.Text;

            // SQL-запрос для выбора только даты посещения порта и других данных
            string sqlQuery = "SELECT Ships.ShipName, Ports.PortName, Data.PurposeOfVisit, DATE(VisitDate) AS VisitDate " +
                              "FROM Data " +
                              "JOIN Ships ON Data.ShipID = Ships.ID " +
                              "JOIN Ports ON Data.PortID = Ports.ID " +
                              "WHERE Ships.ShipName = @ShipName " +
                              "AND Ports.PortName = @PortName " +
                              "AND Data.PurposeOfVisit = @Purpose";

            using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, sqliteConnection))
            {
                cmd.Parameters.AddWithValue("@ShipName", selectedShip);
                cmd.Parameters.AddWithValue("@PortName", selectedPort);
                cmd.Parameters.AddWithValue("@Purpose", selectedPurpose);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    richTextBox1.Clear(); // Очистить содержимое RichTextBox

                    while (reader.Read())
                    {
                        string shipName = reader["ShipName"].ToString();
                        string portName = reader["PortName"].ToString();
                        string purpose = reader["PurposeOfVisit"].ToString();
                        string visitDate = reader["VisitDate"].ToString();

                        string formattedString = $"Корабль `{shipName}` посещал порт `{portName}` по причине `{purpose}` - {visitDate}";
                        richTextBox1.AppendText(formattedString + Environment.NewLine);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string purposeOfVisit = comboBox6.Text;

            // Получаем список всех стран из таблицы "Ports"
            string allCityQuery = "SELECT DISTINCT City FROM Ports";
            List<string> allCity = new List<string>();

            using (SQLiteCommand allCityCmd = new SQLiteCommand(allCityQuery, sqliteConnection))
            using (SQLiteDataReader allCityReader = allCityCmd.ExecuteReader())
            {
                while (allCityReader.Read())
                {
                    allCity.Add(allCityReader["City"].ToString());
                }
            }

            // Получаем страны, в которые приходили корабли с заданной целью
            string visitedCityQuery = "SELECT DISTINCT P.City FROM Ports P " +
                                            "INNER JOIN Data C ON P.ID = C.PortID " +
                                            "WHERE C.PurposeOfVisit = @PurposeOfVisit";

            List<string> visitedCity = new List<string>();

            using (SQLiteCommand visitedCityCmd = new SQLiteCommand(visitedCityQuery, sqliteConnection))
            {
                visitedCityCmd.Parameters.AddWithValue("@PurposeOfVisit", purposeOfVisit);

                using (SQLiteDataReader visitedCityReader = visitedCityCmd.ExecuteReader())
                {
                    while (visitedCityReader.Read())
                    {
                        visitedCity.Add(visitedCityReader["City"].ToString());
                    }
                }
            }

            // Находим страны, в которые не приходили корабли с заданной целью
            List<string> unvisitedCity = allCity.Except(visitedCity).ToList();

            // Очищаем richTextBox1 перед выводом результатов
            richTextBox1.Clear();

            // Выводим страны, в которые не приходили корабли с заданной целью
            foreach (string city in unvisitedCity)
            {
                richTextBox1.AppendText(city + Environment.NewLine);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedPort = comboBox8.Text;

            string query = "SELECT C.PurposeOfVisit, COUNT(C.PurposeOfVisit) AS VisitCity " +
                            "FROM Ports P " +
                            "INNER JOIN Data C ON P.ID = C.PortID " +
                            "WHERE P.PortName = @SelectedPort " +
                            "GROUP BY C.PurposeOfVisit " +
                            "ORDER BY VisitCity DESC " +
                            "LIMIT 1";

            using (SQLiteCommand cmd = new SQLiteCommand(query, sqliteConnection))
            {
                cmd.Parameters.AddWithValue("@SelectedPort", selectedPort);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string purposeOfVisit = reader["PurposeOfVisit"].ToString();
                        int visitCity = Convert.ToInt32(reader["VisitCity"]);

                        string output = $"Чаще всего в порту '{selectedPort}' корабли заходят по цели '{purposeOfVisit}' ({visitCity} раз)";
                        richTextBox1.Text = output;
                    }
                    else
                    {
                        richTextBox1.Text = $"Для порта '{selectedPort}' нет информации о целях посещения кораблей.";
                    }
                }
            }
        }


        #endregion
    }

    // Класс для представления сезона
    public class Season
    {
        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Season(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
