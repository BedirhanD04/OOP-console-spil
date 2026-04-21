using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
     class ControlFight
    {
       
        public void Fight(Player player, Monster monster)//This method controls the fight.

        {
            HealthBar uI = new HealthBar();


            Console.WriteLine($"{monster.Name} her for at dræbe dig!!!");// Message to Player

            while (player.Health > 0 && monster.Health > 0)//The loop continues until the monster or the player dead.
            {
                //Shows the Monsters and The players hp
                uI.ShowHealthBar("Monster", monster.Health, monster.MaxHealth);
                uI.ShowHealthBar("Player", player.Health, player.MaxHealth);

                //write input
                Console.WriteLine("attack / run");
                //read input
                string input = Console.ReadLine().ToLower();


                //This condition allows the player to attack.
                if (input == "attack")
                {
                    Console.WriteLine("Hvad vil du angribe med? ");

                    foreach (var w in player.inventory.OfType<Weapon>()) //Shows all weapons in inventory
                    {
                        Console.WriteLine("- " + w.Name);
                    }

                    string weaponChoise = Console.ReadLine().ToLower();   //Read weapon Choise

                    var weapon = player.inventory.OfType<Weapon>().FirstOrDefault(w => w.Name.ToLower() == weaponChoise);  //Find the first weapon in the inventory whose name matches the user's name.

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


                else if (input == "run")
                {
                    Console.Clear();
                    Console.WriteLine("Du flygtede!");

                    Random rand = new Random();
                    int chance = rand.Next(100);
                    Room escapeRoom = player.PreviousRoom;

                    if (chance < 40)
                    {
                        // Are there any other monsters in the room you escaped from?
                        if (escapeRoom.Monster == null)
                        {
                            player.CurrentRoom.Monster = null;
                            escapeRoom.Monster = monster;
                            Console.WriteLine($"!!! {monster.Name} Følger dig!!!!!!");
                        }
                        else
                        {
                            //There's already a monster in the room, it can't keep up.
                            Console.WriteLine($"{monster.Name} ville følge dig, men vejen var blokeret!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Name} fulgte dig ikke, så du er i sikkerhed for nu.");
                    }

                    player.CurrentRoom = escapeRoom;
                    return;
                }


                if (monster.Health > 0)//The monster will attack until it dies.
                {
                    monster.Attack(player);

                }

                if (player.Health < player.MaxHealth * 0.3)//Warning if HP drops below 30%
                {
                    Console.WriteLine();
                    Console.WriteLine("               !!!!!LOW HP!!!!!");
                }

            }



            if (player.Health > 0)//Message for killing the monster
            {
                Console.Clear();
                Console.WriteLine($"Du besejrede {monster.Name}!");
                monster.IsDead = true;
                player.BossesKilled++; //Count the monster you killed         
                player.GiveReward(); //gives you a reward for killing the monster.
                player.DropLoot(); // Drops random item

                if (player.BossesKilled == 3)//You'll see this message when you've killed all the bosses.
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
    }
}
