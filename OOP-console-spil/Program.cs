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

            Room north = new Room("Velkommen til Cyclones verden! Du befinder dig i øjeblikket i den nordlige skov.\nFor at undslippe skal du dræbe 3 forskellige monstre. Held og lykke! :)");

            Room west = new Room("Hejjj, velkommen til den vestlige skov!");
            Room east = new Room("Hejjj, velkommen til den eastlige skov!");
            Room south = new Room("Hejj, velkommen til den sydlig  skov!");
            Player player = new Player();

            player.CurrentRoom = north;
            player.StartRoom = north;

            north.West = west;
            north.East = east;
            north.South = south;

            west.East = north;
            east.West = north;
            south.North = north;

            west.Monster = new Skyress();
            east.Monster = new Preass();
            south.Monster = new Drago();

            Weapon sword = new Weapon("Sword", 20, false, 999);
            player.inventory.Add(sword);
            Potion potion = new Potion("Health Potion", 30);
            player.inventory.Add(potion);


            Console.WriteLine(player.CurrentRoom.Description);

            while (true)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Health: {player.Health}");
                Console.ResetColor();
                Console.WriteLine("Commands: [Go west], [Go, East], [Go South], [Go north],\n[Inventory], [Attack], [drink potion], [Map]");


                Console.Write("> ");
                string input = Console.ReadLine().ToLower();
                player.ShowMap();
                   

                if (input.StartsWith("go "))
                {
                    Console.Clear();
                    string direction = input.Split(' ')[1];
                    player.Move(direction);
                }
                else if (input == "inventory")
                {
                    Console.Clear();
                    player.ShowInventory();
                }
               
                else if (input.StartsWith("attack "))
                {
                    Console.Clear();
                    string weaponName = input.Substring(7);
                    var weapon = player.inventory.OfType<Weapon>().FirstOrDefault(w => w.Name.ToLower() == weaponName);

                    if (weapon != null && weapon.Hit())
                    {
                        Console.WriteLine($"You attack with {weapon.Name}");
                    }
                }

                else if (input == "map")
                {
                    Console.Clear();
                    player.ShowMap();
                         

                }


                else if (input == "drink potion")
                {
                    var foundPotion = player.inventory.OfType<Potion>().FirstOrDefault();

                    if (potion != null)
                    {
                        potion.Drink(player);
                        player.inventory.Remove(foundPotion);
                    }
                    else
                    {
                        Console.WriteLine("Du har ikke potion");
                    }
                }

            }



        }

       

    }

    
}
