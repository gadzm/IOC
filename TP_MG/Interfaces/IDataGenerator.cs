using System.Collections.Generic;
using System.Collections.ObjectModel;
using IOC.Model;

namespace IOC.Interfaces
{
    public interface IDataGenerator
    {
        void fillData(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations);
        void fillCustomers(Dictionary<int, Customer> customers);
        void fillRooms(List<Room> rooms);
        void fillReservations(Dictionary<int, Customer> customers, List<Room> rooms, ObservableCollection<Reservation> reservations);

    }
}
