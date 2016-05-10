using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Model
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public Room (int roomNumber)
        {
            RoomNumber = roomNumber;
            FloorNumber = roomNumber / 100;
        }

        public static bool operator >(Room room1, Room room2)
        {
            return room1.RoomNumber > room2.RoomNumber;
        }

        public static bool operator < (Room room1, Room room2)
        {
            return room1.RoomNumber < room2.RoomNumber;
        } 

        public override string ToString()
        {
            return String.Format("room no. {0} on {1} floor", RoomNumber, FloorNumber);
        }
    }


    
}
