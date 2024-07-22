using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeePassLib;

namespace KeePassGroupDataExport.groupExport
{
    internal partial class GroupsExporter
    {
        
        
        /// <summary>
        /// Pobiera nazwy podgrup do wyświetlenia.
        /// </summary>
        private string GetSubGroupsNames(string group)
        {
            const int maxDisplayedSubGroups = 4;
            var result = new StringBuilder($"Wybrana Grupa:\n{group}:\n");

            var subGroups = _selectedSubGroups.Take(maxDisplayedSubGroups).ToList();
            foreach (var subGroup in subGroups)
            {
                result.AppendLine($" -> {subGroup.Name.Trim()}");
            }

            int remainingSubGroups = _selectedSubGroups.Count - maxDisplayedSubGroups;
            if (remainingSubGroups > 0)
            {
                result.AppendLine($" -> (jeszcze {remainingSubGroups})...");
            }

            return result.ToString();
        }
        
        
        /// <summary>
        /// Pobiera podsumowanie znalezionych wpisów.
        /// </summary>
        private string GetFoundEntries()
        {
            const int maxEntriesNames = 4;
            var dividedEntries = _entries.Take(maxEntriesNames).ToList();
            int entriesCount = _entries.Count;

            var result = new StringBuilder("Czy chcesz kontynuować?\n\n");
            result.AppendLine($"Znaleziono {entriesCount} wpisy:\n");
            result.Append(string.Join(", ", dividedEntries.Select(e => e.Strings.ReadSafe(PwDefs.TitleField).Trim())));

            int remainingEntries = entriesCount - maxEntriesNames;

            if (remainingEntries > 0)
                result.Append($", oraz {remainingEntries} więcej");

            return result.ToString();
        }
        
        
        /// <summary>
        /// Metoda debugowania do odczytu danych.
        /// </summary>
        private void ReadDataDebug()
        {
            foreach (var group in _entriesData)
            {
                string groupName = group.Key;
                var keys = new List<string>();
                var values = new List<string>();

                foreach (var entryData in group)
                {
                    foreach (var pair in entryData)
                    {
                        keys.Add(pair.Key);
                        values.Add(pair.Value);
                    }
                }

                MessageCreator.CreateInfoMessage("Info", groupName, "Klucz: " + string.Join(", ", keys),
                    "Wartość: " + string.Join(", ", values));
            }
        }
        
        
        /// <summary>
        /// Wyświetla formularz danych komputera.
        /// </summary>
        private void ShowComputerDataForm()
        {
            foreach (var computer in _computers)
            {
                MessageCreator.CreateInfoMessage("Informacje o znalezionych wpisach!",computer.ToString());
            }
        }
    }
}