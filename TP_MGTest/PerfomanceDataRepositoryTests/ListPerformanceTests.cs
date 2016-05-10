using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TP_MG.DataGens;
using TP_MG.Model;
using TP_MG.Repositories;

namespace TP_MGTest.PerfomanceDataRepositoryTests
{
    [TestClass]
    public class ListPerformanceTests
    {
        BigRandomDataGenerator bigDataGenerator;
        Stopwatch stopper = new Stopwatch();
        System.IO.StreamWriter testOutput;
        string path = @"..\_listPerfomanceTest.txt";

        //[TestMethod]
        public void ListPerformanceTest()
        {

            using (testOutput = new System.IO.StreamWriter(path, false))
            {
                testOutput.WriteLine("List perfomance test");
                List<Room> rooms = new List<Room>();
                for (int i = 2; i < 24; i++)
                {
                    bigDataGenerator = new BigRandomDataGenerator((int)Math.Pow(2, i));
                    stopper.Reset();

                    stopper.Start();
                    bigDataGenerator.fillRooms(rooms);
                    stopper.Stop();

                    testOutput.WriteLine("no. of elements: " + ((int)Math.Pow(2, i)).ToString().PadLeft(8)
                        + " time elapsed(ms): " + stopper.ElapsedMilliseconds.ToString().PadLeft(6));
                }
            }
        }
    }
}
