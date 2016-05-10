using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IOC.Interfaces;
using IOC.Model;
using IOC.Repositories;
using IOC.View;

namespace IOC.Services
{
    public class DataService
    {
        private DataRepository data;
        private IDataPrinter printer;


        public DataService(DataRepository data)
        {
            this.data = data;
            printer = new SimpleCollectionPrinter();
        }

        public void setDataPrinter(IDataPrinter printer)
        {
            if (printer != null)
            {
                this.printer = printer;
            }
        }

        public void printAllData()
        {
            printer.printCustomers(data.getAllCustomers());
            printer.printRooms(data.getAllRooms());
            printer.printReservations(data.getAllReservations());
        }

        public void printRooms()
        {
            printer.printRooms(data.getAllRooms());
        }

        public void printCustomers()
        {
            printer.printCustomers(data.getAllCustomers());
        }

        public void printReservation()
        {
            printer.printReservations(data.getAllReservations());
        }

        public void printCustomersByName(string lastName)
        {
            Dictionary<int, Customer> customers = data.getCustomersByLastName(lastName);
            if (customers.Count() == 0)
            {
                printer.printWarning("Brak klienta o podanym nazwisku");
            }
            else
            {
                printer.printCustomers(customers);
            }
        }

        public void printCustomerByGroup(category group)
        {
            printer.printCustomers(data.getCustomersByCategory(group));
        }

        public void printReservationByRoomNumber(int roomNumber)
        {
            try
            {
                printer.printReservations(data.getReservationsByRoomNumber(roomNumber));
            }
            catch
            {
                printer.printWarning("Brak pokoju o podanym numerze");
            }
        }

        public void printReservationByCustomerId(int customerId)
        {
            try
            {
                printer.printReservations(data.getReservationsByCustomerId(customerId));
            }
            catch
            {
                printer.printWarning("Brak klienta o podanym numerze ID");
            }
        }

        public void printReservationByInterval(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                printer.printReservations(data.getReservationsByTimeInterval(dateFrom, dateTo));
            }
            catch
            {
                printer.printWarning("Podano niepoprawny zakres czasu");
            }
        }

        public void printReservationByCustomerName(string lastName)
        {
            try
            {
                printer.printReservations(data.getReservationsByCustomerName(lastName));
            }
            catch
            {
                printer.printWarning("Brak klienta o podanym nazwisku");
            }
        }

        public void addCustomer(string firstName, string lastName, category group)
        {
            try
            {
                data.addCustomer(firstName, lastName, group);
            }
            catch
            {
                printer.printWarning("Wprowadzoenie niekomplentne dane");
            }
        }

        public void addRoom(int roomNumber)
        {
            try
            {
                data.addRoom(roomNumber);
            }
            catch
            {
                printer.printWarning("Pokój o podanym numerze istnieje albo liczba jest po za zakresem");
            }
        }

        public void addReservation(string firstName, string lastName, int roomNumber, DateTime arrivalDate, DateTime departureDate)
        {
            Customer customer;
            Room room;
            try
            {
                customer = data.getCustomerByFirstAndLastName(firstName, lastName);
                room = data.getRoomByNumber(roomNumber);
            }
            catch
            {
                printer.printWarning("Niepoprawne Dane");
                return;
            }

            try
            {
                data.addReservation(customer, room, arrivalDate, departureDate);
            }
            catch
            {
                printer.printWarning("Niepoprawne Dane");
            }
        }

        public void deleteCustomer(string firstName, string lastName)
        {
            int customerId = data.getCustomerIdByFirstAndLastName(firstName, lastName);
            if (customerId == -1)
            {
                printer.printWarning("Niepoprawne Dane");
            }
            else
            {
                data.deleteCustomer(customerId);
            }
        }

        public void deleteRoom(int roomNumber)
        {
            try
            {
                data.deleteRoom(roomNumber);
            }
            catch
            {
                printer.printWarning("Brak pokoju o podanym numerze");
            }
        }

        public void deleteReservation(int resId)
        {
            try
            {
                data.deleteReservationById(resId);
            }
            catch
            {
                printer.printWarning("Brak rezerwacji o podanym numerze");
            }
        }

        public static List<Room> GetRoomsAtFloorLambda(List<Room> rooms, int floor)
        {
            if (rooms != null)
            {
                return rooms.FindAll(r => r.FloorNumber == floor).ToList();
            }
            else
            {
                throw new Exception("rooms is null");
            }

        }

        public static List<Room> GetRoomsAtFloorLinq(List<Room> rooms, int floor)
        {
            if (rooms != null)
            {
                List<Room> result;
                result = (from room in rooms
                          where (room.FloorNumber == floor)
                          select room).ToList();
                return result;
            }
            else
            {
                throw new Exception("rooms is null");
            }
        }

        public static List<Reservation> GetReservationsByTimeIntervalLambda(ICollection<Reservation> reservations, DateTime fromDate, DateTime toDate)
        {
            if (reservations != null & fromDate != null & toDate != null)
            {
                return reservations.Where(r => r.ArrivalDate.CompareTo(fromDate) >= 0
                        && r.ArrivalDate.CompareTo(toDate) <= 0).ToList();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static List<Reservation> GetReservationsByTimeIntervalLinq(ICollection<Reservation> reservations, DateTime fromDate, DateTime toDate)
        {
            if (reservations != null && fromDate != null && toDate != null)
            {
                List<Reservation> result = new List<Reservation>();
                result = (from res in reservations
                          where res.ArrivalDate.CompareTo(fromDate) >= 0
                          &&
                          res.ArrivalDate.CompareTo(toDate) <= 0
                          select res).ToList();
                return result;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static List<string> GetLastNamesFromReservationsLambda(ICollection<Reservation> reservations)
        {
            if (reservations != null)
            {
                return reservations.GroupBy(r => r.Customer.LastName).Select(group => group.First().Customer.LastName).ToList();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static List<string> GetLastNamesFromReservationsLinq(ICollection<Reservation> reservations)
        {
            if (reservations != null)
            {
                List<string> result;
                result = (from res in reservations
                          select res.Customer.LastName).Distinct().ToList();

                return result;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static void CompareListsLambda(List<Room> list1, List<Room> list2)
        {
            if (list1 != null && list2 != null)
            {
                foreach (Room room in list1.Where(a => a.RoomNumber <
                list2.ElementAt(list1.IndexOf(a)).RoomNumber))
                {
                    Console.Out.Write(room.RoomNumber + " " + list2.ElementAt(list1.IndexOf(room)).RoomNumber + "\n");
                }
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static void CompareListsLinq(List<Room> list1, List<Room> list2)
        {
            if (list1 != null && list2 != null)
            {
                var query = (from room1 in list1
                            from room2 in list2
                            where room1.RoomNumber < room2.RoomNumber
                            &&
                            list1.IndexOf(room1) == list2.IndexOf(room2)
                            select new { r1 = room1, r2 = room2 }).ToList();
                foreach (var pair in query)
                {
                    Console.Out.Write(pair.r1.RoomNumber + " " + pair.r2.RoomNumber + "\n");
                }
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static Room GetLeastNumberRoomLambda(List<Room> rooms)
        {
            if (rooms != null)
            {
                return rooms.OrderBy(a => a.RoomNumber).FirstOrDefault();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static Room GetLeastNumberRoomLinq(List<Room> rooms)
        {
            if (rooms != null)
            {
                Room room;
                room = (from r in rooms
                        where r.RoomNumber == rooms.Min(c => c.RoomNumber)
                        select r).FirstOrDefault();
                return room;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static Customer[] GetClientsWithReservationsLambda(ICollection<Reservation> reservation)
        {

            if (reservation != null)
            {
                return reservation.GroupBy(a => a.Customer.LastName).Where(d => d.Count() > 1).Select(a => a.First().Customer).ToArray();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static Customer[] GetClientsWithReservationsLinq(ICollection<Reservation> reservation)
        {

            if (reservation != null)
            {
                Customer[] customers;
                customers = (from res1 in reservation
                             group res1 by res1.Customer.LastName into group1
                             where group1.Count() > 1
                             select group1.First().Customer).ToArray();

                return customers;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }


        public static List<Reservation> GetDistinctReservationsLambda(ICollection<Reservation> reservation)
        {

            if (reservation != null)
            {
                return reservation.GroupBy(a => a.ArrivalDate).Where(d => d.Count() == 1).Select(a => a.First()).ToList();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static List<Reservation> GetDistinctReservationsLinq(ICollection<Reservation> reservation)
        {
            if (reservation != null)
            {
                List<Reservation> result;
                result = (from res in reservation
                          group res by res.ArrivalDate into group1
                          where group1.Count() == 1
                          select group1.First()).ToList();
                return result;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public List<Customer> getCustomerWithMostReservations()
        {
            List<Customer> result;
            int maxCount = data.getAllReservations().GroupBy(c => c.Customer).OrderByDescending(c => c.Count()).First().Count();
            result = ((from res in data.getAllReservations()
                       group res by res.Customer into group1
                       where group1.Count() == 2
                       select group1.First().Customer).ToList());
            Console.WriteLine(maxCount);
            return result;
        }

        public List<Customer>[] getCustomersSubLists()
        {
            List<Customer> customers = data.getAllCustomers().Values.ToList();
            int blockCount = (customers.Count() / 3) + 1;
            List<Customer>[] result = new List<Customer>[blockCount];

            for (int i = 0; i < customers.Count(); i++)
            {
                if (result[i / 3] == null)
                {
                    result[i / 3] = new List<Customer>();
                }
                result[i / 3].Add(customers[i]);
            }

            return result;
        }

        public List<Room> getNonReservedRooms()
        {
            List<Room> rooms = data.getAllRooms();
            ObservableCollection<Reservation> reservations = data.getAllReservations();
            List<Room> result = rooms.Where(c => !reservations.Any(d => d.Room.RoomNumber == c.RoomNumber)).ToList();
            return result;
        }

        public void printLog()
        {
            System.IO.StreamWriter logFile;
            List<string> log = data.getLog();
            using (logFile = new System.IO.StreamWriter(@"..\\log.txt", false))
            {
                foreach (String str in log)
                {
                    logFile.WriteLine(str);
                }
            }
        }

        public static List<SimpleClass> GetReservationsByTimeIntervalAsSClassLambda(ICollection<Reservation> reservations, DateTime fromDate, DateTime toDate)
        {
            if (reservations != null & fromDate != null & toDate != null)
            {
                return reservations.Where(r => r.ArrivalDate.CompareTo(fromDate) >= 0
                        && r.ArrivalDate.CompareTo(toDate) <= 0).
                        Select(b => new SimpleClass() { stringValue = Convert.ToString(b.Room.RoomNumber), intValue = b.ResID }).ToList();
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }

        public static List<SimpleClass> GetReservationsByTimeIntervalAsSClassLinq(ICollection<Reservation> reservations, DateTime fromDate, DateTime toDate)
        {
            if (reservations != null && fromDate != null && toDate != null)
            {
                List<SimpleClass> result;
                result = (from res in reservations
                             where res.ArrivalDate.CompareTo(fromDate) >= 0
                             &&
                             res.ArrivalDate.CompareTo(toDate) <= 0
                             select new SimpleClass() { stringValue = Convert.ToString(res.Room.RoomNumber), intValue = res.ResID }).ToList();
                return result;
            }
            else
            {
                throw new Exception("bad parameters");
            }
        }
    }

    public class SimpleClass
    {
        public string stringValue { get; set; }
        public int intValue { get; set; }
    }
}
