using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandscapeEditor
{
    public partial class newFileForm : Form
    {
        private MainForm parent;
        public newFileForm(MainForm newParent)
        {
            parent = newParent;
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                bool flag = true;
                if (parent.isContainMap())
                {
                    Form form1 = new Form();
                    Button button1 = new Button();
                    Button button2 = new Button();
                    Label label1 = new Label();
   
                    label1.Location = new Point(10, 10);
                    label1.Text = "Unsaved data will be lost! Continue?";
                    button1.Text = "OK";
                    button2.Text = "Cancel";
                    label1.Width = 200;
                    button1.Location = new Point(label1.Left, label1.Height + label1.Top + 10);
                    button2.Location = new Point(button1.Left + button1.Width + 10, label1.Height + label1.Top + 10);
                    button1.DialogResult = DialogResult.OK;
                    button2.DialogResult = DialogResult.Cancel;
                    form1.Width = (label1.Left * 2) + label1.Width;
                    form1.Height = 120;

                    form1.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form1.AcceptButton = button1;
                    form1.CancelButton = button2;
                    form1.StartPosition = FormStartPosition.CenterParent;

                    form1.Controls.Add(button1);
                    form1.Controls.Add(button2);
                    form1.Controls.Add(label1);
                    form1.ShowDialog();
                    if (form1.DialogResult != DialogResult.OK)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    parent.createNewMap(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                    this.Dispose();
                }
                else
                    this.Dispose();
            }
            else
                MessageBox.Show("Enter data first!");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void size_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
