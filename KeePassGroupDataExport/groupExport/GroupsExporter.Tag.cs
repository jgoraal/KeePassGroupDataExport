﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KeePassLib;

namespace KeePassGroupDataExport.groupExport
{
    internal partial class GroupsExporter
    {
        internal void SelectedTagCheckMessage()
        {
            _selectedGroup = GetSelectedGroup();

            _selectedSubGroups = GetSubGroups(_selectedGroup, MaxSubGroupDepth);
            ConfirmSelectedTag();
        }

        private void ConfirmSelectedTag()
        {
            var uniqueTags = GetUniqueTags();
            using (var tagForm = new TagSelectionForm(uniqueTags))
            {
                if (tagForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedTag = tagForm.SelectedTag;
                    if (!string.IsNullOrEmpty(selectedTag))
                    {
                        _entries = GetEntriesByTag(selectedTag);
                        MessageCreator.CreateInfoMessage("Znaleziony wpisy!",
                            $"Znaleziono {_entries.Count} wpisy po tagu: {selectedTag}");
                        ReadDataBeforeExport();
                    }
                    else
                    {
                        MessageCreator.CreateWarningMessage(ErrorMessages.OperationCancelledError);
                    }
                }
                else
                {
                    MessageCreator.CreateWarningMessage(ErrorMessages.OperationCancelledError);
                }
            }
        }

        private List<PwEntry> GetEntriesByTag(string tag)
        {
            var entries = _selectedSubGroups
                .SelectMany(subGroup => subGroup.Entries)
                .Where(entry => entry.Tags != null && entry.Tags.Contains(tag))
                .ToList();


            if (_selectedGroup.Entries.Any())
            {
                entries.AddRange(
                    _selectedGroup.Entries
                        .Where(entry => entry.Tags != null && entry.Tags.Contains(tag))
                );
            }


            if (!entries.Any())
                throw new ApplicationException(ErrorMessages.EntriesNotFoundError);


            return entries;
        }

        private List<string> GetUniqueTags()
        {
            var unique = _selectedSubGroups
                .SelectMany(subGroup => subGroup.Entries)
                .SelectMany(entry => entry?.Tags ?? new List<string>())
                .Distinct()
                .ToList();

            if (_selectedGroup.Entries.Any())
            {
                unique.AddRange(
                    _selectedGroup.Entries
                        .SelectMany(entry => entry.Tags ?? new List<string>())
                        .Distinct()
                );
            }

            if (!unique.Any())
            {
                throw new ApplicationException(ErrorMessages.TagsNotFoundError);
            }

            return unique;
        }
    }
}