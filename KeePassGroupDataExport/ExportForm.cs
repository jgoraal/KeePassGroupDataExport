using System;
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
            checkedListBox1 = new CheckedListBox();
            label1 = new Label();
            checkBox1 = new CheckBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label4 = new Label();
            listBox1 = new ListBox();
            checkBox2 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 28);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(191, 139);
            checkedListBox1.TabIndex = 0;
            checkedListBox1.ItemCheck += CheckedListBox_ItemCheck;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(82, 13);
            label1.TabIndex = 1;
            label1.Text = "Wybrane wpisy:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 200);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(124, 17);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Wypełnić puste pola";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Click += CheckBox1OnClick;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(144, 198);
            textBox1.MaxLength = 10;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(153, 20);
            textBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 179);
            label2.Name = "label2";
            label2.Size = new Size(38, 13);
            label2.TabIndex = 4;
            label2.Text = "Opcje:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(144, 179);
            label4.Name = "label4";
            label4.Size = new Size(151, 13);
            label4.TabIndex = 6;
            label4.Text = "Tekst wypełnienia pustych pól";
            // 
            // listBox1
            // 
            listBox1.Enabled = false;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 246);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(191, 95);
            listBox1.TabIndex = 7;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(12, 224);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(191, 17);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Kolejność eksportowancyh danych";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.Click += CheckBox2OnClick;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Enabled = false;
            button1.Location = new Point(209, 262);
            button1.Name = "button1";
            button1.Size = new Size(62, 23);
            button1.TabIndex = 9;
            button1.Text = "Wyżej";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ButtonUpOnClick;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.Enabled = false;
            button2.Location = new Point(209, 291);
            button2.Name = "button2";
            button2.Size = new Size(62, 23);
            button2.TabIndex = 10;
            button2.Text = "Niżej";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ButtonDownOnClick;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.Location = new Point(80, 359);
            button3.Name = "button3";
            button3.Size = new Size(83, 23);
            button3.TabIndex = 11;
            button3.Text = "Eksportuj";
            button3.UseVisualStyleBackColor = true;
            button3.Click += ButtonExport_Click;
            // 
            // button4
            // 
            button4.AutoSize = true;
            button4.Location = new Point(169, 359);
            button4.Name = "button4";
            button4.Size = new Size(83, 23);
            button4.TabIndex = 12;
            button4.Text = "Przerwij";
            button4.UseVisualStyleBackColor = true;
            button4.Click += ButtonCancel_Click;
            // 
            // button5
            // 
            button5.Location = new Point(209, 43);
            button5.Name = "button5";
            button5.Size = new Size(103, 22);
            button5.TabIndex = 13;
            button5.Text = "Zaznacz wszystko";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(209, 71);
            button6.Name = "button6";
            button6.Size = new Size(103, 22);
            button6.TabIndex = 14;
            button6.Text = "Odznacz wszystko";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // ExportForm
            // 
            AutoSize = true;
            ClientSize = new Size(316, 396);
            Controls.Add(button6);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox2);
            Controls.Add(listBox1);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Controls.Add(checkedListBox1);
            Controls.Add(button5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Opcje eksportu";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button5;
        private Button button6;


        private Label label4;
        private ListBox listBox1;
        private CheckBox checkBox2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;

        private CheckedListBox checkedListBox1;
        private Label label1;
        private CheckBox checkBox1;
        private TextBox textBox1;
        private Label label2;

        public bool FillEmptyFields { get; private set; }
        public string FillEmptyFieldsText { get; private set; }
        public HashSet<string> ExportOrderKeys { get; private set; }
        
        public bool ExportOrder { get; private set; }

        private void AddItemsToCheckListBox(HashSet<string> keys)
        {
            checkedListBox1.Items.Clear();
            listBox1.Items.Clear();

            foreach (var item in keys)
            {
                checkedListBox1.Items.Add(item, true);
                if (!listBox1.Items.Contains(item))
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            SelectedKeys = checkedListBox1.CheckedItems.Cast<string>().ToHashSet();
            FillEmptyFields = checkBox1.Checked;
            ExportOrder = checkBox2.Checked;

            ExportOrderKeys = ExportOrder ? listBox1.Items.Cast<string>().ToHashSet() : SelectedKeys;
            FillEmptyFieldsText = FillEmptyFields ? textBox1.Text.Trim() : string.Empty;

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
            if (e.Index >= 0 && e.Index < checkedListBox1.Items.Count)
            {
                string item = checkedListBox1.Items[e.Index].ToString();

                if (e.NewValue == CheckState.Checked)
                {
                    if (!listBox1.Items.Contains(item))
                    {
                        listBox1.Items.Add(item);
                    }
                }
                else
                {
                    listBox1.Items.Remove(item);
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
            listBox1.Enabled = checkBox2.Checked;
            button1.Enabled = checkBox2.Checked;
            button2.Enabled = checkBox2.Checked;
        }

        private void CheckBox1OnClick(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

        private void MoveItemInDirection(bool direction)
        {
            if (listBox1.SelectedItem == null || listBox1.SelectedIndex < 0) return;

            int newIndex = listBox1.SelectedIndex + (direction ? -1 : 1);

            if (newIndex < 0 || newIndex >= listBox1.Items.Count) return;

            object keyName = listBox1.SelectedItem;
            listBox1.Items.Remove(keyName);
            listBox1.Items.Insert(newIndex, keyName);
            listBox1.SetSelected(newIndex, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i,true);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i,false);
            }
        }
    }
}