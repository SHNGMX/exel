using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace exel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Установлеие соединнеия с базой данных
            string connectionString = "Data Source = db.edu.cchgeu.ru;Initial Catalog = 201s_Tkachenko; User ID = 201s_Tkachenko; Password = QazWsxEdc123;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Путь к файлу CSV
            string filePath = "C:\\data.csv";

            // Чтение данных из файла CSV и вставка их в таблицу
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    SqlCommand command = new SqlCommand("INSERT INTO myTable (Column1, Column2, Column3) VALUES (@val1, @val2, @val3)", connection);
                    command.Parameters.AddWithValue("@val1", values[0]);
                    command.Parameters.AddWithValue("@val2", values[1]);
                    command.Parameters.AddWithValue("@val3", values[2]);
                    command.ExecuteNonQuery();
                }
            }
            // Закрытие соединения
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получение данных из DataGridView
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                sb.Append(dataGridView1.Columns[i].HeaderText);
                sb.Append(",");
            }
            sb.AppendLine();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
    {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    sb.Append(dataGridView1.Rows[i].Cells[j].Value.ToString());
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            //Сохранение данных в файл CSV
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV(*.csv)|*.csv";
            sfd.FileName = "data.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }
    }
}
