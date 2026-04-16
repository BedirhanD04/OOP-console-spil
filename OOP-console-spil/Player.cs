using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.IO;
using System.Threading;

namespace OOP_console_spil
{
    class Player
    {
        //property
        public Room CurrentRoom { get; set; }

        public Room StartRoom { get; set; }

        public int Health { get; set; } = 100;

        public int BossesKilled { get; set; } = 0;

        public int MaxHealth { get; set; } = 100;

        public Room PreviousRoom { get; set; }
        public List<Item> inventory { get; set; } = new List<Item>();



        //------------------------------------------------------------------------------------------------------------------------------------ 
        public void Move(string direction) //This method allows the player to move between rooms.
        {
            Room nextRoom = null;

            switch (direction)//Rooms
            {
                case "north": nextRoom = CurrentRoom.North; break;
                case "south": nextRoom = CurrentRoom.South; break;
                case "east": nextRoom = CurrentRoom.East; break;
                case "west": nextRoom = CurrentRoom.West; break;
            }

            if (nextRoom != null) //If its not null
            {
                PreviousRoom = CurrentRoom;
                CurrentRoom = nextRoom;
                Console.WriteLine(CurrentRoom.Description);
            }

            if (nextRoom == null) // If its null
            {
                Console.WriteLine("Du kan ikke gå den vej.");
                return;
            }


            if (nextRoom == CurrentRoom.South && BossesKilled < 2) // It prevents the player from going to the third boss without killing the first two.
            {
                Console.WriteLine("Du skal først besejre VEST- og ØST-bosserne!");
                return;
            }



            if (CurrentRoom.Monster != null) //It starts a fight with the boss in the room.
            {
                Fight(CurrentRoom.Monster);
                if (CurrentRoom.Monster != null && CurrentRoom.Monster.Health <= 0)
                {
                    CurrentRoom.Monster = null;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------
        public void ShowInventory()//This metos shows inventory
        {
            Console.WriteLine("=== INVENTORY ===");
            foreach (var item in inventory)
            {
                Console.WriteLine("- " + item.Name);
            }
        }
        //______________________________________________________________________________________________________________________________
        public void Fight(Monster monster)//This method controls the fight.
        {
            Console.WriteLine($"{monster.Name} her for at dræbe dig!!!");// Message to Player

            while (Health > 0 && monster.Health > 0)//The loop continues until the monster or the player dead.
            {
                //Shows the Monsters and The players hp
                ShowHealthBar("Monster", monster.Health, monster.MaxHealth);
                ShowHealthBar("Player", this.Health, this.MaxHealth);

                //write input
                Console.WriteLine("\nattack / run");
                //read input
                string input = Console.ReadLine().ToLower();


                //This condition allows the player to attack.
                if (input == "attack")
                {
                    Console.WriteLine("Hvad vil du angribe med? ");

                    foreach (var w in inventory.OfType<Weapon>()) //Shows all weapons in inventory
                    {
                        Console.WriteLine("- " + w.Name);
                    }

                    string weaponChoise = Console.ReadLine().ToLower();   //Read weapon Choise

                    var weapon = inventory.OfType<Weapon>().FirstOrDefault(w => w.Name.ToLower() == weaponChoise);  //Find the first weapon in the inventory whose name matches the user's name.

                    if (weapon != null && weapon.Hit())//Checking if there is a weapon and if it is working.
                    {
                        Console.Clear();
                        monster.Health -= weapon.Damage;
                        Console.WriteLine($"Du rammer {monster.Name} for {weapon.Damage}");
                    }

                    else // if not
                    {
                        Console.WriteLine("Ugyldigt våben!");
                    }


                }


                else if (input == "run")//This condition allows the player to escape the room.
                {
                    Console.Clear();
                    Console.WriteLine("Du flygtede!");

                    CurrentRoom = PreviousRoom;
                    return;
                }


                if (monster.Health > 0)//The monster will attack until it dies.
                {
                    monster.Attack(this);

                }

                if (Health < MaxHealth * 0.3)//Warning if HP drops below 30%
                {
                    Console.WriteLine();
                    Console.WriteLine("               !!!!!LOW HP!!!!!");
                }

            }



            if (Health > 0)//Message for killing the monster
            {
                Console.Clear();
                Console.WriteLine($"Du besejrede {monster.Name}!");
                BossesKilled++; //Count the monster you killed         
                GiveReward(); //gives you a reward for killing the monster.
                DropLoot(); // Drops random item

                if (BossesKilled == 3)//You'll see this message when you've killed all the bosses.
                {
                    Console.WriteLine("Tillykke! Du besejrede alle bosser!!");
                    Environment.Exit(0);//It closes the program immediately and stops working.
                }
            }



            else
            {
                Console.WriteLine("Du er død...");//message when you are dead
                Environment.Exit(0);//It closes the program immediately and stops working.
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void GiveReward()//this method gives reward to player
        {
            if (BossesKilled == 1)//reward when the first boss is killed
            {
                var bow = new Weapon("Bow", 25, true, 15);
                inventory.Add(bow);
                Console.WriteLine("Du har modtaget en Bow!");


            }
            else if (BossesKilled == 2)//reward when the second boss is killed
            {
                var axe = new Weapon("Axe", 35, false, 999);
                inventory.Add(axe);
                Console.WriteLine("Du har modtaget en Axe!");
            }
            else if (BossesKilled == 3)//Reward when the third boss is killed
            {
                var ultimate = new Weapon("Murasame Blade", 50, false, 999);
                inventory.Add(ultimate);
                Console.WriteLine("Du har modtaget det Murasame Blade!");
            }
        }
        //__________________________________________________________________________________________________________________________________________________________

        public void ShowMap()// this metod shows map
        {
            Console.WriteLine("=== World MAP ===");


            string west = CurrentRoom == StartRoom.West ? "[YOU]" : "WEST";
            string east = CurrentRoom == StartRoom.East ? "[YOU]" : "EAST";
            string south = CurrentRoom == StartRoom.South ? "[YOU]" : "SOUTH";
            string north = CurrentRoom == StartRoom ? "[YOU]" : "NORTH";

            Console.WriteLine($"        {north}");
            Console.WriteLine($"   {west}       {east}");
            Console.WriteLine($"        {south}");

            Console.WriteLine("=================\n");
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------

        public void ShowHealthBar(string name, int current, int max)// shows the HealthBar
        {
            int barLength = 20;
            double percent = (double)current / max;
            int filled = (int)(percent * barLength);

            if (filled < 0) filled = 0;
            if (filled > max) max = filled;
            string bar = new string('█', filled) + new string('░', barLength - filled);
            Console.WriteLine($"{name} HP: [{bar}] {current}/{max}");
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public void DropLoot() // drops random loot (gift)
        {
            Random random = new Random();
            int roll = random.Next(100);

            if (roll < 50)
            {
                var potion = new Potion("Health Potion", 30);//There's a 50% chance it will give a potion.
                inventory.Add(potion);
                Console.WriteLine("Du fandt en Health Potion!");
            }

            else
            {
                var weapon = new Weapon("Trance Sword", 40, false, 15);//There's a 50% chance it will give a sword.
                inventory.Add(weapon);
                Console.WriteLine("Du fandt et Trance Sword!");
            }
        }
    }


   
}
