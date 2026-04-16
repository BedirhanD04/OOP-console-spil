using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Threading;

namespace OOP_console_spil
{
    class Program
    {
        static void PrintCentered(string text) //location of menu
        {
            int windowWidth = Console.WindowWidth;
            int spaces = (windowWidth - text.Length) / 2;

            if (spaces < 0) spaces = 0;

            Console.WriteLine(new string(' ', spaces) + text);
        }
        //---------------------------------------------------------------------------------------------
        static void ShowMenu(Player player)//menu
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            PrintCentered("================================");
            PrintCentered("CYCLONE RPG");
            PrintCentered("================================");
            Console.ForegroundColor = ConsoleColor.Red;
            PrintCentered($"HP: {player.Health}");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCentered($"Bosses: {player.BossesKilled}/3");
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintCentered($"{player.CurrentRoom.Description}");
            PrintCentered("================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCentered("1) Move");
            PrintCentered("2) Inventory");
            PrintCentered("3) Potion");
            PrintCentered("0) Exit");
            PrintCentered("================================");
            Console.Write("> ");
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            // Description of rooms
            Room north = new Room("                    Velkommen til Cyclones verden! Du befinder dig i øjeblikket i den nordlige skov.\n                    For at undslippe skal du dræbe 3 forskellige monstre. Held og lykke! :)");

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

           
            //the loop
            while (true)
            {
                ShowMenu(player);//menu

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": // MOVE

                        Console.Clear();
                        Console.Write("Direction (north/south/east/west): ");
                        player.Move(Console.ReadLine().ToLower());
                        break;
                  

                    case "2": // INVENTORY

                        Console.Clear();
                        player.ShowInventory();
                        break;

                    case "3": // POTION

                        Console.Clear();
                        potion = player.inventory.OfType<Potion>().FirstOrDefault();
                        if (potion != null)
                        {
                            player.Health = Math.Min(player.Health + potion.HealAmount, 100);
                            player.inventory.Remove(potion);
                            Console.WriteLine("Potion used!");
                        }
                        else
                        {
                            Console.WriteLine("No potion!");
                        }
                        break;

                        case "4"://MAP

                        Console.Clear();
                        player.ShowMap();
                        break;

                    case "0":// closes the game
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }



        }



    }
}
