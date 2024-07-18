using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib;

namespace KeePassGroupDataExport.groupExport
{
    internal partial class GroupsExporter
    {
        private readonly IPluginHost _host;

        private PwGroup _selectedGroup;
        private List<PwGroup> _selectedSubGroups;
        private List<PwEntry> _entries;
        private ILookup<string, Dictionary<string, string>> _entriesData;
        private List<ComputerData> _computers;
        
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
        
        private void ReadDataBeforeExport()
        {
            _entriesData = GetEntriesData();

            PrepareData();

            // Debug only
            //ReadDataDebug();
        }
        
        private void PrepareData()
        {
            _computers = GetComputersData();

            CreateExportOptionsForm();
        }

        private void CreateExportOptionsForm()
        {
            ShowComputerDataForm();

            ShowExportDataForm();
        }

        private void ShowExportDataForm()
        {
            
            string fillEmptyFieldsText = "";
            using (var exportOptionsForm = new ExportForm(ComputerData.AllKeys))
            {
                if (exportOptionsForm.ShowDialog() != DialogResult.OK)
                {
                    MessageCreator.CreateWarningMessage(ErrorMessages.OperationCancelledError);
                    return;
                }

                if (exportOptionsForm.ExportOrder)
                {
                    ComputerData.ExportKeys = exportOptionsForm.ExportOrderKeys;
                    fillEmptyFieldsText = exportOptionsForm.FillEmptyFields
                        ? exportOptionsForm.FillEmptyFieldsText.Trim()
                        : string.Empty;
                }    
                
            }
            
            
            foreach (var computer in _computers)
            {
                computer.PrepareDataForExport(fillEmptyFieldsText);
            }

            foreach (var computer in _computers)
            {
                MessageCreator.CreateWarningMessage(computer.ExportDataCheck());
            }

        }
    }
}