using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Uchet
{
    public partial class Form1 : Form
    {
        void FirstLoad ()
        {
            string path = Environment.CurrentDirectory + "\\Tables\\" + "table" + numericUpDown1.Value + ".txt";
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory("Tables");
            File.WriteAllText(path, "Продукт-0-0-0-0-0-0");
        }

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Visible = true;
            if(!Directory.Exists(Environment.CurrentDirectory + "\\Tables"))
                FirstLoad();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double tmp1=0, tmp2=0, tmp3=0;
                int count = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    count++;
                    if (count == dataGridView1.Rows.Count)
                        break;
                    tmp1 = Double.Parse(row.Cells[2].Value.ToString()) * Int32.Parse(textBox2.Text);
                    tmp2 = Double.Parse(row.Cells[3].Value.ToString()) * Int32.Parse(textBox1.Text);
                    row.Cells[4].Value = tmp1.ToString();
                    row.Cells[5].Value = tmp2.ToString();
                    tmp3 = Double.Parse(row.Cells[4].Value.ToString()) + Double.Parse(row.Cells[5].Value.ToString());
                    row.Cells[6].Value = tmp3.ToString();

                }
                count = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка: " + ex.Message,
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите загрузить таблицу?", "Предупреждение",
    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string path = Environment.CurrentDirectory + "\\Tables\\" + "table" + numericUpDown1.Value + ".txt";
                    TablesFunctional.UploadTable(dataGridView1, path);
                    MessageBox.Show(
                     "Файл успешно загружен",
                     "Успешно",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ошибка: " + ex.Message,
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите сохранить таблицу?", "Предупреждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string path = Environment.CurrentDirectory + "\\Tables\\" + "table" + numericUpDown1.Value + ".txt";
                    TablesFunctional.SaveTable(dataGridView1, path);
                    MessageBox.Show(
                        "Файл успешно сохранен",
                        "Успешно",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information,
                         MessageBoxDefaultButton.Button1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ошибка: " + ex.Message,
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите очистить таблицу?", "Предупреждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Номер");
                    dt.Columns.Add("Наименование");
                    dt.Columns.Add("Расход на Д.Сад");
                    dt.Columns.Add("Расход на Ясли");
                    dt.Columns.Add("Итого Д.Сад");
                    dt.Columns.Add("Итого Ясли");
                    dt.Columns.Add("Итого");
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ошибка: " + ex.Message,
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\Tables\\" + "table" + numericUpDown1.Value + ".txt";
            if (File.Exists(path))
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите создать новую таблицу? " + "\n" +
                    "Текущие данные будут перезаписаны", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Номер");
                        dt.Columns.Add("Наименование");
                        dt.Columns.Add("Расход на Д.Сад");
                        dt.Columns.Add("Расход на Ясли");
                        dt.Columns.Add("Итого Д.Сад");
                        dt.Columns.Add("Итого Ясли");
                        dt.Columns.Add("Итого");
                        dataGridView1.DataSource = dt;
                        FirstLoad();

                        TablesFunctional.UploadTable(dataGridView1, path);
                        dataGridView1.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Ошибка: " + ex.Message,
                            "Ошибка",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
                    }
                }
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Номер");
                    dt.Columns.Add("Наименование");
                    dt.Columns.Add("Расход на Д.Сад");
                    dt.Columns.Add("Расход на Ясли");
                    dt.Columns.Add("Итого Д.Сад");
                    dt.Columns.Add("Итого Ясли");
                    dt.Columns.Add("Итого");
                    dataGridView1.DataSource = dt;
                    FirstLoad();

                    TablesFunctional.UploadTable(dataGridView1, path);
                    dataGridView1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ошибка: " + ex.Message,
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}
