using System;
using System.Collections.Generic;
using KeePassLib;

namespace KeePassGroupDataExport.groupExport
{
    internal partial class GroupsExporter
    {
        private PwGroup GetSelectedGroup()
        {
            return _host.MainWindow.GetSelectedGroup() ??
                   throw new NullReferenceException(ErrorMessages.NoneGroupSelectedError);
        }

        //BFS algorytm
        private List<PwGroup> GetSubGroups(PwGroup rootGroup, int maxDepth)
        {
            var allSubGroups = new List<PwGroup>();
            var groupsToProcess = new Queue<PwGroup>();
            var groupDepths = new Queue<int>();

            // Dodajemy początkową grupę do kolejki z głębokością 0
            groupsToProcess.Enqueue(rootGroup);
            groupDepths.Enqueue(0);

            while (groupsToProcess.Count > 0)
            {
                // Pobieramy bieżącą grupę i jej głębokość z kolejki
                var currentGroup = groupsToProcess.Dequeue();
                var currentDepth = groupDepths.Dequeue();

                if (currentDepth < maxDepth)
                {
                    foreach (var subGroup in currentGroup.Groups)
                    {
                        allSubGroups.Add(subGroup);
                        groupsToProcess.Enqueue(subGroup);
                        groupDepths.Enqueue(currentDepth + 1);
                    }
                }
            }

            return allSubGroups;
        }

        // Potwierdzenie wybranej grupy przez użytkownika
        private void ConfirmSelectedGroup()
        {
            const string title = "Wybór Grupy";
            string content =
                $"Czy wybrano grupę {_selectedGroup.Name.Trim()}?\n\n{GetSubGroupsNames(_selectedGroup.Name.Trim())}";

            bool checkGroupSelection = MessageCreator.CreateQuestionMessage(title, content);

            if (checkGroupSelection)
                CreateSubGroupsCheckMessage();
            else
                MessageCreator.CreateWarningMessage(ErrorMessages.OperationCancelledError);
        }

        // Obsługa podgrup
        private void CreateSubGroupsCheckMessage()
        {
            _entries = GetEntries();

            bool cancelEntriesOperation = MessageCreator.CreateQuestionMessage($"Znaleziono wpisy!", GetFoundEntries());

            if (cancelEntriesOperation)
                ReadDataBeforeExport();
            else
                MessageCreator.CreateWarningMessage(ErrorMessages.OperationCancelledError);
        }
    }
}