using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IOC.Interfaces;
using IOC.Model;

namespace IOC.DataGens
{
    public class SmallDataGenerator : IDataGenerator
    {
        public void fillData(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations)
        {
            fillCustomers(customers);
            fillRooms(rooms);
            fillReservations(customers, rooms, reservations);
        }

        public void fillCustomers(Dictionary<int, Customer> customers)
        {
            customers.Add(0, new Customer("Frank", "Zappa", category.vip));
            customers.Add(1, new Customer("David", "Bowie", category.vip));
            customers.Add(2, new Customer("Adam", "Miauczyński", category.regular));
            customers.Add(3, new Customer("Krzysztof", "Kononowicz", category.businesclass));
            customers.Add(4, new Customer("Fela", "Kuti", category.businesclass));
            customers.Add(5, new Customer("Pat", "Nixon", category.vip));
            customers.Add(6, new Customer("Richard", "Nixon", category.vip));
            
        }

        public void fillRooms(List<Room> rooms)
        {
            for (int i = 0; i < 10; i++)
            {
                rooms.Add(new Room(100 + i));
                rooms.Add(new Room(200 + i));
            }
            rooms.Sort((room1, room2) => room1.RoomNumber.CompareTo(room2.RoomNumber));
        }
        public void fillReservations(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations)
        {
            reservations.Add(new Reservation(customers[0], rooms[0], 0, new DateTime(2015, 11, 15), new DateTime(2015, 11, 17)));
            reservations.Add(new Reservation(customers[1], rooms[2], 1, new DateTime(2015, 12, 1), new DateTime(2015, 11, 5)));
            reservations.Add(new Reservation(customers[2], rooms[4], 2, new DateTime(2015, 12, 31), new DateTime(2016, 1, 5)));
            reservations.Add(new Reservation(customers[3], rooms[5], 3, new DateTime(2015, 12, 31), new DateTime(2016, 1, 2)));
            reservations.Add(new Reservation(customers[4], rooms[6], 4, new DateTime(2016, 1, 5), new DateTime(2016, 1, 11)));
            reservations.Add(new Reservation(customers[5], rooms[17], 5, new DateTime(2016, 1, 8), new DateTime(2016, 1, 10)));
            reservations.Add(new Reservation(customers[6], rooms[0], 6, new DateTime(2016, 1, 8), new DateTime(2016, 1, 10)));
            reservations.Add(new Reservation(customers[1], rooms[12], 7, new DateTime(2016, 2, 8), new DateTime(2016, 2, 15)));
            reservations.Add(new Reservation(customers[0], rooms[0], 8, new DateTime(2016, 3, 1), new DateTime(2016, 3, 15)));
        }
    }
}
