using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Exam.Models;
using Exam.Data;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var connectionString = configuration.GetSection("connectionStrings")["CountriesAndCities"];

            int option = -1;
            string inputString;
            string countryName;
            string cityName;
            int tmpOption = -1;
            const int COUNTRY_PER_PAGE = 3;
            int currentPage = 0;

            using (ApplicationContext context = new ApplicationContext(connectionString))
            {
                while (option != 7)
                {
                    Console.WriteLine("Выберите опцию:");
                    Console.WriteLine("1)Добавить страну");
                    Console.WriteLine("2)Удалить страну");
                    Console.WriteLine("3)Добавить город");
                    Console.WriteLine("4)Удалить город");
                    Console.WriteLine("5)Посмотреть список стран");
                    Console.WriteLine("6)Посмотреть спикок городов страны");
                    Console.WriteLine("7)Выход");
                    inputString = Console.ReadLine();
                    option = int.Parse(inputString);

                    switch (option)
                    {
                        case 1:
                            {
                                Console.Write("Введите название страны: ");
                                countryName = Console.ReadLine();
                                Country country = new Country { Name = countryName };
                                //context.Counties.Add(new Country{ Name = countryName });
                                context.Counties.Add(country);
                                context.SaveChanges();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Введите название страны, которую хотите удалить: ");
                                countryName = Console.ReadLine();
                                Country countryToDelete = context.Counties.First(c => c.Name == countryName);
                                context.Counties.Remove(countryToDelete);
                                context.SaveChanges();
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Введите название страны, в которую хотите добавить город: ");
                                countryName = Console.ReadLine();
                                foreach (var country in context.Counties)
                                {
                                    if (countryName == country.Name)
                                    {
                                        Console.Write("Введите название города: ");
                                        cityName = Console.ReadLine();
                                        City city = new City { Name = cityName };
                                        //country.Cities.Add(new City{ Name = cityName });
                                        country.Cities.Add(city);
                                        context.Cities.Add(city);
                                        context.SaveChanges();
                                        break;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                Console.Write("Введите название страны, из которой хотите удалить город: ");
                                countryName = Console.ReadLine();
                                Console.Write("Введите название города, который хотите удалить: ");
                                cityName = Console.ReadLine();
                                foreach (var country in context.Counties)
                                {
                                    if (countryName == country.Name)
                                    {
                                        foreach (var city in country.Cities)
                                        {
                                            if (cityName == city.Name)
                                            {
                                                country.Cities.Remove(city);
                                                City cityToDelete = context.Cities.First(c => c.Name == cityName);
                                                context.Cities.Remove(cityToDelete);
                                                context.SaveChanges();
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                currentPage = 0;

                                ShowNextCountryPage(context, COUNTRY_PER_PAGE, ref currentPage);

                                while (tmpOption != 3)
                                {
                                    Console.WriteLine("Выберите опцию:");
                                    Console.WriteLine("1)Следующая страница");
                                    Console.WriteLine("2)Предыдущая страница");
                                    Console.WriteLine("3)Главное меню");
                                    inputString = Console.ReadLine();
                                    tmpOption = int.Parse(inputString);

                                    switch (tmpOption)
                                    {
                                        case 1:
                                            {
                                                ShowNextCountryPage(context, COUNTRY_PER_PAGE, ref currentPage);
                                                break;
                                            }
                                        case 2:
                                            {
                                                ShowPrevCountryPage(context, COUNTRY_PER_PAGE, ref currentPage);
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Вы выбрали неверную опцию");
                                                Console.WriteLine("Попробуйте выбрать еще раз");
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                        case 6:
                            {
                                Console.Write("Введите название страны, города которой хотите посмотреть: ");
                                countryName = Console.ReadLine();
                                foreach (var country in context.Counties)
                                {
                                    if (countryName == country.Name)
                                    {
                                        foreach (var city in country.Cities)
                                        {
                                            Console.WriteLine(city.Name);
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                Console.WriteLine("Good Bye!");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Вы выбрали неверную опцию");
                                Console.WriteLine("Попробуйте выбрать еще раз");
                                break;
                            }
                    }
                }
            }
        }

        public static void ShowNextCountryPage(ApplicationContext context, int countryPerPage, ref int currentPage)
        {
            currentPage++;
            var page = context.Counties.Skip(countryPerPage * (currentPage - 1)).Take(countryPerPage).ToList();
            Console.WriteLine(page.ToString());
        }

        public static void ShowPrevCountryPage(ApplicationContext context, int countryPerPage, ref int currentPage)
        {
            currentPage--;
            var page = context.Counties.Skip(countryPerPage * (currentPage - 1)).Take(countryPerPage).ToList();
            Console.WriteLine(page);
        }
    }
}
