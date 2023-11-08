using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForSanya2
{
    public partial class Form2 : Form
    {
        private SQLiteConnection connection;
        public Form2()
        {
            InitializeComponent();

            pReg.Hide();

            // Инициализация подключения к базе данных
            string dbPath = "UserLogin.db";
            bool createTable = false;

            if (!System.IO.File.Exists(dbPath))
            {
                createTable = true;
                SQLiteConnection.CreateFile(dbPath);
            }

            connection = new SQLiteConnection($"Data Source={dbPath};"); // Используйте поле класса

            connection.Open();

            if (createTable)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "CREATE TABLE Users (Login TEXT, Password TEXT);";
                    cmd.ExecuteNonQuery();

                    // Вставляем значения User и 123 в первую строку
                    cmd.CommandText = "INSERT INTO Users (Login, Password) VALUES ('SaIIIa', '123');";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "CREATE TABLE Admins (Login TEXT, Password TEXT);";
                    cmd.ExecuteNonQuery();

                    // Вставляем значения User и 123 в первую строку
                    cmd.CommandText = "INSERT INTO Admins (Login, Password) VALUES ('Admin', '123');";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool CheckCredentialsInTable(string username, string password, string tableName)
        {
            string query = $"SELECT * FROM {tableName} WHERE Login = @username AND Password = @password";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable.Rows.Count > 0;
                }
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (tbUser.Text != null || tbPass.Text != null)
            {
                string username = tbUser.Text; // Получаем имя пользователя из элемента управления
                string password = tbPass.Text; // Получаем пароль из элемента управления

                // Попробуйте искать в таблице Admins
                if (CheckCredentialsInTable(username, password, "Admins"))
                {
                    // Если запись найдена, откройте Form3
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Hide();
                }
                // Если не найдено в Admins, попробуйте в таблице Users
                else if (CheckCredentialsInTable(username, password, "Users"))
                {
                    // Если запись найдена, откройте Form2
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверные учетные данные. Пожалуйста, попробуйте снова.");
                }
            }
            else
            {
                MessageBox.Show("Неверные учетные данные.");
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (tbUser.Text != null || tbPass.Text != null)
            {
                string username = tbUser.Text; // Получаем имя пользователя из элемента управления
                string password = tbPass.Text; // Получаем пароль из элемента управления

                // Добавление новой записи в базу данных
                string insertQuery = "INSERT INTO Users (Login, Password) VALUES (@username, @password)";
                using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@password", password);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Регистрация прошла успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось зарегистрироваться. Пожалуйста, попробуйте снова.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Не удалось зарегистрироваться.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            pLog.Hide();
            pReg.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            pReg.Hide();
            pLog.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрытие подключения к базе данных при закрытии формы
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            Application.Exit();
        }
    }
}
