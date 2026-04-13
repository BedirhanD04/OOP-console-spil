using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
     class Skyress : Monster
    {
        public Skyress() : base ("Skyres", 40, 8) { }

        public override void SpecialAttack(Player player)
        {
            int damage2 = Damage + 10;
            player.Health -= damage2;

            Console.WriteLine("Skyress bruger WINDS OF FURY!");
            Console.WriteLine($"Du tager {damage2} skade!");
        }
    }
}
