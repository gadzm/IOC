using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using IOC.Interfaces;
using IOC.Model;
using IOC.Services;

namespace IOC.Exstensions
{
    public static class DataServiceExtensionMethods
    {
        public static List<Customer> GetBestCustomer(this DataService data)
        {
            
            return data.getCustomerWithMostReservations();
        }

        public static List<Customer>[] GetCustomerInSublists(this DataService data) //by 3
        {
            return data.getCustomersSubLists();
        }

        public static List<Room>GetUnusedRooms(this DataService data)
        {
            return data.getNonReservedRooms();
        }
    }
}
