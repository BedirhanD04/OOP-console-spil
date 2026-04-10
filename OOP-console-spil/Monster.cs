using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    internal class Monster
    {
        public string Name { get; set; }    
        public int Health { get; set; }
        public int Damage { get; set; }

        public Monster(string name, int health, int damage)
        {
           Name = name;
           Health = health;
           Damage = damage;

        }
       

        public void Attack(Player player)
        {
            player.Health -= Damage;
            Console.WriteLine($"{Name} hits you for {Damage} damage!");
        }


        
        
        

    }
}
