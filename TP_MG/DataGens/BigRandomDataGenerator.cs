using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IOC.Interfaces;
using IOC.Model;
using System.IO;

namespace IOC.DataGens
{
    public class BigRandomDataGenerator : IDataGenerator
    {
        private int dataSize;
        private List<string> firstNames = new List<string>();
        private List<string> lastNames = new List<string>();
        private int firstNamesNo;
        private int lastNamesNo;
        private Dictionary<int, DateTime> datesFrom = new Dictionary<int, DateTime>();
        private Dictionary<int, DateTime> datesTo = new Dictionary<int, DateTime>();
        private List<category> catList = new List<category>() {
            category.vip,
            category.regular,
            category.businesclass
            };

        public void fillData(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations)
        {
            fillCustomers(customers);
            fillRooms(rooms);
            fillReservations(customers, rooms, reservations);
        }

        public void fillCustomers(Dictionary<int, Customer> customers)
        {
            customers.Clear();
            Random random = new Random();
            for (int i = 0; i < dataSize; i++)
            {
                customers.Add(i, new Customer(firstNames[random.Next() % firstNamesNo], lastNames[random.Next() % lastNamesNo], catList[random.Next()%3]));
            }
        }

        public void fillReservations(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations)
        {
            if (customers.Count() == 0 || rooms.Count() == 0)
            {
                throw new Exception("customers or rooms lists are empty");
            }
            reservations.Clear();
            Random random = new Random();
            for (int i = 0; i < dataSize; i++)
            {
                int randomDate = random.Next() % 1000;
                reservations.Add(new Reservation(customers[random.Next()%dataSize],rooms[random.Next() % dataSize],i, datesFrom[randomDate],datesTo[randomDate]));
            }
        }

        public void fillRooms(List<Room> rooms)
        {
            rooms.Clear();
            Random random = new Random();
            for (int i = 0; i < dataSize; i++)
            {
                rooms.Add(new Room(random.Next()%300+100));
            }
        }

        public BigRandomDataGenerator(int dataSize)
        {
            this.dataSize = dataSize;
            prepareNamesLists();
            firstNamesNo = firstNames.Count;
            lastNamesNo = lastNames.Count;
            prepareDates();
        }

        private void prepareNamesLists()
        {
            string line;
                using (StreamReader namesFile = new StreamReader("..\\firstNames"))
                {
                    while ((line = namesFile.ReadLine()) != null)
                    {
                        firstNames.Add(line);
                    }
                }
                using (StreamReader namesFile = new StreamReader("..\\lastNames"))
                {
                    while ((line = namesFile.ReadLine()) != null)
                    {
                        lastNames.Add(line);
                    }
                }
        }

        private void prepareDates()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                DateTime d1 = new DateTime(random.Next() % 3 + 2015, random.Next() % 12 + 1, random.Next() % 28 + 1);
                DateTime d2 = d1.AddDays(random.Next() % 30 + 1);
                datesFrom.Add(i, d1);
                datesTo.Add(i, d2);
            }
        }
    }
}
