using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace OOP_console_spil
{
     class Weapon : Item
    {
        public int Damage { get; set; }
        public bool IsRenged {  get; set; } 

        public int Usesleft {  get; set; }


        public Weapon(string name, int damage, bool isRanged, int uses) : base(name)
        {
            Damage = damage;
            IsRenged = isRanged;
            Usesleft = uses;
        }

        public bool Hit()
        {
            if(Usesleft <= 0)
            {
                Console.WriteLine("Ingen flere angreb tilbage!");
                return false;
            }

            Usesleft -- ;
            return true;
        }

       



    }
}
