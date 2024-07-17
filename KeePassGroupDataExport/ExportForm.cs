using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace KeePassGroupDataExport
{
    internal sealed class ExportForm : Form
    {
        private CheckedListBox _checkedListBox;
        
        private Button _buttonExport;
        private Button _buttonCancel;

        private Label _labelKeys;
        
        public HashSet<string> SelectedKeys { get; private set; }

        internal ExportForm(HashSet<string> keys)
        {
            InitializeComponent();
            AddItemsToCheckListBox(keys);
            CenterToParent();
        }

        private void InitializeComponent()
        {
            _checkedListBox = new CheckedListBox();

            _buttonExport = new Button();
            _buttonCancel = new Button();

            _labelKeys = new Label();
            
            SuspendLayout();

            _checkedListBox.FormattingEnabled = true;
            _checkedListBox.Location = new Point(12, 25);
            _checkedListBox.AutoSize = true;
            
            _buttonExport.Location = new Point(116, 370);
            _buttonExport.Size = new Size(75, 23);
            _buttonExport.Text = "Export";
            _buttonExport.UseVisualStyleBackColor = true;
            _buttonExport.Click += ButtonExport_Click;
            
            _buttonCancel.Location = new Point(197, 370);
            _buttonCancel.Size = new Size(75, 23);
            _buttonCancel.Text = "Cancel";
            _buttonCancel.UseVisualStyleBackColor = true;
            _buttonCancel.Click += ButtonCancel_Click;
            
            _labelKeys.AutoSize = true;
            _labelKeys.Location = new Point(12, 9);
            _labelKeys.Size = new Size(80, 13);
            _labelKeys.Text = "Wybierz wpisy:";
            
            ClientSize = ClientSize = new Size(300, 300);
            Text = "Opcje Eksportu";
            ResumeLayout(false);
            PerformLayout();
        }
        
        private void AddItemsToCheckListBox(HashSet<string> keys)
        {
            foreach (var item in keys)
            {
                _checkedListBox.Items.Add(item, true);
            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            SelectedKeys = _checkedListBox.CheckedItems.Cast<string>().ToHashSet();
            
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}