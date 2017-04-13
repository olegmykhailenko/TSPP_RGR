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
    public partial class SignInForm : Form
    {
        private MainForm parent;

        public SignInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                string[] users = System.IO.File.ReadAllLines("users.txt");
                if (users != null)
                {
                    foreach (string userData in users)
                    {
                        string[] user = userData.Split(' ');
                        if (emailTextBox.Lines.ToString() == user[1] && passwordTextBox.Lines.ToString() == user[2])
                        {

                            parent.setUser(user[0]);
                            this.Close();
                        }
                    }
                }
                //MessageBox.Show("Wrong password!");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No data found");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SignUpForm aForm = new SignUpForm();
            aForm.ShowDialog();
        }

        public SignInForm(MainForm mainForm)
        {
            InitializeComponent();
            parent = mainForm;
        }
    }
}
