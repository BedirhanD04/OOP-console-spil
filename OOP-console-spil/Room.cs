using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOP_console_spil
{
     class Room
    {


        public string Description {  get; set; }

        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public Monster Monster { get; set; }

        public Room(string description) 
        {
            Description = description;
        }





    }

  
}
