using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_console_spil
{
    class HealthBar
    {

        public void ShowHealthBar(string name, int current, int max)
        {
            int barLength = 20;
            double percent = (double)current / max;
            int filled = (int)(percent * barLength);

            if (filled < 0) filled = 0;
            if (filled > barLength) filled = barLength;

            string bar = new string('█', filled) + new string('░', barLength - filled);
            Console.WriteLine($"{name} HP: [{bar}] {current}/{max}");
        }


    }
}
