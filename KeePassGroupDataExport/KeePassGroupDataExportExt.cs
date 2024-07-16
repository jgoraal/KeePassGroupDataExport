using KeePass.Plugins;
using System;
using System.Windows.Forms;
using KeePassGroupDataExport.groupExport;

namespace KeePassGroupDataExport
{
    public sealed class KeePassGroupDataExportExt : Plugin
    {
        private IPluginHost Host { get; set; }

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
            {
                MessageCreator.CreateErrorMessage(ErrorMessages.HostError);
                return false;
            }

            Host = host;

            var tsMenuItem = GetMenuItem(PluginMenuType.Group);
            if (tsMenuItem == null)
            {
                MessageCreator.CreateErrorMessage(ErrorMessages.MenuCreationError);
                return false;
            }

            return true;
        }

        public override void Terminate()
        {
        }

        public override ToolStripMenuItem GetMenuItem(PluginMenuType t)
        {
            if (t == PluginMenuType.Group)
            {
                var tsMenuItem = new ToolStripMenuItem();
                tsMenuItem.Text = "Eksportuj Wybraną Grupę";
                tsMenuItem.Click += ToolsMenuItemClick;
                return tsMenuItem;
            }

            return null;
        }

        private void ToolsMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var exporter = new GroupsExporter(Host);
                exporter.SelectedGroupCheckMessage();
            }
            catch (ArgumentNullException nullException)
            {
                MessageCreator.CreateErrorMessage(nullException.Message);
            }
            catch (ApplicationException applicationException)
            {
                MessageCreator.CreateErrorMessage(applicationException.Message);
            }
            catch (Exception others)
            {
                MessageCreator.CreateErrorMessage(others.Message);
            }
        }
    }
}