using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnalyzeWater
{
    public partial class Form1 : Form
    {
        private OracleDatabase _database = new OracleDatabase();

        public Form1()
        {
            InitializeComponent();

            this.btnLoadData.Click += new EventHandler(this.btnLoadData_Click);
            this.открытьToolStripMenuItem.Click += new EventHandler(открытьToolStripMenuItem_Click);
            this.сохранитьToolStripMenuItem.Click += new EventHandler(сохранитьToolStripMenuItem_Click);
            this.экспортToolStripMenuItem.Click += new EventHandler(экспортToolStripMenuItem_Click);
            this.выходToolStripMenuItem.Click += new EventHandler(выходToolStripMenuItem_Click);
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click); // Кнопка для тестирования соединения

        }

        private void LoadData()
        {
            DataTable dataTable = _database.GetAllWaterData();
            dataGridView1.DataSource = dataTable;
            //MessageBox.Show(dataTable.ToString());
            LoadDataIntoChart(dataTable);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Код для чтения файла
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Код для сохранения файла
            }
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Код для экспорта данных
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Закрывает приложение
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
            //LoadDataIntoChart(dataTable);
        }

        private void LoadDataIntoChart(DataTable data)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Default"));

            var tankIds = data.AsEnumerable().Select(row => row.Field<decimal>("tank_id")).Distinct();

            foreach (var tankId in tankIds)
            {
                Series series = new Series
                {
                    Name = $"Tank {tankId}",
                    ChartType = SeriesChartType.Line
                };

                var tankData = data.AsEnumerable().Where(row => row.Field<decimal>("tank_id") == tankId);

                foreach (var row in tankData)
                {
                    series.Points.AddXY(row.Field<DateTime>("reading_timestamp"), row.Field<decimal>("liquid_level"));

                }
                chart1.Series.Add(series);
            }
            /*
            chart1.Series.Clear();
            var series = chart1.Series.Add("Liquid Level");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            
            int[] tank_ids = {-1};
            foreach (DataRow row in data.Rows)
            {
                if (!tank_ids.Contains(Convert.ToInt32(row["tank_id"])))
                {
                    int tank_id = Convert.ToInt32(row["tank_id"]);
                    tank_ids.Append(tank_id);

                    foreach (DataRow rw in data.Rows)
                    {
                        if (Convert.ToInt32(row["tank_id"]) == tank_id)
                        {
                            series.Points.AddXY(row["reading_timestamp"], row["liquid_level"]);

                        }
                    }

                }
            }*/
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {

            // TestDatabaseConnection();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Ваш код для обновления данных, например, LoadData();
            LoadData();

            // Показать сообщение "Обновлено"
            MessageBox.Show("Обновлено");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void ExportData()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.FileName = "DataExport.csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                        {
                            // Заголовки столбцов
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                sw.Write(dataGridView1.Columns[i].HeaderText);
                                if (i < dataGridView1.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.WriteLine();

                            // Данные строк
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    sw.Write(dataGridView1.Rows[i].Cells[j].Value);
                                    if (j < dataGridView1.Columns.Count - 1)
                                    {
                                        sw.Write(",");
                                    }
                                }
                                sw.WriteLine();
                            }
                        }
                        MessageBox.Show("Данные успешно экспортированы.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при экспорте данных: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет данных для экспорта.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            // _database.testconnections();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Соединение нормально работает");
            //LoadDatatest
        }
    }
}