using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_console_spil
{
    class Drago : Monster
    {
        public Drago() : base("Drago", 100, 18) { }
        public override void SpecialAttack(Player player)
        {
            int damage2 = Damage + 15;
            player.Health -= damage2;

           
            Console.WriteLine("Drago bruger BURNING DRAGON!");
            Console.WriteLine($"Du tager {damage2} skade!");
        }
    }
}
