using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    class Potion : Item
    {
        public int HealAmount {  get; set; }

        public Potion(string name, int heal) : base(name)
        {
            HealAmount = heal;
        }

        public void Drink(Player player)
        {
            player.Health = Math.Min(player.Health + HealAmount, 100);
            Console.WriteLine($"You drank {Name} and healed {HealAmount} HP!");
        }
    }
}
