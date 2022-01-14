using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;


namespace Bunker_Game
{
    class Bunker_Generator
    {
        //Переменные подсчета размера изначальных списков характеристик
        public int ProfessionRange = 0, KindRange = 0, HealthRange = 0, HobbyRange = 0, FearRange = 0, InventoryRange = 0, InfoRange = 0, AbilityRange = 0;
        public static int NowBunker = -1, NowCataclysm = -1; //Статические переменные для Катаклизма и Бункера 

        //Списки характеристик
        public static List<String> Profession = new List<string> { };
        public static List<String> Kind = new List<string> { };
        public static List<String> Health = new List<string> { };
        public static List<String> Hobby = new List<string> { };
        public static List<String> Fear = new List<string> { };
        public static List<String> Inventory = new List<string> { };
        public static List<String> Info = new List<string> { };
        public static List<String> Ability = new List<string> { };
        public List<String> Cataclysm = new List<string> { };
        public List<String> Bunker = new List<string> { };

        //Метод рандомной генерации числа в пределах от First до Second через объект и дальнейшее его использование в нужных методах
        public  Random rng = new Random();
        public int GenerateMethod(Random rng, int First, int Second)
        {
            return rng.Next(First, Second);
        }

        //Метод прочтения определенных .txt файлов для того, чтобы заполнить списки характеристиками. Принимает на вход путь к файлу и флаг характеристики
        public void ReadList(string Path, NumberOfCollection Operation)
        {
            int NumberOfLines = File.ReadAllLines(Path).Length; // Нужна для подсчета строк в файле(Количество доступных харктеристик)
            StreamReader SRLog = new StreamReader(Path); // Открываем поток чтения
            try
            {
                for (int i = 0; i < NumberOfLines; i++)
                {//Прогоняемся по всем строчкам
                    switch (Operation)
                    {//В зависимости от того, какой флаг установлен, записываем в нужный список
                        case NumberOfCollection.ProfessionEnum:
                            Profession.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.KindEnum:
                            Kind.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.HealthEnum:
                            Health.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.HobbyEnum:
                            Hobby.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.FearEnum:
                            Fear.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.InventoryEnum:
                            Inventory.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.InfoEnum:
                            Info.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.AbilityEnum:
                            Ability.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.CataclysmEnum:
                            Cataclysm.Add(SRLog.ReadLine());
                            break;
                        case NumberOfCollection.BunkerEnum:
                            Bunker.Add(SRLog.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Произошла ошибка выбора коллекции при чтении файла");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SRLog.Close();
        }

        //Методы записаи отдельных характеристик в лог
        public void CreateProfession()
        {//Опишу тут смысл Create-ов, дальше по аналогии

            //Генерируем случайное число - номер итерации нужного элемента списка
            int RNumber = GenerateMethod(rng, 0, Profession.Count);

            //Открываем поток записи(В аругментах путь к файлу и true для того, чтобы в файл append-ились записи, а не перезаписывались)
            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Специальность: {Profession[RNumber].ToString()}");
            Profession.RemoveAt(RNumber); // Убираем элемент из списка, чтобы не было одинаковых ролей (Если того требуют правила)
            ProfessionRange = Profession.Count(); // Проверка на кол-во остатка в списке
            SWLog.Close();
        }
        public void CreateGender()
        {
            int RNumber = 0;
            string Gender = "Пол|Возраст: ";

            RNumber = GenerateMethod(rng, 0, 2);
            //Рандомим пол по двум цифрам
            switch (RNumber)
            {
                case 0:
                    Gender += "Мужчина ";
                    break;
                case 1:
                    Gender += "Женщина ";
                    break;
                default:
                    Gender += "Ошибка присовения пола ";
                    break;
            }

            //Тут идет возраст, и правила русского языка "Лет" "Год" и "Года"
            RNumber = GenerateMethod(rng, 14, 80);
            if (RNumber == 21 || RNumber == 31 || RNumber == 41
                || RNumber == 51 || RNumber == 61 || RNumber == 71
                || RNumber == 81)
            {
                Gender += RNumber + " Год ";
            }
            else if (RNumber == 2 || RNumber == 3 || RNumber == 4 || RNumber == 22 || RNumber == 23 || RNumber == 24
                || RNumber == 32 || RNumber == 33 || RNumber == 34 || RNumber == 42 || RNumber == 43 || RNumber == 44
                || RNumber == 52 || RNumber == 53 || RNumber == 54 || RNumber == 62 || RNumber == 63 || RNumber == 64
                || RNumber == 72 || RNumber == 73 || RNumber == 74 || RNumber == 82 || RNumber == 83 || RNumber == 84)
            {
                Gender += RNumber + " Года ";
            }
            else
            {
                Gender += RNumber + " Лет ";
            }

            //Тут рандомит Чайлдфри, якобы шанс 15%, но это псевдорандом, надо будет искать что-то другое, пока работает
            RNumber = GenerateMethod(rng, 0, 100);
            if (RNumber <= 15)
            {
                Gender += " Чайлдфри ";
            }

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine(Gender);
            SWLog.Close();
        }
        public void CreateBody()
        {
            string Body = "Телосложение: ";
            int RNumber = 0;
            RNumber = GenerateMethod(rng, 0, 8);

            switch (RNumber)
            {
                case 0:
                    Body += "Худое";
                    break;
                case 1:
                    Body += "Жилистое";
                    break;
                case 2:
                    Body += "Слегка полный";
                    break;
                case 3:
                    Body += "Крупное";
                    break;
                case 4:
                    Body += "Атлетическое";
                    break;
                case 5:
                    Body += "Очень высокий";
                    break;
                case 6:
                    Body += "Очень низкий";
                    break;
                case 7:
                    Body += "Толстый";
                    break;
                default:
                    Body += "Ошибка ";
                    break;
            }

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine(Body);
            SWLog.Close();
        }
        public void CreateKind()
        {
            int RNumber = GenerateMethod(rng, 0, Kind.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Человеческая черта: {Kind[RNumber].ToString()}");
            Kind.RemoveAt(RNumber);
            KindRange = Kind.Count();
            SWLog.Close();
        }
        public void CreateHealth()
        {
            int RNumber = GenerateMethod(rng, 0, Health.Count);
            int RNumberSupport = GenerateMethod(rng, 0, 3);
            int RNumberIdeal = GenerateMethod(rng, 0, 100);
            string HealthString = "Здоровье: ";

            if (RNumberIdeal >= 35)
            {

                HealthString += Health[RNumber].ToString();

                switch (RNumberSupport)
                {
                    case 0:
                        HealthString += "(Легкая степень)";
                        break;
                    case 1:
                        HealthString += "(Средняя степень)";
                        break;
                    case 2:
                        HealthString += "(Тяжелая степень)";
                        break;
                    default:
                        HealthString += "(Ошибка присовения степени здоровья)";
                        break;
                }
            }
            else
            {
                HealthString += "Идеально здоров";
            }
            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine(HealthString);
            Health.RemoveAt(RNumber);
            HealthRange = Health.Count();
            SWLog.Close();
        }
        public void CreateHobby()
        {
            int RNumber = GenerateMethod(rng, 0, Hobby.Count);
            int RNumberSupport = GenerateMethod(rng, 0, 4);
            string HobbyString = "Увлечение(Хобби): ";

            HobbyString += Hobby[RNumber].ToString();

            switch (RNumberSupport)
            {
                case 0:
                    HobbyString += "(Новичок)";
                    break;
                case 1:
                    HobbyString += "(Любитель)";
                    break;
                case 2:
                    HobbyString += "(Продвинутый)";
                    break;
                case 3:
                    HobbyString += "(Гуру)";
                    break;
                default:
                    HobbyString += "(Ошибка присовения хобби)";
                    break;

            }

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine(HobbyString);
            Hobby.RemoveAt(RNumber);
            HobbyRange = Hobby.Count();
            SWLog.Close();
        }
        public void CreateFear()
        {
            int RNumber = GenerateMethod(rng, 0, Fear.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Фобия: {Fear[RNumber].ToString()}");
            Fear.RemoveAt(RNumber);
            FearRange = Fear.Count();
            SWLog.Close();
        }
        public void CreateInventory()
        {
            int RNumber = GenerateMethod(rng, 0, Inventory.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Инвентарь: {Inventory[RNumber].ToString()}");
            Inventory.RemoveAt(RNumber);
            InventoryRange = Inventory.Count();
            SWLog.Close();
        }
        public void CreateInfo()
        {
            int RNumber = GenerateMethod(rng, 0, Info.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Доп. Сведения: {Info[RNumber].ToString()}");
            Info.RemoveAt(RNumber);
            InfoRange = Info.Count();
            SWLog.Close();
        }
        public void CreateAbilityFirst()
        {
            int RNumber = GenerateMethod(rng, 0, Ability.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Спец. Возможность №1: {Ability[RNumber].ToString()}");
            Ability.RemoveAt(RNumber);
            AbilityRange = Ability.Count();
            SWLog.Close();
        }
        public void CreateAbilitySecond()
        {
            int RNumber = GenerateMethod(rng, 0, Ability.Count);

            StreamWriter SWLog = new StreamWriter("Persons.txt", true);
            SWLog.WriteLine($"Спец. Возможность №2: {Ability[RNumber].ToString()}");
            Ability.RemoveAt(RNumber);
            AbilityRange = Ability.Count();
            SWLog.Close();
        }

        //Объединение всех методов записии характеристик в один, для удобства 
        public void CreateLog(int CountOfOperations)
        {
            try
            {
                for (int i = 0; i < CountOfOperations; i++)
                {
                        CreateGender();
                        CreateBody();
                        CreateKind();
                        CreateProfession();
                        CreateHealth();
                        CreateHobby();
                        CreateFear();
                        CreateInventory();
                        CreateInfo();
                        CreateAbilityFirst();
                        CreateAbilitySecond();

                        StreamWriter SWLog = new StreamWriter("Persons.txt", true);
                        SWLog.WriteLine(Environment.NewLine);
                        SWLog.Close();
                }
                MessageBox.Show(
                        "Пресонажи успешно созданы и находятся в файле Persons.txt",
                        "Успешно",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show(
                "Недостаточно характеристик в сиходных файлах, проверьте введенные данные + Ошибка в логике вылетает ",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                Application.Exit();
                return;
            }
        }
    }
}
