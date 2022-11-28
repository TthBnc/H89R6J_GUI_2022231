using System;
using System.Linq;
using System.Collections.Generic;
using JEH01V_HFT_2021222.Models;
using ConsoleTools;

namespace JEH01V_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;

        static void List(string entity)
        {
            if (entity == "Character")
            {
                List<Character> characters = rest.Get<Character>("character");
                foreach (var item in characters)
                {
                    Console.WriteLine($"{item.Id}: {item.Name} ({item.Element})");
                }
                Console.WriteLine("\n***Characters listed!***");
            }
            if (entity == "Artifact")
            {
                List<Artifact> artifacts = rest.Get<Artifact>("artifact");
                foreach (var item in artifacts)
                {
                    Console.WriteLine($"{item.Id}: {item.Name} ({item.Cost})");
                }
                Console.WriteLine("\n***Artifacts listed!***");
            }
            if (entity == "Weapon")
            {
                List<Weapon> weapons = rest.Get<Weapon>("weapon");
                foreach (var item in weapons)
                {
                    Console.WriteLine($"{item.Id}: {item.Name} ({item.PeakDmg})");
                }
                Console.WriteLine("\n***Weapons listed!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Create(string entity)
        {
            if (entity == "Character")
            {
                Console.Write("Enter the new character's ID: ");
                int charID = int.Parse(Console.ReadLine());
                Console.Write("Enter the character's name: ");
                string charName = Console.ReadLine();
                Console.Write("Enter the character's element: ");
                string element = Console.ReadLine();
                rest.Post(new Character() { Id = charID, Name = charName, Element = element }, "character");
                Console.WriteLine("\n***New character created!***");
            }
            else if (entity == "Artifact")
            {
                Console.Write("Enter the new artifact's ID: ");
                int artID = int.Parse(Console.ReadLine());
                Console.Write("Enter the artifact's name: ");
                string artName = Console.ReadLine();
                Console.Write("Enter the artifact's cost: ");
                int cost = int.Parse(Console.ReadLine());
                rest.Post(new Artifact() { Id = artID, Name = artName, Cost = cost }, "artifact");
                Console.WriteLine("\n***New artifact created!***");
            }
            else if (entity == "Weapon")
            {
                Console.Write("Enter the new weapon's ID: ");
                int weapID = int.Parse(Console.ReadLine());
                Console.Write("Enter the weapon's name: ");
                string weapName = Console.ReadLine();
                Console.Write("Enter the weapon's peak DMG: ");
                int peakDMG = int.Parse(Console.ReadLine());
                rest.Post(new Weapon() { Id = weapID, Name = weapName, PeakDmg = peakDMG }, "weapon");
                Console.WriteLine("\n***New weapon created!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "Character")
            {
                Console.Write("Enter the character's ID you want to update: ");
                int charID = int.Parse(Console.ReadLine());
                Character charToBeUpdated = rest.Get<Character>(charID, "character");
                Console.Write($"Enter the new name for this character [old name: {charToBeUpdated.Name}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new element for this character [old element: {charToBeUpdated.Element}]: ");
                string newElement = Console.ReadLine();
                charToBeUpdated.Name = newName;
                charToBeUpdated.Element = newElement;
                rest.Put(charToBeUpdated, "character");
                Console.WriteLine("\n***Character updated!***");
            }
            if (entity == "Artifact")
            {
                Console.Write("Enter the artifact's ID you want to update: ");
                int artID = int.Parse(Console.ReadLine());
                Artifact artToBeUpdated = rest.Get<Artifact>(artID, "artifact");
                Console.Write($"Enter the new name for this artifact [old name: {artToBeUpdated.Name}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new cost for this artifact [old cost: {artToBeUpdated.Cost}]: ");
                int newCost = int.Parse(Console.ReadLine());
                artToBeUpdated.Name = newName;
                artToBeUpdated.Cost = newCost;
                rest.Put(artToBeUpdated, "artifact");
                Console.WriteLine("\n***Artifact updated!***");
            }
            if (entity == "Weapon")
            {
                Console.Write("Enter the weapon's ID you want to update: ");
                int weapID = int.Parse(Console.ReadLine());
                Weapon weapToBeUpdated = rest.Get<Weapon>(weapID, "weapon");
                Console.Write($"Enter the new name for this weapon [old name: {weapToBeUpdated.Name}]: ");
                string newName = Console.ReadLine();
                Console.Write($"Enter the new peak DMG for this weapon [old element: {weapToBeUpdated.PeakDmg}]: ");
                int newPeakDMG = int.Parse(Console.ReadLine());
                weapToBeUpdated.Name = newName;
                weapToBeUpdated.PeakDmg = newPeakDMG;
                rest.Put(weapToBeUpdated, "weapon");
                Console.WriteLine("\n***Weapon updated!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void Delete(string entity)
        {
            if (entity == "Character")
            {
                Console.Write("Enter the character's ID you want to delete: ");
                int charID = int.Parse(Console.ReadLine());
                rest.Delete(charID, "character");
                Console.WriteLine("\n***Character deleted!***");
            }
            if (entity == "Artifact")
            {
                Console.Write("Enter the artifact's ID you want to delete: ");
                int artID = int.Parse(Console.ReadLine());
                rest.Delete(artID, "artifact");
                Console.WriteLine("\n***Artifact deleted!***");
            }
            if (entity == "Weapon")
            {
                Console.Write("Enter the weapon's ID you want to delete: ");
                int weapID = int.Parse(Console.ReadLine());
                rest.Delete(weapID, "weapon");
                Console.WriteLine("\n***Weapon deleted!***");
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        static void NonCruds(string entity, string method)
        {
            if (entity == "Character")
            {
                if (method == "WeaponsOfGivenElement")
                {
                    Console.WriteLine("[Anemo] [Cryo] [Electro] [Geo] [Hydro] [Pyro]");
                    Console.Write("Enter the element you want to get the weapons for: ");
                    string element = Console.ReadLine();
                    List<Weapon> weapons = rest.Get<Weapon>(element, "character/WeaponsOfGivenElement");
                    Console.WriteLine();
                    foreach (var item in weapons)
                    {
                        Console.WriteLine($"{item.Id}: {item.Name} - {item.PeakDmg}");
                    }
                    Console.WriteLine("\n***Weapons listed!***");
                }
                else if (method == "ArtifactsOfGivenElement")
                {
                    Console.WriteLine("[Anemo] [Cryo] [Electro] [Geo] [Hydro] [Pyro]");
                    Console.Write("Enter the element you want to get the artifacts for: ");
                    string element = Console.ReadLine();
                    List<Artifact> artifacts = rest.Get<Artifact>(element, "character/ArtifactsOfGivenElement");
                    Console.WriteLine();
                    foreach (var item in artifacts)
                    {
                        Console.WriteLine($"{item.Id}: {item.Name} - {item.Cost}");
                    }
                    Console.WriteLine("\n***Artifacts listed!***");
                }
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
            else if (entity == "Artifact")
            {
                if (method == "HighestCostArtifactUser")
                {
                    Console.WriteLine("Character(s) using the artifact with the highest cost: \n");
                    var result = rest.Get<string>("artifact/HighestCostArtifactUser");
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\n***Character(s) listed!***");
                }
                else if (method == "ArtifactAverageCostByCharacterName")
                {
                    Console.Write("Enter the desired character's name: ");
                    string charName = Console.ReadLine();
                    double result = rest.GetSingle<double>(charName, "artifact/ArtifactAverageCostByCharacterName");
                    Console.WriteLine($"\nAverage cost of {charName}'s artifacts: {result}");
                    Console.WriteLine("\n***Task completed!***");
                }
                else if (method == "ArtifactStatistics")
                {
                    Console.WriteLine("Artifact statistics: ");
                    var result = rest.Get<KeyValuePair<double, int>>("artifact/ArtifactStatistics");
                    foreach (var item in result)
                    {
                        Console.WriteLine($"\nThe average cost of all artifacts: {item.Key}");
                        Console.WriteLine($"The highest cost of artifacts: {item.Value}");
                    }
                    Console.WriteLine("\n***Task completed!***");
                }
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
            else if (entity == "Weapon")
            {
                if (method == "HighestDMGWeaponUser")
                {
                    Console.WriteLine("Character(s) using the weapon with the highest peak DMG: \n");
                    var result = rest.Get<string>("weapon/HighestDMGWeaponUser");
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\n***Character(s) listed!***");
                    //Console.WriteLine("Press ENTER to continue...");
                }
                else if (method == "WeaponAverageDMGByCharacterName")
                {
                    Console.Write("Enter the desired character's name: ");
                    string name = Console.ReadLine();
                    double result = rest.GetSingle<double>(name, "weapon/WeaponAverageDMGByCharacterName");
                    Console.WriteLine($"\nAverage peak DMG of {name}'s weapons: {result}");
                    Console.WriteLine("\n***Task completed!***");
                }
                else if (method == "WeaponStatistics")
                {
                    Console.WriteLine("Weapon statistics: ");
                    var result = rest.Get<KeyValuePair<double, int>>("weapon/WeaponStatistics");
                    foreach (var item in result)
                    {
                        Console.WriteLine($"\nThe average peak DMG of all weapons: {item.Key}");
                        Console.WriteLine($"The highest peak DMG of weapons: {item.Value}");
                    }
                    Console.WriteLine("\n***Task completed!***");
                }
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:8160/", "character");

            var characterSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Character"))
                .Add("Create", () => Create("Character"))
                .Add("Update", () => Update("Character"))
                .Add("Delete", () => Delete("Character"))
                .Add("ArtifactsOfGivenElement", () => NonCruds("Character", "ArtifactsOfGivenElement"))
                .Add("WeaponsOfGivenElement", () => NonCruds("Character", "WeaponsOfGivenElement"))
                .Add("Exit", ConsoleMenu.Close);

            var artifactSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Artifact"))
                .Add("Create", () => Create("Artifact"))
                .Add("Update", () => Update("Artifact"))
                .Add("Delete", () => Delete("Artifact"))
                .Add("HighestCostArtifactUser", () => NonCruds("Artifact", "HighestCostArtifactUser"))
                .Add("ArtifactAverageCostByCharacterName", () => NonCruds("Artifact", "ArtifactAverageCostByCharacterName"))
                .Add("ArtifactStatistics", () => NonCruds("Artifact", "ArtifactStatistics"))
                .Add("Exit", ConsoleMenu.Close);

            var weaponSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Weapon"))
                .Add("Create", () => Create("Weapon"))
                .Add("Update", () => Update("Weapon"))
                .Add("Delete", () => Delete("Weapon"))
                .Add("HighestDMGWeaponUser", () => NonCruds("Weapon", "HighestDMGWeaponUser"))
                .Add("WeaponAverageDMGByCharacterName", () => NonCruds("Weapon", "WeaponAverageDMGByCharacterName"))
                .Add("WeaponStatistics", () => NonCruds("Weapon", "WeaponStatistics"))
                .Add("Exit", ConsoleMenu.Close);

            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Characters", () => characterSubMenu.Show())
                .Add("Artifacts", () => artifactSubMenu.Show())
                .Add("Weapons", () => weaponSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();
        }
    }
}
