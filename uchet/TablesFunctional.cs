using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace Uchet
{
    static class TablesFunctional
    {
        public static void ReadFile(string Path, ref string[] StringArray, 
            ref double[] Weight1Array, ref double[] Res1Array, ref double[] Weight2Array, ref double[] Res2Array)
        {
            int NumberOfLines = File.ReadAllLines(Path).Length; // Нужна для подсчета строк в файле(Количество доступных харктеристик)
            StreamReader SRLog = new StreamReader(Path); // Открываем поток чтения

            for (int i = 0; i < NumberOfLines; i++)
            {
                string[] tmp = SRLog.ReadLine().Split('-');

                if (tmp[0] != "")
                    StringArray[i] = tmp[0];
                else
                    StringArray[i] = "-";

                if (tmp[1] != "")
                    Weight1Array[i] = Double.Parse(tmp[1]);
                else
                    Weight1Array[i] = 0;
                if (tmp[2] != "")
                    Weight2Array[i] = Double.Parse(tmp[2]);
                else
                    Weight2Array[i] = 0;
                if (tmp[3] != "")
                    Res1Array[i] = Double.Parse(tmp[3]);
                else
                    Res1Array[i] = 0;
                if (tmp[4] != "")
                    Res2Array[i] = Double.Parse(tmp[4]);
                else
                    Res2Array[i] = 0;

                tmp = null;
            }
            SRLog.Close();
        }

        public static void UploadTable(DataGridView dataGridView1, string path)
        { 
            int NumberOfLines = File.ReadAllLines(path).Length;
            string[] StringArray = new string[NumberOfLines];
            double[] Weight1Array = new double[NumberOfLines];
            double[] Res1Array = new double[NumberOfLines];
            double[] Weight2Array = new double[NumberOfLines];
            double[] Res2Array = new double[NumberOfLines];
            ReadFile(path, ref StringArray, ref Weight1Array, ref Res1Array, ref Weight2Array, ref Res2Array);

            DataTable dt = new DataTable();
            dt.Columns.Add("Номер");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Расход на Д.Сад");
            dt.Columns.Add("Расход на Ясли");
            dt.Columns.Add("Итого Д.Сад");
            dt.Columns.Add("Итого Ясли");
            dt.Columns.Add("Итого");

            for (int i = 0; i < NumberOfLines; i++)
            {
                DataRow row = dt.NewRow();
                row["Номер"] = i + 1;
                row["Наименование"] = StringArray[i];
                row["Расход на Д.Сад"] = Weight1Array[i];
                row["Итого Д.Сад"] = Res1Array[i];
                row["Расход на Ясли"] = Weight2Array[i];
                row["Итого Ясли"] = Res2Array[i];
                row["Итого"] = Res1Array[i] + Res2Array[i];
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Visible = true;
        }

        public static void SaveTable(DataGridView dataGridView1, string path)
        {
            StreamWriter SWLog = new StreamWriter(path);
            string res = "";
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    if (i != dataGridView1.Columns.Count)
                        res += row.Cells[i].Value + "-";
                    else
                        res += row.Cells[i].Value;
                }
                count++;
                if (count == dataGridView1.Rows.Count)
                    break;
                SWLog.WriteLine(res);
                res = "";
            }
            SWLog.Close();
            count = 0;
        }
    }
}
