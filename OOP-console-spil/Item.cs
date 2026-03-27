using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    internal class Item
    {
        public string Name { get; set; }
        

        public Item(string name) 
        {
            Name = name;
        }

        public virtual void use () 
        {
            Console.WriteLine($"You used {Name}");
        }


    }
}
