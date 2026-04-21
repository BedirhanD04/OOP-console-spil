using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace OOP_console_spil
{
    internal class SaveSystem
    {
        private static string savePath = "savee.csv";

        // Finds which room the monster is currently in, returns "dead" if not found
        private static string FindMonsterRoom(Player player, Room west, Room east, Room south, string name)
        {
            Room north = player.StartRoom;
            if (north.Monster != null && north.Monster.Name == name) return "north";
            if (west.Monster != null && west.Monster.Name == name) return "west";
            if (east.Monster != null && east.Monster.Name == name) return "east";
            if (south.Monster != null && south.Monster.Name == name) return "south";
            return "dead";
        }

        // Removes the monster with the given name from all rooms
        private static void ClearMonster(Room north, Room west, Room east, Room south, string name)
        {
            if (north.Monster != null && north.Monster.Name == name) north.Monster = null;
            if (west.Monster != null && west.Monster.Name == name) west.Monster = null;
            if (east.Monster != null && east.Monster.Name == name) east.Monster = null;
            if (south.Monster != null && south.Monster.Name == name) south.Monster = null;
        }

        // Creates a new monster instance based on its name
        private static Monster CreateMonster(string name)
        {
            switch (name)
            {
                case "Skyres": return new Skyress();
                case "Preass": return new Preass();
                case "Drago": return new Drago();
                default: return null;
            }
        }

        // Saves the current game state to a CSV file
        public static void Save(Player player, Room west, Room east, Room south)
        {
            List<string> lines = new List<string>();

            // Save player health, bosses killed and current room
            string room = "north";
            if (player.CurrentRoom == player.StartRoom.West) room = "west";
            else if (player.CurrentRoom == player.StartRoom.East) room = "east";
            else if (player.CurrentRoom == player.StartRoom.South) room = "south";
            lines.Add($"player,{player.Health},{player.BossesKilled},{room}");

            // Save all items in the player's inventory
            foreach (var item in player.inventory)
            {
                if (item is Weapon w)
                    lines.Add($"weapon,{w.Name},{w.Damage},{w.IsRenged},{w.Usesleft}");
                else if (item is Potion p)
                    lines.Add($"potion,{p.Name},{p.HealAmount}");
            }

            // Save the current location of each boss
            lines.Add($"boss,Skyres,{FindMonsterRoom(player, west, east, south, "Skyres")}");
            lines.Add($"boss,Preass,{FindMonsterRoom(player, west, east, south, "Preass")}");
            lines.Add($"boss,Drago,{FindMonsterRoom(player, west, east, south, "Drago")}");

            File.WriteAllLines(savePath, lines);
            Console.WriteLine("spil gemt!");
        }

        // Loads a previously saved game state from the CSV file
        public static void Load(Player player, Room west, Room east, Room south)
        {
            // Check if save file exists
            if (!File.Exists(savePath))
            {
                Console.WriteLine("Logfilen blev ikke fundet!");
                return;
            }

            string[] lines = File.ReadAllLines(savePath);

            // Clear current inventory before loading
            player.inventory.Clear();

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                // Restore player stats and position
                if (parts[0] == "player")
                {
                    player.Health = int.Parse(parts[1]);
                    player.BossesKilled = int.Parse(parts[2]);
                    string room = parts[3];

                    // Set the player's current room based on saved location
                    switch (room)
                    {
                        case "west": player.CurrentRoom = player.StartRoom.West; break;
                        case "east": player.CurrentRoom = player.StartRoom.East; break;
                        case "south": player.CurrentRoom = player.StartRoom.South; break;
                        default: player.CurrentRoom = player.StartRoom; break;
                    }
                }

                // Restore weapons from inventory
                else if (parts[0] == "weapon")
                {
                    var w = new Weapon(parts[1], int.Parse(parts[2]), bool.Parse(parts[3]), int.Parse(parts[4]));
                    player.inventory.Add(w);
                }

                // Restore potions from inventory
                else if (parts[0] == "potion")
                {
                    var p = new Potion(parts[1], int.Parse(parts[2]));
                    player.inventory.Add(p);
                }

                // Restore boss locations
                else if (parts[0] == "boss")
                {
                    string monsterName = parts[1];
                    string location = parts[2];

                    // Remove the monster from all rooms before placing it
                    ClearMonster(player.StartRoom, west, east, south, monsterName);

                    // If the boss is dead, skip placing it
                    if (location == "dead") continue;

                    // Place the monster in the correct room
                    Monster m = CreateMonster(monsterName);
                    switch (location)
                    {
                        case "north": player.StartRoom.Monster = m; break;
                        case "west": west.Monster = m; break;
                        case "east": east.Monster = m; break;
                        case "south": south.Monster = m; break;
                    }
                }
            }
            Console.WriteLine("Spillet er indlæst!!");
        }
    }
}
