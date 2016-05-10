using System;
using System.Collections.Generic;
using IOC.DataGens;
using IOC.Repositories;
using IOC.Services;

namespace IOC
{
    class Zadanie1
    {
        static void Main(string[] args)
        {
            List<string> a = DataBaseService.getProductNameByVendorNameLinq("1");
            Console.WriteLine(a.Count);
            DataRepository data;
            string decision;
            bool dynamic = false;
            Console.Out.WriteLine("Dynamiczne generowanie danych?? [T]");
            decision = Console.ReadLine();

            switch (decision)
            {
                case "T":
                    dynamic = true;
                    break;
                default:
                    dynamic = false;
                    break;

            }
            if (dynamic)
            {
                Console.WriteLine("Podaj liczbę elementów");
                int n;
                n = Convert.ToInt32(Console.ReadLine());
                data = new DataRepository(new BigRandomDataGenerator(n));
                DataService dataService = new DataService(data);
                dataService.printAllData();
            }
            else
            {
                data = new DataRepository(new SmallDataGenerator());
                DataService dataService = new DataService(data);
                dataService.printAllData();
            }
            Console.ReadKey();
        }
    }
}
