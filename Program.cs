namespace EFCodeFirstGame;
using DataAccess;
using Entities;
using System.Globalization;
using System.Text;




internal class Program
{
    static public void PrintInfo(string title, List<Game> ls)
    {
        Console.WriteLine(title);
        foreach (Game game in ls)
        {
            Console.WriteLine($"{game.Id} | {game.Name} | {game.Studio} | {game.Style} | {game.DateRelease} | {game.GameplayMode} | {game.NumberSold}");
        }
    }
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine(1145);
        using (ApplicationContext dc = new ApplicationContext())
        {

            dc.Games.Add(new Game { Name = "CS", Studio = "Valve", Style = "Shooter", DateRelease = new DateOnly(2020, 6, 2, new JulianCalendar()), GameplayMode = Game.Mode.Multiplayer, NumberSold = 87000 });
            dc.Games.Add(new Game { Name = "Withcher3", Studio = "CD Project Red", Style = "RPG", DateRelease = new DateOnly(2019, 6, 2, new JulianCalendar()), GameplayMode = Game.Mode.SinglePlayer, NumberSold = 125668 });
            dc.SaveChanges();

            var games = dc.Games.ToList();
            PrintInfo("Всі гри: ", games);


            Console.WriteLine("Пошук за назвою гри \nВведіть назву гри: ");
            string inputName = Convert.ToString(Console.ReadLine());
            var gameSearchByName = dc.Games.Where(g => g.Name == inputName).ToList();
            PrintInfo("Інформація про цю гру: ", gameSearchByName);

            Console.WriteLine("Пошук за назвою студії \nВведіть назву студії: ");
            string inputStudio = Convert.ToString(Console.ReadLine());
            var gamesSearchByStudio = dc.Games.Where(g => g.Studio == inputStudio).ToList();
            PrintInfo("Ігри цієї студії: ", gamesSearchByStudio);


            Console.WriteLine("Пошук за стилем гри \nВведіть стиль гри: ");
            string inputStyle = Convert.ToString(Console.ReadLine());
            var gamesSearchByStyle = dc.Games.Where(g => g.Style == inputStyle).ToList();
            PrintInfo("Ігри цього стилю: ", gamesSearchByStyle);


            Console.WriteLine("Пошук за роком випуску гри \nВведіть рік випуску гри: ");
            int inputYear = Convert.ToInt32(Console.ReadLine());
            var gamesSearchByYear = dc.Games.Where(g => g.DateRelease >= new DateOnly(inputYear, 1, 1, new JulianCalendar()) && g.DateRelease <= new DateOnly(inputYear, 12, 31, new JulianCalendar())).ToList();
            PrintInfo("Ігри цього року: ", gamesSearchByYear);


            var gamesSingle = dc.Games.Where(g => g.GameplayMode == Game.Mode.SinglePlayer).ToList();
            PrintInfo("Однокористувацькі ігри: ", gamesSingle);


            var gamesMulti = dc.Games.Where(g => g.GameplayMode == Game.Mode.Multiplayer).ToList();
            PrintInfo("Багатокористувацькі ігри: ", gamesMulti);


            var gamesMostPopular = dc.Games.OrderByDescending(g => g.NumberSold).Take(3).ToList();
            PrintInfo("Топ-3 найпопулярніших ігор (за кількістю проданих копій): ", gamesMostPopular);


            var gamesLeastPopular = dc.Games.OrderBy(g => g.NumberSold).Take(3).ToList();
            PrintInfo("Топ-3 найнепопулярніших ігор (за кількістю проданих копій): ", gamesLeastPopular);

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("1 - Додавання");
            Console.WriteLine("2 - Редагування");
            Console.WriteLine("3 - Видалення");
            while (true)
            {
                int input = Convert.ToUInt16(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        {

                            Console.WriteLine("Введіть назву гри: ");
                            string name = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Введіть назву студії гри: ");
                            string studio = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Введіть стиль гри: ");
                            string style = Convert.ToString(Console.ReadLine());
                            DateOnly dateRelease;
                            while (true)
                            {
                                Console.WriteLine("Введіть дату релізу гри: ");
                                string dateInput = Console.ReadLine();

                                if (DateOnly.TryParse(dateInput, out dateRelease))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Некоректний формат дати!");
                                }
                            }

                            Console.WriteLine("Виберіть режим гри");
                            Console.WriteLine("0 - single player");
                            Console.WriteLine("1 - multiplayer");
                            Game.Mode mode = (Game.Mode)Convert.ToInt16(Console.ReadLine());

                            Console.WriteLine("Введіть кількість проданих копій гри: ");
                            int count = Convert.ToInt32(Console.ReadLine());

                            Game newGame = new Game(name, studio, style, dateRelease, mode, count);
                            dc.Games.Add(newGame);
                            dc.SaveChanges();
                            Console.Clear();
                            break;
                        }

                    case 2:
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.WriteLine("Введіть назву гри, дані якої хочете змінити: ");
                                string name = Convert.ToString(Console.ReadLine());

                                if (dc.Games.FirstOrDefault(g => g.Name == name) != null)
                                {
                                    Console.WriteLine("Що саме потрібно змінити");
                                    Console.WriteLine("1 - Назва");
                                    Console.WriteLine("2 - Студія");
                                    Console.WriteLine("3 - Стиль");
                                    Console.WriteLine("4 - Дата");
                                    Console.WriteLine("5 - Режим");
                                    Console.WriteLine("6 - Кількість");
                                    int ch = Convert.ToInt16(Console.ReadLine());
                                    switch (ch)
                                    {
                                        case 1:
                                            Console.Clear();
                                            Console.WriteLine("Введіть нову назву гри: ");
                                            string newName = Convert.ToString(Console.ReadLine());
                                            dc.Games.FirstOrDefault(g => g.Name == name).Name = newName;
                                            break;

                                        case 2:
                                            Console.Clear();
                                            Console.WriteLine("Введіть нову студію гри: ");
                                            string newStudio = Convert.ToString(Console.ReadLine());
                                            dc.Games.FirstOrDefault(g => g.Name == name).Studio = newStudio;
                                            break;

                                        case 3:
                                            Console.Clear();
                                            Console.WriteLine("Введіть нову стиль гри: ");
                                            string newStyle = Convert.ToString(Console.ReadLine());
                                            dc.Games.FirstOrDefault(g => g.Name == name).Style = newStyle;
                                            break;

                                        case 4:
                                            Console.Clear();
                                            DateOnly newdateRelease;
                                            while (true)
                                            {
                                                Console.WriteLine("Введіть нову дату релізу гри: ");
                                                string dateInput = Console.ReadLine();

                                                if (DateOnly.TryParse(dateInput, out newdateRelease))
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Некоректний формат дати!");
                                                }

                                            }

                                            dc.Games.FirstOrDefault(g => g.Name == name).DateRelease = newdateRelease;
                                            break;

                                        case 5:
                                            Console.WriteLine("Виберіть новий режим гри: ");
                                            Console.WriteLine("0 - SinglePlayer");
                                            Console.WriteLine("1 - Multiplayer");
                                            int NewGameMode = Convert.ToInt16(Console.ReadLine());

                                            switch (NewGameMode)
                                            {
                                                case 0:
                                                    dc.Games.FirstOrDefault(g => g.Name == name).GameplayMode = Game.Mode.SinglePlayer;
                                                    break;
                                                case 1:
                                                    dc.Games.FirstOrDefault(g => g.Name == name).GameplayMode = Game.Mode.Multiplayer;
                                                    break;
                                                default:
                                                    break;
                                            }

                                            break;

                                        case 6:
                                            Console.WriteLine("Введіть нову кількість проданих копій гри: ");
                                            int newNumber = Convert.ToInt32(Console.ReadLine());
                                            dc.Games.FirstOrDefault(g => g.Name == name).NumberSold = newNumber;
                                            break;
                                        default:
                                            break;
                                    }

                                    dc.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Гру не знайдено.");
                                }
                            }

                            break;
                        }

                    case 3:
                        {
                            while (true)
                            {
                                Console.WriteLine("Введіть назву гри, яку хочете видалити: ");
                                string name = Convert.ToString(Console.ReadLine());


                                if (dc.Games.FirstOrDefault(g => g.Name == name) != null)
                                {
                                    dc.Games.Remove(dc.Games.FirstOrDefault(g => g.Name == name));
                                    dc.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Гру не знайдено.");

                                }
                                break;
                            }


                            break;
                        }

                    default:
                        break;
                }



            }
        }

    }

}

