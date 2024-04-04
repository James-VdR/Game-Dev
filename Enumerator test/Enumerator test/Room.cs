using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enumerator_test;

namespace SimpleGame
{
    internal class Room
    {
        private string roomName;
        private string description;
        int currentSelection = 0;


        private List<Room> connectedRooms = new List<Room>();
        private List<Item> items = new List<Item>();

        public Room(string aRoomName)
        {
            roomName = aRoomName;
        }

        public Room(string aRoomName, string aDescription)
        {
            roomName = aRoomName;
            description = aDescription;
        }

        public void AddRoom(Room room)
        {
            connectedRooms.Add(room);
        }
        public List<Room> GetConnectedRooms()
        {
            return connectedRooms;
        }
        public string GetRoomName()
        {
            return roomName;
        }

        public void AddItem(Item anItem)
        {
            items.Add(anItem);
        }
        public List<Item> GetItems()
        {
            return items;
        }
        public void RemoveItem(Item anItem)
        {
            items.Remove(anItem);
        }
    }
}