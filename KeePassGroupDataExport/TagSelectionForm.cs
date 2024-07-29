using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KeePassGroupDataExport
{
    internal sealed class TagSelectionForm : Form
    {
        public string SelectedTag { get; private set; }

        public TagSelectionForm(List<string> tags)
        {
            InitializeComponent();
            comboBoxTags.Items.AddRange(tags.ToArray());
            if (comboBoxTags.Items.Count > 0)
            {
                comboBoxTags.SelectedIndex = 0; 
            }
            CenterToParent();
        }

        private ComboBox comboBoxTags;
        private Button buttonOk;
        private Button buttonCancel;

        private void InitializeComponent()
        {
            this.comboBoxTags = new ComboBox();
            this.buttonOk = new Button();
            this.buttonCancel = new Button();
            this.SuspendLayout();

            // 
            // comboBoxTags
            // 
            this.comboBoxTags.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxTags.FormattingEnabled = true;
            this.comboBoxTags.Location = new Point(12, 12);
            this.comboBoxTags.Name = "comboBoxTags";
            this.comboBoxTags.Size = new Size(260, 21);
            this.comboBoxTags.TabIndex = 0;

            // 
            // buttonOk
            // 
            this.buttonOk.Location = new Point(116, 39);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Zatwierdź";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new EventHandler(this.ButtonOk_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new Point(197, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Przerwij";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(this.ButtonCancel_Click);

            // 
            // TagSelectionForm
            // 
            this.ClientSize = new Size(284, 71);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxTags);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            this.Name = "TagSelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Wybierz Tag";
            Icon = null;
            this.ResumeLayout(false);
            PerformLayout();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            SelectedTag = comboBoxTags.SelectedItem?.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}