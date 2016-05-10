using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Model
{
    public class Reservation
    {
        public Customer Customer { get; set; }
        public Room Room { get; set; }
        public int ResID { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public Reservation(Customer customer, Room room, int resID, DateTime arrivalDate, DateTime departureDate)
        {
            Customer = customer;
            Room = room;
            ResID = resID;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
        }

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            returnString.Append("Res no." + ResID.ToString().PadLeft(2));
            returnString.Append(";room " + Room.RoomNumber);
            returnString.Append(" by" + Customer.FirstName.ToString().PadLeft(15));
            returnString.Append(Customer.LastName.ToString().PadLeft(12));
            returnString.Append(" from" + ArrivalDate.ToShortDateString().PadLeft(11));
            returnString.Append(" to" + DepartureDate.ToShortDateString().PadLeft(11));
            return returnString.ToString();
        }
    }
}
