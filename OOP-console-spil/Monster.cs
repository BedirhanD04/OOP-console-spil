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

        public int MaxHealth { get; set; } = 100;


        private Random rand = new Random(); 


        public Monster(string name, int health, int damage)
        {
           Name = name;
           Health = health;
           Damage = damage;
           MaxHealth = health;

        }
       

        public void Attack(Player player)
        {
            int chance = rand.Next(100);

            if(chance < 30)
            {
                SpecialAttack(player);
            } 

            else
            {
                player.Health -= Damage;
                Console.WriteLine($"{Name} rammer dig med {Damage} skader!");
            }

        }

        public  virtual void SpecialAttack(Player player) { }
        
            
        


        
        
        

    }
}
