using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_console_spil
{
     class Program
    {
        static void Main(string[] args)
        {
            // Description of rooms
            Room north = new Room("Velkommen til Cyclones verden! Du befinder dig i øjeblikket i den nordlige skov.\nFor at undslippe skal du dræbe 3 forskellige monstre. Held og lykke! :)");

            Room west = new Room("Hej, velkommen til den vestlige skov!");
            Room east = new Room("Hej, velkommen til den eastlige skov!");
            Room south = new Room("Hej, velkommen til den sydlig  skov!");

            //Object instantiation
            Player player = new Player();

            //Room connection
            player.CurrentRoom = north;
            player.StartRoom = north;

            north.West = west;
            north.East = east;
            north.South = south;

            west.East = north;
            east.West = north;
            south.North = north;

            //Object instantiation
            west.Monster = new Skyress();
            east.Monster = new Preass();
            south.Monster = new Drago();

            //giving the player a potion and a sword
            Weapon sword = new Weapon("Sword", 20, false, 999);
            player.inventory.Add(sword);
            Potion potion = new Potion("Health Potion", 30);
            player.inventory.Add(potion);

            // Showing Description of rooms
            Console.WriteLine(player.CurrentRoom.Description);

            //the loop
            while (true)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                //A small menu that shows commands to player
                Console.WriteLine("Commands: [Go west], [Go, East], [Go South], [Go north],\n[Inventory], [drink potion], [Map]");

                //shows players health with red color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Health: {player.Health}");
                Console.ResetColor();

                //The player enters the input here.
                Console.Write("> ");
                //Reads the input
                string input = Console.ReadLine().ToLower();
                //Shows map
                player.ShowMap();

                //This condition allows the player to move between rooms.
                if (input.StartsWith("go "))
                {
                    Console.Clear();
                    string direction = input.Split(' ')[1];
                    player.Move(direction);
                }
                //This condition shows inventory
                else if (input == "inventory")
                {
                    Console.Clear();
                    player.ShowInventory();
                }

                //This condition shows world map
                else if (input == "map")
                {
                    Console.Clear();
                    player.ShowMap();
                         

                }
                //This condition makes the player drink the potion.
                else if (input == "drink potion")
                {
                    var foundPotion = player.inventory.OfType<Potion>().FirstOrDefault();

                    if (potion != null)
                    {
                        potion.Drink(player);
                        player.inventory.Remove(foundPotion);
                    }
                    //if you dont have any potion
                    else
                    {
                        Console.WriteLine("Du har ikke potion");
                    }
                }


                

            }



        }

       

    }

    
}
