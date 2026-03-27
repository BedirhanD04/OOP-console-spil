using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
     class Player
    {
        public Room CurrentRoom { get; set; }
        public int Health { get; set; } = 100;
        public List<Item> inventory { get; set; } = new List<Item>();

        public void Move(string direction)
        {
            switch (direction)
            {
                case "north":
                    {
                        if (CurrentRoom.North != null) 
                            CurrentRoom = CurrentRoom.North;
                        break;
                    }
                case "south":
                    {
                        if(CurrentRoom.South != null)
                            CurrentRoom = CurrentRoom.South;
                        break;
                    }
                case "east":
                    {
                        if( CurrentRoom.East != null)
                            CurrentRoom = CurrentRoom.East;
                        break;
                    }
                case "west":
                    {
                        if(CurrentRoom.West != null)
                            CurrentRoom = CurrentRoom.West;
                        break;
                    }

            }
            Console.WriteLine(CurrentRoom.Description);

        }

        public void ShowInventory()
        {
            foreach(var item in inventory)
            {
                Console.WriteLine(item.Name);
            }
        }





    }
}
