using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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
            player.Health = Math.Min(player.Health + HealAmount, player.MaxHealth);
            player.inventory.Remove(this);
            Console.WriteLine($"{Name} Brugt! Du fik + {HealAmount} HP!");
        }
    }
}
