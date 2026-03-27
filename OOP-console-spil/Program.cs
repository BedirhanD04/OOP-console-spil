using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    public class Program
    {
        static void Main(string[] args)
        {
            Room room1 = new Room("You are in a dark cave.");
            Room room2 = new Room("You are in a forest.");

            room1.North = room2;
            room2.South = room1;

            Player player = new Player();
            player.CurrentRoom = room1;

            Console.WriteLine(player.CurrentRoom.Description);

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine().ToLower();

                if (input.StartsWith("go "))
                {
                    string direction = input.Split(' ')[1];
                    player.Move(direction);
                }
                else if (input == "inventory")
                {
                    player.ShowInventory();
                }
            }



        }
    }
}
