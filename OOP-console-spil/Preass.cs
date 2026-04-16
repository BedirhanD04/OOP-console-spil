using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_console_spil
{
   
    class Preass : Monster
    {
        public Preass() : base("Preass", 70, 12 ) { }
        public override void SpecialAttack(Player player)
        {
            int damage2 = Damage + 5;
            player.Health -= damage2;

            Console.WriteLine("Preass bruger BLUE Squall!");
            Console.WriteLine($"Du tager {damage2} skade!");
        }
    }
}
