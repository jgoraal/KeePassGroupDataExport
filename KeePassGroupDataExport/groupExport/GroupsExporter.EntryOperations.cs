using System;
using System.Collections.Generic;
using System.Linq;
using KeePassLib;

namespace KeePassGroupDataExport.groupExport
{
    internal partial class GroupsExporter
    {
        private List<PwEntry> GetEntries()
        {
            var entries = _selectedSubGroups
                .SelectMany(subGroup => subGroup.Entries)
                .Where(entry => entry.Strings.ReadSafe(PwDefs.TitleField) == entry.ParentGroup.Name)
                .ToList();

            if (!entries.Any())
                throw new ApplicationException(ErrorMessages.EntriesNotFoundError);

            return entries;
        }
        
        private ILookup<string, Dictionary<string, string>> GetEntriesData()
        {
            var data = _entries.ToLookup(
                group => group.ParentGroup.Name.Trim(),
                item => item.Strings
                    .Where(pair => !pair.Key.Equals("Notes") && !pair.Key.Equals("Password") && !pair.Key.Equals("URL"))
                    .OrderByDescending(pair => pair.Key)
                    .ToDictionary(key => key.Key.Trim(), value => value.Value.ReadString().Trim())
            );

            if (!data.Any())
                throw new ApplicationException(ErrorMessages.EntriesNotFoundError);

            return data;
        }
        
        private List<ComputerData> GetComputersData()
        {
            if (ComputerData.AllKeys != null && ComputerData.AllKeys.Any())
            {
                ComputerData.AllKeys.Clear();
            }
            
            if (ComputerData.ExportKeys != null && ComputerData.ExportKeys.Any())
            {
                ComputerData.ExportKeys.Clear();
            }
            
            
            var computers = new List<ComputerData>();

            foreach (var group in _entriesData)
            {
                string groupName = group.Key;

                foreach (var entry in group)
                {
                    var computer = new ComputerData(groupName);

                    foreach (var pair in entry)
                    {
                        computer.AddAdvancedInformation(pair.Key, pair.Value);
                    }

                    computers.Add(computer);
                }
            }

            if (!computers.Any())
                throw new ApplicationException(ErrorMessages.EntriesNotFoundError);
            
            return computers;
        }
    }
}