using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IOC.Interfaces;
using IOC.Model;

namespace IOC.View
{
    public class SimpleCollectionPrinter : IDataPrinter
    {
        public void printCustomers(Dictionary<int, Customer> customersMap)
        {
            foreach (KeyValuePair<int, Customer> customer in customersMap)
            {
                Console.WriteLine("id" + customer.Key.ToString().PadLeft(5) + ": " + customer.Value.ToString());
            }
        }

        public void printReservations(ObservableCollection<Reservation> reservationList)
        {
            foreach (Reservation res in reservationList)
            {
                Console.WriteLine(res.ToString());
            }
        }

        public void printRooms(List<Room> roomsList)
        {
            foreach (Room room in roomsList)
            {
                Console.WriteLine(room.ToString());
            }
        }

        public void printWarning(string warning)
        {
            Console.WriteLine("Błąd");
            Console.WriteLine(warning);
        }
    }
}
