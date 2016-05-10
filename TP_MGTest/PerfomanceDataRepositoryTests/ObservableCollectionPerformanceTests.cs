using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TP_MG.DataGens;
using TP_MG.Model;
using TP_MG.Repositories;

namespace TP_MGTest.PerfomanceDataRepositoryTests
{
    [TestClass]
    public class ObservableCollectionPerformanceTests
    {
        BigRandomDataGenerator bigDataGenerator;
        Stopwatch stopper = new Stopwatch();
        System.IO.StreamWriter testOutput;
        string path = @"..\_observableCollectionPerfomanceTest.txt";

        //[TestMethod]
        public void ObservableCollectionPerformanceTest()
        {

            using (testOutput = new System.IO.StreamWriter(path, false))
            {
                testOutput.WriteLine("ObservableCollection perfomance test");
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                List<Room> rooms = new List<Room>();
                ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
                for (int i = 2; i < 24; i++)
                {
                    bigDataGenerator = new BigRandomDataGenerator((int)Math.Pow(2, i));
                    bigDataGenerator.fillCustomers(customers);
                    bigDataGenerator.fillRooms(rooms);

                    stopper.Reset();
                    stopper.Start();
                    bigDataGenerator.fillReservations(customers, rooms, reservations);
                    stopper.Stop();

                    testOutput.WriteLine("no. of elements: " + ((int)Math.Pow(2, i)).ToString().PadLeft(8)
                        + " time elapsed(ms): " + stopper.ElapsedMilliseconds.ToString().PadLeft(6));
                }
            }
        }
    }
}

