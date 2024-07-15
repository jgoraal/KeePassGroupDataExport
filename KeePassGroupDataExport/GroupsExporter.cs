using System.Text;

namespace KeePassGroupDataExport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KeePass.Plugins;
    using KeePassLib;

    namespace KeePassGroupDataExport
    {
        internal class GroupsExporter
        {
            private readonly IPluginHost _host;
            private PwGroup _selectedGroup;
            private List<PwGroup> _selectedSubGroups;

            private const int MaxSubGroupDepth = 4;

            internal GroupsExporter(IPluginHost host)
            {
                _host = host ?? throw new ArgumentNullException(ErrorMessages.HostError);
            }

            // Sprawdzanie wybranej grupy
            internal void SelectedGroupCheckMessage()
            {
                _selectedGroup = GetSelectedGroup();

                _selectedSubGroups = GetSubGroups(_selectedGroup, MaxSubGroupDepth);
                ConfirmSelectedGroup();
            }

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

            // Pobieranie nazw podgrup do wyświetlenia
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

            // Obsługa podgrup
            private void CreateSubGroupsCheckMessage()
            {
                throw new NotImplementedException();
            }
        }
    }
}