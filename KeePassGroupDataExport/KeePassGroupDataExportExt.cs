using KeePass.Plugins;
using System;
using System.Drawing;
using System.Reflection;
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
        
        
        /// <summary>
        /// Tworzy element menu dla pluginu.
        /// </summary>
        /// <returns>ToolStripMenuItem dla pluginu.</returns>
        public override ToolStripMenuItem GetMenuItem(PluginMenuType t)
        {
            if (t == PluginMenuType.Group)
            {
                var tsMenuItem = new ToolStripMenuItem();
                tsMenuItem.Text = "Eksportuj Wybraną Grupę";
                GetIconStream(tsMenuItem, "KeePassGroupDataExport.Resources.group_icon.png");
                tsMenuItem.Click += ToolsMenuItemClick;

                var tsMenuItemByTag = new ToolStripMenuItem();
                tsMenuItemByTag.Text = "Eksportuj Po Tagu";
                GetIconStream(tsMenuItemByTag, "KeePassGroupDataExport.Resources.tag_icon.png");
                tsMenuItemByTag.Click += ToolsMenuItemByTagClick;

                var mainMenuItem = new ToolStripMenuItem("Opcje Eksportu");
                GetIconStream(mainMenuItem, "KeePassGroupDataExport.Resources.export_icon.png");
                mainMenuItem.DropDownItems.Add(tsMenuItem);
                mainMenuItem.DropDownItems.Add(tsMenuItemByTag);

                return mainMenuItem;
            }

            return null;
        }

        private void ToolsMenuItemByTagClick(object sender, EventArgs e)
        {
            try
            {
                var exporter = new GroupsExporter(Host);
                exporter.SelectedTagCheckMessage();
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

        /// <summary>
        /// Obsługuje kliknięcie elementu menu.
        /// </summary>
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


        private void GetIconStream(ToolStripMenuItem menu,string resource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    menu.Image = Image.FromStream(stream);
                }
            }
        }
    }
}