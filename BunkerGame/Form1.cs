using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bunker_Game
{
    //Перечисление для выбора файла характеристик (Флаги)
    public enum NumberOfCollection
    {
        ProfessionEnum,
        KindEnum,
        HealthEnum,
        HobbyEnum,
        FearEnum,
        InventoryEnum,
        InfoEnum,
        AbilityEnum,
        CataclysmEnum,
        BunkerEnum
    }


    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonCreatePerson_Click(object sender, EventArgs e)
        {
            try
            {
                int PersonCount = Int32.Parse((textBoxNumberOfPersons.Text));
                if(PersonCount <= 0 )
                {
                    MessageBox.Show(
                        "Ошибка: Кол-во персонажей не может быть меньше или равно 0",
                        "Успешно",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                Bunker_Generator obj = new Bunker_Generator();

                obj.ReadList("profession.txt", NumberOfCollection.ProfessionEnum);
                obj.ReadList("kind.txt", NumberOfCollection.KindEnum);
                obj.ReadList("health.txt", NumberOfCollection.HealthEnum);
                obj.ReadList("hobby.txt", NumberOfCollection.HobbyEnum);
                obj.ReadList("fear.txt", NumberOfCollection.FearEnum);
                obj.ReadList("inventory.txt", NumberOfCollection.InventoryEnum);
                obj.ReadList("ability.txt", NumberOfCollection.AbilityEnum);
                obj.ReadList("info.txt", NumberOfCollection.InfoEnum);

                obj.CreateLog(PersonCount);

                Console.WriteLine(obj.ProfessionRange);
                Console.WriteLine(obj.KindRange);
                Console.WriteLine(obj.HealthRange);
                Console.WriteLine(obj.HobbyRange);
                Console.WriteLine(obj.FearRange);
                Console.WriteLine(obj.InventoryRange);
                Console.WriteLine(obj.InfoRange);
                Console.WriteLine(obj.AbilityRange);
            }
            catch(FormatException)
            {
                MessageBox.Show(
                    "Ошибка: Проверьте введеные данные",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            catch(Exception ex)
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

        private void buttonCataclysm_Click(object sender, EventArgs e)
        {
            Bunker_Generator obj = new Bunker_Generator();//Объект класса генератора
            Random rng = new Random();//Объект для рандома
            int RNumber = 0;//Число для рандома
            try
            {
                if (obj.Cataclysm.Count == 0)//Если списопк пустой, заполняем его
                    obj.ReadList("cataclysm.txt", NumberOfCollection.CataclysmEnum);
                RNumber = obj.GenerateMethod(rng, 0, obj.Cataclysm.Count);//Генерируем рандомное число
                if (Bunker_Generator.NowCataclysm != RNumber)//Если текущее число не совпадает с предыдущим, то !!Меняем!! текст на новый
                    textBoxCataclysm.Text = obj.Cataclysm[RNumber];
                else//Если же выпало то же самое число, то сами его меняем на другое
                {
                    if (RNumber != (obj.Cataclysm.Count - 1))//Если это не максимальное число, то меняем на +1
                        textBoxCataclysm.Text = obj.Cataclysm[RNumber + 1];
                    else//Иначе меняем на -1
                        textBoxCataclysm.Text = obj.Cataclysm[RNumber - 1];
                }
                textBoxCataclysm.Visible = true;
                Bunker_Generator.NowCataclysm = RNumber;//Запоминаем число 
            }
            catch(Exception ex)
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

        private void buttonBunker_Click(object sender, EventArgs e)
        {
            Bunker_Generator obj = new Bunker_Generator();//Объект класса генератора
            Random rng = new Random();//Объект для рандома
            int RNumber = 0;//Число для рандома
            try
            {
                if (obj.Bunker.Count == 0)//Если списопк пустой, заполняем его
                    obj.ReadList("bunker.txt", NumberOfCollection.BunkerEnum);
                RNumber = obj.GenerateMethod(rng, 0, obj.Bunker.Count);//Генерируем рандомное число
                if (Bunker_Generator.NowBunker != RNumber)//Если текущее число не совпадает с предыдущим, то !!Меняем!! текст на новый
                    textBoxBunker.Text = obj.Bunker[RNumber];
                else//Если же выпало то же самое число, то сами его меняем на другое
                {
                    if (RNumber != (obj.Bunker.Count-1))//Если это не максимальное число, то меняем на +1
                        textBoxBunker.Text = obj.Bunker[RNumber + 1];
                    else//Иначе меняем на -1
                        textBoxBunker.Text = obj.Bunker[RNumber - 1];
                }
                textBoxBunker.Visible = true;
                Bunker_Generator.NowBunker = RNumber;//Запоминаем число 

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
}
