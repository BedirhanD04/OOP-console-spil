using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    class Drago : Monster
    {
        public Drago() : base("Drago", 100, 18) { }
        public override void SpecialAttack(Player player)
        {
            int dmg = Damage + 15;
            player.Health -= dmg;

            Console.WriteLine("Drago uses BURNING DRAGON!");
            Console.WriteLine($"You take {dmg} damage!");
        }
    }
}
