using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TP_MG.DataGens;
using TP_MG.Model;
using TP_MG.Repositories;

namespace TP_MGTest.PerfomanceDataRepositoryTests
{
    [TestClass]
    public class DictionaryPerformanceTests
    {
        BigRandomDataGenerator bigDataGenerator;
        Stopwatch stopper = new Stopwatch();
        System.IO.StreamWriter testOutput;
        string path = @"..\_dictionaryPerfomanceTest.txt";

        //[TestMethod]
        public void DictionaryPerformanceTest()
        {

            using (testOutput = new System.IO.StreamWriter(path, false))
            {
                testOutput.WriteLine("Dictionary perfomance test");
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                List<Room> rooms = new List<Room>();
                
                for (int i = 2; i < 24; i++)
                {
                    bigDataGenerator = new BigRandomDataGenerator((int)Math.Pow(2, i));
                    stopper.Reset();

                    stopper.Start();
                    bigDataGenerator.fillCustomers(customers);
                    stopper.Stop();

                    testOutput.WriteLine("no. of elements: " + ((int)Math.Pow(2, i)).ToString().PadLeft(8)
                        + " time elapsed(ms): " + stopper.ElapsedMilliseconds.ToString().PadLeft(6));
                }
            }
        }
    }
}
