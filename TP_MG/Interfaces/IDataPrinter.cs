using System.Collections.Generic;
using System.Collections.ObjectModel;
using IOC.Model;

namespace IOC.Interfaces
{
    public interface IDataPrinter
    {
        void printCustomers(Dictionary<int, Customer> customersMap);
        void printRooms(List<Room> roomsList);
        void printReservations(ObservableCollection<Reservation> reservationList);
        void printWarning(string warning);
    }
}
