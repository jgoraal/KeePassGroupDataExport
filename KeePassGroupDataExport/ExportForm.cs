﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KeePassGroupDataExport
{
    internal sealed class ExportForm : Form
    {
        public HashSet<string> SelectedKeys { get; private set; }

        internal ExportForm(HashSet<string> keys)
        {
            InitializeComponent();
            AddItemsToCheckListBox(keys);
            CenterToParent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _checkedListBox1 = new CheckedListBox();
            _label1 = new Label();
            _checkBox1 = new CheckBox();
            _textBox1 = new TextBox();
            _label2 = new Label();
            _label4 = new Label();
            _listBox1 = new ListBox();
            _checkBox2 = new CheckBox();
            _button1 = new Button();
            _button2 = new Button();
            _button3 = new Button();
            _button4 = new Button();
            _button5 = new Button();
            _button6 = new Button();
            _checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            _checkedListBox1.CheckOnClick = true;
            _checkedListBox1.FormattingEnabled = true;
            _checkedListBox1.Location = new Point(12, 28);
            _checkedListBox1.Name = "_checkedListBox1";
            _checkedListBox1.Size = new Size(191, 139);
            _checkedListBox1.TabIndex = 0;
            _checkedListBox1.ItemCheck += CheckedListBox_ItemCheck;
            // 
            // label1
            // 
            _label1.AutoSize = true;
            _label1.Location = new Point(12, 9);
            _label1.Name = "_label1";
            _label1.Size = new Size(82, 13);
            _label1.TabIndex = 1;
            _label1.Text = "Wybrane wpisy:";
            // 
            // checkBox1
            // 
            _checkBox1.AutoSize = true;
            _checkBox1.Location = new Point(12, 200);
            _checkBox1.Name = "_checkBox1";
            _checkBox1.Size = new Size(124, 17);
            _checkBox1.TabIndex = 2;
            _checkBox1.Text = "Wypełnić puste pola";
            _checkBox1.UseVisualStyleBackColor = true;
            _checkBox1.Click += CheckBox1OnClick;
            // 
            // textBox1
            // 
            _textBox1.Enabled = false;
            _textBox1.Location = new Point(144, 198);
            _textBox1.MaxLength = 10;
            _textBox1.Name = "_textBox1";
            _textBox1.Size = new Size(153, 20);
            _textBox1.TabIndex = 3;
            // 
            // label2
            // 
            _label2.AutoSize = true;
            _label2.Location = new Point(12, 179);
            _label2.Name = "_label2";
            _label2.Size = new Size(38, 13);
            _label2.TabIndex = 4;
            _label2.Text = "Opcje:";
            // 
            // label4
            // 
            _label4.AutoSize = true;
            _label4.Location = new Point(144, 179);
            _label4.Name = "_label4";
            _label4.Size = new Size(151, 13);
            _label4.TabIndex = 6;
            _label4.Text = "Tekst wypełnienia pustych pól";
            // 
            // listBox1
            // 
            _listBox1.Enabled = false;
            _listBox1.FormattingEnabled = true;
            _listBox1.Location = new Point(12, 269);
            _listBox1.Name = "_listBox1";
            _listBox1.Size = new Size(191, 95);
            _listBox1.TabIndex = 7;
            // 
            // checkBox2
            // 
            _checkBox2.AutoSize = true;
            _checkBox2.Location = new Point(12, 246);
            _checkBox2.Name = "_checkBox2";
            _checkBox2.Size = new Size(191, 17);
            _checkBox2.TabIndex = 8;
            _checkBox2.Text = "Kolejność eksportowancyh danych";
            _checkBox2.UseVisualStyleBackColor = true;
            _checkBox2.Click += CheckBox2OnClick;
            // 
            // button1
            // 
            _button1.AutoSize = true;
            _button1.Enabled = false;
            _button1.Location = new Point(209, 290);
            _button1.Name = "_button1";
            _button1.Size = new Size(62, 23);
            _button1.TabIndex = 9;
            _button1.Text = "Wyżej";
            _button1.UseVisualStyleBackColor = true;
            _button1.Click += ButtonUpOnClick;
            // 
            // button2
            // 
            _button2.AutoSize = true;
            _button2.Enabled = false;
            _button2.Location = new Point(209, 319);
            _button2.Name = "_button2";
            _button2.Size = new Size(62, 23);
            _button2.TabIndex = 10;
            _button2.Text = "Niżej";
            _button2.UseVisualStyleBackColor = true;
            _button2.Click += ButtonDownOnClick;
            // 
            // button3
            // 
            _button3.AutoSize = true;
            _button3.Location = new Point(80, 381);
            _button3.Name = "_button3";
            _button3.Size = new Size(83, 23);
            _button3.TabIndex = 11;
            _button3.Text = "Eksportuj";
            _button3.UseVisualStyleBackColor = true;
            _button3.Click += ButtonExport_Click;
            // 
            // button4
            // 
            _button4.AutoSize = true;
            _button4.Location = new Point(169, 381);
            _button4.Name = "_button4";
            _button4.Size = new Size(83, 23);
            _button4.TabIndex = 12;
            _button4.Text = "Przerwij";
            _button4.UseVisualStyleBackColor = true;
            _button4.Click += ButtonCancel_Click;
            // 
            // button5
            // 
            _button5.Location = new Point(209, 43);
            _button5.Name = "_button5";
            _button5.Size = new Size(103, 22);
            _button5.TabIndex = 13;
            _button5.Text = "Zaznacz wszystko";
            _button5.UseVisualStyleBackColor = true;
            _button5.Click += button5_Click;
            // 
            // button6
            // 
            _button6.Location = new Point(209, 71);
            _button6.Name = "_button6";
            _button6.Size = new Size(103, 22);
            _button6.TabIndex = 14;
            _button6.Text = "Odznacz wszystko";
            _button6.UseVisualStyleBackColor = true;
            _button6.Click += button6_Click;
            // 
            // checkBox3
            // 
            _checkBox3.AutoSize = true;
            _checkBox3.Location = new Point(12, 223);
            _checkBox3.Name = "_checkBox3";
            _checkBox3.Size = new Size(128, 17);
            _checkBox3.TabIndex = 15;
            _checkBox3.Text = "Otwórz plik po zapisie";
            _checkBox3.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            AutoSize = true;
            ClientSize = new Size(320, 420);
            Controls.Add(_checkBox3);
            Controls.Add(_button6);
            Controls.Add(_button4);
            Controls.Add(_button3);
            Controls.Add(_button2);
            Controls.Add(_button1);
            Controls.Add(_checkBox2);
            Controls.Add(_listBox1);
            Controls.Add(_label4);
            Controls.Add(_label2);
            Controls.Add(_textBox1);
            Controls.Add(_checkBox1);
            Controls.Add(_label1);
            Controls.Add(_checkedListBox1);
            Controls.Add(_button5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Opcje eksportu";
            ResumeLayout(false);
            PerformLayout();
        }

        private CheckBox _checkBox3;

        private Button _button5;
        private Button _button6;
        
        private Label _label4;
        private ListBox _listBox1;
        private CheckBox _checkBox2;
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Button _button4;

        private CheckedListBox _checkedListBox1;
        private Label _label1;
        private CheckBox _checkBox1;
        private TextBox _textBox1;
        private Label _label2;

        public bool FillEmptyFields { get; private set; }
        public string FillEmptyFieldsText { get; private set; }
        public HashSet<string> ExportOrderKeys { get; private set; }
        public bool ExportOrder { get; private set; }
        public bool OpenFileAfterSave { get; private set; }

        private void AddItemsToCheckListBox(HashSet<string> keys)
        {
            _checkedListBox1.Items.Clear();
            _listBox1.Items.Clear();

            foreach (var item in keys)
            {
                _checkedListBox1.Items.Add(item, true);
                if (!_listBox1.Items.Contains(item))
                {
                    _listBox1.Items.Add(item);
                }
            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            SelectedKeys = _checkedListBox1.CheckedItems.Cast<string>().ToHashSet();
            FillEmptyFields = _checkBox1.Checked;
            ExportOrder = _checkBox2.Checked;
            OpenFileAfterSave = _checkBox3.Checked;
            
            ExportOrderKeys = ExportOrder ? _listBox1.Items.Cast<string>().ToHashSet() : SelectedKeys;
            FillEmptyFieldsText = FillEmptyFields ? _textBox1.Text.Trim() : string.Empty;

            if (ExportOrderKeys == null || SelectedKeys == null || ExportOrderKeys.Count == 0 ||
                SelectedKeys.Count == 0)
            {
                Close();
                throw new ArgumentException(ErrorMessages.NoDataToExport);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index >= 0 && e.Index < _checkedListBox1.Items.Count)
            {
                string item = _checkedListBox1.Items[e.Index].ToString();

                if (e.NewValue == CheckState.Checked)
                {
                    if (!_listBox1.Items.Contains(item))
                    {
                        _listBox1.Items.Add(item);
                    }
                }
                else
                {
                    _listBox1.Items.Remove(item);
                }
            }
        }

        private void ButtonDownOnClick(object sender, EventArgs e)
        {
            MoveItemInDirection(false);
        }

        private void ButtonUpOnClick(object sender, EventArgs e)
        {
            MoveItemInDirection(true);
        }

        private void CheckBox2OnClick(object sender, EventArgs e)
        {
            _listBox1.Enabled = _checkBox2.Checked;
            _button1.Enabled = _checkBox2.Checked;
            _button2.Enabled = _checkBox2.Checked;
        }

        private void CheckBox1OnClick(object sender, EventArgs e)
        {
            _textBox1.Enabled = _checkBox1.Checked;
        }

        private void MoveItemInDirection(bool direction)
        {
            if (_listBox1.SelectedItem == null || _listBox1.SelectedIndex < 0) return;

            int newIndex = _listBox1.SelectedIndex + (direction ? -1 : 1);

            if (newIndex < 0 || newIndex >= _listBox1.Items.Count) return;

            object keyName = _listBox1.SelectedItem;
            _listBox1.Items.Remove(keyName);
            _listBox1.Items.Insert(newIndex, keyName);
            _listBox1.SetSelected(newIndex, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _checkedListBox1.Items.Count; i++)
            {
                _checkedListBox1.SetItemChecked(i,true);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _checkedListBox1.Items.Count; i++)
            {
                _checkedListBox1.SetItemChecked(i,false);
            }
        }

        
    }
}