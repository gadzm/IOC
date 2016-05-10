using System;
using TP_MG.DataGens;
using TP_MG.Repositories;
using TP_MG.Services;

namespace TP_MG
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository data;
            data = new DataRepository(new SmallDataGenerator());
            DataService dataService = new DataService(data);
            dataService.printAllData();
            Console.ReadKey();
        }
    }
}
