using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
     class Player
    {
        public Room CurrentRoom { get; set; }

        public Room StartRoom { get; set; } 

        public int Health { get; set; } = 100;

        public int BossesKilled { get; set; } = 0;
        public List<Item> inventory { get; set; } = new List<Item>();

        public void Move(string direction)
        {
            Room nextRoom = null;

            switch (direction)
            {
                case "north": nextRoom = CurrentRoom.North; break;
                case "south": nextRoom = CurrentRoom.South; break;
                case "east": nextRoom = CurrentRoom.East; break;
                case "west": nextRoom = CurrentRoom.West; break;
            }

            if (nextRoom == null)
            {
                Console.WriteLine("You can't go that way.");
                return;
            }

            
            if (nextRoom == CurrentRoom.South && BossesKilled < 2)
            {
                Console.WriteLine("\"Du skal først besejre VEST- og ØST-bosserne!\"");
                return;
            }

            CurrentRoom = nextRoom;

           
            Console.WriteLine(CurrentRoom.Description);

            if (CurrentRoom.Monster != null)
            {
                Fight(CurrentRoom.Monster);
                CurrentRoom.Monster = null;
            }
        }

        public void ShowInventory()
        {
            foreach(var item in inventory)
            {
                Console.WriteLine("- " + item.Name);
            }
        }

      public void Fight(Monster monster)
      {
            Console.WriteLine($"{monster.Name} her for at dræbe dig!!!");

            while(Health >  0 && monster.Health > 0)
            {
                


                Console.WriteLine("\nattack / run");
                string input = Console.ReadLine().ToLower();


                if (input == "attack")
                {
                    Console.WriteLine("Hvad vil du angribe med? ");

                    foreach(var w in inventory.OfType<Weapon>())
                    {
                        Console.WriteLine("- " + w.Name);
                    }

                    string weaponChoise = Console.ReadLine().ToLower();  

                    var weapon = inventory.OfType<Weapon>().FirstOrDefault(w => w.Name.ToLower() == weaponChoise);  

                    if(weapon != null && weapon.Hit())
                    {
                        monster.Health -= weapon.Damage;
                        Console.WriteLine($"Du rammer {monster.Name} for {weapon.Damage}");
                    }

                    else
                    {
                        Console.WriteLine("Ugyldigt våben!");
                    }
                
                
                }


                else if(input == "run")
                {
                    Console.Clear();
                    Console.WriteLine("Du slap ud!");
                    return;
                }



                if(monster.Health > 0)
                {
                    monster.Attack(this);
                    Console.WriteLine($"Din HP: {Health}");
                    Console.WriteLine($"{monster.Name} HP: {monster.Health}");
                    Console.WriteLine();
                }
            } 


            
        if (Health > 0)
            {
                Console.Clear();
                Console.WriteLine($"Du besejrede {monster.Name}!");
                BossesKilled++;
               
                GiveReward();

                if (BossesKilled == 3)
                {
                    Console.WriteLine("Tillykke! Du besejrede alle bosser!!");
                    Environment.Exit(0);
                }
            }



            else
            {
                Console.WriteLine("Du er død...");
                Environment.Exit(0);
            }

        }

        public void GiveReward()
        {
            if (BossesKilled == 1)
            {
                var bow = new Weapon("Bow", 25, true, 15);
                inventory.Add(bow);
                Console.WriteLine("Du har modtaget en bue!");
                

            }
            else if (BossesKilled == 2)
            {
                var axe = new Weapon("Axe", 35, false, 999);
                inventory.Add(axe);
                Console.WriteLine("Du har modtaget en økse!");
            }
            else if (BossesKilled == 3)
            {
                var ultimate = new Weapon("Ultimate Sword", 50, false, 999);
                inventory.Add(ultimate);
                Console.WriteLine("Du har modtaget det ultimative sværd!");
            }
        }


        public void ShowMap()
        {
            Console.WriteLine("\n=== World MAP ===");


            string west = CurrentRoom == StartRoom.West ? "[YOU]" : "WEST";
            string east = CurrentRoom == StartRoom.East ? "[YOU]" : "EAST";
            string south = CurrentRoom == StartRoom.South ? "[YOU]" : "SOUTH";
            string north = CurrentRoom == StartRoom ? "[YOU]" : "NORTH";

            Console.WriteLine($"        {north}");
            Console.WriteLine($"   {west}       {east}");
            Console.WriteLine($"        {south}");

            Console.WriteLine("=================\n");
        }


    }
}
